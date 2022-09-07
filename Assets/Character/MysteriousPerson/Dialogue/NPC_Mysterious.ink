INCLUDE ../../../Script/globals.ink


->main

=== main ===
Hi, how may I help you today? #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:none
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

=== questcheck===
{quest_tutorial_complete:
    You already completed my quest
    ->main
    -else:
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


===chosen(playerchoice)===
You chose <b><color=\#5B81FF>{playerchoice}</color></b>!
->END


=== questinprogress ===
Are you done?
+[Complete quest]
    Let me check#speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:update
    {quest_tutorial_complete:
        Thank you my friend #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:complete
        -else:
        No you are not done!!!
    }
    ->END
+[No]
Carry on
->END








===function checkRequirement(value)===
{value>quest_tutorial_requiredhp:
~quest_tutorial_complete=true
~quest_tutorial_status="done"
}






