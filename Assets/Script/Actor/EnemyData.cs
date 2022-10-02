using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string enemy_name;
    public Sprite enemy_sprite;
    
    public int enemy_maxhealth;
    public int enemy_currenthealth;
    
    public int enemy_damage;
    public int enemy_defence;

    public EnemySkill[] enemy_skilllist;


}
