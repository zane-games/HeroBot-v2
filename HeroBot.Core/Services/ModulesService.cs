using Dapper;
using Discord;
using Discord.Commands;
using HeroBot.Common.Attributes;
using HeroBot.Common.Entities;
using HeroBot.Common.ExtendedModules;
using HeroBot.Common.Helpers;
using HeroBot.Common.Interfaces;
using HeroBotv2.Services;
using Npgsql;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Core.Services
{
    public sealed class ModulesService
    {
        private readonly IServiceProvider _provider;
        private readonly IDatabaseService _database;
        private readonly CommandService _commandService;
        private readonly LoggingService _logging;
        private readonly ConcurrentDictionary<string, ContextEntity> _contexts;
        private static readonly List<dynamic> LoadedAssemblies = new List<dynamic>();
        public ModulesService(CommandService commandService, IServiceProvider provider, LoggingService loggingService, IDatabaseService databaseService)
        {
            _provider = provider;
            _database = databaseService;
            _commandService = commandService;
            _logging = loggingService;
            _contexts = new ConcurrentDictionary<string, ContextEntity>();

            FetchExternalAssemblies();
        }

        internal static IEnumerable<dynamic> GetLoadedAssemblies()
        {
            return LoadedAssemblies;
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
                LoadedAssemblies.Add(new { ass, moduleContext });
                Console.WriteLine($"Loaded {ass.GetName()} v{ass.GetName().Version} assembly.");
            }
        }

        private void FetchExternalAssemblies()
        {

            foreach (dynamic ass in LoadedAssemblies)
            {
                var name = ((String)ass.ass.GetName().Name).SanitizAssembly();

                var classesRefferal = (ass.ass as Assembly).DefinedTypes.Where(x => !x.IsInterface && x.IsPublic && !x.IsAbstract && x.Name == "PluginRefferal");

                if (classesRefferal.Any())
                {
                    var instance = (IPluginRefferal)classesRefferal.First().GetConstructors().First().Invoke(new object[] { });


                    var context = new ContextEntity
                    {
                        Name = name,
                        Context = ass.moduleContext,
                        Assembly = ass.ass,
                        pluginRefferal = instance
                    };
                    if (_contexts.ContainsKey(name))
                        continue;

                    _contexts.TryAdd(name, context);
                    _logging.Log(LogSeverity.Info, $"Loaded {name} v{ass.ass.GetName().Version} assembly modules.");


                    if (_contexts.IsEmpty)
                        break;
                }
            }

        }

        public async Task TempUnloadAssemblyAsync(string assemblyName)
        {
            if (!_contexts.TryGetValue(assemblyName, out var context))
                return;

            var check = await _commandService.RemoveModuleAsync(context.Module);
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
            if (!_contexts.TryGetValue(assemblyName, out var context))
                return;

            context.Context.LoadFromAssemblyName(context.Assembly.GetName());
            await _commandService.AddModulesAsync(context.Assembly, _provider);

            _logging.Log(LogSeverity.Info, $"Loaded {context.Name} assembly.");
        }

        public async Task LoadModulesFromAssembliesAsync()
        {
            foreach (var context in _contexts.Values)
            {
                var addModule = await _commandService.AddModulesAsync(context.Assembly, _provider);
                context.Module = addModule.FirstOrDefault();
                if (context.Module.Preconditions.Any(x => x is NeedPluginAttribute))
                {
                    _contexts.TryUpdate(context.Name, context, context);
                }
            }

        }
    }
}