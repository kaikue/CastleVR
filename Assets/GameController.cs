using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour { 

    public TextMeshPro score_txt;
    public TextMeshPro wave_txt;
    public int enemies_spawned;
    public int enemies_through;
    public int wave = 0;


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
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add_to_enemies_spawned(int x)
    {
        enemies_spawned += x;
        UpdateScore();
    }

    public void add_to_enemies_through(int x)
    {
        enemies_through += x;
        UpdateScore();
    }

    void UpdateScore()
    {
        score_txt.text = "Spawned: " + enemies_spawned + " Let Through: " + enemies_through;
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

}
