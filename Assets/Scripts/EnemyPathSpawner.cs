using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPathSpawner : MonoBehaviour
{
    //   public GameObject[] enemies;                // The enemy prefab to be spawned.
    //   public float spawnTime = 3f;  // How long between each spawn.
    //   public float wave_time = 5f; // time between waves
    //   public int spawn_time = 2;

    private BezierCurve curve;
    //   public GameController gc;

    //   public int num_enemies = 5;

    private void Awake()
    {
        //GameObject gco = GameObject.Find("GameController");
        //gc = gco.GetComponent<GameController>();
        curve = GetComponent<BezierCurve>();
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        //InvokeRepeating("SpawnWave", wave_time, wave_time);
  
        //StartCoroutine("SpawnWave");

    }

  //  private void SpawnRandomEnemy()
  //  {
  //      GameObject temp = Instantiate(enemies[Random.Range(0,enemies.Length)], curve.GetPointAt(0), Quaternion.identity);
  //      EnemyPathWalk script = temp.GetComponent<EnemyPathWalk>();
  //      gc.add_to_enemies_spawned(1);
		//script.SetCurve(curve);
  //  }


        // spawns an enemy of the sent prefab at this curve

    public void SpawnEnemy(GameObject enemy)
    {

        //print(curve);
        GameObject temp = Instantiate(enemy, curve.GetPointAt(0), Quaternion.identity);
        EnemyPathWalk script = temp.GetComponent<EnemyPathWalk>();
        script.SetCurve(curve);


    }

    //    IEnumerator SpawnWave()
    //    {
    //        //yield return new WaitForSeconds(wave_time);

    //        gc.increase_wave();

    //        for (int i = 0; i< num_enemies; i++)
    //        {
    //            SpawnRandomEnemy();

    //            // wait a random amoount of time between spawining enemenies
    //            int wait = Random.Range(spawn_time, spawn_time+5);
    //            yield return new WaitForSeconds(wait);
    //        }

    //        num_enemies *= 2;

    //        yield return new WaitForSeconds(wave_time);
    //        StartCoroutine("SpawnWave");

    //    }
}