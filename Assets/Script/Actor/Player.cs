using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private int stat_maxhealth;
    [SerializeField] private int stat_maxmentalpoint;
    [SerializeField] private int stat_mentalpower;
    [SerializeField] private int stat_mentalprotection;

    [SerializeField] private int current_health;
    [SerializeField] private int current_mentalpoint;

    private static Player instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else if (instance != this)
        //{
        //    Destroy(instance.gameObject);
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}

    }

    public static Player GetInstance()
    {
        return instance;
    }

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

    public void updatePlayer(int maxhp,int maxmp,int maxpow,int maxprotect,int currhp,int currmp)
    {
        stat_maxhealth = maxhp;
        stat_maxmentalpoint = maxmp;
        stat_mentalpower = maxpow;
        stat_mentalprotection = maxprotect;

        current_health = currhp;
        current_mentalpoint = currmp;

    }

    public Player getPlayer()
    {
        return this;
    }

    public int getStat_MaxHealth()
    {
        return stat_maxhealth;
    }

    public int getStat_MaxMentalPoint()
    {
        return stat_maxmentalpoint;
    }

    public int getStat_MentalProtection()
    {
        return stat_mentalprotection;
    }

    public int getStat_MentalPower()
    {
        return stat_mentalpower;
    }

    public int getCurrent_Health()
    {
        return current_health;
    }

    public int getCurrent_MentalPoint()
    {
        return current_mentalpoint;
    }

    public void setStat_MaxHealth(int value)
    {
        stat_maxhealth = value;
    }

    public void setStat_MaxMentalPoint(int value)
    {
        stat_maxmentalpoint = value;
    }

    public void setStat_MentalPower(int value)
    {
        stat_mentalpower = value;
    }

    public void setStat_MentalProtection(int value)
    {
        stat_mentalprotection = value;
    }

    public void setCurrent_Health(int value)
    {
        current_health = value;
    }

    public void setCurrent_MentalPoint(int value)
    {
        current_mentalpoint = value;
    }


    public void addStat_MentalPower(int value)
    {
        stat_mentalpower += value;
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
