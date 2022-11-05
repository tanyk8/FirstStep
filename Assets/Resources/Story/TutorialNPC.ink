INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink



{
-quest1_progress=="0":
->main
-quest1_progress=="1":
->progress1
-quest1_progress=="3":
->progress3
-quest1_progress=="4":
->progress41
-quest1_progress=="5":
->progress5
-quest1_progress=="6":
->progress6
-quest1_progress=="7":
->progress7
-quest1_progress=="8":
->progress8
}


===main===
(You sense a faint shadowy aura from the person)#logtype:mono
+[Quest]
Hey there you looked troubled, what seems to be the problem?#logtype:di #speaker:Yuuki #portrait:portrait_player #questtrigger:start #quest_id:1
Oh hey umm… actually I lost something and I am trying to find it.#speaker:John #portrait:portrait_parknpc
Do you need help? I am rather free as I am just enjoying a walk around the park.#speaker:??? #portrait:portrait_player
Really? That would be a great help! Actually I’ve lost my favourite guitar pick and I just can’t find it anywhere.#speaker:John #portrait:portrait_parknpc
When was the last time you saw it?#speaker:Yuuki #portrait:portrait_player
…I think it was when I kept it in my wallet after practicing here hours ago.#speaker:John #portrait:portrait_parknpc
That’s why I came back here trying to find it.
Then what about the places you’ve went after practicing?#speaker:Yuuki #portrait:portrait_player
I think I’ve went to the playground for a walk to get some inspiration for my new song.#speaker:John #portrait:portrait_parknpc
Maybe I can try to go there and search for it.#speaker:Yuuki #portrait:portrait_player
Ok thanks man!#speaker:John #portrait:portrait_parknpc
~quest1_progress="1"
->END
+[Cancel]
Maybe I need some time to prepare
->END

