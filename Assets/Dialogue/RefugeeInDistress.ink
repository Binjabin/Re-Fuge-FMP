#player involved true
#merchant involved false
#mysterious involved false
#refugee involved true

#player anim enterNeutral
#refugee anim enterScared
INCOMING TRANSMISSION

#player anim inNeutral
Hello? 
#player anim neutral
Is anyone there?
#player anim scared
...
#player anim outScared
#refugee anim inScared
Please help... 
They saw us trying to cross...
We pleaded...
They left us for dead!
#player anim inScared
#refugee anim outScared
Us? Where are the others?
#player anim outScared
#refugee anim inScared
It's been days, I'm the only one left. 
My family are waiting for me in Othea.
Please...
#player anim inNeutral
#refugee anim outScared
...
    *[Give Resources] -> Give
    *[Leave] -> Leave
    *[Take Them With You] -> With

==Give==
#player anim neutral
I can give you some resources. What do you need? Food, water, power cells?
#player anim outNeutral
#refugee anim inNeutral
Thank you! Anything you can spare.
 -> END

==Leave==
#player anim neutral
I'm sorry. I have my own journey. I can't help you.
#player anim outNeutral
#refugee anim inScared
No... My family... Please!
#refugee anim outScared
END OF TRANSMISSION
-> END


==With==
#player anim neutral
You are heading to Othea? I have family there too!
You should come with me!
Quick get on board, before your ship falls apart.
#player anim outNeutral
#refugee anim inNeutral
Thank you so much! Mabye I will see my family again.
-> END