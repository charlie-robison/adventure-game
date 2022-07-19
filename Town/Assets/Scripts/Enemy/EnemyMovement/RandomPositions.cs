using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositions : MonoBehaviour, IEnemyMove
{
    public Transform [] positions;
    public float speed;

    private int moveTimer = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;

    public void Move()
    {
        if (moveTimer % 10000 == 0)
        {
            Transform randomPos = positions[Random.Range(0, positions.Length - 1)];
            lastPos = transform.position;
            nextPos = randomPos.position;
        }

        transform.position = Vector3.MoveTowards(lastPos, nextPos, speed * Time.deltaTime);
        moveTimer++;
    }
}
