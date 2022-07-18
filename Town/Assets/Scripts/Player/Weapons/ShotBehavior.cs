using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour {

	public float bulletSpeed;
	public GameObject projectile;
	public GameObject explosion;
	public Vector3 targetPosition;
	Cinemachine.CinemachineImpulseSource source;

	void Start()
	{
		// Creates a recoil impulse once the bullet is instantiated.
		source = GetComponent<Cinemachine.CinemachineImpulseSource>();
		source.GenerateImpulse(Camera.main.transform.forward);
	}

	void explode()
	{
		Destroy(projectile);
		GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		Destroy(newExplosion, 1f);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy")
		{
			Destroy(col.gameObject);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (transform.position == targetPosition)
		{
			explode();
		}
		else
		{
		    transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
		}
	}
}
