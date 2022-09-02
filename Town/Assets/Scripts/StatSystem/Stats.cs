using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private GameObject currentObject;

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

    private float baseAttack;
    private float baseDefense;

    private void Start()
    {
        // Sets Health Bar UI to default HP values.
        healthUI.GetComponent<HealthBarUI>().setHealthUI(hp);
        healthUI.GetComponent<HealthBarUI>().setMaxHealthUI(maxHp);

        baseAttack = attack;
        baseDefense = defense;
    }

    public void setHp(float addedHp)
    {
        hp += addedHp;
        healthUI.GetComponent<HealthBarUI>().setHealthUI(hp);
    }

    public void setMaxHp(float addMaxHp)
    {
        maxHp += addMaxHp;
        healthUI.GetComponent<HealthBarUI>().setMaxHealthUI(maxHp);
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

    public void resetAttack()
    {
        attack = baseAttack;
    }

    public void resetDefense()
    {
        defense = baseDefense;
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

    public float getBaseAttack()
    {
        return baseAttack;
    }

    public float getBaseDefense()
    {
        return baseDefense;
    }

    private void Update()
    {
        if (hp <= 0f)
        {
            Destroy(currentObject);
        }
    }
}
