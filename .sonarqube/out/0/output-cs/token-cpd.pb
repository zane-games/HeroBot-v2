®
fC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\CooldownAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
public		 

class		 
CooldownAttribute		 "
:		# $!
PreconditionAttribute		% :
{

 
public 
static 
ICooldownService &
	_cooldown' 0
;0 1
public 
TimeSpan 
cooldown  
;  !
public 
CooldownAttribute  
(  !
int! $
seconds% ,
), -
{. /
cooldown0 8
=9 :
TimeSpan; C
.C D
FromSecondsD O
(O P
secondsP W
)W X
;X Y
}Z [
public 
override 
async 
Task "
<" #
PreconditionResult# 5
>5 6!
CheckPermissionsAsync7 L
(L M
ICommandContextM \
context] d
,d e
CommandInfof q
commandr y
,y z
IServiceProvider	{ ã
services
å î
)
î ï
{ 	
TimeSpan 
? 
cmdCool 
= 
await  %
	_cooldown& /
./ 0
IsCommandCooldowned0 C
(C D
contextD K
.K L
UserL P
.P Q
IdQ S
,S T
commandU \
.\ ]
Name] a
)a b
;b c
if 
( 
cmdCool 
. 
HasValue  
)  !
{" #
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$"4 65
)This command is cooldowned, please, wait 6 _
{_ `
cmdCool` g
.g h
Valueh m
.m n
ToHumanReadablen }
(} ~
)~ 
}	 Ä
"
Ä Å
)
Å Ç
;
Ç É
} 
TimeSpan 
? 
mCool 
= 
await #
	_cooldown$ -
.- .
IsModuleCooldowned. @
(@ A
contextA H
.H I
UserI M
.M N
IdN P
,P Q
commandR Y
.Y Z
ModuleZ `
.` a
Namea e
)e f
;f g
if 
( 
mCool 
. 
HasValue 
) 
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$"4 64
(This module is cooldowned, please, wait 6 ^
{^ _
mCool_ d
.d e
Valuee j
.j k
ToHumanReadablek z
(z {
){ |
}| }
"} ~
)~ 
;	 Ä
} 
return 
PreconditionResult %
.% &
FromSuccess& 1
(1 2
)2 3
;3 4
} 	
} 
} Õ
hC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\NeedPluginAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
public 

