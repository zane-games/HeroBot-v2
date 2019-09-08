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
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$"4 65
)This command is cooldowned, please, wait 6 _
{_ `
cmdCool` g
.g h
Valueh m
.m n
ToHumanReadablen }
(} ~
)~ 
}	 Ä
"
Ä Å
)
Å Ç
;
Ç É
} 
TimeSpan 
? 
mCool 
= 
await #
	_cooldown$ -
.- .
IsModuleCooldowned. @
(@ A
contextA H
.H I
UserI M
.M N
IdN P
,P Q
commandR Y
.Y Z
ModuleZ `
.` a
Namea e
)e f
;f g
if 
( 
mCool 
. 
HasValue 
) 
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$"4 64
(This module is cooldowned, please, wait 6 ^
{^ _
mCool_ d
.d e
Valuee j
.j k
ToHumanReadablek z
(z {
){ |
}| }
"} ~
)~ 
;	 Ä
} 
return 
PreconditionResult %
.% &
FromSuccess& 1
(1 2
)2 3
;3 4
} 	
} 
}   ∫
hC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\NeedPluginAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{		 
public

 

class

 
NeedPluginAttribute

 $
:

% &!
PreconditionAttribute

' <
{ 
public 
override 
async 
Task "
<" #
PreconditionResult# 5
>5 6!
CheckPermissionsAsync7 L
(L M
ICommandContextM \
context] d
,d e
CommandInfof q
commandr y
,y z
IServiceProvider	{ ã
services
å î
)
î ï
{ 	
var 
isP 
= 
await 
services $
.$ %
GetRequiredService% 7
<7 8
IModulesService8 G
>G H
(H I
)I J
.J K
IsPluginEnabledK Z
(Z [
context[ b
.b c
Guildc h
,h i!
ResolveMainModuleNamej 
(	 Ä
command
Ä á
.
á à
Module
à é
)
é è
)
è ê
;
ê ë
if 
( 
! 
isP 
) 
{ 
return 
PreconditionResult )
.) *
	FromError* 3
(3 4
$"4 6&
Plugin is not enabled on `6 P
{P Q
contextQ X
.X Y
GuildY ^
}^ _
`_ `
"` a
)a b
;b c
} 
return 
PreconditionResult %
.% &
FromSuccess& 1
(1 2
)2 3
;3 4
} 	
private 

ModuleInfo !
ResolveMainModuleName 0
(0 1

ModuleInfo1 ;

moduleInfo< F
)F G
{ 	
while 
( 

moduleInfo 
. 
IsSubmodule )
)) *

moduleInfo 
= 

moduleInfo '
.' (
Parent( .
;. /
return 

moduleInfo 
; 
} 	
} 
}   É
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Attributes\PluginAttribute.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Assembly% -
)- .
]. /
class 	
PluginAttribute
 
: 
	Attribute %
{ 
} 
}		 
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
	namespace 	
HeroBot
 
. 
Common 
. 

