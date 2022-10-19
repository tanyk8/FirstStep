using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Skill
{
    public SkillData skillData;
    public bool skillLearnt;
    public int id;
    public Skill(SkillData skilldata)
    {
        skillData = skilldata;
        skillLearnt = false;
        id = skillData.skill_ID;
    }

    public void updateSkillLearnt()
    {
        skillLearnt = true;
    }
}
