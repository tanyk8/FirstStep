INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink
//Fear of social
//Fear of being judged
//fear of what other think
//shut in, avoid people
//social anxiety

VAR questionasked=0

{
-mainquest_progress=="0":
->progress0
-mainquest_progress=="1":
->progress1
-mainquest_progress=="2":
->progress2
}

===progress0===
Where am I...?#logtype:di #speaker:??? #portrait:portrait_player
The moment you open your eyes, you noticed that you're in an unfamiliar place.#logtype:mono
You find yourself lying on the hard dusty floor which is made out of stone.
You tried to get up from the floor and looked around the place.
(Maybe I should try and look around) #logtype:di #speaker:??? #portrait:portrait_player #questtrigger:start #quest_id:101
~mainquest_progress="1"
~mainquest_status="inprog"
->END

===progress1===
After looking around, you got a conclusion that you are stuck here.#logtype:mono #questtrigger:complete #quest_id:101
So you went back to the center near the floating crystal.
(Seriously where am I and why I am here?)#logtype:di #speaker:??? #portrait:portrait_player
You attempt to think back of how you got here, but you noticed that you have no memories other than who you are.#logtype:mono
(My name is Yuuki... ugh- I can't remember anything else!)#logtype:di #speaker:Yuuki #portrait:portrait_player
(Seriously what should I do...)
Is anybody here?!!!
You tried to shout at the void and as expected, there is no response at all.#logtype:mono
(The gate doesn't budge, there is endless void surrounding me, and there is that suspicious crystal.)#logtype:di #speaker:Yuuki #portrait:portrait_player
While you were thinking on how to escape this place, you suddenly felt a strong pressure and a scary feeling.#logtype:mono
Then you are suddenly hit by a strong force and you were being blown into the ground.#cutscene:MagicAttack
Wha- Ugh!! AHHHHH!!!#logtype:di #speaker:Yuuki #portrait:portrait_player
You tried to endure the pain as you tried to find the one responsible for the sudden attack.#logtype:mono #animator:mainenemyappear
You only see a dark being that is emitting a lot of dark energy to the surrounding.
You feel great hatred and shadow energy from it.
You feel pain and despair, not knowing what is happening to you, and you are only able to try and endure it.
Are you okay?!#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
You suddenly heard a voice, somehow the voice made you feel slightly better.#logtype:mono
You tried to stay awake but you were unable to resist, and you've fainted.
~mainquest_progress="2"
->END

===progress2===
As you open your eyes, you felt a sense of DeJa Vu as you find yourself lying on the same floor.#logtype:mono
(Was the encounter a dream...?)#logtype:di #speaker:Yuuki #portrait:portrait_player
Oh you're awake!#speaker:??? #portrait:portrait_npc_mysterious
(Seems like not...)#speaker:Yuuki #portrait:portrait_player
(Looks like I have no other choice but to talk to him...)
->question1
===question1===
(What should I ask about?)#speaker:Yuuki #portrait:portrait_player
->nomorechoice
===nomorechoice===
*[Umm...who are you?]
Oh I am *****, but I don't think you can hear it as "he" prevents me to do so.#speaker:??? #portrait:portrait_npc_mysterious
~questionasked=questionasked+1
{
-questionasked==4:
->nomorechoice
}
->question1
*[Where am I?]
Just as you see, this is literally a barren world with endless void surrounding this place.#speaker:??? #portrait:portrait_npc_mysterious
~questionasked=questionasked+1
{
-questionasked==4:
->nomorechoice
}
->question1
*[Who is he?]
I refer him as the "shadow", he is the one preventing you from leaving this world.#speaker:??? #portrait:portrait_npc_mysterious
~questionasked=questionasked+1
{
-questionasked==4:
->nomorechoice
}
->question1
*[What is his goal...?]
Unfortunately, I can't tell you as I am unable to.#speaker:??? #portrait:portrait_npc_mysterious
~questionasked=questionasked+1
{
-questionasked==4:
->nomorechoice
}
->question1

*->
(This is not going anywhere...)#speaker:Yuuki #portrait:portrait_player
->question2
===question2===
Shadow is preventing me from telling you any vital information.#speaker:??? #portrait:portrait_npc_mysterious
So...what should I do or what can I do.#speaker:Yuuki #portrait:portrait_player
Even though I can't give you the direct answer but I still can help to guide you to the answer.#speaker:??? #portrait:portrait_npc_mysterious
You see those 3 gates? I need you to go in there and find the source of the shadow energy to weaken him.
Is it dangerous...?#speaker:Yuuki #portrait:portrait_player
You might have engage in battle with his minions, purifying the minions drops light shard.#speaker:??? #portrait:portrait_npc_mysterious
The light shard can help to power up the crystal that you saw at the center.
The light shard will power you up and protect you from shadow energy.
...is there any other way?#speaker:Yuuki #portrait:portrait_player
He is gaining more power day by day, if you don't stop him soon, I may be defeated by him.#speaker:??? #portrait:portrait_npc_mysterious
If so, there would be no one else that could protect you.
But don't worry, I will guide you and provide you some powers.
You feel a warm energy flowing into your body.#logtype:mono #learnskill:starter
...Seems like I don't have any other options.#logtype:di #speaker:Yuuki #portrait:portrait_player
Well I am on the same boat with you, so let's do our best.#speaker:??? #portrait:portrait_npc_mysterious
So currently I am only able to open the gate on the left side.
The other 2 gates are too strong for me to unlock for now.
(You hear the sturdy iron bar gets lifted up by itselves)#logtype:mono #playse:gate
~gate_portal1=true
Ok so you're good to go now, good luck!#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger:start #quest_id:102
~mainquest_progress="3"
->END