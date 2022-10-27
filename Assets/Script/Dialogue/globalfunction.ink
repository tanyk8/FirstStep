===function completeQuest(questID,complete)===
{questID:
    - 1:
    {complete=="True":
    ~quest1_complete=true
    ~quest1_status="complete"
    }
    
    - 102:
    {complete=="True":
    ~quest102_complete=true
    ~quest102_status="complete"
    }
    
    - 2:
    {complete=="True":
    ~quest2_complete=true
    ~quest2_status="complete"
    }
    
    - 103:
    {complete=="True":
    ~quest103_complete=true
    ~quest103_status="complete"
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
    
    - 102:
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
    
    - 103:
    {complete=="True":
        ~proceed_progress=true
        -else:
        ~proceed_progress=false
    }
    
    - 3:
    {complete=="True":
        ~proceed_progress=true
        -else:
        ~proceed_progress=false
    }
}
