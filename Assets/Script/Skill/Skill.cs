using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Skill
{
    public SkillData skillData;
    public bool skillLearnt;

    public Skill(SkillData skilldata)
    {
        skillData = skilldata;
        skillLearnt = false;
    }

    public void updateSkillLearnt()
    {
        skillLearnt = true;
    }
}
