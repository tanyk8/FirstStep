INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-quest2_progress=="6":
->main
-quest2_progress=="7":
->main2
-quest2_progress=="8":
->main3
}
->normal

===normal===
The girl is busy talking to her classmate.#logtype:mono
->END

===main===
How should we apporach her?#logtype:di #speaker:Yuuki #portrait:portrait_player
She seems like a cautious person, she probably will cause a commotion if we directly talked to her directly.
Not to mention she is currently talking with another person
Well the same way, let's just link to her mind.#speaker:??? #portrait:portrait_npc_mysterious
Are you ready?
+[Yes]
~quest2_progress="7"
You feel a strange power linking you and the person.#logtype:mono #portal:SQ_2_mindworld
->END
+[No]
Maybe I need some time to prepare
->END

===main2===
Hey there#logtype:di #speaker:Yuuki #portrait:portrait_player
Who are you...?#speaker:Sally #portrait:portrait_schoolnpcgirl
Don't worry I won't hurt you, I am Yuuki.#speaker:Yuuki #portrait:portrait_player
This is like a magic of mine, currently we are communicating directly in your mind.
You are lying, magic does not exist.#speaker:Sally #portrait:portrait_schoolnpcgirl
She shot down my statement immediately.#logtype:mono
Well that sure is a critical hit from her.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Thats the expected reaction that I should had gotten.#speaker:Yuuki #portrait:portrait_player
To be honest I am surprised that Tim actually accepted that explaination.
Looks like he is the only one that believes in magic.#speaker:??? #portrait:portrait_npc_mysterious
...why are you talking to yourself.#speaker:Sally #portrait:portrait_schoolnpcgirl
She gradually become more suspicious of me.#logtype:mono
Err don't mind me.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Ok back to the topic, let's get this straight.#speaker:Yuuki #portrait:portrait_player
Currently I am helping Tim and I need your help, you guys known each other for qutie a long time right?
How did you know...?#speaker:Sally #portrait:portrait_schoolnpcgirl
Well even if I explained about it, you probably wouldn't believe it.#speaker:Yuuki #portrait:portrait_player
Regardless, are you willing to help?
Tim usually wouldn't talk to any stranger#speaker:Sally #portrait:portrait_schoolnpcgirl
I don't really believe you he would accept your help or that you could actually help him.
(Can you do something?)#speaker:Yuuki #portrait:portrait_player
Here, read these to her.#speaker:??? #portrait:portrait_npc_mysterious
You feel that words are flowing into your mind, and you attempt to read it.#logtype:mono
Umm...#logtype:di #speaker:Yuuki #portrait:portrait_player
"That day when he saved me, he was like a super hero"
"...When I grow up I want to-"
Wh- NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO#speaker:Sally #portrait:portrait_schoolnpcgirl
Ok ok! I get it! Stop!
(What was that about)#speaker:Yuuki #portrait:portrait_player
While you guys were chatting, I was looking through her memories.#speaker:??? #portrait:portrait_npc_mysterious
So basically she is his neighbour and they known each other since kinder garden.
One day when she was in danger, he helped her despite of his conditions.
And that made her fell for him.
Even though they rarely talk but she have interest in Tim and wanting to be friends with him.
She tried to approach Tim multiple times but he always ran away in the end.
(Ok stop I think that's enough! Talk about no privacy...)#speaker:Yuuki #portrait:portrait_player
Even I also feel bad for her.
So what do you need...#speaker:Sally #portrait:portrait_schoolnpcgirl
Well thing is like this...#speaker:Yuuki #portrait:portrait_player
You explained the situation to her.#logtype:mono
~quest2_progress="8"
->END
//transition
===main3===
...Looks like you are really not harmful.#logtype:di #speaker:Sally #portrait:portrait_schoolnpcgirl
So can you ask your friend to help too?#speaker:Yuuki #portrait:portrait_player
Ok... I'll try#speaker:Sally #portrait:portrait_schoolnpcgirl
You sound tired#speaker:Yuuki #portrait:portrait_player
Thanks to you#speaker:Sally #portrait:portrait_schoolnpcgirl
...#speaker:Yuuki #portrait:portrait_player
...#speaker:Sally #portrait:portrait_schoolnpcgirl
Umm... Then I'll be taking my leave, bye.#speaker:Yuuki #portrait:portrait_player
How did he know about that... Geez#speaker:Sally #portrait:portrait_schoolnpcgirl #questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
~girlhelped=true
~quest2_progress="9"
->END