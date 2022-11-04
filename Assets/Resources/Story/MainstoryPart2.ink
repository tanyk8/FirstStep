INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-mainquest_progress=="3":
->main
-mainquest_progress=="4":
->progress1
-mainquest_progress=="6":
->progress2
}

===main===
The moment you step out of the gate, you see a trees and a lake#logtype:mono
The warm sun shinning down, it is a great weather.#questtrigger:updateprogressvalue #quest_id:102
This is not what I am expecting, such a peaceful place.#logtype:di #speaker:Yuuki #portrait:portrait_player
Do they actually exist here?
Yeah of course they do, well just as the name suggests, they don't show themselves.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
You could hear the mysterious person's voice through your head.#logtype:mono
The powers that I've shared with you also allow me to link myself with you.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Which means I have the access to all your senses.
I will be helping you on your mission by giving advice too.
So maybe you can try to look around and see if you can find any source of lurking shadow energy. 
Currently I can sense 1 source in this park. Maybe you can start by finding it.#questtrigger:proceedprogress #quest_id:102 #questtrigger_type:force
~mainquest_progress="4"
->END

===progress1===
You sense a weak shadow energy from the person at the middle of the small lake.#logtype:mono #questtrigger:updateprogressvalue #quest_id:102
Seems like it's him.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
However, we need to make the shadow minion leave the person, or it will have unlimited energy.
Which is rather impossible to deal with.
How can we do that?#logtype:di #speaker:Yuuki #portrait:portrait_player
Well the best way is to identify what is causing the shadow energy to accumulate.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
He do seem troubled, maybe helping him would help to weaken the energy and make the shadow minion leave.
Let's approach him.
Ok.#logtype:di #speaker:Yuuki #portrait:portrait_player
Before that I need to make you visible to him as you don't really have a physical form right now.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
So I am invisible to other people?#speaker:Yuuki #portrait:portrait_player
Basically yes, but I can help you to become visible so don't worry.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger:proceedprogress #quest_id:102 #questtrigger_type:force
~areatrigger102_1=true
~mainquest_progress="5"
->END

===progress2===
~mainquest_progress="7"
(What was that? Is it someone's memory?)#logtype:di #speaker:Yuuki #portrait:portrait_player
You somehow feel unease about it but you don't have much clue about it#logtype:mono
Great you defeated it!#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger:proceedprogress #quest_id:102 #questtrigger_type:force
Now lets take the shard back to the crystal.#getitem:item1
->END

