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
The girl is busy talking to her classmate.
->END

===main===
How should we apporach her?
She seems like a cautious person, she probably will cause a commotion if we directly talked to her directly.
Not to mention she is currently talking with another person
Maybe you should approach her in her mind.
Okay, if you say so.
Are you ready?
+[Yes]
~quest2_progress="7"
You feel a strange power linking you and the person.#portal:SQ_2_mindworld
->END
+[No]
->END

===main2===
Hey there
Who are you...?
Don't worry I won't hurt you, I am Yuuki.
This is like a magic of mine, currently we are communicating directly in your mind.
You are lying, magic does not exist.
She shot down my statement immediately.
Well that sure is a critical hit from her.
Thats the expected reaction that I should had gotten.
To be honest I am surprised that he actually accepted that explaination.
Looks like he is the only one that believes in magic.
...why are you talking to yourself.
She gradually become more suspicious of me.
Err don't mind me.
Ok back to the topic, let's get this straight.
Currently I am helping Tim and I need your help, you guys known each other for qutie a long time right?
How did you know...?
Well even if I explained about it, you probably wouldn't believe it.
Regardless, are you willing to help?
Tim usually wouldn't talk to any stranger, I don't really believe you he would accept your help or that you could actually help him.
(Can you do something?)
Here, read these to her.
You feel that words are flowing into your mind, and you attempt to read it.
Umm...
"That day when he saved me, it was like a super hero, when I grow up I want to-"
Wh- NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
Ok ok! I get it! Stop!
(What was that about)
While you guys were chatting, I was looking through her memories.
So basically she is his neighbour and they known each other since kinder garden.
One day when she was in danger, he helped her despite of his conditions.
And that made her fell for him.
Even though they rarely talk but she have interest in Tim and wanting to be friends with him.
She tried to approach Tim multiple times but he always ran away in the end.
(Ok stop I think that's enough! Talk about no privacy...)
Even I also feel bad for her.
So what do you need...
Well thing is like this...
You explained the situation to her.
~quest2_progress="8"
->END
//transition
===main3===
...Looks like you are really not harmful.
So can you ask your friend to help too?
Ok... I'll try
You sound tired
Thanks to you
...
...
Umm... Then I'll be taking my leave, bye.
He's gone... how did he know about that...
Regardless, this is my chance, maybe I should go get "that" too.
~girlhelped=true
~quest2_progress="9"
->END