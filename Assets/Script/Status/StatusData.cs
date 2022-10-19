using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class StatusData : ScriptableObject
{
    public int status_ID;
    public string status_name;
    public string status_description;
    public float status_value;
    public int status_duration;
    public string status_type;
}
