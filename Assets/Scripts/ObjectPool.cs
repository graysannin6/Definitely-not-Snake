using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private GameObject fishContainer;
    [SerializeField] private List<GameObject> fishPool;
    [SerializeField] private int numberOfFishs;
    public static ObjectPool Instance
    {
        get 
        { 
            if (instance == null)
            {
                Debug.LogError("PoolManager is Null!!");
            }
            return instance; 
        }
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fishPool = GenerateFishs(numberOfFishs);
    }

    private List<GameObject> GenerateFishs(int numOfFishs)
    {
        for (int i = 0; i < numOfFishs; i++)
        {
            GameObject fish = Instantiate(fishPrefab);
            fish.transform.parent = fishContainer.transform;
            fish.SetActive(false);
            fishPool.Add(fish);
        }
        return fishPool;
    }

    public GameObject RequestFish()
    {
        foreach (var fish in fishPool)
        {
            if (fish.activeInHierarchy == false)
            {
                fish.SetActive(true);
                return fish;
            }
        }
        GameObject newFish = Instantiate(fishPrefab);
        newFish.transform.parent = fishContainer.transform;
        fishPool.Add(newFish);

        return newFish;
    }
}
