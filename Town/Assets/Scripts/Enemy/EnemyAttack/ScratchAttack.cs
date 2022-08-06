using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchAttack : MonoBehaviour, IEnemyAttack
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private Collider hitPoint;

    private PlayerCheck playerCheck = new PlayerCheck();
    private float attackCounter = 0f;

    public void attack()
    {
        // Checks if enemy is close enough to player for scratch attack.
        if (playerCheck.distanceFromPlayer(player, enemy) <= 2f)
        {
            if (Time.time > attackCounter)
            {
                animator.SetTrigger("Scratch");
                enemy.GetComponent<Enemy>().setStopMove(true);
            }

            attackCounter = Time.time + 0.1f;
        }
    }

    public void resetScratch()
    {
        animator.ResetTrigger("Scratch");
        enemy.GetComponent<Enemy>().setStopMove(false);
        hitPoint.isTrigger = false;
    }

    public void enableHitPoint()
    {
        hitPoint.isTrigger = true;
    }
}
