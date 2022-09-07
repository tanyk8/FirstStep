using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Quest : MonoBehaviour
{
    [Header("NPCName")]
    [SerializeField] string npcname;
    [SerializeField] GameObject Dialoguemanager;

    [Header("test value")]
    [SerializeField] int testval=30;

    string quest_giver;
    string quest_name;
    string quest_type; //main or sub
    string quest_description;
    string quest_reward;
    string quest_rewardType;
    QuestProgress[] quest_progress;
    string quest_totalprogress;

    bool quest_completed;


    struct QuestProgress
    {
        bool progress;
        string description;
        string goal;
    }


    

    List<Quest> questlist = new List<Quest>();

    //public Quest getQuestByNPCName(string npcname)
    //{
    //    Quest tmp=new Quest();
    //    //get quest info from file
    //    //quest file separated by stages
    //    return tmp;
    //}

    //trigger event
    //when to listen for event

    
    
    public void startQuest()
    {
        Debug.Log("Quest start");
    }

    public void checkProgress()
    {

    }

    public void forfreitQuest()
    {

    }

    public void updateQuest()
    {
        Debug.Log("Quest update");
        Story temp=DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
        temp.EvaluateFunction("checkRequirement", testval);

        //check requirement
        //talk to target,gather,kill target(use event to update)
    }

    public void completeQuest()
    {
        Debug.Log("Quest end");
        //if gt reward
        //based on reward type(item,stat,smth)
        //give reward
    }


}

//Dialogue handled by inky, use variable and invoke function, get variable when story end to handle progress dialogue