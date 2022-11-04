INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

->main

===main===
You tired to search here and there around the fountain.#logtype:mono
You noticed a small piece of item on the border wall of the fountain.
You picked the item up and it was a guitar pick.
I think it's that one, I can sense the emotion link to him#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Great, let's deliver this to him.#speaker:Yuuki #portrait:portrait_player #getitem:item3
~fountain_checked=true
->END