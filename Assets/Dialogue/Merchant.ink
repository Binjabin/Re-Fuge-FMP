
#player anim enterNeutral
#merchant anim enterHappy
INCOMING TRANSMISSION
#merchant anim inHappy
You have come to look at my goods have you?
#player anim inNeutral
#merchant anim outHappy
...
    *[Yes!] -> Yes
    *[No!] -> No
    *[Who are you?] -> Who
    
== Who ==
#player anim scared
What? Who are you?
#player anim outScared
#merchant anim inAngry
How Rude!? 
-> DONE

== Yes == 
#player anim neutral
I have heard great things about this market!
#player anim outNeutral
#merchant anim inHappy
Thats what I like to hear!
-> DONE
 
 == No ==
 #player anim neutral
 Not today! Another time mabye!
 #player anim outNeutral
 #merchant anim inSad
 Oh...
 Well come back any time...
 -> DONE