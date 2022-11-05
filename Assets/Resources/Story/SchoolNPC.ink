INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink
//Selective mutism and slight social anxiety disorder
//Playground, a lonely boy under a tree looking at the other kids having fun

{
-quest2_progress=="0":
->main
-quest2_progress=="1":
->progress1
-quest2_progress=="2":
->progress2
-quest2_progress=="3":
->progress3
-quest2_progress=="4":
->progress41
-quest2_progress=="41":
->progress4
-quest2_progress=="42":
->progress42
-quest2_progress=="5":
->progress5
-quest2_progress=="6":
->normal2
-quest2_progress=="61":
->progress61
-quest2_progress=="9":
->progress6
-quest2_progress=="10":
->progress7
-quest2_progress=="11":
->progress8
}
->normal



===normal===
You see they are still practicing.#logtype:mono
->END

===normal2===
I should wait for him to return.#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
->END

===main===
(You feel a faint shadowy aura from the person, do you want to try talking to him?)#logtype:mono
+[Yes]
->progress1
+[No]
Maybe I need some time to prepare
->END

===progress1===
~quest2_progress="1"
(Maybe we shouldn't be talking directly to him)#logtype:di #speaker:Yuuki #portrait:portrait_player #questtrigger:start #quest_id:2
Why?#speaker:??? #portrait:portrait_npc_mysterious
(Won't people feel strange if he is talking to himself?)#speaker:Yuuki #portrait:portrait_player
(Like people can't really see me, and there are people around here)
(Can you do the linking thing?)
Yeah sure.#speaker:??? #portrait:portrait_npc_mysterious
Are you ready?
+[Yes]
~quest2_progress="2"
You feel a strange power linking you and the person.#logtype:mono #portal:SQ_2_backstory
->END
+[No]
Maybe I need some time to prepare
->END
===progress2===
//cutscene eplaining how he have problem with speaking with unknown people
//seems like he have selective mutism
//quiz
Well seems like he probably have selective mutism.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Do you know about selective mutism?
+[Yes]
->selectivem1
+[No]
->selectivem1

===selectivem1===
Which is the correct definition for selective mutism?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[A type of anxiety disorder that affects a one's ability to speak in some situations but not others.]
Correct. It usually starts during childhood and, if left untreated, can persist into adulthood.
Also this is a rather complex case which require to be treated with care.
->selectivem2
+[A long-term and overwhelming fear of social situations.]
This is a little too general and is more to definition of social anxiety.
But it have some similarities to selective mutism.
->selectivem2

===selectivem2===
Maybe we could prepare some reward.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Using the reward to go for exposure-based behavioural therapy.
Of course this would be ideal if it's done with a professional, but thats the current option we have now.
(What’s that?)#speaker:Yuuki #portrait:portrait_player
We can first decide on an exposure hierarchy.#speaker:??? #portrait:portrait_npc_mysterious
It's like creating a list of tasks that is related to his problem
Which in this case is interaction with others.
Working from the easy tasks and gradually increasing more exposure.
Then provide rewards for each task that was completed to keep the motivation going. 
Slowly increasing his confidence and help him getting used to interacting with others.
(Hmm sounds good, maybe we can try that)#speaker:Yuuki #portrait:portrait_player
Ok now that we have some idea about his problem, so let's try speaking with him.#speaker:??? #portrait:portrait_npc_mysterious
->progress21


===progress21===
Hey there.#logtype:di #speaker:Yuuki #portrait:portrait_player
...?! I-… uh… …#speaker:Tim #portrait:portrait_schoolnpc
The boy seemed to start panicking and is nervous about your presence as he tries to speak#logtype:mono
You could already feel the shadow energy is growing rapidly
Seems like we have no choice but to help him calm down.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
I’ll use my powers to calm him down first, but it would be temporary as it doesn’t really solve the root cause.
Now face your hands towards him.
As you lift your hands and aim towards the him, your hand starts to emit a weak but warm light.#logtype:mono #playse:heal
You can feel the shadow energy has subsided.
You feeling better?#logtype:di #speaker:Yuuki #portrait:portrait_player
...yes ...?! I- I can speak...#speaker:Tim #portrait:portrait_schoolnpc
Great!#speaker:Yuuki #portrait:portrait_player
Wh- who are you…? My parents told me not to talk to strangers.#speaker:Tim #portrait:portrait_schoolnpc
But you are already talking to me.#speaker:Yuuki #portrait:portrait_player
Ah…!#speaker:Tim #portrait:portrait_schoolnpc
He then closes his mouth with both hands#logtype:mono
Ahahaha, don't worry I won't do any harm to you.#logtype:di #speaker:Yuuki #portrait:portrait_player
Actually, I am a magician that helps to cheer people up and you seem to be a little down.
Currently we are communicating directly in your mind, so that you won't be talking to yourself in front of others.
Oh... now that you've mentioned about it... Bu- but how did you calm me down?#speaker:Tim #portrait:portrait_schoolnpc
Well I did say that I am a magician so thats my magic.#speaker:Yuuki #portrait:portrait_player
Wow so that light was actually magic?! Ca- can you show me again?#speaker:Tim #portrait:portrait_schoolnpc
Looks like he is more curious with the magic rather than why there is a person directly talking in his mind.#logtype:mono
(Hey, use that magic again. I need to prove myself)#logtype:di #speaker:Yuuki #portrait:portrait_player
My powers aren’t for performance… but fine…#speaker:??? #portrait:portrait_npc_mysterious
Your hands start to emit light again#logtype:mono #playse:heal
Wow… that’s so cool. It’s like magic from the fantasy movies!#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
Yeah, it is actually a special small magic that helps people to calm down.#speaker:Yuuki #portrait:portrait_player
Ca- can you teach me that magic…?#speaker:Tim #portrait:portrait_schoolnpc
…I usually would get really nervous in front of people
But somehow your magic was able to help me calm down.
This is probably my first time being able to properly talk to other people…
So I really want to learn it so I can speak with other people normally…
Yeah sure but the magic only has temporary effect
So maybe I can teach you a different magic which is better.#speaker:Yuuki #portrait:portrait_player
Really? Th- thank you…!#speaker:Tim #portrait:portrait_schoolnpc
Of course, I already told you that I am a magician that cheers people up right?#speaker:Yuuki #portrait:portrait_player
Before that I need to prepare some stuff, I’ll be back.
Okay let's go, I'll break the link and send you back.#speaker:??? #portrait:portrait_npc_mysterious
Ok…? He’s already gone…#speaker:Tim #portrait:portrait_schoolnpc #questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
~quest2_progress="3"
->END
===progress3===
(Maybe we can start from deciding on a list of tasks.)#logtype:di #speaker:Yuuki #portrait:portrait_player
(Hmm maybe he can start from interacting with me without the help of the magic)
(As for the order of the task…)
->quiz1

===quiz1===
What's first...#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to answer close-ended question]
Maybe it's still too early for this.
->quiz1
*[Try to answer open-ended question]
Maybe it's still too early for this.
->quiz1
*[Make eye contact with me]
Making eye contact should be easier than the other tasks.
->quiz2
*[Try to whisper “Hi” to me]
Maybe it's still too early for this.
->quiz1
===quiz2===
What's next...#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to answer open-ended question]
Maybe it's still too early for this.
->quiz2
*[Try to answer close-ended question]
Maybe it's still too early for this.
->quiz2
*[Try to whisper “Hi” to me]
Trying to speak a simple word should be easier than answering questions.
->quiz3
===quiz3===
Then...#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to answer close-ended question]
Answering a close-ended question with predefined answer should be easier.
Since open-ended question require thinking and there is no definite answer.
->progress31
*[Try to answer open-ended question]
Maybe it's still too early for this.
->quiz3

===progress31===
(Basically, try to make eye contact, try to whisper “hi” to me)#logtype:di #speaker:Yuuki #portrait:portrait_player
(Then try to answer close-ended question and finally try to answer open-ended question.)
Seems fine to me.#speaker:??? #portrait:portrait_npc_mysterious
(Before that, we need to know what he likes for the reward)#speaker:Yuuki #portrait:portrait_player
It seems like he likes chocolate#speaker:??? #portrait:portrait_npc_mysterious
(So we need to find chocolate… but how do we get it)#speaker:Yuuki #portrait:portrait_player
Well, you can go to the school convenience store to get it.#speaker:??? #portrait:portrait_npc_mysterious
The one near the portal.
(But I don’t have money on me…)#speaker:Yuuki #portrait:portrait_player
I can give you some money.#speaker:??? #portrait:portrait_npc_mysterious
(Why do you even have money, you don’t really need it right?)#speaker:Yuuki #portrait:portrait_player
Well I don’t but that doesn’t mean that I can’t have it. Don’t worry the money is really mine, I guarantee it.#speaker:??? #portrait:portrait_npc_mysterious
(...Ok then)#speaker:Yuuki #portrait:portrait_player 
You obtained some money#logtype:mono #getitem:item4 #questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
~quest2_progress="4"
->END
//Quest
===progress41===
I should wait for him to return...#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
->END
===progress4===
//After gathering the required items
Hi, I am back#logtype:di #speaker:Yuuki #portrait:portrait_player
Welcome back... I can see you, but seems like no one noticed you#speaker:Tim #portrait:portrait_schoolnpc
I thought that making you visible to him would be easier to simulate real situations.#speaker:??? #portrait:portrait_npc_mysterious
Don't worry other people can't see you for now.
Yeah It's my magic, and like this would be better for the communication simulation.#speaker:Yuuki #portrait:portrait_player
Oh ok...so the preparations are ready…?#speaker:Tim #portrait:portrait_schoolnpc
+[Yeah]
(Let me make sure...)#speaker:Yuuki #portrait:portrait_player #questtrigger:proceedprogress #quest_id:2
    {proceed_progress:
        You took out the bag of chocolate#speaker:Yuuki #portrait:portrait_player
        Is that chocolate...?#speaker:Tim #portrait:portrait_schoolnpc
        You then explain about the plan#logtype:mono
        That doesn’t really sound like magic to me…#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
        He doesn’t seem as excited as before when he was told that he will be taught about magic.#logtype:mono
        You will be rewarded with chocolate for each task completed #logtype:di #speaker:Yuuki #portrait:portrait_player
        ...! ...How did you know I like chocolate#speaker:Tim #portrait:portrait_schoolnpc
        Well it’s magic. So would you like to try?#speaker:Yuuki #portrait:portrait_player
        ...I’ll believe you for once.#speaker:Tim #portrait:portrait_schoolnpc
        You can see that he is drooling knowing that he is attracted by the reward as you showed him the chocolate.#logtype:mono
        Great! Let’s start. So I will release the magic casted on you, but don’t worry I will help you to calm down if needed.#logtype:di #speaker:Yuuki #portrait:portrait_player
        Also you can take your time and keep trying, even if you fail you won’t lose your reward.
        O- okay… I am ready.#speaker:Tim #portrait:portrait_schoolnpc
        You put your hands in front of him and mysterious person removes the magic casted on him.#logtype:mono
        ~quest2_progress="42"
        ~proceed_progress=false
        //transition
        ->END
        -else:
        (I haven't finish searching yet)#logtype:di #speaker:Yuuki #portrait:portrait_player
    }
->END
+[Not yet]
->END

===progress42===
After a while, even though it took some time, he was able to clear all the tasks.#logtype:mono
Good job! You did great!#logtype:di #speaker:Yuuki #portrait:portrait_player #removeitem:item5
Th- thank… you…, I feel… a little more… confident.#speaker:Tim #portrait:portrait_schoolnpc #getitem:item6
Maybe you can now try to communicate with others.#speaker:Yuuki #portrait:portrait_player
O- ok… I think… I can try…#speaker:Tim #portrait:portrait_schoolnpc
I’ll go prepare, wait me for a while.#speaker:Yuuki #portrait:portrait_player
The myeterious person used his powers to make you invisible#logtype:mono
O- ok… he’s gone again…#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
~quest2_progress="5"
->END

===progress5===
(If that’s the case then maybe we need new tasks…)!#logtype:di #speaker:Yuuki #portrait:portrait_player
->quiz4//Arrange the thing from easy to hard

===quiz4===
First...!#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to whisper to a new peer with me]
Maybe it's still too early for this.
->quiz4
*[Try to whisper to a familiar peer with me]
Maybe he can start from trying to talk to a familiar peer with someone he can talk to.
Which in this case would be myself.
->quiz5
*[Try to whisper to a new peer alone]
Maybe it's still too early for this.
->quiz4
*[Try to whisper to a familiar peer alone]
Maybe it's still too early for this.
->quiz4
===quiz5===
Then...!#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to whisper to a familiar peer alone]
Then he can try to do it alone.
->quiz6
*[Try to whisper to a new peer with me]
Maybe it's still too early for this.
->quiz5
*[Try to whisper to a new peer alone]
Maybe it's still too early for this.
->quiz5
===quiz6===
Next...!#logtype:di #speaker:Yuuki #portrait:portrait_player
*[Try to whisper to a new peer alone]
Maybe it's still too early for this.
->quiz6
*[Try to whisper to a new peer with me]
Finally, he will try to talk to a new peer with me and then trying it alone.
->progress51

===progress51===
(Basically it would be talk to someone familiar with me, then trying it alone.)!#logtype:di #speaker:Yuuki #portrait:portrait_player
(After he is confident enough, he can try to talk to someone new together and then trying it alone)
(Currently there’s still enough reward, but we need to find some help from other people)
I can feel that the girl at the front of the classroom is rather familiar with him, maybe we can ask her to help.#speaker:??? #portrait:portrait_npc_mysterious
Well we can try to ask.#speaker:Yuuki #portrait:portrait_player
Let's go then#speaker:??? #portrait:portrait_npc_mysterious
~quest2_progress="6"
->END


===progress6===
I am back again#logtype:di #speaker:Yuuki #portrait:portrait_player
W- welcome back...#speaker:Tim #portrait:portrait_schoolnpc
So for the next tasks...#speaker:Yuuki #portrait:portrait_player
You explained about your next plan.#logtype:mono
...but w- where do we find people... to do this...?#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
Don't worry I brought some help for that#speaker:Yuuki #portrait:portrait_player
I think she will be here soon
Just when you mentioned about her, she arrived#logtype:mono
~quest2_progress="61"
->END
===progress61===//cutscene move
…! Umm… h-… …#logtype:di #speaker:Tim #portrait:portrait_schoolnpc
He starts to tense up but despite that he tries to talk.#logtype:mono
I already heard everything from that person.#logtype:di #speaker:Sally #portrait:portrait_schoolnpcgirl
O- ok...#speaker:Tim #portrait:portrait_schoolnpc
If you’re ready then let’s start#speaker:Yuuki #portrait:portrait_player
I-… I am… ready…!#speaker:Tim #portrait:portrait_schoolnpc
And we proceeded with the tasks.#logtype:mono
~quest2_progress="10"
->END
===progress7===
Ok I think that should be fine, good work everybody.#logtype:di #speaker:Yuuki #portrait:portrait_player #removeitem:item6
Even though it took longer than before but he managed to finish all of it.#logtype:mono
Good job, you cleared all the tasks! You have made your first step!#logtype:di #speaker:Yuuki #portrait:portrait_player
Th- thank… you…#speaker:Tim #portrait:portrait_schoolnpc
I- I think I feel… more confident… even though I… still can’t speak well… but I know I am improving…
Good, I believe that if you keep practicing, you definitely can speak normally with others soon.#speaker:Yuuki #portrait:portrait_player
But you don’t have to force yourself, just try it with your own pace.
~quest2_progress="11"
->END
===progress8===
~quest2_progress="12"
~shadow2_escaped=true
Hey the shadow enemy has escaped, after it.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
I think I have to leave already, I suddenly remembered that I have some urgent matter.#speaker:Yuuki #portrait:portrait_player #questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
Good luck on practicing the “magic” that I have taught you.
Wa- h- he's gone...#speaker:Tim #portrait:portrait_schoolnpc #questtrigger:proceedprogress #questtrigger_type:force #quest_id:103
The mysterious person has made you invisible again, allowing you to focus on the chase.#logtype:mono #questtrigger:complete #quest_id:2
~mainquest_progress="10"
->END