using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float disableTime; //Time in seconds to disable for a blocked hit
    public Material disabledMaterial;

    private Collider mc;
    private MeshRenderer mr;
    private Material enabledMaterial;

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
            StartCoroutine(Disable());
        }

        else
        {
            EnemyPathWalk enemy = collision.gameObject.GetComponent<EnemyPathWalk>();
            if (enemy != null)
            {
                enemy.Kill();
            }
        }
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
