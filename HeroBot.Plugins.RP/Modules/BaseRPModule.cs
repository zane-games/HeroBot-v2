using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.RP.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.RP.Modules
{
    [NeedPlugin()]
    public class BaseRPModule : ModuleBase<SocketCommandContext>
    {
        private readonly RPService _rp;
        private readonly Random _random;

        private readonly object[] gear = new[] {
            new {emoji = "⭐", money = 100 },
            new {emoji = "🍰", money = 500 } ,
            new {emoji = "💸", money = 800 }
        };

        public BaseRPModule(RPService rPService,Random random) {
            _rp = rPService;
            _random = random;
        }

        [Command("me")]
        public async Task Me()
        {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var embed = new EmbedBuilder() {

                };
                await ReplyAsync(embed: embed.Build());
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }
        [Cooldown(600)]
        [Command("lottery")]
        public async Task Lottery() {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var columnCount = 3;
                var ligneCount = 3;
                var array = new dynamic[ligneCount][];
                for (int i = 0; i < ligneCount; i++)
                {
                    var isMiddle = (ligneCount / 2) == i;
                    array[i] = new dynamic[columnCount];
                    // For each line, we build the array
                    for (int x = 0; x < columnCount; x++)
                    {

                        // We need to fill the array's column
                        array[i][x] = gear[_random.Next(gear.Length)];

                    }
                }

                var sb = new StringBuilder();
                var v = array[1].Count(x =>
                {
                    if (array[1][0].emoji != x.emoji) return false;
                    return true;
                }) == columnCount;
                if (v)
                {
                    _rp.UpdateUser(Context.User, x => { x.Money += array[1][0].money; return x; });
                }
                sb.Append(v ? $"**Congratulations ! You won {array[1][0].money} gold!**\r\n" : "*Sad trombone*\r\n");
                foreach (dynamic ar in array)
                {
                    foreach (dynamic value in ar)
                    {
                        sb.Append("| `").Append(value.emoji).Append("`");
                    }
                    sb.Append(" |\r\n");
                }
                await ReplyAsync(sb.ToString());
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }

        [Command("start")]
        public async Task StartRP() {
            if (await _rp.GetRPUser(Context.User) == null && await _rp.CreateUser(Context.User))
            {
                await ReplyAsync(":tada: Welcome to the HeroBot's role-play game !");
            }
            else await ReplyAsync("Im me semble que ton compte existe deja :/");
        }

        [Command("daily")]
        [Cooldown(86400)]
        public async Task Daily() {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var gain = _random.Next() % 200;
                _rp.UpdateUser(Context.User, x => { x.Money += gain; return x; });
                await ReplyAsync($"You won **{gain}** bolts");
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }

        [Command("hourly")]
        [Cooldown(3600)]
        public async Task Hourly() {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var gain = _random.Next() % 20;
                _rp.UpdateUser(Context.User, x => { x.Money += gain; return x; });
                await ReplyAsync($"You won **{gain}** bolts");
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }
        /*
        [Command("pay")]
        public async Task Pay(SocketUser target,String ressource,int amount) {
            var ress = RPService.ToRessourceEnum(ressource);
            if (_rp.GetAccount(Context.User, out RPUser userAccount)) {
                if (ress == Ressources.BOLT)
                {
                    if (userAccount.Bolts < amount)
                    {
                        await ReplyAsync($"... {Context.User.Mention} You do not have enough bolts");
                        return;
                    }
                }
                else {
                    if (userAccount.Gold < amount)
                    {
                        await ReplyAsync($"... {Context.User.Mention} You do not have enough gold");
                        return;
                    }
                }

                if (_rp.GetAccount(target, out RPUser targetAccount))
                {
                    if (ress == Ressources.BOLT)
                    {
                        targetAccount.Bolts += amount;
                        userAccount.Bolts -= amount;
                        _rp.SetAccount( userAccount);
                        _rp.SetAccount( targetAccount);
                    }
                    else
                    {
                        targetAccount.Gold += amount;
                        userAccount.Gold -= amount;
                    }
                    _rp.SetAccount( userAccount);
                    _rp.SetAccount(targetAccount);
                }
                else await ReplyAsync($"... I can't find {target.Mention}'s account `hb!start`");
            }
            else await ReplyAsync($"... I can't find {Context.User.Mention}'s account `hb!start`");
        }*/


        [Command("8ball")]
        public async Task EightBall([Remainder]string question) {
            var questionRep = new[] {
                "I don't know",
                "Yup",
                "Never",
                "No way !"
            };
            await ReplyAsync($"**{question}** : {questionRep[_random.Next() % questionRep.Length]}");
        }
    }
}
