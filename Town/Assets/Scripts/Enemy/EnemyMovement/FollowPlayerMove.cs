using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMove : MonoBehaviour, IEnemyMove
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float playerMaxDistance;

    private PlayerCheck playerCheck = new PlayerCheck();
    private Vector3 enemyPosition;
    private float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    void Start()
    {
        enemyPosition = transform.position;
    }

    public void move()
    {
        if (playerCheck.distanceFromPlayer(player, enemy) <= playerMaxDistance)
        {
            animator.SetTrigger("Run");
            Vector3 newPos = Vector3.MoveTowards(enemy.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(newPos.x, enemyPosition.y, newPos.z);
            transform.rotation = playerCheck.rotateFacingPlayer(player, enemy, turnSmoothTime, turnSmoothVelocity);
        }
        else
        {
            animator.ResetTrigger("Run");
        }
    }
}
