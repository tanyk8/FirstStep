INCLUDE ../../../Script/globals.ink
INCLUDE ../../../Script/globalfunction.ink

VAR ID=1

->main

=== main ===
Hi, how may I help you today? #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:none
{
-quest_tutorial2_status=="inprogress"&&quest_tutorial2_talktonpc==false:
    +[Talk]
    ->chosen("Talk")
    +[Buy]
    ->chosen("Buy")
    +[Sell]
    ->chosen("Sell")
    +[Quest1]
        {quest_tutorial_status=="inprogress":
            ->questinprogress
          - else:
            ->questcheck
        }
    +[Get this]
    ->questupdate
    +[Nothing]
    Ok then bye
    ->END
-else:
    +[Talk]
    ->chosen("Talk")
    +[Buy]
    ->chosen("Buy")
    +[Sell]
    ->chosen("Sell")
    +[Quest]
        {quest_tutorial_status=="inprogress":
            ->questinprogress
          - else:
            ->questcheck
        }
    +[Nothing]
    Ok then bye
    ->END
}


===chosen(playerchoice)===
You chose <b><color=\#5B81FF>{playerchoice}</color></b>!
->END


===questupdate===
Ok tell the duplicate that he is stupid
~quest_tutorial2_talktonpc=true
->END

=== questcheck===
{quest_tutorial_complete:
    You already completed my quest
    ->main
    -else:
    {quest_tutorial_status:
        -"inprogress":
            ->questinprogress
        -"inprogress2":
            ->questinprogress2
        -"inprogress3":
            ->questcomplete
    }
    What did you say?
    Oh so you would like to accept my quest?
    +[Yes]
        Well then carry on with my quest #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:start
        ~quest_tutorial_status="inprogress"
        ->DONE
    +[No]
        Fine, be gone!
        ->END
}



=== questinprogress ===
Are you done? part 1
+[Yes]
    Let me check#speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:update
    {proceed_progress:
        Thank you my friend
        ~proceed_progress=false
        ~quest_tutorial_status="inprogress2"
        -else:
        No you are not done!!!
    }
    ->END
+[No]
Carry on
->END

=== questinprogress2===
Are you done? part 2
+[Yes]
    Let me check#speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:update
    {proceed_progress:
        Thank you my friend
        ~proceed_progress=false
        ~quest_tutorial_status="inprogress3"
        -else:
        No you are not done!!!
    }
    ->END
+[No]
Carry on
->END

===questcomplete===
Are you done? final
+[Yes]
    Let me check#speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:update
    {quest_tutorial_complete:
        Thank you my friend #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:complete
        ~proceed_progress=false
        -else:
        No you are not done!!!
    }
    ->END
+[No]
Carry on
->END










