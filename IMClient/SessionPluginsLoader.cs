
// Type: IMClient.SessionPluginsLoader




using Intermech.Diagnostics;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;


namespace IMClient
{
    internal sealed class SessionPluginsLoader
    {
      private IFileVault fileVaultService;
      private IMessageReporter diagnosticReporter;

      public SessionPluginsLoader(IFileVault fileVaultService)
      {
        this.fileVaultService = fileVaultService != null ? fileVaultService : throw new ArgumentNullException(nameof (fileVaultService));
        this.diagnosticReporter = (IMessageReporter) NullMessageReporter.Default;
      }

      public ISplashService SplashService { get; set; }

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
        List<SessionPluginsLoader.DatabasePluginInfo> pluginsFromDatabase = this.GetPluginsFromDatabase();
        if (this.SplashService != null)
        {
          this.SplashService.Position = 0;
          this.SplashService.Steps = pluginsFromDatabase.Count;
          this.SplashService.StepName = LocalizationHolder.rm.GetString("IMClient_73");
        }
        foreach (SessionPluginsLoader.DatabasePluginInfo pluginInfo in pluginsFromDatabase)
        {
          if (this.SplashService != null)
            this.SplashService.StepDescription = pluginInfo.AssemblyName;
          if (pluginInfo.FileName != string.Empty)
            this.TryLoadDBStoredPlugin(pluginManager, pluginInfo);
          else
            this.TryLoadFileSystemStoredPlugin(pluginManager, pluginInfo);
          if (this.SplashService != null)
            this.SplashService.StepIt();
        }
      }

      private bool IsDBStoredPlugin(SessionPluginsLoader.DatabasePluginInfo pluginInfo)
      {
        return this.fileVaultService.DBFilesInfo.GetMasterFileName(pluginInfo.ObjectId, false) != null;
      }

      private void TryLoadDBStoredPlugin(
        IPluginManager pluginManager,
        SessionPluginsLoader.DatabasePluginInfo pluginInfo)
      {
        string str1 = Path.Combine(Path.Combine(this.fileVaultService.CacheArea.AreaPath, "Plugins"), pluginInfo.ObjectId.ToString());
        if (!Directory.Exists(str1))
          Directory.CreateDirectory(str1);
        List<FileState> fileStates = this.fileVaultService.DBFilesInfo.GetFileStates(pluginInfo.ObjectId);
        string fileName = (string) null;
        List<IFileAttributeAction> actions = new List<IFileAttributeAction>(fileStates.Count * 2);
        foreach (FileState fileState1 in fileStates)
        {
          string str2 = Path.Combine(str1, fileState1.FileName);
          if (File.Exists(str2))
          {
            FileState fileState2 = FileState.FromFile(str2);
            if (fileState2.CompareTo(fileState1) != 0)
            {
              actions.Add((IFileAttributeAction) new DeleteLocalFileAction(fileState2, str2));
              actions.Add((IFileAttributeAction) new DownloadFileAction(fileState1, str2));
            }
          }
          else
            actions.Add((IFileAttributeAction) new DownloadFileAction(fileState1, str2));
          if (fileName == null && !string.IsNullOrEmpty(pluginInfo.AssemblyName) && PathUtils.IsSamePath(pluginInfo.AssemblyName, Path.GetFileName(str2)))
            fileName = str2;
        }
        if (fileName == null && !string.IsNullOrEmpty(pluginInfo.AssemblyName))
        {
          this.ReportMessage($"Ошибка: некорректно задано имя загружаемого файла у модуля расширения '{pluginInfo.PluginName}' (ид. версии {pluginInfo.ObjectId}). В файловом атрибуте модуля расширения отсутствует файл '{pluginInfo.AssemblyName}'.");
        }
        else
        {
          FileOperations.BatchReadFiles(pluginInfo.ObjectId, (ICollection<IFileAttributeAction>) actions);
          if (fileName == null)
            fileName = Path.Combine(str1, fileStates[0].FileName);
          pluginManager.Load(fileName);
        }
      }

      private void TryLoadFileSystemStoredPlugin(
        IPluginManager pluginManager,
        SessionPluginsLoader.DatabasePluginInfo pluginInfo)
      {
        if (string.IsNullOrEmpty(pluginInfo.AssemblyName))
          this.ReportMessage($"Ошибка: не задано загружаемого файла у модуля расширения '{pluginInfo.PluginName}' (ид. версии {pluginInfo.ObjectId})");
        else
          pluginManager.Load(pluginInfo.AssemblyName);
      }

      private void ReportMessage(string message)
      {
        this.DiagnosticReporter.WriteLine(message);
        this.DiagnosticReporter.EndMessage();
      }

      private List<SessionPluginsLoader.DatabasePluginInfo> GetPluginsFromDatabase()
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return this.CreatePluginsFromDataTable(sessionKeeper.Session.GetClientPlugins());
      }

      private List<SessionPluginsLoader.DatabasePluginInfo> CreatePluginsFromDataTable(
        DataTable dataTable)
      {
        if (dataTable == null)
          throw new ArgumentNullException(nameof (dataTable));
        List<SessionPluginsLoader.DatabasePluginInfo> pluginsFromDataTable = new List<SessionPluginsLoader.DatabasePluginInfo>(dataTable.Rows.Count);
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          pluginsFromDataTable.Add(this.CreatePluginFromDataRow(row));
        return pluginsFromDataTable;
      }

      private SessionPluginsLoader.DatabasePluginInfo CreatePluginFromDataRow(DataRow dataRow)
      {
        if (dataRow == null)
          throw new ArgumentNullException(nameof (dataRow));
        SessionPluginsLoader.DatabasePluginInfo pluginFromDataRow = new SessionPluginsLoader.DatabasePluginInfo();
        if (!Convert.IsDBNull(dataRow[0]))
          pluginFromDataRow.ObjectId = Convert.ToInt64(dataRow[0]);
        if (!Convert.IsDBNull(dataRow[1]))
          pluginFromDataRow.PluginName = Convert.ToString(dataRow[1]).Trim();
        if (!Convert.IsDBNull(dataRow[2]))
          pluginFromDataRow.AssemblyName = Convert.ToString(dataRow[2]).Trim();
        if (!Convert.IsDBNull(dataRow[3]))
          pluginFromDataRow.AssemblyVersion = Convert.ToString(dataRow[3]).Trim();
        pluginFromDataRow.FileName = Convert.IsDBNull(dataRow[4]) ? string.Empty : Convert.ToString(dataRow[4]).Trim();
        if (!string.IsNullOrEmpty(pluginFromDataRow.AssemblyName) && !pluginFromDataRow.AssemblyName.EndsWith(".dll") && !pluginFromDataRow.AssemblyName.EndsWith(".exe"))
          pluginFromDataRow.AssemblyName += ".dll";
        return pluginFromDataRow;
      }

      private sealed class DatabasePluginInfo
      {
        public DatabasePluginInfo() => this.ObjectId = 0L;

        public override string ToString()
        {
          return $"#{this.ObjectId}, {this.PluginName}, {this.AssemblyName}, {this.AssemblyVersion}";
        }

        public long ObjectId { get; set; }

        public string PluginName { get; set; }

        public string AssemblyName { get; set; }

        public string AssemblyVersion { get; set; }

        public string FileName { get; set; }
      }
    }
}
