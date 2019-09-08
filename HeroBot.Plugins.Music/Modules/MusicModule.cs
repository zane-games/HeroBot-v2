using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
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
            payer.Channel = Context.Channel;
            await ReplyAsync($"I'm now connected to `<#{payer.VoiceChannelId}>`");
        }

        [Command("play")]
        [Voice(true, true)]
        public async Task Play([Remainder]string search) {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (search.StartsWith("http"))
            {
                var result = await _music.GetLavalinkCluster().GetTrackAsync(search, Lavalink4NET.Rest.SearchMode.None);
                if (result != null)
                {
                    if (!result.IsLiveStream)
                    {
                        await player.PlayAsync(result);
                        await ReplyAsync($"Added `{result.Title} - {result.Author}` to the queue");
                    }
                    else
                    {
                        await ReplyAsync("I can't play livestreams for now");
                    }
                }
                else await ReplyAsync("I can't find any song in this url :/");
            }
            else
            {
                var provider = Lavalink4NET.Rest.SearchMode.YouTube;
                var result = await _music.GetLavalinkCluster().GetTracksAsync(search, provider);
                if (result.Any())
                {
                    var song = result.First();
                    if (!song.IsLiveStream)
                    {
                        await player.PlayAsync(song);
                        await ReplyAsync($"Added `{song.Title} - {song.Author}` to the queue");
                    }
                    else {
                        await ReplyAsync("I can't play livestreams for now");
                    }
                }
                else await ReplyAsync("I can't find any song :/");
            }
        }
        [Voice(true, true)]
        [Command("youtube")]
        public async Task YouTube([Remainder]string search) {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            var provider = Lavalink4NET.Rest.SearchMode.YouTube;
            var result = await _music.GetLavalinkCluster().GetTracksAsync(search, provider);
            if (result.Any())
            {
                var song = result.First();
                if (!song.IsLiveStream)
                {
                    await player.PlayAsync(song);
                    await ReplyAsync($"Added `{song.Title} - {song.Author}` to the queue");
                }
                else
                {
                    await ReplyAsync("I can't play livestreams for now");
                }
            }
            else await ReplyAsync("I can't find any song :/");
        }
        [Voice(true, true)]
        [Command("soundcloud")]
        public async Task SoundCloud([Remainder]string search)
        {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            var provider = Lavalink4NET.Rest.SearchMode.SoundCloud;
            var result = await _music.GetLavalinkCluster().GetTracksAsync(search, provider);
            if (result.Any())
            {
                var song = result.First();
                if (!song.IsLiveStream)
                {
                    await player.PlayAsync(song);
                    await ReplyAsync($"Added `{song.Title} - {song.Author}` to the queue");
                }
                else
                {
                    await ReplyAsync("I can't play livestreams for now");
                }
            }
            else await ReplyAsync("I can't find any song :/");
        }

        [Voice(true, true)]
        [Command("volume")]
        public async Task SetVolume(float volume) {
            if (volume <= 1000 && volume > 0)
            {
                var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
                await player.SetVolumeAsync(volume / 100);
                await player.Channel.SendMessageAsync($"Volume set to {volume}%");
            }
            else
                await ReplyAsync("Volume must br greater than 0 and lower than 1000");
        }
        [Voice(true, true)]
        [Command("skip")]
        public async Task Skip(int count = 1) {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.IsLooping) {
                await player.Channel.SendMessageAsync("You can't skip a sonf if the loop is enabled");
                return;
            }
            await player.SkipAsync(count);
            await ReplyAsync($"I've skiped {count} sounds ^^");
        }
        [Voice(true, true)]
        [Command("textChannel")]
        public async Task ChangeChannel(IMessageChannel socketChannel = null) {
            if (socketChannel == null)
                socketChannel = Context.Channel;
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            player.Channel = socketChannel;
            await socketChannel.SendFileAsync("Now, i will send the music messages here :)");
        }

        [Voice(true, true)]
        [Command("queue")]
        public async Task Queue() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.Channel == null) player.Channel = Context.Channel;
            var queue = player.Queue;

            if (queue.Count == 0) { await ReplyAsync("There is no song in the queue"); return; }
            var sb = new StringBuilder($"There is {queue.Count} song(s) in the queue :\r\n");
            foreach (var song in queue) {
                sb.Append("**~>** *").Append(song.Title).Append(" - ").Append(song.Author).Append("*\r\n");
            }
            await ReplyAsync(embed: new EmbedBuilder()
            {
                Title = "Songs in the queue",
                Description = sb.ToString()
            }.WithRandomColor().Build());
        }
        [Voice(true, true)]
        [Command("stop")]
        public async Task StopMusic() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            await player.DestroyAsync();
            await ReplyAsync("Stopped the music");
        }
        [Voice(true, true)]
        [Command("pause")]
        public async Task Pause() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            if (player.State == Lavalink4NET.Player.PlayerState.Paused)
            {
                await player.ResumeAsync();
                await player.Channel.SendMessageAsync("Resumed the music");
            }
            else {
                await player.PauseAsync();
                await player.Channel.SendMessageAsync("Paused the music");
            }
        }
        [Voice(true, true)]
        [Command("loop")]
        public async Task Loop()
        {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            player.IsLooping = !player.IsLooping;
            await player.Channel.SendMessageAsync($"Looping: {(player.IsLooping ? "Enabled" : "Disabled")}");
        }
        [Command("nowplaying")]
        [Alias(new[] { "np", "playing" })]
        public async Task NowPlaying() {
            var player = _music.GetLavalinkCluster().GetPlayer<Player>(Context.Guild.Id);
            var embed = new EmbedBuilder() {
                Title = player.CurrentTrack.Title,
                Description = MakeBar(player.CurrentTrack.Duration,player.TrackPosition),
                Fields = new System.Collections.Generic.List<EmbedFieldBuilder>() {
                    new EmbedFieldBuilder() {
                        IsInline = true,
                        Name = "Auteur",
                        Value = player.CurrentTrack.Author
                    }, new EmbedFieldBuilder() {
                        IsInline = true,
                        Name = "Plateforme",
                        Value = player.CurrentTrack.Provider
                    }
                }
            }.WithRandomColor().Build();
            await ReplyAsync(embed: embed);
        }
        private string MakeBar(TimeSpan duration, TimeSpan trackPosition)
        {
            const int count = 10;
            const char progress = '=';
            var firstcount = Convert.ToInt32((trackPosition.TotalSeconds * count) / duration.TotalSeconds);
            return $"`[{new String(progress,firstcount)}>{new String(progress,count - firstcount)}] {(trackPosition).ToHumanReadable()}/{duration.ToHumanReadable()}`";
        }
    }
}
