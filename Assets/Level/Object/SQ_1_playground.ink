INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

->main

===main===
You searched here and there but you found nothing.#questtrigger:updateprogressvalue #quest_id:1
~playground_checked_count=playground_checked_count+1
{
-playground_checked_count==5:
Seems like there's no other place left to search, I should report back to him.
}
~playground1_checked=true
->END

