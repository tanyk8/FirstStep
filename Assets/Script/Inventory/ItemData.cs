using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int item_ID;
    public string item_name;
    public Sprite item_icon;
    public string item_description;
    public string item_type;
    public string item_usetype;
    public int item_value;
    public int item_statusID;
}
