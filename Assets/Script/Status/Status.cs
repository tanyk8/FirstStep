using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Status
{
    public StatusData statusData;
    public int current_duration;
    public int id;

    public Status(StatusData statusdata)
    {
        statusData = statusdata;
        current_duration = 0;
        id = statusData.status_ID;
    }

}
