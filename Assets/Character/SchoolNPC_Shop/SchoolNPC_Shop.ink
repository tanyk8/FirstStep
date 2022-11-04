INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-shopchocolate_bought==false&&quest2_progress=="4":
->quest
-shopchocolate_bought==true:
->main
}



===main===
If you're feeling stressful remember to take a rest.#logtype:mono
->END

===quest===
I'll create an illusion that makes you look like a student of this school.
#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Hi how may I help you?#speaker:Shopkeeper #portrait:portrait_shopkeeper
I would like to buy a bag of chocolate#speaker:Yuuki #portrait:portrait_player
That would be 200 penny#speaker:Shopkeeper #portrait:portrait_shopkeeper #removeitem:item4 
You handed him 200 penny and you received a bag of chocolate#logtype:mono #getitem:item5
Thank you, have a nice day.#logtype:di #speaker:Shopkeeper #portrait:portrait_shopkeeper
Ok lets return to him#speaker:??? #portrait:portrait_npc_mysterious
~quest2_progress="41"
~shopchocolate_bought=true
->END
