using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathWalk : MonoBehaviour
{
	private const float SPEED = 1.5f;
	private const float DIE_TIME = 1.0f;

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
			Collider thisCollider = collision.GetContact(0).thisCollider;
			if (thisCollider.CompareTag("Shield"))
			{
                //stick it in the shield and disable physics
                /*FixedJoint fj = collision.gameObject.GetComponentInChildren<FixedJoint>();
                if (fj != null) Destroy(fj);
                Destroy(collision.gameObject.GetComponent<Rigidbody>());
				collision.transform.parent = thisCollider.transform;
				*///Destroy(collision.gameObject); //TODO: stick it in the shield or something?
			}
			else
			{
				Kill(collision.rigidbody.velocity);
			}
        }
        if (collision.gameObject.CompareTag("theWall"))
        {
            gc.add_to_enemies_through(1);
            Destroy(gameObject);
        }
        
    }

    public void Kill(Vector3 velocity)
    {
        //velocity.y = 0;
        gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        print(velocity);
        rb.velocity = velocity;
        rb.useGravity = true;
		DelayDestroy script = gameObject.AddComponent<DelayDestroy>();
		script.destroyTime = DIE_TIME;
		script.onDestroyObject = deathParticlesPrefab;
		enabled = false;
    }
}
