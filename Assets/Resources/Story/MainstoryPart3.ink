INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-mainquest_progress=="8":
->main
}

===main===
When you exit the portal, you see a enclosed space with wooden flooring.#questtrigger:updateprogressvalue #quest_id:103
It seems like you are at a hallway in a building.
Is this a school...?
Somehow you have vague impression that this is a school building.
But you can't remember anything else.
Ok you know the drill, let's move on to find the shadow.#questtrigger:proceedprogress #quest_id:103 #questtrigger_type:force
~mainquest_progress="9"
->END
===progress1===



//solve the quest
//strogn energy
//find
//battle
//memory cutscene

//return to void and power up
//open 3rd gate

//solve
//this time strong energy come to find you
//battle(lose)
//even the guide is having a hard time
//memories of past, knowing everything
//rememebrs everything learnt before
//believe in self
//learnt new skill
//battle
//accepting your past
//purify
//don't avoid the negative, embrace and understand it, it helps to make mental stronger
//restore crystal
//the void become bright world
//portal
//farewell and thank you
//return to real world
//find your self regaining conscious at a hospital with worrying parents
//make up your mind and try to talk 
//story end

->END