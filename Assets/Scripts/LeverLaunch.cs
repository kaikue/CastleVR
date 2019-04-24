using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Vale.VR.InteractionSystem
{
    public class LeverLaunch : MonoBehaviour
    {
        public LinearMapping linearMapping;

        public Rigidbody ball;
        private Transform target;
        public GameObject trajectoryPointPrefab;
        public GameObject ballPrefab;
        private GameObject instance;
        //public LayerMask clickMask;

        private LineRenderer line;
        private Transform launcher;
        private Transform ball_pos;

        public bool debugPath;

        public float h = 30;
        public float gravity = -18;

        void Awake()
        {
            instance = ballPrefab;
            target = trajectoryPointPrefab.GetComponent<Transform>();
            ball.useGravity = false;
        }
        void Start()
        {
            /*instance = ballPrefab;
            ball = instance.GetComponent<Rigidbody>();
            target = trajectoryPointPrefab.GetComponent<Transform>();
            ball.useGravity = false;*/
            line = this.GetComponent<LineRenderer>();
            launcher = this.GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            if (linearMapping.value == 1.0f)
            {
                //Debug.Log("Fire");
                Launch();
            }

            if (debugPath)
            {
                DrawPath();
            }
        }


        void Launch()
        {
            GameObject child = this.transform.GetChild(0).gameObject; 
            ball_pos = child.transform;
            Physics.gravity = Vector3.up * gravity;
            ball.useGravity = true;
            ball.velocity = CalculateLaunchData().initialVelocity;
            linearMapping.value = 0.0f;
            StartCoroutine(Reload());
        }

        LaunchData CalculateLaunchData()
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
        }

        IEnumerator Reload()
        {
            yield return new WaitForSeconds(5.5f);
            Destroy(instance);
            instance = Instantiate(ballPrefab, ball_pos.position,  new Quaternion(0, 0, 0, 0));
            ball = instance.GetComponent<Rigidbody>();
            ball.useGravity = false;
        }
    }
}
