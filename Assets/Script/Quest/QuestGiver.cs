using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestData[] questData;

    public QuestData getTargetQuestData(int id)
    {

        QuestData temp=null;
        for (int x = 0; x < questData.Length; x++)
        {
            if (questData[x].quest_ID == id)
            {
                temp = questData[x];
            }
        }

        return temp;
    }
}
