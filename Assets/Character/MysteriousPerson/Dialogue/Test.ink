INCLUDE ../../../Script/Dialogue/globals.ink
INCLUDE ../../../Script/Dialogue/globalfunction.ink

VAR ID=2
VAR quest_tutorial2_status="inprogress"
VAR quest_tutorial2_progress="2"
VAR quest_tutorial2_complete=true

->main

=== main ===
I am the duplicate? #speaker:?? #portrait:portrait_npc_mysterious #layout:layout_left #questtrigger:none #logtype:mono
{
-quest_tutorial2_status=="inprogress"&&quest_tutorial2_progress=="2":
    +[Get this]
    ->questupdate
    +[Quest]
        {
        -quest_tutorial2_status=="inprogress"&&quest_tutorial2_progress=="1":
            ->questinprogress
        -quest_tutorial2_status=="inprogress"&&quest_tutorial2_progress=="3":
            ->questinprogress2
        -else:
            ->questcheck
        }
    +[Nothing]
    Ok then bye
    ->END
-else:
    +[Quest]
        {
        -quest_tutorial2_status=="inprogress"&&quest_tutorial2_progress=="1":
            ->questinprogress
        -quest_tutorial2_status=="inprogress"&&quest_tutorial2_progress=="3":
            ->questinprogress2
        -else:
            ->questcheck
        }
    +[Battle]
    You engaged in battle#battle:start
    ->END 
    +[Nothing]
    Ok then bye
    ->END
}

===questupdate===
No tell the original that he is stupid #questtrigger:updateprogressvalue #quest_id:2
~quest_tutorial2_progress="3"
->END

=== questcheck===
{quest_tutorial2_complete:
    You already completed my quest
    ->main
    -else:
    Please accept
    +[Yes]
        Thanks  #questtrigger:start #quest_id:2
        ~quest_tutorial2_status="inprogress"
        ~quest_tutorial2_progress="1"
        ->DONE
    +[No]
        Fine, be gone!
        ->END
}


===questinprogress===
Are you done? 1/3
+[Complete quest]
    Let me check #questtrigger:proceedprogress #quest_id:2
    {proceed_progress:
        Thank you my bro 2/3
        ~proceed_progress=false
        ~quest_tutorial2_progress="2"
        -else:
        No you are not done!!!
    } 
    ->END
+[No]
why
->END


=== questinprogress2 ===
Are you done? 2/3
+[Complete quest]
    Let me check #questtrigger:proceedprogress #quest_id:2
    {proceed_progress:
        Thank you my bro 3/3 #questtrigger:complete #quest_id:2
        ~proceed_progress=false
        -else:
        No you are not done!!!
    } 
    ->END
+[No]
why
->END










