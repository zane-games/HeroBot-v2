using Dapper;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using HeroBot.Common;
using HeroBot.Common.ExtendedModules;
using HeroBot.Common.Helpers;
using HeroBot.Common.Interfaces;
using HeroBot.Core.Services.Structs;
using HeroBotv2.Services;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBot.Core.Services
{
    public sealed class ModulesService : IModulesService
    {
        private readonly IServiceProvider _provider;
        private readonly CommandService _commandService;
        private readonly IDatabaseService _database;
        private readonly LoggingService _logging;
        private readonly List<ToLoadAssembly> toLoadAssemblies = new List<ToLoadAssembly>();
        private readonly SimpleCacheImplementation _simpleCacheImplementation;
        public ModulesService( SimpleCacheImplementation simpleCacheImplementation,LoggingService logging, CommandService commandService, IServiceProvider provider, IDatabaseService databaseService)
        {
            _simpleCacheImplementation = simpleCacheImplementation;
            _provider = provider;
            _commandService = commandService;
            _database = databaseService;
            _logging = logging;
            FetchExternalAssemblies();
        }

        internal static void LoadAssembliesInDirrectory()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Modules");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var files = Directory.EnumerateFiles(path).Where(x => x.Contains("HeroBot.Plugins"));
            foreach (var file in files)
            {
                if (!file.Contains(".dll"))
                    continue;

                var moduleContext = new ModuleLoadContext();
                var ass = moduleContext.LoadFromAssemblyPath(file);
                RuntimeAssemblies.AssemblyEntities.TryAdd(ass.GetName().Name, new AssemblyEntity() { Assembly = ass, Context = moduleContext });
            }
        }

        private void FetchExternalAssemblies()
        {
            foreach (ToLoadAssembly ass in toLoadAssemblies)
            {
                var assembly = ass.Assembly;
                var name = assembly.GetName().Name.SanitizAssembly();
                var classesRefferal = assembly.DefinedTypes.Where(x => !x.IsInterface && x.IsPublic && !x.IsAbstract && x.Name == "PluginRefferal");
                if (classesRefferal.Any())
                {
                    var PluginRefferal = (IPluginRefferal)classesRefferal.First().GetConstructors().First().Invoke(new object[] { });
                    var AssemblyEntity = new AssemblyEntity()
                    {
                        Assembly = assembly,
                        Context = ass.AssemblyContext,
                        Module = new List<ModuleInfo>(),
                        Name = name,
                        pluginRefferal = PluginRefferal
                    };
                    if (RuntimeAssemblies.AssemblyEntities.Any(x => x.Value.Name == name))
                        continue;
                    RuntimeAssemblies.AssemblyEntities.TryAdd(name, AssemblyEntity);
                    _logging.Log(LogSeverity.Info, $"Loaded {name} v{ass.Assembly.GetName().Version} assembly modules.");
                    if (!RuntimeAssemblies.AssemblyEntities.Any())
                        break;
                }
            }
        }

        public async Task TempUnloadAssemblyAsync(string assemblyName)
        {
            if (!RuntimeAssemblies.AssemblyEntities.TryGetValue(assemblyName, out var context))
                return;
            var check = true;
            foreach (ModuleInfo moduleInfo in context.Module)
            {
                if (check)
                    check = await _commandService.RemoveModuleAsync(moduleInfo);
            }

            if (!check)
            {
                _logging.Log(LogSeverity.Error, $"Failed to unload {context.Name} module.");
                return;
            }

            context.Context.Unload();
            _logging.Log(LogSeverity.Info, $"Unloaded {context.Name} assembly.");
        }

        public async Task LoadAssemblyAsync(string assemblyName)
        {
            if (!RuntimeAssemblies.AssemblyEntities.TryGetValue(assemblyName, out var context))
                return;

            context.Context.LoadFromAssemblyName(context.Assembly.GetName());
            await _commandService.AddModulesAsync(context.Assembly, _provider);

            _logging.Log(LogSeverity.Info, $"Loaded {context.Name} assembly.");
        }

        internal async Task LoadModulesFromAssembliesAsync()
        {
            foreach (var context in new Dictionary<String, AssemblyEntity>(RuntimeAssemblies.AssemblyEntities).Values)
            {
                var addModule = await _commandService.AddModulesAsync(context.Assembly, _provider);
                context.Module = addModule.ToList();
                RuntimeAssemblies.AssemblyEntities[context.Assembly.GetName().Name.SanitizAssembly()] = context;
            }

        }
        public AssemblyEntity GetAssemblyEntityByModule(ModuleInfo moduleInfo)
        {
            while (moduleInfo.IsSubmodule)
                moduleInfo = moduleInfo.Parent;
            var m = RuntimeAssemblies.AssemblyEntities;
            return m.First(x => x.Value.Module.Any(v => v == moduleInfo)).Value;
        }
        private readonly string GetGuildEnabledPlugins = "SELECT * FROM \"GuildPlugin\" WHERE \"guild\"=@guild";
        private readonly static string InsertPlugin = "INSERT INTO \"GuildPlugin\" (\"guild\",\"plugin\") VALUES (@guild,@plugin)";
        private readonly static string DeletePlugin = "DELETE FROM \"GuildPlugin\" WHERE \"guild\"=@guild AND \"plugin\"=@plugin";

        public async Task<bool> IsPluginEnabled(IGuild guild, ModuleInfo moduleInfo)
        {
            var moduleAssemblyName = GetAssemblyEntityByModule(moduleInfo);
            if (moduleAssemblyName.Assembly.GetName().Name.SanitizAssembly() == "HeroBot.Plugins.HeroBot") return true;
            PluginEnabling[] cv;
            var rd = await _simpleCacheImplementation.GetValueAsync($"guildPluginsCache-{guild.Id}");
            if (!rd.HasValue)
            {
                var connection = _database.GetDbConnection();
                cv = (await connection.QueryAsync<PluginEnabling>(GetGuildEnabledPlugins, new
                {
                    guild = (long)guild.Id
                })).ToArray();
                await _simpleCacheImplementation.CacheValueAsync($"guildPluginsCache-{guild.Id}", JsonConvert.SerializeObject(cv));
            }
            else
            {
                cv = JsonConvert.DeserializeObject<PluginEnabling[]>(rd);
            }
            if (!cv.Any(x => x.Plugin == moduleAssemblyName.Assembly.GetName().Name.SanitizAssembly()))
            {
                return false;
            }
            return true;
        }

        public async Task EnablePlugin(IGuild guild, ModuleInfo moduleInfo)
        {
            await _simpleCacheImplementation.InvalidateValueAsync($"guildPluginsCache-{guild.Id}");
            NpgsqlConnection guildService = (NpgsqlConnection)_database.GetDbConnection("HeroBot.Core");
            var r = this.GetAssemblyEntityByModule(moduleInfo).Assembly.GetName().Name.SanitizAssembly();
            await guildService.ExecuteAsync(InsertPlugin, new
            {
                guild = (long)guild.Id,
                plugin = r
            });
        }

        public async Task DisablePlugin(IGuild guild, ModuleInfo moduleInfo)
        {
            await _simpleCacheImplementation.InvalidateValueAsync($"guildPluginsCache-{guild.Id}");
            NpgsqlConnection guildService = (NpgsqlConnection)_database.GetDbConnection("HeroBot.Core");
            var r = this.GetAssemblyEntityByModule(moduleInfo).Assembly.GetName().Name.SanitizAssembly();
            await guildService.ExecuteAsync(DeletePlugin, new
            {
                guild = (long)guild.Id,
                plugin = r
            });
        }
    }
}