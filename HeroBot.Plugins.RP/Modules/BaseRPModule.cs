using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.RP.Services;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.RP.Modules
{
    [NeedPlugin()]
    public class BaseRPModule : ModuleBase<SocketCommandContext>
    {
        private readonly RPService _rp;
        private readonly Random _random;

        private readonly string[] gear = new[] {
            "⭐",
            "😎",
            "🎉",
            "❌"
        };

        public BaseRPModule(RPService rPService,Random random) {
            _rp = rPService;
            _random = random;
        }

        [Command("me")]
        public async Task Me()
        {
            RPUser userAccount = null;
            if (_rp.GetAccount(Context.User, out userAccount))
            {
                var embed = new EmbedBuilder();
                embed.WithAuthor(Context.User)
                    .WithRandomColor()
                    .WithCopyrightFooter(Context.User.Username,"me")
                    .AddField("Ressources",$"gold: {userAccount.Gold}\nbolt: {userAccount.Bolt}");
                await ReplyAsync(embed: embed.Build());
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }

        [Command("lottery")]
        public async Task Lottery() {
            var str = new StringBuilder("** | **");
            for (int i = 0; i < 3*3; i++) {
                if ((i % 3) == 0 && i != 0) str.Append("\n** | **");
                str.Append("`").Append(gear[_random.Next() % gear.Length]).Append("`** | **");
            }
            await ReplyAsync(str.ToString());
        }

        [Command("start")]
        public async Task StartRP() {
            RPUser userAccount = null;
            if (!_rp.GetAccount(Context.User, out userAccount) && await _rp.Start(Context.User))
            {
                    await ReplyAsync(":tada: Welcome to the HeroBot's role-play game !");
            }
        }

        [Command("daily")]
        [Cooldown(86400)]
        public async Task Daily() {
            RPUser userAccount = null;
            if (_rp.GetAccount(Context.User, out userAccount))
            {
                var gain = _random.Next() % 200;
                userAccount.Bolt += gain;
                _rp.SetAccount(Context.User,userAccount);
                await ReplyAsync($"You won **{gain}** bolts");
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }

        [Command("hourly")]
        [Cooldown(3600)]
        public async Task Hourly() {
            RPUser userAccount = null;
            if (_rp.GetAccount(Context.User, out userAccount))
            {
                var gain = _random.Next() % 20;
                userAccount.Bolt += gain;
                _rp.SetAccount(Context.User, userAccount);
                await ReplyAsync($"You won **{gain}** bolts");
            }
            else await ReplyAsync("... I can't find your account `hb!start`");
        }

        [Command("pay")]
        public async Task Pay(SocketUser target,String ressource,int amount) {
            var ress = RPService.ToRessourceEnum(ressource);
            RPUser userAccount = null;
            RPUser targetAccount = null;
            if (_rp.GetAccount(Context.User, out userAccount)) {
                if (ress == Ressources.BOLT)
                {
                    if (userAccount.Bolt < amount)
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

                if (_rp.GetAccount(target,out targetAccount))
                {
                    if (ress == Ressources.BOLT)
                    {
                        targetAccount.Bolt += amount;
                        userAccount.Bolt -= amount;
                        _rp.SetAccount(Context.User, userAccount);
                        _rp.SetAccount(target, targetAccount);
                    }
                    else
                    {
                        targetAccount.Gold += amount;
                        userAccount.Gold -= amount;
                    }
                    _rp.SetAccount(Context.User, userAccount);
                    _rp.SetAccount(target, targetAccount);
                }
                else await ReplyAsync($"... I can't find {target.Mention}'s account `hb!start`");
            }
            else await ReplyAsync($"... I can't find {Context.User.Mention}'s account `hb!start`");
        }


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
