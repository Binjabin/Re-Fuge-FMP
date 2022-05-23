#player involved true
#mysterious involved true
#merchant involved false
#refugee involved false
#player anim enterNeutral
#mysterious anim enterNeutral
INCOMING TRANSMISSION
#player anim inScared
Who's there? Do you mean me any harm?
#mysterious anim inNeutral
#player anim outScared
That depends on the circumstances.
But I see that you are a refugee. Interesting.
#player anim inScared
#mysterious anim outNeutral
Who are you? Why do you come in an enforcment ship but do not attack?
#mysterious anim inNeutral
#player anim outScared
Do not worry who I am, asylum seeker.
In your position I would be more interested in what I can give you.
#mysterious anim outNeutral
#player anim inScared
What you can give me? You mean to help?
#mysterious anim inNeutral
#player anim outScared
Perhaps. 
Have you thought about what happens once you complete your journey? Arrive at Othea?
#mysterious anim outNeutral
#player anim inNeutral
What do you mean? I will start a new life. Get a job perhaps.
#mysterious anim inAngry
#player anim outNeutral
Before that.
You believe that they will Welcome you with open arms?
You are naive.
I can, however, offer you this.
#mysterious anim outAngry
#player anim inScared
Fake identification? How did you get that?
#mysterious anim inNeutral
#player anim outNeutral
Do not worry about that. Worry about how without this you have no chance of getting past the border.
Othea no longer takes kindly to illegal immigrants.
So. Take it or not. Your choice.
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