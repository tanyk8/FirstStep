INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-mainquest_progress=="12":
->main
-mainquest_progress=="14":
->progress1
-mainquest_progress=="15":
->progress2
-mainquest_progress=="16":
->progress3
}

===main===
When you exit the portal, you see a enclosed white hallway.#logtype:mono #questtrigger:updateprogressvalue #quest_id:104
There are medicine smell floating around the place.
This time it is the hospital...?#logtype:di #speaker:Yuuki #portrait:portrait_player
Somehow you have a mixed feeling about the place.#logtype:mono
Your legs is slightly trembling, but you are not sure of the reason.
Ok let's find the source.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
This is the last place with shadow energy, we must be careful of him.
He might come to stop you.
But don't worry I have regain large portion of my powers, I will support you.#questtrigger:proceedprogress #quest_id:104 #questtrigger_type:force
~mainquest_progress="13"
->END

===progress1===
W- what is with this strong presence?!#logtype:di #speaker:Yuuki #portrait:portrait_player #questtrigger_type:force #questtrigger:proceedprogress #quest_id:104
It's him...#speaker:??? #portrait:portrait_npc_mysterious
... ...#speaker:??? #portrait:portrait_shadow
Looks like he's here to stop us from purifying the last source of shadow energy.#portrait:portrait_npc_mysterious
Well we already expected his appearance, no matter what I have to defeat him to escape from this world.#speaker:Yuuki #portrait:portrait_player
Don't worry I am here for you, I will lend you my strength.#portrait:portrait_npc_mysterious
Thanks, let's do this!#speaker:Yuuki #portrait:portrait_player
~mainquest_progress="15"
->END


===progress2===
~shadow3_first_defeated=true
(That voice... It's mom...)#logtype:di #speaker:Yuuki #portrait:portrait_player
After a long battle you took some distance.#logtype:mono
...Is he weakened yet?#logtype:di #speaker:Yuuki #portrait:portrait_player
Doesn't seem like it#speaker:??? #portrait:portrait_npc_mysterious
You still can feel the overwhelming shadow energy#logtype:mono
...Why#logtype:di #speaker:??? #portrait:portrait_shadow
You suddenly hear a deep and scary voice in your head.#logtype:mono
He spoke...?!#logtype:di #speaker:Yuuki #portrait:portrait_player
...Don't you already know#speaker:??? #portrait:portrait_shadow
How scary... that "place" is...
...#speaker:Yuuki #portrait:portrait_player
You've... realized it right...#speaker:??? #portrait:portrait_shadow
Why you are here...
Yeah...#speaker:Yuuki #portrait:portrait_player
Then why...!!!!!!#speaker:??? #portrait:portrait_shadow
You will only suffer if you went back...
You are here all because of those mean people...
Isn't it better to be in this world... all alone...
No one will hurt you...
But I have to go back#speaker:Yuuki #portrait:portrait_player
Why...#speaker:??? #portrait:portrait_shadow
I did something bad and everyone hates me#speaker:Yuuki #portrait:portrait_player
Well it was actually my fault and I admit it
I tried to change but no one gave me another chance
And I have developed social anxiety, refusing to communicating with anyone
Slowly closing myself in...
In the end, I am here being locked up by my own negative emotions
Or more precisely, I am here in my own mental world
...If you stay here ... you would not need to suffer anymore...#speaker:??? #portrait:portrait_shadow
I refuse#speaker:Yuuki #portrait:portrait_player
I admit I felt despair and lost the will to move on
Trying to forget everything arriving into this world
But even so there was a part of me, reminding me that I should not give up
That part of me tried to protect me from being overwhelmed by you
Providing me strength to carry on and learning how to adapt to the "shadow"
I have learnt a lot during my adventure in this world and going into the past
I realized that those places are actually from my past memories
The places that I fear which is full of "shadow" energy
But "he" guided me and taught myself how to overcome it
Things may not be as scary as I thought
Instead of fearing what may come, why not take the time to prepare
Built up confidence and remove the negative thoughts.
Just do it with my own pace
Slowly learn to adapt by making small goals and slowly working towards the big goal.
Giving myself compliments to keep the motivation going and eventually I could overcome it.
If it is too hard then we should not push ourself
Learn to calm down first with relaxation techniques
Pushing ourself may in the end cause more damage than making progress.
Thats why we must learn to take a rest in between and let ourself be mentally prepared
So that we can deal with our problem at our best conditions.
...You are beyond my understandings#speaker:??? #portrait:portrait_shadow
Why... why... why...
Everyone is a bully...
Isn't it better to stay in this world...
Sorry but I already decided to take my first step#speaker:Yuuki #portrait:portrait_player
Someone is waiting for me
I'll give you all my powers, now overcome it!!#speaker:??? #portrait:portrait_npc_mysterious #addstat:finalpower
I won't lose this time!! #speaker:Yuuki #portrait:portrait_player 
~mainquest_progress="16"
->progress3

===progress3===
(You sense that the shadow has grown stronger than before)#logtype:mono
Are you ready to face him?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
You engaged battle with the shadow.#logtype:mono #battle:start #enemy:finalstage2
->END
+[No]
Maybe I need some time to prepare#speaker:Yuuki #portrait:portrait_player
->END

