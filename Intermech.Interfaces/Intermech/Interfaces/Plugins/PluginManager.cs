
// Type: Intermech.Interfaces.Plugins.PluginManager
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Interfaces.Configuration;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Protection;
using Intermech.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;


namespace Intermech.Interfaces.Plugins
{
    public class PluginManager : IPluginManager, IDisposable
    {
      private List<IPlugin> _plugins;
      private IServiceProvider _serviceProvider;
      private IConfigurationManager _configurationManager;
      private IOutputView _outputView;
      private string _mainOutputCategory;
      private string _errorDetailsOutputCategory;
      private IAlertMessageService _alertService;
      private IPackageActivator _packageActivator;
      private ArrayList _postLoad;
      private volatile bool _loadComplete;
      private bool _autoLoad;
      private Dictionary<string, Assembly> _assemblyResolveCache;
      private PathNormalizer _assemblyPathNormalizer;

      private Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
      {
        string name = args.Name;
        IAssemblyResolveFilter assemblyResolveFilter = this.AssemblyResolveFilter;
        if (assemblyResolveFilter != null && !assemblyResolveFilter.CanResolve(name))
          return (Assembly) null;
        Assembly assembly;
        lock (this._assemblyResolveCache)
        {
          if (!this._assemblyResolveCache.TryGetValue(name, out assembly))
          {
            assembly = this.OnAssemblyResolveSlow(name);
            if (assembly != (Assembly) null)
              this._assemblyResolveCache.Add(name, assembly);
          }
        }
        return assembly;
      }

      private Assembly OnAssemblyResolveSlow(string asmNameToResolve)
      {
        if (this.IsPartialAssemblyName(asmNameToResolve))
        {
          foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
          {
            if (this.GetPartialAssemblyName(assembly.FullName) == asmNameToResolve)
              return assembly;
          }
        }
        return (Assembly) null;
      }

      private bool IsPartialAssemblyName(string asmName) => asmName.IndexOf(',') == -1;

      private string GetPartialAssemblyName(string asmName)
      {
        int length = asmName.IndexOf(',');
        return length >= 0 ? asmName.Substring(0, length) : asmName;
      }

      /// <summary>Создать менеджер плагинов</summary>
      /// <param name="serviceProvider">Контейнер глобальных сервисов приложения</param>
      /// <param name="configurationManager">Сервис чтения/записи конфигураций</param>
      /// <param name="outputView">Сервис вывода информационных сообщений для пользователя</param>
      /// <param name="alertService">Сервис выводы тревожных сообщений для пользователя</param>
      /// <exception cref="T:ArgumentNullException">Параметры <paramref name="serviceProvider" />, <paramref name="configurationManager" />, <paramref name="outputView" />, <paramref name="alertService" /> не должны быть равны null</exception>
      public PluginManager(
        IServiceProvider serviceProvider,
        IConfigurationManager configurationManager,
        IOutputView outputView,
        IAlertMessageService alertService)
      {
        if (serviceProvider == null)
          throw new ArgumentNullException(nameof (serviceProvider));
        if (configurationManager == null)
          throw new ArgumentNullException(nameof (configurationManager));
        if (outputView == null)
          throw new ArgumentNullException(nameof (outputView));
        if (alertService == null)
          throw new ArgumentNullException(nameof (alertService));
        this._plugins = new List<IPlugin>();
        this._postLoad = new ArrayList();
        this._autoLoad = true;
        this._serviceProvider = serviceProvider;
        this._configurationManager = configurationManager;
        this._outputView = outputView;
        this._mainOutputCategory = LocalizationHolder.rm.GetString("Server_91");
        this._errorDetailsOutputCategory = "Загрузка модулей расширения";
        this._alertService = alertService;
        this._assemblyResolveCache = new Dictionary<string, Assembly>();
        this._packageActivator = (IPackageActivator) new DefaultPackageActivator();
        this._assemblyPathNormalizer = new PathNormalizer(AppDomain.CurrentDomain.BaseDirectory);
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(this.OnAssemblyResolve);
      }

