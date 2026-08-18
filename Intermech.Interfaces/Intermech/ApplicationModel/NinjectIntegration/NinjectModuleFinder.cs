
// Type: Intermech.ApplicationModel.NinjectIntegration.NinjectModuleFinder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;


namespace Intermech.ApplicationModel.NinjectIntegration
{
    internal sealed class NinjectModuleFinder
    {
      private Type ninjectModuleInterface;

      public NinjectModuleFinder() => this.ninjectModuleInterface = typeof (INinjectModule);

      public ICollection<Type> FindModules(Assembly assembly, Predicate<Type> predicate)
      {
        if (assembly == (Assembly) null)
          throw new ArgumentNullException(nameof (assembly));
        if (predicate == null)
          throw new ArgumentNullException(nameof (predicate));
        Type[] types = assembly.GetTypes();
        List<Type> modules = new List<Type>();
        foreach (Type c in types)
        {
          if (this.NinjectModuleInterface.IsAssignableFrom(c) && !c.IsAbstract && !c.IsInterface && c.GetConstructor(Type.EmptyTypes) != (ConstructorInfo) null && predicate(c))
            modules.Add(c);
        }
        return (ICollection<Type>) modules;
      }

      private Type NinjectModuleInterface
      {
        [DebuggerStepThrough] get => this.ninjectModuleInterface;
      }
    }
}
