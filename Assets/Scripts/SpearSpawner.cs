using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject spearPrefab;
    int spears = 0;

    void Start()
    {
        
    }
    
    private void FixedUpdate()
    {
        if (spears == 0)
        {
            SpawnSpear();
        }
        spears = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void SpawnSpear()
    {
        //TODO
        Instantiate(spearPrefab);
    }
}
