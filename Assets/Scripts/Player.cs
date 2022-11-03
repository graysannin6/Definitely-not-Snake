using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float steerSpeed = 180f;
    public float BodySpeed = 5f;
    public int Gap = 10;
    private int index = 0;

    [SerializeField] private SpawnFood spawnFood;

    private ObjectPool pool;
    public List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionHistory = new List<Vector3>();
    // Start is called before the first frame update
    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }
    void Start()
    {
        spawnFood.SpawningFood();
    }
    private void FixedUpdate()
    {
        FishMouvement();
    }
    // Update is called once per frame

    private void FishMouvement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        PositionHistory.Insert(0, transform.position);
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = PositionHistory[Mathf.Min(index * Gap, PositionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }


    private void GrowFishAmount()
    {
        GameObject fish = ObjectPool.Instance.RequestFish();
        fish.SetActive(true);
        BodyParts.Add(fish);
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
            Debug.Log("collision detected!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
    private IEnumerator WaitForSec(float sec)
    {
        Debug.Log(index);
        BodyParts[index].GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(sec);
        BodyParts[index].GetComponent<BoxCollider>().enabled = true;
        index++;
        Debug.Log(index);
    }

}

