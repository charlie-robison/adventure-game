using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public IEnemyMove enemyMove;
    public IEnemyAttack enemyAttack;
    public bool stopMove = false;

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

    // Update is called once per frame
    void Update()
    {
        attack();
        move();
    }
}
