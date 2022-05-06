
#player anim enterNeutral
#merchant anim enterAngry
INCOMING TRANSMISSION
#merchant anim inAngry
Hello? 
Oh its you...
#player anim inNeutral
#merchant anim outAngry
Hello Dharlu.
#merchant anim inAngry
#player anim outNeutral
Yes, yes. Are you buying or not?

->Choice

==Choice==
#player anim inNeutral
#merchant anim outAngry
...
    *[Yes] -> Yes
    *[No] -> No
    *[What are you selling?] -> What
    
== What ==
#player anim neutral
What are you selling?
#player anim outNeutral
#merchant anim inAngry
Whatever I can get my hands on to sell.
You know as well as any how hard it is around here.
Anyway, either buy or leave.
#player anim inNeutral
#merchant anim outAngry
...
    *[Buy] -> Yes
    *[Leave] -> No


== Yes == 
#player anim neutral
Alright then, show me your goods.
#player anim outNeutral
#player event openShop
...
-> DONE
 
 == No ==
 #player anim neutral
 No thank you!
 #player anim outNeutral
 #merchant anim inAngry
Fine by me! 
Come back when your willing to buy.

-END OF TRANSMISSION-
 -> DONE