using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class SkillData : ScriptableObject
{

    public int skill_ID;
    public string skill_name;
    public string skill_type; //buff or debuff or attack or heal
    public string skill_description;

    public int skill_power;
    public int skill_cost;
    public int skill_cureID;
    public int skill_statusID;
    
}
