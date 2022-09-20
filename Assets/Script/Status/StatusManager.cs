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
        StatusData teststatus = Resources.Load<StatusData>("Status/status1");
        Debug.Log(teststatus.status_name);
        Status status = new Status(teststatus);
        statusList.Add(status);

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
}
