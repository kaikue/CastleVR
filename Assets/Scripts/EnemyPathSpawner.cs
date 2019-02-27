using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPathSpawner : MonoBehaviour
{
    public GameObject[] enemies;                // The enemy prefab to be spawned.
    public float spawnTime = 3f;            // How long between each spawn.

	private BezierCurve curve;

    void Start()
    {
		curve = GetComponent<BezierCurve>();
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        GameObject temp = Instantiate(enemies[Random.Range(0,enemies.Length)], curve.GetPointAt(0), Quaternion.identity);
        EnemyPathWalk script = temp.GetComponent<EnemyPathWalk>();
		script.SetCurve(curve);
    }
}