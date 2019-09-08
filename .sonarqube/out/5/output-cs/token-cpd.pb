ü
tC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Migrations\BaseMigration01082019.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
GiveAway "
." #

Migrations# -
{ 
[ 
	Migration 
( 
$num 
) 
] 
public 

class !
BaseMigration01082019 &
:' (
	Migration) 2
{ 
public 
override 
void 
Down !
(! "
)" #
{		 	
Delete

 
.

 
Table

 
(

 
$str

 $
)

$ %
;

% &
} 	
public 
override 
void 
Up 
(  
)  !
{ 	
Create 
. 
Table 
( 
$str $
)$ %
. 

WithColumn 
( 
$str  
)  !
.! "
AsInt16" )
() *
)* +
.+ ,
Identity, 4
(4 5
)5 6
.6 7

PrimaryKey7 A
(A B
)B C
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str %
)% &
.& '
AsInt64' .
(. /
)/ 0
. 

WithColumn 
( 
$str #
)# $
.$ %
AsString% -
(- .
). /
;/ 0
} 	
} 
} ∫
jC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Modules\GiveAwayModule.cs
	namespace		 	
HeroBot		
 
.		 
Plugins		 
.		 
GiveAway		 "
.		" #
Modules		# *
{

 
[ 

NeedPlugin 
] 
public 

class 
GiveAwayModule 
:  !

ModuleBase" ,
<, - 
SocketCommandContext- A
>A B
{ 
private 
readonly 
GiveAwayService (
_service) 1
;1 2
public 
GiveAwayModule 
( 
GiveAway &
.& '
Services' /
./ 0
GiveAwayService0 ?
_giveAwayService@ P
)P Q
{R S
_service 
= 
_giveAwayService '
;' (
} 	
[ 	!
RequireUserPermission	 
( 
GuildPermission .
.. /
ManageChannels/ =
)= >
,> ? 
RequireBotPermission  
(  !
GuildPermission! 0
.0 1
Administrator1 >
)> ?
,? @
Command 
( 
$str 
) 
]  
public 
async 
Task 
GiveAway "
(" #
ITextChannel# /
channel0 7
,7 8
TimeSpan9 A
timeB F
,F G
intG J
winnersK R
,R S
[T U
	RemainderU ^
]^ _
String_ e
pricef k
)k l
{m n
var 
embed 
= 
new 
EmbedBuilder (
(( )
)) *
{+ ,
Description 
= 
$"  
{  !
time! %
.% &
ToHumanReadable& 5
(5 6
)6 7
}7 8
 remaining. 8 D
{D E
winnersE L
}L M
	 winners.M V
"V W
,W X
Title 
= 
price 
} 
. 
WithCopyrightFooter !
(! "
)" #
.# $
WithRandomColor$ 3
(3 4
)4 5
;5 6
var 
message 
= 
await 
channel  '
.' (
SendMessageAsync( 8
(8 9
embed9 >
:> ?
embed@ E
.E F
BuildF K
(K L
)L M
)M N
;N O
await 
message 
. 
AddReactionAsync *
(* +
new+ .
Emoji/ 4
(4 5
$str5 9
)9 :
): ;
;; <
await 
_service 
. 
CreateGiveaway )
() *
channel* 1
,1 2
message3 :
,: ;
time; ?
,? @
price@ E
,E F
winnersF M
)M N
;N O
} 	
}   
}!! ˛
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\PluginRefferal.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
GiveAway "
{ 
public 

class 
PluginRefferal 
:  !
IPluginRefferal" 1
{ 
} 
} Â]
lC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Plugins.GiveAway\Services\GiveAwayService.cs
	namespace 	
HeroBot
 
