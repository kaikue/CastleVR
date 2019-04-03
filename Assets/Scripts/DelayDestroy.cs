using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
	public GameObject onDestroyObject;
	public float destroyTime;

    private void Start()
    {
		Invoke("Die", destroyTime);
    }
	
    private void Die()
	{
		Instantiate(onDestroyObject, transform.position, transform.rotation);
        Destroy(gameObject);
	}
}
