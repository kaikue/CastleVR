using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0.1f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //-------------------------------------------------
    void FixedUpdate()
    {
        // Slow-clamp velocity
        rb.velocity = velocity;

    }

    public void setVelocity(Vector3 v)
    {
        velocity = v;
        print(rb.velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spear") || collision.gameObject.CompareTag("projectile"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("theWall"))
        {
            Destroy(gameObject);
        }
        
    }
}
