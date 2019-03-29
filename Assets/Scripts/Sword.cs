using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Sword : MonoBehaviour
{
    public float disableTime; //Time in seconds to disable for a blocked hit
    public Material disabledMaterial;

    private Collider mc;
    private MeshRenderer mr;
    private Material enabledMaterial;

    private Vector3 velocity;
    private Vector3 lastPosition;

    private Hand hand;

    private void Awake()
    {
        hand = GetComponentInParent<Hand>();
    }

    private void Start()
    {
        mc = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        enabledMaterial = mr.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //collision.collider is shield (child obj), collision.gameObject is enemy (parent)
        print(collision.collider.gameObject);
        if (collision.collider.CompareTag("Shield"))
        {
            hand.TriggerHapticPulse(255);
            StartCoroutine(Disable());
        }

        else
        {
            EnemyPathWalk enemy = collision.gameObject.GetComponent<EnemyPathWalk>();
            if (enemy != null)
            {
                hand.TriggerHapticPulse(255);
                enemy.Kill(velocity);
            }
        }
    }

    private void FixedUpdate()
    {
        //Vector3 targetVel = (transform.position - lastPosition) / Time.fixedDeltaTime;
        //velocity = Vector3.Lerp(velocity, targetVel, 0.3f);
        velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    private IEnumerator Disable()
    {
        mr.material = disabledMaterial;
        mc.enabled = false;

        yield return new WaitForSecondsRealtime(disableTime);

        mr.material = enabledMaterial;
        mc.enabled = true;
    }
}
