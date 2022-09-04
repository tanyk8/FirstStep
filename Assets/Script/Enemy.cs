using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Basic stat")]
    [SerializeField] private string enemy_name;
    [SerializeField] private int stat_maxhealth;
    [SerializeField] private int stat_damage;
    [SerializeField] private int stat_defence;

    [SerializeField] private int current_health;

    public bool receiveDamage(int damage)
    {
        current_health -= damage;

        if (current_health <= 0)
        {
            current_health = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void battleSetEnemyStat()
    {
        //load the corresponding enemy stat in battle
    }

    public string getEnemyName()
    {
        return enemy_name;
    }
    public int getStat_MaxHealth()
    {
        return stat_maxhealth;
    }

    public int getCurrent_Health()
    {
        return current_health;
    }

    public int getStat_Damage()
    {
        return stat_damage;
    }

    public int getStat_Defence()
    {
        return stat_defence;
    }

    //enemy buff and debuff/ heal
}
