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
Learn how to relax ownself is important for stress and anxiety management.
->END

===main===
(You feel a faint shadowy aura from the person, do you want to approach him?)
+[Yes]
->progress1
+[No]
->END

===progress1===
~quest3_progress="1"
So what should we do to approach him without being too suspicious?#questtrigger:start #quest_id:3
The old way by linking to his mind?
That's the only way for now unless something happens.
The moment when he said that, you see he turn on the monitor using a remote.
Good afternoon and welcome to JCC news...
The news mentioned about a famous swimming athlete has encountered an accident at a nearby swimming pool.
The details about the incident is yet to be clarified but the athlete himself seems to be safe.
The athlete's picture was shown on TV.
As you look at the picture you felt a sense of DeJa vu as you feel like you seen that face somewhere.
(?! Isn't that him?)
You then realize that the person is the one being mentioned in the news.
You see him staring at the news not knowing what he was thinking, but you sense the shadowy aura grew.
What should we do?
Doesn't seem like I am able to calm him down with my powers, it's too strong.
Not sure what is causing such strong shadow energy.
As you were trying to think of what to do, outside started to rain.
You can see water splashing on the window.
Suddenly you feel a super strong shadow energy coming from the person.
He seems to start panicking and trying to avoid something.
I think it's the rain causing his panic, quick go and close the curtains!
~quest3_progress="2"
->END

===progress2===
He seems to be trembling, trying to hide from something.
+[Talk to him]
(Let me make sure...)#questtrigger:proceedprogress #quest_id:3
    {proceed_progress:
        ~proceed_progress=false
        I closed the curtains, are you okay?
        Umm... yeah thanks.
        You seem to be scared of something.
        ...
        He doesn't seem like feeling to talk about it.
        Looks like the only thing we can do is to find the cause from his mind.
        ~quest3_progress="11"
        ->progress11
        -else:
        (I need to close the curtains first)
    }
    ->END
+[Cancel]->END

===progress11===
Are you ready?
+[Yes]
~quest3_progress="3"
You feel a strange power linking you and the person.#portal:SQ_3_backstory
->END
+[No]
->END

===progress3===
From the looks of it, that incident probably caused development of a phobia.
A phobia?
Yeah, do you know what is phobia?
+[Yes]
->quiz1
+[No]
->quiz1

===quiz1===
Which of the following is the definition of phobia?
->quiz2

===quiz2===
What could cause development of phobia
->quiz3

===quiz3===
Which of the following is not an example of phobia?
->quiz4

===quiz4===
In this case, what could have caused his phobia
->progress4

===progress4===
What do you think we could do to help him?
Well I don't think it's a good idea to bring him out ofthe hospital, since there are professionals here.
We should probably let them do their job.
Besides the shadow energy is too strong for me to help him calm down.
So that means there is nothing we can help?
We might not be able to cure it but we can do other things to help him feel better.
Like what?
Such as relaxation method and help him understand his problem.
Well let's talk to him.
Hey there
?! Where am I? Aren't you the random person who helped me to close the curtains? 
Don't worry I am here to help you.
...Help me with what? You don't even understand my situation.
I believe you have some unknown fear everytime you see water.
...! H- how did you know that?
It's one of my powers.
Just like I've said before, I am here to help so trust me.
...Doesn't seem like I am able to get away from you.
Ok then... so how are you going to help?
First I'll help you understand your problem.
You explained about phobia and the possible cause of his problem.
...So that event became my trauma and cause development of a specific phobia called aquaphobia?
Yeah, thats why you feel fear and start to panic when you see the rain, specifically the water.
That does explain my reaction...
Is- is it curable?
Yeah phobia is curable, but I won't be the one doing that.
W- why I thought you are here to help...!
It's better to find professionals who are expert at your issue. Since you're at the hospital maybe you can seek professional advice.
But still I will help you to calm down, so that you won't be too stressed.
Like how?
Well I already helped you to understand your situation.
Lacking in awareness is one of the biggest challenge to cure mental health disorders.
Knowing your problem and understanding that it is curable helps you to feel more rest assure right?
... Yeah.
Other thing I can help is by teaching you relaxation methods.
So that you learn how to calm down youself when you start to have symptoms of phobia.
Relaxation methods?
Yeah something like quick muscle relaxation and breath focus relaxation methods.
Ok so follow my instructions.
*[Close your eyes and concentrate on your breathing. Slowly breath in through your nose and out through your mouth.]
*[Make a fist, squeezing your hand tightly.]
*[Hold this for a few seconds, noticing the tension.]
+[Slowly open your fingers and feel the difference – notice the tension leaving.]
-What about breath focus?
*[Let your breath flow as deep down into your belly as is comfortable, without forcing it.]
*[Try breathing in through your nose and out through your mouth.]
*[Breathe in gently and regularly. Some people find it helpful to count steadily from 1 to 5. You may not be able to reach 5 at first.]
*[Then let it flow out gently, counting from 1 to 5 again, if you find this helpful.]
*[Keep doing this for at least 5 minutes.]
-Great
~quest3_progress="4"
->END

===progress6===
~quest3_progress="5"
~mainquest_progress="14"
~shadow3_escaped=true
You felt the shadow aura escaping to another place.
...I slightly feel relieve understanding my problem and you also taught me how to deal with my fears.
Thank you.
But you still need to cure your phobia if you plan to return to swimming.
Yeah, I think I'll visit a psychiatrist as what you've suggested.
Follow it!
Ok good luck! I have to leave now.
Good luck to you too, seems like you're on some kind of mission.
You were surpised as he was spot on, you slightly feel encouraged.
Thanks!
->END