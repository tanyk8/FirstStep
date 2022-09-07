using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    string skill_name;
    string skill_type; //buff or debuff or attack or heal
    string skill_description;

    int skill_power;
    int skill_cost;

    List<Skill> skillList = new List<Skill>();

    public void convertRawToAsset()
    {
        //convert raw txt file to serialized file
    }

    public void loadSkillList()
    {
        //load serialized file
    }

    public void useSkill(string type,string name)
    {
        switch (type)
        {
            case "buff":
                useSkill_Buff(name);
                break;
            case "debuff":
                useSkill_Debuff(name);
                break;
            case "damage":
                useSkill_Damage(name);
                break;
            case "heal":
                useSkill_Heal(name);
                break;
        }

    }

    public List<Skill> getSkillList(string type)
    {
        if (type == "learnt")
        {

        }
        else if (type == "unlearnt")
        {

        }
        else if (type == "all")
        {

        }

        return skillList;
    }

    public void useSkill_Buff(string name)
    {
        //Trust yourself
        //Pump up
        //Be positive
        //Praise yourself
        //Relax
    }

    public void useSkill_Debuff(string name)
    {
        //Try
    }

    public void useSkill_Damage(string name)
    {
        //Do my best
    }

    public void useSkill_Heal(string name)
    {
        //Try to not be too concerned on the stressful
        //Enjoy hobby
        //Sleep well
        //Eat well
        //Share your stress
        //Rest
    }



    //load player skill(also check if skill learnt)
    //load enemy skill
}
