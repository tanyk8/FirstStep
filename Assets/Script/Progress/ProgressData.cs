using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{

    public int player_maxhealth;
    public int player_maxmp;
    public int player_power;
    public int player_protection;
    public int player_currhealth;
    public int player_currmp;

    public Vector3 playerlocation;
    public string sceneName;

    public List<Skill> skillList;
    public List<Status> statusList;
    public List<InventoryItem> inventoryList;
    //public Dictionary<ItemData, InventoryItem> itemDictionary;
    public List<Quest> questList;


    //position

}
