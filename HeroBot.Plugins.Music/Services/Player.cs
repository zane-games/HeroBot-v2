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
        bool connected = true;
        internal IMessageChannel socketTextchannel;

        public Player(LavalinkSocket lavalinkSocket, IDiscordClientWrapper client, ulong guildId, bool disconnectOnStop) : base(lavalinkSocket, client, guildId, disconnectOnStop)
        {
        }

        public override async Task OnConnectedAsync(VoiceServer voiceServer, VoiceState voiceState)
        {
            if (connected)
            {
                await socketTextchannel.SendMessageAsync($"I'm now connected to <#{voiceState.VoiceChannelId}>.");
                connected = false;
            }
            await base.OnConnectedAsync(voiceServer, voiceState);
        }
        public override async Task OnTrackExceptionAsync(TrackExceptionEventArgs eventArgs)
        {
            await socketTextchannel.SendMessageAsync($"An exception was throwed during playing a music in <#{eventArgs.Player.VoiceChannelId}>");
            await base.OnTrackExceptionAsync(eventArgs);
        }
        public override async Task OnTrackEndAsync(TrackEndEventArgs eventArgs)
        {
            await base.OnTrackEndAsync(eventArgs);
            if (eventArgs.MayStartNext) {
                var next = this.Queue.Tracks.First();
                await socketTextchannel.SendMessageAsync($"Now playing `{next.Title} - {next.Author}`");
            }
        }
        public override Task OnTrackStuckAsync(TrackStuckEventArgs eventArgs)
        {
            return base.OnTrackStuckAsync(eventArgs);
        }

        public override async Task PauseAsync()
        {
            await base.PauseAsync();
            await socketTextchannel.SendMessageAsync("Music paused !");
        }

        public override async Task DisconnectAsync()
        {
            await base.DisconnectAsync();
            await socketTextchannel.SendMessageAsync("I'm now disconnected !");
        }

        public override async Task ReplayAsync()
        {
            await base.ReplayAsync();
            await socketTextchannel.SendMessageAsync($"Replaying {CurrentTrack.Title} - {CurrentTrack.Author}");
        }

        public override async Task ResumeAsync()
        {
            await base.ResumeAsync();
            await socketTextchannel.SendMessageAsync("Resuming the music");
        }

        public override async Task SeekPositionAsync(TimeSpan position)
        {
            await base.SeekPositionAsync(position);
            await socketTextchannel.SendMessageAsync($"Playing {CurrentTrack.Title} - {CurrentTrack.Author} at {position.ToHumanReadable()}");
        }

        public override async Task SetVolumeAsync(float volume = 1, bool normalize = false)
        {
            await base.SetVolumeAsync(volume, normalize);
            await socketTextchannel.SendMessageAsync($"Volume set to {volume}");
        }

        public override Task UpdateEqualizerAsync(IEnumerable<EqualizerBand> bands, bool reset = true)
        {
            return base.UpdateEqualizerAsync(bands, reset);
        }

        public override Task PlayTopAsync(LavalinkTrack track)
        {
            return base.PlayTopAsync(track);
        }

        public override Task<bool> PushTrackAsync(LavalinkTrack track, bool push = false)
        {
            return base.PushTrackAsync(track, push);
        }

        public override async Task SkipAsync(int count = 1)
        {
            await base.SkipAsync(count);
            await socketTextchannel.SendMessageAsync($"Skipped {count} song.");
        }

        public override async Task StopAsync(bool disconnect = false)
        {
            await base.StopAsync(disconnect);
            await socketTextchannel.SendMessageAsync($"Stopped the music.");
        }
    }
}
