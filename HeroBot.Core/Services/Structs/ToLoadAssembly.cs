using HeroBot.Common.ExtendedModules;
using System.Reflection;

namespace HeroBot.Core.Services.Structs
{
    internal struct ToLoadAssembly
    {
        public Assembly Assembly { get; internal set; }
        public ModuleLoadContext AssemblyContext { get; internal set; }
    }
}