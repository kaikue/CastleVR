using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour { 

    public TextMeshPro txt;
    public int enemies_spawned;
    public int enemies_through;


    // Start is called before the first frame update
    void Start()
    {
        txt.text = "hello";
        
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
        txt.text = "Spawned: " + enemies_spawned + " Let Through: " + enemies_through;
    }
}
