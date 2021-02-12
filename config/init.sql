CREATE DATABASE "HeroBot.Core";
CREATE DATABASE "HeroBot.Plugins.Example";
CREATE DATABASE "HeroBot.Plugins.GiveAway";
CREATE DATABASE "HeroBot.Plugins.HeroBot";
CREATE DATABASE "HeroBot.Plugins.Images";
CREATE DATABASE "HeroBot.Plugins.Mod";
CREATE DATABASE "HeroBot.Plugins.Music";
CREATE DATABASE "HeroBot.Plugins.RemindMe";
CREATE DATABASE "HeroBot.Plugins.RP";

create herobot with password 'super secure password';

GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Core" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.Example" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.GiveAway" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.HeroBot" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.Images" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.Mod" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.Music" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.RemindMe" to herobot;
GRANT ALL PRIVILEGES ON DATABASE "HeroBot.Plugins.RP" to herobot;