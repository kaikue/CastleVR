using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSpawner : MonoBehaviour
{
    public GameObject spearPrefab;
    public Transform spawnPoint;

    private HashSet<GameObject> spears;

    private void Awake()
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

	private static bool IsSpear(Transform t)
	{
		return t.CompareTag("Spear") || t.parent.CompareTag("Spear");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (IsSpear(other.transform) && !spears.Contains(other.gameObject))
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
