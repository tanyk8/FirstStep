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

    [SerializeField] public EnemyData enemydata;


    //List skill
    //List dialogue maybe like when battle start etc

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

    public int enemySkillHeal(EnemySkill enemyskill)
    {
        int original = 0;
        int after = 0;

        original = current_health;
        current_health += stat_maxhealth * (enemyskill.enemyskill_power - 100) / 100;

        if (current_health > stat_maxhealth)
        {
            current_health = stat_maxhealth;

        }
        after = current_health;
        Debug.Log(after + "-" + original);
        return (after - original);
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