. 
Plugins 
. 
GiveAway "
." #
Services# +
{ 
[ 
Service 
] 
public 

class 
GiveAwayService  
{ 
private 
readonly 
IDatabaseService )
_sqlDatabase* 6
;6 7
private 
readonly 
Random 
_random  '
;' (
private 
readonly 
IRedisService &
_redisDatabase' 5
;5 6
private 
readonly 
ISubscriber $
_sub% )
;) *
private 
readonly  
DiscordShardedClient -
_discord. 6
;6 7
private 
readonly 
Timer 
_timer %
;% &
private 
readonly 
static 
string  &
GetGiveawayById' 6
=7 8
$str9 g
;g h
private 
readonly 
static 
string  &
Creategiveaway' 5
=6 7
$str	8 ø
;
ø ¿
public 
GiveAwayService 
( 
IDatabaseService /
databaseService0 ?
,? @
Random@ F
randomG M
,M N
IRedisServiceO \
redisService] i
,i j
IConfigurationRootk }
o~ 
,	 Ä"
DiscordShardedClient
Å ï"
discordShardedClient
ñ ™
)
™ ´
{ 	
_sqlDatabase 
= 
databaseService *
;* +
_random 
= 
random 
; 
_redisDatabase   
=   
redisService   )
;  ) *
_sub!! 
=!! 
_redisDatabase!! !
.!!! "
GetSubscriber!!" /
(!!/ 0
)!!0 1
;!!1 2
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
;%%e f
}&& 	
private(( 
void(( 
Update(( 
((( 
object(( "
sender((# )
,(() *
ElapsedEventArgs((+ ;
e((< =
)((= >
{)) 	
}++ 	
private-- 
void-- 
OnRedisEvent-- !
(--! "
RedisChannel--" .
arg1--/ 3
,--3 4

RedisValue--5 ?
arg2--@ D
)--D E
{.. 	
var// 
name// 
=// 
arg2// 
.//  
ToString//  (
(//( )
)//) *
.//* +
Split//+ 0
(//0 1
$char//1 4
)//4 5
;//5 6
if00 
(00 
name00 
.00 
Length00 
==00  "
$num00# $
&&00% '
name00( ,
[00, -
$num00- .
]00. /
==000 2
$str003 =
)00= >
{11 
var22 
id22 
=22 
long22 !
.22! "
Parse22" '
(22' (
name22( ,
[22, -
$num22- .
]22. /
)22/ 0
;220 1
var44 

connection44 "
=44# $
_sqlDatabase44% 1
.441 2
GetDbConnection442 A
(44A B
)44B C
;44C D

connection66 
.66 

QueryAsync66 )
(66) *
GetGiveawayById66* 9
,669 :
new66; >
{66? @
id66A C
}66D E
)66E F
.66F G
ContinueWith66G S
(66S T
async66T Y
(66Z [
task66[ _
)66_ `
=>66a c
{77 
try88 
{99 
var;; 
result;;  &
=;;' (
task;;) -
.;;- .
Result;;. 4
;;;4 5
if== 
(==  
result==  &
.==& '
Any==' *
(==* +
)==+ ,
)==, -
{>> 
var@@  #
giveaway@@$ ,
=@@- .
result@@/ 5
.@@5 6
First@@6 ;
(@@; <
)@@< =
;@@= >
SocketTextChannelBB  1
channelBB2 9
=BB: ;
(BB< =
SocketTextChannelBB= N
)BBN O
_discordBBO W
.BBW X

GetChannelBBX b
(BBb c
(BBc d
ulongBBd i
)BBi j
giveawayBBj r
.BBr s
channelBBs z
)BBz {
;BB{ |
ifDD  "
(DD# $
channelDD$ +
!=DD, .
nullDD/ 3
)DD3 4
{EE  !
IUserMessageGG$ 0
messageGG1 8
=GG9 :
(GG; <
IUserMessageGG< H
)GGH I
awaitGGI N
channelGGO V
.GGV W
GetMessageAsyncGGW f
(GGf g
(GGg h
ulongGGh m
)GGm n
giveawayGGn v
.GGv w
messageGGw ~
)GG~ 
;	GG Ä
ifHH$ &
(HH' (
messageHH( /
!=HH0 2
nullHH3 7
)HH7 8
{II$ %
varJJ( +
emoteJJ, 1
=JJ2 3
newJJ4 7
EmojiJJ8 =
(JJ= >
$strJJ> B
)JJB C
;JJC D
varKK( +
	reactionsKK, 5
=KK6 7
awaitKK8 =
messageKK> E
.KKE F!
GetReactionUsersAsyncKKF [
(KK[ \
emoteKK\ a
,KKa b
$numKKc g
)KKg h
.KKh i
FlattenAsyncKKi u
(KKu v
)KKv w
;KKw x
varLL( +
winLL, /
=LL0 1
newLL2 5
ListLL6 :
<LL: ;
IUserLL; @
>LL@ A
(LLA B
)LLB C
;LLC D
varMM( +
countMM, 1
=MM2 3
	reactionsMM4 =
.MM= >
CountMM> C
(MMC D
)MMD E
-MMF G
	reactionsMMH Q
.MMQ R
CountMMR W
(MMW X
xMMX Y
=>MMZ \
xMM] ^
.MM^ _
IsBotMM_ d
)MMd e
;MMe f
varNN( +
wCNN, .
=NN/ 0
giveawayNN1 9
.NN9 :
winnersNN: A
;NNA B
ifOO( *
(OO+ ,
wCOO, .
>OO/ 0
countOO1 6
)OO6 7
wCPP, .
=PP/ 0
countPP1 6
;PP6 7
whileQQ( -
(QQ. /
wCQQ/ 1
!=QQ2 4
$numQQ5 6
)QQ6 7
{RR( )
varSS, /
winnerSS0 6
=SS7 8
	reactionsSS9 B
.SSB C
	ElementAtSSC L
(SSL M
_randomSSM T
.SST U
NextSSU Y
(SSY Z
countSSZ _
)SS_ `
)SS` a
;SSa b
whileTT, 1
(TT2 3
winTT3 6
.TT6 7
AnyTT7 :
(TT: ;
xTT; <
=>TT= ?
xTT@ A
.TTA B
IdTTB D
==TTE G
winnerTTH N
.TTN O
IdTTO Q
)TTR S
||TTT V
winnerTTW ]
.TT] ^
IsBotTT^ c
)TTc d
winnerUU0 6
=UU7 8
	reactionsUU9 B
.UUB C
	ElementAtUUC L
(UUL M
_randomUUM T
.UUT U
NextUUU Y
(UUY Z
countUUZ _
)UU_ `
)UU` a
;UUa b
winVV, /
.VV/ 0
AddVV0 3
(VV3 4
winnerVV4 :
)VV: ;
;VV; <
wCWW, .
--WW. 0
;WW0 1
}XX( )
awaitYY( -
channelYY. 5
.YY5 6
SendMessageAsyncYY6 F
(YYF G
stringYYG M
.YYM N
JoinYYN R
(YYR S
$strYYS V
,YYV W
winYYX [
.YY[ \
SelectYY\ b
(YYb c
xYYc d
=>YYe g
xYYh i
.YYi j
MentionYYj q
)YYq r
)YYr s
+YYt u
$strYYv ~
+	YY Ä
giveaway
YYÅ â
.
YYâ ä
price
YYä è
+
YYê ë
$str
YYí ï
)
YYï ñ
;
YYñ ó
awaitZZ( -
messageZZ. 5
.ZZ5 6
ModifyAsyncZZ6 A
(ZZA B
xZZB C
=>ZZD F
xZZG H
.ZZH I
EmbedZZI N
=ZZO P
messageZZQ X
.ZZX Y
EmbedsZZY _
.ZZ_ `
FirstZZ` e
(ZZe f
)ZZf g
.ZZg h
ToEmbedBuilderZZh v
(ZZv w
)ZZw x
.ZZx y
WithDescription	ZZy à
(
ZZà â
$"
ZZâ ã
Finished ! 
ZZã ñ
{
ZZñ ó
string
ZZó ù
.
ZZù û
Join
ZZû ¢
(
ZZ¢ £
$str
ZZ£ ¶
,
ZZ¶ ß
win
ZZ® ´
.
ZZ´ ¨
Select
ZZ¨ ≤
(
ZZ≤ ≥
x
ZZ≥ ¥
=>
ZZµ ∑
x
ZZ∏ π
.
ZZπ ∫
Mention
ZZ∫ ¡
)
ZZ¡ ¬
)
ZZ¬ √
}
ZZ√ ƒ
"
ZZƒ ≈
)
ZZ≈ ∆
.
ZZ∆ «
Build
ZZ« Ã
(
ZZÃ Õ
)
ZZÕ Œ
)
ZZŒ œ
;
ZZœ –
}[[$ %
}\\  !
}]] 
}^^ 
catch__ 
(__ 
	Exception__ (
)__( )
{__* +
}__T U
}`` 
)`` 
;`` 
}bb 
}cc 	
publicee 
asyncee 
Taskee 
CreateGiveawayee (
(ee( )
IGuildChannelee) 6
channelee7 >
,ee> ?
IMessageee? G
messageeeH O
,eeO P
TimeSpaneeP X
timeeeY ]
,ee] ^
stringee^ d
priceeee j
,eej k
inteek n
winnerseeo v
)eev w
{ff 	
vargg 

connectiongg 
=gg 
_sqlDatabasegg )
.gg) *
GetDbConnectiongg* 9
(gg9 :
)gg: ;
;gg; <
varhh 
idhh 
=hh 
(hh 
longhh 
)hh 
awaithh  

connectionhh! +
.hh+ ,
ExecuteScalarAsynchh, >
(hh> ?
Creategiveawayhh? M
,hhM N
newhhO R
{hhS T
winnershhU \
,hh\ ]
pricehh^ c
,hhc d
messagehhe l
=hhm n
(hho p
longhhp t
)hht u
messagehhu |
.hh| }
Idhh} 
,	hh Ä
channel
hhÅ à
=
hhâ ä
(
hhã å
long
hhå ê
)
hhê ë
channel
hhë ò
.
hhò ô
Id
hhô õ
}
hhú ù
)
hhù û
;
hhû ü
awaitii 
_redisDatabaseii  
.ii  !
GetDatabaseii! ,
(ii, -
)ii- .
.ii. /
StringSetAsyncii/ =
(ii= >
$"ii> @
	giveaway:ii@ I
{iiI J
idiiJ L
}iiL M
"iiM N
,iiN O
StringiiP V
.iiV W
EmptyiiW \
,ii\ ]
timeii] a
)iia b
;iib c
}jj 	
publicll 
asyncll 
Taskll 
FinishGiveawayll (
(ll( )
)ll) *
{ll+ ,
}nn 	
}oo 
}pp 