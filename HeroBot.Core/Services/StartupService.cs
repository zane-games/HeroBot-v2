using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HeroBotv2.Services
{
    public class StartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private DiscordBotsList.Api.Objects.IDblSelfBot _self;
        private int[] ShardPresences;
        private readonly string[] presence = new[] {
            "hb!help | Dragon 🐉",
            "hb!help | [GuildCount] guilds",
            "hb!help | Thanks 💕 !",
            "hb!help | [UsersCount] users",
            "hb!help | ❤🚀 v2.0"
        };
        private Task UpdatePresences
        {
            get
            {
                return new Task(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(16000);
                        foreach (DiscordSocketClient discordSocketClient in _discord.Shards)
                        {
                            if (discordSocketClient.ConnectionState == ConnectionState.Connected)
                            {
                                if (discordSocketClient.ShardId > 3)
                                {
                                    if (ShardPresences[discordSocketClient.ShardId] == presence.Length - 1)
                                        ShardPresences[discordSocketClient.ShardId] = 0;
                                    else
                                        ShardPresences[discordSocketClient.ShardId]++;
                                    await discordSocketClient.SetGamewithPlaceholder(_discord, presence[ShardPresences[discordSocketClient.ShardId]]);
                                }
                                else
                                {
                                    if (ShardPresences[0] == presence.Length - 1)
                                        ShardPresences[0] = 0;
                                    else
                                        ShardPresences[0]++;
                                    await discordSocketClient.SetGamewithPlaceholder(_discord, presence[ShardPresences[0]]);
                                }
                            }
                        }
                    }
                });
            }
        }
        // DiscordSocketClient, CommandService, and IConfigurationRoot are injected automatically from the IServiceProvider
        public StartupService(
            IServiceProvider provider,
            DiscordShardedClient discord,
            CommandService commands,
            IConfigurationRoot config)
        {
            _provider = provider;
            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public async Task StartAsync()
        {
            string discordToken = _config["tokens:discord"];     // Get the discord token from the config file
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new InvalidProgramException("Please enter your bot's token into the `_configuration.json` file found in the applications root directory.");
            _discord.ShardConnected += _discord_ShardConnected;
            await _discord.LoginAsync(TokenType.Bot, discordToken);     // Login to discord
            await _discord.StartAsync();                                // Connect to the websocket
            _discord.JoinedGuild += _discord_JoinedGuild;
            _discord.LeftGuild += _discord_LeftGuild;

            ShardPresences = new int[_discord.Shards.Count];
            UpdatePresences.Start();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);     // Load commands and modules into the command service
        }

        private async Task _discord_ShardConnected(DiscordSocketClient arg)
        {
            try
            {
                if (_discord.Shards.Count(x => x.ConnectionState == ConnectionState.Connected) == _discord.Shards.Count)
                {
                    var dblClient = new DiscordBotsList.Api.AuthDiscordBotListApi(_discord.CurrentUser.Id, _config.GetSection("dbl").GetSection("token").Value);
                    _self = await dblClient.GetMeAsync();
                }
            }
            catch { };
        }

        private async Task _discord_LeftGuild(SocketGuild arg)
        {
            try
            {
                await _self.UpdateStatsAsync(_discord.Guilds.Count);
            }
            catch { };
            try
            {
                await (_discord.GetChannel(579320728663359498) as ITextChannel).SendMessageAsync($"<a:leave:616197734978027520> | J'ai quitté le serveur **`{arg.Name}`**. Je suis maintenant sur {_discord.Guilds.Count} serveurs.");
            }
            catch { };
        }

        private async Task _discord_JoinedGuild(SocketGuild arg)
        {
            try
            {
                await _self.UpdateStatsAsync(_discord.Guilds.Count);
            }
            catch { };
            try
            {
                await (_discord.GetChannel(579320728663359498) as ITextChannel).SendMessageAsync($"<a:join:616197720956338176> | J'ai rejoins le serveur **`{arg.Name}`**. Je suis maintenant sur {_discord.Guilds.Count} serveurs.");
            }
            catch { };
            try
            {
                await arg.DefaultChannel.SendMessageAsync($"Hi ! I'm HeroBot, you new discord assistant, you can start configuring me with `hb!plugin list` if you need help, you can look at our wiki (https://doc-herobot.alivecreation.fr/) or at our discord (https://discord.gg/{_config["discord"]})");
            }
            catch { };
        }
    }
    public static class DiscordShardedClientExtension
    {
        public static Task SetGamewithPlaceholder(this DiscordSocketClient discordsocket, DiscordShardedClient discordShardedClient, string str)
        {
            return discordsocket.SetGameAsync(str
                .Replace("[GuildCount]", discordShardedClient.Guilds.Count.ToString())
                .Replace("[UsersCount]", discordShardedClient.Guilds.Sum(x => x.Users.Count).ToString())
                // More here
                );
        }
    }
}
