#player involved true
#mysterious involved false
#mysterious involved false
#refugee involved false
#mystery involved false
#player anim enterNeutral
#mysterious anim enterNeutral
INCOMING TRANSMISSION
#mysterious anim inNeutral
You return? Interesting.
I maintain my offer, if you have changed your mind.
Will you take the identification or not?
->Choice

==Choice==
#player anim inNeutral
#mysterious anim outNeutral
...
    *[Take] -> Yes
    *[Refuse] -> No
    *[What do you want in return?] -> What
    
== What ==
#player anim neutral
And what is it that you want in return?
#player anim outNeutral
#mysterious anim inAngry
For the time being? Nothing.
In the future who knows. You may be of some use to me.
But do not worry about that for the time being.
We do not have much longer, so be fast.
#player anim inNeutral
#mysterious anim outAngry
->Choice


== Yes == 
#player anim neutral
I'll take it. If things have changed how you say then I cannot thank you enough.
I will remember this kind stranger.
#mysterious anim inAngry
#player anim outNeutral
I will remember this also.
Until next time.
#mysterious anim outNeutral
#player event gainID
END OF TRANSMISSION
-> DONE
 
 == No ==
 #player anim neutral
 #mysterious anim outAngry
I will not trust one who comes in a ship of that kind.
I bid youy fairwell.
 #player anim outNeutral
 #mysterious anim inAngry
I see.
Until next time.
#mysterious anim outAngry
END OF TRANSMISSION
 -> DONE