using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static GameManager;
using static UnityEngine.GraphicsBuffer;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
   
    private int index = 0;

    [SerializeField] private SpawnFood spawnFood;
    [SerializeField] private PlayerMouvement playerMouvement;
    private ObjectPool pool;
    
    // Start is called before the first frame update
    private void Awake()
    {
        playerMouvement = GetComponent<PlayerMouvement>();
        pool = GetComponent<ObjectPool>();
    }
    void Start()
    {
        spawnFood.SpawningFood();
    }
    private void FixedUpdate()
    {
        if (GameManager.InstanGameManager.ActualState == GameManager.GameStates.Start ||
            GameManager.InstanGameManager.ActualState == GameManager.GameStates.GameOver)
        {
            return;
        }
        playerMouvement.FishMouvement();
    }
    // Update is called once per frame

    

    private void GrowFishAmount()
    {
        GameObject fish = ObjectPool.Instance.RequestFish();
        fish.SetActive(true);
        playerMouvement.BodyParts.Add(fish);
        StartCoroutine(WaitForSec(2f));
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Walls")
        {
            /*transform.position = new Vector3(0, 2.86f, 0);
            foreach (var item in BodyParts)
            {
                item.SetActive(false);

            }*/
            if (GameManager.InstanGameManager.ActualState == GameStates.GameOver)
            {
                return;
            }
            GameManager.InstanGameManager.ChangeState(GameStates.GameOver);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            Destroy(other.gameObject);
            spawnFood.SpawningFood();
            GrowFishAmount();
        }
        if (other.gameObject.tag == "Queue")
        {
            if (GameManager.InstanGameManager.ActualState == GameStates.GameOver)
            {
                return;
            }
            Debug.Log("collision detected!");
            GameManager.InstanGameManager.ChangeState(GameStates.GameOver);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
    private IEnumerator WaitForSec(float sec)
    {
        Debug.Log(index);
        playerMouvement.BodyParts[index].GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(sec);
        playerMouvement.BodyParts[index].GetComponent<BoxCollider>().enabled = true;
        index++;
        Debug.Log(index);
    }

}

