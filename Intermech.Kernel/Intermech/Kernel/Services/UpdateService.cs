// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.UpdateService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Services.MetadataUpdates;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;


namespace Intermech.Kernel.Services;

internal sealed class UpdateService : LongLifeObject, IUpdateService, IUpdateLogService
{
  private List<string> _filters = new List<string>();
  private readonly string _errorString = "Ошибка при";
  private readonly string _startLogString = LocalizationHolder.rm.GetString("Server_99");
  private readonly string _endLogString = LocalizationHolder.rm.GetString("Server_102");
  private readonly string _traceFileName = string.Empty;
  private IEventLogHelper _eventHelper;
  private IObligatoryObjectsRegistryService _obligatoryObjectsService;
  private IUserSession _session;
  private List<Tuple<string, IUpdatable>> _modules;
  private readonly object _syncRoot;

  public UpdateService(
    IEventLogHelper eventHelper,
    IObligatoryObjectsRegistryService obligatoryObjectsService)
  {
    this._eventHelper = eventHelper ?? throw new ArgumentNullException(nameof (eventHelper));
    this._obligatoryObjectsService = obligatoryObjectsService ?? throw new ArgumentNullException(nameof (obligatoryObjectsService));
    if (ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service)
      this._session = service.GetSystemSessionPermanentClone("UpdateService_Configurations");
    this._modules = new List<Tuple<string, IUpdatable>>();
    this._syncRoot = new object();
    this.LoadFilters();
  }

