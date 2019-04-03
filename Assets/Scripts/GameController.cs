using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameController : MonoBehaviour {
    
    public TextMeshPro score_txt;
    public TextMeshPro wave_txt;
    public int enemies_left_to_spawn;
    public int enemies_through;
    public int wave = 0;


    public GameObject[] enemies;    // The enemy prefabs to be spawned.
    public float spawnTime = 3f;  // How long between each spawn.
    public float wave_time = 5f; // time between waves
    public int spawn_time = 10;

    public EnemyPathSpawner[] enemyPaths;

    public int num_enemies = 5;
    
    // Start is called before the first frame update

    private void Awake()
    {
        if (score_txt == null)
        {
            GameObject score = GameObject.Find("Score_Text");
            score_txt = score.GetComponent<TextMeshPro>();
        }

        if (wave_txt == null)
        {
            GameObject wave = GameObject.Find("Wave_Text");
            wave_txt = wave.GetComponent<TextMeshPro>();
        }
    }
    void Start()
    {


        StartCoroutine(SpawnWaves(0));
        //StartCoroutine(SpawnWaves(1));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void subtract_from_enemies_left(int x)
    {
        enemies_left_to_spawn -= x;
        UpdateScore();
    }

    public void reset_enemies_left(int x)
    {
        enemies_left_to_spawn = x;
        UpdateScore();
    }




    public void add_to_enemies_through(int x)
    {
        enemies_through += x;
        UpdateScore();
    }

    void UpdateScore()
    {
        score_txt.text = "Incoming: " + enemies_left_to_spawn + " Let Through: " + enemies_through;
    }

    public void increase_wave()
    {
        wave += 1;
        UpdateWave();
    }

    void UpdateWave()
    {
        wave_txt.text = "Wave: " + wave;
    }




    IEnumerator SpawnWaves(int path_index)
    {
        //yield return new WaitForSeconds(wave_time);

        increase_wave();
        reset_enemies_left(num_enemies);

        for (int i = 0; i < num_enemies; i++)
        {
            // call spawnEnmenty on a path
            EnemyPathSpawner path = enemyPaths[Random.Range(0, enemyPaths.Length)];
            GameObject enemy_prefab = enemies[Random.Range(0, enemies.Length)];
            path.SpawnEnemy(enemy_prefab);
            subtract_from_enemies_left(1);

            // wait a random amoount of time between spawining enemenies
            int wait = Random.Range(spawn_time, spawn_time + 5);
            yield return new WaitForSeconds(wait);
        }

        num_enemies += 10;

        yield return new WaitForSeconds(wave_time);
        StartCoroutine(SpawnWaves(0));

    }
    
}


