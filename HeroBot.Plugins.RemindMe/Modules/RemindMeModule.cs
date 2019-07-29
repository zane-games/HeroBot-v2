using Discord;
using Discord.Commands;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.RemindMe.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace HeroBot.Plugins.RemindMe.Modules
{
    [NeedPlugin()]

    [Name("Reminder")]
    public class RemindMeModule : ModuleBase<SocketCommandContext>
    {
        private ReminderService _remainder;

        public RemindMeModule(ReminderService remainderService) {
            _remainder = remainderService;
        }
        [Cooldown(500)]
        [Command("remindme"),Alias("remind")]
        public async Task CreateReminder(TimeSpan time,[Remainder]string toRemind) {
            await _remainder.CreateReminder(new Reminder()
            {
                remind = toRemind,
                TimeSpan = time,
                userId = Context.User.Id
            });
            await ReplyAsync($":white_check_mark: I will remind you in {time.ToHumanReadable()}s");
        }
        [Cooldown(5)]
        [Command("myreminders"), Alias("mr")]
        public async Task MyReminders() {
            var reminders = await _remainder.GetReminders(Context.User.Id);
            var embed = new EmbedBuilder()
                .WithRandomColor()
                .WithCopyrightFooter(Context.User.Username, "myreminders")
                .WithTitle("Here is your active reminders");
            foreach (dynamic d in reminders) {
                if(!d.anulated)
                embed.AddField($"**`{d.x.reason}`**", $"*{((TimeSpan)d.r).ToHumanReadable()} remaining*", true);
            }
            await ReplyAsync(embed: embed.Build());
            
        }
    }
}
