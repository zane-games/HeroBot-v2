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

        public HeroBotModule(CommandService service, IConfigurationRoot config)
        {
            _service = service;
            _config = config;
        }

        [Command("help"), Alias(new[] { "h", "hh", "hmp" })]
        public async Task HelpCommandAsync([Remainder]string command = null)
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder()
            {
                Description = "These are the commands you can use"
            };
            builder.AddField("**HeroBot**", "HeroBot is a bot with a plugin system that can be developped by the community, maintained by **Matthieu#2050**, it is supposed to be able to replace several bots. \n [ [_**Our Discord**_](https://alivecreation.fr/discord) | [_**Website 📟**_](https://herobot.alivecreation.fr) ] \n [ [_**Invite HeroBot 😍**_](https://alivecreation.fr/invite) | [**Upvote us on DBL 🎉**](https://discordbots.org/bot/491673480006205461) ]\r **<> is a required argument, [] is an optional argument** If your argument contains spaces, you must put it behind two `\"` Pro tip : You can use ;hh to send the message in the current channel",false)
            .WithDescription("Hay ! Thanks for using HeroBot ! I hope you enjoy using our bot :P, is you have any suggestions, you can tell us your beautiful idea !")
            .WithTitle("HeroBot Help")
            .WithThumbnailUrl("https://cdn.discordapp.com/avatars/491673480006205461/30abe7a1feffb0b06a1611a94fbc1248.png")
            .WithRandomColor().WithCopyrightFooter(Context.User.Username,"help");
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
            else {
                int total = 0;
                int realCommands = 0;
                int subcommands = 0;
                foreach (var module in _service.Modules.Where(x => !x.IsSubmodule))
                {
                    var field = new EmbedFieldBuilder()
                    {
                        Name = $"**`{module.Name}`**"
                    };
                    string description = null;
                    var moduleCommands = new List<CommandInfo>(module.Commands);
                    foreach (var sbm in module.Submodules)
                    {
                        subcommands += sbm.Commands.Count;
                        moduleCommands.AddRange(sbm.Commands);
                    }
                    foreach (var cmd in moduleCommands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                        {
                            total++;
                            description += $"**>** `{prefix}{cmd.Aliases.First()}`";
                        }
                    }
                    
                    if (!string.IsNullOrWhiteSpace(description))
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

        [Command("quit"),Alias(new[] { "leave","goodbye" }),RequireContext(ContextType.Guild)]
        public async Task LeaveServer() {
            await ReplyAsync($"Thanks for using HeroBot during `{(DateTime.Now - Context.Guild.CurrentUser.JoinedAt)}` please, remember you can leave a commant in our support server !!!");
            await Context.Guild.LeaveAsync();
        }
        [Command("support"), Alias(new[] { "discord", "community" })]
        public Task Support() {
            return ReplyAsync($"Hay ! You can join our superb server ! https://discord.gg/{_config["discord"]}");
        }
        [Command("about"), Alias("who-are-you")]
        public Task About() {
            return ReplyAsync($"Hi ! I'm HeroBot, your new discord assistant ! My prefix is `{_config["prefix"]}` in this server !");
        }
        [Command("bot"), Alias(new[] { "botinfo", "ping" })]
        public async Task Bot() {
            var websocketPing = DateTime.Now - Context.Message.CreatedAt;
            var hostPing = PingHost("google.com");
            var pingMoyenne = new List<long>();
            RestUserMessage message = await Context.Channel.SendMessageAsync("Ping :ping_pong: ! **(>====)**");
            var messages = new[] {
                "=>===",
                "==>==",
                "===>=",
                "====>"
            };
            for (int i = 0; 3 >= i; i++) {
                var watch = new Stopwatch();
                watch.Start();
                await message.ModifyAsync((x) => { x.Content = $"Ping :ping_pong: ! **({messages[i]})**"; });
                watch.Stop();
                pingMoyenne.Add(watch.ElapsedMilliseconds);
                await Task.Delay(500);
            }
            var restPing = pingMoyenne.Average();
            var memoryCount = $" Working Set: {SizeSuffix(getAvailableRAM())}";
            var processor = await GetCpuUsageForProcess();
            var dotnetVersion = Environment.Version.ToString();
            var processorCount = Environment.ProcessorCount;
            var threadCount = Process.GetCurrentProcess().Threads;
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var embed = new EmbedBuilder()
                .WithRandomColor()
                .WithCopyrightFooter(Context.User.Username, "bot")
                .WithDescription("Voici quelques informations concernant HeroBot")
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("REST Latency avg").WithValue($"{Math.Round(restPing)}ms"))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("Server latency").WithValue($"{hostPing}ms"))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("Memory details").WithValue(memoryCount))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("Websocket latency").WithValue($"{Math.Round(websocketPing.TotalMilliseconds)} ms"))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("CPU Usage").WithValue($"{processor}%"))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName(".NET Version").WithValue(dotnetVersion))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("Processor(s)").WithValue($"{processorCount}c"))
                .AddField(new EmbedFieldBuilder().WithIsInline(true).WithName("Uptime").WithValue(uptime.ToHumanReadable()))
                .AddField(new EmbedFieldBuilder().WithIsInline(false).WithName("🎉 Contribs").WithValue("`Moitié prix#4263`\n`PsyKo ツ ♡#2586`\n`TheDarkny#9253`\n`Ernest#6450`"));
            await message.ModifyAsync((x) => {
                x.Embed = embed.Build();
                x.Content = ""; });
        }
        public long getAvailableRAM()
        {
            return Process.GetCurrentProcess().PeakPagedMemorySize64;
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
            return Math.Round(cpuUsageTotal * 10000)/100;
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
            bool pingable = false;
            Ping pinger = null;
            try
            {
                timer.Start();
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
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
        public class PluginCommands : ModuleBase<SocketCommandContext> {
            private IDatabaseService _databaseService;
            private CommandService _commands;

            private readonly string GetGuildPlugins = "SELECT \"Plugin\" FROM \"GuildPlugin\" WHERE \"Guild\"=@guild";
            private readonly string GetPluginId = "SELECT \"Id\" FROM \"Plugins\" WHERE \"Name\"=@name";
            private readonly string InsertGuildPlugin = "INSERT INTO \"GuildPlugin\" (\"Guild\",\"Plugin\") VALUES (@guild,@plugin)";
            public PluginCommands(IDatabaseService databaseService,CommandService commandService) {
                _databaseService = databaseService;
                _commands = commandService;
            }

            [Command("list")]
            public async Task ListPlugins() {
                using (NpgsqlConnection guildService = (NpgsqlConnection)_databaseService.GetDbConnection("HeroBot.Core"))
                {
                    var rest = await guildService.QueryAsync(GetGuildPlugins,new { guild = (long)Context.Guild.Id });
                    var resp = "**Liste des modules disponibles**\n";
                    foreach (var module in _commands.Modules)
                    {
                        if (!module.IsSubmodule) {
                            var pluginId = (await guildService.QueryAsync(GetPluginId, new { name = module.Name })).First().Id;
                            var isEnabled = rest.Where(x => x.Plugin == pluginId).Any();
                            resp += $"**{(isEnabled ? "\\🔷" : "\\🔶")}** • {module.Name} {(isEnabled ? "Activé" : "Desactivé")}\n";
                        }
                    }
                    await ReplyAsync(resp);
                }
            }
            [Command("enable")]
            [RequireContext(ContextType.Guild)]
            public async Task EnablePlugin([Remainder]string plugin) {
                if (_commands.Modules.Where(x => x.Name == plugin).Any())
                {
                    using (NpgsqlConnection guildService = (NpgsqlConnection)_databaseService.GetDbConnection("HeroBot.Core"))
                    {
                        var module = _commands.Modules.Where(x => x.Name == plugin).First();
                        var pluginId = (await guildService.QueryAsync(GetPluginId, new { name = module.Name })).First().Id;
                        await guildService.ExecuteAsync(InsertGuildPlugin, new { guild = (long)Context.Guild.Id, plugin = pluginId});
                    }
                }
                else {
                    await ReplyAsync($"I can't find a plugin named `{plugin}`");
                }
            }
            [Command("disable")]
            [RequireContext(ContextType.Guild)]
            public async Task DisablePlugin([Remainder]string plugin)
            {
                /*_databaseService.EditGuild(Context.Guild.Id, (x) =>
                {
                    x.EnabledPlugins.Remove(x.EnabledPlugins.Where(v => v..Name == plugin).FirstOrDefault());
                    return x;
                });
                await ReplyAsync($"Plugin {plugin} activé !");*/
            }
            [Command("disablecommand")]
            public async Task DisableCommand([Remainder]string command) {
                _commands.Commands.Where(x => x.Name == command);
            }
            [Command("enablecommand")]
            public async Task EnableCommand(string command) {
                // TODO to finish
            }
        }
    }
}
