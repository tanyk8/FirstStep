INCLUDE ../../../Script/globals.ink
INCLUDE ../../../Script/globalfunction.ink

VAR ID=2

->main

=== main ===
I am the duplicate? #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:none
+[Quest]
    {quest_tutorial2_status=="inprogress":
        ->questinprogress
      - else:
        ->questcheck
    }
+[Nothing]
Ok then bye
->END

=== questcheck===
{quest_tutorial2_complete:
    You already completed my quest
    ->main
    -else:
    Please accept
    +[Yes]
        Thanks #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:start
        ~quest_tutorial2_status="inprogress"
        ->DONE
    +[No]
        Fine, be gone!
        ->END
}



=== questinprogress ===
Are you done?
+[Complete quest]
    Let me check#speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:update
    {quest_tutorial2_complete:
        Thank you my bro #speaker:??? #portrait:portrait_npc_mysterious #layout:layout_left #questevent:complete
        -else:
        No you are not done!!!
    } 
    ->END
+[No]
why
->END








