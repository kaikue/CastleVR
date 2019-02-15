using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Rigidbody rb;
    private Transform transform;

    public GameObject ImpactAreaPrefab;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        transform = this.GetComponent<Transform>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            Impact();
        }
    }

    void Impact()
    {
        rb.isKinematic = true;
        Vector3 landing = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        GameObject impactArea = Instantiate(ImpactAreaPrefab, landing, transform.rotation);
        impactArea.transform.parent = this.transform;
    }
}
