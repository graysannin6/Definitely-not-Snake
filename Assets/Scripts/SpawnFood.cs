using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public Vector3 center;
    public Vector3 size;
    public GameObject[] Food;

    
    public void SpawningFood()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x/2,size.x/2),2.86f,Random.Range(-size.z/2,size.z/2));
        Instantiate(Food[Random.Range(0,Food.Length)],pos,Quaternion.identity);
    }
}
