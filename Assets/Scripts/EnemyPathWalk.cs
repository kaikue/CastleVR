using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathWalk : MonoBehaviour
{
	private const float SPEED = 1.5f;
	private const float DIE_TIME = 2.0f;

	public GameObject deathParticlesPrefab;

    private Rigidbody rb;
	private BezierCurve curve;
	private float t;
	private float totalT;
    private GameController gc;
	
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameObject gco = GameObject.Find("GameController");
        gc = gco.GetComponent<GameController>();
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
        transform.rotation = Quaternion.LookRotation(goalPos - rb.position);
        rb.MovePosition(goalPos); //TODO: move physically around obstacles?
    }
	
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spear") || collision.gameObject.CompareTag("projectile"))
		{
			if (collision.GetContact(0).thisCollider.CompareTag("Shield"))
			{
				Destroy(collision.gameObject); //TODO: stick it in the shield or something?
			}
			else
			{
				Kill();
			}
        }
        if (collision.gameObject.CompareTag("theWall"))
        {
            gc.add_to_enemies_through(1);
            Kill();
        }
        
    }

    public void Kill()
    {
		//Destroy(gameObject);
		DelayDestroy script = gameObject.AddComponent<DelayDestroy>();
		script.destroyTime = DIE_TIME;
		script.onDestroyObject = deathParticlesPrefab;
		enabled = false;
    }
}
