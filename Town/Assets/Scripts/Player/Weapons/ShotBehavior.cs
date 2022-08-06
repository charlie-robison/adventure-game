using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : MonoBehaviour {

	[SerializeField]
	private float bulletSpeed;

	[SerializeField]
	private GameObject projectile;

	[SerializeField]
	private GameObject explosion;

	[SerializeField]
	private Vector3 targetPosition;

	[SerializeField]
	private Cinemachine.CinemachineImpulseSource source;

	void Start()
	{
		// Creates a recoil impulse once the bullet is instantiated.
		source = GetComponent<Cinemachine.CinemachineImpulseSource>();
		source.GenerateImpulse(Camera.main.transform.forward);
	}

	void explode()
	{
		Destroy(projectile);
		GameObject newExplosion = Instantiate(explosion, transform.position, transform.rotation);
		Destroy(newExplosion, 1f);
	}

	public void setTargetPosition(Vector3 newTargetPos)
    {
		targetPosition = newTargetPos;
    }

	public void setBulletSpeed(float newBulletSpeed)
    {
		bulletSpeed = newBulletSpeed;
    }

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Enemy"))
		{
			Destroy(col.gameObject);
			Destroy(projectile);
		}

		if (col.CompareTag("Crate"))
        {
			Destroy(col.gameObject);
			Destroy(projectile);
			col.gameObject.GetComponent<Crate>().spawnItems();
        }
	}

	// Update is called once per frame
	void Update() 
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
