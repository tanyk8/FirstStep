INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-mainquest_progress=="12":
->main
-mainquest_progress=="14":
->progress1
-mainquest_progress=="15":
->progress2
}

===main===
When you exit the portal, you see a enclosed white hallway.#questtrigger:updateprogressvalue #quest_id:104
There are medicine smell floating around the place.
This time it is the hospital...?
Somehow you have a mixed feeling about the place.
Your legs is slightly trembling, but you are not sure of the reason.
Ok let's find the source.
This is the last place with shadow energy, we must becareful of him.
He might come to stop you.
But don't worry I have regain large portion of my powers, I will support you.#questtrigger:proceedprogress #quest_id:104 #questtrigger_type:force
~mainquest_progress="13"
->END

===progress1===
W- what is with this strong presence?!
It's him...
... ...
Looks like he's here to stop us from purifying the last source of shadow energy.
Well we already expected his appearance, no matter what I have to defeat him to escape from this world.
Don't worry I am here for you.
Thanks, let's do this!
~mainquest_progress="15"
->END


===progress2===
actually I am you,he is also you
You actually refused to wake up after the mental breakdown
now you're in hospital
with your mother
Those gates are your memories of the past
There are many people who are same as you who have their own problems
everyone is doing their best to live
Why... why... do you want to return...
If you return... you have to face your past...
+[I must return]
I have learnt alot, I have to be positive
++[I will do it in my own pace]
He releases a strong pressure on you
Are you sure...!
Why...!
+++[Use the relaxation method calm self down]
I have to take my first step to move foward!
Why... why... why...
everyone is a bully...
isn't it better to stay in this world...
I already decided to take my first step
I won't lose this time!! #battle:start #enemy:finalstage2
~mainquest_progress="16"
->END

//End battle
//Wake up at the hospital
//

