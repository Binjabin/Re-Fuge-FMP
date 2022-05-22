#player involved true
#merchant involved false
#mysterious involved false
#refugee involved true

#player anim enterNeutral
#refugee anim enterScared
INCOMING TRANSMISSION

#refugee anim inScared
Please. Any help you can give.
I'm desperate.
#player anim inScared
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