using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private float hp;

    [SerializeField]
    private float maxHp;

    [SerializeField]
    private float attack;

    [SerializeField]
    private float defense;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float attackFrequency;

    [SerializeField]
    private GameObject healthUI;

    public void setHp(float addedHp)
    {
        hp += addedHp;
    }

    public void setMaxHp(float addMaxHp)
    {
        maxHp += addMaxHp;
    }

    public void setAttack(float addedAttack)
    {
        attack += addedAttack;
    }

    public void setDefense(float addedDefense)
    {
        defense += addedDefense;
    }

    public void setSpeed(float addedSpeed)
    {
        speed += addedSpeed;
    }

    public void setAttackFrequency(float addedFrequency)
    {
        attackFrequency += addedFrequency;
    }

    public float getHp()
    {
        return hp;
    }

    public float getMaxHp()
    {
        return maxHp;
    }

    public float getAttack()
    {
        return attack;
    }

    public float getDefense()
    {
        return defense;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getAttackFrequency()
    {
        return attackFrequency;
    }

    public GameObject getHealthUI()
    {
        return healthUI;
    }
}
