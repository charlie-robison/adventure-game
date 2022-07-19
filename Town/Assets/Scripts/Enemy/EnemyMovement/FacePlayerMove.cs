using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayerMove : MonoBehaviour, IEnemyMove
{
    public GameObject enemy;
    public GameObject player;
    public float maxDistance;

    private PlayerCheck playerCheck = new PlayerCheck();
    private float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;

    public void move()
    {
        if (playerCheck.distanceFromPlayer(player, enemy) <= maxDistance)
        {
            transform.rotation = playerCheck.rotateFacingPlayer(player, enemy, turnSmoothTime, turnSmoothVelocity);
        }
    }
}
