// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseIndexingService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseIndexingService : LongLifeObject, IImbaseIndexingService
{
  private static object _lock = new object();
  private const string TblPrefix = "IMS_IDATA";
  private const string ModuleName = "IMBASE.INDEX";
  private IImbaseParamsService _imbaseParamsService;

  private List<ImbaseIndexingService.Task> Tasks { get; }

  private List<ImbaseIndexingService.Task> CompletedTasks { get; }

  public ImbaseIndexingService()
  {
    this.Tasks = new List<ImbaseIndexingService.Task>();
    this.CompletedTasks = new List<ImbaseIndexingService.Task>();
    this._imbaseParamsService = ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true);
  }

  private int GetFlagValue(bool unique)
  {
    int flagValue = -1;
    int int32 = unique ? Convert.ToInt32((object) IndexesFlags.UniqueValue) : 0;
    if (int32 != 0)
      flagValue = int32;
    return flagValue;
  }

  private void CheckBase(UserSession session, Guid sessionGuid, List<long> catalogIDs)
  {
    session = session ?? this.GetUserSession(sessionGuid, "ImbaseIndexing.CheckBase");
    IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":pAttrID", (object) -1);
    if (catalogIDs.Count == 1)
    {
      if (catalogIDs[0] == -1L)
      {
        DataTable dataTable = session.DataManager.ExecuteDataTable($"SELECT {IndexesField.F_CATALOG_ID} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_ATTRIBUTE_ID}=:pAttrID", dbDataParameter1);
        if (dataTable != null && dataTable.Rows.Count > 0)
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_Path_BusyCatalog"), (object) Convert.ToInt64(dataTable.Rows[0][IndexesField.F_CATALOG_ID])));
      }
      else
      {
        IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":pCatalogID", (object) catalogIDs[0]);
        if (Convert.ToInt32(session.DataManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_CATALOG_ID}=:pCatalogID AND {IndexesField.F_ATTRIBUTE_ID}=:pAttrID", dbDataParameter2, dbDataParameter1)) > 0)
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_Path_BusyCatalog"), (object) catalogIDs[0]));
      }
    }
    else
    {
      List<IDbDataParameter> pars = new List<IDbDataParameter>(catalogIDs.Count);
      pars.Add(dbDataParameter1);
      string paramsRange = this.CreateParamsRange<long>(session.DataManager, catalogIDs, pars);
      DataTable dataTable = session.DataManager.ExecuteDataTable(string.Format("SELECT {0} FROM IMS_IMBASE_INDEXES WHERE {1}=:pAttrID AND {0} IN {2}", (object) IndexesField.F_CATALOG_ID, (object) IndexesField.F_ATTRIBUTE_ID, (object) paramsRange), pars.ToArray());
      if (dataTable != null && dataTable.Rows.Count > 0)
        throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_Path_BusyCatalog"), (object) Convert.ToInt64(dataTable.Rows[0][IndexesField.F_CATALOG_ID])));
    }
  }

  public static string GenerateTableName(long catalogId, int attrId)
  {
    return $"{"IMS_IDATA"}_{catalogId}_{attrId}";
  }

  public void CreateTable(
    IDbManager manager,
    long catalogId,
    int attrId,
    bool uniqueFlag,
    string tableName,
    IndexesStates state = IndexesStates.Locked)
  {
    manager.BeginTransaction();
    try
    {
      manager.ExecuteNonQuery($"INSERT INTO IMS_IMBASE_INDEXES ({IndexesField.F_CATALOG_ID}, {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_FLAG}, {IndexesField.F_TABLE_NAME}, {IndexesField.F_ATTRIBUTE_STATE}) VALUES (:c_ID, :a_ID, :parFlag, :parTblName, :parState)", manager.Parameter(":c_ID", (object) catalogId), manager.Parameter(":a_ID", (object) attrId), manager.Parameter(":parFlag", (object) this.GetFlagValue(uniqueFlag)), manager.Parameter(":parTblName", (object) tableName), manager.Parameter(":parState", (object) (int) state));
      switch (manager.DataProvider.Name)
      {
        case "Sql":
          manager.ExecuteNonQuery(string.Format(this.CreateIdataSql, (object) tableName));
          break;
        case "Oracle":
          manager.ExecuteNonQuery(string.Format(this.CreateIdataOra, (object) tableName));
          break;
        case "PostgreSQL":
          manager.ExecuteNonQuery(string.Format(this.CreateIdataPostgre, (object) tableName));
          break;
      }
      manager.Commit();
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw;
    }
  }

  private string CreateIdataSql
  {
    get
    {
      return $"CREATE TABLE {{0}} ( {IndexesField.F_LINK_ID} BigNumber_DEF NOT NULL, {IndexesField.F_TABLE_ID} BigNumber_DEF NOT NULL, {IndexesField.F_TABKEY} INTEGER NOT NULL, {IndexesField.F_TEXT} nvarchar(850) NULL, {IndexesField.F_HASHTEXT} nvarchar(850) NULL, {IndexesField.F_APPLICABILITY} INTEGER NOT NULL)";
    }
  }

  private string CreateIdataOra
  {
    get
    {
      return $"CREATE TABLE {{0}} ( {IndexesField.F_LINK_ID}  INTEGER NOT NULL, {IndexesField.F_TABLE_ID} INTEGER NOT NULL, {IndexesField.F_TABKEY} INTEGER NOT NULL, {IndexesField.F_TEXT} NVARCHAR2(850) NULL, {IndexesField.F_HASHTEXT} NVARCHAR2(850) NULL, {IndexesField.F_APPLICABILITY} INTEGER NOT NULL)";
    }
  }

  private string CreateIdataPostgre
  {
    get
    {
      return $"CREATE TABLE {{0}} ( {IndexesField.F_LINK_ID}  bigint NOT NULL, {IndexesField.F_TABLE_ID} bigint NOT NULL, {IndexesField.F_TABKEY} INTEGER NOT NULL, {IndexesField.F_TEXT} varchar(850) NULL, {IndexesField.F_HASHTEXT} varchar(850) NULL, {IndexesField.F_APPLICABILITY} INTEGER NOT NULL)";
    }
  }

  private bool RebuildIndex(
    Guid sessionGuid,
    Guid taskGuid,
    long catalogId,
    Dictionary<int, bool> attrs,
    string tableName,
    IEventLogHelper eventHelper)
  {
    bool flag = true;
    if (catalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    if (attrs == null || attrs.Count == 0)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.AttributeListEmpty);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, catalogId)
    {
      Attributes = attrs.Keys.ToList<int>(),
      TaskName = LocalizationHolder.rm.GetString("Imbase.Server_36")
    });
    this.Add(sessionGuid, taskGuid, attrs);
    List<Exception> result = this.GetResult(taskGuid);
    if (result != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Exception exception in result)
      {
        if (exception is IndexingException indexingException)
        {
          stringBuilder.AppendLine(indexingException.Message);
          stringBuilder.AppendLine(indexingException.StackTrace);
          if (indexingException.InnerException != null)
            stringBuilder.AppendLine(indexingException.InnerException.Message);
        }
      }
      if (stringBuilder.Length > 0)
      {
        eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_38"), (object) tableName, (object) stringBuilder));
        flag = false;
      }
    }
    else
      eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_35"), (object) tableName));
    return flag;
  }

  public bool RemoveTable(IDbManager manager, string tableName)
  {
    if (string.IsNullOrEmpty(tableName))
      return false;
    manager.BeginTransaction();
    try
    {
      manager.ExecuteNonQuery("DROP TABLE " + tableName);
      manager.ExecuteNonQuery($"DELETE FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_TABLE_NAME}=:parTblName", manager.Parameter(sc_8027.ssp_appserver_8028(), (object) tableName));
      manager.Commit();
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw;
    }
    return true;
  }

  private void ReleaseTables(IDbManager manager, List<string> tableNames)
  {
    if (tableNames == null || tableNames.Count <= 0)
      return;
    List<IDbDataParameter> pars = new List<IDbDataParameter>(tableNames.Count + 1);
    pars.Add(manager.Parameter(":parValue", (object) 1));
    string paramsRange = this.CreateParamsRange<string>(manager, tableNames, pars);
    string commandText = $"UPDATE IMS_IMBASE_INDEXES SET {IndexesField.F_ATTRIBUTE_STATE}=:parValue WHERE {IndexesField.F_TABLE_NAME} IN {paramsRange}";
    Monitor.Enter(ImbaseIndexingService._lock);
    manager.BeginTransaction();
    try
    {
      manager.ExecuteNonQuery(commandText, pars.ToArray());
      manager.Commit();
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw;
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private List<int> RecreateTableData(UserSession session, ImbaseIndexingService.Task task)
  {
    List<int> intList = new List<int>();
    IDbManager dataManager = session.DataManager;
    string commandText = $"UPDATE IMS_IMBASE_INDEXES SET {IndexesField.F_ATTRIBUTE_STATE}=:parState WHERE {IndexesField.F_TABLE_NAME}=:parTblName";
    IDbDataParameter dbDataParameter = dataManager.Parameter(":parTblName", (object) string.Empty);
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>()
    {
      dataManager.Parameter(":parState", (object) 0),
      dbDataParameter
    };
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      foreach (int attribute in task.Attributes)
      {
        string tableName;
        dbDataParameter.Value = (object) (tableName = ImbaseIndexingService.GenerateTableName(task.CatalogId, attribute));
        dataManager.BeginTransaction();
        try
        {
          dataManager.ExecuteNonQuery("DROP TABLE " + tableName);
          switch (dataManager.DataProvider.Name)
          {
            case "Sql":
              dataManager.ExecuteNonQuery(string.Format(this.CreateIdataSql, (object) tableName));
              break;
            case "Oracle":
              dataManager.ExecuteNonQuery(string.Format(this.CreateIdataOra, (object) tableName));
              break;
            case "PostgreSQL":
              dataManager.ExecuteNonQuery(string.Format(this.CreateIdataPostgre, (object) tableName));
              break;
          }
          dataManager.ExecuteNonQuery(commandText, dbDataParameterList.ToArray());
          dataManager.Commit();
          intList.Add(attribute);
        }
        catch
        {
          dataManager.Rollback();
          string msg = $"Не удалось обновить таблицу с данными для атрибута '{MetaDataHelper.GetAttributeTypeName(attribute)}' (ID={attribute})";
          task.Exceptions.Add(new IndexingException(msg)
          {
            ComputerName = task.ComputerName,
            TaskName = task.TaskName
          });
        }
      }
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
    return intList.Count <= 0 ? (List<int>) null : intList;
  }

  private void SetTaskCompleted(
    ImbaseIndexingService.Task task,
    ImbaseIndexingService.TaskState state)
  {
    task.SetState(state);
    this.Tasks.Remove(task);
    if (this.CompletedTasks.Contains(task))
      return;
    this.CompletedTasks.Add(task);
  }

  private void RemoveAfterComplete(ImbaseIndexingService.Task task)
  {
    this.Tasks.Remove(task);
    this.CompletedTasks.Remove(task);
  }

  public int GetCompleted(Guid taskGuid, out int nState, out string text)
  {
    int completed = 0;
    nState = 0;
    text = string.Empty;
    ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (t => t.TaskGuid == taskGuid));
    if (task != null)
    {
      completed = task.Completed;
      nState = task.Running ? 1 : 0;
      text = task.Caption;
    }
    return completed;
  }

  public List<Exception> GetResult(Guid taskGuid)
  {
    List<Exception> exceptionList = (List<Exception>) null;
    ImbaseIndexingService.Task task = this.CompletedTasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (t => t.TaskGuid == taskGuid));
    if (task != null)
    {
      exceptionList = task.Exceptions.Cast<Exception>().ToList<Exception>();
      this.RemoveAfterComplete(task);
    }
    return exceptionList == null || exceptionList.Count != 0 ? exceptionList : (List<Exception>) null;
  }

  public void RemoveAfterComplete(Guid taskGuid)
  {
    ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (t => t.TaskGuid == taskGuid));
    if (task == null)
      return;
    task.RemoveAfterComplete = true;
  }

  public void StopTask(Guid taskGuid)
  {
    this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (t => t.TaskGuid == taskGuid))?.SetState(ImbaseIndexingService.TaskState.Terminated);
  }

  public void Add(Guid sessionGuid, Guid taskGuid, long catalogId, Dictionary<int, bool> attrs)
  {
    if (catalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    if (attrs == null || attrs.Count == 0)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.AttributeListEmpty);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, catalogId)
    {
      Attributes = attrs.Keys.ToList<int>(),
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexing_CreateIndexes")
    });
    new Action<Guid, Guid, Dictionary<int, bool>>(this.Add).BeginInvoke(sessionGuid, taskGuid, attrs, (AsyncCallback) null, (object) null);
  }

  private void Add(Guid sessionGuid, Guid taskGuid, Dictionary<int, bool> attrs)
  {
    ImbaseIndexingService.Task currentTask = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    UserSession session = (UserSession) null;
    List<Tuple<long, string>> tupleList = new List<Tuple<long, string>>();
    Dictionary<int, string> dictionary1 = (Dictionary<int, string>) null;
    try
    {
      session = this.GetUserSession(sessionGuid, "ImbaseIndexing.Add", true);
      if (currentTask == null)
        return;
      currentTask.ComputerName = session.ComputerName;
      currentTask.Attributes = this.CheckAttributes(session, currentTask, currentTask.Attributes);
      if (currentTask.Attributes.Count > 0)
      {
        Dictionary<int, bool> dictionary2 = attrs.Where<KeyValuePair<int, bool>>((System.Func<KeyValuePair<int, bool>, bool>) (attr => currentTask.Attributes.Contains(attr.Key))).ToDictionary<KeyValuePair<int, bool>, int, bool>((System.Func<KeyValuePair<int, bool>, int>) (p => p.Key), (System.Func<KeyValuePair<int, bool>, bool>) (k => k.Value));
        foreach (KeyValuePair<int, bool> keyValuePair in dictionary2)
          tupleList.Add(new Tuple<long, string>(session.EventLogHelper.AddEvent(currentTask.CatalogId, 0L, 2, (long) Intermech.Imbase.Consts.ImbaseCatalogTypeID, string.Format(LocalizationHolder.rm.GetString("Imbase_Attribute_Index"), (object) MetaDataHelper.GetAttributeTypeName(keyValuePair.Key)), string.Empty, ActionType.Create, EventlogRecordType.Information, session.UserID, session.ComputerName, (IUserSession) session), keyValuePair.Value ? LocalizationHolder.rm.GetString("Imbase_Uniqueness_Enabled") : LocalizationHolder.rm.GetString("Imbase_Uniqueness_Disabled")));
        dictionary1 = this.Add(session, currentTask, dictionary2);
      }
      this.SetTaskCompleted(currentTask, ImbaseIndexingService.TaskState.Terminated);
      tupleList.ForEach((Action<Tuple<long, string>>) (x => session.EventLogHelper.CloseEvent(x.Item1, EventlogRecordType.Information, x.Item2, (IUserSession) session)));
    }
    catch (IndexingException ex)
    {
      if (currentTask == null)
        return;
      currentTask.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(currentTask, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      if (dictionary1 != null && session != null)
        this.ReleaseTables(session.DataManager, dictionary1.Values.ToList<string>());
      session?.Logout("ImbaseIndexing.Add");
      if (currentTask != null && currentTask.RemoveAfterComplete)
        this.RemoveAfterComplete(currentTask);
    }
  }

  private Dictionary<int, string> Add(
    UserSession session,
    ImbaseIndexingService.Task task,
    Dictionary<int, bool> attrIDs)
  {
    long catalogId = task.CatalogId;
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, catalogId);
    if (string.IsNullOrEmpty(classifKeyByObjId))
      throw new IndexingException($"{ImbaseIndexingService.ExceptionsMessages.CatalogClassifKeyEmpty}-'{session.GetObjectInfo(catalogId).Caption}' (ID={catalogId})");
    Dictionary<int, string> attrs = this.AddIndexes(session.DataManager, task, attrIDs);
    if (attrs != null)
    {
      task.Caption = LocalizationHolder.rm.GetString("Imbase_Indexing_AddData");
      task.ClearValue();
      DataTable tableRefIds = this.GetTableRefIDs(session, classifKeyByObjId, true);
      if (tableRefIds != null)
      {
        task.TableRefInfoList = tableRefIds.AsEnumerable().Select<DataRow, ImbaseIndexingService.TableRefInfo>((System.Func<DataRow, ImbaseIndexingService.TableRefInfo>) (x => new ImbaseIndexingService.TableRefInfo()
        {
          CatalogId = catalogId,
          TableId = Convert.ToInt64(x[IndexesField.F_TABLE_ID]),
          TableRefId = Convert.ToInt64(x[IndexesField.F_LINK_ID])
        })).ToList<ImbaseIndexingService.TableRefInfo>();
        task.CountItems = task.TableRefInfoList.Count;
        foreach (ImbaseIndexingService.TableRefInfo tableRefInfo in task.TableRefInfoList)
        {
          if (!task.Terminated)
          {
            this.AddIndexesData(session, task, tableRefInfo);
            ++task.CurrItemNumber;
          }
          else
            break;
        }
        if (task.Running)
          this.CreateIndexesForTableData(session, task, attrs);
      }
    }
    return attrs;
  }

  private Dictionary<int, string> AddIndexes(
    IDbManager manager,
    ImbaseIndexingService.Task task,
    Dictionary<int, bool> attrs)
  {
    Dictionary<int, string> dictionary1 = new Dictionary<int, string>(attrs.Count);
    if (!task.Terminated)
    {
      Monitor.Enter(ImbaseIndexingService._lock);
      task.Caption = task.TaskName;
      task.ClearValue();
      task.CountItems = attrs.Count;
      Dictionary<int, string> dictionary2 = attrs.ToDictionary<KeyValuePair<int, bool>, int, string>((System.Func<KeyValuePair<int, bool>, int>) (k => k.Key), (System.Func<KeyValuePair<int, bool>, string>) (v => ImbaseIndexingService.GenerateTableName(task.CatalogId, v.Key)));
      try
      {
        List<IDbDataParameter> pars = new List<IDbDataParameter>();
        string paramsRange = this.CreateParamsRange<string>(manager, dictionary2.Values.ToList<string>(), pars);
        DataTable source = manager.ExecuteDataTable($"SELECT {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_ATTRIBUTE_STATE} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_TABLE_NAME} IN {paramsRange}", pars.ToArray());
        Dictionary<int, int> dictionary3 = (source != null ? source.AsEnumerable().ToDictionary<DataRow, int, int>((System.Func<DataRow, int>) (k => Convert.ToInt32(k[IndexesField.F_ATTRIBUTE_ID])), (System.Func<DataRow, int>) (v => Convert.ToInt32(v[IndexesField.F_ATTRIBUTE_STATE]))) : (Dictionary<int, int>) null) ?? new Dictionary<int, int>();
        int num = 0;
        foreach (KeyValuePair<int, bool> attr in attrs)
        {
          if (!task.Terminated)
          {
            string tableName = dictionary2[attr.Key];
            if (dictionary3.ContainsKey(attr.Key))
            {
              if (dictionary3[attr.Key] == num)
              {
                task.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeTypeName(attr.Key), (object) attr.Key)));
                ++task.CurrItemNumber;
                continue;
              }
              this.RemoveTable(manager, tableName);
            }
            this.CreateTable(manager, task.CatalogId, attr.Key, attr.Value, tableName);
            dictionary1.Add(attr.Key, tableName);
            ++task.CurrItemNumber;
          }
          else
            break;
        }
      }
      catch (Exception ex)
      {
        throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
      }
      finally
      {
        Monitor.Exit(ImbaseIndexingService._lock);
      }
    }
    return dictionary1.Count <= 0 ? (Dictionary<int, string>) null : dictionary1;
  }

  private void AddIndexesData(
    UserSession session,
    ImbaseIndexingService.Task task,
    ImbaseIndexingService.TableRefInfo tblRefInfo)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      long num1 = Math.Abs(tblRefInfo.TableRefId);
      long num2 = Math.Abs(tblRefInfo.TableId);
      try
      {
        DBObject dbObject = (DBObject) session.GetObject(num1, false);
        if (dbObject == null || dbObject.LevelID == session.IdentHelper.DeletedID)
          throw new Exception();
        if (tblRefInfo.TableRefId < 0L)
        {
          IDBAttribute attributeById1 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
          if (attributeById1 != null)
          {
            string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, task.CatalogId);
            if (attributeById1.AsString.StartsWith(classifKeyByObjId))
            {
              IDBAttribute attributeById2 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
              num2 = attributeById2 != null ? Math.Abs(attributeById2.AsInteger) : 0L;
            }
          }
        }
        else if (num2 == 0L)
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          num2 = attributeById != null ? Math.Abs(attributeById.AsInteger) : 0L;
        }
      }
      catch (Exception ex)
      {
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefNull, (object) num1), ex);
      }
      DataTable dtAttrs;
      DataTable dtData;
      this.GetTables(session, num2, out dtAttrs, out dtData);
      if (dtAttrs == null)
        return;
      this.AssignAttributes(session, num1, num2, dtAttrs, dtData);
      if (session.DataManager.DataProvider.Name == "Sql")
        this.AddIndexesDataForSql(session, task, tblRefInfo.TableRefId, tblRefInfo.TableId, dtData, (Action) null, false);
      else if (session.DataManager.DataProvider.Name == "Oracle")
      {
        this.AddIndexesDataForOracle(session, task, tblRefInfo.TableRefId, tblRefInfo.TableId, dtData, (Action) null, false);
      }
      else
      {
        if (!(session.DataManager.DataProvider.Name == "PostgreSQL"))
          return;
        this.AddIndexesDataForOther(session, task, tblRefInfo.TableRefId, tblRefInfo.TableId, dtData, (Action) null, false);
      }
    }
    catch (IndexingException ex)
    {
      task.Exceptions.Add(ex);
    }
    catch (Exception ex)
    {
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private void CreateIndexesForTableData(
    UserSession session,
    ImbaseIndexingService.Task task,
    Dictionary<int, string> attrs)
  {
    task.Caption = LocalizationHolder.rm.GetString("Imbase_Indexing_TableData_CreateIndexes");
    task.ClearValue();
    task.CountItems = attrs.Count;
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      string format1 = string.Empty;
      string format2 = "CREATE INDEX {0}_HT ON {0} (F_HASHTEXT)" + session.DataManager.DataProvider.IndexTablespaceNameSQL;
      if (session.DataManager.DataProvider.Name == "Sql")
        format1 = "ALTER TABLE {0} ADD PRIMARY KEY CLUSTERED (F_LINK_ID, F_TABKEY)";
      else if (session.DataManager.DataProvider.Name == "Oracle")
        format1 = "ALTER TABLE {0} ADD (PRIMARY KEY (F_LINK_ID, F_TABKEY))";
      else if (session.DataManager.DataProvider.Name == "PostgreSQL")
        format1 = "ALTER TABLE {0} ADD PRIMARY KEY (F_LINK_ID, F_TABKEY)";
      session.DataManager.BeginTransaction();
      try
      {
        foreach (KeyValuePair<int, string> attr in attrs)
        {
          session.DataManager.ExecuteNonQuery(string.Format(format1, (object) attr.Value));
          session.DataManager.ExecuteNonQuery(string.Format(format2, (object) attr.Value));
          ++task.CurrItemNumber;
        }
        session.DataManager.Commit();
      }
      catch (Exception ex)
      {
        session.DataManager.Rollback();
        throw;
      }
    }
    catch (Exception ex)
    {
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  public void Remove(Guid sessionGuid, Guid taskGuid, long catalogId, List<int> attrIDs)
  {
    if (catalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    if (attrIDs == null || attrIDs.Count == 0)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.AttributeListEmpty);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, catalogId)
    {
      Attributes = attrIDs,
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexes_Delete")
    });
    new Action<Guid, Guid>(this.Remove).BeginInvoke(sessionGuid, taskGuid, (AsyncCallback) null, (object) null);
  }

  private void Remove(Guid sessionGuid, Guid taskGuid)
  {
    ImbaseIndexingService.Task task1 = this.Tasks.Find((Predicate<ImbaseIndexingService.Task>) (task => task.TaskGuid == taskGuid));
    UserSession userSession = (UserSession) null;
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      userSession = this.GetUserSession(sessionGuid, "ImbaseIndexing.Remove", true);
      task1.ComputerName = userSession.ComputerName;
      task1.Attributes = this.CheckAttributes(userSession, task1, task1.Attributes);
      if (task1.Attributes.Count > 0)
      {
        task1.Caption = task1.TaskName;
        task1.CountItems = task1.Attributes.Count;
        foreach (int attribute in task1.Attributes)
        {
          if (!task1.Terminated)
          {
            long EventID = userSession.EventLogHelper.AddEvent(task1.CatalogId, 0L, 2, (long) Intermech.Imbase.Consts.ImbaseCatalogTypeID, string.Format(LocalizationHolder.rm.GetString("Imbase_Attribute_Index"), (object) MetaDataHelper.GetAttributeTypeName(attribute)), string.Empty, ActionType.Delete, EventlogRecordType.Information, userSession.UserID, userSession.ComputerName, (IUserSession) userSession);
            this.RemoveTable(userSession.DataManager, ImbaseIndexingService.GenerateTableName(task1.CatalogId, attribute));
            userSession.EventLogHelper.CloseEvent(EventID, EventlogRecordType.Information, string.Empty, (IUserSession) userSession);
            ++task1.CurrItemNumber;
          }
          else
            break;
        }
      }
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      task1.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    catch (Exception ex)
    {
      task1.Exceptions = new List<IndexingException>()
      {
        new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex)
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      userSession?.Logout("ImbaseIndexing.Remove");
      if (task1.RemoveAfterComplete)
        this.RemoveAfterComplete(task1);
    }
  }

  public void Update(Guid sessionGuid, Guid taskGuid, long catalogId)
  {
    if (catalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, catalogId)
    {
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexing_UpdateIndexes")
    });
    new Action<Guid, Guid>(this.Update).BeginInvoke(sessionGuid, taskGuid, (AsyncCallback) null, (object) null);
  }

  private void Update(Guid sessionGuid, Guid taskGuid)
  {
    ImbaseIndexingService.Task task1 = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    if (task1 == null)
      return;
    task1.Caption = task1.TaskName;
    long catalogId = task1.CatalogId;
    UserSession session = (UserSession) null;
    Dictionary<int, string> attrs = (Dictionary<int, string>) null;
    try
    {
      session = this.GetUserSession(sessionGuid, "ImbaseIndexing.Update", true);
      task1.ComputerName = session.ComputerName;
      string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, catalogId);
      if (string.IsNullOrEmpty(classifKeyByObjId))
        throw new IndexingException($"{ImbaseIndexingService.ExceptionsMessages.CatalogClassifKeyEmpty}-'{session.GetObjectInfo(catalogId).Caption}' (ID={catalogId})");
      string commandText = $"SELECT {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_FLAG}, {IndexesField.F_ATTRIBUTE_STATE} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_CATALOG_ID}=:c_ID";
      DataTable dataTable = session.DataManager.ExecuteDataTable(commandText, session.DataManager.Parameter(":c_ID", (object) catalogId));
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        Dictionary<int, bool> dictionary = new Dictionary<int, bool>(dataTable.Rows.Count);
        List<ImbaseIndexingService.Task> list = this.Tasks.Where<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x =>
        {
          if (x.CatalogId == 0L)
            return true;
          return x.CatalogId == catalogId && x.TaskGuid != taskGuid;
        })).ToList<ImbaseIndexingService.Task>();
        Monitor.Enter(ImbaseIndexingService._lock);
        try
        {
          int num = 0;
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            int id = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
            IDBAttributeType attributeType = session.GetAttributeType(id);
            if (attributeType != null)
            {
              ImbaseIndexingService.Task task2 = list.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.Attributes.Contains(id)));
              if (task2 != null)
                task1.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) attributeType.Name, (object) id))
                {
                  ComputerName = task2.ComputerName,
                  TaskName = task2.TaskName
                });
              else if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == num)
              {
                task1.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) attributeType.Name, (object) id)));
              }
              else
              {
                bool flag = false;
                int int32 = Convert.ToInt32(row[IndexesField.F_FLAG]);
                if (int32 != -1 && ((IndexesFlags) int32).HasFlag((Enum) IndexesFlags.UniqueValue))
                  flag = true;
                dictionary.Add(id, flag);
              }
            }
            else
              this.RemoveTable(session.DataManager, ImbaseIndexingService.GenerateTableName(catalogId, id));
          }
          task1.Attributes = dictionary.Keys.ToList<int>();
        }
        catch (Exception ex)
        {
          task1.SetState(ImbaseIndexingService.TaskState.Error);
          throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
        }
        finally
        {
          Monitor.Exit(ImbaseIndexingService._lock);
        }
        if (dictionary.Count > 0)
        {
          task1.Attributes = this.RecreateTableData(session, task1);
          if (task1.Attributes != null)
          {
            attrs = task1.Attributes.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(catalogId, v)));
            DataTable tableRefIds = this.GetTableRefIDs(session, classifKeyByObjId);
            task1.TableRefInfoList = tableRefIds.AsEnumerable().Select<DataRow, ImbaseIndexingService.TableRefInfo>((System.Func<DataRow, ImbaseIndexingService.TableRefInfo>) (x => new ImbaseIndexingService.TableRefInfo()
            {
              CatalogId = catalogId,
              TableId = Convert.ToInt64(x[IndexesField.F_TABLE_ID]),
              TableRefId = Convert.ToInt64(x[IndexesField.F_LINK_ID])
            })).ToList<ImbaseIndexingService.TableRefInfo>();
            task1.Caption = LocalizationHolder.rm.GetString("Imbase_Indexing_AddData");
            task1.ClearValue();
            task1.CountItems = task1.TableRefInfoList.Count;
            foreach (ImbaseIndexingService.TableRefInfo tableRefInfo in task1.TableRefInfoList)
            {
              if (!task1.Terminated)
              {
                this.AddIndexesData(session, task1, tableRefInfo);
                ++task1.CurrItemNumber;
              }
              else
                break;
            }
            if (task1.Running)
              this.CreateIndexesForTableData(session, task1, attrs);
          }
        }
      }
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      task1.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      if (attrs != null)
        this.ReleaseTables(session.DataManager, attrs.Values.ToList<string>());
      session.Logout("ImbaseIndexing.Update");
      if (task1.RemoveAfterComplete)
        this.RemoveAfterComplete(task1);
    }
  }

  public void UpdateFlags(
    Guid sessionGuid,
    Guid taskGuid,
    long catalogId,
    Dictionary<int, bool> attrs)
  {
    if (catalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    if (attrs == null || attrs.Count == 0)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.AttributeListEmpty);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, catalogId)
    {
      Attributes = attrs.Keys.ToList<int>(),
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexes_UpdateUnique")
    });
    new Action<Guid, Guid, Dictionary<int, bool>>(this.UpdateFlags).BeginInvoke(sessionGuid, taskGuid, attrs, (AsyncCallback) null, (object) null);
  }

  private void UpdateFlags(Guid sessionGuid, Guid taskGuid, Dictionary<int, bool> attrs)
  {
    ImbaseIndexingService.Task task1 = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    UserSession userSession = (UserSession) null;
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      userSession = this.GetUserSession(sessionGuid, "ImbaseIndexing.UpdateFlags", true);
      if (task1 == null)
        return;
      task1.ComputerName = userSession.ComputerName;
      task1.Attributes = this.CheckAttributes(userSession, task1, task1.Attributes);
      if (task1.Attributes.Count > 0)
      {
        task1.Caption = task1.TaskName;
        task1.CountItems = task1.Attributes.Count;
        string commandText = $"UPDATE IMS_IMBASE_INDEXES SET {IndexesField.F_FLAG}=:val WHERE {IndexesField.F_TABLE_NAME}=:parTblName";
        IDbDataParameter dbDataParameter1 = userSession.DataManager.Parameter(":val", (object) this.GetFlagValue(false));
        IDbDataParameter dbDataParameter2 = userSession.DataManager.Parameter(":parTblName", (object) string.Empty);
        userSession.StartTransaction();
        try
        {
          foreach (int attribute in task1.Attributes)
          {
            if (!task1.Terminated)
            {
              long EventID = userSession.EventLogHelper.AddEvent(task1.CatalogId, 0L, 2, (long) Intermech.Imbase.Consts.ImbaseCatalogTypeID, string.Format(LocalizationHolder.rm.GetString("Imbase_Attribute_Index"), (object) MetaDataHelper.GetAttributeTypeName(attribute)), string.Empty, ActionType.EditProperties, EventlogRecordType.Information, userSession.UserID, userSession.ComputerName, (IUserSession) userSession);
              dbDataParameter1.Value = (object) this.GetFlagValue(attrs[attribute]);
              dbDataParameter2.Value = (object) ImbaseIndexingService.GenerateTableName(task1.CatalogId, attribute);
              userSession.DataManager.ExecuteNonQuery(commandText, dbDataParameter1, dbDataParameter2);
              userSession.EventLogHelper.CloseEvent(EventID, EventlogRecordType.Information, attrs[attribute] ? LocalizationHolder.rm.GetString("Imbase_Uniqueness_Enabled") : LocalizationHolder.rm.GetString("Imbase_Uniqueness_Disabled"), (IUserSession) userSession);
              ++task1.CurrItemNumber;
            }
            else
              break;
          }
        }
        catch (Exception ex)
        {
          task1.SetState(ImbaseIndexingService.TaskState.Error);
          throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
        }
        finally
        {
          if (task1.Running)
            userSession.Commit();
          else
            userSession.Rollback();
        }
      }
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      if (task1 == null)
        return;
      task1.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      userSession?.Logout("ImbaseIndexing.UpdateFlags");
      if (task1 != null && task1.RemoveAfterComplete)
        this.RemoveAfterComplete(task1);
    }
  }

  private void AddIndexesDataForSql(
    UserSession session,
    ImbaseIndexingService.Task task,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Action indexAddedCallback,
    bool needDelete = true)
  {
    DataTable dtIndexData = new DataTable();
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_LINK_ID, typeof (long)));
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TABLE_ID, typeof (long)));
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TABKEY, typeof (long)));
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TEXT, typeof (string)));
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_HASHTEXT, typeof (string)));
    dtIndexData.Columns.Add(new DataColumn(IndexesField.F_APPLICABILITY, typeof (long)));
    IDbDataParameter dbDataParameter = session.DataManager.Parameter(":pTableRefID", (object) tableRefId);
    int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
    foreach (int attribute in task.Attributes)
    {
      if (task.Terminated)
        break;
      string str = attribute == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? "F_GUID" : Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attribute));
      if (dtData.Columns.Contains(str))
      {
        foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
        {
          string str_to_index = Convert.ToString(row[str]).MaxStringLength();
          if (!string.IsNullOrEmpty(str_to_index))
            dtIndexData.Rows.Add((object) tableRefId, (object) tableId, (object) Convert.ToInt64(row["F_KEY"]), (object) str_to_index, (object) session.StringNormalizer.GetIndexedString(str_to_index), (object) this.DisabledRecord(row, applicabilityColumnIndex));
        }
      }
      if (dtIndexData.Rows.Count > 0 | needDelete)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(task.CatalogId, attribute);
        session.StartTransaction();
        try
        {
          if (needDelete)
            session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:pTableRefID", dbDataParameter);
          if (dtIndexData.Rows.Count > 0)
          {
            IDbManager dataManager = session.DataManager;
            IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[2];
            SqlParameter sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@importTableName";
            sqlParameter1.SqlDbType = SqlDbType.NVarChar;
            sqlParameter1.Value = (object) tableName;
            dbDataParameterArray[0] = (IDbDataParameter) sqlParameter1;
            SqlParameter sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@importTable";
            sqlParameter2.TypeName = "IMS_IDATA_STRUCT";
            sqlParameter2.SqlDbType = SqlDbType.Structured;
            sqlParameter2.Value = (object) new ImbaseIndexingService.ImsTmpIdataStreamingDataRecord(dtIndexData);
            dbDataParameterArray[1] = (IDbDataParameter) sqlParameter2;
            dataManager.ExecuteSpNonQuery("IMS_IDATA_TVP", dbDataParameterArray);
          }
          session.Commit();
          dtIndexData.Rows.Clear();
        }
        catch (Exception ex)
        {
          session.Rollback();
          task.SetState(ImbaseIndexingService.TaskState.Error);
          throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
        }
      }
      if (indexAddedCallback != null)
        indexAddedCallback();
    }
  }

  private void AddIndexesDataForSql(
    UserSession session,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Dictionary<int, string> attrs)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      DataTable dtIndexData = new DataTable();
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_LINK_ID, typeof (long)));
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TABLE_ID, typeof (long)));
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TABKEY, typeof (long)));
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_TEXT, typeof (string)));
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_HASHTEXT, typeof (string)));
      dtIndexData.Columns.Add(new DataColumn(IndexesField.F_APPLICABILITY, typeof (long)));
      int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
      foreach (KeyValuePair<int, string> attr in attrs)
      {
        string str = attr.Key == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? "F_GUID" : Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attr.Key));
        if (dtData.Columns.Contains(str))
        {
          foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
          {
            string str_to_index = Convert.ToString(row[str]).MaxStringLength();
            if (!string.IsNullOrEmpty(str_to_index))
              dtIndexData.Rows.Add((object) tableRefId, (object) tableId, (object) Convert.ToInt64(row["F_KEY"]), (object) str_to_index, (object) session.StringNormalizer.GetIndexedString(str_to_index), (object) this.DisabledRecord(row, applicabilityColumnIndex));
          }
          if (dtIndexData.Rows.Count != 0)
          {
            session.StartTransaction();
            try
            {
              IDbManager dataManager = session.DataManager;
              IDbDataParameter[] dbDataParameterArray = new IDbDataParameter[2];
              SqlParameter sqlParameter1 = new SqlParameter();
              sqlParameter1.ParameterName = "@importTableName";
              sqlParameter1.SqlDbType = SqlDbType.NVarChar;
              sqlParameter1.Value = (object) attr.Value;
              dbDataParameterArray[0] = (IDbDataParameter) sqlParameter1;
              SqlParameter sqlParameter2 = new SqlParameter();
              sqlParameter2.ParameterName = "@importTable";
              sqlParameter2.TypeName = "IMS_IDATA_STRUCT";
              sqlParameter2.SqlDbType = SqlDbType.Structured;
              sqlParameter2.Value = (object) new ImbaseIndexingService.ImsTmpIdataStreamingDataRecord(dtIndexData);
              dbDataParameterArray[1] = (IDbDataParameter) sqlParameter2;
              dataManager.ExecuteSpNonQuery("IMS_IDATA_TVP", dbDataParameterArray);
              session.Commit();
              dtIndexData.Rows.Clear();
            }
            catch (Exception ex)
            {
              session.Rollback();
              throw;
            }
          }
        }
      }
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private void AddIndexesDataForOracle(
    UserSession session,
    ImbaseIndexingService.Task task,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Action indexAddedCallback,
    bool needDelete = true)
  {
    try
    {
      int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
      foreach (int attribute in task.Attributes)
      {
        if (task.Terminated)
          break;
        string tableName = ImbaseIndexingService.GenerateTableName(task.CatalogId, attribute);
        if (needDelete)
        {
          session.StartTransaction();
          try
          {
            IDbDataParameter dbDataParameter = session.DataManager.Parameter(":pTableRefID", (object) tableRefId);
            session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:pTableRefID", dbDataParameter);
            session.Commit();
          }
          catch (Exception ex)
          {
            session.Rollback();
            throw;
          }
        }
        string str = attribute == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? "F_GUID" : Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attribute));
        if (dtData.Columns.Contains(str))
        {
          List<long> tableRecNums = new List<long>();
          List<string> textList = new List<string>();
          List<string> hashTextList = new List<string>();
          List<long> applicabilityList = new List<long>();
          foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
          {
            string str_to_index = Convert.ToString(row[str]).MaxStringLength();
            if (!string.IsNullOrEmpty(str_to_index))
            {
              tableRecNums.Add(Convert.ToInt64(row["F_KEY"]));
              textList.Add(str_to_index);
              hashTextList.Add(session.StringNormalizer.GetIndexedString(str_to_index));
              applicabilityList.Add((long) this.DisabledRecord(row, applicabilityColumnIndex));
            }
          }
          if (textList.Count > 0)
            this.ExecuteBatchSql(session, tableName, tableRefId, tableId, tableRecNums, textList, hashTextList, applicabilityList);
        }
        if (indexAddedCallback != null)
          indexAddedCallback();
      }
    }
    catch (Exception ex)
    {
      task.SetState(ImbaseIndexingService.TaskState.Error);
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
  }

  private void AddIndexesDataForOracle(
    UserSession session,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Dictionary<int, string> attrs)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
      int count = dtData.Rows.Count;
      foreach (KeyValuePair<int, string> attr in attrs)
      {
        string str = attr.Key == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? "F_GUID" : Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attr.Key));
        if (dtData.Columns.Contains(str))
        {
          List<long> tableRecNums = new List<long>(count);
          List<string> textList = new List<string>(count);
          List<string> hashTextList = new List<string>(count);
          List<long> applicabilityList = new List<long>();
          foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
          {
            string str_to_index = Convert.ToString(row[str]).MaxStringLength();
            if (!string.IsNullOrEmpty(str_to_index))
            {
              tableRecNums.Add(Convert.ToInt64(row["F_KEY"]));
              textList.Add(str_to_index);
              hashTextList.Add(session.StringNormalizer.GetIndexedString(str_to_index));
              applicabilityList.Add((long) this.DisabledRecord(row, applicabilityColumnIndex));
            }
          }
          if (textList.Count != 0)
            this.ExecuteBatchSql(session, attr.Value, tableRefId, tableId, tableRecNums, textList, hashTextList, applicabilityList);
        }
      }
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private void ExecuteBatchSql(
    UserSession session,
    string tableName,
    long tableRefId,
    long tableId,
    List<long> tableRecNums,
    List<string> textList,
    List<string> hashTextList,
    List<long> applicabilityList)
  {
    IDbManager dataManager = session.DataManager;
    string str = $"({IndexesField.F_LINK_ID}, {IndexesField.F_TABLE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT}, {IndexesField.F_APPLICABILITY})";
    string commandText = $"INSERT INTO {tableName} {str} VALUES (:pTableRefID, :pTableID, :pTabKey, :pText, :pHashText, :pApplicability)";
    DbCommandParam dbCommandParam1 = dataManager.BatchParameter("pTableRefID", DbType.Int64, (object) tableRefId);
    DbCommandParam dbCommandParam2 = dataManager.BatchParameter("pTableID", DbType.Int64, (object) tableId);
    for (int index = 0; index < textList.Count; ++index)
    {
      DbCommandParam dbCommandParam3 = dataManager.BatchParameter("pTabKey", DbType.Int64, (object) tableRecNums[index]);
      DbCommandParam dbCommandParam4 = dataManager.BatchParameter("pText", DbType.String, (object) textList[index]);
      DbCommandParam dbCommandParam5 = dataManager.BatchParameter("pHashText", DbType.String, (object) hashTextList[index]);
      DbCommandParam dbCommandParam6 = dataManager.BatchParameter("pApplicability", DbType.Int64, (object) applicabilityList[index]);
      dataManager.AddBatchSQL(commandText, new DbCommandParam[6]
      {
        dbCommandParam1,
        dbCommandParam2,
        dbCommandParam3,
        dbCommandParam4,
        dbCommandParam5,
        dbCommandParam6
      });
    }
    session.StartTransaction();
    try
    {
      dataManager.ExecuteBatchSQL();
      session.Commit();
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
  }

  private void AddIndexesDataForOther(
    UserSession session,
    ImbaseIndexingService.Task task,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Action indexAddedCallback,
    bool needDelete = true)
  {
    string str1 = $"({IndexesField.F_LINK_ID}, {IndexesField.F_TABLE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT}, {IndexesField.F_APPLICABILITY})";
    IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) tableRefId);
    IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":t_ID", (object) tableId);
    IDbDataParameter dbDataParameter3 = session.DataManager.Parameter(":tabKey", (object) -1);
    IDbDataParameter dbDataParameter4 = session.DataManager.Parameter(":textValue", (object) string.Empty);
    IDbDataParameter dbDataParameter5 = session.DataManager.Parameter(":hashText", (object) string.Empty);
    IDbDataParameter dbDataParameter6 = session.DataManager.Parameter(":applicability", (object) 0);
    int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
    foreach (int attribute in task.Attributes)
    {
      if (task.Terminated)
        break;
      string str2 = attribute != Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attribute)) : "F_GUID";
      if (dtData.Columns.Contains(str2) | needDelete)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(task.CatalogId, attribute);
        session.StartTransaction();
        try
        {
          if (needDelete)
            session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:l_ID", dbDataParameter1);
          if (dtData.Columns.Contains(str2))
          {
            foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
            {
              string str_to_index = Convert.ToString(row[str2]).MaxStringLength();
              if (!string.IsNullOrEmpty(str_to_index))
              {
                dbDataParameter3.Value = row["F_KEY"];
                dbDataParameter4.Value = (object) str_to_index;
                dbDataParameter5.Value = (object) session.StringNormalizer.GetIndexedString(str_to_index);
                dbDataParameter6.Value = (object) this.DisabledRecord(row, applicabilityColumnIndex);
                session.DataManager.ExecuteNonQuery($"INSERT INTO {tableName} {str1} VALUES (:l_ID, :t_ID, :tabKey, :textValue, :hashText, :applicability)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5, dbDataParameter6);
              }
            }
          }
          if (task.Running)
            session.Commit();
          else
            session.Rollback();
        }
        catch (Exception ex)
        {
          session.Rollback();
          task.SetState(ImbaseIndexingService.TaskState.Error);
          throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
        }
      }
      if (indexAddedCallback != null)
        indexAddedCallback();
    }
  }

  private void AddIndexesDataForOther(
    UserSession session,
    long tableRefId,
    long tableId,
    DataTable dtData,
    Dictionary<int, string> attrs)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      string str1 = $"({IndexesField.F_LINK_ID}, {IndexesField.F_TABLE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT})";
      IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) tableRefId);
      IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":t_ID", (object) tableId);
      IDbDataParameter dbDataParameter3 = session.DataManager.Parameter(":tabKey", (object) -1);
      IDbDataParameter dbDataParameter4 = session.DataManager.Parameter(":textValue", (object) string.Empty);
      IDbDataParameter dbDataParameter5 = session.DataManager.Parameter(":hashText", (object) string.Empty);
      IDbDataParameter dbDataParameter6 = session.DataManager.Parameter(":applicability", (object) 0);
      int applicabilityColumnIndex = dtData.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttGUID.ToString());
      foreach (KeyValuePair<int, string> attr in attrs)
      {
        string str2 = attr.Key != Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID ? Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attr.Key)) : "F_GUID";
        if (dtData.Columns.Contains(str2))
        {
          session.StartTransaction();
          try
          {
            foreach (DataRow row in (InternalDataCollectionBase) dtData.Rows)
            {
              string str_to_index = Convert.ToString(row[str2]).MaxStringLength();
              if (!string.IsNullOrEmpty(str_to_index))
              {
                dbDataParameter3.Value = row["F_KEY"];
                dbDataParameter4.Value = (object) str_to_index;
                dbDataParameter5.Value = (object) session.StringNormalizer.GetIndexedString(str_to_index);
                dbDataParameter6.Value = (object) this.DisabledRecord(row, applicabilityColumnIndex);
                session.DataManager.ExecuteNonQuery($"INSERT INTO {attr.Value} {str1} VALUES (:l_ID, :t_ID, :tabKey, :textValue, :hashText, :applicability)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbDataParameter4, dbDataParameter5, dbDataParameter6);
              }
            }
            session.Commit();
          }
          catch (Exception ex)
          {
            session.Rollback();
          }
        }
      }
    }
    catch (Exception ex)
    {
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  public void CheckUniqueBeforeRegistryInImbase(
    Guid sessionGuid,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    List<long> rowNums)
  {
    if (tableId == 0L || dtAttrs == null || dtAttrs.Rows.Count <= 0 || dtData == null || dtData.Rows.Count <= 0 || rowNums == null || rowNums.Count <= 0)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    Dictionary<long, List<long>> idsGroupByCatalog = this.GetTableRefIDsGroupByCatalog((IUserSession) userSession, tableId);
    if (idsGroupByCatalog == null)
      return;
    List<long> list1 = idsGroupByCatalog.Keys.ToList<long>();
    this.CheckBase(userSession, sessionGuid, list1);
    Dictionary<int, string> attrData = (Dictionary<int, string>) null;
    Dictionary<long, List<int>> registryInImbase = this.GetIndexesForRegistryInImbase(userSession.DataManager, list1, dtAttrs, true, ref attrData);
    if (registryInImbase == null)
      return;
    foreach (long key in registryInImbase.Keys)
    {
      if (idsGroupByCatalog[key].Count > 1)
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) userSession.GetObjectInfo(key).Caption, (object) key, (object) tableId));
    }
    long minRowNum = rowNums.Min();
    minRowNum--;
    foreach (KeyValuePair<long, List<int>> keyValuePair in registryInImbase)
    {
      long tableRefId = idsGroupByCatalog[keyValuePair.Key][0];
      DataTable dataTable = dtData.Copy();
      this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dataTable);
      EnumerableRowCollection<DataRow> source = dataTable.AsEnumerable();
      List<IDbDataParameter> pars = new List<IDbDataParameter>()
      {
        userSession.DataManager.Parameter(":l_ID", (object) tableRefId)
      };
      foreach (int num in keyValuePair.Value)
      {
        if (num != Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID)
        {
          string str = attrData[num];
          string guid = str;
          string attrGuid = str;
          List<string> list2 = source.Where<DataRow>((System.Func<DataRow, bool>) (x => !string.IsNullOrEmpty(Convert.ToString(x[guid])))).Select<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[attrGuid]).MaxStringLength())).ToList<string>();
          if (list2.Count != 0)
          {
            if (list2.Count != list2.Distinct<string>().Count<string>())
              throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueValue"), (object) MetaDataHelper.GetAttributeTypeName(num), (object) num, (object) userSession.GetObjectInfo(keyValuePair.Key).Caption, (object) keyValuePair.Key));
            string tableName = ImbaseIndexingService.GenerateTableName(keyValuePair.Key, num);
            string guid1 = str;
            string attrGuid1 = str;
            List<string> list3 = source.Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_KEY"]) > minRowNum && !string.IsNullOrEmpty(Convert.ToString(x[guid1])))).Select<DataRow, string>((System.Func<DataRow, string>) (x => Convert.ToString(x[attrGuid1]))).ToList<string>();
            if (list3.Count != 0)
            {
              string paramsRange = this.CreateParamsRange<string>(userSession.DataManager, list3, pars);
              if (Convert.ToInt32(userSession.DataManager.ExecuteScalar($"SELECT COUNT(*) FROM {tableName} WHERE {IndexesField.F_LINK_ID}<>:l_ID AND {IndexesField.F_TEXT} IN {paramsRange}", pars.ToArray())) > 0)
                throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_NotUniqueValue"), (object) MetaDataHelper.GetAttributeTypeName(num), (object) num, (object) userSession.GetObjectInfo(keyValuePair.Key).Caption, (object) keyValuePair.Key));
            }
          }
        }
      }
    }
  }

  public void UpdateAfterRegisteredInImbase(
    Guid sessionGuid,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    List<long> rowNums)
  {
    if (tableId == 0L || dtAttrs == null || dtAttrs.Rows.Count <= 0 || dtData == null || dtData.Rows.Count <= 0 || rowNums == null || rowNums.Count <= 0)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = "Обновление данных после регистрации в IMBASE"
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      Dictionary<long, List<long>> idsGroupByCatalog = this.GetTableRefIDsGroupByCatalog((IUserSession) userSession, tableId);
      if (idsGroupByCatalog != null)
      {
        List<long> list = idsGroupByCatalog.Keys.ToList<long>();
        this.CheckBase(userSession, sessionGuid, list);
        Dictionary<int, string> attrData = (Dictionary<int, string>) null;
        Dictionary<long, List<int>> registryInImbase = this.GetIndexesForRegistryInImbase(userSession.DataManager, list, dtAttrs, false, ref attrData);
        if (registryInImbase != null)
        {
          foreach (KeyValuePair<long, List<int>> keyValuePair in registryInImbase)
          {
            task.CatalogId = keyValuePair.Key;
            task.Attributes = keyValuePair.Value;
            foreach (long tableRefId in idsGroupByCatalog[keyValuePair.Key])
            {
              DataTable dataTable1 = dtData.Copy();
              this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dataTable1);
              DataTable dataTable2 = dataTable1.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => rowNums.Contains(Convert.ToInt64(x["F_KEY"])))).CopyToDataTable<DataRow>();
              if (userSession.DataManager.DataProvider.Name == "Sql")
                this.AddIndexesDataForSql(userSession, task, tableRefId, tableId, dataTable2, (Action) null, false);
              else if (userSession.DataManager.DataProvider.Name == "Oracle")
                this.AddIndexesDataForOracle(userSession, task, tableRefId, tableId, dataTable2, (Action) null, false);
              else if (userSession.DataManager.DataProvider.Name == "PostgreSQL")
                this.AddIndexesDataForOther(userSession, task, tableRefId, tableId, dataTable2, (Action) null, false);
            }
          }
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private Dictionary<long, List<long>> GetTableRefIDsGroupByCatalog(
    IUserSession session,
    long tableId)
  {
    Dictionary<long, List<long>> idsGroupByCatalog = (Dictionary<long, List<long>>) null;
    DataTable tableRefIdsByTableId = TableLoadHelper.GetTableRefIDsByTableID(session, tableId);
    if (tableRefIdsByTableId != null)
    {
      Dictionary<string, List<long>> catalogKeysTableRefIDs = new Dictionary<string, List<long>>(tableRefIdsByTableId.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) tableRefIdsByTableId.Rows)
      {
        string str = Convert.ToString(row["F_KEY"]);
        if (str.Length >= 2)
        {
          string key = str.Substring(0, 2);
          if (catalogKeysTableRefIDs.ContainsKey(key))
            catalogKeysTableRefIDs[key].Add(Convert.ToInt64(row["F_LINK_ID"]));
          else
            catalogKeysTableRefIDs.Add(key, new List<long>()
            {
              Convert.ToInt64(row["F_LINK_ID"])
            });
        }
      }
      if (catalogKeysTableRefIDs.Count > 0)
      {
        Dictionary<long, string> idsByClassifKeys = this.GetCatalogIDsByClassifKeys(session, catalogKeysTableRefIDs.Keys.ToList<string>());
        if (idsByClassifKeys != null)
          idsGroupByCatalog = idsByClassifKeys.ToDictionary<KeyValuePair<long, string>, long, List<long>>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, List<long>>) (v => catalogKeysTableRefIDs[v.Value]));
      }
    }
    return idsGroupByCatalog;
  }

  private Dictionary<long, List<int>> GetIndexesForRegistryInImbase(
    IDbManager manager,
    List<long> catalogIDs,
    DataTable dtAttrs,
    bool isUniqueIndexes,
    ref Dictionary<int, string> attrData)
  {
    Dictionary<long, List<int>> dictionary = (Dictionary<long, List<int>>) null;
    string[] colsNames = new string[4]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE,
      IndexesField.F_FLAG
    };
    DataTable indexes = this.GetIndexes(manager, catalogIDs, colsNames);
    if (indexes != null)
    {
      dictionary = new Dictionary<long, List<int>>(indexes.Rows.Count);
      attrData = new Dictionary<int, string>(indexes.Rows.Count);
      long catalogId = 0;
      List<ImbaseIndexingService.Task> taskList = (List<ImbaseIndexingService.Task>) null;
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        int attrId = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
        if (!attrData.ContainsKey(attrId))
          attrData.Add(attrId, Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId)));
        int int32 = Convert.ToInt32(row[IndexesField.F_FLAG]);
        bool flag = false;
        if (int32 != -1 && ((IndexesFlags) int32).HasFlag((Enum) IndexesFlags.UniqueValue))
          flag = true;
        if ((!isUniqueIndexes || flag) && (attrId == Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID || dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{attrData[attrId]}'").Length != 0))
        {
          long int64 = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
          if (catalogId != int64)
          {
            catalogId = int64;
            taskList = this.Tasks.Where<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.CatalogId == 0L || x.CatalogId == catalogId)).ToList<ImbaseIndexingService.Task>();
          }
          ImbaseIndexingService.Task task = (taskList ?? throw new InvalidOperationException()).FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.Attributes.Contains(attrId)));
          if (task != null)
          {
            string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
            throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
          }
          if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))
            throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId));
          if (!dictionary.ContainsKey(catalogId))
            dictionary.Add(catalogId, new List<int>()
            {
              attrId
            });
          else
            dictionary[catalogId].Add(attrId);
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, List<int>>) null : dictionary;
  }

  private Dictionary<long, string> GetCatalogIDsByClassifKeys(
    IUserSession session,
    List<string> keys)
  {
    Dictionary<long, string> idsByClassifKeys = (Dictionary<long, string>) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      throw new IndexingException(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Catalog_Error"));
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) keys.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    DataTable source = objectCollection.Select(paramSet);
    if (source != null && source.Rows.Count > 0)
      idsByClassifKeys = source.AsEnumerable().ToDictionary<DataRow, long, string>((System.Func<DataRow, long>) (k => Convert.ToInt64(k[0])), (System.Func<DataRow, string>) (v => Convert.ToString(v[1])));
    return idsByClassifKeys;
  }

  public void CheckUniqueBeforeTableRefAttrChange(Guid sessionGuid, long tableRefId, long tableId)
  {
    if (tableRefId == 0L || tableId == 0L)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    long catalogId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) userSession, tableRefId);
    if (catalogId == 0L)
      throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_UndefinedCatalogIDForTableRef"), (object) userSession.GetObjectInfo(tableRefId).Caption, (object) tableRefId));
    this.CheckBase(userSession, sessionGuid, new List<long>()
    {
      catalogId
    });
    List<int> indexes = this.GetUniqueIndexes(userSession.DataManager, catalogId);
    if (indexes == null)
      return;
    IDBObjectCollection objectCollection = userSession.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.TableRefListEmpty);
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) userSession, catalogId);
    ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tableId, LogicalOperators.AND, 0, false),
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]{ columnDescriptor });
    DataTable dataTable = objectCollection.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0 && (dataTable.Rows.Count > 1 || Convert.ToInt64(dataTable.Rows[0][0]) != tableRefId))
    {
      DataSet tablesInternal = TableLoadHelper.GetTablesInternal((IUserSession) userSession, tableId, false);
      DataTable source = tablesInternal != null && tablesInternal.Tables.Contains("IMS_ATTR_TYPES") ? tablesInternal.Tables["IMS_ATTR_TYPES"] : throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.ImbaseTableNull, (object) userSession.GetObjectInfo(tableId).Caption, (object) tableId));
      if (indexes.Contains(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID) || source.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => indexes.Contains(MetaDataHelper.GetAttributeTypeID(Convert.ToString(x["F_ATTRIBUTE_GUID"]))))) != null)
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) userSession.GetObjectInfo(catalogId).Caption, (object) catalogId, (object) tableId));
    }
    Dictionary<int, string> dictionary1 = indexes.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
    Dictionary<int, string> dictionary2 = indexes.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(catalogId, v)));
    this.CheckTableRef(userSession, tableRefId, tableId, dictionary1, dictionary2, catalogId: catalogId);
  }

  public void UpdateAfterTableRefAttrChanged(Guid sessionGuid, long tableRefId)
  {
    Guid taskGuid = Guid.NewGuid();
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid)
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_TableRef"), (object) tableRefId)
    });
    this.RemoveAfterComplete(taskGuid);
    this.UpdateTblRefData(sessionGuid, taskGuid, tableRefId, false);
  }

  public void CheckUniqueBeforeAttrInTableRefChange(
    Guid sessionGuid,
    long tableRefId,
    long tableId,
    int attrId,
    object value)
  {
    if (attrId == 0 || tableRefId == 0L)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    long catalogId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) userSession, tableRefId);
    if (catalogId == 0L)
      return;
    this.CheckBase(userSession, sessionGuid, new List<long>()
    {
      catalogId
    });
    List<int> uniqueIndexes = this.GetUniqueIndexes(userSession.DataManager, catalogId);
    if (uniqueIndexes == null)
      return;
    DataTable dtAttrs;
    this.GetTables(userSession, tableId, out dtAttrs, out DataTable _);
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
    string str1 = attributeType != null ? attributeType.Name : string.Empty;
    string sourceAttrGuid = Convert.ToString((object) (attributeType != null ? attributeType.AttributeGuid : MetaDataHelper.GetAttributeTypeGuid(attrId)));
    bool flag = false;
    if (dtAttrs != null)
    {
      DataRow[] dataRowArray = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{sourceAttrGuid}'");
      flag = dataRowArray.Length != 0 && Convert.ToInt32(dataRowArray[0]["F_COMPUTED"]) == 0 && Convert.ToInt32(dataRowArray[0]["F_REQUIRED"]) == 0;
    }
    if (uniqueIndexes.Contains(attrId))
    {
      if (flag)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ChangeAttribute_NotUniqueValue"), (object) str1, (object) attrId));
    }
    else
    {
      if (!flag)
        return;
      List<int> source = new List<int>(uniqueIndexes.Count);
      List<string> attrGuids = new List<string>();
      foreach (int attrTypeID in uniqueIndexes)
      {
        string str2 = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
        if (!attrGuids.Contains(str2))
        {
          attrGuids.Add(str2);
          DataRow[] dataRowArray = dtAttrs?.Select($"[F_ATTRIBUTE_GUID]='{str2}'");
          if (dataRowArray != null && dataRowArray.Length != 0 && Convert.ToInt32(dataRowArray[0]["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRowArray[0]["F_FORMULA"]), sourceAttrGuid, ref attrGuids))
            source.Add(attrTypeID);
        }
      }
      if (source.Count <= 0)
        return;
      Dictionary<int, string> dictionary1 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
      Dictionary<int, string> dictionary2 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(catalogId, v)));
      this.CheckTableRef(userSession, tableRefId, tableId, dictionary1, dictionary2, new Dictionary<int, object>()
      {
        {
          attrId,
          value
        }
      });
    }
  }

  public void CheckUniqueBeforeAttrInTableChange(
    Guid sessionGuid,
    long tableId,
    int attrId,
    object value)
  {
    if (attrId == 0 || tableId == 0L)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    Dictionary<long, string> orEmptyAttribute = this.GetTableRefIDsByTableIdWithNullOrEmptyAttribute(userSession, tableId, attrId);
    if (orEmptyAttribute == null)
      return;
    DataTable dtAttrs;
    this.GetTables(userSession, tableId, out dtAttrs, out DataTable _);
    string sourceAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
    DataRow[] dataRowArray1 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{sourceAttrGuid}'");
    if (dataRowArray1.Length == 0 || Convert.ToInt32(dataRowArray1[0]["F_COMPUTED"]) != 0 || Convert.ToInt32(dataRowArray1[0]["F_REQUIRED"]) != 0)
      return;
    Dictionary<long, List<long>> dictionary1 = (Dictionary<long, List<long>>) null;
    Dictionary<long, List<int>> source1 = (Dictionary<long, List<int>>) null;
    Dictionary<string, List<long>> catalogKeysTableRefIDs = new Dictionary<string, List<long>>(orEmptyAttribute.Count);
    foreach (KeyValuePair<long, string> keyValuePair in orEmptyAttribute)
    {
      string str = keyValuePair.Value;
      if (str.Length >= 2)
      {
        string key = str.Substring(0, 2);
        if (catalogKeysTableRefIDs.ContainsKey(key))
          catalogKeysTableRefIDs[key].Add(keyValuePair.Key);
        else
          catalogKeysTableRefIDs.Add(key, new List<long>()
          {
            keyValuePair.Key
          });
      }
    }
    if (catalogKeysTableRefIDs.Count > 0)
    {
      Dictionary<long, string> idsByClassifKeys = this.GetCatalogIDsByClassifKeys((IUserSession) userSession, catalogKeysTableRefIDs.Keys.ToList<string>());
      this.CheckBase(userSession, sessionGuid, idsByClassifKeys.Keys.ToList<long>());
      dictionary1 = idsByClassifKeys.ToDictionary<KeyValuePair<long, string>, long, List<long>>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, List<long>>) (v => catalogKeysTableRefIDs[v.Value]));
      Dictionary<int, string> attrData = (Dictionary<int, string>) null;
      source1 = this.GetUniqueIndexes(userSession.DataManager, dictionary1.Keys.ToList<long>(), dtAttrs, ref attrData);
    }
    if (source1 == null)
      return;
    foreach (long key in source1.Keys)
    {
      if (dictionary1[key].Count != 1)
      {
        QuickObjectInfo objectInfo = userSession.GetObjectInfo(key);
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) objectInfo.Caption, (object) key, (object) tableId));
      }
    }
    if (source1.Any<KeyValuePair<long, List<int>>>((System.Func<KeyValuePair<long, List<int>>, bool>) (x => x.Value.Contains(attrId))))
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
      string str = attributeType != null ? attributeType.Name : string.Empty;
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ChangeAttribute_NotUniqueValue"), (object) str, (object) attrId));
    }
    List<string> attrGuids = new List<string>();
    foreach (KeyValuePair<long, List<int>> keyValuePair in source1)
    {
      KeyValuePair<long, List<int>> pair = keyValuePair;
      List<int> source2 = new List<int>(pair.Value.Count);
      attrGuids.Clear();
      foreach (int attrTypeID in pair.Value)
      {
        string str = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
        if (!attrGuids.Contains(str))
        {
          attrGuids.Add(str);
          DataRow[] dataRowArray2 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{str}'");
          if (dataRowArray2.Length != 0 && Convert.ToInt32(dataRowArray2[0]["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRowArray2[0]["F_FORMULA"]), sourceAttrGuid, ref attrGuids))
            source2.Add(attrTypeID);
        }
      }
      if (source2.Count != 0)
      {
        Dictionary<int, string> dictionary2 = source2.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
        Dictionary<int, string> dictionary3 = source2.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(pair.Key, v)));
        this.CheckTableRef(userSession, dictionary1[pair.Key][0], tableId, dictionary2, dictionary3, new Dictionary<int, object>()
        {
          {
            attrId,
            value
          }
        });
      }
    }
  }

  public void CheckUniqueBeforeAttrInTableRefDelete(
    Guid sessionGuid,
    long tableRefId,
    long tableId,
    int attrId)
  {
    if (attrId == 0 || tableRefId == 0L || tableId == 0L)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    long catalogId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) userSession, tableRefId);
    if (catalogId == 0L)
    {
      QuickObjectInfo objectInfo = userSession.GetObjectInfo(tableRefId);
      throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_UndefinedCatalogIDForTableRef"), (object) objectInfo.Caption, (object) tableRefId));
    }
    this.CheckBase(userSession, sessionGuid, new List<long>()
    {
      catalogId
    });
    DataTable dtAttrs;
    this.GetTables(userSession, tableId, out dtAttrs, out DataTable _);
    string sourceAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
    DataRow[] dataRowArray1 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{sourceAttrGuid}'");
    if (dataRowArray1.Length == 0 || Convert.ToInt32(dataRowArray1[0]["F_COMPUTED"]) != 0 || Convert.ToInt32(dataRowArray1[0]["F_REQUIRED"]) != 0)
      return;
    List<int> uniqueIndexes = this.GetUniqueIndexes(userSession.DataManager, catalogId);
    if (uniqueIndexes == null)
      return;
    if (uniqueIndexes.Contains(attrId))
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrId);
      string str = attributeType != null ? attributeType.Name : string.Empty;
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ChangeAttribute_NotUniqueValue"), (object) str, (object) attrId));
    }
    List<int> source = new List<int>(uniqueIndexes.Count);
    List<string> attrGuids = new List<string>();
    foreach (int attrTypeID in uniqueIndexes)
    {
      string str = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
      if (!attrGuids.Contains(str))
      {
        attrGuids.Add(str);
        DataRow[] dataRowArray2 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{str}'");
        if (dataRowArray2.Length != 0 && Convert.ToInt32(dataRowArray2[0]["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRowArray2[0]["F_FORMULA"]), sourceAttrGuid, ref attrGuids))
          source.Add(attrTypeID);
      }
    }
    if (source.Count <= 0)
      return;
    IDBObject dbObject = userSession.GetObject(tableId, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(attrId);
    Dictionary<int, string> dictionary1 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
    Dictionary<int, string> dictionary2 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(catalogId, v)));
    this.CheckTableRef(userSession, tableRefId, tableId, dictionary1, dictionary2, new Dictionary<int, object>()
    {
      {
        attrId,
        attributeById?.Value
      }
    });
  }

  public void CheckUniqueBeforeAttrInTableDelete(Guid sessionGuid, long tableId, int attrId)
  {
    if (attrId == 0 || tableId == 0L)
      return;
    UserSession userSession = this.GetUserSession(sessionGuid);
    DataTable dtAttrs;
    this.GetTables(userSession, tableId, out dtAttrs, out DataTable _);
    string sourceAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
    DataRow[] dataRowArray1 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{sourceAttrGuid}'");
    if (dataRowArray1.Length == 0 || Convert.ToInt32(dataRowArray1[0]["F_COMPUTED"]) != 0 || Convert.ToInt32(dataRowArray1[0]["F_REQUIRED"]) != 0)
      return;
    Dictionary<long, string> orEmptyAttribute = this.GetTableRefIDsByTableIdWithNullOrEmptyAttribute(userSession, tableId, attrId);
    if (orEmptyAttribute == null)
      return;
    Dictionary<long, List<long>> dictionary1 = (Dictionary<long, List<long>>) null;
    Dictionary<long, List<int>> dictionary2 = (Dictionary<long, List<int>>) null;
    Dictionary<string, List<long>> catalogKeysTableRefIDs = new Dictionary<string, List<long>>(orEmptyAttribute.Count);
    foreach (KeyValuePair<long, string> keyValuePair in orEmptyAttribute)
    {
      if (keyValuePair.Value.Length >= 2)
      {
        string key = keyValuePair.Value.Substring(0, 2);
        if (catalogKeysTableRefIDs.ContainsKey(key))
          catalogKeysTableRefIDs[key].Add(keyValuePair.Key);
        else
          catalogKeysTableRefIDs.Add(key, new List<long>()
          {
            keyValuePair.Key
          });
      }
    }
    if (catalogKeysTableRefIDs.Count > 0)
    {
      Dictionary<long, string> idsByClassifKeys = this.GetCatalogIDsByClassifKeys((IUserSession) userSession, catalogKeysTableRefIDs.Keys.ToList<string>());
      this.CheckBase(userSession, sessionGuid, idsByClassifKeys.Keys.ToList<long>());
      dictionary1 = idsByClassifKeys.ToDictionary<KeyValuePair<long, string>, long, List<long>>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, List<long>>) (v => catalogKeysTableRefIDs[v.Value]));
      Dictionary<int, string> attrData = (Dictionary<int, string>) null;
      dictionary2 = this.GetUniqueIndexes(userSession.DataManager, dictionary1.Keys.ToList<long>(), dtAttrs, ref attrData);
    }
    if (dictionary2 == null)
      return;
    foreach (long key in dictionary2.Keys)
    {
      if (dictionary1[key].Count != 1)
      {
        QuickObjectInfo objectInfo = userSession.GetObjectInfo(key);
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) objectInfo.Caption, (object) key, (object) tableId));
      }
    }
    List<string> attrGuids = new List<string>();
    foreach (KeyValuePair<long, List<int>> keyValuePair in dictionary2)
    {
      KeyValuePair<long, List<int>> pair = keyValuePair;
      List<int> source = new List<int>(pair.Value.Count);
      attrGuids.Clear();
      foreach (int attrTypeID in pair.Value)
      {
        string str = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
        if (!attrGuids.Contains(str))
        {
          attrGuids.Add(str);
          DataRow[] dataRowArray2 = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{str}'");
          if (dataRowArray2.Length != 0 && Convert.ToInt32(dataRowArray2[0]["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRowArray2[0]["F_FORMULA"]), sourceAttrGuid, ref attrGuids))
            source.Add(attrTypeID);
        }
      }
      if (source.Count != 0)
      {
        Dictionary<int, string> dictionary3 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
        Dictionary<int, string> dictionary4 = source.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(pair.Key, v)));
        this.CheckTableRef(userSession, dictionary1[pair.Key][0], tableId, dictionary3, dictionary4, new Dictionary<int, object>()
        {
          {
            attrId,
            (object) null
          }
        }, ignoreTableAttr: true);
      }
    }
  }

  public void UpdateAfterAttrInTableRefChanged(
    Guid sessionGuid,
    long tableRefId,
    long tableId,
    int attrId,
    object value)
  {
    if (attrId == 0 || tableRefId == 0L)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_TableRef"), (object) tableRefId)
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) userSession, tableRefId);
      if (catalogIdByObjectId != 0L)
      {
        task.CatalogId = catalogIdByObjectId;
        this.CheckBase(userSession, sessionGuid, new List<long>()
        {
          catalogIdByObjectId
        });
        task.Attributes = this.GetIndexes(userSession.DataManager, task, catalogIdByObjectId);
        if (task.Attributes != null)
        {
          DataTable dtAttrs;
          DataTable dtData;
          this.GetTables(userSession, tableId, out dtAttrs, out dtData);
          if (dtAttrs != null)
          {
            string strSourceAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
            DataRow dataRow1 = dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strSourceAttrGuid));
            if (dataRow1 != null && Convert.ToInt32(dataRow1["F_COMPUTED"]) == 0 && Convert.ToInt32(dataRow1["F_REQUIRED"]) == 0)
            {
              List<int> intList = new List<int>(task.Attributes.Count);
              List<string> attrGuids = new List<string>();
              foreach (int attribute in task.Attributes)
              {
                if (attribute == attrId)
                {
                  intList.Add(attribute);
                }
                else
                {
                  string strAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attribute));
                  if (!attrGuids.Contains(strAttrGuid))
                  {
                    attrGuids.Add(strAttrGuid);
                    DataRow dataRow2 = dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strAttrGuid));
                    if (dataRow2 != null && Convert.ToInt32(dataRow2["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRow2["F_FORMULA"]), strSourceAttrGuid, ref attrGuids))
                      intList.Add(attribute);
                  }
                }
              }
              task.Attributes = intList;
              if (task.Attributes.Count > 0)
              {
                this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dtData);
                if (userSession.DataManager.DataProvider.Name == "Sql")
                  this.AddIndexesDataForSql(userSession, task, tableRefId, tableId, dtData, (Action) null);
                else if (userSession.DataManager.DataProvider.Name == "Oracle")
                  this.AddIndexesDataForOracle(userSession, task, tableRefId, tableId, dtData, (Action) null);
                else if (userSession.DataManager.DataProvider.Name == "PostgreSQL")
                  this.AddIndexesDataForOther(userSession, task, tableRefId, tableId, dtData, (Action) null);
              }
            }
          }
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  public void UpdateAfterAttrInTableChanged(Guid sessionGuid, long tableId, int attrId)
  {
    if (attrId == 0 || tableId == 0L)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_Table"), (object) tableId)
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      DataTable dtAttrs;
      DataTable dtData1;
      this.GetTables(userSession, tableId, out dtAttrs, out dtData1);
      string strSourceAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
      DataRow dataRow1 = dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strSourceAttrGuid));
      if (dataRow1 != null && Convert.ToInt32(dataRow1["F_COMPUTED"]) == 0 && Convert.ToInt32(dataRow1["F_REQUIRED"]) == 0)
      {
        Dictionary<long, string> orEmptyAttribute = this.GetTableRefIDsByTableIdWithNullOrEmptyAttribute(userSession, tableId, attrId);
        if (orEmptyAttribute != null)
        {
          Dictionary<long, List<long>> dictionary1 = (Dictionary<long, List<long>>) null;
          Dictionary<long, List<int>> dictionary2 = (Dictionary<long, List<int>>) null;
          Dictionary<string, List<long>> catalogKeysTableRefIDs = new Dictionary<string, List<long>>(orEmptyAttribute.Count);
          foreach (KeyValuePair<long, string> keyValuePair in orEmptyAttribute)
          {
            string str = keyValuePair.Value;
            if (str.Length >= 2)
            {
              string key = str.Substring(0, 2);
              if (catalogKeysTableRefIDs.ContainsKey(key))
                catalogKeysTableRefIDs[key].Add(keyValuePair.Key);
              else
                catalogKeysTableRefIDs.Add(key, new List<long>()
                {
                  keyValuePair.Key
                });
            }
          }
          if (catalogKeysTableRefIDs.Count > 0)
          {
            Dictionary<long, string> idsByClassifKeys = this.GetCatalogIDsByClassifKeys((IUserSession) userSession, catalogKeysTableRefIDs.Keys.ToList<string>());
            this.CheckBase(userSession, sessionGuid, idsByClassifKeys.Keys.ToList<long>());
            dictionary1 = idsByClassifKeys.ToDictionary<KeyValuePair<long, string>, long, List<long>>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, List<long>>) (v => catalogKeysTableRefIDs[v.Value]));
            Dictionary<int, string> attrData = (Dictionary<int, string>) null;
            dictionary2 = this.GetIndexes(userSession.DataManager, dictionary1.Keys.ToList<long>(), dtAttrs, ref attrData);
          }
          if (dictionary2 != null)
          {
            List<string> attrGuids = new List<string>();
            Action<UserSession, ImbaseIndexingService.Task, long, long, DataTable, Action, bool> action = (Action<UserSession, ImbaseIndexingService.Task, long, long, DataTable, Action, bool>) null;
            if (userSession.DataManager.DataProvider.Name == "Sql")
              action = new Action<UserSession, ImbaseIndexingService.Task, long, long, DataTable, Action, bool>(this.AddIndexesDataForSql);
            else if (userSession.DataManager.DataProvider.Name == "Oracle")
            {
              action = new Action<UserSession, ImbaseIndexingService.Task, long, long, DataTable, Action, bool>(this.AddIndexesDataForOracle);
            }
            else
            {
              int num = userSession.DataManager.DataProvider.Name == "PostgreSQL" ? 1 : 0;
            }
            foreach (KeyValuePair<long, List<int>> keyValuePair in dictionary2)
            {
              task.CatalogId = keyValuePair.Key;
              task.Attributes = keyValuePair.Value;
              List<int> intList = new List<int>(keyValuePair.Value.Count);
              attrGuids.Clear();
              foreach (int attrTypeID in keyValuePair.Value)
              {
                if (attrTypeID == attrId)
                {
                  intList.Add(attrTypeID);
                }
                else
                {
                  string strAttrGuid = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrTypeID));
                  if (!attrGuids.Contains(strAttrGuid))
                  {
                    attrGuids.Add(strAttrGuid);
                    DataRow dataRow2 = dtAttrs.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strAttrGuid));
                    if (dataRow2 != null && Convert.ToInt32(dataRow2["F_COMPUTED"]) == 2 && this.CheckEntry(dtAttrs, Convert.ToString(dataRow2["F_FORMULA"]), strSourceAttrGuid, ref attrGuids))
                      intList.Add(attrTypeID);
                  }
                }
              }
              if (intList.Count != 0)
              {
                foreach (long tableRefId in dictionary1[keyValuePair.Key])
                {
                  DataTable dtData2 = dtData1.Copy();
                  this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dtData2);
                  if (action != null)
                    action(userSession, task, tableRefId, tableId, dtData2, (Action) null, true);
                }
              }
            }
          }
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private bool CheckEntry(
    DataTable dtAttrs,
    string formula,
    string sourceAttrGuid,
    ref List<string> attrGuids)
  {
    bool flag = false;
    if (!string.IsNullOrEmpty(formula))
    {
      List<string> formula1 = this.ParseFormula(formula);
      flag = formula1.FirstOrDefault<string>((System.Func<string, bool>) (x => x == sourceAttrGuid)) != null;
      if (!flag)
      {
        foreach (string str in formula1)
        {
          if (!attrGuids.Contains(str))
          {
            attrGuids.Add(str);
            DataRow[] dataRowArray = dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{str}'");
            if (dataRowArray.Length != 0)
            {
              RequiredModes int32 = (RequiredModes) Convert.ToInt32(dataRowArray[0]["F_REQUIRED"]);
              if (Convert.ToInt32(dataRowArray[0]["F_COMPUTED"]) == 2 || int32 == RequiredModes.Manual)
              {
                formula = Convert.ToString(dataRowArray[0]["F_FORMULA"]);
                if (!this.CheckEntry(dtAttrs, formula, sourceAttrGuid, ref attrGuids))
                  break;
              }
            }
          }
        }
      }
    }
    return flag;
  }

  private List<string> ParseFormula(string formula)
  {
    List<string> formula1 = (List<string>) null;
    if (!string.IsNullOrEmpty(formula))
    {
      List<string> stringList = new List<string>((IEnumerable<string>) formula.Split('['));
      if (stringList.Count > 0)
      {
        formula1 = new List<string>(stringList.Count);
        foreach (string str in stringList)
        {
          if (!string.IsNullOrEmpty(str))
          {
            int length = str.IndexOf(']');
            if (length != -1)
            {
              string text = str.Substring(0, length);
              if (GuidHelper.IsGuid(text))
                formula1.Add(text);
            }
          }
        }
      }
    }
    return formula1;
  }

  private Dictionary<long, string> GetTableRefIDsByTableIdWithNullOrEmptyAttribute(
    UserSession session,
    long tableId,
    int attrId)
  {
    Dictionary<long, string> dictionary1 = (Dictionary<long, string>) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection != null)
    {
      ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
      ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.ASC, 0);
      ConditionStructure conditionStructure1 = new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.Equal, (object) tableId, LogicalOperators.AND, 0, false);
      ConditionStructure conditionStructure2 = new ConditionStructure(attrId, RelationalOperators.AttributeExists, (object) null, LogicalOperators.NONE, 0, false);
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure1
      }, new ColumnDescriptor[2]
      {
        columnDescriptor1,
        columnDescriptor2
      });
      DataTable source1 = objectCollection.Select(paramSet);
      if (source1 != null && source1.Rows.Count > 0)
      {
        Dictionary<long, string> dictionary2 = source1.AsEnumerable().ToDictionary<DataRow, long, string>((System.Func<DataRow, long>) (k => Convert.ToInt64(k[0])), (System.Func<DataRow, string>) (v => Convert.ToString(v[1])));
        paramSet = new DBRecordSetParams(new ConditionStructure[2]
        {
          conditionStructure1,
          conditionStructure2
        }, new ColumnDescriptor[2]
        {
          columnDescriptor1,
          columnDescriptor2
        });
        DataTable source2 = objectCollection.Select(paramSet);
        if (source2 != null && source2.Rows.Count > 0)
        {
          Dictionary<long, string> dictionary3 = source2.AsEnumerable().ToDictionary<DataRow, long, string>((System.Func<DataRow, long>) (k => Convert.ToInt64(k[0])), (System.Func<DataRow, string>) (v => Convert.ToString(v[1])));
          dictionary1 = dictionary2.Except<KeyValuePair<long, string>>((IEnumerable<KeyValuePair<long, string>>) dictionary3).ToDictionary<KeyValuePair<long, string>, long, string>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, string>) (v => v.Value));
        }
        else
          dictionary1 = dictionary2;
      }
    }
    return dictionary1 == null || dictionary1.Count <= 0 ? (Dictionary<long, string>) null : dictionary1;
  }

  private Dictionary<long, List<int>> GetUniqueIndexes(
    IDbManager manager,
    List<long> catalogIDs,
    DataTable dtAttrs,
    ref Dictionary<int, string> attrData)
  {
    Dictionary<long, List<int>> dictionary = (Dictionary<long, List<int>>) null;
    string[] colsNames = new string[4]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE,
      IndexesField.F_FLAG
    };
    DataTable uniqueIndexes = this.GetUniqueIndexes(manager, catalogIDs, colsNames);
    if (uniqueIndexes != null)
    {
      dictionary = new Dictionary<long, List<int>>(uniqueIndexes.Rows.Count);
      attrData = new Dictionary<int, string>(uniqueIndexes.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) uniqueIndexes.Rows)
      {
        int attrId = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
        if (!attrData.ContainsKey(attrId))
          attrData.Add(attrId, Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId)));
        if (dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{attrData[attrId]}'").Length != 0)
        {
          long catalogId = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
          ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
          if (task != null)
          {
            string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
            throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
          }
          if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))
            throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId));
          if (!dictionary.ContainsKey(catalogId))
            dictionary.Add(catalogId, new List<int>()
            {
              attrId
            });
          else
            dictionary[catalogId].Add(attrId);
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, List<int>>) null : dictionary;
  }

  private Dictionary<long, List<int>> GetIndexes(
    IDbManager manager,
    List<long> catalogIDs,
    DataTable dtAttrs,
    ref Dictionary<int, string> attrData)
  {
    Dictionary<long, List<int>> dictionary = (Dictionary<long, List<int>>) null;
    string[] colsNames = new string[3]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE
    };
    DataTable indexes = this.GetIndexes(manager, catalogIDs, colsNames);
    if (indexes != null)
    {
      dictionary = new Dictionary<long, List<int>>(indexes.Rows.Count);
      attrData = new Dictionary<int, string>(indexes.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        int attrId = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
        if (!attrData.ContainsKey(attrId))
          attrData.Add(attrId, Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId)));
        if (dtAttrs.Select($"[F_ATTRIBUTE_GUID]='{attrData[attrId]}'").Length != 0)
        {
          long catalogId = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
          ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
          if (task != null)
          {
            string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
            throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
          }
          if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))
            throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId));
          if (!dictionary.ContainsKey(catalogId))
            dictionary.Add(catalogId, new List<int>()
            {
              attrId
            });
          else
            dictionary[catalogId].Add(attrId);
        }
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, List<int>>) null : dictionary;
  }

  public List<int> CheckUniqueBeforeTableRefCreate(
    Guid sessionGuid,
    long catalogId,
    Dictionary<int, object> values)
  {
    List<int> intList = new List<int>(values.Count);
    if (catalogId != 0L && values.Count > 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      IDbManager dataManager = userSession.DataManager;
      this.CheckBase(userSession, sessionGuid, new List<long>()
      {
        catalogId
      });
      foreach (KeyValuePair<int, object> keyValuePair in values)
      {
        KeyValuePair<int, object> pair = keyValuePair;
        if (this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.CatalogId == catalogId && x.Attributes.Contains(pair.Key))) != null)
          throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(pair.Key).Name, (object) pair.Key));
        string str = Convert.ToString(pair.Value).MaxStringLength();
        if (!string.IsNullOrEmpty(str))
        {
          string tableName = ImbaseIndexingService.GenerateTableName(catalogId, pair.Key);
          if (Convert.ToInt32(dataManager.ExecuteScalar($"SELECT COUNT(*) FROM {tableName} WHERE {IndexesField.F_TEXT}=:parText", dataManager.Parameter(":parText", (object) str))) != 0)
            intList.Add(pair.Key);
        }
      }
    }
    return intList.Count <= 0 ? (List<int>) null : intList;
  }

  public List<int> CheckUniqueBeforeTableRefCreate(
    Guid sessionGuid,
    long catalogId,
    Dictionary<int, object> values,
    DataTable dtData,
    out List<int> notUniqueColumns)
  {
    List<int> intList = this.CheckUniqueBeforeTableRefCreate(sessionGuid, catalogId, values);
    notUniqueColumns = new List<int>();
    if (catalogId != 0L && dtData != null && dtData.Rows.Count > 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      this.CheckBase(userSession, sessionGuid, new List<long>()
      {
        catalogId
      });
      int maximumInOperands = userSession.DataManager.DataProvider.MaximumINOperands;
      List<string> stringList = new List<string>(dtData.Rows.Count);
      List<string> values1 = new List<string>();
      foreach (DataColumn column in (InternalDataCollectionBase) dtData.Columns)
      {
        int attrId = MetaDataHelper.GetAttributeTypeID(new Guid(column.ColumnName));
        ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.CatalogId == catalogId && x.Attributes.Contains(attrId)));
        if (task != null)
        {
          string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
        }
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, attrId);
        stringList.Clear();
        int num1 = 0;
        while (num1 < dtData.Rows.Count)
        {
          List<IDbDataParameter> pars = new List<IDbDataParameter>();
          values1.Clear();
          int num2 = dtData.Rows.Count - num1 > maximumInOperands ? maximumInOperands : dtData.Rows.Count - num1;
          for (int index = 0; index < num2; ++index)
          {
            string str = Convert.ToString(dtData.Rows[num1++][column.ColumnName]).MaxStringLength();
            if (!string.IsNullOrEmpty(str))
            {
              if (stringList.Contains(str))
              {
                if (!notUniqueColumns.Contains(attrId))
                {
                  notUniqueColumns.Add(attrId);
                  break;
                }
                break;
              }
              stringList.Add(str);
              values1.Add(str);
            }
          }
          if (values1.Count != 0)
          {
            string paramsRange = this.CreateParamsRange<string>(userSession.DataManager, values1, pars);
            if (Convert.ToInt32(userSession.DataManager.ExecuteScalar($"SELECT COUNT(*) FROM {tableName} WHERE {IndexesField.F_TEXT} IN {paramsRange}", pars.ToArray())) != 0)
            {
              if (!notUniqueColumns.Contains(attrId))
              {
                notUniqueColumns.Add(attrId);
                break;
              }
              break;
            }
          }
        }
      }
    }
    return intList;
  }

  public void UpdateAfterTableRefCreated(
    Guid sessionGuid,
    Guid taskGuid,
    long tableRefId,
    bool isNewObj)
  {
    if (tableRefId == 0L)
      throw new IndexingException(LocalizationHolder.rm.GetString("Imbase_UndefinedTableRefID"));
    string str = LocalizationHolder.rm.GetString("Imbase_Indexing_TableRefIndexing");
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid)
    {
      Caption = str,
      TaskName = str
    });
    new System.Action<Guid, Guid, long, bool>(this.UpdateTblRefData).BeginInvoke(sessionGuid, taskGuid, tableRefId, isNewObj, (AsyncCallback) null, (object) null);
  }

  private void UpdateTblRefData(Guid sessionGuid, Guid taskGuid, long tableRefId, bool isNewObj)
  {
    if (tableRefId == 0L)
      return;
    ImbaseIndexingService.Task currentTask = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    UserSession session = (UserSession) null;
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      session = this.GetUserSession(sessionGuid, "ImbaseIndexing.UpdateTblRefData", true);
      if (currentTask == null)
        return;
      currentTask.ComputerName = session.ComputerName;
      try
      {
        if (session.GetObject(tableRefId, false) is DBObject dbObject)
        {
          if (dbObject.LevelID != session.IdentHelper.DeletedID)
            goto label_10;
        }
        throw new Exception();
      }
      catch (Exception ex)
      {
        throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRefObject_Null_Msg"), (object) tableRefId), ex);
      }
label_10:
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
      long asInteger = attributeById != null ? attributeById.AsInteger : 0L;
      long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) session, tableRefId);
      this.CheckBase(session, sessionGuid, new List<long>()
      {
        catalogIdByObjectId
      });
      List<int> indexes = this.GetIndexes(session.DataManager, currentTask, catalogIdByObjectId);
      DataTable dtData = (DataTable) null;
      if (indexes != null)
      {
        currentTask.Attributes = indexes;
        currentTask.CatalogId = catalogIdByObjectId;
        if (asInteger != 0L)
        {
          DataTable dtAttrs;
          this.GetTables(session, asInteger, out dtAttrs, out dtData);
          this.AssignAttributes(session, tableRefId, asInteger, dtAttrs, dtData);
        }
      }
      if (dtData != null)
      {
        currentTask.CountItems = currentTask.Attributes.Count;
        if (session.DataManager.DataProvider.Name == "Sql")
          this.AddIndexesDataForSql(session, currentTask, tableRefId, asInteger, dtData, (Action) (() => ++currentTask.CurrItemNumber), !isNewObj);
        else if (session.DataManager.DataProvider.Name == "Oracle")
          this.AddIndexesDataForOracle(session, currentTask, tableRefId, asInteger, dtData, (Action) (() => ++currentTask.CurrItemNumber), !isNewObj);
        else if (session.DataManager.DataProvider.Name == "PostgreSQL")
          this.AddIndexesDataForOther(session, currentTask, tableRefId, asInteger, dtData, (Action) (() => ++currentTask.CurrItemNumber), !isNewObj);
      }
      else
      {
        IDbDataParameter dbDataParameter = session.DataManager.Parameter(":l_ID", (object) tableRefId);
        foreach (int attribute in currentTask.Attributes)
        {
          if (!currentTask.Terminated)
          {
            string tableName = ImbaseIndexingService.GenerateTableName(catalogIdByObjectId, attribute);
            session.StartTransaction();
            try
            {
              session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:l_ID", dbDataParameter);
              session.Commit();
            }
            catch (Exception ex)
            {
              session.Rollback();
              currentTask.SetState(ImbaseIndexingService.TaskState.Error);
              throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
            }
          }
          else
            break;
        }
      }
      this.SetTaskCompleted(currentTask, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      if (currentTask == null)
        return;
      currentTask.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(currentTask, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      session?.Logout("ImbaseIndexing.UpdateTblRefData");
      if (currentTask != null && currentTask.RemoveAfterComplete)
        this.RemoveAfterComplete(currentTask);
    }
  }

  private List<int> GetIndexes(IDbManager manager, ImbaseIndexingService.Task task, long catalogId)
  {
    List<int> intList = (List<int>) null;
    IDbManager manager1 = manager;
    List<long> catalogIDs = new List<long>();
    catalogIDs.Add(catalogId);
    string[] colsNames = new string[2]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE
    };
    DataTable indexes = this.GetIndexes(manager1, catalogIDs, colsNames);
    if (indexes != null)
    {
      List<int> list = indexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
      if (list.Count > 0)
      {
        foreach (int attrTypeID in list)
          task.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrTypeID).Name, (object) attrTypeID)));
      }
      intList = indexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) != Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
    }
    return intList == null || intList.Count <= 0 ? (List<int>) null : intList;
  }

  public List<long> CheckUniqueBeforeCopyMove(
    Guid sessionGuid,
    long catalogId,
    List<long> objIDs,
    bool isCopy)
  {
    List<long> longList = (List<long>) null;
    if (catalogId != 0L && objIDs != null && objIDs.Count > 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      this.CheckBase(userSession, sessionGuid, new List<long>()
      {
        catalogId
      });
      List<int> uniqueIndexes = this.GetUniqueIndexes(userSession.DataManager, catalogId);
      if (uniqueIndexes != null)
      {
        List<long> source = new List<long>();
        foreach (long objId in objIDs)
        {
          QuickObjectInfo objectInfo = userSession.GetObjectInfo(objId);
          if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
            source.Add(objId);
          else if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID)
          {
            string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) userSession, objId);
            DataTable tableRefIds = this.GetTableRefIDs(userSession, classifKeyByObjId);
            if (tableRefIds.Rows.Count != 0)
              source.AddRange((IEnumerable<long>) tableRefIds.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Math.Abs(Convert.ToInt64(x[IndexesField.F_LINK_ID])))));
          }
        }
        List<long> list = source.Distinct<long>().ToList<long>();
        if (list.Count > 0)
        {
          if (uniqueIndexes.Contains(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID))
            longList = this.CheckDublicatesTable(userSession, catalogId, list, isCopy);
          if (longList == null)
          {
            Dictionary<int, string> dictionary1 = uniqueIndexes.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(v))));
            Dictionary<int, string> dictionary2 = uniqueIndexes.ToDictionary<int, int, string>((System.Func<int, int>) (k => k), (System.Func<int, string>) (v => ImbaseIndexingService.GenerateTableName(catalogId, v)));
            foreach (long num in list)
              this.CheckTableRef(userSession, isCopy ? num : Math.Abs(num), dictionary1, dictionary2, catalogId);
          }
        }
      }
    }
    return longList;
  }

  public void UpdateAfterCopiedMoved(
    Guid sessionGuid,
    Guid taskGuid,
    long oldCatalogId,
    long newCatalogId,
    List<long> objIDs)
  {
    if (newCatalogId == 0L)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.UndefinedCatalogId);
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      oldCatalogId,
      newCatalogId
    });
    string str = LocalizationHolder.rm.GetString("Imbase_Indexing_CopyMoveIndexing");
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid, newCatalogId)
    {
      Caption = str,
      TaskName = str
    });
    new Action<Guid, Guid, long, long, List<long>>(this.UpdateDataAfterCopyMove).BeginInvoke(sessionGuid, taskGuid, oldCatalogId, newCatalogId, objIDs, (AsyncCallback) null, (object) null);
  }

  private void UpdateDataAfterCopyMove(
    Guid sessionGuid,
    Guid taskGuid,
    long oldCatalogId,
    long newCatalogId,
    List<long> objIDs)
  {
    if ((objIDs == null || objIDs.Count <= 0) && newCatalogId == 0L)
      return;
    ImbaseIndexingService.Task task1 = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    UserSession session = (UserSession) null;
    try
    {
      session = this.GetUserSession(sessionGuid, "ImbaseIndexing.UpdateDataAfterCopyMove", true);
      if (task1 == null)
        return;
      task1.ComputerName = session.ComputerName;
      List<int> indexes1 = this.GetIndexes(session.DataManager, task1, newCatalogId);
      if (indexes1 != null)
      {
        int index = 0;
        while (index < indexes1.Count)
        {
          if (session.GetAttributeType(indexes1[index], false) == null)
          {
            task1.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.NullAttribute, (object) indexes1[index])));
            indexes1.RemoveAt(index);
          }
          else
            ++index;
        }
        task1.Attributes = indexes1;
      }
      if (task1.Attributes.Count > 0 || oldCatalogId != 0L)
      {
        List<ImbaseIndexingService.TableRefInfo> source = new List<ImbaseIndexingService.TableRefInfo>();
        if (objIDs != null)
        {
          foreach (long objId in objIDs)
          {
            long num = Math.Abs(objId);
            QuickObjectInfo objectInfo = session.GetObjectInfo(num);
            if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
            {
              long tableReference = TableLoadHelper.GetTableReference((IUserSession) session, num);
              if (tableReference != 0L)
                source.Add(new ImbaseIndexingService.TableRefInfo()
                {
                  CatalogId = newCatalogId,
                  TableRefId = num,
                  TableId = tableReference
                });
            }
            else if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID)
            {
              string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, num);
              DataTable tableRefIds = this.GetTableRefIDs(session, classifKeyByObjId);
              if (tableRefIds.Rows.Count != 0)
                source.AddRange((IEnumerable<ImbaseIndexingService.TableRefInfo>) tableRefIds.AsEnumerable().Select<DataRow, ImbaseIndexingService.TableRefInfo>((System.Func<DataRow, ImbaseIndexingService.TableRefInfo>) (x => new ImbaseIndexingService.TableRefInfo()
                {
                  CatalogId = newCatalogId,
                  TableRefId = Math.Abs(Convert.ToInt64(x[IndexesField.F_LINK_ID])),
                  TableId = Convert.ToInt64(x[IndexesField.F_TABLE_ID])
                })));
            }
          }
        }
        task1.TableRefInfoList = source;
        if (source.Count > 0)
        {
          if (oldCatalogId != 0L)
          {
            IDbManager dataManager = session.DataManager;
            List<long> catalogIDs = new List<long>();
            catalogIDs.Add(oldCatalogId);
            string[] colsNames = new string[1]
            {
              IndexesField.F_ATTRIBUTE_ID
            };
            DataTable indexes2 = this.GetIndexes(dataManager, catalogIDs, colsNames);
            if (indexes2 != null)
            {
              List<int> list1 = indexes2.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
              List<long> list2 = source.Select<ImbaseIndexingService.TableRefInfo, long>((System.Func<ImbaseIndexingService.TableRefInfo, long>) (x => Math.Abs(x.TableRefId))).ToList<long>();
              this.DeleteOldTableRefs(session.DataManager, oldCatalogId, list1, list2);
            }
          }
          if (task1.Attributes.Count > 0)
          {
            task1.ClearValue();
            task1.CountItems = task1.TableRefInfoList.Count;
            foreach (ImbaseIndexingService.TableRefInfo tableRefInfo in task1.TableRefInfoList)
            {
              if (!task1.Terminated)
              {
                if (session.GetObject(tableRefInfo.TableRefId, false) != null)
                  this.AddIndexesData(session, task1, tableRefInfo);
                ++task1.CurrItemNumber;
              }
              else
                break;
            }
          }
        }
      }
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      task1.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      session?.Logout("ImbaseIndexing.UpdateDataAfterCopyMove");
      if (task1 != null && task1.RemoveAfterComplete)
        this.RemoveAfterComplete(task1);
    }
  }

  private void DeleteOldTableRefs(
    IDbManager manager,
    long catalogId,
    List<int> indexes,
    List<long> tableRefIDs)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    int maximumInOperands = manager.DataProvider.MaximumINOperands;
    List<IDbDataParameter> pars = new List<IDbDataParameter>();
    try
    {
      foreach (int index1 in indexes)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, index1);
        manager.BeginTransaction();
        if (tableRefIDs.Count > 1)
        {
          int index2 = 0;
          while (index2 < tableRefIDs.Count)
          {
            pars.Clear();
            int count = tableRefIDs.Count - index2 > maximumInOperands ? maximumInOperands : tableRefIDs.Count - index2;
            long[] numArray = new long[count];
            tableRefIDs.CopyTo(index2, numArray, 0, count);
            index2 += count;
            string paramsRange = this.CreateParamsRange<long>(manager, new List<long>((IEnumerable<long>) numArray), pars);
            manager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID} IN {paramsRange}", pars.ToArray());
          }
        }
        else if (tableRefIDs.Count == 1)
          manager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:l_ID", manager.Parameter(":l_ID", (object) tableRefIDs[0]));
        manager.Commit();
      }
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private Dictionary<long, long> GetTableRefIdTableIdCopy(
    IDBObjectCollection coll,
    List<long> tableRefIDs)
  {
    Dictionary<long, long> refIdTableIdCopy = new Dictionary<long, long>(tableRefIDs.Count);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) tableRefIDs.ToArray(), LogicalOperators.AND, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    DataTable dataTable = coll.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = row[1] != DBNull.Value ? Convert.ToInt64(row[1]) : 0L;
        if (int64 != 0L)
          refIdTableIdCopy.Add(Math.Abs(Convert.ToInt64(row[0])), int64);
      }
    }
    return refIdTableIdCopy;
  }

  private Dictionary<long, long> GetTableRefIdTableIdMove(
    UserSession session,
    IDBObjectCollection coll,
    List<long> tableRefIDs)
  {
    Dictionary<long, long> refIdTableIdMove = new Dictionary<long, long>(tableRefIDs.Count);
    ColumnDescriptor columnDescriptor1 = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    ColumnDescriptor columnDescriptor2 = new ColumnDescriptor((object) Intermech.Imbase.Consts.ImbaseTableRefAttID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) tableRefIDs.ToArray(), LogicalOperators.AND, 0, false)
    }, new ColumnDescriptor[2]
    {
      columnDescriptor1,
      columnDescriptor2
    });
    DataTable dataTable = coll.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        long num = row[1] != DBNull.Value ? Convert.ToInt64(row[1]) : 0L;
        if (int64 < 0L)
        {
          IDBAttribute attributeById = session.GetObject(Math.Abs(int64), false)?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
          if (attributeById != null && attributeById.Value != DBNull.Value)
            num = attributeById.AsInteger;
          else
            continue;
        }
        if (num != 0L)
          refIdTableIdMove.Add(Math.Abs(int64), num);
      }
    }
    return refIdTableIdMove;
  }

  private List<int> GetUniqueIndexes(IDbManager manager, long catalogId)
  {
    List<int> intList = (List<int>) null;
    IDbManager manager1 = manager;
    List<long> catalogIDs = new List<long>();
    catalogIDs.Add(catalogId);
    string[] colsNames = new string[3]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE,
      IndexesField.F_FLAG
    };
    DataTable uniqueIndexes = this.GetUniqueIndexes(manager1, catalogIDs, colsNames);
    if (uniqueIndexes != null)
    {
      List<int> list = uniqueIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
      if (list.Count > 0)
      {
        List<string> lst = new List<string>(list.Count);
        list.ForEach((Action<int>) (x => lst.Add($"'{MetaDataHelper.GetAttributeType(x).Name}' (ID = {x})")));
        throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Attributes_Busy"), (object) string.Join(", ", lst.ToArray())));
      }
      intList = uniqueIndexes.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
    }
    return intList == null || intList.Count <= 0 ? (List<int>) null : intList;
  }

  private List<long> CheckDublicatesTable(
    UserSession session,
    long catalogId,
    List<long> tableRefIDs,
    bool isCopy)
  {
    List<long> longList = (List<long>) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.TableRefListEmpty);
    Dictionary<long, long> source1 = isCopy ? this.GetTableRefIdTableIdCopy(objectCollection, tableRefIDs) : this.GetTableRefIdTableIdMove(session, objectCollection, tableRefIDs);
    if (source1.Count > 0)
    {
      if (source1.Count > 1)
      {
        Dictionary<long, List<long>> dictionary = source1.GroupBy<KeyValuePair<long, long>, long, long>((System.Func<KeyValuePair<long, long>, long>) (x => x.Value), (System.Func<KeyValuePair<long, long>, long>) (x => x.Key)).ToDictionary<IGrouping<long, long>, long, List<long>>((System.Func<IGrouping<long, long>, long>) (x => x.Key), (System.Func<IGrouping<long, long>, List<long>>) (y => y.ToList<long>()));
        if (source1.Count != dictionary.Count)
        {
          longList = new List<long>();
          foreach (KeyValuePair<long, List<long>> keyValuePair in dictionary)
          {
            if (keyValuePair.Value.Count != 1)
              longList.AddRange((IEnumerable<long>) keyValuePair.Value);
          }
        }
      }
      if (longList == null)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
        List<IDbDataParameter> pars = new List<IDbDataParameter>(source1.Count);
        string paramsRange = this.CreateParamsRange<long>(session.DataManager, source1.Values.ToList<long>(), pars);
        string commandText = string.Format("SELECT DISTINCT {0}, {1} FROM {2} WHERE {1} IN {3}", (object) IndexesField.F_LINK_ID, (object) IndexesField.F_TABLE_ID, (object) tableName, (object) paramsRange);
        DataTable source2 = session.DataManager.ExecuteDataTable(commandText, pars.ToArray());
        if (source2 != null && source2.Rows.Count > 0)
        {
          List<long> tIDs = source2.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_TABLE_ID]))).ToList<long>();
          List<long> list = source1.Where<KeyValuePair<long, long>>((System.Func<KeyValuePair<long, long>, bool>) (x => tIDs.Contains(x.Value))).Select<KeyValuePair<long, long>, long>((System.Func<KeyValuePair<long, long>, long>) (x => x.Key)).ToList<long>();
          longList = source2.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_LINK_ID]))).ToList<long>();
          longList.AddRange((IEnumerable<long>) list);
        }
      }
    }
    return longList;
  }

  private void CheckTableRef(
    UserSession session,
    long tableRefId,
    Dictionary<int, string> indexes,
    Dictionary<int, string> tableNames,
    long catalogId)
  {
    IDBObject dbObject = session.GetObject(tableRefId, false);
    if (dbObject == null)
      return;
    DataTable dtAttrs;
    DataTable dtData;
    long tables = this.GetTables(session, tableRefId, out dtAttrs, out dtData);
    this.AssignAttributes(session, tableRefId, tables, dtAttrs, dtData);
    int maximumInOperands = session.DataManager.DataProvider.MaximumINOperands;
    List<IDbDataParameter> pars = new List<IDbDataParameter>();
    List<string> stringList = new List<string>(dtData.Rows.Count);
    List<string> values = new List<string>();
    string empty = string.Empty;
    IDbDataParameter dbDataParameter = session.DataManager.Parameter(":l_ID", (object) tableRefId);
    foreach (KeyValuePair<int, string> index1 in indexes)
    {
      KeyValuePair<int, string> pair = index1;
      if (!dtData.Columns.Contains(pair.Value) || dtData.Rows.Count == 0)
      {
        if (!string.IsNullOrEmpty(empty))
        {
          ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(pair.Key)));
          if (task != null)
          {
            string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(pair.Key).Name, (object) pair.Key);
            throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
          }
          string commandText = $"SELECT {IndexesField.F_LINK_ID} FROM {tableNames[pair.Key]} WHERE {IndexesField.F_TEXT}=:parText";
          if (session.DataManager.ExecuteDataTable(commandText, session.DataManager.Parameter(":parText", (object) empty)).Rows.Count != 0)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) dbObject.Caption, (object) tableRefId));
        }
      }
      else
      {
        ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(pair.Key)));
        if (task != null)
        {
          string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(pair.Key).Name, (object) pair.Key);
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
        }
        stringList.Clear();
        int num1 = 0;
        while (num1 < dtData.Rows.Count)
        {
          pars.Clear();
          values.Clear();
          int num2 = dtData.Rows.Count - num1 > maximumInOperands ? maximumInOperands : dtData.Rows.Count - num1;
          int num3 = 0;
          if (!string.IsNullOrEmpty(empty))
          {
            num3 = num2 == dtData.Rows.Count ? 0 : 1;
            stringList.Add(empty);
            values.Add(empty);
          }
          for (int index2 = num3; index2 < num2; ++index2)
          {
            string str = Convert.ToString(dtData.Rows[num1++][pair.Value]).MaxStringLength();
            if (!string.IsNullOrEmpty(str))
            {
              if (stringList.Contains(str))
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) dbObject.Caption, (object) tableRefId));
              stringList.Add(str);
              values.Add(str);
            }
          }
          empty = string.Empty;
          pars.Add(dbDataParameter);
          if (values.Count != 0)
          {
            string paramsRange = this.CreateParamsRange<string>(session.DataManager, values, pars);
            string commandText = $"SELECT COUNT(*) FROM {tableNames[pair.Key]} WHERE {IndexesField.F_LINK_ID}<>:l_ID AND {IndexesField.F_TEXT} IN {paramsRange}";
            if (Convert.ToInt32(session.DataManager.ExecuteScalar(commandText, pars.ToArray())) > 0)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) dbObject.Caption, (object) tableRefId));
          }
        }
      }
    }
  }

  private void CheckTableRef(
    UserSession session,
    long tableRefId,
    long tableId,
    Dictionary<int, string> indexes,
    Dictionary<int, string> tableNames,
    Dictionary<int, object> values = null,
    long catalogId = 0,
    bool ignoreTableAttr = false)
  {
    bool flag = true;
    dbObject = (DBObject) null;
    try
    {
      if (session.GetObjectActualCopy(tableRefId, false) is DBObject dbObject)
      {
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout)
        {
          if (dbObject.CheckoutBy == session.UserID)
            throw new Exception();
        }
      }
    }
    catch (Exception ex)
    {
      flag = false;
    }
    if (!flag)
      return;
    DataTable dtAttrs;
    DataTable dtData;
    tableId = this.GetTables(session, tableId != 0L ? tableId : tableRefId, out dtAttrs, out dtData);
    this.AssignAttributes(session, tableRefId, tableId, dtAttrs, dtData, values, ignoreTableAttr);
    int maximumInOperands = session.DataManager.DataProvider.MaximumINOperands;
    List<IDbDataParameter> pars = new List<IDbDataParameter>();
    List<string> stringList = new List<string>(dtData.Rows.Count);
    List<string> values1 = new List<string>();
    IDbDataParameter dbDataParameter = session.DataManager.Parameter(":l_ID", (object) tableRefId);
    foreach (KeyValuePair<int, string> index1 in indexes)
    {
      KeyValuePair<int, string> pair = index1;
      if (dtData.Columns.Contains(pair.Value) && dtData.Rows.Count != 0)
      {
        ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(pair.Key)));
        if (task != null)
        {
          string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(pair.Key).Name, (object) pair.Key);
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
        }
        stringList.Clear();
        int num1 = 0;
        while (num1 < dtData.Rows.Count)
        {
          pars.Clear();
          values1.Clear();
          int num2 = dtData.Rows.Count - num1 > maximumInOperands ? maximumInOperands : dtData.Rows.Count - num1;
          for (int index2 = 0; index2 < num2; ++index2)
          {
            string str = Convert.ToString(dtData.Rows[num1++][pair.Value]).MaxStringLength();
            if (!string.IsNullOrEmpty(str))
            {
              if (stringList.Contains(str) && dbObject != null)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) dbObject.Caption, (object) tableRefId));
              stringList.Add(str);
              values1.Add(str);
            }
          }
          if (values1.Count != 0)
          {
            pars.Add(dbDataParameter);
            string paramsRange = this.CreateParamsRange<string>(session.DataManager, values1, pars);
            string commandText = $"SELECT COUNT(*) FROM {tableNames[pair.Key]} WHERE {IndexesField.F_LINK_ID}<>:l_ID AND {IndexesField.F_TEXT} IN {paramsRange}";
            if (Convert.ToInt32(session.DataManager.ExecuteScalar(commandText, pars.ToArray())) > 0)
              throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_TableRef_NotUniqueData"), (object) dbObject?.Caption, (object) tableRefId));
          }
        }
      }
    }
  }

  public DataTable CheckUniqueBeforeTableDataChange(
    Guid sessionGuid,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    out List<int> uIndexes,
    out List<long> keys)
  {
    DataTable dtDistination = (DataTable) null;
    keys = new List<long>();
    uIndexes = new List<int>();
    if (tableId != 0L && dtAttrs != null && dtAttrs.Rows.Count > 0 && dtData != null && dtData.Rows.Count > 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      Dictionary<long, List<long>> idsGroupByCatalog = this.GetTableRefIDsGroupByCatalog((IUserSession) userSession, tableId);
      if (idsGroupByCatalog != null)
      {
        this.CheckBase(userSession, sessionGuid, idsGroupByCatalog.Keys.ToList<long>());
        Dictionary<int, string> attrData = (Dictionary<int, string>) null;
        Dictionary<long, List<int>> uniqueIndexes = this.GetUniqueIndexes(userSession.DataManager, idsGroupByCatalog.Keys.ToList<long>(), dtAttrs, ref attrData);
        if (uniqueIndexes != null)
        {
          foreach (long key in uniqueIndexes.Keys)
          {
            if (idsGroupByCatalog[key].Count != 1)
              throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) userSession.GetObjectInfo(key).Caption, (object) key, (object) tableId));
          }
          Dictionary<string, List<long>> dictionary = new Dictionary<string, List<long>>(dtData.Rows.Count);
          List<string> values = new List<string>();
          int maximumInOperands = userSession.DataManager.DataProvider.MaximumINOperands;
          List<IDbDataParameter> pars = new List<IDbDataParameter>();
          foreach (KeyValuePair<long, List<int>> keyValuePair1 in uniqueIndexes)
          {
            if (idsGroupByCatalog.ContainsKey(keyValuePair1.Key))
            {
              long num1 = idsGroupByCatalog[keyValuePair1.Key][0];
              IDBObject dbObject = userSession.GetObject(num1, false);
              DataTable dtAttrs1 = dtAttrs.Copy();
              DataTable dtData1 = dtData.Copy();
              this.AssignAttributes(userSession, num1, tableId, dtAttrs1, dtData1);
              IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":l_ID", (object) num1);
              foreach (int num2 in keyValuePair1.Value)
              {
                dictionary.Clear();
                string tableName = ImbaseIndexingService.GenerateTableName(keyValuePair1.Key, num2);
                IDBAttribute attributeById = dbObject.GetAttributeByID(num2);
                if (attributeById != null)
                {
                  string key = attributeById.AsString.MaxStringLength();
                  if (!string.IsNullOrEmpty(key))
                    dictionary.Add(key, new List<long>()
                    {
                      -1L
                    });
                }
                int num3 = 0;
                while (num3 < dtData1.Rows.Count)
                {
                  pars.Clear();
                  values.Clear();
                  int num4 = dtData1.Rows.Count - num3 > maximumInOperands ? maximumInOperands : dtData1.Rows.Count - num3;
                  for (int index = 0; index < num4; ++index)
                  {
                    DataRow row = dtData1.Rows[num3++];
                    if (row.RowState != DataRowState.Deleted)
                    {
                      string str_to_index = Convert.ToString(row[attrData[num2]]).MaxStringLength();
                      string indexedString = userSession.StringNormalizer.GetIndexedString(str_to_index);
                      if (!string.IsNullOrEmpty(indexedString))
                      {
                        long int64 = Convert.ToInt64(row["F_KEY"]);
                        if (dictionary.ContainsKey(indexedString))
                        {
                          dictionary[indexedString].Add(int64);
                        }
                        else
                        {
                          dictionary.Add(indexedString, new List<long>()
                          {
                            int64
                          });
                          values.Add(indexedString);
                        }
                      }
                    }
                  }
                  if (values.Count != 0)
                  {
                    pars.Add(dbDataParameter);
                    string paramsRange = this.CreateParamsRange<string>(userSession.DataManager, values, pars);
                    string commandText = $"SELECT * FROM {tableName} WHERE {IndexesField.F_LINK_ID}<>:l_ID AND {IndexesField.F_HASHTEXT} IN {paramsRange}";
                    DataTable dtSource = userSession.DataManager.ExecuteDataTable(commandText, pars.ToArray());
                    if (dtSource != null && dtSource.Rows.Count != 0)
                    {
                      uIndexes.Add(num2);
                      this.CopyRowsFromTableToTable(ref dtDistination, dtSource, num2);
                      foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
                        keys.AddRange((IEnumerable<long>) dictionary[Convert.ToString(row[IndexesField.F_HASHTEXT])]);
                    }
                  }
                }
                foreach (KeyValuePair<string, List<long>> keyValuePair2 in dictionary)
                {
                  if (keyValuePair2.Value.Count >= 2)
                  {
                    uIndexes.Add(num2);
                    keys.AddRange((IEnumerable<long>) keyValuePair2.Value);
                  }
                }
              }
            }
          }
        }
        keys = keys.Where<long>((System.Func<long, bool>) (x => x > -1L)).Distinct<long>().ToList<long>();
        uIndexes = uIndexes.Distinct<int>().ToList<int>();
      }
    }
    return dtDistination;
  }

  public void UpdateAfterTableDataChanged(
    Guid sessionGuid,
    Guid taskGuid,
    long tableId,
    List<long> deletedRowNums,
    List<int> deletedIndexes)
  {
    if (tableId == 0L)
      return;
    this.Tasks.Add(new ImbaseIndexingService.Task(taskGuid)
    {
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_AfterTableEdit")
    });
    new Action<Guid, Guid, long, List<long>, List<int>>(this.UpdateDataAfterTableDataChanged).BeginInvoke(sessionGuid, taskGuid, tableId, deletedRowNums, deletedIndexes, (AsyncCallback) null, (object) null);
  }

  private void CopyRowsFromTableToTable(
    ref DataTable dtDistination,
    DataTable dtSource,
    int attrId)
  {
    if (dtDistination == null)
    {
      dtDistination = dtSource.Clone();
      dtDistination.Columns.Add(IndexesField.F_ATTRIBUTE_ID);
    }
    foreach (DataRow row1 in (InternalDataCollectionBase) dtSource.Rows)
    {
      DataRow row2 = dtDistination.NewRow();
      row2.ItemArray = row1.ItemArray;
      row2[IndexesField.F_ATTRIBUTE_ID] = (object) attrId;
      dtDistination.Rows.Add(row2);
    }
  }

  private void UpdateDataAfterTableDataChanged(
    Guid sessionGuid,
    Guid taskGuid,
    long tableId,
    List<long> deletedRowNums,
    List<int> deletedIndexes)
  {
    ImbaseIndexingService.Task task1 = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (task => task.TaskGuid == taskGuid));
    UserSession session = (UserSession) null;
    try
    {
      session = this.GetUserSession(sessionGuid, "ImbaseIndexing.UpdateDataAfterTableDataChanged", true);
      if (task1 == null)
        return;
      task1.ComputerName = session.ComputerName;
      Dictionary<long, List<long>> idsGroupByCatalog = this.GetTableRefIDsGroupByCatalog((IUserSession) session, tableId);
      if (idsGroupByCatalog != null)
      {
        this.CheckBase(session, sessionGuid, idsGroupByCatalog.Keys.ToList<long>());
        Dictionary<long, List<int>> indexes = this.GetIndexes(session.DataManager, task1, idsGroupByCatalog.Keys.ToList<long>());
        if (indexes != null)
        {
          DataTable dtAttrs;
          DataTable dtData;
          this.GetTables(session, tableId, out dtAttrs, out dtData);
          Dictionary<int, string> dictionary = new Dictionary<int, string>(dtAttrs.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) dtAttrs.Rows)
          {
            string Guid = Convert.ToString(row["F_ATTRIBUTE_GUID"]);
            int attributeTypeId = MetaDataHelper.GetAttributeTypeID(Guid);
            switch (attributeTypeId)
            {
              case -10000:
              case 0:
                continue;
              default:
                dictionary.Add(attributeTypeId, Guid);
                continue;
            }
          }
          foreach (KeyValuePair<long, List<int>> keyValuePair in indexes)
          {
            task1.CatalogId = keyValuePair.Key;
            task1.Attributes = keyValuePair.Value;
            if (deletedIndexes != null && deletedIndexes.Count > 0)
            {
              List<int> list = keyValuePair.Value.Intersect<int>((IEnumerable<int>) deletedIndexes).ToList<int>();
              if (list.Count > 0)
                this.DeleteIndexesFromTable(session.DataManager, keyValuePair.Key, list, tableId);
            }
            if (dictionary.Count != 0)
            {
              task1.Attributes = keyValuePair.Value.Intersect<int>((IEnumerable<int>) dictionary.Keys.ToList<int>()).ToList<int>();
              if (keyValuePair.Value.Contains(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID))
                task1.Attributes.Add(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
              if (task1.Attributes.Count != 0)
              {
                if (deletedRowNums.Count > 0)
                  this.DeleteRowsFromTable(session.DataManager, keyValuePair.Key, task1.Attributes, tableId, deletedRowNums);
                foreach (long tableRefId in idsGroupByCatalog[keyValuePair.Key])
                  this.UpdateIndexDataForTableRef(session, task1, tableRefId, tableId, dtAttrs.Copy(), dtData.Copy());
              }
            }
          }
        }
      }
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (IndexingException ex)
    {
      task1.Exceptions = new List<IndexingException>()
      {
        ex
      };
      this.SetTaskCompleted(task1, ImbaseIndexingService.TaskState.Error);
    }
    finally
    {
      session.Logout("ImbaseIndexing.UpdateDataAfterTableDataChanged");
      if (task1 != null && task1.RemoveAfterComplete)
        this.RemoveAfterComplete(task1);
    }
  }

  private void DeleteIndexesFromTable(
    IDbManager manager,
    long catalogId,
    List<int> indexes,
    long tableId)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    IDbDataParameter dbDataParameter = manager.Parameter(":t_ID", (object) tableId);
    try
    {
      foreach (int index in indexes)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, index);
        manager.BeginTransaction();
        manager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_TABLE_ID}=:t_ID", dbDataParameter);
        manager.Commit();
      }
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private void DeleteRowsFromTable(
    IDbManager manager,
    long catalogId,
    List<int> indexes,
    long tableId,
    List<long> rowNums)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    List<IDbDataParameter> pars = new List<IDbDataParameter>()
    {
      manager.Parameter(":t_ID", (object) tableId)
    };
    string paramsRange = this.CreateParamsRange<long>(manager, rowNums, pars);
    try
    {
      foreach (int index in indexes)
      {
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, index);
        manager.BeginTransaction();
        manager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_TABLE_ID}=:t_ID AND {IndexesField.F_TABKEY} IN {paramsRange}", pars.ToArray());
        manager.Commit();
      }
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  private Dictionary<long, List<int>> GetIndexes(
    IDbManager manager,
    ImbaseIndexingService.Task task,
    List<long> catalogIDs)
  {
    Dictionary<long, List<int>> dictionary = (Dictionary<long, List<int>>) null;
    DataTable indexes = this.GetIndexes(manager, catalogIDs, new string[3]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE
    });
    if (indexes != null)
    {
      dictionary = new Dictionary<long, List<int>>(indexes.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        int int32 = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
        if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))
          task.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(int32).Name, (object) int32)));
        long int64 = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
        if (!dictionary.ContainsKey(int64))
          dictionary.Add(int64, new List<int>() { int32 });
        else
          dictionary[int64].Add(int32);
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, List<int>>) null : dictionary;
  }

  private void UpdateIndexDataForTableRef(
    UserSession session,
    ImbaseIndexingService.Task task,
    long tableRefId,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData)
  {
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      this.AssignAttributes(session, tableRefId, tableId, dtAttrs, dtData);
      if (session.DataManager.DataProvider.Name == "Sql")
        this.AddIndexesDataForSql(session, task, tableRefId, tableId, dtData, (Action) null);
      else if (session.DataManager.DataProvider.Name == "Oracle")
      {
        this.AddIndexesDataForOracle(session, task, tableRefId, tableId, dtData, (Action) null);
      }
      else
      {
        if (!(session.DataManager.DataProvider.Name == "PostgreSQL"))
          return;
        this.AddIndexesDataForOther(session, task, tableRefId, tableId, dtData, (Action) null);
      }
    }
    catch (IndexingException ex)
    {
      task.Exceptions.Add(ex);
    }
    catch (Exception ex)
    {
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
    }
  }

  public void UpdateAfterRestructured(
    Guid sessionGuid,
    long catalogId,
    List<long> tableRefIDs,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    List<int> attrIDs)
  {
    if (catalogId == 0L || tableRefIDs == null || tableRefIDs.Count <= 0 || tableId == 0L || dtAttrs == null || dtData == null || attrIDs == null || attrIDs.Count <= 0)
      return;
    this.CheckBase((UserSession) null, sessionGuid, new List<long>()
    {
      catalogId
    });
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid(), catalogId)
    {
      Attributes = attrIDs,
      TaskName = LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_AfterRestructured")
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      Dictionary<int, string> indexes = this.GetIndexes(userSession.DataManager, task, attrIDs);
      if (indexes != null)
      {
        task.Attributes = indexes.Keys.ToList<int>();
        List<IDbDataParameter> pars = new List<IDbDataParameter>(tableRefIDs.Count);
        string paramsRange = this.CreateParamsRange<long>(userSession.DataManager, tableRefIDs, pars);
        foreach (string str in indexes.Values)
        {
          userSession.StartTransaction();
          try
          {
            userSession.DataManager.ExecuteNonQuery($"DELETE FROM {str} WHERE {IndexesField.F_LINK_ID} IN {paramsRange}", pars.ToArray());
            userSession.Commit();
          }
          catch (Exception ex)
          {
            userSession.Rollback();
            this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Error);
            throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
          }
        }
        foreach (long tableRefId in tableRefIDs)
        {
          DataTable dtData1 = dtData.Copy();
          this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dtData1);
          if (userSession.DataManager.DataProvider.Name == "Sql")
            this.AddIndexesDataForSql(userSession, task, tableRefId, tableId, dtData1, (Action) null, false);
          else if (userSession.DataManager.DataProvider.Name == "Oracle")
            this.AddIndexesDataForOracle(userSession, task, tableRefId, tableId, dtData1, (Action) null, false);
          else if (userSession.DataManager.DataProvider.Name == "PostgreSQL")
            this.AddIndexesDataForOther(userSession, task, tableRefId, tableId, dtData1, (Action) null, false);
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private Dictionary<int, string> GetIndexes(
    IDbManager manager,
    ImbaseIndexingService.Task task,
    List<int> attrIDs)
  {
    Dictionary<int, string> dictionary = (Dictionary<int, string>) null;
    IDbManager manager1 = manager;
    List<long> catalogIDs = new List<long>();
    catalogIDs.Add(task.CatalogId);
    string[] colsNames = new string[3]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE,
      IndexesField.F_TABLE_NAME
    };
    DataTable indexes = this.GetIndexes(manager1, catalogIDs, colsNames);
    if (indexes != null)
    {
      List<int> list = indexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))).Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
      if (list.Count > 0)
        list.ForEach((Action<int>) (x => task.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(x).Name, (object) x)))));
      dictionary = indexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_STATE]) != Convert.ToInt32((object) IndexesStates.Locked) && attrIDs.Contains(Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID])))).ToDictionary<DataRow, int, string>((System.Func<DataRow, int>) (k => Convert.ToInt32(k[IndexesField.F_ATTRIBUTE_ID])), (System.Func<DataRow, string>) (v => Convert.ToString(v[IndexesField.F_TABLE_NAME])));
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<int, string>) null : dictionary;
  }

  public DataTable CheckUniqueBeforeTableRefCheckIn(
    Guid sessionGuid,
    long tableRefId,
    out List<int> uIndexes,
    out List<long> keys)
  {
    DataTable dataTable = (DataTable) null;
    keys = new List<long>();
    uIndexes = new List<int>();
    if (tableRefId != 0L)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      long num = 0;
      try
      {
        num = TableLoadHelper.GetTableReference((IUserSession) userSession, tableRefId);
      }
      catch
      {
      }
      if (num == 0L)
      {
        this.CheckUniqueTableRefOnly(userSession, tableRefId);
      }
      else
      {
        IDBObject objectActualCopy = userSession.GetObjectActualCopy(num, false);
        if (objectActualCopy == null || objectActualCopy.CheckoutBy == userSession.UserID)
          this.CheckUniqueTableRefOnly(userSession, tableRefId);
        else
          dataTable = this.CheckUniqueTableRefWithTable(userSession, tableRefId, num, ref uIndexes, ref keys);
      }
    }
    return dataTable;
  }

  private void CheckUniqueTableRefOnly(UserSession session, long tableRefId)
  {
    IDBObject objectActualCopy = session.GetObjectActualCopy(tableRefId, false);
    if (objectActualCopy == null)
      throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefNull, (object) tableRefId));
    long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) session, objectActualCopy.ObjectID);
    if (catalogIdByObjectId == 0L)
      return;
    this.CheckBase(session, session.SessionGUID, new List<long>()
    {
      catalogIdByObjectId
    });
    List<int> uniqueIndexes = this.GetUniqueIndexes(session.DataManager, catalogIdByObjectId);
    if (uniqueIndexes == null)
      return;
    foreach (int num in uniqueIndexes)
    {
      IDBAttribute attributeById = objectActualCopy.GetAttributeByID(num);
      string str = attributeById != null ? Convert.ToString(attributeById.Value).MaxStringLength() : string.Empty;
      if (!string.IsNullOrEmpty(str))
      {
        string commandText = $"SELECT COUNT(*) FROM {ImbaseIndexingService.GenerateTableName(catalogIdByObjectId, num)} WHERE {IndexesField.F_TEXT}=:textValue AND {IndexesField.F_LINK_ID}<>:l_ID";
        IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) Math.Abs(tableRefId));
        IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":textValue", (object) str);
        if (Convert.ToInt32(session.DataManager.ExecuteScalar(commandText, dbDataParameter2, dbDataParameter1)) > 0)
          throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_CheckInObject_NotUniqueValue"), (object) attributeById?.Name, (object) num));
      }
    }
  }

  private DataTable CheckUniqueTableRefWithTable(
    UserSession session,
    long tableRefId,
    long tableId,
    ref List<int> uIndexes,
    ref List<long> keys)
  {
    DataTable dtDistination = (DataTable) null;
    IDBObject objectActualCopy = session.GetObjectActualCopy(tableRefId, false);
    if (objectActualCopy == null)
      throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefNull, (object) tableRefId));
    long catalogId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) session, objectActualCopy.ObjectID);
    if (catalogId != 0L)
    {
      this.CheckBase(session, session.SessionGUID, new List<long>()
      {
        catalogId
      });
      List<int> uniqueIndexes = this.GetUniqueIndexes(session.DataManager, catalogId);
      if (uniqueIndexes != null)
      {
        DataTable dtAttrs;
        DataTable dtData;
        tableId = this.GetTables(session, tableId, out dtAttrs, out dtData);
        this.AssignAttributes(session, objectActualCopy.ObjectID, tableId, dtAttrs, dtData);
        Dictionary<string, List<long>> dictionary = new Dictionary<string, List<long>>(dtData.Rows.Count);
        List<string> values = new List<string>();
        int maximumInOperands = session.DataManager.DataProvider.MaximumINOperands;
        List<IDbDataParameter> pars = new List<IDbDataParameter>();
        IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) Math.Abs(tableRefId));
        foreach (int num1 in uniqueIndexes)
        {
          int attrId = num1;
          dictionary.Clear();
          string tableName = ImbaseIndexingService.GenerateTableName(catalogId, attrId);
          string str1 = Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId));
          string key;
          try
          {
            IDBAttribute attributeById = objectActualCopy.GetAttributeByID(attrId);
            key = attributeById != null ? attributeById.AsString.MaxStringLength() : string.Empty;
          }
          catch (Exception ex)
          {
            key = string.Empty;
          }
          if (!dtData.Columns.Contains(str1) || dtData.Rows.Count == 0)
          {
            if (!string.IsNullOrEmpty(key))
            {
              ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
              if (task != null)
              {
                string str2 = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
                throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str2, (object) task.ComputerName, (object) task.TaskName));
              }
              string commandText = $"SELECT COUNT(*) FROM {tableName} WHERE {IndexesField.F_TEXT}=:textValue AND {IndexesField.F_LINK_ID}<>:l_ID";
              IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":textValue", (object) key);
              if (Convert.ToInt32(session.DataManager.ExecuteScalar(commandText, dbDataParameter2, dbDataParameter1)) != 0)
                throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_CheckInObject_NotUniqueValue"), (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId));
            }
          }
          else
          {
            ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
            if (task != null)
            {
              string str3 = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
              throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str3, (object) task.ComputerName, (object) task.TaskName));
            }
            int num2 = 0;
            while (num2 < dtData.Rows.Count)
            {
              pars.Clear();
              values.Clear();
              int num3 = dtData.Rows.Count - num2 > maximumInOperands ? maximumInOperands : dtData.Rows.Count - num2;
              int num4 = 0;
              if (!string.IsNullOrEmpty(key))
              {
                num4 = num3 == dtData.Rows.Count ? 0 : 1;
                if (dictionary.ContainsKey(key))
                {
                  dictionary[key].Add(-1L);
                }
                else
                {
                  dictionary.Add(key, new List<long>()
                  {
                    -1L
                  });
                  values.Add(key);
                }
              }
              for (int index = num4; index < num3; ++index)
              {
                DataRow row = dtData.Rows[num2++];
                if (row.RowState != DataRowState.Deleted)
                {
                  string str_to_index = Convert.ToString(row[str1]).MaxStringLength();
                  string indexedString = session.StringNormalizer.GetIndexedString(str_to_index);
                  if (!string.IsNullOrEmpty(indexedString))
                  {
                    long int64 = Convert.ToInt64(row["F_KEY"]);
                    if (dictionary.ContainsKey(indexedString))
                    {
                      dictionary[indexedString].Add(int64);
                    }
                    else
                    {
                      dictionary.Add(indexedString, new List<long>()
                      {
                        int64
                      });
                      values.Add(indexedString);
                    }
                  }
                }
              }
              key = string.Empty;
              if (values.Count != 0)
              {
                pars.Add(dbDataParameter1);
                string paramsRange = this.CreateParamsRange<string>(session.DataManager, values, pars);
                string commandText = $"SELECT * FROM {tableName} WHERE {IndexesField.F_LINK_ID}<>:l_ID AND {IndexesField.F_HASHTEXT} IN {paramsRange}";
                DataTable dtSource = session.DataManager.ExecuteDataTable(commandText, pars.ToArray());
                if (dtSource != null && dtSource.Rows.Count != 0)
                {
                  uIndexes.Add(attrId);
                  this.CopyRowsFromTableToTable(ref dtDistination, dtSource, attrId);
                  foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
                    keys.AddRange((IEnumerable<long>) dictionary[Convert.ToString(row[IndexesField.F_HASHTEXT])]);
                  key = string.Empty;
                }
              }
            }
            foreach (KeyValuePair<string, List<long>> keyValuePair in dictionary)
            {
              if (keyValuePair.Value.Count >= 2)
              {
                uIndexes.Add(attrId);
                keys.AddRange((IEnumerable<long>) keyValuePair.Value);
              }
            }
          }
        }
        keys = keys.Where<long>((System.Func<long, bool>) (x => x > -1L)).Distinct<long>().ToList<long>();
        uIndexes = uIndexes.Distinct<int>().ToList<int>();
      }
    }
    return dtDistination;
  }

  public void UpdateAfterTableRefCheckIn(Guid sessionGuid, long tableRefId, long tableId)
  {
    if (tableRefId == 0L)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_AfterTableRefCheckIn"), (object) Convert.ToString(tableRefId))
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) userSession, tableRefId);
      if (catalogIdByObjectId == 0L)
        throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_UndefinedCatalogIDForTableRef"), (object) userSession.GetObjectInfo(tableRefId).Caption, (object) tableRefId));
      this.CheckBase(userSession, sessionGuid, new List<long>()
      {
        catalogIdByObjectId
      });
      task.CatalogId = catalogIdByObjectId;
      task.Attributes = this.GetIndexes(userSession.DataManager, task, catalogIdByObjectId);
      if (task.Attributes != null)
      {
        if (tableId == 0L)
        {
          this.UpdateAfterCheckInTableRefOnly(userSession, task, tableRefId, tableId);
        }
        else
        {
          IDBObject objectActualCopy = userSession.GetObjectActualCopy(tableId, false);
          if (objectActualCopy == null || objectActualCopy.CheckoutBy == userSession.UserID)
          {
            this.UpdateAfterCheckInTableRefOnly(userSession, task, tableRefId, tableId);
          }
          else
          {
            DataTable dtAttrs;
            DataTable dtData;
            this.GetTables(userSession, tableId, out dtAttrs, out dtData);
            this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dtData);
            if (userSession.DataManager.DataProvider.Name == "Sql")
              this.AddIndexesDataForSql(userSession, task, tableRefId, tableId, dtData, (Action) null);
            else if (userSession.DataManager.DataProvider.Name == "Oracle")
              this.AddIndexesDataForOracle(userSession, task, tableRefId, tableId, dtData, (Action) null);
            else if (userSession.DataManager.DataProvider.Name == "PostgreSQL")
              this.AddIndexesDataForOther(userSession, task, tableRefId, tableId, dtData, (Action) null);
          }
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private void UpdateAfterCheckInTableRefOnly(
    UserSession session,
    ImbaseIndexingService.Task task,
    long tableRefId,
    long tableId)
  {
    IDBObject dbObject = session.GetObject(tableRefId, false);
    if (dbObject == null)
      throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefNull, (object) tableRefId));
    IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) tableRefId);
    IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":tabKey", (object) -1);
    foreach (int attribute in task.Attributes)
    {
      string tableName = ImbaseIndexingService.GenerateTableName(task.CatalogId, attribute);
      IDBAttribute attributeById = dbObject.GetAttributeByID(attribute);
      string str_to_index = attributeById != null ? Convert.ToString(attributeById.Value).MaxStringLength() : string.Empty;
      string commandText1 = $"SELECT COUNT(*) FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:l_ID AND {IndexesField.F_TABKEY}=:tabKey";
      session.StartTransaction();
      try
      {
        object obj = session.DataManager.ExecuteScalar(commandText1, dbDataParameter1, dbDataParameter2);
        if (string.IsNullOrEmpty(str_to_index))
        {
          if (Convert.ToInt32(obj) > 0)
            session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE {IndexesField.F_LINK_ID}=:l_ID AND {IndexesField.F_TABKEY}=:tabKey", dbDataParameter1, dbDataParameter2);
        }
        else if (Convert.ToInt32(obj) > 0)
        {
          string commandText2 = $"UPDATE {tableName} SET {IndexesField.F_TEXT}=:textValue, {IndexesField.F_HASHTEXT}=:hashText WHERE {IndexesField.F_LINK_ID}=:l_ID AND {IndexesField.F_TABKEY}=:tabKey";
          IDbDataParameter dbDataParameter3 = session.DataManager.Parameter(":textValue", (object) str_to_index);
          IDbDataParameter dbDataParameter4 = session.DataManager.Parameter(":hashText", (object) session.StringNormalizer.GetIndexedString(str_to_index));
          session.DataManager.ExecuteNonQuery(commandText2, dbDataParameter3, dbDataParameter4, dbDataParameter1, dbDataParameter2);
        }
        else
        {
          string commandText3 = $"INSERT INTO {tableName} ({IndexesField.F_LINK_ID}, {IndexesField.F_TABLE_ID}, {IndexesField.F_TABKEY}, {IndexesField.F_TEXT}, {IndexesField.F_HASHTEXT}, {IndexesField.F_APPLICABILITY}) VALUES (:l_ID, :t_ID, :tabKey, :textValue, :hashText, 1)";
          IDbDataParameter dbDataParameter5 = session.DataManager.Parameter(":t_ID", (object) tableId);
          IDbDataParameter dbDataParameter6 = session.DataManager.Parameter(":textValue", (object) str_to_index);
          IDbDataParameter dbDataParameter7 = session.DataManager.Parameter(":hashText", (object) session.StringNormalizer.GetIndexedString(str_to_index));
          session.DataManager.ExecuteNonQuery(commandText3, dbDataParameter1, dbDataParameter5, dbDataParameter2, dbDataParameter6, dbDataParameter7);
        }
        session.Commit();
      }
      catch (Exception ex)
      {
        session.Rollback();
        task.Exceptions.Add(new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex));
      }
    }
  }

  public DataTable CheckUniqueBeforeTableCheckIn(
    Guid sessionGuid,
    long tableId,
    out List<int> uIndexes,
    out List<long> keys)
  {
    DataTable dataTable = (DataTable) null;
    keys = new List<long>();
    uIndexes = new List<int>();
    if (tableId != 0L)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      IDBObject objectActualCopy = userSession.GetObjectActualCopy(tableId, false);
      if (objectActualCopy == null)
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableNull, (object) tableId));
      Dictionary<long, List<long>> idsGroupByCatalog = this.GetNotCheckOutTableRefIDsGroupByCatalog((IUserSession) userSession, tableId);
      if (idsGroupByCatalog != null)
      {
        this.CheckBase(userSession, sessionGuid, idsGroupByCatalog.Keys.ToList<long>());
        DataTable dtAttrs1;
        DataTable dtData1;
        this.GetTables(userSession, objectActualCopy.ObjectID, out dtAttrs1, out dtData1);
        Dictionary<int, string> attrData = (Dictionary<int, string>) null;
        Dictionary<long, List<int>> uniqueIndexes = this.GetUniqueIndexes(userSession.DataManager, idsGroupByCatalog.Keys.ToList<long>(), dtAttrs1, ref attrData);
        if (uniqueIndexes != null)
        {
          foreach (long key in uniqueIndexes.Keys)
          {
            if (idsGroupByCatalog[key].Count != 1)
              throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.MultiTableReferences, (object) userSession.GetObjectInfo(key).Caption, (object) key, (object) tableId));
          }
          foreach (KeyValuePair<long, List<int>> keyValuePair in uniqueIndexes)
          {
            DataTable dtAttrs2 = dtAttrs1.Copy();
            DataTable dtData2 = dtData1.Copy();
            DataTable table = this.CheckTableRef(userSession, keyValuePair.Key, idsGroupByCatalog[keyValuePair.Key][0], objectActualCopy.ObjectID, dtAttrs2, dtData2, keyValuePair.Value, attrData, ref uIndexes, ref keys);
            if (table != null)
            {
              if (dataTable == null)
                dataTable = table;
              else
                dataTable.Merge(table);
            }
          }
        }
      }
      keys = keys.Where<long>((System.Func<long, bool>) (x => x > -1L)).Distinct<long>().ToList<long>();
      uIndexes = uIndexes.Distinct<int>().ToList<int>();
    }
    return dataTable;
  }

  private Dictionary<long, List<long>> GetNotCheckOutTableRefIDsGroupByCatalog(
    IUserSession session,
    long tableId)
  {
    Dictionary<long, List<long>> idsGroupByCatalog = (Dictionary<long, List<long>>) null;
    DataTable tableRefIdsByTableId = TableLoadHelper.GetTableRefIDsByTableID(session, Math.Abs(tableId));
    if (tableRefIdsByTableId != null)
    {
      Dictionary<string, List<long>> catalogKeysTableRefIDs = new Dictionary<string, List<long>>(tableRefIdsByTableId.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) tableRefIdsByTableId.Rows)
      {
        long int64 = Convert.ToInt64(row["F_LINK_ID"]);
        IDBObject objectActualCopy = session.GetObjectActualCopy(int64, false);
        if (objectActualCopy != null && objectActualCopy.CheckoutBy != session.UserID)
        {
          string str = Convert.ToString(row["F_KEY"]);
          if (str.Length >= 2)
          {
            string key = str.Substring(0, 2);
            if (catalogKeysTableRefIDs.ContainsKey(key))
              catalogKeysTableRefIDs[key].Add(int64);
            else
              catalogKeysTableRefIDs.Add(key, new List<long>()
              {
                int64
              });
          }
        }
      }
      if (catalogKeysTableRefIDs.Count > 0)
      {
        Dictionary<long, string> idsByClassifKeys = this.GetCatalogIDsByClassifKeys(session, catalogKeysTableRefIDs.Keys.ToList<string>());
        if (idsByClassifKeys != null)
          idsGroupByCatalog = idsByClassifKeys.ToDictionary<KeyValuePair<long, string>, long, List<long>>((System.Func<KeyValuePair<long, string>, long>) (k => k.Key), (System.Func<KeyValuePair<long, string>, List<long>>) (v => catalogKeysTableRefIDs[v.Value]));
      }
    }
    return idsGroupByCatalog;
  }

  private DataTable CheckTableRef(
    UserSession session,
    long catalogId,
    long tableRefId,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    List<int> indexes,
    Dictionary<int, string> attrData,
    ref List<int> uIndexes,
    ref List<long> keys)
  {
    DataTable dtDistination = (DataTable) null;
    Dictionary<string, List<long>> dictionary = new Dictionary<string, List<long>>(dtData.Rows.Count);
    List<string> values = new List<string>();
    int maximumInOperands = session.DataManager.DataProvider.MaximumINOperands;
    this.AssignAttributes(session, tableRefId, tableId, dtAttrs, dtData);
    List<IDbDataParameter> pars = new List<IDbDataParameter>();
    IDbDataParameter dbDataParameter1 = session.DataManager.Parameter(":l_ID", (object) tableRefId);
    IDbDataParameter dbDataParameter2 = session.DataManager.Parameter(":tabKey", (object) -1);
    foreach (int index1 in indexes)
    {
      int attrId = index1;
      dictionary.Clear();
      string tableName = ImbaseIndexingService.GenerateTableName(catalogId, attrId);
      ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
      if (task != null)
      {
        string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
        throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
      }
      int num1 = 0;
      while (num1 < dtData.Rows.Count)
      {
        pars.Clear();
        values.Clear();
        int num2 = dtData.Rows.Count - num1 > maximumInOperands ? maximumInOperands : dtData.Rows.Count - num1;
        for (int index2 = 0; index2 < num2; ++index2)
        {
          DataRow row = dtData.Rows[num1++];
          if (row.RowState != DataRowState.Deleted)
          {
            string str_to_index = Convert.ToString(row[attrData[attrId]]).MaxStringLength();
            string indexedString = session.StringNormalizer.GetIndexedString(str_to_index);
            long int64 = Convert.ToInt64(row["F_KEY"]);
            if (!string.IsNullOrEmpty(indexedString))
            {
              if (dictionary.ContainsKey(indexedString))
              {
                dictionary[indexedString].Add(int64);
              }
              else
              {
                dictionary.Add(indexedString, new List<long>()
                {
                  int64
                });
                values.Add(indexedString);
              }
            }
          }
        }
        if (values.Count != 0)
        {
          pars.Add(dbDataParameter1);
          pars.Add(dbDataParameter2);
          string paramsRange = this.CreateParamsRange<string>(session.DataManager, values, pars);
          string commandText = $"SELECT * FROM {tableName} WHERE ({IndexesField.F_LINK_ID}<>:l_ID OR {IndexesField.F_TABKEY}=:tabKey) AND {IndexesField.F_HASHTEXT} IN {paramsRange}";
          DataTable dtSource = session.DataManager.ExecuteDataTable(commandText, pars.ToArray());
          if (dtSource != null && dtSource.Rows.Count != 0)
          {
            uIndexes.Add(attrId);
            this.CopyRowsFromTableToTable(ref dtDistination, dtSource, attrId);
            foreach (DataRow row in (InternalDataCollectionBase) dtSource.Rows)
              keys.AddRange((IEnumerable<long>) dictionary[Convert.ToString(row[IndexesField.F_HASHTEXT])]);
          }
        }
      }
      foreach (KeyValuePair<string, List<long>> keyValuePair in dictionary)
      {
        if (keyValuePair.Value.Count >= 2)
        {
          uIndexes.Add(attrId);
          keys.AddRange((IEnumerable<long>) keyValuePair.Value);
        }
      }
    }
    return dtDistination;
  }

  public void UpdateAfterTableCheckIn(Guid sessionGuid, long tableId)
  {
    if (tableId == 0L)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DataUpdate_AfterTableCheckIn"), (object) Convert.ToString(tableId))
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      IDBObject objectActualCopy = userSession.GetObjectActualCopy(tableId, false);
      if (objectActualCopy == null)
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableNull, (object) tableId));
      Dictionary<long, List<long>> idsGroupByCatalog = this.GetNotCheckOutTableRefIDsGroupByCatalog((IUserSession) userSession, tableId);
      if (idsGroupByCatalog != null)
      {
        this.CheckBase(userSession, sessionGuid, idsGroupByCatalog.Keys.ToList<long>());
        DataTable dtAttrs;
        DataTable dtData1;
        this.GetTables(userSession, objectActualCopy.ObjectID, out dtAttrs, out dtData1);
        Dictionary<int, string> attrData = (Dictionary<int, string>) null;
        Dictionary<long, List<int>> indexes = this.GetIndexes(userSession.DataManager, idsGroupByCatalog.Keys.ToList<long>(), ref attrData);
        if (indexes != null)
        {
          foreach (KeyValuePair<long, List<int>> keyValuePair in indexes)
          {
            task.CatalogId = keyValuePair.Key;
            task.Attributes = keyValuePair.Value;
            foreach (long tableRefId in idsGroupByCatalog[keyValuePair.Key])
            {
              DataTable dtData2 = dtData1.Copy();
              this.AssignAttributes(userSession, tableRefId, tableId, dtAttrs, dtData2);
              if (userSession.DataManager.DataProvider.Name == "Sql")
                this.AddIndexesDataForSql(userSession, task, tableRefId, tableId, dtData2, (Action) null);
              else if (userSession.DataManager.DataProvider.Name == "Oracle")
                this.AddIndexesDataForOracle(userSession, task, tableRefId, tableId, dtData2, (Action) null);
              else if (userSession.DataManager.DataProvider.Name == "PostgreSQL")
                this.AddIndexesDataForOther(userSession, task, tableRefId, tableId, dtData2, (Action) null);
            }
          }
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private Dictionary<long, List<int>> GetIndexes(
    IDbManager manager,
    List<long> catalogIDs,
    ref Dictionary<int, string> attrData)
  {
    Dictionary<long, List<int>> dictionary = (Dictionary<long, List<int>>) null;
    string[] colsNames = new string[3]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_ATTRIBUTE_STATE
    };
    DataTable indexes = this.GetIndexes(manager, catalogIDs, colsNames);
    if (indexes != null)
    {
      dictionary = new Dictionary<long, List<int>>(indexes.Rows.Count);
      attrData = new Dictionary<int, string>(indexes.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        int attrId = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
        if (!attrData.ContainsKey(attrId))
          attrData.Add(attrId, Convert.ToString((object) MetaDataHelper.GetAttributeTypeGuid(attrId)));
        long catalogId = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
        ImbaseIndexingService.Task task = this.Tasks.FirstOrDefault<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => (x.CatalogId == 0L || x.CatalogId == catalogId) && x.Attributes.Contains(attrId)));
        if (task != null)
        {
          string str = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId);
          throw new IndexingException(string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_ExceptionMsg"), (object) str, (object) task.ComputerName, (object) task.TaskName));
        }
        if (Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]) == Convert.ToInt32((object) IndexesStates.Locked))
          throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) MetaDataHelper.GetAttributeType(attrId).Name, (object) attrId));
        if (!dictionary.ContainsKey(catalogId))
          dictionary.Add(catalogId, new List<int>()
          {
            attrId
          });
        else
          dictionary[catalogId].Add(attrId);
      }
    }
    return dictionary == null || dictionary.Count <= 0 ? (Dictionary<long, List<int>>) null : dictionary;
  }

  public bool CheckBeforeObjectDelete(Guid sessionGuid, long objId, int objTypeId)
  {
    bool flag = true;
    if (objTypeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
    {
      this.CheckBase((UserSession) null, sessionGuid, new List<long>()
      {
        objId
      });
      flag = this.Tasks.Count<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.CatalogId == objId)) == 0;
    }
    return flag;
  }

  public void UpdateAfterObjectDelete(Guid sessionGuid, long objId, int objTypeId)
  {
    if (objId == 0L)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DeleteObject"), (object) Convert.ToString(objId))
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      if (objTypeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
        this.RemoveCatalog(userSession, task, objId);
      else if (objTypeId == Intermech.Imbase.Consts.ImbaseFolderTypeID)
        this.RemoveFolder(userSession, task, objId);
      else if (objTypeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        this.RemoveTableRef(userSession, task, objId);
      else if (objTypeId == Intermech.Imbase.Consts.ImbaseTableTypeID)
        this.RemoveTable(userSession, task, objId);
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  private void RemoveCatalog(UserSession session, ImbaseIndexingService.Task task, long objId)
  {
    this.CheckBase(session, session.SessionGUID, new List<long>()
    {
      objId
    });
    IDbManager dataManager = session.DataManager;
    List<long> catalogIDs = new List<long>();
    catalogIDs.Add(objId);
    string[] colsNames = new string[2]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_TABLE_NAME
    };
    DataTable indexes = this.GetIndexes(dataManager, catalogIDs, colsNames);
    if (indexes == null)
      return;
    task.CatalogId = objId;
    task.Attributes = indexes.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
    foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
    {
      string tableName = Convert.ToString(row[IndexesField.F_TABLE_NAME]);
      this.RemoveTable(session.DataManager, tableName);
      task.Attributes.Remove(Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]));
    }
  }

  private void RemoveFolder(UserSession session, ImbaseIndexingService.Task task, long objId)
  {
    long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) session, objId);
    if (catalogIdByObjectId == 0L)
      return;
    List<long> catalogIDs = new List<long>()
    {
      catalogIdByObjectId
    };
    this.CheckBase(session, session.SessionGUID, catalogIDs);
    DataTable indexes = this.GetIndexes(session.DataManager, catalogIDs, new string[2]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_TABLE_NAME
    });
    if (indexes == null)
      return;
    task.CatalogId = catalogIdByObjectId;
    task.Attributes = indexes.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, objId);
    DataTable tableRefIds = this.GetTableRefIDs(session, classifKeyByObjId, true);
    if (tableRefIds == null || tableRefIds.Rows.Count <= 0)
      return;
    int maximumInOperands = session.DataManager.DataProvider.MaximumINOperands;
    List<object> values = new List<object>();
    session.StartTransaction();
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        string str = Convert.ToString(row[IndexesField.F_TABLE_NAME]);
        int index1 = 0;
        while (index1 < tableRefIds.Rows.Count)
        {
          List<IDbDataParameter> pars = new List<IDbDataParameter>();
          values.Clear();
          int num = tableRefIds.Rows.Count - index1 > maximumInOperands ? maximumInOperands : tableRefIds.Rows.Count - index1;
          for (int index2 = 0; index2 < num; ++index2)
          {
            values.Add(tableRefIds.Rows[index1][IndexesField.F_LINK_ID]);
            ++index1;
          }
          string paramsRange = this.CreateParamsRange<object>(session.DataManager, values, pars);
          session.DataManager.ExecuteNonQuery($"DELETE FROM {str} WHERE {IndexesField.F_LINK_ID} IN {paramsRange}", pars.ToArray());
        }
        task.Attributes.Remove(Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]));
      }
      session.Commit();
    }
    catch (Exception ex)
    {
      session.Rollback();
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Error);
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
  }

  private void RemoveTableRef(UserSession session, ImbaseIndexingService.Task task, long objId)
  {
    long catalogIdByObjectId = TableLoadHelper.GetCatalogIDByObjectID((IUserSession) session, objId);
    if (catalogIdByObjectId == 0L)
      return;
    List<long> catalogIDs = new List<long>()
    {
      catalogIdByObjectId
    };
    this.CheckBase(session, session.SessionGUID, catalogIDs);
    DataTable indexes = this.GetIndexes(session.DataManager, catalogIDs, new string[2]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_TABLE_NAME
    });
    if (indexes == null)
      return;
    session.StartTransaction();
    try
    {
      task.CatalogId = catalogIdByObjectId;
      task.Attributes = indexes.AsEnumerable().Select<DataRow, int>((System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToList<int>();
      IDbDataParameter dbDataParameter = session.DataManager.Parameter(":l_ID", (object) objId);
      foreach (DataRow row in (InternalDataCollectionBase) indexes.Rows)
      {
        string str = Convert.ToString(row[IndexesField.F_TABLE_NAME]);
        session.DataManager.ExecuteNonQuery($"DELETE FROM {str} WHERE {IndexesField.F_LINK_ID}=:l_ID", dbDataParameter);
        task.Attributes.Remove(Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]));
      }
      session.Commit();
    }
    catch (Exception ex)
    {
      session.Rollback();
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Error);
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
  }

  private void RemoveTable(UserSession session, ImbaseIndexingService.Task task, long objId)
  {
    Dictionary<long, List<long>> idsGroupByCatalog = this.GetTableRefIDsGroupByCatalog((IUserSession) session, objId);
    if (idsGroupByCatalog == null)
      return;
    List<long> list = idsGroupByCatalog.Keys.ToList<long>();
    this.CheckBase(session, session.SessionGUID, list);
    DataTable indexes = this.GetIndexes(session.DataManager, list, new string[2]
    {
      IndexesField.F_CATALOG_ID,
      IndexesField.F_ATTRIBUTE_ID
    });
    if (indexes == null)
      return;
    Dictionary<long, List<int>> dictionary = indexes.AsEnumerable().GroupBy<DataRow, long, int>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID])), (System.Func<DataRow, int>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]))).ToDictionary<IGrouping<long, int>, long, List<int>>((System.Func<IGrouping<long, int>, long>) (x => x.Key), (System.Func<IGrouping<long, int>, List<int>>) (y => y.ToList<int>()));
    session.StartTransaction();
    try
    {
      foreach (KeyValuePair<long, List<int>> keyValuePair in dictionary)
      {
        task.CatalogId = keyValuePair.Key;
        task.Attributes = new List<int>((IEnumerable<int>) keyValuePair.Value);
        List<long> values = idsGroupByCatalog[keyValuePair.Key];
        IDbDataParameter[] dbDataParameterArray;
        string str;
        if (values.Count > 1)
        {
          List<IDbDataParameter> pars = new List<IDbDataParameter>();
          string paramsRange = this.CreateParamsRange<long>(session.DataManager, values, pars);
          dbDataParameterArray = pars.ToArray();
          str = $"WHERE {IndexesField.F_LINK_ID} IN {paramsRange}";
        }
        else
        {
          dbDataParameterArray = new IDbDataParameter[1]
          {
            session.DataManager.Parameter(":l_ID", (object) values[0])
          };
          str = $"WHERE {IndexesField.F_LINK_ID}=:l_ID";
        }
        foreach (int attrId in keyValuePair.Value)
        {
          string tableName = ImbaseIndexingService.GenerateTableName(keyValuePair.Key, attrId);
          session.DataManager.ExecuteNonQuery($"DELETE FROM {tableName} {str}", dbDataParameterArray);
          task.Attributes.Remove(attrId);
        }
      }
      session.Commit();
    }
    catch (Exception ex)
    {
      session.Rollback();
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Error);
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
  }

  public bool CheckBeforeAttributeDelete(Guid sessionGuid, int attrId)
  {
    bool flag = false;
    try
    {
      if (this.Tasks.Count<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.Attributes.Contains(attrId))) == 0)
      {
        UserSession userSession = this.GetUserSession(sessionGuid);
        string commandText = $"SELECT COUNT(*) FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_ATTRIBUTE_ID}=:a_ID AND {IndexesField.F_ATTRIBUTE_STATE}=:parState";
        flag = Convert.ToInt32(userSession.DataManager.ExecuteScalar(commandText, userSession.DataManager.Parameter(":a_ID", (object) attrId), userSession.DataManager.Parameter(":parState", (object) 0))) <= 0;
      }
    }
    catch (Exception ex)
    {
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    return flag;
  }

  public void UpdateAfterAttributeDelete(Guid sessionGuid, int attrId)
  {
    if (attrId == 0)
      return;
    ImbaseIndexingService.Task task = new ImbaseIndexingService.Task(Guid.NewGuid())
    {
      Attributes = new List<int>() { attrId },
      TaskName = string.Format(LocalizationHolder.rm.GetString("Imbase_Indexing_DeleteAttribute"), (object) Convert.ToString(attrId))
    };
    this.Tasks.Add(task);
    Monitor.Enter(ImbaseIndexingService._lock);
    try
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      task.ComputerName = userSession.ComputerName;
      string commandText = $"SELECT {IndexesField.F_CATALOG_ID}, {IndexesField.F_TABLE_NAME} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_ATTRIBUTE_ID}=:a_ID";
      DataTable source = userSession.DataManager.ExecuteDataTable(commandText, userSession.DataManager.Parameter(":a_ID", (object) attrId));
      if (source != null && source.Rows.Count > 0)
      {
        List<long> list = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID]))).Distinct<long>().ToList<long>();
        this.CheckBase(userSession, userSession.SessionGUID, list);
        foreach (DataRow row in (InternalDataCollectionBase) source.Rows)
        {
          string tableName = Convert.ToString(row[IndexesField.F_TABLE_NAME]);
          this.RemoveTable(userSession.DataManager, tableName);
        }
      }
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Terminated);
    }
    catch (Exception ex)
    {
      this.SetTaskCompleted(task, ImbaseIndexingService.TaskState.Error);
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.DataBaseException, ex);
    }
    finally
    {
      Monitor.Exit(ImbaseIndexingService._lock);
      this.RemoveAfterComplete(task);
    }
  }

  public DataTable GetIndexes(Guid sessionGuid, long sourceId, string[] colsNames)
  {
    UserSession userSession = this.GetUserSession(sessionGuid);
    if (sourceId != -1L)
      this.CheckBase(userSession, userSession.SessionGUID, new List<long>()
      {
        sourceId
      });
    IDbManager dataManager = userSession.DataManager;
    List<long> catalogIDs = new List<long>();
    catalogIDs.Add(sourceId);
    string[] colsNames1 = colsNames;
    return this.GetIndexes(dataManager, catalogIDs, colsNames1);
  }

  public DataTable GetIndexes(Guid sessionGuid, List<long> catalogIDs, string[] colsNames = null)
  {
    UserSession userSession = this.GetUserSession(sessionGuid);
    if (catalogIDs != null && catalogIDs.Count > 0)
      this.CheckBase(userSession, userSession.SessionGUID, catalogIDs);
    return this.GetIndexes(userSession.DataManager, catalogIDs == null || catalogIDs.Count <= 0 ? (List<long>) null : catalogIDs, colsNames);
  }

  private DataTable GetIndexes(IDbManager manager, List<long> catalogIDs, string[] colsNames = null)
  {
    List<IDbDataParameter> pars = new List<IDbDataParameter>(0);
    string str1 = colsNames == null || colsNames.Length == 0 ? "*" : string.Join(", ", colsNames);
    string str2 = string.Empty;
    if (catalogIDs != null && catalogIDs.Count > 0)
    {
      if (catalogIDs.Count == 1)
      {
        if (catalogIDs[0] != -1L)
        {
          pars = new List<IDbDataParameter>()
          {
            manager.Parameter(":c_ID", (object) catalogIDs[0])
          };
          str2 = $"WHERE {IndexesField.F_CATALOG_ID}=:c_ID";
        }
      }
      else
      {
        pars = new List<IDbDataParameter>(catalogIDs.Count);
        str2 = $"WHERE {IndexesField.F_CATALOG_ID} IN {this.CreateParamsRange<long>(manager, catalogIDs, pars)}";
      }
    }
    else if (colsNames != null && colsNames.Length == 1 && colsNames[0] == IndexesField.F_ATTRIBUTE_ID)
    {
      pars = new List<IDbDataParameter>(0);
      str1 = "DISTINCT " + str1;
    }
    string commandText = $"SELECT {str1} FROM IMS_IMBASE_INDEXES {str2}";
    DataTable dataTable = manager.ExecuteDataTable(commandText, pars.ToArray());
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  public DataTable GetUniqueIndexes(Guid sessionGuid, List<long> catalogIDs, string[] colsNames = null)
  {
    UserSession userSession = this.GetUserSession(sessionGuid);
    if (catalogIDs != null && catalogIDs.Count > 0)
      this.CheckBase(userSession, userSession.SessionGUID, catalogIDs);
    return this.GetUniqueIndexes(userSession.DataManager, catalogIDs, colsNames);
  }

  private DataTable GetUniqueIndexes(IDbManager manager, List<long> catalogIDs, string[] colsNames = null)
  {
    DataTable dataTable = (DataTable) null;
    if (catalogIDs != null && catalogIDs.Count > 0)
    {
      string str1 = colsNames == null || colsNames.Length == 0 ? "*" : string.Join(", ", colsNames);
      List<IDbDataParameter> pars;
      string str2;
      if (catalogIDs.Count == 1)
      {
        pars = new List<IDbDataParameter>()
        {
          manager.Parameter(":c_ID", (object) catalogIDs[0])
        };
        str2 = $"WHERE {IndexesField.F_CATALOG_ID}=:c_ID";
      }
      else
      {
        pars = new List<IDbDataParameter>(catalogIDs.Count + 1);
        str2 = $"WHERE {IndexesField.F_CATALOG_ID} IN {this.CreateParamsRange<long>(manager, catalogIDs, pars)}";
      }
      string commandText = $"SELECT {str1} FROM IMS_IMBASE_INDEXES {str2}";
      EnumerableRowCollection<DataRow> source = manager.ExecuteDataTable(commandText, pars.ToArray()).AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row =>
      {
        int int32 = Convert.ToInt32(row[IndexesField.F_FLAG]);
        return int32 != -1 && ((IndexesFlags) int32).HasFlag((Enum) IndexesFlags.UniqueValue);
      }));
      if (source.Any<DataRow>())
        dataTable = source.CopyToDataTable<DataRow>();
    }
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  private int DisabledRecord(DataRow row, int applicabilityColumnIndex)
  {
    if (applicabilityColumnIndex == -1)
      return 1;
    string str = row[applicabilityColumnIndex]?.ToString();
    return string.IsNullOrEmpty(str) || str[0] != '-' ? 1 : 0;
  }

  private void AssignAttributes(
    UserSession session,
    long tableRefId,
    long tableId,
    DataTable dtAttrs,
    DataTable dtData,
    Dictionary<int, object> values = null,
    bool ignoreTableAttr = false)
  {
    try
    {
      ImbaseKeyInfo keyInfo = new ImbaseKeyInfo(-1L);
      AttributeTypeProperties[] columnsAttributes;
      if (values != null)
        TableLoadHelper.AssignAttributes((IUserSession) session, tableRefId, tableId, dtData, dtAttrs, out columnsAttributes, new List<CalculatedColumn>(), ref keyInfo, values, ignoreTableAttr);
      else
        TableLoadHelper.AssignAttributes((IUserSession) session, tableRefId, tableId, dtData, dtAttrs, out columnsAttributes, new List<CalculatedColumn>(), ref keyInfo);
    }
    catch (Exception ex)
    {
      throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.AssignAttrsError, (object) session.GetObjectInfo(tableRefId).Caption, (object) tableRefId), ex);
    }
  }

  private List<int> CheckAttributes(
    UserSession session,
    ImbaseIndexingService.Task currentTask,
    List<int> attributes)
  {
    List<int> intList = new List<int>(attributes.Count);
    List<ImbaseIndexingService.Task> list1 = this.Tasks.Where<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x =>
    {
      if (x.CatalogId == 0L)
        return true;
      return x.CatalogId == currentTask.CatalogId && x.TaskGuid != currentTask.TaskGuid;
    })).ToList<ImbaseIndexingService.Task>();
    foreach (int attribute in attributes)
    {
      int attrId = attribute;
      IDBAttributeType attributeType = session.GetAttributeType(attrId, false);
      if (attributeType == null)
      {
        currentTask.Exceptions.Add(new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.NullAttribute, (object) attrId)));
      }
      else
      {
        List<ImbaseIndexingService.Task> list2 = list1.Where<ImbaseIndexingService.Task>((System.Func<ImbaseIndexingService.Task, bool>) (x => x.Attributes.Contains(attrId))).ToList<ImbaseIndexingService.Task>();
        if (list2.Count > 0)
        {
          string msg = string.Format(ImbaseIndexingService.ExceptionsMessages.BusyAttribute, (object) attributeType.Name, (object) attrId);
          foreach (ImbaseIndexingService.Task task in list2)
            currentTask.Exceptions.Add(new IndexingException(msg)
            {
              ComputerName = task.ComputerName,
              TaskName = task.TaskName
            });
        }
        else
          intList.Add(attrId);
      }
    }
    return intList;
  }

  private string CreateParamsRange<T>(
    IDbManager manager,
    List<T> values,
    List<IDbDataParameter> pars)
  {
    string[] strArray = new string[values.Count];
    for (int index = 0; index < values.Count; ++index)
    {
      string parameterName = $":par{index}";
      pars.Add(manager.Parameter(parameterName, (object) values[index]));
      strArray[index] = parameterName;
    }
    return $"({string.Join(", ", strArray)})";
  }

  private DataTable GetTableRefIDs(UserSession session, string classifKey, bool allowNull = false)
  {
    DataTable tableRefData = TableLoadHelper.GetTableRefData((IUserSession) session, classifKey);
    if (tableRefData != null)
    {
      tableRefData.Columns[0].ColumnName = IndexesField.F_LINK_ID;
      tableRefData.Columns[1].ColumnName = IndexesField.F_TABLE_ID;
      tableRefData.Columns[2].ColumnName = IndexesField.F_CLASSIF_KEY;
    }
    else if (!allowNull)
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.TableRefListEmpty);
    return tableRefData;
  }

  private long GetTables(
    UserSession session,
    long objId,
    out DataTable dtAttrs,
    out DataTable dtData)
  {
    QuickObjectInfo objectInfo = session.GetObjectInfo(objId);
    dtAttrs = (DataTable) null;
    dtData = (DataTable) null;
    if (objectInfo.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
    {
      IDBObject dbObject;
      try
      {
        dbObject = session.GetObject(objId, false);
        if (dbObject == null)
          throw new Exception();
      }
      catch (Exception ex)
      {
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefNull, (object) objId), ex);
      }
      try
      {
        objId = (dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID) ?? throw new Exception()).AsInteger;
      }
      catch (Exception ex)
      {
        throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.TableRefAttrNull, (object) dbObject.Caption, (object) objId), ex);
      }
    }
    try
    {
      if (objId != 0L)
      {
        DataSet tablesInternal = TableLoadHelper.GetTablesInternal((IUserSession) session, objId, false);
        dtAttrs = tablesInternal != null && tablesInternal.Tables.Contains("IMS_ATTR_TYPES") && tablesInternal.Tables.Contains("IMS_DATA") ? tablesInternal.Tables["IMS_ATTR_TYPES"] : throw new Exception();
        dtData = tablesInternal.Tables["IMS_DATA"];
      }
    }
    catch (Exception ex)
    {
      throw new IndexingException(string.Format(ImbaseIndexingService.ExceptionsMessages.ImbaseTableNull, (object) session.GetObjectInfo(objId).Caption, (object) objId), ex);
    }
    return objId;
  }

  private UserSession GetUserSession(Guid sessionGuid, string sessionName = "", bool cloneSession = false)
  {
    if (!(ImbaseServer.GetSession(sessionGuid) is UserSession userSession))
      throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.NullSession);
    if (cloneSession)
      userSession = userSession.Clone(sessionName) as UserSession;
    return userSession == null || userSession.DataManager != null ? userSession : throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.NullDbManager);
  }

  public bool FindByIndex(
    Guid sessionGuid,
    int attrId,
    string request,
    out long tableRefId,
    out long recId)
  {
    return this.FindByIndex(sessionGuid, -1L, attrId, request, out tableRefId, out recId);
  }

  public bool FindByIndex(
    Guid sessionGuid,
    long catalogId,
    int attrId,
    string request,
    out long tableRefId,
    out long recId)
  {
    bool byIndex = false;
    recId = -1L;
    tableRefId = 0L;
    catalogId = catalogId == 0L ? -1L : catalogId;
    DataTable indexes = this.GetIndexes(sessionGuid, new List<long>()
    {
      catalogId
    }, (string[]) null);
    if (indexes == null || indexes.Rows.Count == 0)
      throw new IndexNotFoundException(LocalizationHolder.rm.GetString("Imbase_Indexing_IndexesNotFound"));
    DataRow[] dataRowArray = indexes.Select($"{IndexesField.F_ATTRIBUTE_ID}='{attrId}'");
    if (dataRowArray == null || dataRowArray.Length == 0)
      throw new IndexNotFoundException(LocalizationHolder.rm.GetString("Imbase_Indexing_IndexNotFound"));
    Guid sessionGuid1 = sessionGuid;
    List<long> catalogIDs;
    if (catalogId == -1L)
    {
      catalogIDs = (List<long>) null;
    }
    else
    {
      catalogIDs = new List<long>();
      catalogIDs.Add(catalogId);
    }
    int attrId1 = attrId;
    string request1 = request;
    DataTable dataTable = this.Search(sessionGuid1, catalogIDs, attrId1, (string[]) null, request1, SearchesAccuracy.Exact);
    if (dataTable.Rows.Count > 0)
    {
      if (dataTable.Columns.Contains(IndexesField.F_LINK_ID))
        tableRefId = Convert.ToInt64(dataTable.Rows[0][IndexesField.F_LINK_ID]);
      if (dataTable.Columns.Contains(IndexesField.F_TABKEY))
        recId = Convert.ToInt64(dataTable.Rows[0][IndexesField.F_TABKEY]);
      byIndex = tableRefId != 0L && recId > -1L;
    }
    return byIndex;
  }

  [Obsolete("Использовать функцию DataTable Search(Guid sessionGuid, List<Int64> catalogIDs, Int32 attrID, string[] colsNames, string request, SearchesAccuracy sa)")]
  public DataTable Search(
    Guid sessionGuid,
    long catalogId,
    int attrId,
    string[] colsNames,
    string request,
    SearchesAccuracy sa)
  {
    List<long> longList;
    if (catalogId == 0L || catalogId == -1L)
      longList = (List<long>) null;
    else
      longList = new List<long>() { catalogId };
    List<long> catalogIDs = longList;
    return this.Search(sessionGuid, catalogIDs, attrId, colsNames, request, sa);
  }

  public DataTable Search(
    Guid sessionGuid,
    List<long> catalogIDs,
    int attrId,
    string[] colsNames,
    string request,
    SearchesAccuracy sa)
  {
    DataTable dataTable = new DataTable();
    bool securityCheckForIndexes = this._imbaseParamsService.CommonParams.UseExtendedSecurityCheckForIndexes;
    if (attrId != 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      IDbDataParameter dbDataParameter = userSession.DataManager.Parameter(":a_ID", (object) attrId);
      DataTable dt = userSession.DataManager.ExecuteDataTable($"SELECT {IndexesField.F_CATALOG_ID} FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_ATTRIBUTE_ID}=:a_ID", dbDataParameter);
      if (dt != null && dt.Rows.Count > 0)
      {
        List<long> ds = catalogIDs;
        catalogIDs = catalogIDs == null ? dt.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID]))).ToList<long>() : dt.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => ds.Contains(Convert.ToInt64(x[IndexesField.F_CATALOG_ID])))).Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[IndexesField.F_CATALOG_ID]))).ToList<long>();
      }
      else
        catalogIDs = (List<long>) null;
      if (catalogIDs != null && catalogIDs.Count > 0)
      {
        this.CheckBase(userSession, userSession.SessionGUID, catalogIDs);
        string str1 = colsNames == null || colsNames.Length == 0 ? "*" : string.Join(", ", colsNames);
        string format = string.Empty;
        switch (sa)
        {
          case SearchesAccuracy.Start:
            format = $"WHERE {IndexesField.F_HASHTEXT} LIKE '{{0}}%'";
            break;
          case SearchesAccuracy.Сontain:
            format = $"WHERE {IndexesField.F_HASHTEXT} LIKE '%{{0}}%'";
            break;
          case SearchesAccuracy.End:
            format = $"WHERE {IndexesField.F_HASHTEXT} LIKE '%{{0}}'";
            break;
          case SearchesAccuracy.Exact:
            format = $"WHERE {IndexesField.F_HASHTEXT}='{{0}}'";
            break;
          case SearchesAccuracy.Template:
            format = $"WHERE {IndexesField.F_HASHTEXT} LIKE '{{0}}'";
            request = request.Replace('*', '%');
            request = request.Replace('?', '_');
            break;
        }
        string str2 = $"AND {IndexesField.F_APPLICABILITY}=1";
        request = userSession.StringNormalizer.GetIndexedString(request);
        request = request.Replace("'", "''");
        string str3 = string.Format(format, (object) request);
        foreach (long catalogId in catalogIDs)
        {
          string tableName = ImbaseIndexingService.GenerateTableName(catalogId, attrId);
          ImbaseIndexSecurity imbaseIndexSecurity = new ImbaseIndexSecurity(userSession, catalogId, attrId);
          string str4;
          if (!imbaseIndexSecurity.CheckAccess(ActionType.ShowNonApplicabilityImbaseRecords, false, false))
            str4 = $"SELECT {str1} FROM {tableName} {str3} {str2}";
          else
            str4 = $"SELECT {str1} FROM {tableName} {str3}";
          string commandText = str4;
          dt = userSession.DataManager.ExecuteDataTable(commandText);
          DataColumn column = new DataColumn(IndexesField.F_CATALOG_ID)
          {
            DefaultValue = (object) catalogId
          };
          dt.Columns.Add(column);
          if (securityCheckForIndexes)
          {
            if (!imbaseIndexSecurity.CheckAccess(ActionType.ShowNonVisibleColumnImbaseRecords, false, false))
            {
              List<long> list = dt.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[IndexesField.F_TABLE_ID]))).Distinct<long>().ToList<long>();
              if (list.Count != 0)
              {
                List<long> denyTableIds = new List<long>();
                long num = list.Min();
                long tableId1 = list.Max();
                long categoryId1 = ImbaseHelper.CreateCategoryId(num, (long) attrId);
                long recordId = (long) attrId;
                long categoryId2 = ImbaseHelper.CreateCategoryId(tableId1, recordId);
                DBObject table = userSession.GetObject(num) as DBObject;
                ImbaseAttSecurity imbaseAttSecurity = new ImbaseAttSecurity(userSession, table, attrId);
                if (imbaseAttSecurity.LoadCacheTable(ActionType.View, categoryId1, categoryId2) > 0)
                {
                  foreach (long tableId2 in list)
                  {
                    imbaseAttSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(tableId2, (long) attrId));
                    if (!imbaseAttSecurity.CheckAccess(ActionType.View, true, false))
                      denyTableIds.Add(tableId2);
                  }
                  dt.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => denyTableIds.Contains(Convert.ToInt64(row[IndexesField.F_TABLE_ID])))).ToList<DataRow>().ForEach((Action<DataRow>) (r => dt.Rows.Remove(r)));
                  dt.AcceptChanges();
                }
              }
            }
            bool flag1 = imbaseIndexSecurity.CheckAccess(ActionType.ShowNonVisibleRowImbaseRecords, false, false);
            bool flag2 = imbaseIndexSecurity.CheckAccess(ActionType.ShowNonUseImbaseRecords, false, false);
            if (!flag1 || !flag2)
            {
              List<\u003C\u003Ef__AnonymousType1<long, List<int>>> list = dt.AsEnumerable().Select(row => new
              {
                Tableid = Convert.ToInt64(row[IndexesField.F_TABLE_ID]),
                RecordKey = Convert.ToInt32(row[IndexesField.F_TABKEY])
              }).GroupBy(key => key.Tableid, g => g.RecordKey, (key, g) => new
              {
                TableId = key,
                RecordIds = g.ToList<int>()
              }).ToList();
              if (list.Count != 0)
              {
                List<Tuple<long, int>> deniedRows = new List<Tuple<long, int>>();
                long minTableId = list.Select(x => x.TableId).Min();
                long maxTableId = list.Select(x => x.TableId).Max();
                int recordId1 = list.Where(k => k.TableId == minTableId).SelectMany(x => (IEnumerable<int>) x.RecordIds).Min();
                int recordId2 = list.Where(k => k.TableId == maxTableId).SelectMany(x => (IEnumerable<int>) x.RecordIds).Max();
                long categoryId3 = ImbaseHelper.CreateCategoryId(minTableId, (long) recordId1);
                long categoryId4 = ImbaseHelper.CreateCategoryId(maxTableId, (long) recordId2);
                DBObject table = userSession.GetObject(minTableId) as DBObject;
                if (!flag1)
                {
                  ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(userSession, table, (long) recordId1);
                  if (imbaseRecordSecurity.LoadCacheTable(ActionType.View, categoryId3, categoryId4) > 0)
                  {
                    foreach (var data in list)
                    {
                      foreach (int recordId3 in data.RecordIds)
                      {
                        imbaseRecordSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(data.TableId, (long) recordId3));
                        if (!imbaseRecordSecurity.CheckAccess(ActionType.View, true, false))
                          deniedRows.Add(new Tuple<long, int>(data.TableId, recordId3));
                      }
                    }
                  }
                }
                if (!flag2)
                {
                  ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(userSession, table, (long) recordId1);
                  if (imbaseRecordSecurity.LoadCacheTable(ActionType.Use, categoryId3, categoryId4) > 0)
                  {
                    foreach (var data in list)
                    {
                      foreach (int recordId4 in data.RecordIds)
                      {
                        imbaseRecordSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(data.TableId, (long) recordId4));
                        if (!imbaseRecordSecurity.CheckAccess(ActionType.Use, true, false))
                          deniedRows.Add(new Tuple<long, int>(data.TableId, recordId4));
                      }
                    }
                  }
                }
                dt.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => deniedRows.Contains(new Tuple<long, int>(Convert.ToInt64(row[IndexesField.F_TABLE_ID]), Convert.ToInt32(row[IndexesField.F_TABKEY]))))).Distinct<DataRow>().ToList<DataRow>().ForEach((Action<DataRow>) (r => dt.Rows.Remove(r)));
                dt.AcceptChanges();
              }
            }
          }
          try
          {
            dataTable.Merge(dt);
          }
          catch (Exception ex)
          {
            $"Search error for catalogs \"{string.Join<long>(",", (IEnumerable<long>) catalogIDs)}\" current catalog \"{catalogId}\" attributeId {attrId}: {ex.Message}";
          }
        }
        dataTable.RemotingFormat = SerializationFormat.Binary;
      }
    }
    return dataTable;
  }

  public DataTable Search(
    Guid sessionGuid,
    long catalogId,
    int attrId,
    long objId,
    string[] colsNames,
    string request,
    SearchesAccuracy sa)
  {
    DataTable dataTable = new DataTable();
    if (catalogId != 0L && objId != 0L)
    {
      List<long> longList;
      if (catalogId == -1L)
      {
        longList = (List<long>) null;
      }
      else
      {
        longList = new List<long>();
        longList.Add(catalogId);
      }
      List<long> catalogIDs = longList;
      if (!((IEnumerable<string>) colsNames).Contains<string>(IndexesField.F_LINK_ID))
      {
        List<string> stringList = new List<string>(colsNames.Length + 1);
        stringList.AddRange((IEnumerable<string>) colsNames);
        stringList.Add(IndexesField.F_LINK_ID);
        colsNames = stringList.ToArray();
      }
      DataTable source = this.Search(sessionGuid, catalogIDs, attrId, colsNames, request, sa);
      if (source.Rows.Count > 0)
      {
        UserSession userSession = this.GetUserSession(sessionGuid);
        IDBObjectCollection objectCollection = userSession.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
        if (objectCollection == null)
          throw new IndexingException(ImbaseIndexingService.ExceptionsMessages.TableRefListEmpty);
        string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) userSession, objId);
        ColumnDescriptor columnDescriptor = new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, LogicalOperators.NONE, 0, false)
        }, new ColumnDescriptor[1]{ columnDescriptor });
        List<long> tableRefIDs = objectCollection.Select(paramSet).AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
        foreach (DataRow row in source.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => !tableRefIDs.Contains(Convert.ToInt64(x[IndexesField.F_LINK_ID])))).Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToList<DataRow>())
          source.Rows.Remove(row);
        source.AcceptChanges();
      }
      dataTable = source;
    }
    dataTable.RemotingFormat = SerializationFormat.Binary;
    return dataTable;
  }

  public DataTable GetNotUniqueValues(Guid sessionGuid, long catalogId, int attrId)
  {
    DataTable notUniqueValues = new DataTable();
    if (catalogId != 0L && attrId != 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      this.CheckBase(userSession, userSession.SessionGUID, new List<long>()
      {
        catalogId
      });
      string tableName = ImbaseIndexingService.GenerateTableName(catalogId, attrId);
      notUniqueValues = userSession.DataManager.ExecuteDataTable(string.Format("SELECT T1.F_TEXT, T1.F_LINK_ID, T1.F_TABLE_ID, T1.F_TABKEY, T1.F_HASHTEXT FROM {0} T1 INNER JOIN ( SELECT F_HASHTEXT FROM {0} GROUP BY F_HASHTEXT HAVING COUNT(*) > 1 ) T2 ON T1.F_HASHTEXT = T2.F_HASHTEXT", (object) tableName));
      if (notUniqueValues.Rows.Count > 0)
      {
        List<long> longList = new List<long>(1024 /*0x0400*/);
        foreach (DataRow row in (InternalDataCollectionBase) notUniqueValues.Rows)
        {
          long int64 = Convert.ToInt64(row[IndexesField.F_LINK_ID]);
          int num = longList.BinarySearch(int64);
          if (num < 0)
            longList.Insert(~num, int64);
        }
        DataTable foldersForObjects = ImbaseServer.Instance.GetFoldersForObjects(userSession.SessionGUID, longList.ToArray(), new long[1]
        {
          catalogId
        });
        notUniqueValues.BeginLoadData();
        notUniqueValues.Columns.Add(IndexesField.F_FULL_PATH, typeof (string));
        EnumerableRowCollection<DataRow> source = foldersForObjects.AsEnumerable();
        Dictionary<long, string> dictionary = new Dictionary<long, string>(longList.Count);
        foreach (DataRow row1 in (InternalDataCollectionBase) notUniqueValues.Rows)
        {
          long linkid = Convert.ToInt64(row1[IndexesField.F_LINK_ID]);
          string str;
          if (dictionary.ContainsKey(linkid))
          {
            str = dictionary[linkid];
          }
          else
          {
            DataRow row2 = source.FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (r => Convert.ToInt64(r["F_OBJECT_ID"]) == linkid));
            string classKey = row2 != null ? row2.Field<string>("F_PATH") : (string) null;
            if (!string.IsNullOrEmpty(classKey))
            {
              str = string.Join<object>("\\", (IEnumerable<object>) source.Where<DataRow>((System.Func<DataRow, bool>) (r => classKey.Contains(Convert.ToString(r["F_PATH"])))).OrderBy<DataRow, object>((System.Func<DataRow, object>) (r => r["F_PATH"])).Select<DataRow, object>((System.Func<DataRow, object>) (r => r["CAPTION"])).ToList<object>());
              dictionary.Add(linkid, str);
            }
            else
              continue;
          }
          row1[IndexesField.F_FULL_PATH] = (object) str;
        }
        notUniqueValues.EndLoadData();
        notUniqueValues.AcceptChanges();
      }
    }
    notUniqueValues.RemotingFormat = SerializationFormat.Binary;
    return notUniqueValues;
  }

  public DataTable QuickSearch(
    Guid sessionGuid,
    List<long> catalogIDs,
    string request,
    DataTable dtFilter,
    int recordCount)
  {
    DataTable dataTable1 = (DataTable) null;
    if (catalogIDs != null && catalogIDs.Count > 0 && request.Length > 2 && recordCount > 0)
    {
      UserSession userSession = this.GetUserSession(sessionGuid);
      DataTable indexes = this.GetIndexes(userSession.DataManager, catalogIDs, new string[3]
      {
        IndexesField.F_CATALOG_ID,
        IndexesField.F_TABLE_NAME,
        IndexesField.F_ATTRIBUTE_ID
      });
      if (indexes != null)
      {
        int num = recordCount;
        foreach (IGrouping<long, Tuple<string, int>> grouping in (Lookup<long, Tuple<string, int>>) indexes.AsEnumerable().ToLookup<DataRow, long, Tuple<string, int>>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0])), (System.Func<DataRow, Tuple<string, int>>) (x => new Tuple<string, int>(Convert.ToString(x[1]), Convert.ToInt32(x[2])))))
        {
          if (num != 0)
          {
            List<long> visibleObjectIds = this.GetVisibleObjectIDs(sessionGuid, grouping.Key);
            foreach (Tuple<string, int> tuple in (IEnumerable<Tuple<string, int>>) grouping)
            {
              if (num != 0)
              {
                string tableName = tuple.Item1;
                int attrId = tuple.Item2;
                DataTable dataTable2 = this.QuickSearch(userSession, tableName, grouping.Key, attrId, request, dtFilter, 0);
                if (dataTable2 != null)
                {
                  dataTable1 = dataTable1 ?? dataTable2.Clone();
                  foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
                  {
                    if (num != 0)
                    {
                      if (visibleObjectIds == null || visibleObjectIds.BinarySearch(Convert.ToInt64(row[IndexesField.F_LINK_ID])) >= 0)
                      {
                        dataTable1.Rows.Add(row.ItemArray);
                        --num;
                      }
                    }
                    else
                      break;
                  }
                }
              }
              else
                break;
            }
          }
          else
            break;
        }
      }
    }
    return dataTable1;
  }

  private DataTable QuickSearch(
    UserSession session,
    string tableName,
    long catalogId,
    int attrId,
    string request,
    DataTable dtFilter,
    int recordCount)
  {
    IDbManager dataManager = session.DataManager;
    string str1 = string.Empty;
    string str2 = string.Empty;
    string str3 = $"AND {IndexesField.F_APPLICABILITY}=1";
    ImbaseIndexSecurity imbaseIndexSecurity = new ImbaseIndexSecurity(session, catalogId, attrId);
    bool flag1 = imbaseIndexSecurity.CheckAccess(ActionType.ShowNonApplicabilityImbaseRecords, false, false);
    string str4;
    if (dataManager.DataProvider.Name == "Sql")
    {
      str1 = recordCount > 0 ? $" TOP {Convert.ToString(recordCount)}" : string.Empty;
      str4 = "@searchString";
    }
    else if (dataManager.DataProvider.Name == "Oracle")
    {
      str2 = recordCount > 0 ? $" AND ROWNUM < {Convert.ToString(recordCount)}" : string.Empty;
      str4 = ":searchString";
    }
    else
    {
      str2 = recordCount > 0 ? $" LIMIT {Convert.ToString(recordCount)}" : string.Empty;
      str4 = "@searchString";
    }
    string format;
    if (dtFilter == null || dtFilter.Rows.Count == 0)
    {
      format = $" SELECT  {{0}}  tbl_idx.*  FROM {tableName} tbl_idx  WHERE  tbl_idx.F_HASHTEXT LIKE {str4} {{1}} {{2}}";
    }
    else
    {
      string str5 = this.QuickSearch_BuildPathCond("tbl_path.F_STRING_VALUE", dtFilter);
      format = $" SELECT  {{0}}  tbl_idx.*  FROM {tableName} tbl_idx, IMV_A{Convert.ToString(Intermech.Imbase.Consts.ImbaseTableRefTypeID)} tbl_path  WHERE  tbl_idx.F_LINK_ID = tbl_path.F_OBJECT_ID AND  tbl_path.F_ATTRIBUTE_ID = {(object) Intermech.Imbase.Consts.ClassifFolderKeyAttId} AND  tbl_idx.F_HASHTEXT LIKE {str4} AND {str5} {{1}} {{2}}";
    }
    string commandText = string.Format(format, (object) str1, flag1 ? (object) string.Empty : (object) str3, (object) str2);
    string str6 = $"%{session.StringNormalizer.GetIndexedString(request)}%";
    DataTable retValue = dataManager.ExecuteDataTable(CommandType.Text, commandText, dataManager.Parameter("searchString", (object) str6));
    if (this._imbaseParamsService.CommonParams.UseExtendedSecurityCheckForIndexes)
    {
      if (!imbaseIndexSecurity.CheckAccess(ActionType.ShowNonVisibleColumnImbaseRecords, false, false))
      {
        List<long> list = retValue.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (row => Convert.ToInt64(row[IndexesField.F_TABLE_ID]))).Distinct<long>().ToList<long>();
        if (list.Count == 0)
          return retValue;
        List<long> denyTableIds = new List<long>();
        long num = list.Min();
        long tableId1 = list.Max();
        long categoryId1 = ImbaseHelper.CreateCategoryId(num, (long) attrId);
        long recordId = (long) attrId;
        long categoryId2 = ImbaseHelper.CreateCategoryId(tableId1, recordId);
        DBObject table = session.GetObject(num) as DBObject;
        ImbaseAttSecurity imbaseAttSecurity = new ImbaseAttSecurity(session, table, attrId);
        if (imbaseAttSecurity.LoadCacheTable(ActionType.View, categoryId1, categoryId2) > 0)
        {
          foreach (long tableId2 in list)
          {
            imbaseAttSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(tableId2, (long) attrId));
            if (!imbaseAttSecurity.CheckAccess(ActionType.View, true, false))
              denyTableIds.Add(tableId2);
          }
          retValue.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => denyTableIds.Contains(Convert.ToInt64(row[IndexesField.F_TABLE_ID])))).ToList<DataRow>().ForEach((Action<DataRow>) (r => retValue.Rows.Remove(r)));
          retValue.AcceptChanges();
        }
      }
      bool flag2 = imbaseIndexSecurity.CheckAccess(ActionType.ShowNonVisibleRowImbaseRecords, false, false);
      bool flag3 = imbaseIndexSecurity.CheckAccess(ActionType.ShowNonUseImbaseRecords, false, false);
      if (!flag2 || !flag3)
      {
        List<\u003C\u003Ef__AnonymousType1<long, List<int>>> list = retValue.AsEnumerable().Select(row => new
        {
          Tableid = Convert.ToInt64(row[IndexesField.F_TABLE_ID]),
          RecordKey = Convert.ToInt32(row[IndexesField.F_TABKEY])
        }).GroupBy(key => key.Tableid, g => g.RecordKey, (key, g) => new
        {
          TableId = key,
          RecordIds = g.ToList<int>()
        }).ToList();
        if (list.Count != 0)
        {
          List<Tuple<long, int>> deniedRows = new List<Tuple<long, int>>();
          long minTableId = list.Select(x => x.TableId).Min();
          long maxTableId = list.Select(x => x.TableId).Max();
          int recordId1 = list.Where(k => k.TableId == minTableId).SelectMany(x => (IEnumerable<int>) x.RecordIds).Min();
          int recordId2 = list.Where(k => k.TableId == maxTableId).SelectMany(x => (IEnumerable<int>) x.RecordIds).Max();
          long categoryId3 = ImbaseHelper.CreateCategoryId(minTableId, (long) recordId1);
          long categoryId4 = ImbaseHelper.CreateCategoryId(maxTableId, (long) recordId2);
          DBObject table = session.GetObject(minTableId) as DBObject;
          if (!flag2)
          {
            ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(session, table, (long) recordId1);
            if (imbaseRecordSecurity.LoadCacheTable(ActionType.View, categoryId3, categoryId4) > 0)
            {
              foreach (var data in list)
              {
                foreach (int recordId3 in data.RecordIds)
                {
                  imbaseRecordSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(data.TableId, (long) recordId3));
                  if (!imbaseRecordSecurity.CheckAccess(ActionType.View, true, false))
                    deniedRows.Add(new Tuple<long, int>(data.TableId, recordId3));
                }
              }
            }
          }
          if (!flag3)
          {
            ImbaseRecordSecurity imbaseRecordSecurity = new ImbaseRecordSecurity(session, table, (long) recordId1);
            if (imbaseRecordSecurity.LoadCacheTable(ActionType.Use, categoryId3, categoryId4) > 0)
            {
              foreach (var data in list)
              {
                foreach (int recordId4 in data.RecordIds)
                {
                  imbaseRecordSecurity.SetCategoryId(ImbaseHelper.CreateCategoryId(data.TableId, (long) recordId4));
                  if (!imbaseRecordSecurity.CheckAccess(ActionType.Use, true, false))
                    deniedRows.Add(new Tuple<long, int>(data.TableId, recordId4));
                }
              }
            }
          }
          retValue.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => deniedRows.Contains(new Tuple<long, int>(Convert.ToInt64(row[IndexesField.F_TABLE_ID]), Convert.ToInt32(row[IndexesField.F_TABKEY]))))).Distinct<DataRow>().ToList<DataRow>().ForEach((Action<DataRow>) (r => retValue.Rows.Remove(r)));
          retValue.AcceptChanges();
        }
      }
    }
    return retValue.Rows.Count <= 0 ? (DataTable) null : retValue;
  }

  private string QuickSearch_BuildPathCond(string fieldName, DataTable dtFilter)
  {
    if (dtFilter == null)
      throw new ArgumentNullException(nameof (dtFilter));
    if (string.IsNullOrEmpty(fieldName) || dtFilter.Rows.Count == 0)
      return " ( 1 = 1) ";
    List<string> values = new List<string>(dtFilter.Rows.Count);
    int columnIndex1 = dtFilter.Columns.IndexOf("F_PATH");
    int columnIndex2 = dtFilter.Columns.IndexOf("#FLT");
    foreach (DataRow row in (InternalDataCollectionBase) dtFilter.Rows)
    {
      string str = Convert.ToString(row[columnIndex1]);
      object obj = row[columnIndex2];
      if (obj != DBNull.Value && Convert.ToBoolean(obj))
        values.Add($"{fieldName} LIKE '{str}%'");
      else
        values.Add($"{fieldName}= '{str}'");
    }
    return $"({string.Join(" OR ", (IEnumerable<string>) values)})";
  }

  private List<long> GetVisibleObjectIDs(Guid sessionGuid, long parentId)
  {
    List<long> visibleObjectIds = (List<long>) null;
    ImbaseServer instance = ImbaseServer.Instance;
    DataTable dataTable;
    if (instance == null)
    {
      dataTable = (DataTable) null;
    }
    else
    {
      // ISSUE: explicit non-virtual call
      dataTable = __nonvirtual (instance.GetAllSubfolders(sessionGuid, parentId, new int[1]
      {
        Intermech.Imbase.Consts.ImbaseTableRefTypeID
      }));
    }
    DataTable source = dataTable;
    if (source != null)
    {
      visibleObjectIds = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_OBJECT_ID"]))).ToList<long>();
      visibleObjectIds.Sort();
    }
    return visibleObjectIds;
  }

  public void UpdateBase()
  {
    new Action(this.StartUpdateBase).BeginInvoke((AsyncCallback) null, (object) null);
  }

  private void StartUpdateBase()
  {
    Thread.Sleep(60000);
    session = (UserSession) null;
    try
    {
      if ((this.GetSystemSession("ImbaseIndexing.StartUpdateBase") is UserSession session ? session.DataManager : (IDbManager) null) == null)
        return;
      IDbManager dataManager = session.DataManager;
      bool flag1 = false;
      bool flag2 = true;
      try
      {
        int result;
        if (int.TryParse(Convert.ToString(dataManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE.INDEX'")), out result))
        {
          if (result < 4)
          {
            dataManager.ExecuteNonQuery("DELETE FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE.INDEX'");
          }
          else
          {
            try
            {
              dataManager.ExecuteDataTable("SELECT F_CATALOG_ID FROM IMS_IMBASE_INDEXES WHERE 0=1");
              flag2 = false;
            }
            catch
            {
              dataManager.ExecuteNonQuery("DELETE FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE.INDEX'");
            }
          }
        }
      }
      catch
      {
      }
      if (flag2)
      {
        try
        {
          dataManager.ExecuteDataTable("SELECT F_CATALOG_ID FROM IMS_IMBASE_INDEX WHERE 0=1");
          flag1 = true;
        }
        catch
        {
          try
          {
            dataManager.ExecuteDataTable("SELECT F_CATALOG_ID FROM IMS_IMBASE_INDEXES WHERE 0=1");
            dataManager.ExecuteScalar("INSERT INTO IMS_DBVERSION VALUES('IMBASE.INDEX',4,0)");
          }
          catch
          {
          }
        }
      }
      if (flag1)
      {
        this.CreateImbaseIndexesTable(dataManager);
        this.CreateTempTable(dataManager);
        DataTable source = dataManager.ExecuteDataTable("SELECT F_CATALOG_ID, F_ATTRIBUTE_ID, F_TABKEY FROM IMS_IMBASE_INDEX WHERE F_LINK_ID = -1 ORDER BY F_CATALOG_ID");
        DataTable dtIndexFinished = dataManager.ExecuteDataTable("SELECT * FROM IMS_IMBASE_INDEXES_TMP");
        if (source != null && source.Rows.Count > 0)
        {
          foreach (KeyValuePair<long, List<DataRow>> keyValuePair in (dtIndexFinished == null || dtIndexFinished.Rows.Count <= 0 ? (IEnumerable<DataRow>) source.AsEnumerable().Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToList<DataRow>() : (IEnumerable<DataRow>) source.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => dtIndexFinished.Select($"[F_CATALOG_ID]='{x["F_CATALOG_ID"]}' and [F_ATTRIBUTE_ID]='{x["F_ATTRIBUTE_ID"]}'").Length == 0)).Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToList<DataRow>()).GroupBy<DataRow, long, DataRow>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_CATALOG_ID"])), (System.Func<DataRow, DataRow>) (y => y)).ToDictionary<IGrouping<long, DataRow>, long, List<DataRow>>((System.Func<IGrouping<long, DataRow>, long>) (k => k.Key), (System.Func<IGrouping<long, DataRow>, List<DataRow>>) (v => v.ToList<DataRow>())))
            this.UpdateCatalogIndexes(session, keyValuePair.Key, keyValuePair.Value);
        }
        dataManager.BeginTransaction();
        try
        {
          dataManager.ExecuteNonQuery(sc_8027.ssp_appserver_8029());
          dataManager.ExecuteNonQuery("DROP TABLE IMS_IMBASE_INDEXES_TMP");
          dataManager.Commit();
          dataManager.ExecuteScalar("INSERT INTO IMS_DBVERSION VALUES('IMBASE.INDEX',4,0)");
        }
        catch (Exception ex)
        {
          dataManager.Rollback();
        }
      }
      IEventLogHelper service = ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true);
      try
      {
        int result;
        int.TryParse(Convert.ToString(dataManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = :module_name", dataManager.Parameter(":module_name", (object) "IMBASE.INDEX"))), out result);
        if (result >= 6)
          return;
        service.AddToTrace(LocalizationHolder.rm.GetString("UpdateIndexes60"));
        bool flag3 = true;
        foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT * FROM IMS_IMBASE_INDEXES").Rows)
        {
          bool flag4 = false;
          long int64 = Convert.ToInt64(row[IndexesField.F_CATALOG_ID]);
          int int32_1 = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_ID]);
          string tableName1 = Convert.ToString(row[IndexesField.F_TABLE_NAME]);
          int int32_2 = Convert.ToInt32(row[IndexesField.F_ATTRIBUTE_STATE]);
          bool flag5 = false;
          int int32_3 = Convert.ToInt32(row[IndexesField.F_FLAG]);
          if (int32_3 != -1 && ((IndexesFlags) int32_3).HasFlag((Enum) IndexesFlags.UniqueValue))
            flag5 = true;
          if (int32_2 == 0)
          {
            this.ReleaseTables(dataManager, new List<string>()
            {
              tableName1
            });
            flag4 = true;
          }
          if (!flag4)
          {
            DataTable dataTable = (DataTable) null;
            try
            {
              dataTable = dataManager.ExecuteDataTable($"SELECT * FROM {tableName1} WHERE {IndexesField.F_LINK_ID} = :l_id", dataManager.Parameter(":l_id", (object) -1));
            }
            catch
            {
              this.RemoveTableFromImsIMbaseIndexes(dataManager, tableName1);
              string tableName2 = ImbaseIndexingService.GenerateTableName(int64, int32_1);
              this.RemoveTableFromBase(dataManager, tableName2);
              flag4 = true;
            }
            if (!flag4)
            {
              if ((dataTable != null ? dataTable.Columns.IndexOf(IndexesField.F_APPLICABILITY) : -1) == -1)
              {
                service.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_32"), (object) tableName1));
                flag4 = true;
              }
              if (!flag4 && dataManager.DataProvider.Name == "Sql" && dataTable.Columns[IndexesField.F_LINK_ID].DataType == typeof (int))
              {
                service.AddToTrace(string.Format(LocalizationHolder.rm.GetString("IndexTableInvalidType"), (object) tableName1, (object) IndexesField.F_LINK_ID));
                flag4 = true;
              }
            }
          }
          if (flag4)
          {
            Dictionary<int, bool> attrs = new Dictionary<int, bool>()
            {
              {
                int32_1,
                flag5
              }
            };
            service.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Imbase.Server_33"), (object) tableName1));
            if (!this.RebuildIndex(session.SessionGUID, Guid.NewGuid(), int64, attrs, tableName1, service))
              flag3 = false;
          }
        }
        if (!flag3)
          return;
        dataManager.BeginTransaction();
        try
        {
          dataManager.ExecuteScalar("UPDATE IMS_DBVERSION SET F_VERSION_ID = :version_id, F_REVISION_ID = :revision_id WHERE F_MODULE_NAME = :module_name", dataManager.Parameter(":version_id", (object) 6), dataManager.Parameter(":revision_id", (object) 0), dataManager.Parameter(":module_name", (object) "IMBASE.INDEX"));
          dataManager.Commit();
          service.AddToTrace(LocalizationHolder.rm.GetString("EndUpdateIndexes60"));
        }
        catch (Exception ex)
        {
          dataManager.Rollback();
          throw;
        }
      }
      catch (Exception ex)
      {
        service.AddToTrace(string.Format(LocalizationHolder.rm.GetString("ErrorUpdateIndexes60"), (object) ex.Message, (object) Environment.NewLine, (object) ex.StackTrace));
      }
    }
    catch (Exception ex)
    {
    }
    finally
    {
      session?.Logout("ImbaseIndexing.StartUpdateBase");
    }
  }

  private void RemoveTableFromImsIMbaseIndexes(IDbManager manager, string tableName)
  {
    try
    {
      manager.ExecuteNonQuery($"DELETE FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_TABLE_NAME}=:parTblName", manager.Parameter(":parTblName", (object) tableName));
    }
    catch (Exception ex)
    {
    }
  }

  private void RemoveTableFromBase(IDbManager manager, string tableName)
  {
    try
    {
      manager.ExecuteNonQuery("DROP TABLE " + tableName);
    }
    catch (Exception ex)
    {
    }
  }

  private IUserSession GetSystemSession(string sessionName)
  {
    IUserSession systemSession = (IUserSession) null;
    if (ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service)
      systemSession = service.GetSystemSessionPermanentClone(sessionName);
    return systemSession;
  }

  private void CreateImbaseIndexesTable(IDbManager manager)
  {
    bool flag = true;
    try
    {
      manager.ExecuteDataTable($"SELECT {IndexesField.F_CATALOG_ID} FROM IMS_IMBASE_INDEXES WHERE 0 = 1");
      flag = false;
    }
    catch (Exception ex)
    {
    }
    if (!flag)
      return;
    manager.BeginTransaction();
    try
    {
      if (manager.DataProvider.Name == "Sql")
      {
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES (F_CATALOG_ID   BigNumber_DEF NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_FLAG         INTEGER NOT NULL,F_TABLE_NAME   MaximumString_DEF NOT NULL,F_ATTRIBUTE_STATE  INTEGER NOT NULL)");
        manager.ExecuteNonQuery(sc_8027.ssp_appserver_8030());
      }
      else if (manager.DataProvider.Name == "Oracle")
      {
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES (F_CATALOG_ID   INTEGER NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_FLAG         INTEGER NOT NULL,F_TABLE_NAME   NVARCHAR2(30) NOT NULL,F_ATTRIBUTE_STATE  INTEGER NOT NULL)");
        manager.ExecuteNonQuery(sc_8027.ssp_appserver_8031());
      }
      else if (manager.DataProvider.Name == "PostgreSQL")
      {
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES (F_CATALOG_ID   bigint NOT NULL,F_ATTRIBUTE_ID INTEGER NOT NULL,F_FLAG         INTEGER NOT NULL,F_TABLE_NAME   varchar(30) NOT NULL,F_ATTRIBUTE_STATE  INTEGER NOT NULL)");
        manager.ExecuteNonQuery(sc_8027.ssp_appserver_8032());
      }
      manager.Commit();
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw;
    }
  }

  private void CreateTempTable(IDbManager manager)
  {
    bool flag = true;
    try
    {
      manager.ExecuteDataTable($"SELECT {IndexesField.F_CATALOG_ID} FROM IMS_IMBASE_INDEXES_TMP WHERE 0 = 1");
      flag = false;
    }
    catch (Exception ex)
    {
    }
    if (!flag)
      return;
    manager.BeginTransaction();
    try
    {
      if (manager.DataProvider.Name == "Sql")
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES_TMP (F_CATALOG_ID BigNumber_DEF NOT NULL, F_ATTRIBUTE_ID INTEGER NOT NULL)");
      else if (manager.DataProvider.Name == "Oracle")
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES_TMP (F_CATALOG_ID INTEGER NOT NULL, F_ATTRIBUTE_ID INTEGER NOT NULL)");
      else if (manager.DataProvider.Name == "PostgreSQL")
        manager.ExecuteNonQuery("CREATE TABLE IMS_IMBASE_INDEXES_TMP (F_CATALOG_ID bigint NOT NULL, F_ATTRIBUTE_ID INTEGER NOT NULL)");
      manager.Commit();
    }
    catch (Exception ex)
    {
      manager.Rollback();
      throw;
    }
  }

  private void UpdateCatalogIndexes(UserSession session, long catalogId, List<DataRow> rows)
  {
    IDbManager dataManager = session.DataManager;
    string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID((IUserSession) session, catalogId);
    if (string.IsNullOrEmpty(classifKeyByObjId))
      return;
    IDbDataParameter dbDataParameter1 = dataManager.Parameter(":pCatalogID", (object) catalogId);
    IDbDataParameter dbDataParameter2 = dataManager.Parameter(":pAttrID", (object) -1);
    session.StartTransaction();
    try
    {
      if (Convert.ToInt32(session.DataManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_IMBASE_INDEXES WHERE {IndexesField.F_CATALOG_ID}=:pCatalogID AND {IndexesField.F_ATTRIBUTE_ID}=:pAttrID", dbDataParameter1, dbDataParameter2)) == 0)
        dataManager.ExecuteNonQuery($"INSERT INTO IMS_IMBASE_INDEXES ({IndexesField.F_CATALOG_ID}, {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_FLAG}, {IndexesField.F_TABLE_NAME}, {IndexesField.F_ATTRIBUTE_STATE}) VALUES (:pCatalogID, :pAttrID, :pFlag, :pTblName, :pStatus)", dbDataParameter1, dbDataParameter2, dataManager.Parameter(":pFlag", (object) -1), dataManager.Parameter(":pTblName", (object) "tmp"), dataManager.Parameter(":pStatus", (object) 1));
      session.Commit();
    }
    catch (Exception ex)
    {
      session.Rollback();
      throw;
    }
    try
    {
      Dictionary<int, string> attrs = new Dictionary<int, string>(rows.Count);
      foreach (DataRow row in rows)
      {
        int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
        int int32_2 = Convert.ToInt32(row["F_TABKEY"]);
        string tableName = ImbaseIndexingService.GenerateTableName(catalogId, int32_1);
        try
        {
          this.RemoveTable(dataManager, tableName);
        }
        catch (Exception ex)
        {
        }
        this.CreateTable(dataManager, catalogId, int32_1, int32_2 == 17, tableName, IndexesStates.Active);
        attrs.Add(int32_1, tableName);
      }
      try
      {
        DataTable tableRefIds = this.GetTableRefIDs(session, classifKeyByObjId, true);
        if (tableRefIds != null)
        {
          foreach (DataRow row in (InternalDataCollectionBase) tableRefIds.Rows)
          {
            try
            {
              long num1 = Math.Abs(Convert.ToInt64(row[IndexesField.F_LINK_ID]));
              DBObject dbObject = (DBObject) session.GetObject(num1, false);
              if (dbObject == null || dbObject.LevelID == session.IdentHelper.DeletedID)
                throw new Exception();
              IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
              if (attributeById != null)
              {
                if (attributeById.Value != null)
                {
                  if (attributeById.Value != DBNull.Value)
                  {
                    long num2 = Math.Abs(attributeById.AsInteger);
                    DataTable dtAttrs;
                    DataTable dtData;
                    this.GetTables(session, num2, out dtAttrs, out dtData);
                    this.AssignAttributes(session, num1, num2, dtAttrs, dtData);
                    if (session.DataManager.DataProvider.Name == "Sql")
                      this.AddIndexesDataForSql(session, num1, num2, dtData, attrs);
                    else if (session.DataManager.DataProvider.Name == "Oracle")
                      this.AddIndexesDataForOracle(session, num1, num2, dtData, attrs);
                    else if (session.DataManager.DataProvider.Name == "PostgreSQL")
                      this.AddIndexesDataForOther(session, num1, num2, dtData, attrs);
                  }
                }
              }
            }
            catch (Exception ex)
            {
            }
          }
        }
        foreach (KeyValuePair<int, string> keyValuePair in attrs)
        {
          session.StartTransaction();
          try
          {
            dbDataParameter2.Value = (object) keyValuePair.Key;
            dataManager.ExecuteNonQuery("INSERT INTO IMS_IMBASE_INDEXES_TMP (F_CATALOG_ID, F_ATTRIBUTE_ID) VALUES (:pCatalogID, :pAttrID)", dbDataParameter1, dbDataParameter2);
            session.Commit();
          }
          catch (Exception ex)
          {
            session.Rollback();
            throw;
          }
        }
      }
      catch (Exception ex)
      {
      }
    }
    finally
    {
      dbDataParameter2.Value = (object) -1;
      dataManager.ExecuteNonQuery("DELETE FROM IMS_IMBASE_INDEXES WHERE F_CATALOG_ID=:pCatalogID AND F_ATTRIBUTE_ID=:pAttrID", dbDataParameter1, dbDataParameter2);
    }
  }

  public Dictionary<int, IndexesFlags> GetAttributesFlags(
    Guid sessionGuid,
    long catalogId,
    IEnumerable<int> attrs)
  {
    Dictionary<int, IndexesFlags> reslt = new Dictionary<int, IndexesFlags>();
    DataTable indexes = this.GetIndexes(sessionGuid, catalogId, new string[2]
    {
      IndexesField.F_ATTRIBUTE_ID,
      IndexesField.F_FLAG
    });
    if (indexes == null)
      return reslt;
    Dictionary<int, int> attrOptDict = indexes.AsEnumerable().ToDictionary<DataRow, int, int>((System.Func<DataRow, int>) (key => Convert.ToInt32(key[IndexesField.F_ATTRIBUTE_ID])), (System.Func<DataRow, int>) (val => Convert.ToInt32(val[IndexesField.F_FLAG])));
    attrs.ToList<int>().ForEach((Action<int>) (x =>
    {
      int num;
      if (!attrOptDict.TryGetValue(x, out num) || num == -1)
        return;
      reslt.Add(x, (IndexesFlags) num);
    }));
    return reslt;
  }

  private static class ExceptionsMessages
  {
    internal static readonly string TableRefListEmpty = LocalizationHolder.rm.GetString("Imbase_Indexes_TableRefList_Empty");
    internal static readonly string NullDbManager = LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullDBManager");
    internal static readonly string NullSession = LocalizationHolder.rm.GetString("Imbase.Server.ImbaseindexingService.NullSession");
    internal static readonly string DataBaseException = LocalizationHolder.rm.GetString("Imbase_DataBase_Exception");
    internal static readonly string UndefinedCatalogId = LocalizationHolder.rm.GetString("Imbase_UndefinedCatalogID");
    internal static readonly string AttributeListEmpty = LocalizationHolder.rm.GetString("Imbase_Indexes_AttributeList_Empty");
    internal static readonly string CatalogClassifKeyEmpty = LocalizationHolder.rm.GetString("Imbase_Indexes_CatalogClassifKey_Empty");
    internal static readonly string NullAttribute = LocalizationHolder.rm.GetString("Imbase_Attribute_Null");
    internal static readonly string BusyAttribute = LocalizationHolder.rm.GetString("Imbase_Attribute_Busy");
    internal static readonly string TableRefNull = LocalizationHolder.rm.GetString("Imbase_Indexing_TableRefObject_Null_Msg");
    internal static readonly string TableNull = LocalizationHolder.rm.GetString("Imbase_Indexing_TableObject_Null_Msg");
    internal static readonly string TableRefAttrNull = LocalizationHolder.rm.GetString("Imbase_Indexing_TableRefAttr_Null");
    internal static readonly string ImbaseTableNull = LocalizationHolder.rm.GetString("Imbase_ImbaseTable_TablesNull");
    internal static readonly string AssignAttrsError = LocalizationHolder.rm.GetString("Imbase_AssignAttrs_Error");
    internal static readonly string MultiTableReferences = LocalizationHolder.rm.GetString("Imbase_Indexing_MultiTableReferences");
  }

  private class TableRefInfo
  {
    internal long CatalogId { get; set; }

    internal long TableId { get; set; }

    internal long TableRefId { get; set; }
  }

  private class Task
  {
    private ImbaseIndexingService.TaskState _state = ImbaseIndexingService.TaskState.Running;

    internal string Caption { get; set; }

    internal int Completed
    {
      get
      {
        return this.CountItems != 0 && !this.Terminated ? (int) ((double) this.CurrItemNumber / (double) this.CountItems * 100.0) : 100;
      }
    }

    internal int CountItems { get; set; }

    internal int CurrItemNumber { get; set; }

    internal bool Running => this._state == ImbaseIndexingService.TaskState.Running;

    internal bool Terminated => this._state == ImbaseIndexingService.TaskState.Terminated;

    internal List<int> Attributes { get; set; }

    internal long CatalogId { get; set; }

    internal List<IndexingException> Exceptions { get; set; }

    internal bool RemoveAfterComplete { get; set; }

    internal List<ImbaseIndexingService.TableRefInfo> TableRefInfoList { get; set; }

    internal Guid TaskGuid { get; }

    internal string ComputerName { get; set; }

    internal string TaskName { get; set; }

    public Task(Guid taskGuid, long catalogId = 0)
    {
      this.TaskGuid = taskGuid;
      this.CatalogId = catalogId;
      this.Exceptions = new List<IndexingException>(1);
      this.TableRefInfoList = new List<ImbaseIndexingService.TableRefInfo>();
      this.Attributes = new List<int>(0);
      this.CountItems = this.CurrItemNumber = 0;
      this.RemoveAfterComplete = false;
    }

    internal void ClearValue() => this.CountItems = this.CurrItemNumber = 0;

    internal void SetState(ImbaseIndexingService.TaskState state) => this._state = state;
  }

  private enum TaskState
  {
    [CustomDescription("Attribute.Interfaces.Client_25")] Error,
    [CustomDescription("Attribute.Interfaces.Client_21")] Running,
    [CustomDescription("Attribute.Interfaces.Client_24")] Terminated,
  }

  private class ImsTmpIdataStreamingDataRecord : IEnumerable<SqlDataRecord>, IEnumerable
  {
    private DataTable _dt;
    private SqlMetaData[] _columnStructure;

    public ImsTmpIdataStreamingDataRecord(DataTable dtIndexData)
    {
      this._dt = dtIndexData ?? throw new ArgumentNullException(nameof (dtIndexData));
      this._columnStructure = new SqlMetaData[6]
      {
        new SqlMetaData(IndexesField.F_LINK_ID, SqlDbType.BigInt),
        new SqlMetaData(IndexesField.F_TABLE_ID, SqlDbType.BigInt),
        new SqlMetaData(IndexesField.F_TABKEY, SqlDbType.BigInt),
        new SqlMetaData(IndexesField.F_TEXT, SqlDbType.NVarChar, 850L),
        new SqlMetaData(IndexesField.F_HASHTEXT, SqlDbType.NVarChar, 850L),
        new SqlMetaData(IndexesField.F_APPLICABILITY, SqlDbType.BigInt)
      };
    }

    public IEnumerator<SqlDataRecord> GetEnumerator()
    {
      foreach (DataRow row in (InternalDataCollectionBase) this._dt.Rows)
      {
        SqlDataRecord sqlDataRecord = new SqlDataRecord(this._columnStructure);
        sqlDataRecord.SetInt64(0, Convert.ToInt64(row[IndexesField.F_LINK_ID]));
        sqlDataRecord.SetInt64(1, Convert.ToInt64(row[IndexesField.F_TABLE_ID]));
        sqlDataRecord.SetInt64(2, Convert.ToInt64(row[IndexesField.F_TABKEY]));
        sqlDataRecord.SetString(3, Convert.ToString(row[IndexesField.F_TEXT]));
        sqlDataRecord.SetString(4, Convert.ToString(row[IndexesField.F_HASHTEXT]));
        sqlDataRecord.SetInt64(5, Convert.ToInt64(row[IndexesField.F_APPLICABILITY]));
        yield return sqlDataRecord;
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
