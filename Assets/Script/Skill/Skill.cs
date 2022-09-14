using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    SkillData skillData;
    bool skillLearnt;

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