===progress1===
(Report to him?)#logtype:mono
+[Quest]
    Have you found it yet?#logtype:di #speaker:John #portrait:portrait_parknpc #questtrigger:proceedprogress #quest_id:1
    {proceed_progress:
        I tried to find it at the playground but no luck.#speaker:Yuuki #portrait:portrait_player
        ~proceed_progress=false
        ~quest1_progress="2"
        ->progress2
        -else:
        (I haven't finish searching yet)#speaker:Yuuki #portrait:portrait_player
    }
    ->END
+[Cancel]
(I haven't finish searching yet)#speaker:Yuuki #portrait:portrait_player
->END

===progress2===
Oh ok… damn where did I dropped it…#logtype:di #speaker:John #portrait:portrait_parknpc
(Hey is it possible to locate it with your powers?)#speaker:Yuuki #portrait:portrait_player
Well I can sense the linking bond between the item and the owner.#speaker:??? #portrait:portrait_npc_mysterious
But I would need him create a strong emotion trail, so that I could use that emotion energy to track it.
How could he do that?#speaker:Yuuki #portrait:portrait_player
Maybe you can try to ask more details about it so that he would think more about it.#speaker:??? #portrait:portrait_npc_mysterious
Is it really an important stuff?#speaker:Yuuki #portrait:portrait_player
Yeah it’s actually a present from my father, the reason that I like to play guitar is all thanks to him.#speaker:John #portrait:portrait_parknpc
My father is lead vocal and guitarist of the “Step Up Party” band.
His band became famous recently.
I admire him and he is my inspiration, I dreamed of being like him.
Actually there will be a community music event happening soon.
So I was thinking of creating a new song here.
But I accidently lost the guitar pick today, ... I need it to play my music.
Ok great seems like I found it, it is near the fountain located at the park entrance.#speaker:??? #portrait:portrait_npc_mysterious
(Ok Let's go get it)#speaker:Yuuki #portrait:portrait_player
I see, but don't worry I'll help you to find it since I still have time.
Thanks… I really appreciate it.#speaker:John #portrait:portrait_parknpc
~quest1_progress="3"
->END

===progress3===
(Report to him?)#logtype:mono
+[Quest]
    Oh you're back, so did you find it?#logtype:di #speaker:John #portrait:portrait_parknpc #questtrigger:proceedprogress #quest_id:1
    {proceed_progress:
        You took the guitar pick and showed to him.#logtype:mono
        Hey I’ve found it, it’s on the border wall of the fountain.#logtype:di #speaker:Yuuki #portrait:portrait_player
        ~proceed_progress=false
        ~quest1_progress="3"
        ->progress4
        -else:
        (I haven't finish searching yet)#speaker:Yuuki #portrait:portrait_player
    }
    ->END
+[Cancel]
(I haven't finish searching yet)#speaker:Yuuki #portrait:portrait_player
->END

===progress4===
Really?! Oh, I think I did take it out again before leaving the park and I had some urgent matters.#logtype:di #speaker:John #portrait:portrait_parknpc #removeitem:item3
Maybe that is when I forgot to put it back in my wallet and ran off.
Thank you very much!!
No problem.#speaker:Yuuki #portrait:portrait_player
(Hmm... the shadow energy is not decreasing at all)
Maybe that is not the root cause, we need to find out what is the actual thing that is troubling him.#speaker:??? #portrait:portrait_npc_mysterious
Some people aren't even aware of their own problems.
Identifying and understanding a problem is crucial for solving problems.
So maybe we can help him identify his worries using my powers.
I will link you to his mind and maybe you can find some clues.
~quest1_progress="4"
->progress41
===progress41===
I can link you with him any time, are you ready?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
~quest1_progress="5"
You feel a strange power linking you and the person.#logtype:mono #portal:SQ_1_backstory
->END
+[No]
->END


===progress5===
Well from the looks of it, seems like the upcoming event is causing some anxiety for him.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
And seems like the anxiety is starting to affect his daily life.
That is proably the root source of shadow energy which shadow beings like to feed on.
Is there any way to help him?#speaker:Yuuki #portrait:portrait_player
If we're able to change his thoughts on the matter to be more optimistic about it, maybe he would feel better.#speaker:??? #portrait:portrait_npc_mysterious
Easier said than done, it's not like we are professionals.#speaker:Yuuki #portrait:portrait_player
Even if we are not professionals there are still things that we can help to cheer them up.#speaker:??? #portrait:portrait_npc_mysterious
Before that, just to make sure you are clear on what we are dealing with, let me ask you a few question.
->quiz1
===quiz1===
Do you know what is anxiety?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
->quiz11
+[No]
Well let me explain
Anxiety is an emotion characterized by feelings of tension, worried thoughts, and physical changes like increased blood pressure.
->quiz2

===quiz11===
Well then, can you describe about anxiety?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[An emotion characterized by feelings of tension, worried thoughts, and physical changes like increased blood pressure.]
Good job!
The other option was actually the definition for fear.
+[A basic, intense emotion aroused by the detection of imminent threat, involving an immediate alarm reaction that mobilizes the organism by triggering a set of physiological changes.]
This is actually the definition for fear.
-Fear is actually different from anxiety.//Both taken from APA dictionary
->quiz12
===quiz12===
Fear is considered an appropriate short-term response to a present, clearly identifiable threat#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
For example like when you are in a dangerous situation
The risk of being hurt in that situation may cause fear in you.
Meanwhile anxiety is future-oriented, long-term response focused on a diffuse threat.
The thing that cause anxiety may not be a present threat, and it also have long term effects on you.
->quiz2

===quiz2===
Moving on to the next question#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Is anxiety same as anxiety disorder?//comparing normal anxiety and disorder
+[Yes]
Actually both are different.
+[No]
Correct, both are different.
-Anxiety is a rather normal thing that anyone could have any point in their life.
You may feel worried and anxious about sitting an exam, having medical test or job interview.
Anxiety only becomes a problem when it becomes unmanagable and start to affect your daily life.
From that point, it is considered an anxiety disorder.
->quiz3

===quiz3===
Do you think this person have anxiety disorder?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Yes]
+[No]
-We do know it is starting to affect his daily life as he have some trouble sleeping and eating.
But whether if it is anxiety disorder, it is best to let the professionals decide such as visiting a psychiatrist.
All we can do is try our best to help him to solve his problems for now.
->quiz4

===quiz4===
Do you know what is the possible cause of his anxiety?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
+[Failing to meet other's expectation]
+[Possibility of making mistakes with the performace]
+[No experience playing before other people]
+[All of above]
-Basically all may be the cause of his anxiety for the event.
Low self-esteem as he think that he could not meet expectations
and have low confidence in his abilities.//negativity and low confidence
Pessimistic thoughts on he might not be able to finish his performance
And making mistakes due to reasons such as nervourness.
Fear of unknown as it will be his first time showing his performance in front of other people
Not knowing how other people think of his performance.
->quiz5
===quiz5===
What do you think can help him to ease his anxiety?#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
*[Cheer him up]
Well it's a good choice, we can do that too but it doesn't really solve the problem.
->quiz5
*[Giving advice for his performance]
(I don't really know much about music... maybe there are other options)#speaker:Yuuki #portrait:portrait_player
->quiz5
*[Help him practice]
Yeah, I think that's the best option we have.
Since he never played before others, maybe that's why he is not confident enough.
So he probably just need someone else's approval on his abilities
Maybe that will help him to be more confident.
Well then since it's decided, let's try proposing to help him with his practice.#questtrigger_type:force #questtrigger:proceedprogress #quest_id:1
~quest1_progress="6"
->END



===progress6===
Hey, what don't you try to play a song for me?#logtype:di #speaker:Yuuki #portrait:portrait_player
Eh…? But…#speaker:John #portrait:portrait_parknpc
So that you can feel more confident on the playing in front of others.#speaker:Yuuki #portrait:portrait_player
I don't know... does it really help?#speaker:John #portrait:portrait_parknpc
Even though I don't play music but I do often listen to many songs.#speaker:Yuuki #portrait:portrait_player
Maybe I can give some advice from an audience standpoint.
Y- you won't laugh at me if I didn't play well or be better than my father...?#speaker:John #portrait:portrait_parknpc
Why would I, not to mention I don't even know how to play music at all.#speaker:Yuuki #portrait:portrait_player
You are already better than me when it comes to music.
Believe in yourself and the things that you've learnt.
Thank you… I think I’ll try.#speaker:John #portrait:portrait_parknpc
Let me get my guitar.
(He quickly left the park and went to get his guitar)#logtype:mono
~quest1_progress="7"
->END

===progress7===
After a while, he thanked everyone and other people left.#logtype:mono
He started to tidy up his stuff while you approach him.
I always thought that I need to be as good as my father to impress people.#logtype:di #speaker:John #portrait:portrait_parknpc
Surprisingly people actually liked my performance!
He was rather excited and happy that people liked his performance.#logtype:mono
Thank you... if it wasn't for you I would probably still be worried whether I can play well or not.#logtype:di #speaker:John #portrait:portrait_parknpc
Now I think I have more confidence to be on stage.
Yeah no problem! Good luck with your debut live.#speaker:Yuuki #portrait:portrait_player
I think I'll be leaving for now, I just remembered that I have something to do.
Oh ok, next time if we meet I'll repay you somehow.#speaker:John #portrait:portrait_parknpc
We bid farewell and he returns to tidying up his stuff.#logtype:mono #questtrigger_type:force #questtrigger:proceedprogress #quest_id:1
He's not looking, I think I'll switch you back to invisible.#logtype:di #speaker:??? #portrait:portrait_npc_mysterious
Ok so let's find that shadow, it should had went north west.#questtrigger_type:force #questtrigger:proceedprogress #quest_id:102
Let's go and purify it.#questtrigger:complete #quest_id:1
~quest1_progress="8"
~mainquest_progress="6"
->END

===progress8===
Time to practice more to get more confidence.#logtype:di #speaker:John #portrait:portrait_parknpc
->END
