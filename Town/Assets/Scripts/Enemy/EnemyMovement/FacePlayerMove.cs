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

    public void Move()
    {
        if (playerCheck.DistanceFromPlayer(player, enemy) <= maxDistance)
        {
            transform.rotation = playerCheck.RotateFacingPlayer(player, enemy, turnSmoothTime, turnSmoothVelocity);
        }
    }
}
