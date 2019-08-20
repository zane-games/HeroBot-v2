using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBotv2.Services
{
    public class CommandHandler
    {
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IServiceProvider _provider;
        private readonly CooldownService _cooldown;
        // DiscordSocketClient, CommandService, IConfigurationRoot, and IServiceProvider are injected automatically from the IServiceProvider
        public CommandHandler(
            DiscordShardedClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IServiceProvider provider,
            CooldownService cooldownService)
        {
            _cooldown = cooldownService;
            _discord = discord;
            _commands = commands;
            _config = config;
            _provider = provider;

            _discord.MessageReceived += OnMessageReceivedAsync;
            _discord.MessageUpdated += OnMessageUpdated;
        }

        private Task OnMessageUpdated(Cacheable<IMessage, ulong> oldMessage, SocketMessage newMessage, ISocketMessageChannel messageChannel)
        {
            return OnMessageReceivedAsync(newMessage);
        }

        private async Task OnMessageReceivedAsync(SocketMessage s)
        {
            SocketUserMessage msg = s as SocketUserMessage;                                             // Ensure the message is from a user/bot
            if (msg == null) return;
            if (msg.Author.Id == _discord.CurrentUser.Id || msg.Author.IsBot) return;     // Ignore self or bot when checking commands
            if (!(msg.Channel is SocketGuildChannel)) return;
            var context = new SocketCommandContext(_discord.GetShardFor((msg.Channel as SocketGuildChannel).Guild), msg);                        // Create the command context
            int argPos = 0;     // Check if the message has a valid command prefix
            if (msg.HasStringPrefix(_config["prefix"], ref argPos) || msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
            {
                var command = _commands.Search(context, argPos);
                if (command.IsSuccess) {

                    var cmd = command.Commands.First().Command;
                    var result = await _commands.ExecuteAsync(context, argPos, _provider);
                    if (!result.IsSuccess)
                    {
                        switch (result.Error)
                        {
                            case CommandError.BadArgCount:
                                await s.Channel.SendMessageAsync($"Oops, it look like you made a syntax error in your command :/ `{_config["prefix"]}{(cmd.Aliases.Count > 0 ? $"{"{"}{string.Join(",", cmd.Aliases)}{"}"}" : cmd.Name)} {string.Join(" ", cmd.Parameters.Select(p => p.IsOptional ? $"[{p.Name}{(p.IsRemainder ? "..." : String.Empty)}]" : $"<{p.Name}{(p.IsRemainder ? "..." : String.Empty)}>"))}`");
                                break;
                            case CommandError.ParseFailed:
                                await s.Channel.SendMessageAsync("Oops, it look like you sent us a bad argument :/");
                                break;
                            case CommandError.UnmetPrecondition:
                                await s.Channel.SendMessageAsync(result.ErrorReason);
                                break;
                            case CommandError.Exception:
                                await s.Channel.SendMessageAsync("Oops, HeroBot ran into an error :/");
                                break;
                            default:
                                await s.Channel.SendMessageAsync($"Unknown error `{result}`");
                                break;
                        }

                    }
                    else
                        await _cooldown.OnCommand(command.Commands.First().Command, context);
                }
            }  
        }
    }
}