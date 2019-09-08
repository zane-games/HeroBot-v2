using Discord;
using Discord.Commands;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Plugins.GiveAway.Services;
using System;
using System.Threading.Tasks;

namespace HeroBot.Plugins.GiveAway.Modules
{
    [NeedPlugin]
    public class GiveAwayModule : ModuleBase<SocketCommandContext>
    {
        private readonly GiveAwayService _service;

        public GiveAwayModule(GiveAway.Services.GiveAwayService _giveAwayService) {
            _service = _giveAwayService;
        }

        [RequireUserPermission(GuildPermission.ManageChannels),
            RequireBotPermission(GuildPermission.Administrator),
            Command("giveaway")]
        public async Task GiveAway(ITextChannel channel, TimeSpan time,int winners, [Remainder]String price) {
            var embed = new EmbedBuilder() {
                Description = $"{time.ToHumanReadable()} remaining. {winners} winners.",
                Title = price
            }.WithCopyrightFooter().WithRandomColor();
            var message = await channel.SendMessageAsync(embed: embed.Build());
            await message.AddReactionAsync(new Emoji("🎉"));
            await _service.CreateGiveaway(channel, message,time,price,winners);
        }
    }
}
