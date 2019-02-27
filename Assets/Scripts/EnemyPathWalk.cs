using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathWalk : MonoBehaviour
{
	private const float SPEED = 1.5f;

    private Rigidbody rb;
	private BezierCurve curve;
	private float t;
	private float totalT;
	
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	public void SetCurve(BezierCurve curve)
	{
		this.curve = curve;
		t = 0;
		totalT = curve.length / SPEED;
	}

	private void FixedUpdate()
    {
		Vector3 goalPos = curve.GetPointAt(t / totalT);
		t += Time.fixedDeltaTime;
		rb.MovePosition(goalPos); //TODO: move physically around obstacles?
        //rb.velocity = velocity;
    }
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spear") || collision.gameObject.CompareTag("projectile"))
        {
            Kill();
        }
        if (collision.gameObject.CompareTag("theWall"))
        {
            Kill();
        }
        
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
