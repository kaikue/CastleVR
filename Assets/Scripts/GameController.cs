﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameController : MonoBehaviour {
    
    public TextMeshPro let_through_txt;
    public TextMeshPro incoming_txt;
    public TextMeshPro wave_txt;
    public TextMeshPro let_through_txt_2;
    public TextMeshPro incoming_txt_2;
    public TextMeshPro wave_txt_2;
    public int enemies_left_to_spawn;
    public int enemies_through;
    public int wave = 0;


    public GameObject[] enemies;    // The enemy prefabs to be spawned.
    //public float spawnTime = 3f;  // How long between each spawn.
    public float wave_time = 5f; // time between waves
    public int spawn_time = 3;

    public EnemyPathSpawner[] enemyPaths;

    public int num_enemies = 5;
    
    // Start is called before the first frame update

    private void Awake()
    {
        //if (score_txt == null)
        //{
        //    GameObject score = GameObject.Find("Score_Text");
        //    score_txt = score.GetComponent<TextMeshPro>();
        //}

        //if (wave_txt == null)
        //{
        //    GameObject wave = GameObject.Find("Wave_Text");
        //    wave_txt = wave.GetComponent<TextMeshPro>();
        //}
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
        incoming_txt.text = "Incoming: " + enemies_left_to_spawn;
            
        let_through_txt.text =  " Let Through: " + enemies_through;
        incoming_txt_2.text = "Incoming: " + enemies_left_to_spawn;

        let_through_txt_2.text = " Let Through: " + enemies_through;
    }

    public void increase_wave()
    {
        wave += 1;
        UpdateWave();
    }

    void UpdateWave()
    {
        wave_txt.text = "Wave: " + wave;
        wave_txt_2.text = "Wave: " + wave;

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

            if (enemies_left_to_spawn <= 2)
            {
                GameObject enemy_prefab = enemies[Random.Range(0, enemies.Length)];
                path.SpawnEnemy(enemy_prefab);
                subtract_from_enemies_left(1);
            }
            else
            {

                // pick a path now randomly generate a horde
                int horde_size = Random.Range(1, Mathf.Min(enemies_left_to_spawn, 11));
                //print(" hs: "+  horde_size);
                //print(" left : " + enemies_left_to_spawn);
                for (int j = 0; j < horde_size; j++)
                {
                    GameObject enemy_prefab = enemies[Random.Range(0, enemies.Length)];
                    path.SpawnEnemy(enemy_prefab);
                    subtract_from_enemies_left(1);
                    i++;
                    yield return new WaitForSeconds(1f);

                }
            }
            // wait a random amoount of time betwen spawining hordes
            int wait = Random.Range(spawn_time, spawn_time + 5);
            yield return new WaitForSeconds(wait);
        }

        num_enemies += 10;

        yield return new WaitForSeconds(wave_time);
        StartCoroutine(SpawnWaves(0));

    }
    
}


