¥-
gC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Helpers\ServiceCollectionHelper.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Helpers 
{ 
public 

static 
class #
ServiceCollectionHelper /
{ 
public 
static 
IServiceCollection (1
%LoadAllServicesFromExternalAssembiles) N
(N O
thisO S
IServiceCollectionT f
servicesg o
)o p
{ 	
var #
serviceProviderMigrateg '
=( )!
CreateserviceMigrator* ?
(? @
typeof@ F
(F G
Migration1270720191G Z
)Z [
.[ \
Assembly\ d
)d e
;e f
using 
( 
var 
scope 
= #
serviceProviderMigrateg 6
.6 7
CreateScope7 B
(B C
)C D
)D E
{ 
(   
scope   
as   
IServiceProvider   *
)  * +
.  + ,
GetRequiredService  , >
<  > ?
IMigrationRunner  ? O
>  O P
(  P Q
)  Q R
.  R S
	MigrateUp  S \
(  \ ]
)  ] ^
;  ^ _
}!! 
ModulesService## 
.## &
LoadAssembliesInDirrectory## 5
(##5 6
)##6 7
;##7 8
List$$ 
<$$ 
AssemblyEntity$$ 
>$$  
ass$$! $
=$$% &
RuntimeAssemblies$$' 8
.$$8 9
asseblyEntities$$9 H
.$$H I
Values$$I O
.$$O P
ToList$$P V
($$V W
)$$W X
;$$X Y
foreach&& 
(&& 
AssemblyEntity&& #
assembly&&$ ,
in&&- /
ass&&0 3
)&&3 4
{'' 
var(( 
assm(( 
=(( 
assembly(( #
.((# $
Assembly(($ ,
;((, -
try)) 
{** 
var,, "
serviceProviderMigrate,, .
=,,/ 0!
CreateserviceMigrator,,1 F
(,,F G
assm,,G K
),,K L
;,,L M
using.. 
(.. 
var.. 
scope.. $
=..% &"
serviceProviderMigrate..' =
...= >
CreateScope..> I
(..I J
)..J K
)..K L
{// 
(00 
scope00 
as00 !
IServiceProvider00" 2
)002 3
.003 4
GetRequiredService004 F
<00F G
IMigrationRunner00G W
>00W X
(00X Y
)00Y Z
.00Z [
	MigrateUp00[ d
(00d e
)00e f
;00f g
}11 
}22 
catch33 
{33 
}33J K
IEnumerable44 
<44 
TypeInfo44 $
>44$ %
servicesAss44& 1
=442 3
assm444 8
.448 9
DefinedTypes449 E
.44E F
Where44F K
(44K L
x44L M
=>44N P
!44Q R
x44R S
.44S T
IsInterface44T _
&&44` b
!44c d
x44d e
.44e f
IsEnum44f l
&&44m o
x44p q
.44q r
IsClass44r y
&&44z |
x44} ~
.44~ 
IsPublic	44 á
&&
44à ä
x
44ã å
.
44å ç
Name
44ç ë
.
44ë í
Contains
44í ö
(
44ö õ
$str
44õ §
)
44§ •
)
44• ¶
;
44¶ ß
foreach55 
(55 
TypeInfo55 $
typeInfo55% -
in55. 0
servicesAss551 <
)55< =
services66  
.66  !
AddSingleton66! -
(66- .
assm66. 2
.662 3
GetTypes663 ;
(66; <
)66< =
.66= >
FirstOrDefault66> L
(66L M
x66M N
=>66O Q
x66R S
.66S T
Name66T X
==66Y [
typeInfo66\ d
.66d e
Name66e i
)66i j
)66j k
;66k l
}88 
return99 
services99 
;99 
}:: 	
private@@ 
static@@ 
ServiceProvider@@ &!
CreateserviceMigrator@@' <
(@@< =
Assembly@@= E
assembly@@F N
)@@N O
{AA 	
varBB 
collBB 
=BB 
newBB 
ServiceCollectionBB ,
(BB, -
)BB- .
;BB. /
collCC 
.CC !
AddFluentMigratorCoreCC &
(CC& '
)CC' (
.CC( )
ConfigureRunnerCC) 8
(CC8 9
(CC9 :
xCC: ;
)CC; <
=>CC= ?
{DD 
xEE 
.EE 
AddPostgresEE 
(EE 
)EE 
.FF &
WithGlobalConnectionStringFF +
(FF+ ,
$"FF, .<
0Server=ssh.alivecreation.fr;Port=26257;Database=FF. ^
{FF^ _
assemblyFF_ g
.FFg h
GetNameFFh o
(FFo p
)FFp q
.FFq r
NameFFr v
}FFv wg
Z;User Id=matthieu;Password=4d9*jC%(hu\"ecN2&;SslMode=Require;Trust Server Certificate=true	FFw —
"
FF— “
)
FF“ ”
.GG 
ScanInGG 
(GG 
assemblyGG  
)GG  !
.GG! "
ForGG" %
.GG% &

MigrationsGG& 0
(GG0 1
)GG1 2
;GG2 3
}HH 
)HH 
;HH 
returnII 
collII 
.II  
BuildServiceProviderII ,
(II, -
)II- .
;II. /
}JJ 	
}KK 
}MM Â
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Helpers\ServiceProviderHelper.cs
	namespace

 	
HeroBot


 
.

 
Core

 
.

 
Helpers

 
{ 
public 

static 
class !
ServiceProviderHelper -
{ 
public 
static 
ServiceProvider %0
$GetAllServicesFromExternalAssemblies& J
(J K
thisK O
ServiceProviderP _
services` h
)h i
{ 	
foreach 
( 
AssemblyEntity #
d$ %
in& (
RuntimeAssemblies) :
.: ;
asseblyEntities; J
.J K
ValuesK Q
)Q R
{ 
Assembly 
ass 
= 
d  
.  !
Assembly! )
;) *
IEnumerable 
< 
TypeInfo $
>$ %
servicesAss& 1
=2 3
ass4 7
.7 8
DefinedTypes8 D
.D E
WhereE J
(J K
xK L
=>M O
!P Q
xQ R
.R S
IsInterfaceS ^
&&_ a
!b c
xc d
.d e
IsEnume k
&&l n
xo p
.p q
IsClassq x
&&y {
x| }
.} ~
IsPublic	~ Ü
&&
á â
x
ä ã
.
ã å
Name
å ê
.
ê ë
Contains
ë ô
(
ô ö
$str
ö £
)
£ §
)
§ •
;
• ¶
if 
( 
servicesAss 
.  
Any  #
(# $
)$ %
)% &
{ 
foreach 
( 
TypeInfo %
typeInfo& .
in/ 1
servicesAss2 =
)= >
{ 
services  
.  !
GetRequiredService! 3
(3 4
ass4 7
.7 8
GetTypes8 @
(@ A
)A B
.B C
FirstOrDefaultC Q
(Q R
xR S
=>T V
xW X
.X Y
NameY ]
==^ `
typeInfoa i
.i j
Namej n
)n o
)o p
;p q
} 
} 
}   
return!! 
services!! 
;!! 
}"" 	
}## 
}$$ ì
gC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Migrations\Migration1_270720191.cs
	namespace 	
HeroBot
 
. 
Core 
. 

Migrations !
{ 
[ 
	Migration 
( 
$num 
) 
] 
public		 

class		 
Migration1270720191		 $
:		% &
	Migration		' 0
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
$str &
)& '
;' (
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
$str &
)& '
. 

WithColumn 
( 
$str  
)  !
.! "
AsInt32" )
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
$str $
)$ %
.% &
AsString& .
(. /
)/ 0
. 

WithColumn 
( 
$str #
)# $
.$ %
AsInt64% ,
(, -
)- .
. 

WithColumn 
( 
$str $
)$ %
.% &
AsString& .
(. /
)/ 0
.0 1
Nullable1 9
(9 :
): ;
;; <
} 	
} 
} “
OC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Program.cs
	namespace 	
	HeroBotv2
 
{ 
static 

class 
Program 
{ 
public 
static 
Task 
Main 
(  
)  !
=>		 
Startup		 
.		 
RunAsync		 
(		  
)		  !
;		! "
}

 
} ﬁ
YC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\RuntimeAssemblies.cs
	namespace 	
HeroBot
 
. 
Core 
{ 
public 

class 
RuntimeAssemblies "
{		 
public

 
static

 

Dictionary

  
<

  !
String

! '
,

' (
AssemblyEntity

( 6
>

6 7
asseblyEntities

8 G
{

H I
get

J M
;

M N
}

O P
=

Q R
new

S V

Dictionary

W a
<

a b
string

b h
,

h i
AssemblyEntity

j x
>

x y
(

y z
)

z {
;

{ |
} 
} ®L
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\CommandHandler.cs
	namespace 	
	HeroBotv2
 
. 
Services 
{ 
public 

class 
CommandHandler 
{ 
private 
readonly  
DiscordShardedClient -
_discord. 6
;6 7
private 
readonly 
CommandService '
	_commands( 1
;1 2
private 
readonly 
IConfigurationRoot +
_config, 3
;3 4
private 
readonly 
IServiceProvider )
	_provider* 3
;3 4
private 
readonly 
CooldownService (
	_cooldown) 2
;2 3
public 
CommandHandler 
(  
DiscordShardedClient  
discord! (
,( )
CommandService 
commands #
,# $
IConfigurationRoot 
config %
,% &
IServiceProvider 
provider %
,% &
CooldownService 
cooldownService +
)+ ,
{ 	
	_cooldown 
= 
cooldownService '
;' (
_discord 
= 
discord 
; 
	_commands   
=   
commands    
;    !
_config!! 
=!! 
config!! 
;!! 
	_provider"" 
="" 
provider""  
;""  !
_discord$$ 
.$$ 
MessageReceived$$ $
+=$$% '"
OnMessageReceivedAsync$$( >
;$$> ?
_discord%% 
.%% 
MessageUpdated%% #
+=%%$ &
OnMessageUpdated%%' 7
;%%7 8
}&& 	
private(( 
Task(( 
OnMessageUpdated(( %
(((% &
	Cacheable((& /
<((/ 0
IMessage((0 8
,((8 9
ulong((: ?
>((? @

oldMessage((A K
,((K L
SocketMessage((M Z

newMessage(([ e
,((e f!
ISocketMessageChannel((g |
messageChannel	((} ã
)
((ã å
{)) 	
return** "
OnMessageReceivedAsync** )
(**) *

newMessage*** 4
)**4 5
;**5 6
}++ 	
private-- 
async-- 
Task-- "
OnMessageReceivedAsync-- 1
(--1 2
SocketMessage--2 ?
s--@ A
)--A B
{.. 	
var// 
msg// 
=// 
s// 
as// 
SocketUserMessage// ,
;//, -
if00 
(00 
msg00 
==00 
null00 
)00 
return00 #
;00# $
if11 
(11 
msg11 
.11 
Author11 
.11 
Id11 
==11  
_discord11! )
.11) *
CurrentUser11* 5
.115 6
Id116 8
||119 ;
msg11< ?
.11? @
Author11@ F
.11F G
IsBot11G L
)11L M
return11N T
;11T U
if22 
(22 
!22 
(22 
msg22 
.22 
Channel22 
is22  
SocketGuildChannel22! 3
)223 4
)224 5
return226 <
;22< =
var33 
context33 
=33 
new33  
SocketCommandContext33 2
(332 3
_discord333 ;
.33; <
GetShardFor33< G
(33G H
(33H I
msg33I L
.33L M
Channel33M T
as33U W
SocketGuildChannel33X j
)33j k
.33k l
Guild33l q
)33q r
,33r s
msg33t w
)33w x
;33x y
int44 
argPos44 
=44 
$num44 
;44 
if55 
(55 
msg55 
.55 
HasStringPrefix55 #
(55# $
_config55$ +
[55+ ,
$str55, 4
]554 5
,555 6
ref557 :
argPos55; A
)55A B
||55C E
msg55F I
.55I J
HasMentionPrefix55J Z
(55Z [
_discord55[ c
.55c d
CurrentUser55d o
,55o p
ref55q t
argPos55u {
)55{ |
)55| }
{66 
var77 
command77 
=77 
	_commands77 '
.77' (
Search77( .
(77. /
context77/ 6
,776 7
argPos778 >
)77> ?
;77? @
if88 
(88 
command88 
.88 
	IsSuccess88 %
)88% &
{88' (
var:: 
cmd:: 
=:: 
command:: %
.::% &
Commands::& .
.::. /
First::/ 4
(::4 5
)::5 6
.::6 7
Command::7 >
;::> ?
var;; 
result;; 
=;;  
await;;! &
	_commands;;' 0
.;;0 1
ExecuteAsync;;1 =
(;;= >
context;;> E
,;;E F
argPos;;G M
,;;M N
	_provider;;O X
);;X Y
;;;Y Z
if<< 
(<< 
!<< 
result<< 
.<<  
	IsSuccess<<  )
)<<) *
{== 
switch>> 
(>>  
result>>  &
.>>& '
Error>>' ,
)>>, -
{?? 
case@@  
CommandError@@! -
.@@- .
BadArgCount@@. 9
:@@9 :
awaitAA  %
sAA& '
.AA' (
ChannelAA( /
.AA/ 0
SendMessageAsyncAA0 @
(AA@ A
$"AAA CL
?Oops, it look like you made a syntax error in your command :/ `	AAC Ç
{
AAÇ É
_config
AAÉ ä
[
AAä ã
$str
AAã ì
]
AAì î
}
AAî ï
{
AAï ñ
(
AAñ ó
cmd
AAó ö
.
AAö õ
Aliases
AAõ ¢
.
AA¢ £
Count
AA£ ®
>
AA© ™
$num
AA´ ¨
?
AA≠ Æ
$"
AAØ ±
{
AA± ≤
$str
AA≤ µ
}
AAµ ∂
{
AA∂ ∑
string
AA∑ Ω
.
AAΩ æ
Join
AAæ ¬
(
AA¬ √
$str
AA√ ∆
,
AA∆ «
cmd
AA» À
.
AAÀ Ã
Aliases
AAÃ ”
)
AA” ‘
}
AA‘ ’
{
AA’ ÷
$str
AA÷ Ÿ
}
AAŸ ⁄
"
AA⁄ €
:
AA‹ ›
cmd
AAﬁ ·
.
AA· ‚
Name
AA‚ Ê
)
AAÊ Á
}
AAÁ Ë
{
AAÈ Í
string
AAÍ 
.
AA Ò
Join
AAÒ ı
(
AAı ˆ
$str
AAˆ ˘
,
AA˘ ˙
cmd
AA˚ ˛
.
AA˛ ˇ

Parameters
AAˇ â
.
AAâ ä
Select
AAä ê
(
AAê ë
p
AAë í
=>
AAì ï
p
AAñ ó
.
AAó ò

IsOptional
AAò ¢
?
AA£ §
$"
AA• ß
[
AAß ®
{
AA® ©
p
AA© ™
.
AA™ ´
Name
AA´ Ø
}
AAØ ∞
{
AA∞ ±
(
AA± ≤
p
AA≤ ≥
.
AA≥ ¥
IsRemainder
AA¥ ø
?
AA¿ ¡
$str
AA¬ «
:
AA» …
String
AA  –
.
AA– —
Empty
AA— ÷
)
AA÷ ◊
}
AA◊ ÿ
]
AAÿ Ÿ
"
AAŸ ⁄
:
AA€ ‹
$"
AA› ﬂ
<
AAﬂ ‡
{
AA‡ ·
p
AA· ‚
.
AA‚ „
Name
AA„ Á
}
AAÁ Ë
{
AAË È
(
AAÈ Í
p
AAÍ Î
.
AAÎ Ï
IsRemainder
AAÏ ˜
?
AA¯ ˘
$str
AA˙ ˇ
:
AAÄ Å
String
AAÇ à
.
AAà â
Empty
AAâ é
)
AAé è
}
AAè ê
>
AAê ë
"
AAë í
)
AAí ì
)
AAì î
}
AAî ï
`
AAï ñ
"
AAñ ó
)
AAó ò
;
AAò ô
breakBB  %
;BB% &
caseCC  
CommandErrorCC! -
.CC- .
ParseFailedCC. 9
:CC9 :
awaitDD  %
sDD& '
.DD' (
ChannelDD( /
.DD/ 0
SendMessageAsyncDD0 @
(DD@ A
$strDDA s
)DDs t
;DDt u
breakEE  %
;EE% &
caseFF  
CommandErrorFF! -
.FF- .
UnmetPreconditionFF. ?
:FF? @
awaitGG  %
sGG& '
.GG' (
ChannelGG( /
.GG/ 0
SendMessageAsyncGG0 @
(GG@ A
resultGGA G
.GGG H
ErrorReasonGGH S
)GGS T
;GGT U
breakHH  %
;HH% &
caseII  
CommandErrorII! -
.II- .
	ExceptionII. 7
:II7 8
awaitJJ  %
sJJ& '
.JJ' (
ChannelJJ( /
.JJ/ 0
SendMessageAsyncJJ0 @
(JJ@ A
$strJJA e
)JJe f
;JJf g
breakKK  %
;KK% &
defaultLL #
:LL# $
awaitMM  %
sMM& '
.MM' (
ChannelMM( /
.MM/ 0
SendMessageAsyncMM0 @
(MM@ A
$"MMA C
Unknown error `MMC R
{MMR S
resultMMS Y
}MMY Z
`MMZ [
"MM[ \
)MM\ ]
;MM] ^
breakNN  %
;NN% &
}OO 
}QQ 
elseRR 
awaitSS 
	_cooldownSS '
.SS' (
	OnCommandSS( 1
(SS1 2
commandSS2 9
.SS9 :
CommandsSS: B
.SSB C
FirstSSC H
(SSH I
)SSI J
.SSJ K
CommandSSK R
,SSR S
contextSST [
)SS[ \
;SS\ ]
}TT 
}UU 
}VV 	
}WW 
}XX ˚.
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\CooldownService.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Services 
{		 
public

 

class

 
CooldownService

  
:

! "
ICooldownService

# 3
{ 
private 
readonly 
IRedisService &
_redis' -
;- .
public 
CooldownService 
( 
IRedisService ,
redisService- 9
)9 :
{; <
CooldownAttribute 
. 
	_cooldown '
=( )
this* .
;. /
_redis 
= 
redisService !
;! "
} 	
public 
async 
Task 
< 
TimeSpan "
?" #
># $
IsCommandCooldowned% 8
(8 9
ulong9 >
userid? E
,E F
stringG M
commandNameN Y
)Y Z
{ 	
return 
( 
await 
_redis  
.  !
GetDatabase! ,
(, -
)- .
.. /$
StringGetWithExpiryAsync/ G
(G H
$"H J
{J K
useridK Q
}Q R
-c-R U
{U V
commandNameV a
}a b
"b c
)c d
)d e
.e f
Expiryf l
;l m
} 	
public 
async 
Task 
< 
TimeSpan "
?" #
># $
IsModuleCooldowned% 7
(7 8
ulong8 =
userid> D
,D E
stringF L

moduleNameM W
)W X
{ 	
return 
( 
await 
_redis $
.$ %
GetDatabase% 0
(0 1
)1 2
.2 3$
StringGetWithExpiryAsync3 K
(K L
$"L N
{N O
useridO U
}U V
-m-V Y
{Y Z

moduleNameZ d
}d e
"e f
)f g
)g h
.h i
Expiryi o
;o p
} 	
internal 
async 
Task 
	OnCommand %
(% &
CommandInfo& 1
commandInfo2 =
,= > 
SocketCommandContext> R
commandContextS a
)a b
{c d
if 
( 
commandInfo 
. 
Preconditions )
.) *
Any* -
(- .
x. /
=>0 2
x3 4
is5 7
CooldownAttribute8 I
)I J
)J K
{L M
var 
seconds 
= 
( 
commandInfo *
.* +
Preconditions+ 8
.8 9
Where9 >
(> ?
x? @
=>A C
xD E
isF H
CooldownAttributeI Z
)Z [
.[ \
OrderByDescending\ m
(m n
xn o
=>p r
(s t
xt u
asv x
CooldownAttribute	y ä
)
ä ã
.
ã å
cooldown
å î
.
î ï
Seconds
ï ú
)
ú ù
.
ù û
First
û £
(
£ §
)
§ •
as
¶ ®
CooldownAttribute
© ∫
)
∫ ª
.
ª º
cooldown
º ƒ
;
ƒ ≈
await   
_redis   
.   
GetDatabase   (
(  ( )
)  ) *
.  * +
StringSetAsync  + 9
(  9 :
$"  : <
{  < =
commandContext  = K
.  K L
User  L P
.  P Q
Id  Q S
}  S T
-c-  T W
{  W X
commandInfo  X c
.  c d
Name  d h
}  h i
"  i j
,  j k
String  l r
.  r s
Empty  s x
,  x y
seconds	  z Å
)
  Å Ç
;
  Ç É
}!! 
if"" 
("" 
commandInfo"" 
."" 
Module"" "
.""" #
Preconditions""# 0
.""0 1
Any""1 4
(""4 5
x""5 6
=>""7 9
x"": ;
is""< >
CooldownAttribute""? P
)""P Q
)""Q R
{""S T
var## 
seconds## 
=## 
(## 
commandInfo## *
.##* +
Module##+ 1
.##1 2
Preconditions##2 ?
.##? @
Where##@ E
(##E F
x##F G
=>##H J
x##K L
is##M O
CooldownAttribute##P a
)##a b
.##b c
OrderByDescending##c t
(##t u
x##u v
=>##w y
(##z {
x##{ |
as##} 
CooldownAttribute
##Ä ë
)
##ë í
.
##í ì
cooldown
##ì õ
.
##õ ú
Seconds
##ú £
)
##£ §
.
##§ •
First
##• ™
(
##™ ´
)
##´ ¨
as
##≠ Ø
CooldownAttribute
##∞ ¡
)
##¡ ¬
.
##¬ √
cooldown
##√ À
;
##À Ã
await$$ 
_redis$$ 
.$$ 
GetDatabase$$ (
($$( )
)$$) *
.$$* +
StringSetAsync$$+ 9
($$9 :
$"$$: <
{$$< =
commandContext$$= K
.$$K L
User$$L P
.$$P Q
Id$$Q S
}$$S T
-m-$$T W
{$$W X
commandInfo$$X c
.$$c d
Module$$d j
.$$j k
Name$$k o
}$$o p
"$$p q
,$$q r
String$$s y
.$$y z
Empty$$z 
,	$$ Ä
seconds
$$Ä á
)
$$á à
;
$$à â
}%% 
}&& 	
}'' 
}(( ï)
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\HeroBotContext.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Services 
{ 
public 

class 
HeroBotContext 
:  !
IDatabaseService" 2
{ 
private 
readonly 
IConfigurationRoot +
_config, 3
;3 4
public 
HeroBotContext 
( 
IConfigurationRoot 0
confg1 6
)6 7
{ 	
_config 
= 
confg 
; 
} 	
public 
IDbConnection 
GetDbConnection ,
(, -
)- .
{ 	

StackTrace 

stackTrace !
=" #
new$ '

StackTrace( 2
(2 3
)3 4
;4 5

StackFrame 
[ 
] 
stackFrames $
=% &

stackTrace' 1
.1 2
	GetFrames2 ;
(; <
)< =
;= >
var 
assembly 
= 
stackFrames &
[& '
$num' (
]( )
.) *
	GetMethod* 3
(3 4
)4 5
.5 6
DeclaringType6 C
.C D
AssemblyD L
;L M
return 
new 
Npgsql 
. 
NpgsqlConnection .
(. /
$"/ 1
Server=1 8
{8 9
_config9 @
.@ A

GetSectionA K
(K L
$strL V
)V W
.W X

GetSectionX b
(b c
$strc i
)i j
.j k
Valuek p
}p q
;Port=q w
{w x
_configx 
.	 Ä

GetSection
Ä ä
(
ä ã
$str
ã ï
)
ï ñ
.
ñ ó

GetSection
ó °
(
° ¢
$str
¢ ®
)
® ©
.
© ™
Value
™ Ø
}
Ø ∞

;Database=
∞ ∫
{
∫ ª
assembly
ª √
.
√ ƒ
GetName
ƒ À
(
À Ã
)
Ã Õ
.
Õ Œ
Name
Œ “
}
“ ”
	;User Id=
” ‹
{
‹ ›
_config
› ‰
.
‰ Â

GetSection
Â Ô
(
Ô 
$str
 ˙
)
˙ ˚
.
˚ ¸

GetSection
¸ Ü
(
Ü á
$str
á ç
)
ç é
.
é è

GetSection
è ô
(
ô ö
$str
ö †
)
† °
.
° ¢
Value
¢ ß
}
ß ®

;Password=
® ≤
{
≤ ≥
_config
≥ ∫
.
∫ ª

GetSection
ª ≈
(
≈ ∆
$str
∆ –
)
– —
.
— “

GetSection
“ ‹
(
‹ ›
$str
› „
)
„ ‰
.
‰ Â

GetSection
Â Ô
(
Ô 
$str
 ˙
)
˙ ˚
.
˚ ¸
Value
¸ Å
}
Å ÇJ
<;SslMode=Require;Trust Server Certificate=true;Pooling=true;
Ç æ
"
æ ø
)
ø ¿
;
¿ ¡
} 	
public!! 
IDbConnection!! 
GetDbConnection!! ,
(!!, -
string!!- 3
v!!4 5
)!!5 6
{"" 	
return## 
new## 
Npgsql## 
.## 
NpgsqlConnection## .
(##. /
$"##/ 1
Server=##1 8
{##8 9
_config##9 @
.##@ A

GetSection##A K
(##K L
$str##L V
)##V W
.##W X

GetSection##X b
(##b c
$str##c i
)##i j
.##j k
Value##k p
}##p q
;Port=##q w
{##w x
_config##x 
.	## Ä

GetSection
##Ä ä
(
##ä ã
$str
##ã ï
)
##ï ñ
.
##ñ ó

GetSection
##ó °
(
##° ¢
$str
##¢ ®
)
##® ©
.
##© ™
Value
##™ Ø
}
##Ø ∞

;Database=
##∞ ∫
{
##∫ ª
v
##ª º
}
##º Ω
	;User Id=
##Ω ∆
{
##∆ «
_config
##« Œ
.
##Œ œ

GetSection
##œ Ÿ
(
##Ÿ ⁄
$str
##⁄ ‰
)
##‰ Â
.
##Â Ê

GetSection
##Ê 
(
## Ò
$str
##Ò ˜
)
##˜ ¯
.
##¯ ˘

GetSection
##˘ É
(
##É Ñ
$str
##Ñ ä
)
##ä ã
.
##ã å
Value
##å ë
}
##ë í

;Password=
##í ú
{
##ú ù
_config
##ù §
.
##§ •

GetSection
##• Ø
(
##Ø ∞
$str
##∞ ∫
)
##∫ ª
.
##ª º

GetSection
##º ∆
(
##∆ «
$str
##« Õ
)
##Õ Œ
.
##Œ œ

GetSection
##œ Ÿ
(
##Ÿ ⁄
$str
##⁄ ‰
)
##‰ Â
.
##Â Ê
Value
##Ê Î
}
##Î ÏJ
<;SslMode=Require;Trust Server Certificate=true;Pooling=true;
##Ï ®
"
##® ©
)
##© ™
;
##™ ´
}$$ 	
}%% 
}&& æ"
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\LoggingService.cs
	namespace 	
	HeroBotv2
 
. 
Services 
{ 
public 

class 
LoggingService 
{ 
public 
LoggingService 
(  
DiscordShardedClient 2
discord3 :
,: ;
CommandService< J
commandsK S
)S T
{ 	
discord 
. 
Log 
+= 

OnLogAsync %
;% &
commands 
. 
Log 
+= 

OnLogAsync &
;& '
} 	
public 
void 
Log 
( 
LogSeverity #
logSeverity$ /
,/ 0
string1 7
message8 ?
)? @
{A B

StackTrace 

stackTrace !
=" #
new$ '

StackTrace( 2
(2 3
)3 4
;4 5

StackFrame 
[ 
] 
stackFrames $
=% &

stackTrace' 1
.1 2
	GetFrames2 ;
(; <
)< =
;= >
var 
assembly 
= 
stackFrames &
[& '
$num' (
]( )
.) *
	GetMethod* 3
(3 4
)4 5
.5 6
DeclaringType6 C
.C D
AssemblyD L
;L M
DisplayLogAsync 
( 
new 

LogMessage  *
(* +
logSeverity+ 6
,6 7
assembly8 @
.@ A
GetNameA H
(H I
)I J
.J K
NameK O
,O P
messageP W
)W X
)X Y
.Y Z
WaitZ ^
(^ _
)_ `
;` a
} 	
private 
async 
Task 

OnLogAsync %
(% &

LogMessage& 0
msg1 4
)4 5
{   	

StackTrace!! 

stackTrace!! !
=!!" #
new!!$ '

StackTrace!!( 2
(!!2 3
)!!3 4
;!!4 5

StackFrame"" 
["" 
]"" 
stackFrames"" $
=""% &

stackTrace""' 1
.""1 2
	GetFrames""2 ;
(""; <
)""< =
;""= >
var$$ 
assembly$$ 
=$$ 
stackFrames$$ &
[$$& '
$num$$' (
]$$( )
.$$) *
	GetMethod$$* 3
($$3 4
)$$4 5
.$$5 6
DeclaringType$$6 C
.$$C D
Assembly$$D L
;$$L M
await&& 
DisplayLogAsync&& !
(&&! "
new&&" %

LogMessage&&& 0
(&&0 1
msg&&1 4
.&&4 5
Severity&&5 =
,&&= >
assembly&&? G
.&&G H
GetName&&H O
(&&O P
)&&P Q
.&&Q R
Name&&R V
,&&V W
msg&&X [
.&&[ \
Message&&\ c
,&&c d
msg&&e h
.&&h i
	Exception&&i r
)&&r s
)&&s t
;&&t u
}'' 	
private)) 
Task)) 
DisplayLogAsync)) $
())$ %

LogMessage))% /
msg))0 3
)))3 4
{))5 6
string** 
logText** 
=** 
$"** 
{**  
DateTime**  (
.**( )
UtcNow**) /
.**/ 0
ToString**0 8
(**8 9
$str**9 C
)**C D
}**D E
 [**E G
{**G H
msg**H K
.**K L
Severity**L T
}**T U
] **U W
{**W X
msg**X [
.**[ \
Source**\ b
}**b c
: **c e
{**e f
msg**f i
.**i j
	Exception**j s
?**s t
.**t u
ToString**u }
(**} ~
)**~ 
??
**Ä Ç
msg
**É Ü
.
**Ü á
Message
**á é
}
**é è
"
**è ê
;
**ê ë
return++ 
Console++ 
.++ 
Out++ 
.++ 
WriteLineAsync++ -
(++- .
logText++. 5
)++5 6
;++6 7
},, 	
}-- 
}.. ’∞
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\ModulesService.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Services 
{ 
public 

sealed 
class 
ModulesService &
:' (
IModulesService) 8
{ 
private 
readonly 
IServiceProvider )
	_provider* 3
;3 4
private 
readonly 
CommandService '
_commandService( 7
;7 8
private 
readonly 
IRedisService &
_redis' -
;- .
private 
readonly 
IDatabaseService )
	_database* 3
;3 4
private 
readonly 
LoggingService '
_logging( 0
;0 1
private 
readonly 
List 
< 
ToLoadAssembly ,
>, -
toLoadAssemblies. >
=? @
newA D
ListE I
<I J
ToLoadAssemblyJ X
>X Y
(Y Z
)Z [
;[ \
private 
readonly %
SimpleCacheImplementation 2&
_simpleCacheImplementation3 M
;M N
public 
ModulesService 
( %
SimpleCacheImplementation 7%
simpleCacheImplementation8 Q
,Q R
CommandServiceR `
commandServicea o
,o p
IServiceProvider	q Å
provider
Ç ä
,
ä ã
LoggingService
å ö
loggingService
õ ©
,
© ™
IRedisService
´ ∏
redisService
π ≈
,
≈ ∆
IDatabaseService
« ◊
databaseService
ÿ Á
)
Á Ë
{ 	&
_simpleCacheImplementation &
=' (%
simpleCacheImplementation) B
;B C
	_provider   
=   
provider    
;    !
_commandService!! 
=!! 
commandService!! ,
;!!, -
_redis"" 
="" 
redisService"" !
;""! "
	_database## 
=## 
databaseService## '
;##' (#
FetchExternalAssemblies$$ #
($$# $
)$$$ %
;$$% &
}%% 	
internal'' 
static'' 
void'' &
LoadAssembliesInDirrectory'' 7
(''7 8
)''8 9
{(( 	
var)) 
path)) 
=)) 
Path)) 
.)) 
Combine)) #
())# $
	Directory))$ -
.))- .
GetCurrentDirectory)). A
())A B
)))B C
,))C D
$str))E N
)))N O
;))O P
if++ 
(++ 
!++ 
	Directory++ 
.++ 
Exists++ !
(++! "
path++" &
)++& '
)++' (
	Directory,, 
