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

    bool skill_learnt;

    List<Skill> skilllist = new List<Skill>();
    
    

    //load player skill(also check if skill learnt)
    //load enemy skill
}