  private bool LoadObligatoryObjects(
    IUserSession session,
    IEventLogHelper eventHelper,
    string fileName)
  {
    bool flag = false;
    FileInfo fileInfo = new FileInfo(fileName);
    try
    {
      if (!fileInfo.Exists)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1039"), (object) fileInfo.FullName));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(fileName);
      for (int i1 = 0; i1 < xmlDocument.ChildNodes.Count; ++i1)
      {
        XmlNode childNode1 = xmlDocument.ChildNodes[i1];
        if (childNode1.NodeType == XmlNodeType.Element && childNode1.Name == "Objects")
        {
          string str = childNode1.Attributes["PluginVersion"].Value;
          if (childNode1.HasChildNodes)
          {
            for (int i2 = 0; i2 < childNode1.ChildNodes.Count; ++i2)
            {
              XmlNode childNode2 = childNode1.ChildNodes[i2];
              if (childNode2.NodeType == XmlNodeType.Element)
              {
                int int32 = Convert.ToInt32(childNode2.Attributes["CategoryID"].Value);
                Guid guid = new Guid(childNode2.Attributes["Guid"].Value);
                try
                {
                  NodeReader nodeReader = NodeReaderHelper.GetNodeReader(childNode2, session, eventHelper, fileInfo.DirectoryName, int32, this._obligatoryObjectsService, guid);
                  if (nodeReader == null)
                    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1040"), (object) int32));
                  (session as UserSession).StartTransaction();
                  nodeReader.Read();
                  (session as UserSession).Commit();
                }
                catch (Exception ex)
                {
                  flag = true;
                  (session as UserSession).Rollback();
                  if (eventHelper != null)
                  {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(ex.Message);
                    ObjectsFoundException objectsFoundException = this.FoundObjectsFoundException(ex);
                    if (objectsFoundException != null && objectsFoundException.ObjectsID.Length != 0)
                    {
                      stringBuilder.Append("(");
                      for (int index = 0; index < objectsFoundException.ObjectsID.Length; ++index)
                      {
                        if (index > 0)
                          stringBuilder.Append(", ");
                        stringBuilder.Append(objectsFoundException.ObjectsID[index]);
                      }
                      stringBuilder.Append(")");
                    }
                    this.WriteToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1041"), (object) guid, (object) int32, (object) fileInfo.FullName, (object) stringBuilder.ToString()), ex.StackTrace);
                  }
                }
              }
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      this.WriteToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_1042"), (object) fileInfo.FullName, (object) ex.Message), ex.StackTrace);
      flag = true;
    }
    return !flag;
  }

  private ObjectsFoundException FoundObjectsFoundException(Exception ex)
  {
    if (ex is ObjectsFoundException)
      return (ObjectsFoundException) ex;
    return ex.InnerException != null ? this.FoundObjectsFoundException(ex.InnerException) : (ObjectsFoundException) null;
  }

  public void AddModule(string name, IUpdatable module)
  {
    if (module == null)
      throw new ArgumentNullException(nameof (module));
    lock (this._syncRoot)
      this._modules.Add(new Tuple<string, IUpdatable>(name, module));
  }

  private void CheckUpdateEnable(IUserSession session, int currentScriptsLocale)
  {
    object obj = (session as UserSession).DataManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'LOCALE'");
    if (obj != null && obj != DBNull.Value && Convert.ToInt32(obj) != currentScriptsLocale)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("ScriptsLanguageDif"), (object) currentScriptsLocale, obj));
  }

  public bool StartUpdate(string updatesFolder)
  {
    lock (this._syncRoot)
      return this.StartUpdateInternal(updatesFolder);
  }

  private bool StartUpdateInternal(string updatesFolder)
  {
    bool flag = false;
    if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
      Console.WriteLine(LocalizationHolder.rm.GetString("Kernel_1043"));
    IUserSession session = (IUserSession) null;
    try
    {
      MetaDataHelper.Locked = true;
      session = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("UPDATE_SERVICE");
      this.CheckUpdateEnable(session, 570);
      foreach (Tuple<string, IUpdatable> module in this._modules)
      {
        if (module.Item2 is IExtendedUpdatable extendedUpdatable)
          extendedUpdatable.BeforeUpdates(session);
      }
      foreach (Tuple<string, IUpdatable> module in this._modules)
      {
        IUpdatable updatable = module.Item2;
        try
        {
          string[] updateScripts = updatable.GetUpdateScripts();
          if (updateScripts != null)
          {
            foreach (string str in updateScripts)
            {
              updatable.BeforeExecScript(session, str);
              if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
                Console.WriteLine(string.Format(LocalizationHolder.rm.GetString("Kernel_1044"), (object) str));
              if (!this.LoadObligatoryObjects(session, this._eventHelper, Path.Combine(updatesFolder, str)) && !flag)
                flag = true;
              updatable.AfterExecScript(session, str);
            }
          }
          updatable.AfterExecAllScripts(session);
        }
        catch (Exception ex)
        {
          this._eventHelper.AddToTrace($"Ошибка при выполнении обновлений модуля {module.Item1}: {ex.Message}", Consts.traceAlways, this._traceFileName);
          this._eventHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, this._traceFileName);
        }
      }
    }
    finally
    {
      MetaDataHelper.Locked = false;
      MetaDataHelper.SyncMetadata((session as IUserSessionCacheDataSet).CacheDataSet, true);
      session?.Logout("UPDATE_SERVICE");
    }
    return !flag;
  }

  public void WriteToLog(string message, string stack)
  {
    if (this.InFilter(message))
      return;
    this._eventHelper.AddToTrace(message, Consts.traceAlways, this._traceFileName);
    this._eventHelper.AddToTrace(stack, Consts.traceAlways, this._traceFileName);
  }

  public void WriteStartToLog()
  {
    this._eventHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_99"), Consts.traceAlways, this._traceFileName);
  }

  public void WriteEndToLog()
  {
    this._eventHelper.AddToTrace(LocalizationHolder.rm.GetString("Server_102"), Consts.traceAlways, this._traceFileName);
  }

  private bool InFilter(string message)
  {
    foreach (string filter in this._filters)
    {
      if (message.Contains(filter))
        return true;
    }
    return false;
  }

  public bool ClearLogFilters()
  {
    if (this._filters.Count <= 0)
      return false;
    this._filters.Clear();
    this.SaveFilters();
    return true;
  }

  public int AddLogFilter(string filter)
  {
    if (this._filters.Exists((Predicate<string>) (x => x.Equals(filter))))
      return 0;
    this._filters.Add(filter);
    this.SaveFilters();
    return 1;
  }

  public bool RemoveLogFilter(string filter)
  {
    int index = this._filters.FindIndex((Predicate<string>) (x => x.Equals(filter)));
    if (index < 0)
      return false;
    this._filters.RemoveAt(index);
    this.SaveFilters();
    return true;
  }

  public int EditLogFilter(string oldFilter, string newFilter)
  {
    int index = this._filters.FindIndex((Predicate<string>) (x => x.Equals(oldFilter)));
    if (index < 0)
      return 0;
    this._filters[index] = newFilter;
    this.SaveFilters();
    return 1;
  }

  public string[] GetLastUpdateLog(bool filtered)
  {
    List<string> stringList1 = new List<string>();
    bool flag1 = false;
    using (StreamReader streamReader = File.OpenText(this._logFileName))
    {
      while (streamReader.Peek() > 0)
      {
        string str = streamReader.ReadLine();
        if (str.EndsWith(this._startLogString))
        {
          flag1 = true;
          if (stringList1.Count > 0)
            stringList1.Clear();
        }
        else if (str.EndsWith(this._endLogString))
          flag1 = false;
        else if (flag1)
          stringList1.Add(str);
      }
    }
    if (!filtered)
      return stringList1.ToArray();
    List<string> stringList2 = new List<string>();
    bool flag2 = false;
    foreach (string message in stringList1)
    {
      if (message.Contains(this._errorString))
        flag2 = this.InFilter(message);
      if (!flag2)
        stringList2.Add(message);
    }
    return stringList2.ToArray();
  }

  private void LoadFilters()
  {
    this._filters.Clear();
    DataTable dataTable = this._session.Configurations.ReadSection("KERNEL", nameof (UpdateService), 0L);
    if (dataTable.Rows.Count <= 0)
      return;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (Convert.ToString(row["F_PARAM_NAME"]).StartsWith("Filter"))
        this._filters.Add(Convert.ToString(row["F_VALUE"]));
    }
  }

  public string[] Filters => this._filters.ToArray();

  private void SaveFilters()
  {
    DataTable table = new DataTable();
    table.Columns.AddRange(new DataColumn[2]
    {
      new DataColumn("F_PARAM_NAME"),
      new DataColumn("F_VALUE")
    });
    int num = 0;
    foreach (string filter in this._filters)
    {
      table.Rows.Add((object) $"Filter{num}", (object) filter);
      ++num;
    }
    this._session.Configurations.WriteSection("KERNEL", nameof (UpdateService), table, 0L);
  }

  private string _logFileName
  {
    get => (this._eventHelper as EventLogHelper).GetFullTraceFileName(this._traceFileName);
  }
}
