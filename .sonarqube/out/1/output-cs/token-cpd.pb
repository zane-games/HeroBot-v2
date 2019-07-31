Å
hC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\Herobot.Plugins.Example\Modules\ExampleModule.cs
	namespace		 	
Herobot		
 
.		 
Plugins		 
.		 
Example		 !
.		! "
Modules		" )
{

 
[ 
Cooldown 
( 
$num 
) 
] 
[ 

NeedPlugin 
( 
) 
] 
[ 
Name 	
(	 

$str
 
) 
] 
public 

class 
ExampleModule 
:  

ModuleBase! +
<+ , 
SocketCommandContext, @
>@ A
{ 
[ 	
Command	 
( 
$str 
) 
]  
public 
Task 
Testcommand 
(  
)  !
=>" $

ReplyAsync% /
(/ 0
$str0 <
)< =
;= >
[ 	
Command	 
( 
$str 
) 
] 
[ 	
Cooldown	 
( 
$num 
) 
] 
public 
async 
Task 
SendMp  
(  !
SocketGuildUser! 0
socketGuildUser1 @
,@ A
[A B
	RemainderB K
]K L
stringL R
messageS Z
)Z [
{\ ]
await 
socketGuildUser !
.! "%
GetOrCreateDMChannelAsync" ;
(; <
)< =
.= >
ContinueWith> J
(J K
(K L
channelOpenL W
)W X
=>Y [
{ 
channelOpen 
. 
Result "
." #
SendMessageAsync# 3
(3 4
message4 ;
); <
;< =
} 
) 
; 
} 	
} 
} ¸
aC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\Herobot.Plugins.Example\PluginRefferal.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
HeroBot !
{ 
public 

class 
PluginRefferal 
:  !
IPluginRefferal" 1
{		 
}

 
} 