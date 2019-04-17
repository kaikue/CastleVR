using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float speed;
    public int axis = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (axis == 0)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.back * speed * Time.deltaTime);

        }
    }
}
