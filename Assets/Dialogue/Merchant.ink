#player involved true
#merchant involved true
#mysterious involved false
#refugee involved false
#mystery involved false
#player anim enterNeutral
#merchant anim enterAngry
INCOMING TRANSMISSION
#merchant anim inHappy
Welcome to Dharlu's emporium. What may I offer you today?
#player anim inNeutral
#merchant anim outHappy
Hello?
#merchant anim inAngry
#player anim outNeutral
Oh. It's one of your kind. 
Before you ask, I don't give away goods for free. 
I have a business to run you know.
So I'm afraid you're either buying or going.
#player anim inNeutral
#merchant anim outAngry
You look like you are doing alright to me. 
I've not seen that much food in months.
#merchant anim inAngry
#player anim outNeutral
And you won't be seeing that much food again if you keep loitering!
Are you buying or not?

->Choice

==Choice==
#player anim inNeutral
#merchant anim outAngry
...
    *[Yes] -> Yes
    *[No] -> No
    *[What do you even sell?] -> What
    
== What ==
#player anim neutral
What do you even sell?
#player anim outNeutral
#merchant anim inAngry
Whatever I can get my hands on to sell. Food, water, fuel cells and the like. 
Anything that the locals are likely to want.
#player anim inNeutral
#merchant anim outAngry
Right, The "Locals". You sell to <i>them</i>?.
#player anim outNeutral
#merchant anim inAngry
I sell to anyone. I Wouldn't get by otherwise.
They would kill me, or worse shut down my business in a heartbeat.
But why am I even talking. Last chance. Buy or leave.
#player anim inNeutral
#merchant anim outAngry
...


    *[Buy] -> Yes
    *[Leave] -> No


== Yes == 
#player anim neutral
#merchant anim outAngry
Alright then, show me your goods I guess.
#player anim outNeutral
#player event openShop
...
-> DONE
 
 == No ==
 #player anim neutral
 #merchant anim outAngry
I'll leave then. I won't be funding the likes of you.
 #player anim outNeutral
 #merchant anim inAngry
Thats fine by me! I don't think you could fund me anyway.
Come back when your willing to buy. 
#merchant anim outAngry
END OF TRANSMISSION
 -> DONE