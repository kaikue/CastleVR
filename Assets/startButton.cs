using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gco = GameObject.Find("GameController");
        gc = gco.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_game()
    {
        gc.on_start_button();
    }
}