Attributes #
{ 
[ 
AttributeUsage 
( 
AttributeTargets $
.$ %
Class% *
)* +
]+ ,
public 

class 
ServiceAttribute !
:" #
	Attribute$ -
{ 
} 
}		 √
jC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Contexts\CancelableSocketContext.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Contexts !
{ 
public		 

class		 #
CancelableSocketContext		 (
:		) * 
SocketCommandContext		+ ?
{

 
public #
CancelableSocketContext &
(& '
DiscordSocketClient' :
client; A
,A B
SocketUserMessageC T
msgU X
)X Y
:Z [
base\ `
(` a
clienta g
,g h
msgi l
)l m
{ 	
} 	
public 
bool 
CooldownCancelled %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
=4 5
false6 ;
;; <
} 
} ª
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
} „
gC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\EmbedBuilderExtension.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Helpers  
{ 
public 

static 
class !
EmbedBuilderExtension -
{ 
public 
static 
Random 
Random #
=$ %
new& )
Random* 0
(0 1
)1 2
;2 3
public		 
static		 
Color		 
[		 
]		 
Colors		 $
=		% &
new		' *
[		* +
]		+ ,
{		- .
new		/ 2
Color		3 8
(		8 9
$num		9 <
,		< =
$num		> A
,		A B
$num		C F
)		F G
,		G H
new		I L
Color		M R
(		R S
$num		S V
,		V W
$num		X [
,		[ \
$num		] `
)		` a
,		a b
new		c f
Color		g l
(		l m
$num		m p
,		p q
$num		r u
,		u v
$num		w z
)		z {
,		{ |
new			} Ä
Color
		Å Ü
(
		Ü á
$num
		á ä
,
		ä ã
$num
		å è
,
		è ê
$num
		ë î
)
		î ï
,
		ï ñ
new
		ó ö
Color
		õ †
(
		† °
$num
		° §
,
		§ •
$num
		¶ ©
,
		© ™
$num
		´ Æ
)
		Æ Ø
}
		∞ ±
;
		± ≤
public

 
static

 
EmbedBuilder

 "
WithRandomColor

# 2
(

2 3
this

3 7
EmbedBuilder

8 D
embedBuilder

E Q
)

Q R
{ 	
return 
embedBuilder 
.  
	WithColor  )
() *
Colors* 0
[0 1
Random1 7
.7 8
Next8 <
(< =
)= >
%? @
ColorsA G
.G H
LengthH N
]N O
)O P
;P Q
} 	
public 
static 
EmbedBuilder "
WithCopyrightFooter# 6
(6 7
this7 ;
EmbedBuilder< H
embedBuilderI U
,U V
stringW ]
userName^ f
=g h
nulli m
,m n
stringo u
commandv }
=~ 
null
Ä Ñ
)
Ñ Ö
{ 	
return 
embedBuilder 
.   
WithCurrentTimestamp  4
(4 5
)5 6
.6 7

WithFooter7 A
(A B
newB E
EmbedFooterBuilderF X
(X Y
)Y Z
.Z [
WithIconUrl[ f
(f g
$str	g √
)
√ ƒ
.
ƒ ≈
WithText
≈ Õ
(
Õ Œ
$"
Œ –
{
– —
(
— “
command
“ Ÿ
==
⁄ ‹
null
› ·
?
‚ „
String
‰ Í
.
Í Î
Empty
Î 
:
Ò Ú
$"
Û ı
Command 
ı ˝
{
˝ ˛
command
˛ Ö
}
Ö Ü
"
Ü á
)
á à
}
à â
 ¬© HeroBot 
â î
{
î ï
DateTime
ï ù
.
ù û
Now
û °
.
° ¢
Year
¢ ¶
}
¶ ß
 ‚Ä¢ 
ß ™
{
™ ´
(
´ ¨
userName
¨ ¥
==
µ ∑
null
∏ º
?
Ω æ
String
ø ≈
.
≈ ∆
Empty
∆ À
:
Ã Õ
$"
Œ –
Requested by 
– ›
{
› ﬁ
userName
ﬁ Ê
}
Ê Á
"
Á Ë
)
Ë È
}
È Í
"
Í Î
)
Î Ï
)
Ï Ì
;
Ì Ó
} 	
} 
} ‰
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\ExtensionHelpers.cs
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
class 
ExtensionHelper '
{ 
public		 
static		 
string		 
SanitzeEntity		 *
(		* +
this		+ /
string		0 6
entity		7 =
)		= >
{

 	
return 
entity 
. 
Replace !
(! "
$str" *
,* +
string, 2
.2 3
Empty3 8
)8 9
;9 :
} 	
public 
static 
string 
SanitizAssembly ,
(, -
this- 1
string2 8
module9 ?
)? @
{ 	
return 
module 
. 
Replace !
(! "
$str" ,
,, -
string. 4
.4 5
Empty5 :
): ;
;; <
} 	
public 
static 
string 
ObjectToString +
(+ ,
this, 0
object1 7
obj8 ;
); <
{ 	
var 
sf 
= 
new 
StringBuilder &
(& '
)' (
;( )
var 

properties 
= 
obj  
.  !
GetType! (
(( )
)) *
.* + 
GetRuntimeProperties+ ?
(? @
)@ A
;A B
var 
max 
= 

properties  
.  !
Max! $
($ %
x% &
=>' )
x* +
.+ ,
Name, 0
.0 1
Length1 7
)7 8
+9 :
$num; <
;< =
foreach 
( 
var 
property !
in" $

properties% /
)/ 0
{ 
var 
space 
= 
new 
string  &
(& '
$char' *
,* +
max, /
-0 1
property2 :
.: ;
Name; ?
.? @
Length@ F
)F G
;G H
var 
value 
= 
property $
.$ %
GetValue% -
(- .
obj. 1
)1 2
;2 3
sf 
. 
Append 
( 
$" 
{ 
property %
.% &
Name& *
}* +
{+ ,
space, 1
}1 2
: 2 4
{4 5
value5 :
}: ;
\n; =
"= >
)> ?
;? @
} 
return!! 
$"!! 
```ini\n===== [ !! %
{!!% &
obj!!& )
.!!) *
GetType!!* 1
(!!1 2
)!!2 3
.!!3 4
Name!!4 8
}!!8 9"
 Information ] =====\n!!9 O
{!!O P
sf!!P R
.!!R S
ToString!!S [
(!![ \
)!!\ ]
}!!] ^
\n```!!^ c
"!!c d
;!!d e
}"" 	
}## 
}$$ ù	
`C:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Helpers\TimeSpanHelper.cs
	namespace 	
HeroBot
 
. 
Common 
. 
Helpers  
{ 
public 

static 
class 
TimeSpanHelper &
{ 
private 
static 
Regex 
Regex "
{# $
get% (
;( )
set* -
;- .
}/ 0
=1 2
new3 6
Regex7 <
(< =
$str= O
)O P
;P Q
public		 
static		 
string		 
ToHumanReadable		 ,
(		, -
this		- 1
TimeSpan		2 :
timeSpan		; C
)		C D
{

 	
return 
Regex 
. 
Replace  
(  !
string! '
.' (
Format( .
(. /
$str/ Q
,Q R
timeSpanS [
)[ \
,\ ]
string] c
.c d
Emptyd i
)i j
;j k
} 	
} 
} ©	
cC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\AssemblyEntity.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

class 
AssemblyEntity 
{		 
public

 
Assembly

 
Assembly

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
public 
ModuleLoadContext  
Context! (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
public 
List 
< 

ModuleInfo 
> 
Module  &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
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
} Ê
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\ICooldownService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
ICooldownService %
{ 
Task 
< 
TimeSpan 
? 
> 
IsModuleCooldowned *
(* +
ulong+ 0
userid1 7
,7 8
string9 ?

moduleName@ J
)J K
;K L
Task		 
<		 
TimeSpan		 
?		 
>		 
IsCommandCooldowned		 +
(		+ ,
ulong		, 1
userid		2 8
,		8 9
string		: @
commandName		A L
)		L M
;		M N
}

 
} Ò
eC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IDatabaseService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
IDatabaseService %
{ 
public 
IDbConnection 
GetDbConnection ,
(, -
)- .
;. /
public		 
IDbConnection		 
GetDbConnection		 ,
(		, -
string		- 3
v		4 5
)		5 6
;		6 7
}

 
} Î
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IModulesService.cs
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
IModulesService $
{ 
Task		 
DisablePlugin		 
(		 
IGuild		 !
guild		" '
,		' (

ModuleInfo		) 3

moduleInfo		4 >
)		> ?
;		? @
Task

 
EnablePlugin

 
(

 
IGuild

  
guild

! &
,

& '

ModuleInfo

( 2

moduleInfo

3 =
)

= >
;

> ?
AssemblyEntity %
GetAssemblyEntityByModule 0
(0 1

ModuleInfo1 ;

moduleInfo< F
)F G
;G H
Task 
< 
bool 
> 
IsPluginEnabled "
(" #
IGuild# )
guild* /
,/ 0

ModuleInfo1 ;

moduleInfo< F
)F G
;G H
} 
} ⁄
dC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IPluginRefferal.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
IPluginRefferal $
{ 
} 
} ô
bC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\Interfaces\IRedisService.cs
	namespace 	
HeroBot
 
. 
Common 
. 

Interfaces #
{ 
public 

	interface 
IRedisService "
{ 
ISubscriber 
GetSubscriber !
(! "
)" #
;# $
IDatabaseAsync 
GetDatabase "
(" #
)# $
;$ %
}		 
}

 Ô
cC:\Users\Matthieu\source\repos\HeroBot-deux-point-z√©ro\HeroBot.Common\SimpleCacheImplementation.cs
	namespace 	
HeroBot
 
. 
Common 
{ 
public 

class %
SimpleCacheImplementation *
{		 
private

 
readonly

 
IDatabaseAsync

 '
_redis

( .
;

. /
public %
SimpleCacheImplementation (
(( )
IRedisService) 6
redisService7 C
)C D
{ 	
_redis 
= 
redisService !
.! "
GetDatabase" -
(- .
). /
;/ 0
} 	
public 
Task 
CacheValueAsync #
(# $
string$ *
key+ .
,. /
string0 6
value7 <
)< =
{ 	
return 
_redis 
. 
StringSetAsync (
(( )
key) ,
,, -
value. 3
,3 4
TimeSpan5 =
.= >
FromSeconds> I
(I J
$numJ M
)M N
)N O
;O P
} 	
public 
async 
Task 
< 

RedisValue $
>$ %
GetValueAsync& 3
(3 4
string4 :
key; >
)> ?
{ 	
if 
( 
await 
_redis 
. 
KeyExistsAsync +
(+ ,
key, /
)/ 0
)0 1
await 
_redis 
. 
KeyExpireAsync +
(+ ,
key, /
,/ 0
TimeSpan1 9
.9 :
FromSeconds: E
(E F
$numF I
)I J
)J K
;K L
return 
await 
_redis 
.  
StringGetAsync  .
(. /
key/ 2
)2 3
;3 4
} 	
public 
Task  
InvalidateValueAsync (
(( )
string) /
key0 3
)3 4
{ 	
return 
_redis 
. 
KeyDeleteAsync (
(( )
key) ,
), -
;- .
} 	
} 
}   