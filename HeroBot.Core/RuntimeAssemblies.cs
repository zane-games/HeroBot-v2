using HeroBot.Common.Interfaces;
using System.Collections.Generic;

namespace HeroBot.Core
{
    public class RuntimeAssemblies
    {
        public static Dictionary<string,AssemblyEntity> AssemblyEntities { get; } = new Dictionary<string, AssemblyEntity>();
    }
}
