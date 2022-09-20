using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{

    public List<Skill> skilllist=new List<Skill>();

    private static SkillManager instance;

    private void Start()
    {
        SkillData testskill = Resources.Load<SkillData>("Skill/skill1");
        Debug.Log(testskill.skill_name);
        Skill skill = new Skill(testskill);
        skill.updateSkillLearnt();
        skilllist.Add(skill);

        SkillData testskill2 = Resources.Load<SkillData>("Skill/skill2");
        Skill skill2 = new Skill(testskill2);
        skill2.updateSkillLearnt();
        skilllist.Add(skill2);
    }

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Debug.LogWarning("Found more than one Skill Manager in the scene");
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public static SkillManager GetInstance()
    {
        return instance;
    }

    public int useSkill(string type, string name)
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
        return 1;

    }

    public void getSkillList(string type)
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

    public bool checkSkillEmpty(string type)
    {
        bool empty = true;

        if (type == "notlearnt")
        {
            for (int x = 0; x < skilllist.Count; x++)
            {
                if (skilllist.ElementAt(x).skillLearnt == false)
                {
                    empty = false;
                }
                if (empty == false)
                {
                    break;
                }
            }
        }
        else if (type == "learnt")
        {
            for (int x = 0; x < skilllist.Count; x++)
            {
                if (skilllist.ElementAt(x).skillLearnt == true)
                {
                    empty = false;
                }
                if (empty == false)
                {
                    break;
                }
            }
        }

        return empty;
    }


    public void updateSkill(List<Skill> listskill)
    {
        skilllist = listskill;
    }
    //load player skill(also check if skill learnt)
    //load enemy skill
}
