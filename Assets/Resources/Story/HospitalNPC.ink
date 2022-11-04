INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

//aquaphobia
{
-quest3_progress=="0":
->main
-quest3_progress=="1":
->progress1
-quest3_progress=="2":
->progress2
-quest3_progress=="11":
->progress11
-quest3_progress=="3":
->progress3
-quest3_progress=="4":
->progress6
}
->normal

===normal===
Learn how to relax ownself is important for stress and anxiety management.#logtype:di #speaker:Alex #portrait:portrait_hospitalnpc
->END

===main===
(You feel a faint shadowy aura from the person, do you want to approach him?)#logtype:mono
+[Yes]
->progress1
+[No]
Maybe I need some time to prepare
->END

===progress1===
~quest3_progress="1"
Ok so link me to him#logtype:di #speaker:Yuuki #portrait:portrait_player #questtrigger:start #quest_id:3
...You sure are already getting used to the process#speaker:??? #portrait:portrait_npc_mysterious
Ok then I'll-#questtrigger:proceedprogress #quest_id:104 #questtrigger_type:force
The next moment, before the mysterious person was able to finish his sentence#logtype:mono
You see he turn on the monitor using a remote.
Good afternoon and welcome to JCC news...
The news mentioned about a famous swimming athlete has encountered an accident at a nearby swimming pool.
The details about the incident is yet to be clarified but the athlete himself seems to be safe.
The athlete's picture was shown on TV.
As you look at the picture you felt a sense of DeJa vu as you feel like you seen that face somewhere.
(?! Isn't that him?)#logtype:di #speaker:Yuuki #portrait:portrait_player
You then realize that the person is the one being mentioned in the news.#logtype:mono
You see him staring at the news not knowing what he was thinking, but you sense the shadowy aura grew.
What should we do?#logtype:di #speaker:Yuuki #portrait:portrait_player
Doesn't seem like I am able to calm him down with my powers, it's too strong.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Not sure what is causing such strong shadow energy.
As you were trying to think of what to do, outside started to rain.#logtype:mono
You can see water splashing on the window.
Suddenly you feel a super strong shadow energy coming from the person.
He seems to start panicking and trying to avoid something.
I think it's the rain causing his panic, quick go and close the curtains!#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
~quest3_progress="2"
->END

===progress2===
He seems to be trembling, trying to hide from something.#logtype:mono
+[Talk to him]
(Let me make sure...)#logtype:di #speaker:Yuuki #portrait:portrait_player #questtrigger:proceedprogress #quest_id:3
    {proceed_progress:
        ~proceed_progress=false
        I closed the curtains, are you okay?
        Umm... yeah thanks.#speaker:Alex #portrait:portrait_hospitalnpc
        You seem to be scared of something.#speaker:Yuuki #portrait:portrait_player
        ...#speaker:Alex #portrait:portrait_hospitalnpc
        He doesn't seem like feeling to talk about it.#logtype:mono
        Looks like the only thing we can do is to find the cause from his mind.#logtype:di #speaker:Yuuki #portrait:portrait_player
        ~quest3_progress="11"
        ->progress11
        -else:
        (I need to close the curtains first)
    }
    ->END
+[Cancel]
Maybe I need some time to prepare
->END

===progress11===
Are you ready?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
~quest3_progress="3"
You feel a strange power linking you and the person.#logtype:mono #portal:SQ_3_backstory
->END
+[No]
->END

===progress3===
From the looks of it, that incident probably caused development of a phobia.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious #questtrigger_type:force #questtrigger:proceedprogress #quest_id:3
A phobia?#speaker:Yuuki #portrait:portrait_player
Yeah, do you know what is phobia?
+[Yes]
->quiz1
+[No]
->quiz1

===quiz1===
Which of the following is the definition of phobia?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Repeated, persistent and unwanted thoughts, urges or images that are intrusive and cause distress or anxiety.]
This is actually the definition for obsessive compulsive disorder.
A phobia is a persistent, excessive, unrealistic fear of an object, person, animal, activity or situation
->quiz2
+[A persistent, excessive, unrealistic fear of an object, person, animal, activity or situation]
Correct!
->quiz2

