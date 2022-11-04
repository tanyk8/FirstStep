INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-mainquest_progress=="8":
->main
-mainquest_progress=="91":
->progress91
-mainquest_progress=="10":
->progress1
}

===main===
When you exit the portal, you see a enclosed space with wooden flooring.#logtype:mono #questtrigger:updateprogressvalue #quest_id:103
It seems like you are at a hallway in a building.
Is this a school...?#logtype:di #speaker:Yuuki #portrait:portrait_player
Somehow you have vague impression that this is a school building.#logtype:mono
But you can't remember anything else.
Ok you know the drill, let's move on to find the shadow.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger:proceedprogress #quest_id:103 #questtrigger_type:force
~mainquest_progress="91"
->END

===progress91===
~areatrigger103=true
I can sense the energy in this room.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
I think it's that boy at the corner seat.#questtrigger:proceedprogress #questtrigger_type:force #quest_id:103
~mainquest_progress="9"
->END

===progress1===
~mainquest_progress="11"
~shadow2_defeated=true
(Again with this...but...)#logtype:di #speaker:Yuuki #portrait:portrait_player
You somehow have a feeling that it is related to yourself#logtype:mono
Great you defeated it!#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger:proceedprogress #quest_id:103 #questtrigger_type:force
Now lets take the shard back to the crystal.#getitem:item1
->END
