INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-crystal_checked==false&&mainquest_progress=="1":
->quest
-crystal_checked==true:
->main
}

===main===
The crystal shines dimmly 
->END

===quest===
You find a large crystal at the middle of the room.#logtype:mono
It is levitating on mid air and the crystal is shinning dimmly.
You couldn't find any way to interact with it.#questtrigger:updateprogressvalue #quest_id:101
~crystal_checked=true

->END