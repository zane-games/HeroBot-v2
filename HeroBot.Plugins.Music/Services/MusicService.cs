using Discord.WebSocket;
using HeroBot.Common.Attributes;
using Lavalink4NET;
using Lavalink4NET.Cluster;
using Lavalink4NET.Discord_NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
namespace HeroBot.Plugins.Music.Services
{
    [Service]
    public class MusicService
    {
        private DiscordShardedClient _discord;
        private LavalinkCluster lavalinkCluster;
        public MusicService(DiscordShardedClient discordShardedclient)
        {
            _discord = discordShardedclient;
            var dnsLavalinks = ScanDns();

            var LavalinkNodeOptions = new List<LavalinkNodeOptions>();
            foreach (var ip in dnsLavalinks)
            {
                LavalinkNodeOptions.Add(new Lavalink4NET.LavalinkNodeOptions()
                {
                    RestUri = $"http://{ip.ToString()}:8080/",
                    WebSocketUri = $"ws://{ip.ToString()}:8080/",
                    Password = "123",
                    AllowResuming = true
                });
            }

            lavalinkCluster = new LavalinkCluster(new LavalinkClusterOptions()
            {
                StayOnline = true,
                Nodes = LavalinkNodeOptions.ToArray(),
                LoadBalacingStrategy = LoadBalancingStrategies.ScoreStrategy
            }, new DiscordClientWrapper(discordShardedclient));
            discordShardedclient.ShardReady += DiscordShardedclient_ShardReady;
        }

        private async System.Threading.Tasks.Task DiscordShardedclient_ShardReady(DiscordSocketClient arg)
        {
            if (_discord.Shards.Where(x => x.ConnectionState == Discord.ConnectionState.Connected).Count() == _discord.Shards.Count)
            {
                await lavalinkCluster.InitializeAsync();
            }
        }

        private IEnumerable<IPAddress> ScanDns()
        {
            IPAddress[] ipaddress = Dns.GetHostAddresses("localhost");
            return ipaddress.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        }
    }
}
