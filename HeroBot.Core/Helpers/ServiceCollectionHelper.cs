using Dapper;
using FluentMigrator.Runner;
using HeroBot.Core.Migrations;
using HeroBot.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HeroBot.Core.Helpers
{
    /// <summary>
    /// Class that defines some methods used to load the plugins.
    /// </summary>
    public static class ServiceCollectionHelper
    {
        /// <summary>
        /// Load all the assemblies & run all the migrations
        /// </summary>
        /// <param name="services">The ServiceCollection "this"</param>
        /// <returns>The modified IServiceCollection</returns>
        public static IServiceCollection LoadAllServicesFromExternalAssembiles(this IServiceCollection services)
        {
            // Service migraotr used for the main system migrating
            var serviceProviderMigrateg = CreateserviceMigrator(typeof(Migration1270720191).Assembly);
            // We create a scope for the migrator service
            using (var scope = serviceProviderMigrateg.CreateScope())
            {
                (scope as IServiceProvider).GetRequiredService<IMigrationRunner>().MigrateUp();
            }
            // Call the loading method from the ModulesService
            ModulesService.LoadAssembliesInDirrectory();
            List<Assembly> ass = new List<Assembly>();
            // We add each assemblies to the "ass" assembly list
            foreach (dynamic assembly in ModulesService.GetLoadedAssemblies())
            {
                ass.Add(assembly.ass);
            }
            // For each assemblies, we load the migrations
            foreach (dynamic assembly in ModulesService.GetLoadedAssemblies())
            {
                try
                {
                    var assm = ((Assembly)(assembly.ass));
                    var serviceProviderMigrate = CreateserviceMigrator(assm);

                    using (var scope = serviceProviderMigrate.CreateScope())
                    {
                        (scope as IServiceProvider).GetRequiredService<IMigrationRunner>().MigrateUp();
                    }
                }
                catch { /* We ignore the assemblies loading exceptions */ }
            }
            // We register each service into the ServiceCollection
            foreach (Assembly assm in ass)
            {
                IEnumerable<TypeInfo> servicesAss = assm.DefinedTypes.Where(x => !x.IsInterface && !x.IsEnum && x.IsClass && x.IsPublic && x.Name.Contains("Service"));
                if (servicesAss.Any())
                {
                    foreach (TypeInfo typeInfo in servicesAss)
                    {
                        services.AddSingleton(assm.GetTypes().FirstOrDefault(x => x.Name == typeInfo.Name));
                    }
                }
            }
            return services;
        }
        /// <summary>
        /// Used to build a ServiceMigrator utility for migrating each assembly
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>A service provider</returns>
        private static ServiceProvider CreateserviceMigrator(Assembly assembly)
        {
            var coll = new ServiceCollection();
            coll.AddFluentMigratorCore().ConfigureRunner((x) =>
            {
                x.AddPostgres()
                .WithGlobalConnectionString($"Server=ssh.alivecreation.fr;Port=26257;Database={assembly.GetName().Name};User Id=matthieu;Password=4d9*jC%(hu\"ecN2&;SslMode=Require;Trust Server Certificate=true")
                .ScanIn(assembly).For.Migrations();
            });
            return coll.BuildServiceProvider();
        }
    }

}