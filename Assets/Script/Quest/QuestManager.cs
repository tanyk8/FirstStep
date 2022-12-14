using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.Linq;
using UnityEngine.SceneManagement;


public class QuestManager : MonoBehaviour
{

    [SerializeField] GameObject questmanager;

    private static QuestManager instance;

    [Header("ManagerRef")]
    [SerializeField] GameObject dialoguemanager;
    [SerializeField] GameObject playermanager;
    [SerializeField] GameObject inventorymanager;
    [SerializeField] GameObject progressmanager;

    public List<Quest> questlist=new List<Quest>();

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            Destroy(gameObject);
        }
    }

    public static QuestManager GetInstance()
    {
        return instance;
    }

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

    public void updateKillProgressValue()
    {

    }

    public void updateGatherProgressValue(int questid, int stacksize)
    {
        questlist.ElementAt(findQuestIndexwithID(questid)).setProgressValue(stacksize);
        

    }

    public void updateTalkProgressValue(int receivequest_ID)
    {
        int targetIndex = findQuestIndexwithID(receivequest_ID);
        questlist.ElementAt(targetIndex).addProgressValue();
        checkList();
    }

    public void updateQuestProgress(QuestData questData,string type)
    {
        Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
        int questID = questData.quest_ID;
        int targetIndex = findQuestIndexwithID(questID);

        if (type == "proceedprogress")
        {
            if (questlist.ElementAt(targetIndex).quest_progress < questData.quest_totalprogress-1)
            {
                temp.EvaluateFunction("checkRequirement", questID, checkRequirement(questData, questID).ToString());
                if (temp.variablesState.GetVariableWithName("proceed_progress").ToString() == "true")
                {
                    questlist.ElementAt(targetIndex).quest_progress++;
                    questlist.ElementAt(targetIndex).resetProgressValue();
                }
            }
            else if (questlist.ElementAt(targetIndex).quest_progress == questData.quest_totalprogress-1)
            {
                temp.EvaluateFunction("checkRequirement", questID, checkRequirement(questData, questID).ToString());
                if (temp.variablesState.GetVariableWithName("proceed_progress").ToString() == "true")
                {
                    questlist.ElementAt(targetIndex).quest_progress++;
                    questlist.ElementAt(targetIndex).resetProgressValue();
                }
            }
        }
        else if (type == "force")
        {
            if (questlist.ElementAt(targetIndex).quest_progress < questData.quest_totalprogress - 1)
            {

                questlist.ElementAt(targetIndex).quest_progress++;
                questlist.ElementAt(targetIndex).resetProgressValue();

            }
            else if (questlist.ElementAt(targetIndex).quest_progress == questData.quest_totalprogress - 1)
            {
                questlist.ElementAt(targetIndex).quest_progress++;
                questlist.ElementAt(targetIndex).resetProgressValue();
            }
        }
        checkList();
    }

    public void completeQuest(QuestData questData)
    {
        Debug.Log("Quest end");
        Story temp = DialogueManager.GetInstance().GetComponent<DialogueManager>().getStory();
        int questID = questData.quest_ID;

        temp.EvaluateFunction("completeQuest", questID, "True");

        questlist.ElementAt(findQuestIndexwithID(questID)).questState = QuestState.COMPLETED;
        questlist.ElementAt(findQuestIndexwithID(questID)).resetProgressValue();
        checkList();

    }

    public bool checkRequirement(QuestData questData,int questID)
    {
        int requirement = 0;
        bool complete = false;
        int targetIndex = findQuestIndexwithID(questID);


        requirement = questData.quest_progress.ElementAt(questlist.ElementAt(targetIndex).quest_progress - 1).requirement;
        Debug.Log(questlist.ElementAt(targetIndex).quest_progressvalue+"/"+ requirement);

        if (questlist.ElementAt(targetIndex).quest_progressvalue >= requirement)
        {
            complete = true;
        }
        
        //else if (type == "final")
        //{
        //    requirement = questData.quest_totalprogress;
        //    if (questlist.ElementAt(targetIndex).quest_progress >= requirement)
        //    {
        //        complete = true;
        //    }
        //}
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
            output += "|state:";
            output += questlist.ElementAt(i).questState;
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

    public bool checkQuestListEmpty(string type)
    {
        bool empty = true;

        if (type == "inprog")
        {
            for (int x = 0; x < questlist.Count; x++)
            {
                if (questlist.ElementAt(x).questState == QuestState.INPROGRESS)
                {
                    empty = false;
                }
                if (empty == false)
                {
                    break;
                }
            }
        }
        else if (type == "completed")
        {
            for(int x=0;x< questlist.Count; x++){
                if (questlist.ElementAt(x).questState == QuestState.COMPLETED)
                {
                    empty = false;
                }
                if (empty == false)
                {
                    break;
                }
            }
        }

        return empty;
    }

    public void updateQuestManager(List<Quest> questList)
    {
        questlist = questList;
    }

    public QuestManager getQuestManager()
    {
        return this;
    }

    public Quest getQuestWName(string name)
    {
        int index = 0;
        index = questlist.FindIndex(x => x.questData.quest_name == name);

        return questlist.ElementAt(index);
    }

    public bool checkQuestExist(int id)
    {
        bool result = false;
        if (questlist.Count != 0)
        {
            result = questlist.Exists(x => x.questData.quest_ID == id);
        }
        

        return result;
    }

    public string getRequirmentType(int id,int progress)
    {
        int index = 0;
        index = questlist.FindIndex(x => x.questData.quest_ID == id);

        return questlist.ElementAt(index).questData.quest_progress[progress].requirementType;
    }

    public int getCurrentProg(int id)
    {
        int index = 0;
        index = questlist.FindIndex(x => x.questData.quest_ID == id);

        return questlist.ElementAt(index).quest_progress;
    }
}
