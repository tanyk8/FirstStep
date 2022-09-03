using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    string quest_name;
    string quest_type; //main or sub
    string quest_description;
    string quest_reward;
    string quest_progress;
    string quest_totalprogress;

    bool quest_completed;

    List<Quest> questlist = new List<Quest>();
}
