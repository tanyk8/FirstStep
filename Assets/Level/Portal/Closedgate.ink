INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-gate_checked==false&&mainquest_progress=="1":
->quest
}

===quest===
The gate is closed with sturdy iron bar#logtype:mono
It seems impossible to open it with brute force #questtrigger:updateprogressvalue #quest_id:101
~gate_checked=true
->END