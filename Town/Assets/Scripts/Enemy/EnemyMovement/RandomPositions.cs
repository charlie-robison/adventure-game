using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositions : MonoBehaviour, IEnemyMove
{
    [SerializeField]
    private Transform [] positions;

    [SerializeField]
    private float speed;

    private int moveTimer = 0;
    private Vector3 lastPos;
    private Vector3 nextPos;

    public void move()
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
