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
    public static class ServiceCollectionHelper
    {
        public static IServiceCollection LoadAllServicesFromExternalAssembiles(this IServiceCollection services)
        {

            var assmg = ((Assembly)(typeof(Migration1_270720191).Assembly));
            var serviceProviderMigrateg = CreateserviceMigrator(assmg);

            using (var scope = serviceProviderMigrateg.CreateScope())
            {
                (scope as IServiceProvider).GetRequiredService<IMigrationRunner>().MigrateUp();
            }

            ModulesService.LoadAssembliesInDirrectory();
            List<Assembly> ass = new List<Assembly>();
            foreach (dynamic assembly in ModulesService.GetLoadedAssemblies())
            {
                ass.Add(assembly.ass);
            }

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
                    catch (Exception) { }
                }


            foreach (Assembly assm in ass)
            {
                IEnumerable<TypeInfo> servicesAss = assm.DefinedTypes.Where(x => !x.IsInterface && !x.IsEnum && x.IsClass && x.IsPublic && x.Name.Contains("Service"));
                if (servicesAss.Count() > 0)
                {
                    foreach (TypeInfo typeInfo in servicesAss)
                    {
                        services.AddSingleton(assm.GetTypes().Where(x => x.Name == typeInfo.Name).FirstOrDefault());
                    }
                }
            }
            return services;
        }

        private static ServiceProvider CreateserviceMigrator(Assembly assembly)
        {
            var coll = new ServiceCollection();
            coll.AddFluentMigratorCore().ConfigureRunner((x) =>
            {
                x.AddPostgres()
                .WithGlobalConnectionString($"Server=ssh.alivecreation.fr;Port=26257;Database={assembly.GetName().Name};User Id=matthieu;Password=4d9*jC%(hu\"ecN2&;SslMode=Require;Trust Server Certificate=true")
                .ScanIn(assembly).For.Migrations();
            });
            coll.AddLogging(x => x.AddFluentMigratorConsole());
            return coll.BuildServiceProvider();
        }
    }

}