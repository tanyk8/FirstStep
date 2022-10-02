using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemySkill : ScriptableObject
{
    public int enemyskill_ID;
    public string enemyskill_name;
    public string enemyskill_type; //buff or debuff or attack or heal
    public string enemyskill_description;

    public int enemyskill_power;
    public int enemyskill_cost;
    public int enemyskill_statusID;
}
