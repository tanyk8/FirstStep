INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

->main

===main===
You closed the curtains.#questtrigger:updateprogressvalue #quest_id:3
~window_checked_count=window_checked_count+1
{
-window_checked_count==2:
Ok seems like I've closed everything, let's report back
}
~window2_checked=true
->END