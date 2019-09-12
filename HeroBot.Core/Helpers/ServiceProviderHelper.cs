using HeroBot.Common.Interfaces;
using HeroBot.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HeroBot.Core.Helpers
{
    public static class ServiceProviderHelper
    {
        /// <summary>
        /// Enable all the loaded services from <see cref="ServiceCollectionHelper.LoadAllServicesFromExternalAssembiles(IServiceCollection)"/>
        /// </summary>
        /// <param name="services">The this from the extension method</param>
        /// <returns>this</returns>
        public static ServiceProvider GetAllServicesFromExternalAssemblies(this ServiceProvider services)
        {
            foreach (AssemblyEntity d in RuntimeAssemblies.AssemblyEntities.Values)
            {
                Assembly ass = d.Assembly;
                IEnumerable<TypeInfo> servicesAss = ass.DefinedTypes.Where(x => !x.IsInterface && !x.IsEnum && x.IsClass && x.IsPublic && x.Name.Contains("Service"));
                if (servicesAss.Any())
                {
                    foreach (TypeInfo typeInfo in servicesAss)
                    {
                        services.GetRequiredService(ass.GetTypes().FirstOrDefault(x => x.Name == typeInfo.Name));
                    }
                }
            }
            return services;
        }
    }
}