      private string GetExceptionDetails(Exception e)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(e.Message);
          if (e is ReflectionTypeLoadException)
          {
            stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Server_82"), (object) Environment.NewLine);
            Exception[] loaderExceptions = (e as ReflectionTypeLoadException).LoaderExceptions;
            int length = loaderExceptions.Length;
            for (int index = 0; index < length; ++index)
              stringBuilder.AppendFormat("{0}{1}) {2}", (object) Environment.NewLine, (object) (index + 1), (object) loaderExceptions[index].Message);
          }
          if (!(e is ProtectionException))
          {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(ExceptionServices.GetExtendedStackTrace(e));
          }
          return stringBuilder.ToString();
        }
      }

      private void ShowErrorMessage(string errorMessage)
      {
        this.AlertMessageService.ShowMessage(this._errorDetailsOutputCategory, errorMessage, AlertMessageType.Error);
      }

      private void ReportErrorDetails(string errorDetails)
      {
        this.OutputView.WriteString(this._errorDetailsOutputCategory, errorDetails);
      }

      protected IPlugin FindPlugin(string fileName)
      {
        foreach (IPlugin plugin in this._plugins)
        {
          string fileName1 = Path.GetFileName(fileName.Trim());
          if (Path.GetFileName(plugin.Location).Equals(Path.GetFileName(fileName1), StringComparison.InvariantCultureIgnoreCase))
            return plugin;
        }
        return (IPlugin) null;
      }

      /// <summary>
      /// Загружает плагин и инициализирует все модули расширения в плагине.
      /// </summary>
      /// <param name="fileName">Путь к файлу плагина</param>
      /// <returns>Объект плагина</returns>
      public IPlugin Load(string fileName) => this.Load(fileName, false);

      /// <summary>
      /// Загружает плагин и инициализирует все модули расширения в плагине.
      /// </summary>
      /// <param name="fileName">Путь к файлу плагина</param>
      /// <param name="autoReload">Признак, что сведения о плагине необходимо сохранить в файле конфигурации для автоматической загрузки плагина при следующем запуске приложения</param>
      /// <returns>Объект плагина</returns>
      public IPlugin Load(string fileName, bool autoReload)
      {
        this.OutputView.WriteString(this._mainOutputCategory, string.Format(LocalizationHolder.rm.GetString("Server_85"), (object) fileName));
        string location = fileName;
        IPlugin plugin1 = this.FindPlugin(fileName);
        if (plugin1 != null)
          return plugin1;
        IPlugin plugin2;
        try
        {
          fileName = this.AssemblyPathNormalizer.Normalize(fileName);
          Assembly assembly = Assembly.LoadFrom(fileName);
          Version version = assembly.GetName().Version;
          string shortDateString = new FileInfo(assembly.Location).LastWriteTime.ToShortDateString();
          this.OutputView.WriteString(this._mainOutputCategory, string.Format(LocalizationHolder.rm.GetString("Server_85_2"), (object) assembly.Location, (object) version, (object) shortDateString));
          Plugin plugin3 = new Plugin(this, location, assembly);
          plugin3.LoadPackages(this.PackageActivator, this._serviceProvider, this._postLoad);
          plugin3.AutoReload = autoReload;
          this._plugins.Add((IPlugin) plugin3);
          this.RaisePluginAdded((IPlugin) plugin3);
          plugin2 = (IPlugin) plugin3;
          this.OutputView.WriteString(this._mainOutputCategory, "OK.");
        }
        catch (Exception ex)
        {
          plugin2 = (IPlugin) null;
          string errorMessage = $"При загрузке модуля расширения '{Path.GetFileName(fileName)}' произошла ошибка. Подробные технические сведения об ошибке доступны в окне 'Вывод' в категории '{this._errorDetailsOutputCategory}'.";
          this.ShowErrorMessage(errorMessage);
          this.OutputView.WriteString(this._mainOutputCategory, "   " + errorMessage);
          this.OutputView.WriteString(this._mainOutputCategory, LocalizationHolder.rm.GetString("Server_92"));
          this.ReportErrorDetails($"Необработанное исключение при загрузке модуля расширения '{fileName}'." + Environment.NewLine + this.GetExceptionDetails(ex));
        }
        return plugin2;
      }

      /// <summary>
      /// Завершает процесс автоматической загрузки плагинов.
      /// Все плагины, загружаемые после вызова этого метода считаются загруженными вручную.
      /// </summary>
      public void FinishAutoLoad()
      {
        if (this._loadComplete)
          return;
        if (this.LoadComplete != null)
        {
          foreach (EventHandler invocation in this.LoadComplete.GetInvocationList())
          {
            try
            {
              invocation((object) this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
              Type reflectedType = invocation.Method.ReflectedType;
              this.ShowErrorMessage($"При инициализации модуля расширения '{reflectedType.Assembly.ManifestModule.Name}' произошла ошибка. Подробные технические сведения об ошибке доступны в окне 'Вывод' в категории '{this._errorDetailsOutputCategory}'.");
              this.ReportErrorDetails($"Необработанное исключение при выполнении обработчика события IPluginManager.LoadComplete в классе '{reflectedType}'." + Environment.NewLine + this.GetExceptionDetails(ex));
            }
          }
        }
        this._loadComplete = true;
        this.RaisePostLoadMethod();
      }

      private void RaisePluginAdded(IPlugin plugin)
      {
        if (this.PluginAdded == null)
          return;
        this.PluginAdded((object) this, new PluginEventArgs(plugin));
      }

      private void RaisePluginRemoved(IPlugin plugin)
      {
        if (this.PluginRemoved == null)
          return;
        this.PluginRemoved((object) this, new PluginEventArgs(plugin));
      }

      private void RaisePostLoadMethod()
      {
        ArrayList arrayList = new ArrayList((ICollection) this._postLoad);
        int count = arrayList.Count;
        bool flag;
        do
        {
          flag = false;
          for (int index = 0; index < count; ++index)
          {
            if (arrayList[index] is IPackageExtension packageExtension)
            {
              try
              {
                if (packageExtension.PostInit())
                {
                  arrayList[index] = (object) null;
                  flag = true;
                }
              }
              catch (Exception ex)
              {
                arrayList[index] = (object) null;
                flag = true;
                Type type = packageExtension.GetType();
                this.ShowErrorMessage($"При инициализации модуля расширения '{type.Assembly.ManifestModule.Name}' произошла ошибка. Подробные технические сведения об ошибке доступны в окне 'Вывод'.");
                this.ReportErrorDetails($"Необработанное исключение при выполнении метода IPackageExtension.PostInit() в классе '{type}'." + Environment.NewLine + this.GetExceptionDetails(ex));
              }
            }
          }
        }
        while (flag);
      }

      public void LoadConfiguration()
      {
        this.ScanAssembliesForAutoLoad((Func<string, bool>) (location => this.Load(location, true) != null));
        this._autoLoad = false;
      }

      public void ScanAssembliesForAutoLoad(Func<string, bool> scanAction)
      {
        if (scanAction == null)
          throw new ArgumentNullException(nameof (scanAction));
        IConfiguration configuration1 = this.ConfigurationManager.Open(nameof (PluginManager));
        if (configuration1 == null)
          return;
        IConfiguration[] configurationArray = configuration1.Select("Plugin");
        int length = configurationArray.Length;
        for (int index = 0; index < length; ++index)
        {
          IConfiguration configuration2 = configurationArray[index];
          if (configuration2.HasProperty("Location"))
          {
            string property = configuration2.GetProperty("Location");
            if (!scanAction(property))
              this.TryScanIntermechPluginFromBaseDirectory(property, scanAction);
          }
        }
      }

      private bool TryScanIntermechPluginFromBaseDirectory(
        string location,
        Func<string, bool> scanAction)
      {
        string fileName = Path.GetFileName(location);
        return !string.IsNullOrEmpty(fileName) && fileName != location && fileName.StartsWith("Intermech") && scanAction(fileName);
      }

      public void SaveConfiguration()
      {
        this.ConfigurationManager.Delete(nameof (PluginManager));
        if (this.Plugins.Count <= 0)
          return;
        IConfiguration configuration = this.ConfigurationManager.Create(nameof (PluginManager));
        foreach (IPlugin plugin in this._plugins)
        {
          if (plugin.AutoReload)
            configuration.Add("Plugin").SetProperty("Location", plugin.Location);
          foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
          {
            if (package is IConfigurable configurable)
              configurable.SaveConfiguration(this.ConfigurationManager);
          }
        }
      }

      /// <summary>
      /// Выгружает плагин. Метод завершает работу всех модулей расширения в плагине и освобождает все выделенные ресурсы.
      /// Физически сборка плагина из памяти приложения не выгружается, так как это не поддерживается исполняющей средой.
      /// </summary>
      /// <param name="plugin">Объект плагина</param>
      /// <exception cref="T:ArgumentNullException">Параметр <paramref name="plugin" /> не должен быть равен null</exception>
      public void Unload(IPlugin plugin)
      {
        if (plugin == null)
          throw new ArgumentNullException(nameof (plugin));
        this._plugins.Remove(plugin);
        this.RaisePluginRemoved(plugin);
        plugin.Dispose();
      }

      public void LoadNextTime(IPlugin iPlugin, bool value)
      {
        if (!(iPlugin is Plugin plugin))
          return;
        plugin.AutoReload = value;
      }

      /// <summary>Возвращает коллекцию загруженных плагинов.</summary>
      public IList<IPlugin> Plugins => (IList<IPlugin>) this._plugins;

      public bool AutoLoad => this._autoLoad;

      /// <summary>Возвращает true, если загрузка плагинов завершена.</summary>
      public bool IsLoadComplete => this._loadComplete;

      public ArrayList PostLoadPackages => this._postLoad;

      public IConfigurationManager ConfigurationManager
      {
        [DebuggerStepThrough] get => this._configurationManager;
        [DebuggerStepThrough] set => this._configurationManager = value;
      }

      public IPackageActivator PackageActivator
      {
        [DebuggerStepThrough] get => this._packageActivator;
        [DebuggerStepThrough] set
        {
          this._packageActivator = value != null ? value : throw new ArgumentNullException(nameof (PackageActivator));
        }
      }

      public PathNormalizer AssemblyPathNormalizer
      {
        [DebuggerStepThrough] get => this._assemblyPathNormalizer;
      }

      /// <summary>
      /// Возвращает или задает фильтр для загрузчика сборок. Фильтр применяется к сборкам, которые не были найдены по обычным правилам поиска и загрузки сборок на платформе .NET.
      /// По умолчанию значение свойства не задано.
      /// </summary>
      public IAssemblyResolveFilter AssemblyResolveFilter { get; set; }

      internal IOutputView OutputView
      {
        [DebuggerStepThrough] get => this._outputView;
      }

      internal IAlertMessageService AlertMessageService
      {
        [DebuggerStepThrough] get => this._alertService;
      }

      /// <summary>Событие успешной выгрузки плагина.</summary>
      public event PluginEventHandler PluginRemoved;

      /// <summary>Событие успешной загрузки и инициализации плагина.</summary>
      public event PluginEventHandler PluginAdded;

      /// <summary>Событие завершения загрузки плагинов.</summary>
      public event EventHandler LoadComplete;

      public void Dispose()
      {
        AppDomain.CurrentDomain.AssemblyResolve -= new ResolveEventHandler(this.OnAssemblyResolve);
      }
    }
}
