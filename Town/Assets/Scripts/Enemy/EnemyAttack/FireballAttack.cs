using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject spawner;

    [SerializeField]
    private GameObject bullet;

    private int attackTimer = 0;
    private float bulletSpeed = 5f;

    public void attack()
    {
        if (attackTimer % 500 == 0)
        {
            float enemyAngleY = (enemy.transform.eulerAngles.y + 90f) * Mathf.Deg2Rad;
            float enemyAngleX = enemy.transform.eulerAngles.x * Mathf.Deg2Rad;

            if (enemy.transform.eulerAngles.x >= 0f)
            {
                enemyAngleX = 0f;
            }

            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = spawner.transform.position;
            newBullet.GetComponent<Rigidbody>().velocity = new Vector3(-1 * bulletSpeed * Mathf.Cos(enemyAngleY), -1 * bulletSpeed * Mathf.Sin(enemyAngleX), bulletSpeed * Mathf.Sin(enemyAngleY));
        }

        attackTimer++;
    }
}
