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
Both of them are practicing to communicate
->END



===main===
(You feel a faint shadowy aura from the person, do you want to try talking to him?)
+[Yes]
->progress1
+[No]
->END

===progress1===
~quest2_progress="1"
(Maybe we shouldn't be talking directly to him)#questtrigger:start #quest_id:2
Why?
(Won't people feel strange if he is talking to himself?)
(Like people can't really see me, and it doesn't seem like a good idea to be visible here)
(It's not like I am a student here, we might scare him)
Hmm never thought of that, maybe I'll link you with him like last time.
Great, let's do this.
Are you ready?
+[Yes]
~quest2_progress="2"
You feel a strange power linking you and the person.#portal:SQ_2_backstory
->END
+[No]
->END
===progress2===
//cutscene eplaining how he have problem with speaking with unknown people
//seems like he have selective mutism
//quiz
Well seems like he probably have selective mutism.
Do you know about selective mutism?
+[Yes]
->selectivem1
+[No]
->selectivem1

===selectivem1===
Which is the correct definition for selective mutism
+[a]
->selectivem2
+[b]
->selectivem2

===selectivem2===
Which do you think can help him?
...I am not really sure, since it's like a complex case.
Well maybe we could prepare some reward and go for exposure-based behavioural therapy.
Of course this would be ideal if it's done with a professional, but thats the current option we have now.
(What’s that?)
We can first decide on an exposure hierarchy.
It's like creating a list of tasks that is related to interacting with others starting small and gradually increasing more exposure.
Then provide rewards for each task that was completed to keep the motivation going. 
Slowly increasing his confidence and help him getting used to interacting with others.
(Hmm sounds good, maybe we can try that)
Ok now that we have some idea about his problem, so let's try speaking with him.
->progress21


===progress21===
Hey there.
...?! I-… uh… …
The boy seemed to start panicking and is nervous about your presence as he tries to speak
(You could already feel the shadow energy is growing rapidly)
Seems like we have no choice but to help him calm down.
I’ll use my powers to calm him down first, but it would be temporary as it doesn’t really solve the root cause.
Now face your hands towards him.
As you lift your hands and aim towards the child, your hand starts to emit a weak but warm light.
You can feel the shadow energy has subsided.
You feeling better?
...yes ...?! I- I can speak better...
Great!
Wh- who are you…? My parents told me not to talk to strangers.
But you are already talking to me.
Ah…!
He then closes his mouth with both hands
Ahahaha, don't worry I won't do any harm to you. 
Actually, I am a magician that helps to cheer people up and you seem to be a little down.
Currently we are communicating directly in your mind, so that you won't be talking to yourself in front of others.
Oh... now that you've mentioned about it... Bu- but how did you calm me down? 
Well I did say that I am a magician so thats my magic.
Wow so that light was actually magic?! Ca- can you show me again?
Looks like he is more interested with the magic rather than why there is a person directly talking in his mind.
(Hey, use that magic again. I need to prove myself)
Mysterious person: My powers aren’t for performance… but fine…
(You emit light again)
Wow… that’s so cool. It’s like magic from the fantasy movies!
Yeah, it is actually a special small magic that helps people to calm down.
Ca- can you teach me that magic…? 
Why?
…Actually I usually would get really nervous in front of people, but somehow your magic was able to help me calm down.
This is probably my first time being able to properly talk to other people…
So I really want to learn it so I can speak with other people normally…
Yeah sure but the magic only has temporary effect, so maybe I can teach you a different magic which is better.
Really? Th- thank you…!
Of course, I already told you that I am a magician that cheers people up right?  Before that I need to prepare some stuff, I’ll be back.
Ok…? He’s already gone…#questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
~quest2_progress="3"
->END
===progress3===
You: (Maybe we can start from deciding on the list of tasks.)
You: (Hmm maybe he can start from interacting with me without the help of the magic, as for the order of the task…)//Arrange the thing from easy to hard
->quiz1

===quiz1===
What's first...
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
What's next...
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
Then...
*[Try to answer close-ended question]
Answering a close-ended question with predefined answer should be easier.
Since open-ended question require thinking and there is no definite answer.
->progress31
*[Try to answer open-ended question]
Maybe it's still too early for this.
->quiz3

===progress31===
(Basically, try to make eye contact, try to whisper “hi” to me, try to answer close-ended question and finally try to answer open-ended question.)
Seems fine to me.
(Before that, we need to know what he likes for the reward.)
It seems like he likes chocolate
(So we need to find chocolate… but how do we get it)
Well, you can go to the school convenience store to get it.
The one near the portal.
(But I don’t have money on me…)
I can give you some money.
(Why do you even have money, you don’t really need it right?)
Well I don’t but that doesn’t mean that I can’t have it. Don’t worry the money is really mine, I guarantee it.
(...Ok then)#getitem:item4 #questtrigger:proceedprogress #questtrigger_type:force #quest_id:2
~quest2_progress="4"
->END
//Quest
===progress41===
I should wait for him to return...
->END
===progress4===
//After gathering the required items
Hi, I am back
Welcome back... I can see you
I thought that making you visible to him would be easier to simulate real situations.
Don't worry other people can't see you for now.
Yeah I thought this would be better for the communication simulation.
Oh ok...so the preparations are ready…?
+[Yeah]
(Let me make sure...)#questtrigger:proceedprogress #quest_id:2
    {proceed_progress:
        You took out the bag of chocolate
        Is that chocolate...?
        You then explain about the plan
        That doesn’t really sound like magic to me… 
        He doesn’t seem as excited as before when he was told that he will be taught about magic.
        You will be rewarded with chocolate for each task completed
        ...! …How did you know I like chocolate
        Well it’s magic. So would you like to try?
        …I’ll believe you for once.
        You can see that he is drooling knowing that he is attracted by the reward as you showed him the chocolate.
        Great! Let’s start. So I will release the magic casted on you, but don’t worry I will help you to calm down if needed.
        Also you can take your time and keep trying, even if you fail you won’t lose your reward.
        O- okay… I am ready.
        You put your hands in front of him and mysterious person removes the magic casted on him.
        You can see him starting to feel slightly nervous in front of you.
        ~quest2_progress="42"
        ~proceed_progress=false
        //transition
        ->END
        -else:
        (I haven't finish searching yet)
    }
->END
+[Not yet]
->END

===progress42===
After a while, even though it took some time, he was able to clear all the tasks.
Good job! You did great!
Th- thank… you…, I feel… a little more… confident.
Maybe you can now try to communicate with others.
O- ok… I think… I can try…
I’ll go prepare, wait me for a while.
O- ok… he’s gone again…
~quest2_progress="5"
->END


===progress5===
(If that’s the case then maybe we need new tasks…)
->quiz4//Arrange the thing from easy to hard

===quiz4===
First...
*[Try to whisper to a new peer with me]
Maybe it's still too early for this.
->quiz4
*[Try to whisper to a familiar peer with me]
Maybe he can start from tryign to talk to a familiar peer with someone he can talk to.
Which in this case would be myself.
->quiz5
*[Try to whisper to a new peer alone]
Maybe it's still too early for this.
->quiz4
*[Try to whisper to a familiar peer alone]
Maybe it's still too early for this.
->quiz4
===quiz5===
Then...
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
Next...
*[Try to whisper to a new peer alone]
Maybe it's still too early for this.
->quiz6
*[Try to whisper to a new peer with me]
Finally, he will try to talk to a new peer with me and then trying it alone.
->progress51

===progress51===
(Basically it would be talk to someone familiar with me, then trying it alone.)
(After he is confident enough, he can try to talk to someone new together and then trying it alone)
(Currently there’s still enough reward, but we need to find some help from other people)
I can feel that the girl at the front of the classroom is rather familiar with him, maybe we can ask her to help.
Well we can try to ask.
Let's go then
~quest2_progress="6"
->END


===progress6===
I am back again
W- welcome back...
So for the next tasks...
You explained about your next plan.
...but w- where do we find people... to do this...?
Don't worry I brought some help for that
I think she will be here soon
Just when you mentioned about her, she arrived
~quest2_progress="61"
->END
===progress61===//cutscene move
…! Umm… h-… …
He starts to tense up but despite that he tries to talk.
I already heard everything from that person.
O- ok...
If you’re ready then let’s start
I-… I am… ready…!
And we proceeded with the tasks.
~quest2_progress="10"
->END
===progress7===
Ok I think that should be fine, good work everybody.
Even though he took more tries than before but he somehow managed to finish all of it.
Good job, you cleared all the tasks! You have made your first step!
Th- thank… you…
How do you feel now?
I- I feel… more confident… even though I… still can’t speak well… but I know I am improving…
Good, I believe that if you keep practicing, you definitely can speak normally with others soon.
But you don’t have to force yourself, just try it with your own pace.
~quest2_progress="11"
->END
===progress8===
~quest2_progress="12"
Hey the shadow enemy has escaped, after it.
I think I have to leave already, I suddenly remembered that I have some urgent matter.
Good luck on practicing the “magic” that I have taught you.
Wa- h- he's gone...
The mysterious person has made you invisible again, allowing you to focus on the chase.
~shadow2_escaped=true
->END