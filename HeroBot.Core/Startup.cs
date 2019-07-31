using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FluentMigrator.Runner;
using HeroBot.Common.Interfaces;
using HeroBot.Core.Helpers;
using HeroBot.Core.Services;
using HeroBotv2.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.Logging;
using System;
using System.Threading.Tasks;

namespace HeroBotv2
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup()
        {
            NpgsqlLogManager.Provider = new ConsoleLoggingProvider(NpgsqlLogLevel.Debug, true, true);
            var builder = new ConfigurationBuilder()                  // Create a new instance of the config builder
                .SetBasePath(AppContext.BaseDirectory)                // Specify the default location for the config file
                .AddYamlFile("_config.yml");                          // Add this (yaml encoded) file to the configuration
            Configuration = builder.Build();                          // Build the configuration
        }

        public static async Task RunAsync()
        {
            var startup = new Startup();                              // Creating Startup object
            await startup._RunAsync();                                // Running
        }

        public async Task _RunAsync()
        {
            // 
            var services = new ServiceCollection();                   // Create a new instance of a service collection
            ConfigureServices(services);                              // Call the ConfigureService method
            var provider = services.BuildServiceProvider();           // Build the service provider
            provider.GetRequiredService<LoggingService>();            // Start the logging service
            provider.GetRequiredService<IDatabaseService>();          // Initialize the database service
            provider.GetRequiredService<IRedisService>();             // Initialize the redis cache connection
            provider.GetRequiredService<ICooldownService>();
            provider.GetRequiredService<CooldownService>();
            provider.GetAllServicesFromExternalAssemblies();          // Initialize services from other assemblies
            provider.GetRequiredService<CommandHandler>(); 		      // Start the command handler service
            provider.GetRequiredService<IVoteService>();
            await provider.GetRequiredService<StartupService>()       // Get the bot's startup service
                .StartAsync();                                        // Start the startup service
            await provider.GetRequiredService<ModulesService>()       // Get the external modules loader
                .LoadModulesFromAssembliesAsync();                    // Calll the startup method
            await Task.Delay(-1);                                     // Keep the program alive
        }

        internal void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
            {                                                          // Add discord to the collection
                LogLevel = LogSeverity.Info,                        // Tell the logger to give Verbose amount of info
                MessageCacheSize = 5                                  // Cache 50 messages per channel

            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {                                                         // Add the command service to the collection
                LogLevel = LogSeverity.Info,                       // Tell the logger to give Verbose amount of info
                DefaultRunMode = RunMode.Async,                       // Force all commands to run async by default
            }))


            .AddSingleton<IDatabaseService, HeroBotContext>()         // Adding interface-resolvable database service
            .AddSingleton<IRedisService, RedisService>()              // Adding interface-resolvable redis for caching
            .AddSingleton<ICooldownService, CooldownService>()
            .AddSingleton<CooldownService>()
            .AddSingleton<CommandHandler>()                           // Add the command handler to the collection
            .AddSingleton<StartupService>()                           // Add startupservice to the collection
            .AddSingleton<LoggingService>()                           // Add loggingservice to the collection
            .AddSingleton<ModulesService>()                           // Add module handler to the collection
            .AddSingleton<Random>()                                   // Add random to the collection
            .AddSingleton<IVoteService, AsynchronousSocketListener>()
            .AddSingleton(Configuration)                              // Add the configuration to the collection
            .LoadAllServicesFromExternalAssembiles();                 // Now we need to load external services.
        }
    }
}
