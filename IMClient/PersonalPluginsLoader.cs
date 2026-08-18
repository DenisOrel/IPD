using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Interfaces.Plugins;
using Intermech.IO;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;


namespace IMClient
{
    internal class PersonalPluginsLoader
    {
      private Version currentIPSVersion;
      private string pluginDirectory;
      private IMessageReporter diagnosticReporter;

      public PersonalPluginsLoader()
      {
        this.currentIPSVersion = this.GetType().Assembly.GetName(false).Version;
        this.pluginDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IPS\\Plugins");
        this.diagnosticReporter = (IMessageReporter) NullMessageReporter.Default;
      }

      public IMessageReporter DiagnosticReporter
      {
        [DebuggerStepThrough] get => this.diagnosticReporter;
        set
        {
          this.diagnosticReporter = value != null ? value : throw new ArgumentNullException(nameof (DiagnosticReporter));
        }
      }

      public void LoadPlugins(IPluginManager pluginManager)
      {
        if (pluginManager == null)
          throw new ArgumentNullException(nameof (pluginManager));
        if (!Directory.Exists(this.pluginDirectory))
          return;
        string[] directories = Directory.GetDirectories(this.pluginDirectory);
        List<PersonalPluginsLoader.PluginDirectoryInfo> foundPlugins = new List<PersonalPluginsLoader.PluginDirectoryInfo>(directories.Length);
        foreach (string directoryPath in directories)
        {
          PersonalPluginsLoader.PluginDirectoryInfo pluginInfo = this.CheckForPlugin(directoryPath);
          if (pluginInfo != null)
            this.CollectFoundPlugin(pluginInfo, (ICollection<PersonalPluginsLoader.PluginDirectoryInfo>) foundPlugins);
        }
        foreach (PersonalPluginsLoader.PluginDirectoryInfo pluginDirectoryInfo in foundPlugins)
          pluginManager.Load(pluginDirectoryInfo.AssemblyFilePath, false);
      }

      private void CollectFoundPlugin(
        PersonalPluginsLoader.PluginDirectoryInfo pluginInfo,
        ICollection<PersonalPluginsLoader.PluginDirectoryInfo> foundPlugins)
      {
        PersonalPluginsLoader.PluginDirectoryInfo plugin1 = CollectionUtils.Find<PersonalPluginsLoader.PluginDirectoryInfo>((IEnumerable<PersonalPluginsLoader.PluginDirectoryInfo>) foundPlugins, (Predicate<PersonalPluginsLoader.PluginDirectoryInfo>) (item => PathUtils.IsSamePath(item.AssemblyFileName, pluginInfo.AssemblyFileName)));
        if (plugin1 != null)
        {
          if (this.SelectBetterPluginVersion(plugin1, pluginInfo) == plugin1)
            return;
          foundPlugins.Remove(plugin1);
          foundPlugins.Add(pluginInfo);
        }
        else
          foundPlugins.Add(pluginInfo);
      }

      private PersonalPluginsLoader.PluginDirectoryInfo SelectBetterPluginVersion(
        PersonalPluginsLoader.PluginDirectoryInfo plugin1,
        PersonalPluginsLoader.PluginDirectoryInfo plugin2)
      {
        return !(plugin1.MinimalIPSVersion >= plugin2.MinimalIPSVersion) ? plugin2 : plugin1;
      }

      private PersonalPluginsLoader.PluginDirectoryInfo CheckForPlugin(string directoryPath)
      {
        string[] files = Directory.GetFiles(directoryPath, "*.dll.config");
        if (files.Length != 1)
        {
          if (files.Length != 0)
            this.ReportMessage($"Предупреждение: в каталоге модуля расширения '{directoryPath}' должен быть только один конфигурационный файл с расширением '.dll.config'. Данный каталог будет пропущен.");
          return (PersonalPluginsLoader.PluginDirectoryInfo) null;
        }
        string configFilePath = files[0];
        string str = configFilePath.Substring(0, configFilePath.Length - 7);
        if (!File.Exists(str))
        {
          this.ReportMessage($"Предупреждение: в каталоге модуля расширения '{directoryPath}' не найден модуль расширения '{Path.GetFileName(str)}'. Данный каталог будет пропущен.");
          return (PersonalPluginsLoader.PluginDirectoryInfo) null;
        }
        System.Configuration.Configuration configuration = this.LoadAssemblyConfiguration(configFilePath);
        Version minimalIPSVersion = this.TryReadMinimalIPSVersion(configuration);
        if (minimalIPSVersion == (Version) null)
        {
          this.ReportMessage($"Предупреждение: конфигурационном файле модуля расширения '{configFilePath}' не указана минимальная версия IPS, требуемая этому модулю. Модуль расширения не будет загружен.");
          return (PersonalPluginsLoader.PluginDirectoryInfo) null;
        }
        return this.currentIPSVersion < minimalIPSVersion ? (PersonalPluginsLoader.PluginDirectoryInfo) null : new PersonalPluginsLoader.PluginDirectoryInfo(directoryPath, str, configuration, minimalIPSVersion);
      }

      private System.Configuration.Configuration LoadAssemblyConfiguration(string configFilePath)
      {
        return ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap()
        {
          ExeConfigFilename = configFilePath
        }, ConfigurationUserLevel.None);
      }

      private Version TryReadMinimalIPSVersion(System.Configuration.Configuration assemblyConfiguration)
      {
        string version = TextServices.Trim(assemblyConfiguration.AppSettings.Settings["IPSVersion"].Value);
        return !string.IsNullOrEmpty(version) ? new Version(version) : (Version) null;
      }

      private void ReportMessage(string message)
      {
        this.DiagnosticReporter.WriteLine(message);
        this.DiagnosticReporter.EndMessage();
      }

      private class PluginDirectoryInfo
      {
        public PluginDirectoryInfo(
          string directoryPath,
          string assemblyFilePath,
          System.Configuration.Configuration configuration,
          Version minimalIPSVersion)
        {
          this.AssemblyFilePath = assemblyFilePath;
          this.AssemblyFileName = Path.GetFileName(assemblyFilePath);
          this.Configuration = configuration;
          this.MinimalIPSVersion = minimalIPSVersion;
        }

        public string DirectoryPath { get; private set; }

        public string AssemblyFilePath { get; private set; }

        public string AssemblyFileName { get; private set; }

        public System.Configuration.Configuration Configuration { get; private set; }

        public Version MinimalIPSVersion { get; private set; }
      }
    }
}