class 
NeedPluginAttribute $
:% &!
PreconditionAttribute' <
{ 
public 
static 
IDatabaseService &
databaseService' 6
;6 7
private 
readonly 
string 
GetGuildByIdQuery  1
=2 3
$str4 _
;_ `
private 
readonly 
string 
InsertGuild  +
=, -
$str. \
;\ ]
private 
readonly 
string 
InsertGuildPlugin  1
=2 3
$str	4 Ä
;
Ä Å
private 
readonly 
string 
GetPluginId  +
=, -
$str. c
;c d
private 
readonly 
string "
CheckIfPluginIsEnabled  6
=7 8
$str	9 à
;
à â
public 
override 
async 
Task "
<" #
PreconditionResult# 5
>5 6!
CheckPermissionsAsync7 L
(L M
ICommandContextM \
context] d
,d e
CommandInfof q
commandr y
,y z
IServiceProvider	{ ã
services
å î
)
î ï
{ 	
returnJJ 
PreconditionResultJJ %
.JJ% &
FromSuccessJJ& 1
(JJ1 2
)JJ2 3
;JJ3 4
}KK 	
privateMM 
boolMM 
CheckSubModuleMM #
(MM# $
IDbConnectionMM$ 1
guildMM2 7
,MM7 8

ModuleInfoMM9 C
moduleMMD J
,MMJ K
IGuildMMK Q
bsonElementsMMR ^
)MM^ _
{NN 	
returnOO 
trueOO 
;OO 
}UU 	
}VV 
}WW É
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\PluginAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Assembly% -
)- .
]. /
class 	
PluginAttribute
 
: 
	Attribute %
{		 
}

 
} 
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\RuntimeConstants.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
public 

class 
RuntimeConstants !
{ 
public 
static 
object 
MasterPluginId +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
public 
static 
string 

CheckEmoji '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
=6 7
$str8 U
;U V
} 
}		 ñ
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\ServiceAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
)* +
]+ ,
public 

class 
ServiceAttribute !
:" #
	Attribute$ -
{		 
}

 
} á	
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Entities\Contextentity.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Entities !
{ 
public 

sealed 
class 
ContextEntity %
{		 
public

 
string

 
Name

 
{

 
get

  
;

  !
set

" %
;

% &
}

' (
public 

ModuleInfo 
Module  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
Assembly 
Assembly  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
ModuleLoadContext  
Context! (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
IPluginRefferal 
pluginRefferal -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
} 
} ª
kC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\ExtendedModules\ModuleLoadcontext.cs
	namespace 	
HeroBot
 
. 
Common 
. 
ExtendedModules (
{ 
public 

sealed 
class 
ModuleLoadContext )
:* +
AssemblyLoadContext, ?
{ 
public 
ModuleLoadContext  
(  !
)! "
:# $
base% )
() *
true* .
). /
{		 	
}

 	
	protected 
override 
Assembly #
Load$ (
(( )
AssemblyName) 5
assemblyName6 B
)B C
{ 	
return 
base 
. 
Load 
( 
assemblyName )
)) *
;* +
} 	
} 
} ‚
gC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\EmbedBuilderExtension.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Helpers  
{ 
public 

static 
class !
EmbedBuilderExtension -
{		 
public

 
static

 
Random

 
Random

 #
=

$ %
new

& )
Random

* 0
(

0 1
)

1 2
;

2 3
public 
static 
Color 
[ 
] 
Colors $
=% &
new' *
[* +
]+ ,
{- .
new/ 2
Color3 8
(8 9
$num9 <
,< =
$num> A
,A B
$numC F
)F G
,G H
newI L
ColorM R
(R S
$numS V
,V W
$numX [
,[ \
$num] `
)` a
,a b
newc f
Colorg l
(l m
$numm p
,p q
$numr u
,u v
$numw z
)z {
,{ |
new	} Ä
Color
Å Ü
(
Ü á
$num
á ä
,
ä ã
$num
å è
,
è ê
$num
ë î
)
î ï
,
ï ñ
new
ó ö
Color
õ †
(
† °
$num
° §
,
§ •
$num
¶ ©
,
© ™
$num
´ Æ
)
Æ Ø
}
∞ ±
;
± ≤
public 
static 
EmbedBuilder "
WithRandomColor# 2
(2 3
this3 7
EmbedBuilder8 D
embedBuilderE Q
)Q R
{S T
return 
embedBuilder 
.  
	WithColor  )
() *
Colors* 0
[0 1
Random1 7
.7 8
Next8 <
(< =
)= >
%? @
ColorsA G
.G H
LengthH N
]N O
)O P
;P Q
} 	
public 
static 
EmbedBuilder "
WithCopyrightFooter# 6
(6 7
this7 ;
EmbedBuilder< H
embedBuilderI U
,U V
stringV \
userName] e
=f g
nullh l
,l m
stringm s
commandt {
=| }
null	~ Ç
)
Ç É
{ 	
return 
embedBuilder 
.   
WithCurrentTimestamp  4
(4 5
)5 6
.6 7

WithFooter7 A
(A B
newB E
EmbedFooterBuilderF X
(X Y
)Y Z
.Z [
WithIconUrl[ f
(f g
$str	g √
)
√ ƒ
.
ƒ ≈
WithText
≈ Õ
(
Õ Œ
$"
Œ –
{
– —
(
— “
command
“ Ÿ
==
⁄ ‹
null
› ·
?
‚ „
String
‰ Í
.
Í Î
Empty
Î 
:
Ò Ú
$"
Û ı
Command 
ı ˝
{
˝ ˛
command
˛ Ö
}
Ö Ü
"
Ü á
)
á à
}
à â
 ¬© HeroBot 
â î
{
î ï
DateTime
ï ù
.
ù û
Now
û °
.
° ¢
Year
¢ ¶
}
¶ ß
 ‚Ä¢ 
ß ™
{
™ ´
(
´ ¨
userName
¨ ¥
==
µ ∑
null
∏ º
?
Ω æ
String
ø ≈
.
≈ ∆
Empty
∆ À
:
Ã Õ
$"
Œ –
Requested by 
– ›
{
› ﬁ
userName
ﬁ Ê
}
Ê Á
"
Á Ë
)
Ë È
}
È Í
"
Í Î
)
Î Ï
)
Ï Ì
;
Ì Ó
} 	
} 
} ëD
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\ExtensionHelpers.cs
	namespace		 	
HeroBot		
 
.		 
Common		 
.		 
Helpers		  
{

 
public 

static 
class 
ExtensionHelper '
{ 
public 
static 
string 
SanitzeEntity *
(* +
this+ /
string0 6
entity7 =
)= >
{ 	
return 
entity 
. 
Replace !
(! "
$str" *
,* +
string, 2
.2 3
Empty3 8
)8 9
;9 :
} 	
public 
static 
string 
SanitizAssembly ,
(, -
this- 1
string2 8
module9 ?
)? @
{ 	
return 
module 
. 
Replace !
(! "
$str" ,
,, -
string. 4
.4 5
Empty5 :
): ;
;; <
} 	
public 
static 
bool 
HasPrefixes &
(& '
this' +
IUserMessage, 8
message9 @
,@ A
refB E
intF I
argposJ P
,P Q
paramsR X
charY ]
[] ^
]^ _
prefixes` h
)h i
{ 	
var 
content 
= 
message !
.! "
Content" )
;) *
var 
shouldContinue 
=  
false! &
;& '
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
content* 1
)1 2
)2 3
shouldContinue 
=  
false! &
;& '
foreach 
( 
var 
prefix 
in  "
prefixes# +
)+ ,
{   
if!! 
(!! 
content!! 
[!! 
$num!! 
]!! 
==!! !
prefix!!" (
)!!( )
{"" 
shouldContinue## "
=### $
true##% )
;##) *
break$$ 
;$$ 
}%% 
}&& 
argpos(( 
=(( 
shouldContinue(( #
?(($ %
$num((& '
:((( )
$num((* +
;((+ ,
return)) 
shouldContinue)) !
;))! "
}** 	
public,, 
static,, 
IServiceCollection,, ($
AddImplementedInterfaces,,) A
(,,A B
this,,B F
IServiceCollection,,G Y
service,,Z a
,,,a b
Assembly,,c k
assembly,,l t
,,,t u
params-- 
Type-- 
[-- 
]-- 

interfaces-- $
)--$ %
{.. 	
if// 
(// 
assembly// 
is// 
null//  
||//! #

interfaces//$ .
.//. /
Length/// 5
is//6 8
$num//9 :
)//: ;
return00 
service00 
;00 
foreach22 
(22 
var22 
inter22 
in22 !

interfaces22" ,
)22, -
{33 
var44 
matches44 
=44 
assembly44 &
.44& '
GetTypes44' /
(44/ 0
)440 1
.441 2
Where442 7
(447 8
x448 9
=>44: <
!44= >
x44> ?
.44? @

IsAbstract44@ J
&&44K M
inter44N S
.44S T
IsAssignableFrom44T d
(44d e
x44e f
)44f g
)44g h
.55 
ToArray55 
(55 
)55 
;55 
if77 
(77 
matches77 
.77 
Length77 "
is77# %
$num77& '
)77' (
continue88 
;88 
foreach:: 
(:: 
var:: 
match:: "
in::# %
matches::& -
)::- .
service;; 
.;; 
AddSingleton;; (
(;;( )
match;;) .
);;. /
;;;/ 0
}<< 
return== 
service== 
;== 
}>> 	
public@@ 
static@@ 
IServiceCollection@@ (
AddServices@@) 4
(@@4 5
this@@5 9
IServiceCollection@@: L
service@@M T
,@@T U
params@@V \
Type@@] a
[@@a b
]@@b c
types@@d i
)@@i j
{AA 	
ifBB 
(BB 
typesBB 
.BB 
LengthBB 
isBB 
$numBB  !
)BB! "
returnCC 
serviceCC 
;CC 
foreachEE 
(EE 
varEE 
typeEE 
inEE  
typesEE! &
)EE& '
{FF 
serviceGG 
.GG 
AddSingletonGG $
(GG$ %
typeGG% )
)GG) *
;GG* +
}HH 
returnJJ 
serviceJJ 
;JJ 
}KK 	
publicMM 
staticMM 
doubleMM 
ConvertToMbMM (
(MM( )
thisMM) -
longMM. 2
valueMM3 8
)MM8 9
{NN 	
returnOO 
(OO 
doubleOO 
)OO 
valueOO  
/OO! "
$numOO# '
/OO( )
$numOO* .
;OO. /
}PP 	
publicRR 
staticRR 
CastRR 
CastToRR !
<RR! "
CastRR" &
>RR& '
(RR' (
thisRR( ,
objectRR- 3
objRR4 7
)RR7 8
{SS 	
returnTT 
objTT 
isTT 
CastTT 
valTT "
?TT# $
valTT% (
:TT) *
defaultTT+ 2
;TT2 3
}UU 	
publicWW 
staticWW 
CastWW 
CastAsWW !
<WW! "
CastWW" &
>WW& '
(WW' (
thisWW( ,
objectWW- 3
objWW4 7
)WW7 8
{XX 	
returnYY 
(YY 
CastYY 
)YY 
objYY 
;YY 
}ZZ 	
public\\ 
static\\ 
string\\ 
ObjectToString\\ +
(\\+ ,
this\\, 0
object\\1 7
obj\\8 ;
)\\; <
{]] 	
var^^ 
sf^^ 
=^^ 
new^^ 
StringBuilder^^ &
(^^& '
)^^' (
;^^( )
var__ 

properties__ 
=__ 
obj__  
.__  !
GetType__! (
(__( )
)__) *
.__* + 
GetRuntimeProperties__+ ?
(__? @
)__@ A
;__A B
varaa 
maxaa 
=aa 

propertiesaa  
.aa  !
Maxaa! $
(aa$ %
xaa% &
=>aa' )
xaa* +
.aa+ ,
Nameaa, 0
.aa0 1
Lengthaa1 7
)aa7 8
+aa9 :
$numaa; <
;aa< =
foreachbb 
(bb 
varbb 
propertybb !
inbb" $

propertiesbb% /
)bb/ 0
{cc 
vardd 
spacedd 
=dd 
newdd 
stringdd  &
(dd& '
$chardd' *
,dd* +
maxdd, /
-dd0 1
propertydd2 :
.dd: ;
Namedd; ?
.dd? @
Lengthdd@ F
)ddF G
;ddG H
varee 
valueee 
=ee 
propertyee $
.ee$ %
GetValueee% -
(ee- .
objee. 1
)ee1 2
;ee2 3
sfgg 
.gg 
Appendgg 
(gg 
$"gg 
{gg 
propertygg %
.gg% &
Namegg& *
}gg* +
{gg+ ,
spacegg, 1
}gg1 2
: gg2 4
{gg4 5
valuegg5 :
}gg: ;
\ngg; =
"gg= >
)gg> ?
;gg? @
}hh 
returnjj 
$"jj 
```ini\n===== [ jj %
{jj% &
objjj& )
.jj) *
GetTypejj* 1
(jj1 2
)jj2 3
.jj3 4
Namejj4 8
}jj8 9"
 Information ] =====\njj9 O
{jjO P
sfjjP R
.jjR S
ToStringjjS [
(jj[ \
)jj\ ]
}jj] ^
\n```jj^ c
"jjc d
;jjd e
}kk 	
}ll 
}mm Ø	
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\TimeSpanHelper.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Helpers  
{ 
public 

static 
class 
TimeSpanHelper &
{ 
public		 
static		 
string		 
ToHumanReadable		 ,
(		, -
this		- 1
TimeSpan		2 :
timeSpan		; C
)		C D
{		E F
return

 
String

 
.

 
Format

  
(

  !
$str

! ]
,

] ^
timeSpan

_ g
)

g h
. 
Replace 
( 
$str "
," #
String# )
.) *
Empty* /
)/ 0
. 
Replace 
( 
$str #
,# $
String$ *
.* +
Empty+ 0
)0 1
. 
Replace 
( 
$str )
,) *
String* 0
.0 1
Empty1 6
)6 7
;7 8
} 	
} 
} Ê
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\ICooldownService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
ICooldownService %
{		 
Task

 
<

 
TimeSpan

 
?

 
>

 
IsModuleCooldowned

 *
(

* +
ulong

+ 0
userid

1 7
,

7 8
string

8 >

moduleName

? I
)

I J
;

J K
Task 
< 
TimeSpan 
? 
> 
IsCommandCooldowned +
(+ ,
ulong, 1
userid2 8
,8 9
string: @
commandNameA L
)L M
;M N
} 
} Ò
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IDatabaseService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public		 

	interface		 
