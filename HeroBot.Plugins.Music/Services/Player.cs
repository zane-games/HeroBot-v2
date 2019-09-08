using Discord;
using HeroBot.Common.Helpers;
using Lavalink4NET;
using Lavalink4NET.Events;
using Lavalink4NET.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Music.Services
{
    class Player : QueuedLavalinkPlayer
    {
        internal IMessageChannel Channel { get; set; }

        public Player(LavalinkSocket lavalinkSocket, IDiscordClientWrapper client, ulong guildId, bool disconnectOnStop) : base(lavalinkSocket, client, guildId, disconnectOnStop)
        {
        }

        public override async Task OnTrackEndAsync(TrackEndEventArgs eventArgs)
        {
            await base.OnTrackEndAsync(eventArgs);
            if(eventArgs.MayStartNext && !Queue.IsEmpty && !IsLooping)
                await this.Channel.SendMessageAsync($"Now playing `{CurrentTrack.Title}` :play:");
        }
    }
}
