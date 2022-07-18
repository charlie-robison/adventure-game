using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour, IEnemyAttack
{
    public GameObject enemy;
    public GameObject bombRight;
    public GameObject bombLeft;
    public GameObject bomb;
    public Animator animator;
    public float bombSpeed = 2f;

    private float timer = 0;
    private bool isAttacking = false;
    private GameObject currentRightBomb;
    private GameObject currentLeftBomb;

    public void attack()
    {
        if (Time.time > timer && !isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
            currentRightBomb = Instantiate(bomb, bombRight.transform.position, bombRight.transform.rotation);
            currentLeftBomb = Instantiate(bomb, bombLeft.transform.position, bombLeft.transform.rotation);
        }

        timer = Time.time + 0.1f;
    }

    public void throwRightBomb()
    {
        float enemyAngleY = (enemy.transform.eulerAngles.y + 90f) * Mathf.Deg2Rad;
        float enemyAngleX = enemy.transform.eulerAngles.x * Mathf.Deg2Rad;

        if (enemy.transform.eulerAngles.x >= 0f)
        {
            enemyAngleX = 0f;
        }

        currentRightBomb.GetComponent<Rigidbody>().velocity = new Vector3(-1 * bombSpeed * Mathf.Cos(enemyAngleY), -1 * bombSpeed * Mathf.Sin(enemyAngleX), bombSpeed * Mathf.Sin(enemyAngleY));
    }

    public void throwLeftBomb()
    {
        float enemyAngleY = (enemy.transform.eulerAngles.y + 90f) * Mathf.Deg2Rad;
        float enemyAngleX = enemy.transform.eulerAngles.x * Mathf.Deg2Rad;

        if (enemy.transform.eulerAngles.x >= 0f)
        {
            enemyAngleX = 0f;
        }

        // GameObject newBullet = Instantiate(bullet);
        // bombRight.transform.position = spawner.transform.position;
        currentLeftBomb.GetComponent<Rigidbody>().velocity = new Vector3(-1 * bombSpeed * Mathf.Cos(enemyAngleY), -1 * bombSpeed * Mathf.Sin(enemyAngleX), bombSpeed * Mathf.Sin(enemyAngleY));
    }

    public void finishAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("Attack");
    }
}
