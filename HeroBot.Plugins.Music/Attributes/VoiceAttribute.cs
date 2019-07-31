using Discord.Commands;
using HeroBot.Plugins.Music.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Music.Attributes
{
    public class VoiceAttribute : PreconditionAttribute
    {
        private readonly bool _playerExists;
        private readonly bool _needUserInVoiceChannel;
        public VoiceAttribute(bool needPlayerExists, bool needUserInvoicechannel)
        {
            _playerExists = needPlayerExists;
            _needUserInVoiceChannel = needUserInvoicechannel;
        }

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var musicService = services.GetService<MusicService>();
            if (_playerExists && !musicService.GetLavalinkCluster().HasPlayer(context.Guild.Id))
            {
  return PreconditionResult.FromError("I'm not connected to any voice channel...");
                
            }
            if (_needUserInVoiceChannel && (await context.Guild.GetUserAsync(context.User.Id)).VoiceChannel == null)
            {

                    return PreconditionResult.FromError("You must be in a voice channel...");
                
            }
            return PreconditionResult.FromSuccess();
        }
    }
}
