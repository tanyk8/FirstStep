using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;

public enum QuestState { NONE,INPROGRESS,COMPLETED}
[Serializable]
public class Quest
{
    

    public QuestData questData;
    public QuestState questState;
    public int quest_progress;
    public int quest_progressvalue;

    

    public Quest(QuestData questdata)
    {
        questData = questdata;
        questState = QuestState.NONE;
        quest_progress = 0;
        quest_progressvalue = 0;
    }
    
    public void updateQuestState()
    {
        if (questState == QuestState.NONE)
        {
            questState=QuestState.INPROGRESS;
        }
        else if (questState == QuestState.INPROGRESS)
        {
            questState = QuestState.COMPLETED;
        }
    }

    public string getQuestRewardType()
    {
        return questData.quest_rewardType;
    }
    
    public int getQuestReward()
    {
        return questData.quest_reward;
    }

    public void addProgressValue()
    {
        quest_progressvalue++;
    }

    public void resetProgressValue()
    {
        quest_progressvalue = 0;
    }

}

//Dialogue handled by inky, use variable and invoke function, get variable when story end to handle progress dialogue