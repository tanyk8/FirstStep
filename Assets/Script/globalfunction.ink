===function completeQuest(questID,complete)===
{questID:
    - 1:
    {complete=="True":
    ~quest_tutorial_complete=true
    ~quest_tutorial_status="done"
    }
    - 2:
    {complete=="True":
    ~quest_tutorial2_complete=true
    ~quest_tutorial2_status="done"
    }
}

===function checkRequirement(questID,complete)===
{questID:
    - 1:
    {complete=="True":
        ~proceed_progress=true
        -else:
        ~proceed_progress=false
    }
    - 2:
    {complete=="True":
        ~proceed_progress=true
        -else:
        ~proceed_progress=false
    }
}
