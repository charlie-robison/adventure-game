using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : MonoBehaviour, IPlayerWeapon
{
    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private GameObject spawner;

    [SerializeField]
    private float shootRate;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private RaycastHit hitData;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float maxDistance;

    private float shootRateTimestamp;

    public void weaponAttack()
    {
        // Checks if the current time is greater than the shoot buffer time.
        if (Time.time > shootRateTimestamp)
        {
            // Shoots the bullet.
            shootBullet();

            // Sets the shoot buffer time.
            shootRateTimestamp = Time.time + shootRate;
        }
    }

    void shootBullet()
    {
        // Creates the ray.
        Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(centerOfScreen);
		// Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);

        // Checks if something was hit by the ray. (hitData stores the RaycastHit info).
        if (Physics.Raycast(ray, out hitData, maxDistance))
		{
            // Spawns a new bullet from the spawn point.
            GameObject newProjectile = Instantiate(projectile, spawner.transform.position, spawner.transform.rotation);

            // Sets final point and bullet speed for the projectile.
            newProjectile.GetComponent<ShotBehavior>().setTargetPosition(hitData.point);
            newProjectile.GetComponent<ShotBehavior>().setBulletSpeed(bulletSpeed);
            Destroy(newProjectile, 2f);
		}
    }
}
