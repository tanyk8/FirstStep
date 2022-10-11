INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-void_checked==false&&mainquest_progress=="1":
->quest
-void_checked==true:
->main
}

===main===
You see an endless void, it seems impossible to escape this place.
->END

===quest===
When you look far away, all you can see is an endless void.#logtype:mono
Even if you look down black space streches out without limit.
It seems impossible to escape this place.#questtrigger:updateprogressvalue #quest_id:101
~void_checked=true
->END
