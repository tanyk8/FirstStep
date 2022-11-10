using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static Player GetInstance()
    {
        return instance;
    }

    //contructor
    public Player()
    {
        stat_maxhealth = 750;
        stat_maxmentalpoint = 200;
        stat_mentalpower = 100;
        stat_mentalprotection = 25;

        current_health = 750;
        current_mentalpoint = 200;
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

    public int playerSkillHeal(Skill skill)
    {
        int original = 0;
        int after = 0;

        original = current_health;
        current_health += stat_maxhealth*(skill.skillData.skill_power-100)/100;

        if (current_health > stat_maxhealth)
        {
            current_health = stat_maxhealth;
            
        }
        after = current_health;
        Debug.Log(after+ "-" +original);
        return (after-original);
    }

    public int playerChannelMP(Skill skill)
    {
        int original = 0;
        int after = 0;

        original = current_mentalpoint;
        current_mentalpoint += stat_maxmentalpoint * (skill.skillData.skill_power - 100) / 100;

        if (current_mentalpoint > stat_maxmentalpoint)
        {
            current_mentalpoint = stat_maxmentalpoint;

        }
        after = current_mentalpoint;
        Debug.Log(after + "-" + original);
        return (after - original);
    }

    public int playerItemHeal(InventoryItem inventoryitem)
    {
        int original = 0;
        int after = 0;

        original = current_health;
        current_health += stat_maxhealth * (inventoryitem.itemData.item_value-100)/100;
        

        if (current_health > stat_maxhealth)
        {
            current_health = stat_maxhealth;
        }
        after = current_health;
        Debug.Log(after + "-" + original);
        return (after - original);
    }

    public void updatePlayerMP(Skill skill)
    {
        current_mentalpoint -= skill.skillData.skill_cost;

        if (current_mentalpoint < 0)
        {
            current_mentalpoint = 0;
        }

    }
}
