using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SkillManager : MonoBehaviour
{

    public List<Skill> skilllist=new List<Skill>();

    [SerializeField] public SkillData[] skillData;

    private static SkillManager instance;

    private void Start()
    {
        //SkillData testskill = Resources.Load<SkillData>("Skill/skill1");
        //Debug.Log(testskill.skill_name);
        //Skill skill = new Skill(testskill);
        //skill.updateSkillLearnt();
        //skilllist.Add(skill);

        //SkillData testskill2 = Resources.Load<SkillData>("Skill/skill2");
        //Debug.Log(testskill2.skill_name);
        //Skill skill2 = new Skill(testskill2);
        //skill2.updateSkillLearnt();
        //skilllist.Add(skill2);

        //SkillData testskill3 = Resources.Load<SkillData>("Skill/skill3");
        //Debug.Log(testskill3.skill_name);
        //Skill skill3 = new Skill(testskill3);
        //skill3.updateSkillLearnt();
        //skilllist.Add(skill3);

        //SkillData testskill4 = Resources.Load<SkillData>("Skill/skill4");
        //Debug.Log(testskill4.skill_name);
        //Skill skill4 = new Skill(testskill4);
        //skill4.updateSkillLearnt();
        //skilllist.Add(skill4);

        
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static SkillManager GetInstance()
    {
        return instance;
    }

    public int useSkill(int skillindex,string skillname)
    {
        switch (skillname)
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

    public Skill getSkillWName(string name)
    {
        int index = 0;
        index= skilllist.FindIndex(x => x.skillData.skill_name == name);

        return skilllist.ElementAt(index);
    }
    //load player skill(also check if skill learnt)
    //load enemy skill
}
