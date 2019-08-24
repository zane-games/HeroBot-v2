using Discord.WebSocket;
using HeroBot.Common.Attributes;
using Lavalink4NET.Cluster;
using Lavalink4NET.Discord_NET;
using System;

namespace HeroBot.Plugins.Music.Services
{
    [Service]
    public class MusicService
    {
        private readonly DiscordShardedClient _discord;
        private LavalinkCluster lavalinkCluster;
        public MusicService(DiscordShardedClient discordShardedclient)
        {
            _discord = discordShardedclient;
            discordShardedclient.ShardReady += DiscordShardedclient_ShardReady;
        }
        internal LavalinkCluster GetLavalinkCluster() { return lavalinkCluster; }


        private async System.Threading.Tasks.Task DiscordShardedclient_ShardReady(DiscordSocketClient arg)
        {
                lavalinkCluster = new LavalinkCluster(new LavalinkClusterOptions()
                {
                    StayOnline = true,
                    Nodes = new[] { new Lavalink4NET.LavalinkNodeOptions()
                {
                    RestUri = $"http://lavalink.alivecreation.fr:80/",
                    WebSocketUri = $"ws://lavalink.alivecreation.fr:80/",
                    Password = Uri.EscapeDataString("AyF6c62M3Lu2t5jcRCMMfhcGZ34dGjBv95cVPJsbbhKcBUBcnFEDDbvXBzU7EFgAN2ucE2ZLz6gnrwWRDxYKwvWsvqYntxLYYb4quUdhQAPLvDWqYanwAusE3rcGxhyC6aswGgDDWwEZ8ZNWR5bUBWfTm62fzeXafmzjNTwNFRwDz9ksJHj9BCT2MwBgdcqTxpGMQ8QLNQdJqEUmuPR3Xn8SczmecFpjpSfDcC42xAL5LyNQMtZur6YAZRu5t85g"),
                    AllowResuming = true
                }},

                    LoadBalacingStrategy = LoadBalancingStrategies.ScoreStrategy
                }, new DiscordClientWrapper(_discord));
                await lavalinkCluster.InitializeAsync();
            


        }
    }
}
