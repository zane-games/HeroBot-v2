using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HeroBotv2.Services
{
    public class LoggingService
    {

        // DiscordSocketClient and CommandService are injected automatically from the IServiceProvider
        public LoggingService(DiscordShardedClient discord, CommandService commands)
        {
            discord.Log += OnLogAsync;
            commands.Log += OnLogAsync;
        }

        public void Log(LogSeverity logSeverity, string message) {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            var assembly = stackFrames[1].GetMethod().DeclaringType.Assembly;
            DisplayLogAsync(new LogMessage(logSeverity, assembly.GetName().Name,message)).Wait();
        }

        private async Task OnLogAsync(LogMessage msg)
        {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            var assembly = stackFrames[3].GetMethod().DeclaringType.Assembly;
            
            await DisplayLogAsync(new LogMessage(msg.Severity, assembly.GetName().Name, msg.Message, msg.Exception));
        }

        private Task DisplayLogAsync(LogMessage msg) {
            string logText = $"{DateTime.UtcNow.ToString("hh:mm:ss")} [{msg.Severity}] {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            return Console.Out.WriteLineAsync(logText);       // Write the log text to the console
        }
    }
}
