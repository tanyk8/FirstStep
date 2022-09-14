using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillData : ScriptableObject
{
    string skill_name;
    string skill_type; //buff or debuff or attack or heal
    string skill_description;

    int skill_power;
    int skill_cost;

    
}
