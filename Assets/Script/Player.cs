using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int stat_maxhealth;
    [SerializeField] private int stat_maxmentalpoint;
    [SerializeField] private int stat_mentalpower;
    [SerializeField] private int stat_mentalprotection;

    [SerializeField] private int current_health;
    [SerializeField] private int current_mentalpoint;



    //contructor
    public Player()
    {
        stat_maxhealth = 1000;
        stat_maxmentalpoint = 100;
        stat_mentalpower = 62;
        stat_mentalprotection = 20;

        current_health = 1000;
        current_mentalpoint = 100;


    }

    public void addStat_MentalPower(int value)
    {
        stat_mentalpower += value;
    }

    public int getStat_MentalPower()
    {
        return stat_mentalpower;
    }

    public int getStat_MaxHealth()
    {
        return stat_maxhealth;
    }

    public int getCurrent_Health()
    {
        return current_health;
    }

    public int getStat_MaxMentalPoint()
    {
        return stat_maxmentalpoint;
    }

    public int getCurrent_MentalPoint()
    {
        return current_mentalpoint;
    }

    public int getStat_MentalProtection()
    {
        return stat_mentalprotection;
    }

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

    void playerBuff(string skill_name)
    {

    }

    void playerDebuff(string debuff_name)
    {

    }

    void useSkill()
    {
        //base on skill cost
        //deal damage to enemy
    }

    void battleSetPlayerStat()
    {
        //load player stat in battle
    }
}