.,, 
CreateDirectory,, )
(,,) *
path,,* .
),,. /
;,,/ 0
var.. 
files.. 
=.. 
	Directory.. !
...! "
EnumerateFiles.." 0
(..0 1
path..1 5
)..5 6
...6 7
Where..7 <
(..< =
x..= >
=>..? A
x..B C
...C D
Contains..D L
(..L M
$str..M ^
)..^ _
).._ `
;..` a
foreach// 
(// 
var// 
file// 
in//  
files//! &
)//& '
{00 
if11 
(11 
!11 
file11 
.11 
Contains11 "
(11" #
$str11# )
)11) *
)11* +
continue22 
;22 
var44 
moduleContext44 !
=44" #
new44$ '
ModuleLoadContext44( 9
(449 :
)44: ;
;44; <
var55 
ass55 
=55 
moduleContext55 '
.55' ( 
LoadFromAssemblyPath55( <
(55< =
file55= A
)55A B
;55B C
RuntimeAssemblies66 !
.66! "
asseblyEntities66" 1
.661 2
TryAdd662 8
(668 9
ass669 <
.66< =
GetName66= D
(66D E
)66E F
.66F G
Name66G K
,66K L
new66M P
AssemblyEntity66Q _
(66_ `
)66` a
{66b c
Assembly66d l
=66m n
ass66o r
,66r s
Context66t {
=66| }
moduleContext	66~ ã
}
66å ç
)
66ç é
;
66é è
}77 
}88 	
private:: 
void:: #
FetchExternalAssemblies:: ,
(::, -
)::- .
{;; 	
foreach<< 
(<< 
ToLoadAssembly<< #
ass<<$ '
in<<( *
toLoadAssemblies<<+ ;
)<<; <
{== 
var>> 
assembly>> 
=>> 
ass>> "
.>>" #
Assembly>># +
;>>+ ,
var?? 
name?? 
=?? 
assembly?? #
.??# $
GetName??$ +
(??+ ,
)??, -
.??- .
Name??. 2
.??2 3
SanitizAssembly??3 B
(??B C
)??C D
;??D E
var@@ 
classesRefferal@@ #
=@@$ %
assembly@@& .
.@@. /
DefinedTypes@@/ ;
.@@; <
Where@@< A
(@@A B
x@@B C
=>@@D F
!@@G H
x@@H I
.@@I J
IsInterface@@J U
&&@@V X
x@@Y Z
.@@Z [
IsPublic@@[ c
&&@@d f
!@@g h
x@@h i
.@@i j

IsAbstract@@j t
&&@@u w
x@@x y
.@@y z
Name@@z ~
==	@@ Å
$str
@@Ç í
)
@@í ì
;
@@ì î
ifAA 
(AA 
classesRefferalAA #
.AA# $
AnyAA$ '
(AA' (
)AA( )
)AA) *
{BB 
varCC 
PluginRefferalCC &
=CC' (
(CC) *
IPluginRefferalCC* 9
)CC9 :
classesRefferalCC: I
.CCI J
FirstCCJ O
(CCO P
)CCP Q
.CCQ R
GetConstructorsCCR a
(CCa b
)CCb c
.CCc d
FirstCCd i
(CCi j
)CCj k
.CCk l
InvokeCCl r
(CCr s
newCCs v
objectCCw }
[CC} ~
]CC~ 
{
CCÄ Å
}
CCÇ É
)
CCÉ Ñ
;
CCÑ Ö
varDD 
AssemblyEntityDD &
=DD' (
newDD) ,
AssemblyEntityDD- ;
(DD; <
)DD< =
{EE 
AssemblyFF  
=FF! "
assemblyFF# +
,FF+ ,
ContextGG 
=GG  !
assGG" %
.GG% &
assemblycontextGG& 5
,GG5 6
ModuleHH 
=HH  
newHH! $
ListHH% )
<HH) *

ModuleInfoHH* 4
>HH4 5
(HH5 6
)HH6 7
,HH7 8
NameII 
=II 
nameII #
,II# $
pluginRefferalJJ &
=JJ' (
PluginRefferalJJ) 7
}KK 
;KK 
ifLL 
(LL 
RuntimeAssembliesLL )
.LL) *
asseblyEntitiesLL* 9
.LL9 :
AnyLL: =
(LL= >
xLL> ?
=>LL@ B
xLLC D
.LLD E
ValueLLE J
.LLJ K
NameLLK O
==LLP R
nameLLS W
)LLW X
)LLX Y
continueMM  
;MM  !
RuntimeAssembliesNN %
.NN% &
asseblyEntitiesNN& 5
.NN5 6
TryAddNN6 <
(NN< =
nameNN= A
,NNA B
AssemblyEntityNNC Q
)NNQ R
;NNR S
_loggingOO 
.OO 
LogOO  
(OO  !
LogSeverityOO! ,
.OO, -
InfoOO- 1
,OO1 2
$"OO3 5
Loaded OO5 <
{OO< =
nameOO= A
}OOA B
 vOOB D
{OOD E
assOOE H
.OOH I
AssemblyOOI Q
.OOQ R
GetNameOOR Y
(OOY Z
)OOZ [
.OO[ \
VersionOO\ c
}OOc d
 assembly modules.OOd v
"OOv w
)OOw x
;OOx y
ifPP 
(PP 
!PP 
RuntimeAssembliesPP *
.PP* +
asseblyEntitiesPP+ :
.PP: ;
AnyPP; >
(PP> ?
)PP? @
)PP@ A
breakQQ 
;QQ 
}RR 
}SS 
}TT 	
publicVV 
asyncVV 
TaskVV #
TempUnloadAssemblyAsyncVV 1
(VV1 2
stringVV2 8
assemblyNameVV9 E
)VVE F
{WW 	
ifXX 
(XX 
!XX 
RuntimeAssembliesXX "
.XX" #
asseblyEntitiesXX# 2
.XX2 3
TryGetValueXX3 >
(XX> ?
assemblyNameXX? K
,XXK L
outXXM P
varXXQ T
contextXXU \
)XX\ ]
)XX] ^
returnYY 
;YY 
varZZ 
checkZZ 
=ZZ 
trueZZ 
;ZZ 
foreach[[ 
([[ 

ModuleInfo[[ 

moduleInfo[[  *
in[[+ -
context[[. 5
.[[5 6
Module[[6 <
)[[< =
{\\ 
if]] 
(]] 
check]] 
)]] 
check^^ 
=^^ 
await^^ !
_commandService^^" 1
.^^1 2
RemoveModuleAsync^^2 C
(^^C D

moduleInfo^^D N
)^^N O
;^^O P
}__ 
ifaa 
(aa 
!aa 
checkaa 
)aa 
{bb 
_loggingcc 
.cc 
Logcc 
(cc 
LogSeveritycc (
.cc( )
Errorcc) .
,cc. /
$"cc0 2
Failed to unload cc2 C
{ccC D
contextccD K
.ccK L
NameccL P
}ccP Q
 module.ccQ Y
"ccY Z
)ccZ [
;cc[ \
returndd 
;dd 
}ee 
contextgg 
.gg 
Contextgg 
.gg 
Unloadgg "
(gg" #
)gg# $
;gg$ %
_logginghh 
.hh 
Loghh 
(hh 
LogSeverityhh $
.hh$ %
Infohh% )
,hh) *
$"hh+ -
	Unloaded hh- 6
{hh6 7
contexthh7 >
.hh> ?
Namehh? C
}hhC D

 assembly.hhD N
"hhN O
)hhO P
;hhP Q
}ii 	
publickk 
asynckk 
Taskkk 
LoadAssemblyAsynckk +
(kk+ ,
stringkk, 2
assemblyNamekk3 ?
)kk? @
{ll 	
ifmm 
(mm 
!mm 
RuntimeAssembliesmm "
.mm" #
asseblyEntitiesmm# 2
.mm2 3
TryGetValuemm3 >
(mm> ?
assemblyNamemm? K
,mmK L
outmmM P
varmmQ T
contextmmU \
)mm\ ]
)mm] ^
returnnn 
;nn 
contextpp 
.pp 
Contextpp 
.pp  
LoadFromAssemblyNamepp 0
(pp0 1
contextpp1 8
.pp8 9
Assemblypp9 A
.ppA B
GetNameppB I
(ppI J
)ppJ K
)ppK L
;ppL M
awaitqq 
_commandServiceqq !
.qq! "
AddModulesAsyncqq" 1
(qq1 2
contextqq2 9
.qq9 :
Assemblyqq: B
,qqB C
	_providerqqD M
)qqM N
;qqN O
_loggingss 
.ss 
Logss 
(ss 
LogSeverityss $
.ss$ %
Infoss% )
,ss) *
$"ss+ -
Loaded ss- 4
{ss4 5
contextss5 <
.ss< =
Namess= A
}ssA B

 assembly.ssB L
"ssL M
)ssM N
;ssN O
}tt 	
internalvv 
asyncvv 
Taskvv *
LoadModulesFromAssembliesAsyncvv :
(vv: ;
)vv; <
{ww 	
foreachxx 
(xx 
varxx 
contextxx  
inxx! #
newxx$ '

Dictionaryxx( 2
<xx2 3
Stringxx3 9
,xx9 :
AssemblyEntityxx; I
>xxI J
(xxJ K
RuntimeAssembliesxxK \
.xx\ ]
asseblyEntitiesxx] l
)xxl m
.xxm n
Valuesxxn t
)xxt u
{yy 
varzz 
	addModulezz 
=zz 
awaitzz  %
_commandServicezz& 5
.zz5 6
AddModulesAsynczz6 E
(zzE F
contextzzF M
.zzM N
AssemblyzzN V
,zzV W
	_providerzzX a
)zza b
;zzb c
context{{ 
.{{ 
Module{{ 
={{  
	addModule{{! *
.{{* +
ToList{{+ 1
({{1 2
){{2 3
;{{3 4
RuntimeAssemblies|| !
.||! "
asseblyEntities||" 1
[||1 2
context||2 9
.||9 :
Assembly||: B
.||B C
GetName||C J
(||J K
)||K L
.||L M
Name||M Q
.||Q R
SanitizAssembly||R a
(||a b
)||b c
]||c d
=||e f
context||g n
;||n o
}}} 
} 	
public
ÄÄ 
AssemblyEntity
ÄÄ 
?
ÄÄ '
GetAssemblyEntityByModule
ÄÄ 8
(
ÄÄ8 9

ModuleInfo
ÄÄ9 C

moduleInfo
ÄÄD N
)
ÄÄN O
{
ÄÄP Q
while
ÅÅ 
(
ÅÅ 

moduleInfo
ÅÅ 
.
ÅÅ 
IsSubmodule
ÅÅ )
)
ÅÅ) *

moduleInfo
ÇÇ 
=
ÇÇ 

moduleInfo
ÇÇ '
.
ÇÇ' (
Parent
ÇÇ( .
;
ÇÇ. /
var
ÉÉ 
m
ÉÉ 
=
ÉÉ 
RuntimeAssemblies
ÉÉ %
.
ÉÉ% &
asseblyEntities
ÉÉ& 5
;
ÉÉ5 6
return
ÑÑ 
m
ÑÑ 
.
ÑÑ 
First
ÑÑ 
(
ÑÑ 
x
ÑÑ 
=>
ÑÑ 
x
ÑÑ  !
.
ÑÑ! "
Value
ÑÑ" '
.
ÑÑ' (
Module
ÑÑ( .
.
ÑÑ. /
Any
ÑÑ/ 2
(
ÑÑ2 3
v
ÑÑ3 4
=>
ÑÑ5 7
v
ÑÑ8 9
==
ÑÑ: <

moduleInfo
ÑÑ= G
)
ÑÑG H
)
ÑÑH I
.
ÑÑI J
Value
ÑÑJ O
;
ÑÑO P
}
ÖÖ 	
private
ÜÜ 
readonly
ÜÜ 
string
ÜÜ $
GetGuildEnabledPlugins
ÜÜ  6
=
ÜÜ7 8
$str
ÜÜ9 o
;
ÜÜo p
private
áá 
readonly
áá 
static
áá 
string
áá  &
InsertPlugin
áá' 3
=
áá4 5
$stráá6 Ç
;ááÇ É
private
àà 
readonly
àà 
static
àà 
string
àà  &
DeletePlugin
àà' 3
=
àà4 5
$stràà6 Å
;ààÅ Ç
public
ää 
async
ää 
Task
ää 
<
ää 
bool
ää 
>
ää 
IsPluginEnabled
ää  /
(
ää/ 0
IGuild
ää0 6
guild
ää7 <
,
ää< =

ModuleInfo
ää= G

moduleInfo
ääH R
)
ääR S
{
ääT U
var
ãã  
moduleAssemblyName
ãã "
=
ãã# $'
GetAssemblyEntityByModule
ãã% >
(
ãã> ?

moduleInfo
ãã? I
)
ããI J
;
ããJ K
if
åå 
(
åå  
moduleAssemblyName
åå "
.
åå" #
Assembly
åå# +
.
åå+ ,
GetName
åå, 3
(
åå3 4
)
åå4 5
.
åå5 6
Name
åå6 :
.
åå: ;
SanitizAssembly
åå; J
(
ååJ K
)
ååK L
==
ååM O
$str
ååP i
)
ååi j
return
ååk q
true
åår v
;
ååv w
PluginEnabling
çç 
[
çç 
]
çç 
cv
çç 
;
çç  
var
éé 
rd
éé 
=
éé 
await
éé (
_simpleCacheImplementation
éé 5
.
éé5 6
GetValueAsync
éé6 C
(
ééC D
$"
ééD F 
guildPluginsCache-
ééF X
{
ééX Y
guild
ééY ^
.
éé^ _
Id
éé_ a
}
ééa b
"
ééb c
)
ééc d
;
ééd e
if
èè 
(
èè 
!
èè 
rd
èè 
.
èè 
HasValue
èè 
)
èè 
{
êê 
var
ëë 

connection
ëë 
=
ëë  
	_database
ëë! *
.
ëë* +
GetDbConnection
ëë+ :
(
ëë: ;
)
ëë; <
;
ëë< =
cv
íí 
=
íí 
(
íí 
await
íí 

connection
íí &
.
íí& '

QueryAsync
íí' 1
<
íí1 2
PluginEnabling
íí2 @
>
íí@ A
(
ííA B$
GetGuildEnabledPlugins
ííB X
,
ííX Y
new
ííZ ]
{
ìì 
guild
îî 
=
îî 
(
îî 
long
îî !
)
îî! "
guild
îî" '
.
îî' (
Id
îî( *
}
ïï 
)
ïï 
)
ïï 
.
ïï 
ToArray
ïï 
(
ïï 
)
ïï 
;
ïï 
await
ññ (
_simpleCacheImplementation
ññ 0
.
ññ0 1
CacheValueAsync
ññ1 @
(
ññ@ A
$"
ññA C 
guildPluginsCache-
ññC U
{
ññU V
guild
ññV [
.
ññ[ \
Id
ññ\ ^
}
ññ^ _
"
ññ_ `
,
ññ` a
JsonConvert
ññb m
.
ññm n
SerializeObject
ññn }
(
ññ} ~
cvññ~ Ä
)ññÄ Å
)ññÅ Ç
;ññÇ É
}
óó 
else
òò 
{
òò 
cv
ôô 
=
ôô 
JsonConvert
ôô  
.
ôô  !
DeserializeObject
ôô! 2
<
ôô2 3
PluginEnabling
ôô3 A
[
ôôA B
]
ôôB C
>
ôôC D
(
ôôD E
rd
ôôE G
)
ôôG H
;
ôôH I
}
öö 
if
õõ 
(
õõ 
!
õõ 
cv
õõ 
.
õõ 
Any
õõ 
(
õõ 
x
õõ 
=>
õõ 
x
õõ 
.
õõ 
Plugin
õõ %
==
õõ& ( 
moduleAssemblyName
õõ) ;
.
õõ; <
Assembly
õõ< D
.
õõD E
GetName
õõE L
(
õõL M
)
õõM N
.
õõN O
Name
õõO S
.
õõS T
SanitizAssembly
õõT c
(
õõc d
)
õõd e
)
õõe f
)
õõf g
{
úú 
return
ùù 
false
ùù 
;
ùù 
}
ûû 
return
üü 
true
üü 
;
üü 
}
†† 	
public
¢¢ 
async
¢¢ 
Task
¢¢ 
EnablePlugin
¢¢ &
(
¢¢& '
IGuild
¢¢' -
guild
¢¢. 3
,
¢¢3 4

ModuleInfo
¢¢4 >

moduleInfo
¢¢? I
)
¢¢I J
{
££ 	
await
§§ (
_simpleCacheImplementation
§§ ,
.
§§, -"
InvalidateValueAsync
§§- A
(
§§A B
$"
§§B D 
guildPluginsCache-
§§D V
{
§§V W
guild
§§W \
.
§§\ ]
Id
§§] _
}
§§_ `
"
§§` a
)
§§a b
;
§§b c
NpgsqlConnection
•• 
guildService
•• )
=
••* +
(
••, -
NpgsqlConnection
••- =
)
••= >
	_database
••> G
.
••G H
GetDbConnection
••H W
(
••W X
$str
••X f
)
••f g
;
••g h
var
¶¶ 
r
¶¶ 
=
¶¶ 
this
¶¶ 
.
¶¶ '
GetAssemblyEntityByModule
¶¶ 2
(
¶¶2 3

moduleInfo
¶¶3 =
)
¶¶= >
.
¶¶> ?
Assembly
¶¶? G
.
¶¶G H
GetName
¶¶H O
(
¶¶O P
)
¶¶P Q
.
¶¶Q R
Name
¶¶R V
.
¶¶V W
SanitizAssembly
¶¶W f
(
¶¶f g
)
¶¶g h
;
¶¶h i
await
ßß 
guildService
ßß 
.
ßß 
ExecuteAsync
ßß +
(
ßß+ ,
InsertPlugin
ßß, 8
,
ßß8 9
new
ßß: =
{
®® 
guild
©© 
=
©© 
(
©© 
long
©© 
)
©© 
guild
©© #
.
©©# $
Id
©©$ &
,
©©& '
plugin
™™ 
=
™™ 
r
™™ 
}
´´ 
)
´´ 
;
´´ 
}
¨¨ 	
public
ÆÆ 
async
ÆÆ 
Task
ÆÆ 
DisablePlugin
ÆÆ '
(
ÆÆ' (
IGuild
ÆÆ( .
guild
ÆÆ/ 4
,
ÆÆ4 5

ModuleInfo
ÆÆ6 @

moduleInfo
ÆÆA K
)
ÆÆK L
{
ÆÆM N
await
ØØ (
_simpleCacheImplementation
ØØ ,
.
ØØ, -"
InvalidateValueAsync
ØØ- A
(
ØØA B
$"
ØØB D 
guildPluginsCache-
ØØD V
{
ØØV W
guild
ØØW \
.
ØØ\ ]
Id
ØØ] _
}
ØØ_ `
"
ØØ` a
)
ØØa b
;
ØØb c
NpgsqlConnection
∞∞ 
guildService
∞∞ )
=
∞∞* +
(
∞∞, -
NpgsqlConnection
∞∞- =
)
∞∞= >
	_database
∞∞> G
.
∞∞G H
GetDbConnection
∞∞H W
(
∞∞W X
$str
∞∞X f
)
∞∞f g
;
∞∞g h
var
±± 
r
±± 
=
±± 
this
±± 
.
±± '
GetAssemblyEntityByModule
±± 2
(
±±2 3

moduleInfo
±±3 =
)
±±= >
.
±±> ?
Assembly
±±? G
.
±±G H
GetName
±±H O
(
±±O P
)
±±P Q
.
±±Q R
Name
±±R V
.
±±V W
SanitizAssembly
±±W f
(
±±f g
)
±±g h
;
±±h i
await
≤≤ 
guildService
≤≤ 
.
≤≤ 
ExecuteAsync
≤≤ +
(
≤≤+ ,
DeletePlugin
≤≤, 8
,
≤≤8 9
new
≤≤: =
{
≥≥ 
guild
¥¥ 
=
¥¥ 
(
¥¥ 
long
¥¥ 
)
¥¥ 
guild
¥¥ #
.
¥¥# $
Id
¥¥$ &
,
¥¥& '
plugin
µµ 
=
µµ 
r
µµ 
}
∂∂ 
)
∂∂ 
;
∂∂ 
}
∑∑ 	
}
∏∏ 
}ππ Ã
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\PluginEnabling.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Services 
{ 
class 	
PluginEnabling
 
{ 
public		 
ulong		 
Guild		 
;		 
public

 
string

 
Plugin

 
;

 
} 
} ¿,
]C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\RedisService.cs
	namespace 	
	HeroBotv2
 
. 
Services 
{ 
public 

class 
RedisService 
: 
IRedisService  -
{ 
private 
readonly 
LoggingService '
_loggingService( 7
;7 8
private 
readonly 
IConfigurationRoot +
_config, 3
;3 4
private 
ISubscriber 
_subscriber '
;' (
private 
IDatabaseAsync 
	_database (
;( )
public 
RedisService 
( 
LoggingService *
loggingService+ 9
,9 :
IConfigurationRoot; M
configurationRootN _
)_ `
{a b
_loggingService 
= 
loggingService ,
;, -
_config 
= 
configurationRoot '
;' (
BeginConnection 
( 
) 
; 
} 	
private 
void 
BeginConnection $
($ %
)% &
{ 	
var 
config 
= 
new  
ConfigurationOptions 1
(1 2
)2 3
;3 4
config 
. 

ClientName 
= 
$str  4
;4 5
config 
. 
Password 
= 
_config %
.% &

GetSection& 0
(0 1
$str1 8
)8 9
.9 :

GetSection: D
(D E
$strE K
)K L
.L M

GetSectionM W
(W X
$strX b
)b c
.c d
Valued i
;i j
config 
. 
DefaultDatabase "
=# $
int% (
.( )
Parse) .
(. /
_config/ 6
.6 7

GetSection7 A
(A B
$strB I
)I J
.J K

GetSectionK U
(U V
$strV `
)` a
.a b
Valueb g
)g h
;h i
config 
. 
	EndPoints 
. 
Add  
(  !
$"! #
{# $
_config$ +
.+ ,

GetSection, 6
(6 7
$str7 >
)> ?
.? @

GetSection@ J
(J K
$strK Q
)Q R
.R S
ValueS X
}X Y
:Y Z
{Z [
_config[ b
.b c

GetSectionc m
(m n
$strn u
)u v
.v w

GetSection	w Å
(
Å Ç
$str
Ç à
)
à â
.
â ä
Value
ä è
}
è ê
"
ê ë
)
ë í
;
í ì
var   
redis   
=   !
ConnectionMultiplexer   -
.  - .
Connect  . 5
(  5 6
config  6 <
)  < =
;  = >
redis!! 
.!! 
ConnectionFailed!! "
+=!!# %
(!!& '
sender!!' -
,!!- .
evcent!!. 4
)!!4 5
=>!!6 8
{!!9 :
_loggingService"" 
.""  
Log""  #
(""# $
Discord""$ +
.""+ ,
LogSeverity"", 7
.""7 8
Error""8 =
,""= >
$str""? \
)""\ ]
;""] ^
}## 
;## 
redis$$ 
.$$ 
ConnectionRestored$$ $
+=$$% '
($$( )
sender$$) /
,$$/ 0
evcent$$1 7
)$$7 8
=>$$9 ;
{$$< =
_loggingService%% 
.%%  
Log%%  #
(%%# $
Discord%%$ +
.%%+ ,
LogSeverity%%, 7
.%%7 8
Warning%%8 ?
,%%? @
$str%%A c
)%%c d
;%%d e
}&& 
;&& 
_subscriber'' 
='' 
redis'' 
.''  
GetSubscriber''  -
(''- .
)''. /
;''/ 0
_loggingService(( 
.(( 
Log(( 
(((  
Discord((  '
.((' (
LogSeverity((( 3
.((3 4
Info((4 8
,((8 9
$str((: X
)((X Y
;((Y Z
	_database)) 
=)) 
redis)) 
.)) 
GetDatabase)) )
())) *
int))* -
.))- .
Parse)). 3
())3 4
_config))4 ;
.)); <

GetSection))< F
())F G
$str))G N
)))N O
[))O P
$str))P Z
]))Z [
)))[ \
,))\ ]
new))^ a
object))b h
{))i j
}))k l
)))l m
;))m n
_loggingService** 
.** 
Log** 
(**  
Discord**  '
.**' (
LogSeverity**( 3
.**3 4
Info**4 8
,**8 9
$str**: Y
)**Y Z
;**Z [
}++ 	
public-- 
ISubscriber-- 
GetSubscriber-- (
(--( )
)--) *
{.. 	
return// 
_subscriber// 
;// 
}00 	
public22 
IDatabaseAsync22 
GetDatabase22 )
(22) *
)22* +
{33 	
return44 
	_database44 
;44 
}55 	
}66 
}77 ù=
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\StartupService.cs
	namespace 	
	HeroBotv2
 
. 
Services 
{ 
public 

class 
StartupService 
{ 
private 
readonly 
IServiceProvider )
	_provider* 3
;3 4
private 
readonly  
DiscordShardedClient -
_discord. 6
;6 7
private 
readonly 
CommandService '
	_commands( 1
;1 2
private 
readonly 
IConfigurationRoot +
_config, 3
;3 4
private 
int 
[ 
] 
ShardPresences $
;$ %
private 
readonly 
string 
[  
]  !
presence" *
=+ ,
new- 0
[0 1
]1 2
{3 4
$str 
, 
$str !
,! "
$str +
,+ ,
$str #
,# $
$str *
,* +
$str  
} 	
;	 

private 
Task 
UpdatePresences $
{ 	
get 
{   
return!! 
new!! 
Task!! 
(!!  
async!!  %
(!!& '
)!!' (
=>!!) +
{"" 
while## 
(## 
true## 
)##  
{$$ 
await%% 
Task%% "
.%%" #
Delay%%# (
(%%( )
$num%%) .
)%%. /
;%%/ 0
foreach&& 
(&&  !
DiscordSocketClient&&! 4
discordSocketClient&&5 H
in&&I K
_discord&&L T
.&&T U
Shards&&U [
)&&[ \
{'' 
if(( 
(((  
discordSocketClient((  3
.((3 4
ConnectionState((4 C
==((D F
ConnectionState((G V
.((V W
	Connected((W `
)((` a
{)) 
if**  "
(**# $
discordSocketClient**$ 7
.**7 8
ShardId**8 ?
>**@ A
$num**B C
)**C D
{++  !
if,,$ &
(,,' (
ShardPresences,,( 6
[,,6 7
discordSocketClient,,7 J
.,,J K
ShardId,,K R
],,R S
==,,T V
presence,,W _
.,,_ `
Length,,` f
-,,f g
$num,,g h
),,h i
ShardPresences--( 6
[--6 7
discordSocketClient--7 J
.--J K
ShardId--K R
]--R S
=--T U
$num--V W
;--W X
else..$ (
ShardPresences//( 6
[//6 7
discordSocketClient//7 J
.//J K
ShardId//K R
]//R S
++//S U
;//U V
await00$ )
discordSocketClient00* =
.00= >"
SetGamewithPlaceholder00> T
(00T U
_discord00U ]
,00] ^
presence00^ f
[00f g
ShardPresences00g u
[00u v 
discordSocketClient	00v â
.
00â ä
ShardId
00ä ë
]
00ë í
]
00í ì
)
00ì î
;
00î ï
}11  !
else22  $
{33  !
if44$ &
(44' (
ShardPresences44( 6
[446 7
$num447 8
]448 9
==44: <
presence44= E
.44E F
Length44F L
-44L M
$num44M N
)44N O
ShardPresences55( 6
[556 7
$num557 8
]558 9
=55: ;
$num55< =
;55= >
else66$ (
ShardPresences77( 6
[776 7
$num777 8
]778 9
++779 ;
;77; <
await88$ )
discordSocketClient88* =
.88= >"
SetGamewithPlaceholder88> T
(88T U
_discord88U ]
,88] ^
presence88^ f
[88f g
ShardPresences88g u
[88u v
$num88v w
]88w x
]88x y
)88y z
;88z {
}99  !
}:: 
};; 
}<< 
}== 
)== 
;== 
}>> 
}?? 	
publicAA 
StartupServiceAA 
(AA 
IServiceProviderBB 
providerBB %
,BB% & 
DiscordShardedClientCC  
discordCC! (
,CC( )
CommandServiceDD 
commandsDD #
,DD# $
IConfigurationRootEE 
configEE %
)EE% &
{FF 	
	_providerGG 
=GG 
providerGG  
;GG  !
_configHH 
=HH 
configHH 
;HH 
_discordII 
=II 
discordII 
;II 
	_commandsJJ 
=JJ 
commandsJJ  
;JJ  !
}KK 	
publicMM 
asyncMM 
TaskMM 

StartAsyncMM $
(MM$ %
)MM% &
{NN 	
stringOO 
discordTokenOO 
=OO  !
_configOO" )
[OO) *
$strOO* :
]OO: ;
;OO; <
ifPP 
(PP 
stringPP 
.PP 
IsNullOrWhiteSpacePP )
(PP) *
discordTokenPP* 6
)PP6 7
)PP7 8
throwQQ 
newQQ #
InvalidProgramExceptionQQ 1
(QQ1 2
$str	QQ2 ü
)
QQü †
;
QQ† °
awaitSS 
_discordSS 
.SS 

LoginAsyncSS %
(SS% &
	TokenTypeSS& /
.SS/ 0
BotSS0 3
,SS3 4
discordTokenSS5 A
)SSA B
;SSB C
awaitTT 
_discordTT 
.TT 

StartAsyncTT %
(TT% &
)TT& '
;TT' (
ShardPresencesUU 
=UU 
newUU  
intUU! $
[UU$ %
_discordUU% -
.UU- .
ShardsUU. 4
.UU4 5
CountUU5 :
]UU: ;
;UU; <
UpdatePresencesVV 
.VV 
StartVV !
(VV! "
)VV" #
;VV# $
awaitWW 
	_commandsWW 
.WW 
AddModulesAsyncWW +
(WW+ ,
AssemblyWW, 4
.WW4 5
GetEntryAssemblyWW5 E
(WWE F
)WWF G
,WWG H
	_providerWWI R
)WWR S
;WWS T
}XX 	
}YY 
publicZZ 

staticZZ 
classZZ )
DiscordShardedClientExtensionZZ 5
{ZZ6 7
public[[ 
static[[ 
Task[[ "
SetGamewithPlaceholder[[ 1
([[1 2
this[[2 6
DiscordSocketClient[[7 J
discordsocket[[K X
,[[X Y 
DiscordShardedClient[[Y m!
discordShardedClient	[[n Ç
,
[[Ç É
string
[[Ñ ä
str
[[ã é
)
[[é è
{
[[ê ë
return\\ 
discordsocket\\  
.\\  !
SetGameAsync\\! -
(\\- .
str\\. 1
.]] 
Replace]] 
(]] 
$str]] '
,]]' ( 
discordShardedClient]]) =
.]]= >
Guilds]]> D
.]]D E
Count]]E J
.]]J K
ToString]]K S
(]]S T
)]]T U
)]]U V
.^^ 
Replace^^ 
(^^ 
$str^^ '
,^^' ( 
discordShardedClient^^) =
.^^= >
Guilds^^> D
.^^D E
Sum^^E H
(^^H I
x^^I J
=>^^K M
x^^N O
.^^O P
Users^^P U
.^^U V
Count^^V [
)^^[ \
.^^\ ]
ToString^^] e
(^^e f
)^^f g
)^^g h
)`` 
;`` 
}aa 	
}bb 
}cc ∆
_C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Services\ToLoadAssembly.cs
	namespace 	
HeroBot
 
. 
Core 
. 
Services 
{ 
internal 
class 
ToLoadAssembly !
{ 
public 
Assembly 
Assembly  
{! "
get# &
;& '
internal( 0
set1 4
;4 5
}6 7
public		 
ModuleLoadContext		  
assemblycontext		! 0
{		1 2
get		3 6
;		6 7
internal		8 @
set		A D
;		D E
}		F G
}

 
} €9
OC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Core\Startup.cs
	namespace 	
	HeroBotv2
 
{ 
public 

class 
Startup 
{ 
public 
IConfigurationRoot !
Configuration" /
{0 1
get2 5
;5 6
}7 8
public 
Startup 
( 
) 
{ 	
NpgsqlLogManager 
. 
Provider %
=& '
new( +"
ConsoleLoggingProvider, B
(B C
NpgsqlLogLevelC Q
.Q R
DebugR W
,W X
trueY ]
,] ^
true_ c
)c d
;d e
var 
builder 
= 
new  
ConfigurationBuilder 2
(2 3
)3 4
. 
SetBasePath 
( 

AppContext '
.' (
BaseDirectory( 5
)5 6
. 
AddYamlFile 
( 
$str *
)* +
;+ ,
Configuration 
= 
builder #
.# $
Build$ )
() *
)* +
;+ ,
} 	
public 
static 
async 
Task  
RunAsync! )
() *
)* +
{   	
var!! 
startup!! 
=!! 
new!! 
Startup!! %
(!!% &
)!!& '
;!!' (
await"" 
startup"" 
."" 
	_RunAsync"" #
(""# $
)""$ %
;""% &
}## 	
public%% 
async%% 
Task%% 
	_RunAsync%% #
(%%# $
)%%$ %
{&& 	
var(( 
services(( 
=(( 
new(( 
ServiceCollection(( 0
(((0 1
)((1 2
;((2 3
ConfigureServices)) 
()) 
services)) &
)))& '
;))' (
var** 
provider** 
=** 
services** #
.**# $ 
BuildServiceProvider**$ 8
(**8 9
)**9 :
;**: ;
provider++ 
.++ 
GetRequiredService++ '
<++' (
LoggingService++( 6
>++6 7
(++7 8
)++8 9
;++9 :
provider,, 
.,, 
GetRequiredService,, '
<,,' (
IDatabaseService,,( 8
>,,8 9
(,,9 :
),,: ;
;,,; <
provider-- 
.-- 
GetRequiredService-- '
<--' (
IRedisService--( 5
>--5 6
(--6 7
)--7 8
;--8 9
provider.. 
... 
GetRequiredService.. '
<..' (
ICooldownService..( 8
>..8 9
(..9 :
)..: ;
;..; <
provider// 
.// 
GetRequiredService// '
<//' (
CooldownService//( 7
>//7 8
(//8 9
)//9 :
;//: ;
provider00 
.00 
GetRequiredService00 '
<00' (%
SimpleCacheImplementation00( A
>00A B
(00B C
)00C D
;00D E
provider11 
.11 0
$GetAllServicesFromExternalAssemblies11 9
(119 :
)11: ;
;11; <
provider22 
.22 
GetRequiredService22 '
<22' (
CommandHandler22( 6
>226 7
(227 8
)228 9
;229 :
await33 
provider33 
.33 
GetRequiredService33 -
<33- .
StartupService33. <
>33< =
(33= >
)33> ?
.44 

StartAsync44 
(44 
)44 
;44 
await55 
(55 
(55 
ModulesService55 "
)55" #
provider55# +
.55+ ,
GetRequiredService55, >
<55> ?
IModulesService55? N
>55N O
(55O P
)55P Q
)55Q R
.66 *
LoadModulesFromAssembliesAsync66 /
(66/ 0
)660 1
;661 2
await77 
Task77 
.77 
Delay77 
(77 
-77 
$num77 
)77  
;77  !
}88 	
internal:: 
void:: 
ConfigureServices:: '
(::' (
IServiceCollection::( :
services::; C
)::C D
{;; 	
services<< 
.<< 
AddSingleton<< !
(<<! "
new<<" % 
DiscordShardedClient<<& :
(<<: ;
new<<; >
DiscordSocketConfig<<? R
{== 
LogLevel>> 
=>> 
LogSeverity>> &
.>>& '
Info>>' +
,>>+ ,
MessageCacheSize??  
=??! "
$num??# $
,@@ 
AlwaysDownloadUsers@@ %
=@@& '
false@@( -
,@@- .
ExclusiveBulkDeleteAA #
=AA$ %
trueAA& *
,BB 
LargeThresholdBB 
=BB  !
$numBB" #
}CC 
)CC 
)CC 
.DD 
AddSingletonDD 
(DD 
newDD 
CommandServiceDD ,
(DD, -
newDD- 0 
CommandServiceConfigDD1 E
{EE 
LogLevelFF 
=FF 
LogSeverityFF &
.FF& '
InfoFF' +
,FF+ ,
DefaultRunModeGG 
=GG  
RunModeGG! (
.GG( )
AsyncGG) .
,GG. /
}HH 
)HH 
)HH 
.KK 
AddSingletonKK 
<KK 
IDatabaseServiceKK *
,KK* +
HeroBotContextKK, :
>KK: ;
(KK; <
)KK< =
.LL 
AddSingletonLL 
<LL %
SimpleCacheImplementationLL 3
>LL3 4
(LL4 5
)LL5 6
.MM 
AddSingletonMM 
<MM 
IRedisServiceMM '
,MM' (
RedisServiceMM) 5
>MM5 6
(MM6 7
)MM7 8
.NN 
AddSingletonNN 
<NN 
ICooldownServiceNN *
,NN* +
CooldownServiceNN, ;
>NN; <
(NN< =
)NN= >
.OO 
AddSingletonOO 
<OO 
CooldownServiceOO )
>OO) *
(OO* +
)OO+ ,
.PP 
AddSingletonPP 
<PP 
CommandHandlerPP (
>PP( )
(PP) *
)PP* +
.QQ 
AddSingletonQQ 
<QQ 
StartupServiceQQ (
>QQ( )
(QQ) *
)QQ* +
.RR 
AddSingletonRR 
<RR 
LoggingServiceRR (
>RR( )
(RR) *
)RR* +
.SS 
AddSingletonSS 
<SS 
IModulesServiceSS )
,SS) *
ModulesServiceSS* 8
>SS8 9
(SS9 :
)SS: ;
.TT 
AddSingletonTT 
<TT 
RandomTT  
>TT  !
(TT! "
)TT" #
.UU 
AddSingletonUU 
(UU 
ConfigurationUU '
)UU' (
.VV 1
%LoadAllServicesFromExternalAssembilesVV 2
(VV2 3
)VV3 4
;VV4 5
}WW 	
}XX 
}YY 