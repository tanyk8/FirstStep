INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink
{
-shadow3_first_defeated==false:
->main
-shadow3_first_defeated==true:
->main2
}


===main===
(You feel a strong pressure from shadow, do you wish to engage in battle?)#logtype:mono
+[Yes]
You engaged battle with the shadow.#enemy:finalstage #battle:start
->END
+[No]
Maybe I need some time to prepare
->END


===main2===
(You sense that the shadow has grown stronger than before)#logtype:mono
Are you ready to face him?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
You engaged battle with the shadow.#logtype:mono #battle:start #enemy:finalstage2
->END
+[No]
Maybe I need some time to prepare#speaker:Yuuki #portrait:portrait_player
->END