using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Plugins.Music.Attributes;
using HeroBot.Plugins.Music.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Music.Modules
{
    [NeedPlugin]
    public class MusicModule : ModuleBase<SocketCommandContext>
    {
        private readonly MusicService _music;

        public MusicModule(MusicService musicService) {
            _music = musicService;
        }
        [Voice(false, true)]
        [Command("join")]
        public async Task Join() {
            var playerExists = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (playerExists != null) {
                await ReplyAsync($"I'm already connected to `<#{playerExists.VoiceChannelId}>`");
            }
            var member = Context.Guild.GetUser(Context.User.Id);
            var payer = await _music.GetLavalinkCluster().JoinAsync<Player>(Context.Guild.Id, member.VoiceChannel.Id);
            payer.socketTextchannel = Context.Channel;
        }

        [Command("play")]
        [Voice(true, true)]
        public async Task Play([Remainder]string search) {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.socketTextchannel == null) player.socketTextchannel = Context.Channel;
            if (search.StartsWith("http"))
            {
                var result = await _music.GetLavalinkCluster().GetTrackAsync(search, Lavalink4NET.Rest.SearchMode.None);
                if (result != null)
                {
                    await player.PlayAsync(result);
                    await ReplyAsync($"Added `{result.Title} - {result.Author}` to the queue ");
                }
                else await ReplyAsync("I can't find any song in this url :/");
            }
            else
            {
                var provider = Lavalink4NET.Rest.SearchMode.YouTube;
                if (search.StartsWith("cs:")) {
                    provider = Lavalink4NET.Rest.SearchMode.SoundCloud;
                    search = search.Replace("cs:", String.Empty);
                }
                var result = await _music.GetLavalinkCluster().GetTracksAsync(search, provider);
                if (result.Any())
                {
                    var song = result.First();
                    await player.PlayAsync(song);
                    await ReplyAsync($"Added `{song.Title} - {song.Author}` to the queue ");
                }
                else await ReplyAsync("I can't find any song :/");
            }
        }
        [Voice(true, true)]
        [Command("volume")]
        public async Task SetVolume(float volume) {
            if (volume <= 100 && volume > 0)
            {
                var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
                if (player.socketTextchannel == null) player.socketTextchannel = Context.Channel;

                await player.SetVolumeAsync(volume / 100);
            }
            else
                await ReplyAsync("Volume must br greater than 0 and lower than 100");
        }
        [Voice(true, true)]
        [Command("megavolume")]
        public async Task SetMegaVolume(float volume) {
            if (volume <= 10 && volume > 0)
            {
                var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
                if (player.socketTextchannel == null) player.socketTextchannel = Context.Channel;

                await player.SetVolumeAsync(volume);
            }
            else
                await ReplyAsync("Volume must br greater than 0 and lower than 10");
        }
        [Voice(true, true)]
        [Command("skip")]
        public async Task Skip(int count = 1) {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.socketTextchannel == null) player.socketTextchannel = Context.Channel;
            await player.SkipAsync(count);
        }
        [Voice(true, true)]
        [Command("changeChannel")]
        public async Task ChangeChannel(IMessageChannel socketChannel = null) {
            if (socketChannel == null)
                socketChannel = Context.Channel;
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            player.socketTextchannel = socketChannel;
            await socketChannel.SendFileAsync("");
        }
        [Voice(true, true)]
        [Command("queue")]
        public async Task Queue() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.socketTextchannel == null) player.socketTextchannel = Context.Channel;
            var queue = player.Queue;

            if (queue.Count == 0) { await ReplyAsync("There is no song in the queue"); return; }
            var sb = new StringBuilder($"There is {queue.Count} song(s) in the queue :\r\n");
            foreach (var song in queue) {
                sb.Append("**~>** *").Append(song.Title).Append(" - ").Append(song.Author).Append("*\r\n");
            }
            await ReplyAsync(sb.ToString());
        }
        [Voice(true, true)]
        [Command("stop")]
        public async Task StopMusic() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            await player.StopAsync();
        }
        [Voice(true, true)]
        [Command("disconnect")]
        public async Task Disconnect() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            await player.DisconnectAsync();
            await player.DisposeAsync();
        }
        [Voice(true, true)]
        [Command("resume")]
        public async Task Resume() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            await player.ResumeAsync();
        }
        [Command("pause")]
        public async Task Pause() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            await player.PauseAsync();
        }
        [Command("loop")]
        public Task Loop()
        {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            player.IsLooping = !player.IsLooping;
            return Task.CompletedTask;
        }
    }
}
