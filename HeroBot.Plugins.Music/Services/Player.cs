using Discord;
using Discord.WebSocket;
using Lavalink4NET;
using Lavalink4NET.Events;
using Lavalink4NET.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Music.Services
{
    class Player : QueuedLavalinkPlayer
    {

        internal IMessageChannel socketTextchannel;

        public Player(LavalinkSocket lavalinkSocket, IDiscordClientWrapper client, ulong guildId, bool disconnectOnStop) : base(lavalinkSocket, client, guildId, disconnectOnStop)
        {
        }

        public override async Task OnConnectedAsync(VoiceServer voiceServer, VoiceState voiceState)
        {
            await socketTextchannel.SendMessageAsync($"I'm connected to <#{voiceState.VoiceChannelId}>.");
            await base.OnConnectedAsync(voiceServer, voiceState);
        }
        public override async Task OnTrackExceptionAsync(TrackExceptionEventArgs eventArgs)
        {
            await socketTextchannel.SendMessageAsync($"An exception was throwed during playing a music in <#{eventArgs.Player.VoiceChannelId}>");
            await base.OnTrackExceptionAsync(eventArgs);
        }
        public override async Task OnTrackEndAsync(TrackEndEventArgs eventArgs)
        {
            if (eventArgs.MayStartNext) {
                var next = this.Queue.Tracks.First();
                await socketTextchannel.SendMessageAsync($"Now playing `{next.Title} - {next.Author}`");
            }
            await base.OnTrackEndAsync(eventArgs);
        }
    }
}
