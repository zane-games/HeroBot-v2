using FluentMigrator.Runner;
using HeroBot.Common.Interfaces;
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
        public static IServiceCollection LoadAllServicesFromExternalAssembiles(this IServiceCollection services,IConfigurationRoot _config)
        {
            // Service migraotr used for the main system migrating
            var serviceProviderMigrateg = CreateserviceMigrator(typeof(Migration1270720191).Assembly,_config);
            // We create a scope for the migrator service
            using (var scope = serviceProviderMigrateg.CreateScope())
            {
                (scope as IServiceProvider).GetRequiredService<IMigrationRunner>().MigrateUp();
            }
            // Call the loading method from the ModulesService
            ModulesService.LoadAssembliesInDirrectory();
            List<AssemblyEntity> ass = RuntimeAssemblies.AssemblyEntities.Values.ToList();
            // For each assemblies, we load the migrations
            foreach (AssemblyEntity assembly in ass)
            {
                var assm = assembly.Assembly;
                try
                {

                    var serviceProviderMigrate = CreateserviceMigrator(assm,_config);
                    using var scope = serviceProviderMigrate.CreateScope();
                    (scope as IServiceProvider).GetRequiredService<IMigrationRunner>().MigrateUp();
                }
                catch(Exception e) { Console.WriteLine(e); }
                    foreach(TypeInfo typeInfo in assm.DefinedTypes.Where(x => !x.IsInterface && !x.IsEnum && x.IsClass && x.IsPublic && x.Name.Contains("Service"))) 
                        services.AddSingleton(assm.GetTypes().FirstOrDefault(x => x.Name == typeInfo.Name));

            }
            return services;
        }
        /// <summary>
        /// Used to build a ServiceMigrator utility for migrating each assembly
        /// </summary>
        /// <param name="assembly">Assembly</param>
        /// <returns>A service provider</returns>
        private static ServiceProvider CreateserviceMigrator(Assembly assembly,IConfigurationRoot _config)
        {
            return new ServiceCollection().AddFluentMigratorCore().ConfigureRunner((x) =>
            {
                x.AddPostgres()
                .WithGlobalConnectionString($"Server={_config.GetSection("postgres").GetSection("host").Value};Port={_config.GetSection("postgres").GetSection("port").Value};Database={assembly.GetName().Name};User Id={_config.GetSection("postgres").GetSection("auth").GetSection("name").Value};Password={_config.GetSection("postgres").GetSection("auth").GetSection("password").Value};SslMode=Require;Trust Server Certificate=true;Pooling=true;")
                .ScanIn(assembly).For.Migrations();
            }).AddLogging(x => x.AddFluentMigratorConsole()).BuildServiceProvider();
        }
    }

}