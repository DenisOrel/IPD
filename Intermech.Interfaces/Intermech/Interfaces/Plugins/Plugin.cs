
// Type: Intermech.Interfaces.Plugins.Plugin
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Configuration;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;


namespace Intermech.Interfaces.Plugins
{
    internal sealed class Plugin : IPlugin, IDisposable
    {
      private PluginManager _manager;
      private Assembly _assembly;
      private string _location;
      private List<IPackage> _packages;
      private bool _autoReload;

      public Plugin(PluginManager manager, string location, Assembly assembly)
      {
        if (manager == null)
          throw new ArgumentNullException(nameof (manager));
        if (location == null)
          throw new ArgumentNullException(nameof (location));
        if (assembly == (Assembly) null)
          throw new ArgumentNullException(nameof (assembly));
        this._packages = new List<IPackage>();
        this._manager = manager;
        this._location = location;
        this._assembly = assembly;
      }

      public void LoadPackages(
        IPackageActivator packageActivator,
        IServiceProvider serviceProvider,
        ArrayList postLoadPackages)
      {
        if (packageActivator == null)
          throw new ArgumentNullException(nameof (packageActivator));
        if (serviceProvider == null)
          throw new ArgumentNullException(nameof (serviceProvider));
        if (postLoadPackages == null)
          throw new ArgumentNullException(nameof (postLoadPackages));
        Type[] types = this._assembly.GetTypes();
        Type type1 = typeof (IPackage);
        int length = types.Length;
        for (int index = 0; index < length; ++index)
        {
          Type type2 = types[index];
          if (type2.IsClass && !type2.IsAbstract && type1.IsAssignableFrom(type2))
          {
            IPackage instance = packageActivator.CreateInstance(type2);
            instance.Load(serviceProvider);
            if (instance is IConfigurable configurable)
              configurable.LoadConfiguration(this._manager.ConfigurationManager);
            if (instance is IPackageExtension)
              postLoadPackages.Add((object) instance);
            this._packages.Add(instance);
            this._manager.OutputView.WriteString(LocalizationHolder.rm.GetString("Server_76"), string.Format(LocalizationHolder.rm.GetString("Server_77"), (object) instance.Name));
          }
        }
      }

      public void Dispose()
      {
        foreach (IPackage package in this._packages)
          package.Unload();
      }

      [CustomDisplayName("Attribute.Server_1")]
      [CustomDescription("Attribute.Server_2")]
      public string Location => this._location;

      [CustomDisplayName("Attribute.Server_3")]
      [CustomDescription("Attribute.Server_4")]
      public string Name => this._assembly.GetName().Name;

      [Browsable(false)]
      public IList<IPackage> Packages => (IList<IPackage>) this._packages;

      /// <summary>
      /// Возвращает или задает признак, что сведения о плагине необходимо сохранить в файле конфигурации для автоматической загрузки плагина при следующем запуске приложения
      /// </summary>
      [CustomDisplayName("Attribute.Server_5")]
      [CustomDescription("Attribute.Server_6")]
      public bool AutoReload
      {
        get => this._autoReload;
        internal set => this._autoReload = value;
      }
    }
}
