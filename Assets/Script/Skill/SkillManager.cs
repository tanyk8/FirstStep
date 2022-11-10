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

    private void Awake()
    { 
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
}
