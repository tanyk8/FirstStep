using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestData : ScriptableObject
{
    public int quest_ID;
    public string quest_giver;
    public string quest_name;
    public string quest_type; //main or sub
    public string quest_description;
    public string quest_rewardType;
    public int quest_reward;
    public int quest_totalprogress;

    public QuestProgress[] quest_progress;
    
    
    [System.Serializable]
    public struct QuestProgress
    {
        public string description;
        public string requirementType;
        public int requirement;
    }
}
