using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private IEnemyMove enemyMove;

    [SerializeField]
    private IEnemyAttack enemyAttack;

    [SerializeField]
    private bool stopMove = false;

    void Start()
    {
        enemyMove = enemy.GetComponent<IEnemyMove>();
        enemyAttack = enemy.GetComponent<IEnemyAttack>();
    }
    
    void attack() 
    {
        enemyAttack.attack();
    }

    void move()
    {
        if (!stopMove)
        {
            enemyMove.move();
        }
    }

    public void setStopMove(bool newState)
    {
        stopMove = newState;
    }

    // Update is called once per frame
    void Update()
    {
        attack();
        move();
    }
}
