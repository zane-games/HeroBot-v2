∏
jC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Attributes\VoiceAttribute.cs
	namespace		 	
HeroBot		
 
.		 
Plugins		 
.		 
Music		 
.		  

Attributes		  *
{

 
public 

class 
VoiceAttribute 
:  !!
PreconditionAttribute" 7
{ 
private 
readonly 
bool 
_playerExists +
;+ ,
private 
readonly 
bool #
_needUserInVoiceChannel 5
;5 6
public 
VoiceAttribute 
( 
bool "
needPlayerExists# 3
,3 4
bool5 9"
needUserInvoicechannel: P
)P Q
{ 	
_playerExists 
= 
needPlayerExists ,
;, -#
_needUserInVoiceChannel #
=$ %"
needUserInvoicechannel& <
;< =
} 	
public 
override 
async 
Task "
<" #
PreconditionResult# 5
>5 6!
CheckPermissionsAsync7 L
(L M
ICommandContextM \
context] d
,d e
CommandInfof q
commandr y
,y z
IServiceProvider	{ ã
services
å î
)
î ï
{ 	
var 
musicService 
= 
services '
.' (

GetService( 2
<2 3
MusicService3 ?
>? @
(@ A
)A B
;B C
if 
( 
_playerExists 
&&  
!! "
musicService" .
.. /
GetLavalinkCluster/ A
(A B
)B C
.C D
	HasPlayerD M
(M N
contextN U
.U V
GuildV [
.[ \
Id\ ^
)^ _
)_ `
{ 
return 
PreconditionResult	 
. 
	FromError %
(% &
$str& Q
)Q R
;R S
} 
if 
( #
_needUserInVoiceChannel '
&&( *
(+ ,
await, 1
context2 9
.9 :
Guild: ?
.? @
GetUserAsync@ L
(L M
contextM T
.T U
UserU Y
.Y Z
IdZ \
)\ ]
)] ^
.^ _
VoiceChannel_ k
==l n
nullo s
)s t
{ 
return   
PreconditionResult   -
.  - .
	FromError  . 7
(  7 8
$str  8 [
)  [ \
;  \ ]
}"" 
return## 
PreconditionResult## %
.##% &
FromSuccess##& 1
(##1 2
)##2 3
;##3 4
}$$ 	
}%% 
}&& Œx
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Modules\MusicModule.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
.  
Modules  '
{ 
[ 

NeedPlugin 
] 
public 

class 
MusicModule 
: 

ModuleBase )
<) * 
SocketCommandContext* >
>> ?
{ 
private 
readonly 
MusicService %
_music& ,
;, -
public 
MusicModule 
( 
MusicService '
musicService( 4
)4 5
{6 7
_music 
= 
musicService !
;! "
Assembly 
assembly 
= 
this  $
.$ %
GetType% ,
(, -
)- .
.. /
Assembly/ 7
;7 8
} 	
[ 	
Voice	 
( 
false 
, 
true 
) 
] 
[ 	
Command	 
( 
$str 
) 
] 
public 
async 
Task 
Join 
( 
)  
{! "
var 
playerExists 
= 
_music %
.% &
GetLavalinkCluster& 8
(8 9
)9 :
.: ;
	GetPlayer; D
<D E
PlayerE K
>K L
(L M
ContextM T
.T U
GuildU Z
.Z [
Id[ ]
)] ^
;^ _
if 
( 
playerExists 
!= 
null  $
)$ %
{& '
await   

ReplyAsync    
(    !
$"  ! #'
I'm already connected to <#  # >
{  > ?
playerExists  ? K
.  K L
VoiceChannelId  L Z
}  Z [
>  [ \
"  \ ]
)  ] ^
;  ^ _
}!! 
var"" 
member"" 
="" 
Context""  
.""  !
Guild""! &
.""& '
GetUser""' .
("". /
Context""/ 6
.""6 7
User""7 ;
.""; <
Id""< >
)""> ?
;""? @
await## 
_music## 
.## 
GetLavalinkCluster## ,
(##, -
)##- .
.##. /
	JoinAsync##/ 8
<##8 9
Player##9 ?
>##? @
(##@ A
Context##A H
.##H I
Guild##I N
.##N O
Id##O Q
,##Q R
member##R X
.##X Y
VoiceChannel##Y e
.##e f
Id##f h
)##h i
;##i j
}$$ 	
[&& 	
Command&&	 
(&& 
$str&& 
)&& 
]&& 
['' 	
Voice''	 
('' 
true'' 
,'' 
true'' 
)'' 
]'' 
public(( 
async(( 
Task(( 
Play(( 
((( 
[((  
	Remainder((  )
](() *
string((* 0
search((1 7
)((7 8
{((9 :
var)) 
player)) 
=)) 
_music)) 
.))  
GetLavalinkCluster))  2
())2 3
)))3 4
.))4 5
	GetPlayer))5 >
<))> ?
Player))? E
>))E F
())F G
Context))G N
.))N O
Guild))O T
.))T U
Id))U W
)))W X
;))X Y
if** 
(** 
player** 
.** 
socketTextchannel** (
==**) +
null**, 0
)**0 1
player**2 8
.**8 9
socketTextchannel**9 J
=**K L
Context**M T
.**T U
Channel**U \
;**\ ]
if++ 
(++ 
search++ 
.++ 

StartsWith++ !
(++! "
$str++" (
)++( )
)++) *
{,, 
var-- 
result-- 
=-- 
await-- "
_music--# )
.--) *
GetLavalinkCluster--* <
(--< =
)--= >
.--> ?
GetTrackAsync--? L
(--L M
search--M S
,--S T
Lavalink4NET--U a
.--a b
Rest--b f
.--f g

SearchMode--g q
.--q r
None--r v
)--v w
;--w x
if.. 
(.. 
result.. 
!=.. 
null.. "
).." #
{// 
await00 
player00  
.00  !
	PlayAsync00! *
(00* +
result00+ 1
)001 2
;002 3
await11 

ReplyAsync11 $
(11$ %
$"11% '
Added `11' .
{11. /
result11/ 5
.115 6
Title116 ;
}11; <
 - 11< ?
{11? @
result11@ F
.11F G
Author11G M
}11M N
` to the queue 11N ]
"11] ^
)11^ _
;11_ `
}22 
else33 
await33 

ReplyAsync33 %
(33% &
$str33& L
)33L M
;33M N
}44 
else55 
{66 
var77 
provider77 
=77 
Lavalink4NET77 +
.77+ ,
Rest77, 0
.770 1

SearchMode771 ;
.77; <
YouTube77< C
;77C D
if88 
(88 
search88 
.88 

StartsWith88 %
(88% &
$str88& +
)88+ ,
)88, -
{88. /
provider99 
=99 
Lavalink4NET99 +
.99+ ,
Rest99, 0
.990 1

SearchMode991 ;
.99; <

SoundCloud99< F
;99F G
search:: 
=:: 
search:: #
.::# $
Replace::$ +
(::+ ,
$str::, 1
,::1 2
String::3 9
.::9 :
Empty::: ?
)::? @
;::@ A
};; 
var<< 
result<< 
=<< 
await<< "
_music<<# )
.<<) *
GetLavalinkCluster<<* <
(<<< =
)<<= >
.<<> ?
GetTracksAsync<<? M
(<<M N
search<<N T
,<<T U
provider<<V ^
)<<^ _
;<<_ `
if== 
(== 
result== 
.== 
Any== 
(== 
)==  
)==  !
{>> 
var?? 
song?? 
=?? 
result?? %
.??% &
First??& +
(??+ ,
)??, -
;??- .
await@@ 
player@@  
.@@  !
	PlayAsync@@! *
(@@* +
song@@+ /
)@@/ 0
;@@0 1
awaitAA 

ReplyAsyncAA $
(AA$ %
$"AA% '
Added `AA' .
{AA. /
songAA/ 3
.AA3 4
TitleAA4 9
}AA9 :
 - AA: =
{AA= >
songAA> B
.AAB C
AuthorAAC I
}AAI J
` to the queue AAJ Y
"AAY Z
)AAZ [
;AA[ \
}BB 
elseCC 
awaitCC 

ReplyAsyncCC %
(CC% &
$strCC& @
)CC@ A
;CCA B
}DD 
}EE 	
[FF 	
VoiceFF	 
(FF 
trueFF 
,FF 
trueFF 
)FF 
]FF 
[GG 	
CommandGG	 
(GG 
$strGG 
)GG 
]GG 
publicHH 
asyncHH 
TaskHH 
	SetVolumeHH #
(HH# $
floatHH$ )
volumeHH* 0
)HH0 1
{HH2 3
ifII 
(II 
volumeII 
<=II 
$numII 
&&II  
volumeII! '
>II( )
$numII* +
)II+ ,
{JJ 
varKK 
playerKK 
=KK 
_musicKK #
.KK# $
GetLavalinkClusterKK$ 6
(KK6 7
)KK7 8
.KK8 9
	GetPlayerKK9 B
<KKB C
PlayerKKC I
>KKI J
(KKJ K
ContextKKK R
.KKR S
GuildKKS X
.KKX Y
IdKKY [
)KK[ \
;KK\ ]
ifLL 
(LL 
playerLL 
.LL 
socketTextchannelLL ,
==LL- /
nullLL0 4
)LL4 5
playerLL6 <
.LL< =
socketTextchannelLL= N
=LLO P
ContextLLQ X
.LLX Y
ChannelLLY `
;LL` a
awaitNN 
playerNN 
.NN 
SetVolumeAsyncNN +
(NN+ ,
volumeNN, 2
/NN2 3
$numNN3 6
)NN6 7
;NN7 8
}OO 
elsePP 
awaitQQ 

ReplyAsyncQQ  
(QQ  !
$strQQ! S
)QQS T
;QQT U
}RR 	
[SS 	
VoiceSS	 
(SS 
trueSS 
,SS 
trueSS 
)SS 
]SS 
[TT 	
CommandTT	 
(TT 
$strTT 
)TT 
]TT 
publicUU 
asyncUU 
TaskUU 
SetMegaVolumeUU '
(UU' (
floatUU( -
volumeUU. 4
)UU4 5
{UU6 7
varVV 
playerVV 
=VV 
_musicVV 
.VV  
GetLavalinkClusterVV  2
(VV2 3
)VV3 4
.VV4 5
	GetPlayerVV5 >
<VV> ?
PlayerVV? E
>VVE F
(VVF G
ContextVVG N
.VVN O
GuildVVO T
.VVT U
IdVVU W
)VVW X
;VVX Y
ifWW 
(WW 
playerWW 
.WW 
socketTextchannelWW (
==WW) +
nullWW, 0
)WW0 1
playerWW2 8
.WW8 9
socketTextchannelWW9 J
=WWK L
ContextWWM T
.WWT U
ChannelWWU \
;WW\ ]
awaitXX 
playerXX 
.XX 
SetVolumeAsyncXX '
(XX' (
volumeXX( .
)XX. /
;XX/ 0
}YY 	
[ZZ 	
VoiceZZ	 
(ZZ 
trueZZ 
,ZZ 
trueZZ 
)ZZ 
]ZZ 
[[[ 	
Command[[	 
([[ 
$str[[ 
)[[ 
][[ 
public\\ 
async\\ 
Task\\ 
Skip\\ 
(\\ 
int\\ "
count\\# (
=\\) *
$num\\+ ,
)\\, -
{\\. /
var]] 
player]] 
=]] 
_music]] 
.]]  
GetLavalinkCluster]]  2
(]]2 3
)]]3 4
.]]4 5
	GetPlayer]]5 >
<]]> ?
Player]]? E
>]]E F
(]]F G
Context]]G N
.]]N O
Guild]]O T
.]]T U
Id]]U W
)]]W X
;]]X Y
if^^ 
(^^ 
player^^ 
.^^ 
socketTextchannel^^ (
==^^) +
null^^, 0
)^^0 1
player^^2 8
.^^8 9
socketTextchannel^^9 J
=^^K L
Context^^M T
.^^T U
Channel^^U \
;^^\ ]
await__ 
player__ 
.__ 
	SkipAsync__ "
(__" #
)__# $
;__$ %
}`` 	
[aa 	
Voiceaa	 
(aa 
trueaa 
,aa 
trueaa 
)aa 
]aa 
[bb 	
Commandbb	 
(bb 
$strbb  
)bb  !
]bb! "
publiccc 
asynccc 
Taskcc 
ChangeChannelcc '
(cc' (
IMessageChannelcc( 7
socketChannelcc8 E
=ccF G
nullccH L
)ccL M
{ccN O
ifdd 
(dd 
socketChanneldd 
==dd  
nulldd! %
)dd% &
socketChannelee 
=ee 
Contextee  '
.ee' (
Channelee( /
;ee/ 0
varff 
playerff 
=ff 
_musicff 
.ff  
GetLavalinkClusterff  2
(ff2 3
)ff3 4
.ff4 5
	GetPlayerff5 >
<ff> ?
Playerff? E
>ffE F
(ffF G
ContextffG N
.ffN O
GuildffO T
.ffT U
IdffU W
)ffW X
;ffX Y
playergg 
.gg 
socketTextchannelgg $
=gg% &
socketChannelgg' 4
;gg4 5
awaithh 
socketChannelhh 
.hh  
SendFileAsynchh  -
(hh- .
$strhh. 0
)hh0 1
;hh1 2
}ii 	
[jj 	
Voicejj	 
(jj 
truejj 
,jj 
truejj 
)jj 
]jj 
[kk 	
Commandkk	 
(kk 
$strkk 
)kk 
]kk 
publicll 
asyncll 
Taskll 
Queuell 
(ll  
)ll  !
{ll" #
varmm 
playermm 
=mm 
_musicmm 
.mm  
GetLavalinkClustermm  2
(mm2 3
)mm3 4
.mm4 5
	GetPlayermm5 >
<mm> ?
Playermm? E
>mmE F
(mmF G
ContextmmG N
.mmN O
GuildmmO T
.mmT U
IdmmU W
)mmW X
;mmX Y
ifnn 
(nn 
playernn 
.nn 
socketTextchannelnn (
==nn) +
nullnn, 0
)nn0 1
playernn2 8
.nn8 9
socketTextchannelnn9 J
=nnK L
ContextnnM T
.nnT U
ChannelnnU \
;nn\ ]
varoo 
queueoo 
=oo 
playeroo 
.oo 
Queueoo $
;oo$ %
ifqq 
(qq 
queueqq 
.qq 
Countqq 
==qq 
$numqq  
)qq  !
{qq" #
awaitqq$ )

ReplyAsyncqq* 4
(qq4 5
$strqq5 T
)qqT U
;qqU V
returnqqW ]
;qq] ^
}qq_ `
varrr 
sbrr 
=rr 
newrr 
StringBuilderrr &
(rr& '
$"rr' )
	There is rr) 2
{rr2 3
queuerr3 8
.rr8 9
Countrr9 >
}rr> ?#
 song(s) in the queue :rr? V
"rrV W
)rrW X
;rrX Y
foreachss 
(ss 
varss 
songss 
inss  
queuess! &
)ss& '
{ss( )
sbtt 
.tt 
Appendtt 
(tt 
$strtt $
)tt$ %
.tt% &
Appendtt& ,
(tt, -
songtt- 1
.tt1 2
Titlett2 7
)tt7 8
.tt8 9
Appendtt9 ?
(tt? @
$strtt@ E
)ttE F
.ttF G
AppendttG M
(ttM N
songttN R
.ttR S
AuthorttS Y
)ttY Z
.ttZ [
Appendtt[ a
(tta b
$strttb i
)tti j
;ttj k
}uu 
awaitvv 

ReplyAsyncvv 
(vv 
sbvv 
.vv  
ToStringvv  (
(vv( )
)vv) *
)vv* +
;vv+ ,
}ww 	
}xx 
}yy ¯
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\PluginRefferal.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
{ 
public 

class 
PluginRefferal 
:  !
IPluginRefferal" 1
{ 
} 
}		 ›
fC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Services\MusicService.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
.  
Services  (
{ 
[ 
Service 
] 
public 

class 
MusicService 
{ 
private 
readonly  
DiscordShardedClient -
_discord. 6
;6 7
private 
LavalinkCluster 
lavalinkCluster  /
;/ 0
public 
MusicService 
(  
DiscordShardedClient 0 
discordShardedclient1 E
)E F
{ 	
_discord 
=  
discordShardedclient +
;+ , 
discordShardedclient  
.  !

ShardReady! +
+=, .+
DiscordShardedclient_ShardReady/ N
;N O
} 	
internal 
LavalinkCluster  
GetLavalinkCluster! 3
(3 4
)4 5
{6 7
return8 >
lavalinkCluster? N
;N O
}P Q
private 
async 
System 
. 
	Threading &
.& '
Tasks' ,
., -
Task- 1+
DiscordShardedclient_ShardReady2 Q
(Q R
DiscordSocketClientR e
argf i
)i j
{   	
Console!! 
.!! 
	WriteLine!! !
(!!! "
$str!!" 5
)!!5 6
;!!6 7
lavalinkCluster"" 
=""  !
new""" %
LavalinkCluster""& 5
(""5 6
new""6 9"
LavalinkClusterOptions"": P
(""P Q
)""Q R
{## 

StayOnline$$ 
=$$  
true$$! %
,$$% &
Nodes%% 
=%% 
new%% 
[%%  
]%%  !
{%%" #
new%%$ '
Lavalink4NET%%( 4
.%%4 5
LavalinkNodeOptions%%5 H
(%%H I
)%%I J
{&& 
RestUri'' 
='' 
$"''  0
$http://lavalink.alivecreation.fr:80/''  D
"''D E
,''E F
WebSocketUri((  
=((! "
$"((# %.
"ws://lavalink.alivecreation.fr:80/((% G
"((G H
,((H I
Password)) 
=)) 
Uri)) "
.))" #
EscapeDataString))# 3
())3 4
$str	))4 ∂
)
))∂ ∑
,
))∑ ∏
AllowResuming** !
=**" #
true**$ (
}++ 
}++ 
,++  
LoadBalacingStrategy-- (
=--) *#
LoadBalancingStrategies--+ B
.--B C
ScoreStrategy--C P
}.. 
,.. 
new..  
DiscordClientWrapper.. +
(..+ ,
_discord.., 4
)..4 5
)..5 6
;..6 7
await// 
lavalinkCluster// %
.//% &
InitializeAsync//& 5
(//5 6
)//6 7
;//7 8
}33 	
}44 
}55 ®
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Services\Player.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
.  
Services  (
{ 
class 	
Player
 
:  
QueuedLavalinkPlayer '
{ 
internal 
IMessageChannel  
socketTextchannel! 2
;2 3
public 
Player 
( 
LavalinkSocket $
lavalinkSocket% 3
,3 4!
IDiscordClientWrapper5 J
clientK Q
,Q R
ulongS X
guildIdY `
,` a
boolb f
disconnectOnStopg w
)w x
:y z
base{ 
(	 Ä
lavalinkSocket
Ä é
,
é è
client
ê ñ
,
ñ ó
guildId
ò ü
,
ü †
disconnectOnStop
° ±
)
± ≤
{ 	
} 	
public 
override 
async 
Task "
OnConnectedAsync# 3
(3 4
VoiceServer4 ?
voiceServer@ K
,K L

VoiceStateM W

voiceStateX b
)b c
{ 	
await 
socketTextchannel #
.# $
SendMessageAsync$ 4
(4 5
$"5 7
I'm connected to <#7 J
{J K

voiceStateK U
.U V
VoiceChannelIdV d
}d e
>.e g
"g h
)h i
;i j
await 
base 
. 
OnConnectedAsync '
(' (
voiceServer( 3
,3 4

voiceState5 ?
)? @
;@ A
} 	
public 
override 
async 
Task "!
OnTrackExceptionAsync# 8
(8 9#
TrackExceptionEventArgs9 P
	eventArgsQ Z
)Z [
{ 	
await 
socketTextchannel #
.# $
SendMessageAsync$ 4
(4 5
$"5 7A
5An exception was throwed during playing a music in <#7 l
{l m
	eventArgsm v
.v w
Playerw }
.} ~
VoiceChannelId	~ å
}
å ç
>
ç é
"
é è
)
è ê
;
ê ë
await 
base 
. !
OnTrackExceptionAsync ,
(, -
	eventArgs- 6
)6 7
;7 8
}   	
public!! 
override!! 
async!! 
Task!! "
OnTrackEndAsync!!# 2
(!!2 3
TrackEndEventArgs!!3 D
	eventArgs!!E N
)!!N O
{"" 	
if## 
(## 
	eventArgs## 
.## 
MayStartNext## &
)##& '
{##( )
var$$ 
next$$ 
=$$ 
this$$ 
.$$  
Queue$$  %
.$$% &
Tracks$$& ,
.$$, -
First$$- 2
($$2 3
)$$3 4
;$$4 5
await%% 
socketTextchannel%% '
.%%' (
SendMessageAsync%%( 8
(%%8 9
$"%%9 ;
Now playing `%%; H
{%%H I
next%%I M
.%%M N
Title%%N S
}%%S T
 - %%T W
{%%W X
next%%X \
.%%\ ]
Author%%] c
}%%c d
`%%d e
"%%e f
)%%f g
;%%g h
}&& 
await'' 
base'' 
.'' 
OnTrackEndAsync'' &
(''& '
	eventArgs''' 0
)''0 1
;''1 2
}(( 	
})) 
}** 