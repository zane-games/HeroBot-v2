using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Contexts;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.RP.Entities;
using HeroBot.Plugins.RP.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.RP.Modules
{
    [NeedPlugin()]
    public class BaseRPModule : ModuleBase<CancelableSocketContext>
    {
        private readonly RPService _rp;
        private readonly Random _random;

        private readonly object[] gear = new[] {
            new {emoji = "👀", money = 2 },
            new {emoji = "🎮", money = 6 } ,
            new {emoji = "😘", money = 10 }
        };

        public BaseRPModule(RPService rPService, Random random)
        {
            _rp = rPService;
            _random = random;
        }

        [Command("me"),Alias(new[] { "profil","profile" })]
        public async Task Me(IUser user = null)
        {
            if (user == null)
                user = Context.User;
            var userg = await _rp.GetRPUser(user);
            if (userg != null)
            {
                var embed = new EmbedBuilder()
                {
                    Title = $"{userg.Emoji} {user.Username}'s profile",
                    Description = $"{userg.Description}",
                    Url = userg.Website ?? null,
                    ThumbnailUrl = user.GetAvatarUrl(),
                    Fields = new System.Collections.Generic.List<EmbedFieldBuilder>() {
                        new EmbedFieldBuilder() {
                            IsInline = true,
                            Name = "Likes \\👍",
                            Value = $"{userg.Likes} `👍`"
                        },
                        new EmbedFieldBuilder() {
                            IsInline = true,
                            Name = "Money",
                            Value = $"{userg.Money} 💸"
                        },
                        new EmbedFieldBuilder() {
                            IsInline = true,
                            Name = "Personality",
                            Value = $"{(userg.Personality == String.Empty ? "*not definied*" : userg.Personality )}"
                        },
                        new EmbedFieldBuilder() {
                            IsInline = true,
                            Name = "Job",
                            Value = $"{userg.Job.GetDescription()}"
                        }
                    }
                }.WithRandomColor();
                await ReplyAsync(embed: embed.Build());
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }
        [Command("like")]
        [Cooldown(86400)]
        public async Task Like(IUser user) {
            if (user.Id == Context.User.Id) {
                await ReplyAsync("You can't like your own profile.");
                return;
            }
            var userg = await _rp.GetRPUser(Context.User);
            if (userg != null) {
                var target = await _rp.GetRPUser(user);
                if (target != null)
                {
                    target.Likes++;
                    await _rp.UpdateUser(target);
                    await ReplyAsync($"You liked the {user.Mention}'s profile  \\👍");
                }
                else { await ReplyAsync($"{user.Mention} doesn't have an account.");  this.Context.CooldownCancelled = true; }
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }
        [Cooldown(5)]
        [Command("lottery")]
        public async Task Lottery(int prize)
        {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                if (user.Money > 0 && prize < user.Money)
                {
                    user.Money -= prize;
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
                        user.Money += array[1][0].money * prize;

                    }
                    sb.Append(v ? $"**Congratulations ! You won {array[1][0].money * prize} 💸!**\r\n" : "*Oops, you lost...*\r\n");
                    foreach (dynamic ar in array)
                    {
                        foreach (dynamic value in ar)
                        {
                            sb.Append("| `").Append(value.emoji).Append("`");
                        }
                        sb.Append(" |\r\n");
                    }
                    await ReplyAsync(sb.ToString());
                    await _rp.UpdateUser(user);
                }
                else { await ReplyAsync($"You don't have {prize} 💸"); }
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }

        [Command("start")]
        public async Task StartRP()
        {
            if (await _rp.GetRPUser(Context.User) == null && await _rp.CreateUser(Context.User))
            {
                await ReplyAsync(":tada: Welcome to the HeroBot's role-play game !");
            }
            else await ReplyAsync("Im me semble que ton compte existe deja :/");
        }

        [Command("daily")]
        [Cooldown(86400)]
        public async Task Daily()
        {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var gain = _random.Next() % 200;
                user.Money += gain;
                await _rp.UpdateUser(user);
                await ReplyAsync($"You won **{gain}** 💸");
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }

        [Command("hourly")]
        [Cooldown(3600)]
        public async Task Hourly()
        {
            var user = await _rp.GetRPUser(Context.User);
            if (user != null)
            {
                var gain = _random.Next() % 20;
                user.Money += gain;
                await _rp.UpdateUser(user);
                await ReplyAsync($"You won **{gain}** 💸");
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }

        [Command("pay")]
        public async Task Pay(IUser target, int amount)
        {
            if (target.Id == Context.User.Id) { await ReplyAsync("You can't pay yourself..."); return; }
            var userAccount = await _rp.GetRPUser(Context.User);
            if (userAccount != null)
            {
                if (amount > 0 && userAccount.Money >= amount)
                {
                    var targetAccount = await _rp.GetRPUser(target);
                    if (targetAccount != null)
                    {
                        targetAccount.Money += amount;
                        userAccount.Money -= amount;
                        await _rp.UpdateUser(targetAccount);
                        await _rp.UpdateUser(userAccount);
                        try
                        {
                            await target.SendMessageAsync($"{Context.User.Username} gived you {amount}💸");
                        }
                        catch (Exception) { /* Ignore the mp's exceptions */}
                        await ReplyAsync("Payment performed.");
                    }
                    else { await ReplyAsync($"... I can't find {target.Mention}'s account `hb!start`"); this.Context.CooldownCancelled = true; }
                }
                else await ReplyAsync($"{target.Mention} do not have {amount}💸");
            }
            else { await ReplyAsync("... I can't find your account `hb!start`"); this.Context.CooldownCancelled = true; }
        }
        [Group("profile")]
        public class Profile : ModuleBase<SocketCommandContext>
        {
            private readonly RPService _rp;

            public Profile(RPService rPService)
            {
                _rp = rPService;
            }

            [Command("description")]
            public async Task Description([Remainder]string description = null)
            {
                var user = await _rp.GetRPUser(Context.User);
                if (user != null)
                {
                    if (description == null)
                    {
                        await ReplyAsync($"Your description is ```{user.Description}```");
                    }
                    else {
                        user.Description = description;
                        await _rp.UpdateUser(user);
                        await ReplyAsync($"You description is now ```{user.Description}```");
                    }
                }
            }
            [Command("emoji")]
            public async Task Emote([Remainder]string emote = null)
            {
                var user = await _rp.GetRPUser(Context.User);
                if (user != null)
                {
                    if (emote == null)
                    {
                        await ReplyAsync($"Your emote is `{user.Emoji}`");
                    }
                    else
                    {
                        user.Emoji = emote;
                        await _rp.UpdateUser(user);
                        await ReplyAsync($"You emote is now `{user.Emoji}`");
                    }
                }
            }
            [Command("personality")]
            public async Task Personality([Remainder]string personality = null)
            {
                var user = await _rp.GetRPUser(Context.User);
                if (user != null)
                {
                    if (personality == null)
                    {
                        await ReplyAsync($"Your personality is `{user.Personality}`");
                    }
                    else
                    {
                        if (personality.Length < 51)
                        {
                            user.Personality = personality;
                            await _rp.UpdateUser(user);
                            await ReplyAsync($"You personality is now ```{user.Personality}```");
                        }
                        else await ReplyAsync($"Please, the maximum length is 50...");
                    }
                }
            }
            [Command("website")]
            public async Task Website([Remainder]string website = null)
            {
                var user = await _rp.GetRPUser(Context.User);
                if (user != null)
                {
                    if (website == null)
                    {
                        await ReplyAsync($"Your website is `{user.Website}`");
                    }
                    else
                    {
                            user.Website = website;
                            await _rp.UpdateUser(user);
                            await ReplyAsync($"You website is now ```{user.Website}```");
                    }
                }
            }
        }


        [Command("8ball")]
        public async Task EightBall([Remainder]string question)
        {
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