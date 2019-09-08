using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Helpers;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.HeroBot.Modules
{
    [NeedPlugin()]
    [Cooldown(2)]
    [Name("HeroBot Commands")]
    public class HeroBotModule : ModuleBase<SocketCommandContext>
    {
        private readonly DiscordShardedClient _client;
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;
        private readonly IModulesService _modules;

        public HeroBotModule(DiscordShardedClient client,CommandService service, IConfigurationRoot config, IServiceProvider serviceProvider,IModulesService _module)
        {
            _client = client;
            _service = service;
            _config = config;
            _provider = serviceProvider;
            _modules = _module;
        }

        [Command("help"), Alias(new[] { "h", "hh", "hmp" }),Summary("Gives you some information about a certain command")]
        public async Task HelpCommandAsync([Remainder]string command = null)
        {
            string prefix = _config["prefix"];
            var builder = new EmbedBuilder();
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
                    x.Name = "Summary";
                    x.IsInline = true;
                    x.Value = cmd.Summary ?? "*no summary*";
                });
                var permBot = string.Join(", ", cmd.Preconditions.Where(v => v is RequireBotPermissionAttribute).Select(v => ((RequireBotPermissionAttribute)v).ChannelPermission).Where(x => x.HasValue).Select(x => x.Value));
                builder.AddField(x => {
                    x.Name = "Permissions nécessaires au bot";
                    x.Value = string.IsNullOrEmpty(permBot) ? "*no permissions required*" : permBot;
                });
                var permUser = string.Join(", ", cmd.Preconditions.Where(v => v is RequireUserPermissionAttribute).Select(v => ((RequireUserPermissionAttribute)v).ChannelPermission).Where(x => x.HasValue).Select(x => x.Value));
                builder.AddField(x =>
                {
                    x.Name = "Permissions nécessaires a l'utilisateur";
                    x.Value = string.IsNullOrEmpty(permUser) ? "*no permissions required*" : permUser;
                });
                builder.AddField(x =>
                {
                    x.Name = "Plugin";
                    x.Value = _modules.GetAssemblyEntityByModule(cmd.Module).Assembly.GetName().Name.SanitizAssembly();
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
                        realCommands += module.Commands.Count;
                        var commands = ResolveAllCommandsFromModule(module);
                        subcommands += commands.Count() - module.Commands.Count;
                        foreach (var cmd in commands)
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

        private IEnumerable<CommandInfo> ResolveAllCommandsFromModule(ModuleInfo module)
        {
            var allModules = ResolveAllModules(module);
            foreach (ModuleInfo moduleInfo in allModules)
                foreach (CommandInfo commandInfo in moduleInfo.Commands)
                    yield return commandInfo;
        }

        private List<ModuleInfo> ResolveAllModules(ModuleInfo module)
        {
            var modules = new List<ModuleInfo>();
            modules.Add(module);
            foreach (ModuleInfo moduleInfo in module.Submodules) {
                modules.AddRange(ResolveAllModules(moduleInfo));
            }
            return modules;
        }

        [Command("quit"), Alias(new[] { "leave", "goodbye" }), RequireContext(ContextType.Guild), RequireUserPermission(GuildPermission.Administrator)]
        public async Task LeaveServer()
        {
            await ReplyAsync($"<:exit:606088713532866591> Thanks for using HeroBot during `{(DateTime.Now - Context.Guild.CurrentUser.JoinedAt).Value.ToHumanReadable()}` please, remember you can leave a commant in our support server !!!");
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
        [Cooldown(10)]
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
            var memoryCount = $" {SizeSuffix(GetAvailableRam())}";
            var processor = await GetCpuUsageForProcess();
            var dotnetVersion = Environment.Version.ToString();
            var processorCount = Environment.ProcessorCount;
            var threadCount = Process.GetCurrentProcess().Threads;
            var uptime = DateTime.Now - Process.GetCurrentProcess().StartTime;
            var embed = new EmbedBuilder()
                .WithRandomColor()
                .WithCopyrightFooter(Context.User.Username, "bot")
                .WithDescription("Voici quelques informations concernant HeroBot")
                .AddField("REST Latency avg", $"> {Math.Round(restPing)}ms", true)
                .AddField("Server latency", $"> {hostPing}ms", true)
                .AddField("Memory details", memoryCount, true)
                .AddField("Websocket latency", $"> {Math.Round(websocketPing.TotalMilliseconds)} ms", true)
                .AddField("CPU Usage", $"> {processor}%", true)
                .AddField(".NET Version", "> " + dotnetVersion, true)
                .AddField("Processor(s)", $"> {processorCount}c", true)
                .AddField("Threads actifs", "> " + threadCount.Count, true)
                .AddField("Uptime", "> " + uptime.ToHumanReadable())
                .AddField("Bot statistics", $"Channels: {_client.Guilds.Sum(x => x.Channels.Count)} ({_client.Guilds.Sum(x => x.VoiceChannels.Count)} voice channels, {_client.Guilds.Sum(x => x.TextChannels.Count)} text channels, {_client.Guilds.Sum(x => x.CategoryChannels.Count)} categories) \r\nGuilds: {_client.Guilds.Count} \r\nGuild Users: {_client.Guilds.Sum(x => x.MemberCount)}\r\n")
                .AddField("🎉 Contribs", "> `Moitié prix#4263`\n`PsyKo ツ ♡#2586`\n`TheDarkny#9253`\n`Ernest#6450`");
            await message.ModifyAsync((x) =>
            {
                x.Embed = embed.Build();
                x.Content = String.Empty;
            });
        }
        public long GetAvailableRam()
        {
            return Process.GetCurrentProcess().PeakVirtualMemorySize64;
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
        internal static string SizeSuffix(long value)
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
            private readonly CommandService _commands;
            private readonly IModulesService _modules;
            public PluginCommands(CommandService commandService, IModulesService modulesService)
            {
                _commands = commandService;
                _modules = modulesService;
            }

            [Command("list")]
            public async Task ListPlugins()
            {
                var resp = new StringBuilder("**Liste des modules disponibles**\n");
                foreach (var module in _commands.Modules)
                {
                    if (!module.IsSubmodule)
                    {
                        var isEnabled = await _modules.IsPluginEnabled(Context.Guild, module);
                        resp.Append($"**{(isEnabled ? "\\🔷" : "\\🔶")} • {(isEnabled ? "Enabled" : "Disabled")}** • {_modules.GetAssemblyEntityByModule(module).Assembly.GetName().Name.SanitizAssembly()} \n");
                    }
                }
                await ReplyAsync(resp.ToString());
            }
            [Command("enable")]
            [RequireUserPermission(GuildPermission.Administrator)]
            [RequireContext(ContextType.Guild)]
            public async Task EnablePlugin([Remainder]string plugin)
            {
                if (_commands.Modules.Any(x => plugin == _modules.GetAssemblyEntityByModule(x).Assembly.GetName().Name.SanitizAssembly()))
                {
                    await _modules.EnablePlugin(Context.Guild,_commands.Modules.First(x => plugin == _modules.GetAssemblyEntityByModule(x).Assembly.GetName().Name.SanitizAssembly()));
                    await ReplyAsync("<:check:606088713897902081> The plugin is now enabled.");
                }
                else
                {
                    await ReplyAsync($"I can't find a plugin named `{plugin}`");
                }
            }
            [Command("disable")]
            [RequireUserPermission(GuildPermission.Administrator)]
            [RequireContext(ContextType.Guild)]
            public async Task DisablePlugin([Remainder]string plugin)
            {
                if (_commands.Modules.Any(x => plugin == _modules.GetAssemblyEntityByModule(x).Assembly.GetName().Name.SanitizAssembly()))
                {
                    await _modules.DisablePlugin(Context.Guild, _commands.Modules.First(x => plugin == _modules.GetAssemblyEntityByModule(x).Assembly.GetName().Name.SanitizAssembly()));
                    await ReplyAsync("<:check:606088713897902081> The plugin is now disabled.");
                }
                else
                {
                    await ReplyAsync($"I can't find a plugin named `{plugin}`");
                }
            }
        }
    }
}
