using Dapper;
using Discord;
using Discord.Commands;
using Discord.Rest;
using HeroBot.Common.Attributes;
using HeroBot.Common.Entities;
using HeroBot.Common.Helpers;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeroBot.Plugins.HeroBot.Modules
{
    [NeedPlugin()]
    [Cooldown(2)]
    [Name("Basic Module")]
    public class HeroBotModule : ModuleBase<SocketCommandContext>
    {


        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;

        public HeroBotModule(CommandService service, IConfigurationRoot config,IServiceProvider serviceProvider)
        {
            _service = service;
            _config = config;
            _provider = serviceProvider;
        }

        [Command("help"), Alias(new[] { "h", "hh", "hmp" })]
        public async Task HelpCommandAsync([Remainder]string command = null)
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Description = "These are the commands you can use"
            };
            builder.AddField("**HeroBot**", "HeroBot is a bot with a plugin system that can be developped by the community, maintained by **Matthieu#2050**, it is supposed to be able to replace several bots. \n [ [**Our Discord**](https://alivecreation.fr/discord) | [**Website `📟`**](https://herobot.alivecreation.fr) ] \n [ [*Invite HeroBot `😍`*](https://alivecreation.fr/invite) | [*Upvote us on DBL `🎉`*](https://discordbots.org/bot/491673480006205461) ]\r **<> is a required argument, [] is an optional argument** If your argument contains spaces, you must put it behind two `\"` Pro tip : You can use ;hh to send the message in the current channel", false)
            .WithDescription("Hay ! Thanks for using HeroBot ! I hope you enjoy using our bot :P, is you have any suggestions, you can tell us your beautiful idea !")
            .WithAuthor(Context.User)
            .WithThumbnailUrl("https://cdn.discordapp.com/avatars/491673480006205461/30abe7a1feffb0b06a1611a94fbc1248.png")
            .WithRandomColor().WithCopyrightFooter(Context.User.Username, "help");
            if (command != null)
            {
                var result = _service.Search(Context, command);

                if (!result.IsSuccess)
                {
                    await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                    return;
                }

                var cmd = result.Commands.First().Command;
                builder.AddField(x =>
                {
                    x.Name = "Command syntax";
                    x.Value = $"`{prefix}{(cmd.Aliases.Count > 0 ? $"{"{"}{string.Join(",", cmd.Aliases)}{"}"}" : cmd.Name)} {string.Join(" ", cmd.Parameters.Select(p => p.IsOptional ? $"[{p.Name}{(p.IsRemainder ? "..." : String.Empty)}]" : $"<{p.Name}{(p.IsRemainder ? "..." : String.Empty)}>"))}`";
                    x.IsInline = true;
                });
                builder.AddField((x) =>
                {
                    x.Name = "Summary :";
                    x.IsInline = true;
                    x.Value = cmd.Summary == null ? "*no summary*" : cmd.Summary;
                });

            }
            else
            {
                int total = 0;
                int realCommands = 0;
                int subcommands = 0;
                foreach (var module in _service.Modules.Where(x => !x.IsSubmodule && x.Commands.Count > 0))
                {
                    var description = new StringBuilder();
                    var precondition = module.Preconditions.First(x => x is NeedPluginAttribute);
                    
                    var run = await precondition.CheckPermissionsAsync(Context, module.Commands.First(), _provider);
                    if (run.IsSuccess)
                    {
                        foreach (var cmd in module.Commands)
                        {


                            total++;
                            description.Append($"**>** `{prefix}{cmd.Aliases.First()}`");

                        }
                    }

                        if (!string.IsNullOrWhiteSpace(description.ToString()))
                        {
                            builder.AddField(x =>
                            {
                                x.Name = module.Name;
                                x.Value = description;
                                x.IsInline = false;
                            });
                        }
                    
                }
                realCommands = total - subcommands;
                await ReplyAsync($"{realCommands} real commands, {subcommands} sub-commands and {total} total commands commands for {this.Context.User.Mention}", false, builder.Build());
                return;
            }
            await ReplyAsync($"Available commands for {this.Context.User.Mention}", false, builder.Build());
        }

        [Command("quit"), Alias(new[] { "leave", "goodbye" }), RequireContext(ContextType.Guild)]
        public async Task LeaveServer()
        {
            await ReplyAsync($"<:exit:606088713532866591> Thanks for using HeroBot during `{(DateTime.Now - Context.Guild.CurrentUser.JoinedAt)}` please, remember you can leave a commant in our support server !!!");
            await Context.Guild.LeaveAsync();
        }
        [Command("support"), Alias(new[] { "discord", "community" })]
        public Task Support()
        {
            return ReplyAsync($"<:wave:606089927356317708> Hay ! You can join our superb server ! https://discord.gg/{_config["discord"]}");
        }
        [Command("about"), Alias("who-are-you")]
        public Task About()
        {
            return ReplyAsync($"<:wave:606089927356317708> Hi ! I'm HeroBot, your new discord assistant ! My prefix is `{_config["prefix"]}` in this server !");
        }
        [Command("bot"), Alias(new[] { "botinfo", "ping" })]
        public async Task Bot()
        {
            var websocketPing = DateTime.Now - Context.Message.CreatedAt;
            var hostPing = PingHost("google.com");
            var pingMoyenne = new List<long>();
            RestUserMessage message = await Context.Channel.SendMessageAsync("Ping <:ping:581772617481060363> ! **(>====)**");
            var messages = new[] {
                "=>===",
                "==>==",
                "===>=",
                "====>"
            };
            for (int i = 0; 3 >= i; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                await message.ModifyAsync((x) => { x.Content = $"Ping <:ping:581772617481060363> ! **({messages[i]})**"; });
                watch.Stop();
                pingMoyenne.Add(watch.ElapsedMilliseconds);
                await Task.Delay(500);
            }
            var restPing = pingMoyenne.Average();
            var memoryCount = $" Heap {SizeSuffix(getAvailableRAM())}";
            var processor = await GetCpuUsageForProcess();
            var dotnetVersion = Environment.Version.ToString();
            var processorCount = Environment.ProcessorCount;
            var threadCount = Process.GetCurrentProcess().Threads;
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var embed = new EmbedBuilder()
                .WithRandomColor()
                .WithCopyrightFooter(Context.User.Username, "bot")
                .WithDescription("Voici quelques informations concernant HeroBot")
                .AddField("REST Latency avg",$"> {Math.Round(restPing)}ms",true)
                .AddField("Server latency",$"> {hostPing}ms",true)
                .AddField("Memory details",memoryCount,true)
                .AddField("Websocket latency",$"> {Math.Round(websocketPing.TotalMilliseconds)} ms",true)
                .AddField("CPU Usage",$"> {processor}%",true)
                .AddField(".NET Version","> "+dotnetVersion,true)
                .AddField("Processor(s)",$"> {processorCount}c",true)
                .AddField("Threads actifs", "> "+threadCount.Count, true)
                .AddField("Uptime","> "+uptime.ToHumanReadable())
                .AddField("🎉 Contribs","`Moitié prix#4263`\n`PsyKo ツ ♡#2586`\n`TheDarkny#9253`\n`Ernest#6450`");
            await message.ModifyAsync((x) =>
            {
                x.Embed = embed.Build();
                x.Content = String.Empty;
            });
        }
        public long getAvailableRAM()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64;
        }
        private async Task<double> GetCpuUsageForProcess()
        {
            var startTime = DateTime.UtcNow;
            var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            await Task.Delay(500);

            var endTime = DateTime.UtcNow;
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (endTime - startTime).TotalMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
            return Math.Round(cpuUsageTotal * 10000) / 100;
        }

        /// <summary>
        /// Defines the SizeSuffixes
        /// </summary>
        internal static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// The SizeSuffix
        /// </summary>
        /// <param name="value">The value<see cref="Int64"/></param>
        /// <returns>The <see cref="string"/></returns>
        internal static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
        public static long PingHost(string nameOrAddress)
        {
            var timer = new Stopwatch();
            Ping pinger = null;
            try
            {
                timer.Start();
                new Ping().Send(nameOrAddress);
            }
            catch (PingException)
            {
                // We ignore ping exceptions
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }


        [Group("plugin")]
        [Cooldown(120)]
        public class PluginCommands : ModuleBase<SocketCommandContext>
        {
            private readonly IDatabaseService _databaseService;
            private readonly CommandService _commands;

            private readonly static string IsPluginEnabled = "SELECT \"plugin\" FROM \"GuildPlugin\" WHERE \"guild\"=@guild";
            private readonly static string InsertPlugin = "INSERT INTO \"GuildPlugin\" (\"guild\",\"plugin\") VALUES (@guild,@plugin)";
            public PluginCommands(IDatabaseService databaseService, CommandService commandService)
            {
                _databaseService = databaseService;
                _commands = commandService;
            }

            [Command("list")]
            public async Task ListPlugins()
            {
                NpgsqlConnection guildService = (NpgsqlConnection)_databaseService.GetDbConnection("HeroBot.Core");
                

                    var plugins = await guildService.QueryAsync(IsPluginEnabled,new {
                        guild = (long)Context.Guild.Id
                    });
                    var resp = new StringBuilder("**Liste des modules disponibles**\n");
                    foreach (var module in _commands.Modules)
                    {
                        if (!module.IsSubmodule)
                        {
                            var isEnabled = plugins.Any(x => x.plugin == module.Name);
                            resp.Append($"**{(isEnabled ? "\\🔷" : "\\🔶")}** • {module.Name} {(isEnabled ? "Enabled" : "Disabled")}\n");
                        }
                    }
                    await ReplyAsync(resp.ToString());
                
            }
            [Command("enable")]
            [RequireContext(ContextType.Guild)]
            public async Task EnablePlugin([Remainder]string plugin)
            {
                if (_commands.Modules.Any(x => x.Name == plugin))
                {
                    NpgsqlConnection guildService = (NpgsqlConnection)_databaseService.GetDbConnection("HeroBot.Core");
                    
                        var module = _commands.Modules.First(x => x.Name == plugin);
                        await guildService.ExecuteAsync(InsertPlugin,new {
                            guild = (long)Context.Guild.Id,
                            plugin = module.Name
                        });
                        await ReplyAsync("<:check:606088713897902081> The plugin is now enabled.");
                    
                }
                else
                {
                    await ReplyAsync($"I can't find a plugin named `{plugin}`");
                }
            }
            [Command("disable")]
            [RequireContext(ContextType.Guild)]
            public async Task DisablePlugin([Remainder]string plugin)
            {
            }
            [Command("disablecommand")]
            public async Task DisableCommand([Remainder]string command)
            {
            }
            [Command("enablecommand")]
            public async Task EnableCommand(string command)
            {
            }
        }
    }
}
