
// Type: Intermech.Runtime.ComInterop.LocalServer.ComClassSearchHelper
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    internal sealed class ComClassSearchHelper : MarshalByRefObject
    {
      public List<string> GetComClasses(ICollection<string> pluginPaths, bool isolatedMode = false)
      {
        if (pluginPaths == null)
          throw new ArgumentNullException(nameof (pluginPaths));
        if (pluginPaths.Count == 0)
          return new List<string>(0);
        return !isolatedMode ? this.GetComClassesInternal(pluginPaths) : this.GetComClassesIsolated(pluginPaths);
      }

      private List<string> GetComClassesIsolated(ICollection<string> pluginPaths)
      {
        AppDomain domain = AppDomain.CreateDomain(nameof (ComClassSearchHelper), (Evidence) null, new AppDomainSetup()
        {
          ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
          ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
        });
        try
        {
          return ((ComClassSearchHelper) domain.CreateInstanceAndUnwrap(typeof (ComClassSearchHelper).Assembly.FullName, typeof (ComClassSearchHelper).FullName)).GetComClassesInternal(pluginPaths);
        }
        finally
        {
          AppDomain.Unload(domain);
        }
      }

      private List<string> GetComClassesInternal(ICollection<string> pluginPaths)
      {
        RegistrationServices registrationServices = new RegistrationServices();
        List<string> comClassesInternal = new List<string>();
        foreach (string pluginPath in (IEnumerable<string>) pluginPaths)
        {
          Assembly assembly = Assembly.LoadFrom(pluginPath);
          foreach (Type type in registrationServices.GetRegistrableTypesInAssembly(assembly))
            comClassesInternal.Add(type.AssemblyQualifiedName);
        }
        return comClassesInternal;
      }
    }
}
