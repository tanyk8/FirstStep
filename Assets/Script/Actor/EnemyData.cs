using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string enemy_name;
    
    public int enemy_maxhealth;
    public int enemy_currenthealth;
    
    public int enemy_damage;
    public int enemy_defence;

    public ESkill[] enemy_skilllist;

    public struct ESkill
    {
        string enemy_skillname;
        float enemy_skillvalue;
    }
}
