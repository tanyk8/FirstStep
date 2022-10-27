INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-crystal_checked==false&&mainquest_progress=="1":
->quest
-crystal_checked==true&&mainquest_progress=="7":
->clear1
-crystal_checked==true&&mainquest_progress=="11":
->clear2
-crystal_checked==true:
->main
}

===main===
The crystal shines dimmly.
->END

===clear1===
Do you wish to restore the crystal with shard of light?
+[Yes]
You search your inventory for the shard#questtrigger:proceedprogress #quest_id:102
    {proceed_progress:
        ~proceed_progress=false
        ~gate_portal2=true
        ~mainquest_progress="8"
        You took out a piece of shard of light, the shard reacted with the crystal and it disappears#removeitem:item1
        You feel the power within yourself gets stronger.#questtrigger:complete #quest_id:102
        Okay now I will open the second gate.
        You hear a loud noise and you see that the second gate has been opened.#questtrigger:start #quest_id:103
        ->END
        -else:
        (I haven't finish searching yet)
    }
->END
+[No]
->END

===clear2===
Do you wish to restore the crystal with shard of light?
+[Yes]
You search your inventory for the shard#questtrigger:proceedprogress #quest_id:103
    {proceed_progress:
        ~proceed_progress=false
        ~gate_portal3=true
        ~mainquest_progress="12"
        You took out a piece of shard of light, the shard reacted with the crystal and it disappears#removeitem:item1
        You feel the power within yourself gets stronger.#questtrigger:complete #quest_id:103
        Okay now I will open the third gate.
        You hear a loud noise and you see that the third gate has been opened.#questtrigger:start #quest_id:104
        ->END
        -else:
        (I haven't finish searching yet)
    }
->END
+[No]
->END


===quest===
You find a large crystal at the middle of the room.#logtype:mono
It is levitating on mid air and the crystal is shinning dimmly.
You couldn't find any way to interact with it.#questtrigger:updateprogressvalue #quest_id:101
~crystal_checked=true

->END