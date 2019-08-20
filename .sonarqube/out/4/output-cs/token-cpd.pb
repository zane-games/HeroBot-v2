‘
tC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Migrations\BaseMigration01082019.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
GiveAway "
." #

Migrations# -
{ 
[ 
	Migration 
( 
$num 
) 
] 
public		 

class		 !
BaseMigration01082019		 &
:		' (
	Migration		) 2
{

 
public 
override 
void 
Down !
(! "
)" #
{ 	
Delete 
. 
Table 
( 
$str $
)$ %
;% &
} 	
public 
override 
void 
Up 
(  
)  !
{ 	
Create 
. 
Table 
( 
$str $
)$ %
. 

WithColumn 
( 
$str  
)  !
.! "
AsInt16" )
() *
)* +
.+ ,
Identity, 4
(4 5
)5 6
.6 7

PrimaryKey7 A
(A B
)B C
. 

WithColumn 
( 
$str #
)# $
.$ %
AsInt64% ,
(, -
)- .
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str !
)! "
." #

AsDateTime# -
(- .
). /
. 

WithColumn 
( 
$str #
)# $
.$ %
AsString% -
(- .
). /
;/ 0
} 	
} 
} •
jC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Modules\GiveAwayModule.cs
	namespace

 	
HeroBot


 
.

 
Plugins

 
.

 
GiveAway

 "
.

" #
Modules

