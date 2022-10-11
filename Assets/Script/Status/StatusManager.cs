using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusManager : MonoBehaviour
{
    public List<Status> statusList=new List<Status>();

    private static StatusManager instance;


    private void Start()
    {
        //StatusData teststatus = Resources.Load<StatusData>("Status/status1");
        //Debug.Log(teststatus.status_name);
        //Status status = new Status(teststatus);
        //statusList.Add(status);

        //StatusData teststatus2 = Resources.Load<StatusData>("Status/status2");
        //Status status2 = new Status(teststatus2);
        //statusList.Add(status2);
    }

    private void Awake()
    {
        //if (instance != null)
        //{
        //    Debug.LogWarning("Found more than one Status Manager in the scene");
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
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

    public static StatusManager GetInstance()
    {
        return instance;
    }

    public bool checkStatusEmpty()
    {
        bool empty = true;

        if (statusList.Count != 0)
        {
            empty = false;
        }

        return empty;
    }

    public void updateStatus(List<Status> liststatus)
    {
        statusList = liststatus;
    }

    public Status getStatusWName(string name)
    {
        int index=0;


        index=statusList.FindIndex(x => x.statusData.status_name == name);

        return statusList.ElementAt(index);
    }

    public Status getStatusWID(int id)
    {
        int index = 0;


        index = statusList.FindIndex(x => x.statusData.status_ID == id);

        return statusList.ElementAt(index);
    }

    public void skill_CureStatus(Skill skill)
    {
        int index = 0;

        index = statusList.FindIndex(x => x.statusData.status_ID == skill.skillData.skill_cureID);

        statusList.RemoveAt(index);
    }

    public void skill_InflictBuffStatus(Skill skill)
    {

        StatusData teststatus = Resources.Load<StatusData>("Status/status"+skill.skillData.skill_statusID);
        Status status = new Status(teststatus);

        bool exist = false;
        int index = 0;

        //if already have reset duration
        for(int x = 0; x < statusList.Count; x++)
        {
            if (statusList.ElementAt(x).statusData.status_ID == skill.skillData.skill_statusID)
            {
                exist = true;
                index = x;
                return;
            }
        }

        if (exist)
        {
            statusList.ElementAt(index).current_duration = statusList.ElementAt(index).statusData.status_duration;
        }
        else
        {
            statusList.Add(status);
            statusList.ElementAt(statusList.Count - 1).current_duration = statusList.ElementAt(statusList.Count - 1).statusData.status_duration;
        }
        
    }

    public void skill_InflictDebuffStatus(EnemySkill enemyskill)
    {

        StatusData teststatus = Resources.Load<StatusData>("Status/status" + enemyskill.enemyskill_statusID);
        Status status = new Status(teststatus);

        bool exist = false;
        int index = 0;

        //if already have reset duration
        for (int x = 0; x < statusList.Count; x++)
        {
            if (statusList.ElementAt(x).statusData.status_ID == enemyskill.enemyskill_statusID)
            {
                exist = true;
                index = x;
                return;
            }
        }

        if (exist)
        {
            statusList.ElementAt(index).current_duration = statusList.ElementAt(index).statusData.status_duration;
        }
        else
        {
            statusList.Add(status);
            statusList.ElementAt(statusList.Count - 1).current_duration = statusList.ElementAt(statusList.Count - 1).statusData.status_duration;
        }

    }

    public void item_CureStatus(InventoryItem inventoryItem)
    {
        int index = 0;

        index = statusList.FindIndex(x => x.statusData.status_ID == inventoryItem.itemData.item_statusID);

        statusList.RemoveAt(index);
    }

    public void updateStatusDuration()
    {
        for(int x = 0; x < statusList.Count; x++)
        {
            statusList.ElementAt(x).current_duration -= 1;
        }

        for (int x = 0; x < statusList.Count; x++)
        {
            if(statusList.ElementAt(x).current_duration == 0)
            {
                statusList.RemoveAt(x);
            }
        }
    }

}
