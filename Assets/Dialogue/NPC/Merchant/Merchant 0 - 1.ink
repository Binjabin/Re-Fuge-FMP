#player involved true
#merchant involved true
#mysterious involved false
#refugee involved false
#mystery involved false
#player anim enterNeutral
#merchant anim enterAngry
INCOMING TRANSMISSION
#merchant anim inHappy
Hello!
Are you buying or not?
->Choice

==Choice==
#player anim inNeutral
#merchant anim outAngry
...
    *[Yes] -> Yes
    *[No] -> No
    *[Remind me who you are?] -> What
    
== What ==
#player anim neutral
Who are you again.
#player anim outNeutral
#merchant anim inAngry
I am a merchant nothing more. You may call me Dharlu. 
I exchange goods for goods at fair prices.
#player anim inNeutral
#merchant anim outAngry
Fair prices? I'll be the judge of that.
#player anim outNeutral
#merchant anim inAngry
The prices are fair for me. I need to make a profit, otherwise i wouldn't be a businessman.
Talking of business, are we doing any?
#player anim inNeutral
#merchant anim outAngry
...


    *[Buy] -> Yes
    *[Leave] -> No


== Yes == 
#player anim neutral
#merchant anim outAngry
I am looking to purchase, yes.
#player anim outNeutral
#player event openShop
...
-> DONE
 
 == No ==
 #player anim neutral
 #merchant anim outAngry
Not today.
 #player anim outNeutral
 #merchant anim inAngry
Well then don't waste my time!
#merchant anim outAngry
END OF TRANSMISSION
 -> DONE