# *
{ 
[ 

NeedPlugin 
] 
public 

class 
GiveAwayModule 
:  !

ModuleBase" ,
<, - 
SocketCommandContext- A
>A B
{ 
private 
GiveAwayService 
_service  (
;( )
public 
GiveAwayModule 
( 
GiveAway &
.& '
Services' /
./ 0
GiveAwayService0 ?
_giveAwayService@ P
)P Q
{R S
_service 
= 
_giveAwayService '
;' (
} 	
[ 	
Command	 
( 
$str 
) 
] 
public 
async 
Task 
GiveAway "
(" #
IChannel# +
channel, 3
,3 4
TimeSpan5 =
time> B
,B C
intC F
winnersG N
,N O
[P Q
	RemainderQ Z
]Z [
String[ a
priceb g
)g h
{i j
} 	
} 
} ˝
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\PluginRefferal.cs
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
} ˜D
lC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Services\GiveAwayService.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
GiveAway "
." #
Services# +
{ 
[ 
Service 
] 
public 

class 
GiveAwayService  
{ 
private 
IDatabaseService  
	_database! *
;* +
private 
IRedisService 
_redis $
;$ %
private 
ISubscriber 
_sub  
;  !
private  
DiscordShardedClient $
_discord% -
;- .
private 
Timer 
_timer 
; 
private 
int 
Incremental 
=  !
$num" #
;# $
private 
readonly 
static 
string  &
GetGiveawayById' 6
=7 8
$str9 g
;g h
private 
readonly 
static 
string  &
Getallgiveaways' 6
=7 8
$str9 m
;m n
private 
readonly 
static 
string  &
Creategiveaway' 5
=6 7
$str	8 –
;
– —
public 
GiveAwayService 
( 
IDatabaseService /
databaseService0 ?
,? @
IRedisServiceA N
redisServiceO [
,[ \
IConfigurationRoot] o
op q
,q r!
DiscordShardedClient	s á"
discordShardedClient
à ú
)
ú ù
{ 	
	_database 
= 
databaseService '
;' (
_redis   
=   
redisService   !
;  ! "
_sub!! 
=!! 
_redis!! 
.!! 
GetSubscriber!! '
(!!' (
)!!( )
;!!) *
_discord"" 
=""  
discordShardedClient"" +
;""+ ,
_timer## 
=## 
new## 
Timer## 
(## 
$num## %
)##% &
;##& '
_timer$$ 
.$$ 
Elapsed$$ 
+=$$ 
Update$$ $
;$$$ %
_sub%% 
.%% 
	Subscribe%% 
(%% 
$"%% 
__keyevent@%% (
{%%( )
o%%) *
.%%* +

GetSection%%+ 5
(%%5 6
$str%%6 =
)%%= >
[%%> ?
$str%%? I
]%%I J
}%%J K

__:expired%%K U
"%%U V
,%%V W
OnRedisEvent%%X d
)%%d e
;%%e f
_discord&& 
.&& 
ReactionAdded&& "
+=&&# %
OnReactionAdd&&& 3
;&&3 4
_discord'' 
.'' 
ReactionRemoved'' $
+=''% '
OnReactionRemoved''( 9
;''9 :
}(( 	
private** 
void** 
Update** 
(** 
object** "
sender**# )
,**) *
ElapsedEventArgs**+ ;
e**< =
)**= >
{++ 	
Incremental,, 
++,, 
;,, 
if-- 
(-- 
Incremental-- 
%-- 
$num-- 
==--  "
$num--# $
)--$ %
{--& '
var// '
giveawaysMoinsDecinqMinutes// /
=//0 1
	_database//2 ;
.//; <
GetDbConnection//< K
(//K L
)//L M
.//M N
Query//N S
(//S T
Getallgiveaways//T c
,//c d
new//e h
{00 
time11 
=11 
DateTime11 #
.11# $
Now11$ '
.11' (

AddMinutes11( 2
(112 3
$num113 4
)114 5
}22 
)22 
;22 
foreach33 
(33 
var33 
r33 
in33 !'
giveawaysMoinsDecinqMinutes33" =
)33= >
{33? @
try55 
{66 
var77 
channel77 #
=77$ %
_discord77& .
.77. /

GetChannel77/ 9
(779 :
)77: ;
;77; <
}88 
catch99 
(99 
	Exception99 $
error99% *
)99* +
{99, -
}99. /
}:: 
};; 
}<< 	
private>> 
Task>> 
OnReactionAdd>> "
(>>" #
	Cacheable>># ,
<>>, -
IUserMessage>>- 9
,>>9 :
ulong>>; @
>>>@ A
arg1>>B F
,>>F G!
ISocketMessageChannel>>H ]
arg2>>^ b
,>>b c
SocketReaction>>d r
arg3>>s w
)>>w x
{?? 	
throw@@ 
new@@ #
NotImplementedException@@ -
(@@- .
)@@. /
;@@/ 0
}AA 	
privateCC 
TaskCC 
OnReactionRemovedCC &
(CC& '
	CacheableCC' 0
<CC0 1
IUserMessageCC1 =
,CC= >
ulongCC? D
>CCD E
arg1CCF J
,CCJ K!
ISocketMessageChannelCCL a
arg2CCb f
,CCf g
SocketReactionCCh v
arg3CCw {
)CC{ |
{DD 	
throwEE 
newEE #
NotImplementedExceptionEE -
(EE- .
)EE. /
;EE/ 0
}GG 	
privateII 
voidII 
OnRedisEventII !
(II! "
RedisChannelII" .
arg1II/ 3
,II3 4

RedisValueII5 ?
arg2II@ D
)IID E
{JJ 	
varKK 
nameKK 
=KK 
arg2KK 
.KK 
ToStringKK $
(KK$ %
)KK% &
.KK& '
SplitKK' ,
(KK, -
$charKK- 0
)KK0 1
;KK1 2
ifLL 
(LL 
nameLL 
.LL 
LengthLL 
==LL 
$numLL  
&&LL! #
nameLL$ (
[LL( )
$numLL) *
]LL* +
==LL, .
$strLL/ 9
)LL9 :
{MM 
varNN 
idNN 
=NN 
nameNN 
[NN 
$numNN 
]NN  
;NN  !
varPP 

connectionPP 
=PP  
	_databasePP! *
.PP* +
GetDbConnectionPP+ :
(PP: ;
)PP; <
;PP< =

connectionQQ 
.QQ 

QueryAsyncQQ %
(QQ% &
GetGiveawayByIdQQ& 5
,QQ5 6
newQQ7 :
{QQ; <
idQQ= ?
=QQ@ A
idQQB D
}QQE F
)QQF G
.QQG H
ContinueWithQQH T
(QQT U
asyncQQU Z
(QQ[ \
taskQQ\ `
)QQ` a
=>QQb d
{RR 
varSS 
resultSS 
=SS  
taskSS! %
.SS% &
ResultSS& ,
;SS, -
ifTT 
(TT 
resultTT 
.TT 
AnyTT "
(TT" #
)TT# $
)TT$ %
{UU 
varVV 
giveawayVV $
=VV% &
resultVV' -
.VV- .
FirstVV. 3
(VV3 4
)VV4 5
;VV5 6
IMessageChannelWW '
channelWW( /
=WW0 1
_discordWW2 :
.WW: ;

GetChannelWW; E
(WWE F
giveawayWWF N
.WWN O
ChannelWWO V
)WWV W
;WWW X
ifXX 
(XX 
channelXX #
!=XX$ &
nullXX' +
)XX+ ,
{YY 
IUserMessageZZ (
messageZZ) 0
=ZZ1 2
awaitZZ3 8
channelZZ9 @
.ZZ@ A
GetMessageAsyncZZA P
(ZZP Q
giveawayZZQ Y
.ZZY Z
MessageZZZ a
)ZZa b
;ZZb c
if[[ 
([[  
message[[  '
!=[[( *
null[[+ /
)[[/ 0
{[[1 2
var\\  #
emote\\$ )
=\\* +
Emote\\, 1
.\\1 2
Parse\\2 7
(\\7 8
$str\\8 @
)\\@ A
;\\A B
var]]  #
	reactions]]$ -
=]]. /
message]]0 7
.]]7 8!
GetReactionUsersAsync]]8 M
(]]M N
emote]]N S
,]]S T
message]]U \
.]]\ ]
	Reactions]]] f
.]]f g
Count]]g l
)]]l m
;]]m n
}^^ 
}__ 
}`` 
}bb 
)bb 
;bb 
}dd 
}ee 	
publicgg 
asyncgg 
Taskgg 
CreateGiveawaygg (
(gg( )
)gg) *
{hh 	
}kk 	
}ll 
}mm 