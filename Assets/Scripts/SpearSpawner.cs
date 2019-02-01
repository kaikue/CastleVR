using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject spearPrefab;
    public Transform spawnPoint;

    private HashSet<GameObject> spears;

    private void Start()
    {
        spears = new HashSet<GameObject>();
    }
    
    private void FixedUpdate()
    {
        if (spears.Count == 0)
        {
            SpawnSpear();
        }
        spears.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Spear") && !spears.Contains(other.gameObject))
        {
            spears.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void SpawnSpear()
    {
        Instantiate(spearPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
