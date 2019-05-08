using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPathWalk : MonoBehaviour
{
	public float speed = 1f;
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
		totalT = curve.length / speed;
	}

    private float FindGroundPos(Vector3 pos)
    {
        Vector3 start = pos + 3 * Vector3.up;
        RaycastHit[] hits = Physics.RaycastAll(start, Vector3.down, 5, LayerMask.GetMask("LevelGeometry"));

        if (hits.Length == 0)
        {
            return pos.y;
        }
        else
        {
            float hit = hits.Max(h => h.point.y);
            return hit;
        }
    }

	private void FixedUpdate()
    {
        float st = Mathf.Clamp(t / totalT, 0.00001f, 0.99999f);
		Vector3 goalPos = curve.GetPointAt(st);
        goalPos.y = FindGroundPos(goalPos);
		t += Time.fixedDeltaTime;
        transform.rotation = Quaternion.LookRotation(goalPos - rb.position);
        rb.MovePosition(goalPos);
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
                //gc.subtract_from_enemies_done(1);
                Kill(collision.rigidbody.velocity);
			}
        }
        if (collision.gameObject.CompareTag("theWall"))
        {
            gc.add_to_enemies_through(1);
            gc.subtract_from_enemies_done(1);
            Destroy(gameObject);
        }
        
    }

    public void Kill(Vector3 velocity)
    {
        //velocity.y = 0;
        gc.subtract_from_enemies_done(1);
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
