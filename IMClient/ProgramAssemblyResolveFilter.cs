
// Type: IMClient.ProgramAssemblyResolveFilter




using Intermech.Interfaces.Plugins;
using Intermech.IO;
using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text.RegularExpressions;


namespace IMClient
{
    internal sealed class ProgramAssemblyResolveFilter : IAssemblyResolveFilter
    {
      private Regex serverAssemblies;
      private Regex kernelAssembly;
      private ConcurrentDictionary<string, bool> canResolveCache;

      public ProgramAssemblyResolveFilter()
      {
        this.serverAssemblies = RegexHelper.ToRegex("*.Server*", true);
        this.kernelAssembly = RegexHelper.ToRegex("Intermech.Kernel*", true);
        this.canResolveCache = new ConcurrentDictionary<string, bool>();
      }

      public bool CanResolve(string name)
      {
        return this.canResolveCache.GetOrAdd(name, new Func<string, bool>(this.CanResolveSlow));
      }

      private bool CanResolveSlow(string name)
      {
        AssemblyName assemblyName = new AssemblyName(name);
        return !this.serverAssemblies.IsMatch(assemblyName.Name) && !this.kernelAssembly.IsMatch(assemblyName.Name);
      }
    }
}
