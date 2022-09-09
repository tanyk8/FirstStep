using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    [Header("ManagerRef")]
    [SerializeField] GameObject dialoguemanager;
    [SerializeField] GameObject playermanager;
    [SerializeField] GameObject inventorymanager;
    [SerializeField] GameObject progressmanager;

    public List<Quest> questlist=new List<Quest>();

    public void startQuest(QuestData questData)
    {
        Debug.Log("Quest start");
        Quest quest = new Quest(questData);

        int questID = questData.quest_ID;
        questlist.Add(quest);
        questlist.ElementAt(findQuestIndexwithID(questID)).quest_progress++;
        questlist.ElementAt(findQuestIndexwithID(questID)).questState=QuestState.INPROGRESS;
        checkList();
    }

    public void updateQuest(QuestData questData)
    {
        Debug.Log("Quest update");
        Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
        //int questID = int.Parse(temp.variablesState.GetVariableWithName("ID").ToString());
        int questID = questData.quest_ID;
        if (questlist.ElementAt(findQuestIndexwithID(questID)).quest_progress < questData.quest_totalprogress)
        {
            temp.EvaluateFunction("checkRequirement", questID, checkRequirement(questData,questID).ToString());
            if (temp.variablesState.GetVariableWithName("proceed_progress").ToString()=="true")
            {
                questlist.ElementAt(findQuestIndexwithID(questID)).quest_progress++;
                questlist.ElementAt(findQuestIndexwithID(questID)).resetProgressValue();
            }
        }
        else
        {
            temp.EvaluateFunction("completeQuest", questID, checkRequirement(questData,questID).ToString());
        }


    }

    public void completeQuest(QuestData questData)
    {
        Debug.Log("Quest end");
        Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
        int questID = questData.quest_ID;
        

        if (questlist.ElementAt(findQuestIndexwithID(questID)).getQuestRewardType() == "stat")
        {
            playermanager.GetComponent<Player>().addStat_MentalPower(questlist.ElementAt(findQuestIndexwithID(questID)).getQuestReward());
        }

        questlist.ElementAt(findQuestIndexwithID(questID)).questState = QuestState.COMPLETED;

    }

    public bool checkRequirement(QuestData questData,int questID)
    {
        int requirement = 0;
        bool complete = false;

        requirement = questData.quest_progress.ElementAt(questlist.ElementAt(findQuestIndexwithID(questID)).quest_progress - 1).requirement;


        
        if (questlist.ElementAt(findQuestIndexwithID(questID)).quest_progressvalue >= requirement)
        {
            complete = true;
        }
        checkList();
        Debug.Log(questlist.ElementAt(findQuestIndexwithID(questID)).quest_progressvalue + ">=" + requirement);
        return complete;
    }

    public int findQuestIndexwithID(int id)
    {
        return questlist.FindIndex(x => x.questData.quest_ID == id);
    }

    private void checkList()
    {
        string output = "";
        for (int i = 0; i < questlist.Count; i++)
        {
            output += "id:";
            output += questlist.ElementAt(i).questData.quest_ID;
            output += "|progress:";
            output += questlist.ElementAt(i).quest_progress;
            output += "|progressvalue:";
            output += questlist.ElementAt(i).quest_progressvalue;
            output += ",";
        }
        Debug.Log(output);
    }

    public bool checkQuestInProgress(int id)
    {
        bool found = false;

        for(int x = 0; x < questlist.Count; x++)
        {
            if (questlist.ElementAt(x).questData.quest_ID == id)
            {
                found = true;
            }
        }


        return found;
    }
}