===quiz2===
What could cause development of phobia#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Negative experiences]
+[Genetics and environment]
+[Brain function]
+[All of above]
Correct!
-Many phobias develop as a result of having a negative experience or panic attack
Which is related to a specific object or situation.
There may be a link between your own specific phobia and the phobia or anxiety of your parents
This could be due to genetics or learned behavior.
Changes in brain functioning also may play a role in developing specific phobias.
->quiz3

===quiz3===
Which of the following is not an example of phobia?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Agoraphobia]
+[Aquaphobia]
+[Apaphobia]
+[Acrophobia]
-The answer is "Apaphobia"
Agoraphobia is an intense fear of places that are difficult to escape
Sometimes involving a fear of crowded or open spaces.
Aquaphobia is an irrational fear of water.
Acrophobia is an intense fear of heights.
->quiz4

===quiz4===
In this case, what could have caused his phobia#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
*[Genetics and environment]
(Doesn't seem like thats the case...)#speaker:Yuuki #portrait:portrait_player
->quiz4
*[Brain function]
(Doesn't seem like thats the case...)#speaker:Yuuki #portrait:portrait_player
->quiz4
*[Negative experiences]
From what we have saw in the television, that accident may be the cause of his development of phobia.
->progress4


===progress4===
What do you think we could do to help him?#logtype:di #speaker:Yuuki #portrait:portrait_player
We might not be able to cure it but we can do other things to help him feel better.#speaker:??? #portrait:portrait_npc_mysterious
Like what?#speaker:Yuuki #portrait:portrait_player
Such as relaxation method and help him understand his problem.#speaker:??? #portrait:portrait_npc_mysterious
Well let's talk to him.
Hey there#speaker:Yuuki #portrait:portrait_player
?! Where am I? Aren't you the random person who helped me to close the curtains?#speaker:Alex #portrait:portrait_hospitalnpc
Don't worry I am here to help you.#speaker:Yuuki #portrait:portrait_player
...Help me with what?#speaker:Alex #portrait:portrait_hospitalnpc
I believe you have some unknown fear everytime you see water.#speaker:Yuuki #portrait:portrait_player
...! H- how did you know that?#speaker:Alex #portrait:portrait_hospitalnpc
It's one of my powers.#speaker:Yuuki #portrait:portrait_player
Just like I've said before, I am here to help so trust me.
...Doesn't seem like I am able to get away from you.#speaker:Alex #portrait:portrait_hospitalnpc
Ok then... so how are you going to help?
First I'll help you understand your problem.#speaker:Yuuki #portrait:portrait_player
You explained about phobia and the possible cause of his problem.#logtype:mono
...So that event became my trauma and cause development of a specific phobia called aquaphobia?#logtype:di #speaker:Alex #portrait:portrait_hospitalnpc
Yeah, thats why you feel fear and start to panic when you see the rain, specifically the water.#speaker:Yuuki #portrait:portrait_player
That does explain my reaction...#speaker:Alex #portrait:portrait_hospitalnpc
Is- is it curable?
Yeah phobia is curable, but I won't be the one doing that.#speaker:Yuuki #portrait:portrait_player
W- why I thought you are here to help...!#speaker:Alex #portrait:portrait_hospitalnpc
It's better to find professionals who are expert at your issue.
Since you're at the hospital maybe you can seek professional advice.
But still I will help you to learn how to calm down, so that you won't be too stressed.#speaker:Yuuki #portrait:portrait_player
Like how?#speaker:Alex #portrait:portrait_hospitalnpc
Well I already helped you to understand your situation.#speaker:Yuuki #portrait:portrait_player
Lacking in awareness is one of the biggest challenge to cure mental health disorders.
Knowing your problem and understanding that it is curable helps you to feel more rest assure right?
... Yeah.#speaker:Alex #portrait:portrait_hospitalnpc
Other thing I can help is by teaching you relaxation methods.#speaker:Yuuki #portrait:portrait_player
So that you learn how to calm down youself when you start to have symptoms of phobia.
Relaxation methods?
Yeah something like quick muscle relaxation and breath focus relaxation methods.
->relax1
===relax1===
Ok so follow my instructions.#speaker:Yuuki #portrait:portrait_player
*[Make a fist, squeezing your hand tightly.]
I think it's the other one
->relax1
*[Hold this for a few seconds, noticing the tension.]
I think it's the other one
->relax1
*[Close your eyes and concentrate on your breathing. Slowly breath in through your nose and out through your mouth.]
->relax2
*[Slowly open your fingers and feel the difference – notice the tension leaving.]
I think it's the other one
->relax1
===relax2===
Close your eyes and concentrate on your breathing. Slowly breath in through your nose and out through your mouth.#speaker:Yuuki #portrait:portrait_player
*[Hold this for a few seconds, noticing the tension.]
I think it's the other one
->relax2
*[Slowly open your fingers and feel the difference – notice the tension leaving.]
I think it's the other one
->relax2
*[Make a fist, squeezing your hand tightly.]
->relax3

===relax3===
Make a fist, squeezing your hand tightly.#speaker:Yuuki #portrait:portrait_player
*[Hold this for a few seconds, noticing the tension.]
->relax4
*[Slowly open your fingers and feel the difference – notice the tension leaving.]
I think it's the other one
->relax3
===relax4===
Hold this for a few seconds, noticing the tension.#speaker:Yuuki #portrait:portrait_player
Slowly open your fingers and feel the difference – notice the tension leaving.
He followed the instructions for a few times.#logtype:mono
He seems to have remembered the technique.
Ok moving on to another technique, which would be breath focus#logtype:di #speaker:Yuuki #portrait:portrait_player
->relax5
===relax5===
Ok so follow my instructions.#speaker:Yuuki #portrait:portrait_player
*[Let your breath flow as deep down into your belly as is comfortable, without forcing it.]
->relax6
*[Try breathing in through your nose and out through your mouth.]
I think it's the other one
->relax5
*[Breathe in gently and regularly. Some people find it helpful to count steadily from 1 to 5. You may not be able to reach 5 at first.]
I think it's the other one
->relax5
*[Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.]
I think it's the other one
->relax5
*[Keep doing this for at least 5 minutes.]
I think it's the other one
->relax5
===relax6===
Let your breath flow as deep down into your belly as is comfortable, without forcing it.#speaker:Yuuki #portrait:portrait_player
*[Try breathing in through your nose and out through your mouth.]
->relax7
*[Breathe in gently and regularly. Some people find it helpful to count steadily from 1 to 5. You may not be able to reach 5 at first.]
I think it's the other one
->relax6
*[Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.]
I think it's the other one
->relax6
*[Keep doing this for at least 5 minutes.]
I think it's the other one
->relax6
===relax7===
Try breathing in through your nose and out through your mouth.#speaker:Yuuki #portrait:portrait_player
*[Breathe in gently and regularly. Some people find it helpful to count steadily from 1 to 5. You may not be able to reach 5 at first.]
I think it's the other one
->relax8
*[Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.]
I think it's the other one
->relax7
*[Keep doing this for at least 5 minutes.]
I think it's the other one
->relax7
===relax8===
Breathe in gently and regularly. Some people find it helpful to count steadily from 1 to 5. You may not be able to reach 5 at first.#speaker:Yuuki #portrait:portrait_player
*[Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.]
->relax9
*[Keep doing this for at least 5 minutes.]
I think it's the other one
->relax8

===relax9===
Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.#speaker:Yuuki #portrait:portrait_player
Keep doing this for at least 5 minutes.
He followed the instructions for a few times.#logtype:mono
He seems to have remembered the technique.
Ok good job, these technique could help you to calm down.#logtype:di #speaker:Yuuki #portrait:portrait_player
I do feel a little better than before, thanks.#speaker:Alex #portrait:portrait_hospitalnpc
I think the shadow is escaping, I'll send you back to the original place.#speaker:??? #portrait:portrait_npc_mysterious
~quest3_progress="4"
->END

===progress6===
~quest3_progress="5"
~mainquest_progress="14"
~shadow3_escaped=true
You felt the shadow aura escaping to another place.#logtype:mono #questtrigger:proceedprogress #quest_id:104 #questtrigger_type:force
...I slightly feel relieve understanding my problem and you also taught me how to deal with my fears.#logtype:di #speaker:Alex #portrait:portrait_hospitalnpc
Thank you.#questtrigger_type:force #questtrigger:proceedprogress #quest_id:3
But you still need to cure your phobia if you plan to return to swimming.#speaker:Yuuki #portrait:portrait_player #questtrigger:complete #quest_id:3
Yeah, I think I'll visit a professional as what you've suggested.#speaker:Alex #portrait:portrait_hospitalnpc
Ok good luck!#speaker:Yuuki #portrait:portrait_player
Thanks again!#speaker:Alex #portrait:portrait_hospitalnpc
->END