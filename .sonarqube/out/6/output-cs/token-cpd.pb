∏
jC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Attributes\VoiceAttribute.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
.  

Attributes  *
{ 
public		 

class		 
VoiceAttribute		 
:		  !!
PreconditionAttribute		" 7
{

 
private 
readonly 
bool 
_playerExists +
;+ ,
private 
readonly 
bool #
_needUserInVoiceChannel 5
;5 6
public 
VoiceAttribute 
( 
bool "
needPlayerExists# 3
,3 4
bool5 9"
needUserInvoicechannel: P
)P Q
{ 	
_playerExists 
= 
needPlayerExists ,
;, -#
_needUserInVoiceChannel #
=$ %"
needUserInvoicechannel& <
;< =
} 	
public 
override 
async 
Task "
<" #
PreconditionResult# 5
>5 6!
CheckPermissionsAsync7 L
(L M
ICommandContextM \
context] d
,d e
CommandInfof q
commandr y
,y z
IServiceProvider	{ ã
services
å î
)
î ï
{ 	
var 
musicService 
= 
services '
.' (

GetService( 2
<2 3
MusicService3 ?
>? @
(@ A
)A B
;B C
if 
( 
_playerExists 
&&  
!! "
musicService" .
.. /
GetLavalinkCluster/ A
(A B
)B C
.C D
	HasPlayerD M
(M N
contextN U
.U V
GuildV [
.[ \
Id\ ^
)^ _
)_ `
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$str4 _
)_ `
;` a
} 
if 
( #
_needUserInVoiceChannel '
&&( *
(+ ,
await, 1
context2 9
.9 :
Guild: ?
.? @
GetUserAsync@ L
(L M
contextM T
.T U
UserU Y
.Y Z
IdZ \
)\ ]
)] ^
.^ _
VoiceChannel_ k
==l n
nullo s
)s t
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$str4 W
)W X
;X Y
}   
return!! 
PreconditionResult!! %
.!!% &
FromSuccess!!& 1
(!!1 2
)!!2 3
;!!3 4
}"" 	
}## 
}$$ ≤°
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
true 
) 
] 
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
;""? @
var## 
payer## 
=## 
await## 
_music## $
.##$ %
GetLavalinkCluster##% 7
(##7 8
)##8 9
.##9 :
	JoinAsync##: C
<##C D
Player##D J
>##J K
(##K L
Context##L S
.##S T
Guild##T Y
.##Y Z
Id##Z \
,##\ ]
member##^ d
.##d e
VoiceChannel##e q
.##q r
Id##r t
)##t u
;##u v
payer$$ 
.$$ 
socketTextchannel$$ #
=$$$ %
Context$$& -
.$$- .
Channel$$. 5
;$$5 6
}%% 	
['' 	
Command''	 
('' 
$str'' 
)'' 
]'' 
[(( 	
Voice((	 
((( 
true(( 
,(( 
true(( 
)(( 
](( 
public)) 
async)) 
Task)) 
Play)) 
()) 
[))  
	Remainder))  )
]))) *
string))* 0
search))1 7
)))7 8
{))9 :
var** 
player** 
=** 
_music** 
.**  
GetLavalinkCluster**  2
(**2 3
)**3 4
.**4 5
	GetPlayer**5 >
<**> ?
Player**? E
>**E F
(**F G
Context**G N
.**N O
Guild**O T
.**T U
Id**U W
)**W X
;**X Y
if++ 
(++ 
player++ 
.++ 
socketTextchannel++ (
==++) +
null++, 0
)++0 1
player++2 8
.++8 9
socketTextchannel++9 J
=++K L
Context++M T
.++T U
Channel++U \
;++\ ]
if,, 
(,, 
search,, 
.,, 

StartsWith,, !
(,,! "
$str,," (
),,( )
),,) *
{-- 
var.. 
result.. 
=.. 
await.. "
_music..# )
...) *
GetLavalinkCluster..* <
(..< =
)..= >
...> ?
GetTrackAsync..? L
(..L M
search..M S
,..S T
Lavalink4NET..U a
...a b
Rest..b f
...f g

SearchMode..g q
...q r
None..r v
)..v w
;..w x
if// 
(// 
result// 
!=// 
null// "
)//" #
{00 
await11 
player11  
.11  !
	PlayAsync11! *
(11* +
result11+ 1
)111 2
;112 3
await22 

ReplyAsync22 $
(22$ %
$"22% '
Added `22' .
{22. /
result22/ 5
.225 6
Title226 ;
}22; <
 - 22< ?
{22? @
result22@ F
.22F G
Author22G M
}22M N
` to the queue 22N ]
"22] ^
)22^ _
;22_ `
}33 
else44 
await44 

ReplyAsync44 %
(44% &
$str44& L
)44L M
;44M N
}55 
else66 
{77 
var88 
provider88 
=88 
Lavalink4NET88 +
.88+ ,
Rest88, 0
.880 1

SearchMode881 ;
.88; <
YouTube88< C
;88C D
if99 
(99 
search99 
.99 

StartsWith99 %
(99% &
$str99& +
)99+ ,
)99, -
{99. /
provider:: 
=:: 
Lavalink4NET:: +
.::+ ,
Rest::, 0
.::0 1

SearchMode::1 ;
.::; <

SoundCloud::< F
;::F G
search;; 
=;; 
search;; #
.;;# $
Replace;;$ +
(;;+ ,
$str;;, 1
,;;1 2
String;;3 9
.;;9 :
Empty;;: ?
);;? @
;;;@ A
}<< 
var== 
result== 
=== 
await== "
_music==# )
.==) *
GetLavalinkCluster==* <
(==< =
)=== >
.==> ?
GetTracksAsync==? M
(==M N
search==N T
,==T U
provider==V ^
)==^ _
;==_ `
if>> 
(>> 
result>> 
.>> 
Any>> 
(>> 
)>>  
)>>  !
{?? 
var@@ 
song@@ 
=@@ 
result@@ %
.@@% &
First@@& +
(@@+ ,
)@@, -
;@@- .
awaitAA 
playerAA  
.AA  !
	PlayAsyncAA! *
(AA* +
songAA+ /
)AA/ 0
;AA0 1
awaitBB 

ReplyAsyncBB $
(BB$ %
$"BB% '
Added `BB' .
{BB. /
songBB/ 3
.BB3 4
TitleBB4 9
}BB9 :
 - BB: =
{BB= >
songBB> B
.BBB C
AuthorBBC I
}BBI J
` to the queue BBJ Y
"BBY Z
)BBZ [
;BB[ \
}CC 
elseDD 
awaitDD 

ReplyAsyncDD %
(DD% &
$strDD& @
)DD@ A
;DDA B
}EE 
}FF 	
[GG 	
VoiceGG	 
(GG 
trueGG 
,GG 
trueGG 
)GG 
]GG 
[HH 	
CommandHH	 
(HH 
$strHH 
)HH 
]HH 
publicII 
asyncII 
TaskII 
	SetVolumeII #
(II# $
floatII$ )
volumeII* 0
)II0 1
{II2 3
ifJJ 
(JJ 
volumeJJ 
<=JJ 
$numJJ 
&&JJ  
volumeJJ! '
>JJ( )
$numJJ* +
)JJ+ ,
{KK 
varLL 
playerLL 
=LL 
_musicLL #
.LL# $
GetLavalinkClusterLL$ 6
(LL6 7
)LL7 8
.LL8 9
	GetPlayerLL9 B
<LLB C
PlayerLLC I
>LLI J
(LLJ K
ContextLLK R
.LLR S
GuildLLS X
.LLX Y
IdLLY [
)LL[ \
;LL\ ]
ifMM 
(MM 
playerMM 
.MM 
socketTextchannelMM ,
==MM- /
nullMM0 4
)MM4 5
playerMM6 <
.MM< =
socketTextchannelMM= N
=MMO P
ContextMMQ X
.MMX Y
ChannelMMY `
;MM` a
awaitOO 
playerOO 
.OO 
SetVolumeAsyncOO +
(OO+ ,
volumeOO, 2
/OO3 4
$numOO5 8
)OO8 9
;OO9 :
}PP 
elseQQ 
awaitRR 

ReplyAsyncRR  
(RR  !
$strRR! S
)RRS T
;RRT U
}SS 	
[TT 	
VoiceTT	 
(TT 
trueTT 
,TT 
trueTT 
)TT 
]TT 
[UU 	
CommandUU	 
(UU 
$strUU 
)UU 
]UU 
publicVV 
asyncVV 
TaskVV 
SetMegaVolumeVV '
(VV' (
floatVV( -
volumeVV. 4
)VV4 5
{VV6 7
ifWW 
(WW 
volumeWW 
<=WW 
$numWW 
&&WW 
volumeWW  &
>WW' (
$numWW) *
)WW* +
{XX 
varYY 
playerYY 
=YY 
_musicYY #
.YY# $
GetLavalinkClusterYY$ 6
(YY6 7
)YY7 8
.YY8 9
	GetPlayerYY9 B
<YYB C
PlayerYYC I
>YYI J
(YYJ K
ContextYYK R
.YYR S
GuildYYS X
.YYX Y
IdYYY [
)YY[ \
;YY\ ]
ifZZ 
(ZZ 
playerZZ 
.ZZ 
socketTextchannelZZ ,
==ZZ- /
nullZZ0 4
)ZZ4 5
playerZZ6 <
.ZZ< =
socketTextchannelZZ= N
=ZZO P
ContextZZQ X
.ZZX Y
ChannelZZY `
;ZZ` a
await\\ 
player\\ 
.\\ 
SetVolumeAsync\\ +
(\\+ ,
volume\\, 2
)\\2 3
;\\3 4
}]] 
else^^ 
await__ 

ReplyAsync__  
(__  !
$str__! R
)__R S
;__S T
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
$strbb 
)bb 
]bb 
publiccc 
asynccc 
Taskcc 
Skipcc 
(cc 
intcc "
countcc# (
=cc) *
$numcc+ ,
)cc, -
{cc. /
vardd 
playerdd 
=dd 
_musicdd 
.dd  
GetLavalinkClusterdd  2
(dd2 3
)dd3 4
.dd4 5
	GetPlayerdd5 >
<dd> ?
Playerdd? E
>ddE F
(ddF G
ContextddG N
.ddN O
GuildddO T
.ddT U
IdddU W
)ddW X
;ddX Y
ifee 
(ee 
playeree 
.ee 
socketTextchannelee (
==ee) +
nullee, 0
)ee0 1
playeree2 8
.ee8 9
socketTextchannelee9 J
=eeK L
ContexteeM T
.eeT U
ChanneleeU \
;ee\ ]
awaitff 
playerff 
.ff 
	SkipAsyncff "
(ff" #
countff# (
)ff( )
;ff) *
}gg 	
[hh 	
Voicehh	 
(hh 
truehh 
,hh 
truehh 
)hh 
]hh 
[ii 	
Commandii	 
(ii 
$strii  
)ii  !
]ii! "
publicjj 
asyncjj 
Taskjj 
ChangeChanneljj '
(jj' (
IMessageChanneljj( 7
socketChanneljj8 E
=jjF G
nulljjH L
)jjL M
{jjN O
ifkk 
(kk 
socketChannelkk 
==kk  
nullkk! %
)kk% &
socketChannelll 
=ll 
Contextll  '
.ll' (
Channelll( /
;ll/ 0
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
;mmX Y
playernn 
.nn 
socketTextchannelnn $
=nn% &
socketChannelnn' 4
;nn4 5
awaitoo 
socketChanneloo 
.oo  
SendFileAsyncoo  -
(oo- .
$stroo. 0
)oo0 1
;oo1 2
}pp 	
[qq 	
Voiceqq	 
(qq 
trueqq 
,qq 
trueqq 
)qq 
]qq 
[rr 	
Commandrr	 
(rr 
$strrr 
)rr 
]rr 
publicss 
asyncss 
Taskss 
Queuess 
(ss  
)ss  !
{ss" #
vartt 
playertt 
=tt 
_musictt 
.tt  
GetLavalinkClustertt  2
(tt2 3
)tt3 4
.tt4 5
	GetPlayertt5 >
<tt> ?
Playertt? E
>ttE F
(ttF G
ContextttG N
.ttN O
GuildttO T
.ttT U
IdttU W
)ttW X
;ttX Y
ifuu 
(uu 
playeruu 
.uu 
socketTextchanneluu (
==uu) +
nulluu, 0
)uu0 1
playeruu2 8
.uu8 9
socketTextchanneluu9 J
=uuK L
ContextuuM T
.uuT U
ChanneluuU \
;uu\ ]
varvv 
queuevv 
=vv 
playervv 
.vv 
Queuevv $
;vv$ %
ifxx 
(xx 
queuexx 
.xx 
Countxx 
==xx 
$numxx  
)xx  !
{xx" #
awaitxx$ )

ReplyAsyncxx* 4
(xx4 5
$strxx5 T
)xxT U
;xxU V
returnxxW ]
;xx] ^
}xx_ `
varyy 
sbyy 
=yy 
newyy 
StringBuilderyy &
(yy& '
$"yy' )
	There is yy) 2
{yy2 3
queueyy3 8
.yy8 9
Countyy9 >
}yy> ?'
 song(s) in the queue :\r\nyy? Z
"yyZ [
)yy[ \
;yy\ ]
foreachzz 
(zz 
varzz 
songzz 
inzz  
queuezz! &
)zz& '
{zz( )
sb{{ 
.{{ 
Append{{ 
({{ 
$str{{ $
){{$ %
.{{% &
Append{{& ,
({{, -
song{{- 1
.{{1 2
Title{{2 7
){{7 8
.{{8 9
Append{{9 ?
({{? @
$str{{@ E
){{E F
.{{F G
Append{{G M
({{M N
song{{N R
.{{R S
Author{{S Y
){{Y Z
.{{Z [
Append{{[ a
({{a b
$str{{b i
){{i j
;{{j k
}|| 
await}} 

ReplyAsync}} 
(}} 
sb}} 
.}}  
ToString}}  (
(}}( )
)}}) *
)}}* +
;}}+ ,
}~~ 	
[ 	
Voice	 
( 
true 
, 
true 
) 
] 
[
ÄÄ 	
Command
ÄÄ	 
(
ÄÄ 
$str
ÄÄ 
)
ÄÄ 
]
ÄÄ 
public
ÅÅ 
async
ÅÅ 
Task
ÅÅ 
	StopMusic
ÅÅ #
(
ÅÅ# $
)
ÅÅ$ %
{
ÅÅ& '
var
ÇÇ 
player
ÇÇ 
=
ÇÇ 
_music
ÇÇ 
.
ÇÇ   
GetLavalinkCluster
ÇÇ  2
(
ÇÇ2 3
)
ÇÇ3 4
.
ÇÇ4 5
	GetPlayer
ÇÇ5 >
<
ÇÇ> ?
Player
ÇÇ? E
>
ÇÇE F
(
ÇÇF G
Context
ÇÇG N
.
ÇÇN O
Guild
ÇÇO T
.
ÇÇT U
Id
ÇÇU W
)
ÇÇW X
;
ÇÇX Y
await
ÉÉ 
player
ÉÉ 
.
ÉÉ 
	StopAsync
ÉÉ "
(
ÉÉ" #
)
ÉÉ# $
;
ÉÉ$ %
}
ÑÑ 	
[
ÖÖ 	
Voice
ÖÖ	 
(
ÖÖ 
true
ÖÖ 
,
ÖÖ 
true
ÖÖ 
)
ÖÖ 
]
ÖÖ 
[
ÜÜ 	
Command
ÜÜ	 
(
ÜÜ 
$str
ÜÜ 
)
ÜÜ 
]
ÜÜ 
public
áá 
async
áá 
Task
áá 

Disconnect
áá $
(
áá$ %
)
áá% &
{
áá' (
var
àà 
player
àà 
=
àà 
_music
àà 
.
àà   
GetLavalinkCluster
àà  2
(
àà2 3
)
àà3 4
.
àà4 5
	GetPlayer
àà5 >
<
àà> ?
Player
àà? E
>
ààE F
(
ààF G
Context
ààG N
.
ààN O
Guild
ààO T
.
ààT U
Id
ààU W
)
ààW X
;
ààX Y
await
ââ 
player
ââ 
.
ââ 
DisconnectAsync
ââ (
(
ââ( )
)
ââ) *
;
ââ* +
await
ää 
player
ää 
.
ää 
DisposeAsync
ää %
(
ää% &
)
ää& '
;
ää' (
}
ãã 	
[
åå 	
Voice
åå	 
(
åå 
true
åå 
,
åå 
true
åå 
)
åå 
]
åå 
[
çç 	
Command
çç	 
(
çç 
$str
çç 
)
çç 
]
çç 
public
éé 
async
éé 
Task
éé 
Resume
éé  
(
éé  !
)
éé! "
{
éé# $
var
èè 
player
èè 
=
èè 
_music
èè 
.
èè   
GetLavalinkCluster
èè  2
(
èè2 3
)
èè3 4
.
èè4 5
	GetPlayer
èè5 >
<
èè> ?
Player
èè? E
>
èèE F
(
èèF G
Context
èèG N
.
èèN O
Guild
èèO T
.
èèT U
Id
èèU W
)
èèW X
;
èèX Y
await
êê 
player
êê 
.
êê 
ResumeAsync
êê $
(
êê$ %
)
êê% &
;
êê& '
}
ëë 	
[
íí 	
Command
íí	 
(
íí 
$str
íí 
)
íí 
]
íí 
public
ìì 
async
ìì 
Task
ìì 
Pause
ìì 
(
ìì  
)
ìì  !
{
ìì" #
var
îî 
player
îî 
=
îî 
_music
îî 
.
îî   
GetLavalinkCluster
îî  2
(
îî2 3
)
îî3 4
.
îî4 5
	GetPlayer
îî5 >
<
îî> ?
Player
îî? E
>
îîE F
(
îîF G
Context
îîG N
.
îîN O
Guild
îîO T
.
îîT U
Id
îîU W
)
îîW X
;
îîX Y
await
ïï 
player
ïï 
.
ïï 

PauseAsync
ïï #
(
ïï# $
)
ïï$ %
;
ïï% &
}
ññ 	
[
óó 	
Command
óó	 
(
óó 
$str
óó 
)
óó 
]
óó 
public
òò 
async
òò 
Task
òò 
Loop
òò 
(
òò 
)
òò  
{
ôô 	
var
öö 
player
öö 
=
öö 
_music
öö 
.
öö   
GetLavalinkCluster
öö  2
(
öö2 3
)
öö3 4
.
öö4 5
	GetPlayer
öö5 >
<
öö> ?
Player
öö? E
>
ööE F
(
ööF G
Context
ööG N
.
ööN O
Guild
ööO T
.
ööT U
Id
ööU W
)
ööW X
;
ööX Y
player
õõ 
.
õõ 
	IsLooping
õõ 
=
õõ 
!
õõ  
player
õõ  &
.
õõ& '
	IsLooping
õõ' 0
;
õõ0 1
}
úú 	
}
ùù 
}ûû ¯
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
}		 „
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
{   	
lavalinkCluster!! 
=!!  !
new!!" %
LavalinkCluster!!& 5
(!!5 6
new!!6 9"
LavalinkClusterOptions!!: P
(!!P Q
)!!Q R
{"" 

StayOnline## 
=##  
true##! %
,##% &
Nodes$$ 
=$$ 
new$$ 
[$$  
]$$  !
{$$" #
new$$$ '
Lavalink4NET$$( 4
.$$4 5
LavalinkNodeOptions$$5 H
($$H I
)$$I J
{%% 
RestUri&& 
=&& 
$"&&  0
$http://lavalink.alivecreation.fr:80/&&  D
"&&D E
,&&E F
WebSocketUri''  
=''! "
$"''# %.
"ws://lavalink.alivecreation.fr:80/''% G
"''G H
,''H I
Password(( 
=(( 
Uri(( "
.((" #
EscapeDataString((# 3
(((3 4
$str	((4 ∂
)
((∂ ∑
,
((∑ ∏
AllowResuming)) !
=))" #
true))$ (
}** 
}** 
,**  
LoadBalacingStrategy,, (
=,,) *#
LoadBalancingStrategies,,+ B
.,,B C
ScoreStrategy,,C P
}-- 
,-- 
new--  
DiscordClientWrapper-- +
(--+ ,
_discord--, 4
)--4 5
)--5 6
;--6 7
await.. 
lavalinkCluster.. %
...% &
InitializeAsync..& 5
(..5 6
)..6 7
;..7 8
}22 	
}33 
}44 óS
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.Music\Services\Player.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
Music 
.  
Services  (
{ 
class 	
Player
 
:  
QueuedLavalinkPlayer '
{ 
bool 
	connected 
= 
true 
; 
internal 
IMessageChannel  
socketTextchannel! 2
;2 3
public 
Player 
( 
LavalinkSocket $
lavalinkSocket% 3
,3 4!
IDiscordClientWrapper5 J
clientK Q
,Q R
ulongS X
guildIdY `
,` a
boolb f
disconnectOnStopg w
)w x
:y z
base{ 
(	 Ä
lavalinkSocket
Ä é
,
é è
client
ê ñ
,
ñ ó
guildId
ò ü
,
ü †
disconnectOnStop
° ±
)
± ≤
{ 	
} 	
public 
override 
async 
Task "
OnConnectedAsync# 3
(3 4
VoiceServer4 ?
voiceServer@ K
,K L

VoiceStateM W

voiceStateX b
)b c
{ 	
if 
( 
	connected 
) 
{ 
await 
socketTextchannel '
.' (
SendMessageAsync( 8
(8 9
$"9 ;#
I'm now connected to <#; R
{R S

voiceStateS ]
.] ^
VoiceChannelId^ l
}l m
>.m o
"o p
)p q
;q r
	connected 
= 
false !
;! "
} 
await 
base 
. 
OnConnectedAsync '
(' (
voiceServer( 3
,3 4

voiceState5 ?
)? @
;@ A
}   	
public!! 
override!! 
async!! 
Task!! "!
OnTrackExceptionAsync!!# 8
(!!8 9#
TrackExceptionEventArgs!!9 P
	eventArgs!!Q Z
)!!Z [
{"" 	
await## 
socketTextchannel## #
.### $
SendMessageAsync##$ 4
(##4 5
$"##5 7A
5An exception was throwed during playing a music in <###7 l
{##l m
	eventArgs##m v
.##v w
Player##w }
.##} ~
VoiceChannelId	##~ å
}
##å ç
>
##ç é
"
##é è
)
##è ê
;
##ê ë
await$$ 
base$$ 
.$$ !
OnTrackExceptionAsync$$ ,
($$, -
	eventArgs$$- 6
)$$6 7
;$$7 8
}%% 	
public&& 
override&& 
async&& 
Task&& "
OnTrackEndAsync&&# 2
(&&2 3
TrackEndEventArgs&&3 D
	eventArgs&&E N
)&&N O
{'' 	
await(( 
base(( 
.(( 
OnTrackEndAsync(( &
(((& '
	eventArgs((' 0
)((0 1
;((1 2
if)) 
()) 
	eventArgs)) 
.)) 
MayStartNext)) &
)))& '
{))( )
var** 
next** 
=** 
this** 
.**  
Queue**  %
.**% &
Tracks**& ,
.**, -
First**- 2
(**2 3
)**3 4
;**4 5
await++ 
socketTextchannel++ '
.++' (
SendMessageAsync++( 8
(++8 9
$"++9 ;
Now playing `++; H
{++H I
next++I M
.++M N
Title++N S
}++S T
 - ++T W
{++W X
next++X \
.++\ ]
Author++] c
}++c d
`++d e
"++e f
)++f g
;++g h
},, 
}-- 	
public.. 
override.. 
Task.. 
OnTrackStuckAsync.. .
(... /
TrackStuckEventArgs../ B
	eventArgs..C L
)..L M
{// 	
return00 
base00 
.00 
OnTrackStuckAsync00 )
(00) *
	eventArgs00* 3
)003 4
;004 5
}11 	
public33 
override33 
async33 
Task33 "

PauseAsync33# -
(33- .
)33. /
{44 	
await55 
base55 
.55 

PauseAsync55 !
(55! "
)55" #
;55# $
await66 
socketTextchannel66 #
.66# $
SendMessageAsync66$ 4
(664 5
$str665 E
)66E F
;66F G
}77 	
public99 
override99 
async99 
Task99 "
DisconnectAsync99# 2
(992 3
)993 4
{:: 	
await;; 
base;; 
.;; 
DisconnectAsync;; &
(;;& '
);;' (
;;;( )
await<< 
socketTextchannel<< #
.<<# $
SendMessageAsync<<$ 4
(<<4 5
$str<<5 M
)<<M N
;<<N O
}== 	
public?? 
override?? 
async?? 
Task?? "
ReplayAsync??# .
(??. /
)??/ 0
{@@ 	
awaitAA 
baseAA 
.AA 
ReplayAsyncAA "
(AA" #
)AA# $
;AA$ %
awaitBB 
socketTextchannelBB #
.BB# $
SendMessageAsyncBB$ 4
(BB4 5
$"BB5 7

Replaying BB7 A
{BBA B
CurrentTrackBBB N
.BBN O
TitleBBO T
}BBT U
 - BBU X
{BBX Y
CurrentTrackBBY e
.BBe f
AuthorBBf l
}BBl m
"BBm n
)BBn o
;BBo p
}CC 	
publicEE 
overrideEE 
asyncEE 
TaskEE "
ResumeAsyncEE# .
(EE. /
)EE/ 0
{FF 	
awaitGG 
baseGG 
.GG 
ResumeAsyncGG "
(GG" #
)GG# $
;GG$ %
awaitHH 
socketTextchannelHH #
.HH# $
SendMessageAsyncHH$ 4
(HH4 5
$strHH5 I
)HHI J
;HHJ K
}II 	
publicKK 
overrideKK 
asyncKK 
TaskKK "
SeekPositionAsyncKK# 4
(KK4 5
TimeSpanKK5 =
positionKK> F
)KKF G
{LL 	
awaitMM 
baseMM 
.MM 
SeekPositionAsyncMM (
(MM( )
positionMM) 1
)MM1 2
;MM2 3
awaitNN 
socketTextchannelNN #
.NN# $
SendMessageAsyncNN$ 4
(NN4 5
$"NN5 7
Playing NN7 ?
{NN? @
CurrentTrackNN@ L
.NNL M
TitleNNM R
}NNR S
 - NNS V
{NNV W
CurrentTrackNNW c
.NNc d
AuthorNNd j
}NNj k
 at NNk o
{NNo p
positionNNp x
.NNx y
ToHumanReadable	NNy à
(
NNà â
)
NNâ ä
}
NNä ã
"
NNã å
)
NNå ç
;
NNç é
}OO 	
publicQQ 
overrideQQ 
asyncQQ 
TaskQQ "
SetVolumeAsyncQQ# 1
(QQ1 2
floatQQ2 7
volumeQQ8 >
=QQ? @
$numQQA B
,QQB C
boolQQD H
	normalizeQQI R
=QQS T
falseQQU Z
)QQZ [
{RR 	
awaitSS 
baseSS 
.SS 
SetVolumeAsyncSS %
(SS% &
volumeSS& ,
,SS, -
	normalizeSS. 7
)SS7 8
;SS8 9
awaitTT 
socketTextchannelTT #
.TT# $
SendMessageAsyncTT$ 4
(TT4 5
$"TT5 7
Volume set to TT7 E
{TTE F
volumeTTF L
}TTL M
"TTM N
)TTN O
;TTO P
}UU 	
publicWW 
overrideWW 
TaskWW  
UpdateEqualizerAsyncWW 1
(WW1 2
IEnumerableWW2 =
<WW= >
EqualizerBandWW> K
>WWK L
bandsWWM R
,WWR S
boolWWT X
resetWWY ^
=WW_ `
trueWWa e
)WWe f
{XX 	
returnYY 
baseYY 
.YY  
UpdateEqualizerAsyncYY ,
(YY, -
bandsYY- 2
,YY2 3
resetYY4 9
)YY9 :
;YY: ;
}ZZ 	
public\\ 
override\\ 
Task\\ 
PlayTopAsync\\ )
(\\) *
LavalinkTrack\\* 7
track\\8 =
)\\= >
{]] 	
return^^ 
base^^ 
.^^ 
PlayTopAsync^^ $
(^^$ %
track^^% *
)^^* +
;^^+ ,
}__ 	
publicaa 
overrideaa 
Taskaa 
<aa 
boolaa !
>aa! "
PushTrackAsyncaa# 1
(aa1 2
LavalinkTrackaa2 ?
trackaa@ E
,aaE F
boolaaG K
pushaaL P
=aaQ R
falseaaS X
)aaX Y
{bb 	
returncc 
basecc 
.cc 
PushTrackAsynccc &
(cc& '
trackcc' ,
,cc, -
pushcc. 2
)cc2 3
;cc3 4
}dd 	
publicff 
overrideff 
asyncff 
Taskff "
	SkipAsyncff# ,
(ff, -
intff- 0
countff1 6
=ff7 8
$numff9 :
)ff: ;
{gg 	
awaithh 
basehh 
.hh 
	SkipAsynchh  
(hh  !
counthh! &
)hh& '
;hh' (
awaitii 
socketTextchannelii #
.ii# $
SendMessageAsyncii$ 4
(ii4 5
$"ii5 7
Skipped ii7 ?
{ii? @
countii@ E
}iiE F
 song.iiF L
"iiL M
)iiM N
;iiN O
}jj 	
publicll 
overridell 
asyncll 
Taskll "
	StopAsyncll# ,
(ll, -
boolll- 1

disconnectll2 <
=ll= >
falsell? D
)llD E
{mm 	
awaitnn 
basenn 
.nn 
	StopAsyncnn  
(nn  !

disconnectnn! +
)nn+ ,
;nn, -
awaitoo 
socketTextchanneloo #
.oo# $
SendMessageAsyncoo$ 4
(oo4 5
$"oo5 7
Stopped the music.oo7 I
"ooI J
)ooJ K
;ooK L
}pp 	
}qq 
}rr 