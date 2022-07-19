using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchAttack : MonoBehaviour, IEnemyAttack
{
    public Animator animator;
    public GameObject player;
    public GameObject enemy;
    public Collider hitPoint;

    private PlayerCheck playerCheck = new PlayerCheck();
    private float attackCounter = 0f;

    public void Attack()
    {
        // Checks if enemy is close enough to player for scratch attack.
        if (playerCheck.DistanceFromPlayer(player, enemy) <= 2f)
        {
            if (Time.time > attackCounter)
            {
                animator.SetTrigger("Scratch");
                enemy.GetComponent<Enemy>().stopMove = true;
            }

            attackCounter = Time.time + 0.1f;
        }
    }

    public void ResetScratch()
    {
        animator.ResetTrigger("Scratch");
        enemy.GetComponent<Enemy>().stopMove = false;
        hitPoint.isTrigger = false;
    }

    public void EnableHitPoint()
    {
        hitPoint.isTrigger = true;
    }
}
