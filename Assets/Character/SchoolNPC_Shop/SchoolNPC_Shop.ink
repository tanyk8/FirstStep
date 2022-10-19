INCLUDE ../../Script/Dialogue/globals.ink
INCLUDE ../../Script/Dialogue/globalfunction.ink

{
-shopchocolate_bought==false&&quest2_progress=="4":
->quest
-shopchocolate_bought==true:
->main
}



===main===
If you're feeling stressful remember to take a rest.
->END

===quest===
I'll create an illusion that makes you look like a student of this school.
Hi how may I help you?
I would like to buy a bag of chocolate
That would be 200 penny#removeitem:item4 
You handed him 200 penny and you received a bag of chocolate#getitem:item5
Thank you, have a nice day.
Ok time to return to him
~quest2_progress="41"
~shopchocolate_bought=true
->END
