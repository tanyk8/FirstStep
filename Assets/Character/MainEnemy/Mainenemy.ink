INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

->main

===main===
(You see a cloud of shadow energy, do you wish to engage in battle?)
+[Yes]
You engaged battle with the shadow.#enemy:finalstage #battle:start
->END
+[No]
->END