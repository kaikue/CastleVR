﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTracking : MonoBehaviour
{
    public Rigidbody ball;
    public Transform target;
    //public GameObject trajectoryPointPrefab;
    public LayerMask clickMask;

    //public bool debugPath;

    public float h = 30;
    public float gravity = -18;


    void Start()
    {
        //ball = GetComponent<Rigidbody>();
        ball.useGravity = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, clickMask))
            {
                clickPosition = hit.point;
            }
            //Instantiate(trajectoryPointPrefab, clickPosition, transform.rotation); 
            print(clickPosition);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        /*if (debugPath)
        {
            DrawPath();
        }*/
    }
       
    void Launch()
    {
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        //ball.velocity = CalculateLaunchData().initialVelocity;
        ball.velocity = CalculateLaunchVelocity();
        //print(CalculateLaunchData().initialVelocity);
        print(CalculateLaunchVelocity());
    }

    /*LaunchData CalculateLaunchData()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY, time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = ball.position;
        int resolution = 30;

        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }*/

    Vector3 CalculateLaunchVelocity()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity));

        return velocityXZ + velocityY;
    }
}
