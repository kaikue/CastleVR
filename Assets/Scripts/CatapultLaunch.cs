﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultLaunch : MonoBehaviour
{
    // TrajectoryPoint and Ball will be instantiated
    public GameObject TrajectoryPointPrefab;
    public GameObject BallPrefab;

    private GameObject ball;
    //private Renderer renderer;
    private Rigidbody rb;
    private bool isPressed, isBallThrown;
    private float power = 25;
    private int numOfTrajectoryPoints = 30;
    //private List<gameobject> trajectoryPoints;
    //private List<trajectorypoints> trajectoryPoints;
    private List<GameObject> trajectoryPoints;
    //---------------------------------------
    void Start()
    {
        //List<trajectorypoints> trajectoryPoints = new List<trajectorypoints>();
        List<GameObject> trajectoryPoints = new List<GameObject>();
        isPressed = isBallThrown = false;
        //renderer = TrajectoryPointPrefab.GetComponent<Renderer>();
        //rb = ball.GetComponent<Rigidbody>();
        // TrajectoryPoints are instatiated
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            GameObject dot = Instantiate(TrajectoryPointPrefab);
            dot.GetComponent<Renderer>().enabled = true;
            trajectoryPoints.Add(dot);
            //testGameObjectList.Add(dot);
        }
    }
    //---------------------------------------
    void Update()
    {
        if (isBallThrown)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
            if (!ball)
                createBall();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
            if (!isBallThrown)
                throwBall();
        }
        // when mouse button is pressed, cannon is rotated as per mouse movement and projectile trajectory path is displayed.
        if (isPressed)
        {
            Vector3 vel = GetForceFrom(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            setTrajectoryPoints(transform.position, vel / ball.GetComponent<Rigidbody>().mass);
        }
    }
    //---------------------------------------
    // Following method creates new ball
    //---------------------------------------
    private void createBall()
    {
        ball = (GameObject)Instantiate(BallPrefab);
        Vector3 pos = transform.position;
        pos.z = 1;
        ball.transform.position = pos;
        ball.SetActive(false);
    }
    //---------------------------------------
    // Following method gives force to the ball
    //---------------------------------------
    private void throwBall()
    {
        ball.SetActive(true);
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<Rigidbody>().AddForce(GetForceFrom(ball.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)), ForceMode.Impulse);
        isBallThrown = true;
    }
    //---------------------------------------
    // Following method returns force by calculating distance between given two points
    //---------------------------------------
    private Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y)) * power;
    }
    //---------------------------------------
    // Following method displays projectile trajectory path. It takes two arguments, start position of object(ball) and initial velocity of object(ball).
    //---------------------------------------
    void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    {
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
        fTime += 0.1f;
        for (int i = 0; i < numOfTrajectoryPoints; i++)
        {
            float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
            float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
            Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, 2);
            trajectoryPoints[i].transform.position = pos;
            //trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
            trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg);
            fTime += 0.1f;
        }
    }
}
