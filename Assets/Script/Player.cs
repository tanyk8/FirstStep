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
    [SerializeField] private int current_mentalpower;
    [SerializeField] private int current_mentalprotection;

    List<Inventory> player_inventory_list;
    List<Quest> player_quest_list;
    List<Skill> player_skill_list;


    //contructor
    public Player()
    {
        stat_maxhealth = 100;
        stat_maxmentalpoint = 100;
        stat_mentalpower = 10;
        stat_mentalprotection = 10;

        current_health = 100;
        current_mentalpoint = 100;
        current_mentalpower = 10;
        current_mentalprotection = 10;

        player_inventory_list = new List<Inventory>();
        player_quest_list = new List<Quest>();
        player_skill_list = new List<Skill>();

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

    public bool receiveDamage(int damage)
    {
        current_health -= damage;

        if (current_health <= 0)
        {
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