IDatabaseService		 %
{

 
public 
IDbConnection 
GetDbConnection ,
(, -
)- .
;. /
public 
IDbConnection 
GetDbConnection ,
(, -
string- 3
v4 5
)5 6
;6 7
} 
} ⁄
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IPluginRefferal.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
IPluginRefferal $
{ 
}		 
}

 ô
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IRedisService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
IRedisService "
{		 
ISubscriber

 
GetSubscriber

 !
(

! "
)

" #
;

# $
IDatabaseAsync 
GetDatabase "
(" #
)# $
;$ %
} 
} ”
aC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IVoteService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public		 

	interface		 
IVoteService		 !
{

 
public 
delegate 
void 
OnVoteHandler *
(* +
DblVote, 3
e4 5
)5 6
;6 7
public 
event 
OnVoteHandler "
VoteHandler# .
;. /
} 
public 

class 
DblVote 
{ 
public 
IUser 
user 
; 
public 
bool 
isPrimed 
; 
} 
} ù	
pC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Migrations\201807182_Migration_Plugins.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Migrations #
{ 
[ 
	Migration 
( 
$num 
, 
$str "
)" #
]# $
class		 	(
_201807182_Migration_Plugins		
 &
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
$str "
)" #
;# $
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
$str "
)" #
. 

WithColumn 
( 
$str  
)  !
.! "
AsBinary" *
(* +
)+ ,
;, -
} 	
} 
} 