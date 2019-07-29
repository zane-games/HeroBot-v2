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
        public static ServiceProvider GetAllServicesFromExternalAssemblies(this ServiceProvider services)
        {
            foreach (dynamic d in ModulesService.GetLoadedAssemblies())
            {
                Assembly ass = d.ass;
                IEnumerable<TypeInfo> servicesAss = ass.DefinedTypes.Where(x => !x.IsInterface && !x.IsEnum && x.IsClass && x.IsPublic && x.Name.Contains("Service"));
                if (servicesAss.Count() > 0)
                {
                    foreach (TypeInfo typeInfo in servicesAss)
                    {
                        services.GetRequiredService(ass.GetTypes().Where(x => x.Name == typeInfo.Name).FirstOrDefault());
                    }
                }
            }
            return services;
        }
    }
}
