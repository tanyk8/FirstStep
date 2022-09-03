using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int mental_health;
    int mental_power;

    List<Inventory> player_inventory;

    struct Inventory
    {
        string item_name;
        string item_type;
        int item_quantity;
        string item_description;
    }

    struct Quest
    {
        string quest_name;
        string quest_type; //main or sub
        string quest_description;
        string quest_reward;
        string quest_progress;
        bool quest_completed;

    }


}
