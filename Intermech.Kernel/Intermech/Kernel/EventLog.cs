// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EventLog
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ICSharpCode.SharpZipLib.Zip;
using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;


namespace Intermech.Kernel;

public class EventLog : DBRecordSet, IEventLog, IDBRecords, IDBSessionable, IDBSecurity
{
  internal EventLogHelper _EventLogHelper;
  private bool _TranslateValues;
  private EventlogSettings _Settings;
  private bool _SettingsLoaded;
  internal bool _NeedSelectCheckAccess = true;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(5);

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.Events;
  }

  static EventLog()
  {
    Intermech.Kernel.EventLog.metadataActions.Add(ActionType.GetAccess, false);
    Intermech.Kernel.EventLog.metadataActions.Add(ActionType.SetAccess, false);
    Intermech.Kernel.EventLog.metadataActions.Add(ActionType.List, false);
    Intermech.Kernel.EventLog.metadataActions.Add(ActionType.EditProperties, false);
    Intermech.Kernel.EventLog.metadataActions.Add(ActionType.Delete, false);
  }

  public EventLog(UserSession uSession, bool archiveMode)
    : base(uSession, 0)
  {
    if (archiveMode)
      this._DBObjectTableName = "IMS_EVENTLOG_ARC";
    else
      this._DBObjectTableName = "IMS_EVENTLOG";
    this._DBKeyField = "F_EVENT_ID";
    this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_EVENT_ID);
    this._EventLogHelper = ServerServices.GetService(typeof (IEventLogHelper)) as EventLogHelper;
    this.InitSecurityOptions(10, 0L);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_839");

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, Intermech.Kernel.EventLog.metadataActions);
  }

  public long CloseEvent(
    long EventID,
    long objectID,
    long categoryID,
    string objectName,
    string Note,
    EventlogRecordType AuditType,
    IUserSession aSession)
  {
    return this.EventHelper.CloseEvent(EventID, objectID, categoryID, objectName, Note, AuditType, (IUserSession) this.UserSession);
  }

  public Hashtable GetActionNamesHash() => Intermech.Interfaces.EventLog.Helper.ActionNames;

  public EventlogSettings Settings
  {
    get
    {
      if (!this._SettingsLoaded)
      {
        this._EventLogHelper.LoadSettings(this.UserSession.DataManager);
        long[] numArray1 = new long[this._EventLogHelper.NotLoggedObjects.Count];
        this._EventLogHelper.NotLoggedObjects.Keys.CopyTo(numArray1, 0);
        int[] numArray2 = new int[this._EventLogHelper.NotLoggedTypes.Keys.Count];
        this._EventLogHelper.NotLoggedTypes.Keys.CopyTo(numArray2, 0);
        this._Settings = new EventlogSettings(this._EventLogHelper._TraceOn, numArray1, numArray2, this._EventLogHelper._AutoClear, this._EventLogHelper._RecordKeepDays);
        this._SettingsLoaded = true;
      }
      return this._Settings;
    }
    set
    {
      IDbManager dataManager = this.UserSession.DataManager;
      long EventID = this.AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, "");
      this.CheckAccess(ActionType.EditProperties);
      this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
      if (value.NotLoggedObjects.Length > dataManager.DataProvider.MaximumINOperands)
        throw new KernelExceptionID(sc_12350.ssp_appserver_12351(518268044), (object) dataManager.DataProvider.MaximumINOperands);
      DataTable table = new DataTable();
      table.Columns.Add("F_PARAM_NAME", typeof (string));
      table.Columns.Add("F_VALUE", typeof (string));
      DataRow row1 = table.NewRow();
      row1["F_PARAM_NAME"] = (object) "SWITCH";
      row1["F_VALUE"] = value.LogOn ? (object) "1" : (object) "0";
      table.Rows.Add(row1);
      DataRow row2 = table.NewRow();
      row2["F_PARAM_NAME"] = (object) "AUTO";
      row2["F_VALUE"] = value.AutoClear ? (object) "1" : (object) "0";
      table.Rows.Add(row2);
      DataRow row3 = table.NewRow();
      row3["F_PARAM_NAME"] = (object) "DAYS";
      row3["F_VALUE"] = (object) value.RecordsKeepDays.ToString();
      table.Rows.Add(row3);
      for (int index = 0; index < value.NotLoggedObjects.Length; ++index)
      {
        DataRow row4 = table.NewRow();
        row4["F_PARAM_NAME"] = (object) ("O" + index.ToString());
        row4["F_VALUE"] = (object) value.NotLoggedObjects[index].ToString();
        table.Rows.Add(row4);
      }
      for (int index = 0; index < value.NotLoggedTypes.Length; ++index)
      {
        DataRow row5 = table.NewRow();
        row5["F_PARAM_NAME"] = (object) ("T" + index.ToString());
        row5["F_VALUE"] = (object) value.NotLoggedTypes[index].ToString();
        table.Rows.Add(row5);
      }
      table.AcceptChanges();
      this.UserSession.Configurations.WriteSection("KERNEL", "EVENTS", table, 0L);
      this._Settings = value;
      this._EventLogHelper.LoadSettings(value);
    }
  }

  public override int Delete(long[] idList, bool throwException, long deleteMode)
  {
    this.DeleteEvents(idList);
    return idList.Length;
  }

  public void DeleteEvents(long[] EventsID)
  {
    if (EventsID.Length == 0)
      return;
    long EventID = this.AddEvent(0L, ActionType.Delete, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_840"), (object) EventsID.Length));
    this.CheckAccess(ActionType.Delete);
    this.CloseEvent(EventID, EventlogRecordType.AccessGranted);
    this.UserSession.StartTransaction();
    try
    {
      int index = 0;
      int num = 0;
      DataTable dataTable1 = (DataTable) null;
      StringBuilder stringBuilder = new StringBuilder();
      do
      {
        stringBuilder.Append(EventsID[index].ToString());
        if (index == EventsID.Length - 1 || num == 200)
        {
          if (ServerConsts.BackupEventlogRecords)
          {
            DataTable dataTable2 = this.UserSession.DataManager.ExecuteDataTable($"SELECT * FROM {this._DBObjectTableName} WHERE F_EVENT_ID IN ({stringBuilder})");
            if (dataTable1 == null)
              dataTable1 = dataTable2;
            else
              SqlHelper.AssignRows(dataTable1, (IEnumerable<DataRow>) dataTable2.Select());
          }
          this.UserSession.DataManager.ExecuteNonQuery($"DELETE FROM {this._DBObjectTableName} WHERE F_EVENT_ID IN ({stringBuilder})");
          stringBuilder.Length = 0;
          num = 0;
        }
        else
        {
          stringBuilder.Append(",");
          ++num;
        }
        ++index;
      }
      while (index < EventsID.Length);
      if (dataTable1 != null)
        this.SaveRecordsToFile(dataTable1);
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.CloseEvent(EventID, EventlogRecordType.Error, ex.Message);
      throw;
    }
  }

  private void SaveRecordsToFile(DataTable recs)
  {
    recs.TableName = "Event";
    string fullTraceFileName = (this.UserSession.EventLogHelper as EventLogHelper).GetFullTraceFileName($"{this.UserSession.ComputerName}_{DateTime.UtcNow.Ticks.ToString()}.xml");
    string path = fullTraceFileName + ".zip";
    recs.WriteXml(fullTraceFileName);
    using (ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) File.Create(path)))
    {
      zipOutputStream.SetLevel(5);
      byte[] buffer = new byte[4096 /*0x1000*/];
      ZipEntry entry = new ZipEntry(Path.GetFileName(fullTraceFileName));
      zipOutputStream.PutNextEntry(entry);
      using (FileStream fileStream = File.OpenRead(fullTraceFileName))
      {
        int count;
        do
        {
          count = fileStream.Read(buffer, 0, buffer.Length);
          zipOutputStream.Write(buffer, 0, count);
        }
        while (count > 0);
      }
      zipOutputStream.Finish();
      zipOutputStream.Close();
    }
    File.Delete(fullTraceFileName);
  }

  public void ArchiveEvents(DateTime fromDate)
  {
    fromDate = fromDate.Date;
    try
    {
      this.CheckAccess(ActionType.Delete);
    }
    catch
    {
      this.AddEvent(0L, ActionType.Remove, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_841") + fromDate.ToString());
      throw;
    }
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      dataManager.SetAdminCommandTimeout();
      dataManager.ExecuteNonQuery("INSERT INTO IMS_EVENTLOG_ARC (F_EVENT_ID, F_CATEGORY_TYPE, F_CATEGORY_ID, F_OBJECT_ID, F_RELATION_ID, F_OBJECT_NAME, F_USER_ID, F_COMPUTER_NAME, F_NOTE, F_EVENT_TYPE, F_BEGIN_DATE, F_END_DATE, F_AUDIT_TYPE) SELECT F_EVENT_ID, F_CATEGORY_TYPE, F_CATEGORY_ID, F_OBJECT_ID, F_RELATION_ID, F_OBJECT_NAME, F_USER_ID, F_COMPUTER_NAME, F_NOTE, F_EVENT_TYPE, F_BEGIN_DATE, F_END_DATE, F_AUDIT_TYPE FROM IMS_EVENTLOG WHERE F_BEGIN_DATE <= :date1", dataManager.Parameter("date1", (object) fromDate));
      dataManager.ExecuteNonQuery("DELETE FROM IMS_EVENTLOG WHERE F_BEGIN_DATE <= :date1", dataManager.Parameter("date1", (object) fromDate));
      dataManager.SetNormalCommandTimeout();
      this.AddEvent(0L, ActionType.Remove, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("Kernel_841") + fromDate.ToString());
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.AddEvent(0L, ActionType.Remove, EventlogRecordType.Error, $"{LocalizationHolder.rm.GetString("Kernel_841")}{fromDate.ToString()}:{ex.Message}");
      throw;
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }

  public void ClearEvents(DateTime fromDate)
  {
    fromDate = fromDate.Date;
    try
    {
      this.CheckAccess(ActionType.Delete);
    }
    catch
    {
      this.AddEvent(0L, ActionType.Clear, EventlogRecordType.AccessDenied, LocalizationHolder.rm.GetString("Kernel_841") + fromDate.ToString());
      throw;
    }
    IDbManager dataManager = this.UserSession.DataManager;
    this.UserSession.StartTransaction();
    try
    {
      dataManager.SetAdminCommandTimeout();
      if (ServerConsts.BackupEventlogRecords)
        this.SaveRecordsToFile(dataManager.ExecuteDataTable($"SELECT * FROM {this._DBObjectTableName} WHERE F_BEGIN_DATE <= :date1 ORDER BY F_EVENT_ID DESC", dataManager.Parameter("date1", (object) fromDate)));
      dataManager.ExecuteNonQuery($"DELETE FROM {this._DBObjectTableName} WHERE F_BEGIN_DATE <= :date1", dataManager.Parameter("date1", (object) fromDate));
      dataManager.SetNormalCommandTimeout();
      this.AddEvent(0L, ActionType.Clear, EventlogRecordType.AccessGranted, LocalizationHolder.rm.GetString("Kernel_841") + fromDate.ToString());
      this.UserSession.Commit();
    }
    catch (Exception ex)
    {
      this.UserSession.Rollback();
      this.AddEvent(0L, ActionType.Clear, EventlogRecordType.Error, $"{LocalizationHolder.rm.GetString("Kernel_841")}{fromDate.ToString()}:{ex.Message}");
      throw;
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
    }
  }

  private string ReplaceWithNVarchar(string sqlStr, string fldName, int len)
  {
    return sqlStr.Replace($"{this.UserSession.QueryBuilder.SystemTableAlias}.{fldName}", this.UserSession.DataManager.DataProvider.NVARCHARCast(fldName, len, this.UserSession.QueryBuilder.SystemTableAlias));
  }

  internal override string GetColumnsSQL(
    IDBAttributeType[] columns,
    ColumnContents[] contents,
    Intermech.Kernel.Search.ColumnInfo[] cinfo,
    int recordsCount)
  {
    string sqlStr = base.GetColumnsSQL(columns, contents, cinfo, recordsCount);
    if (this._TranslateValues)
    {
      string str = this.ReplaceWithNVarchar(sqlStr, "F_EVENT_TYPE", 10);
      int attributeId = this.UserSession.GetAttributeTypeCollection(-1).GetAttributeType((object) new Guid("cad0001d-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
      sqlStr = this.ReplaceWithNVarchar(this.ReplaceWithNVarchar(str.Replace(this.UserSession.QueryBuilder.SystemTableAlias + ".F_USER_ID", string.Format("(SELECT USERA.CAPTION FROM IMV_O{1} USERA WHERE USERA.F_OBJECT_ID = {0}.F_USER_ID) USER_ID", (object) this.UserSession.QueryBuilder.SystemTableAlias, (object) this.UserSession.IdentHelper.UsersTypeID)), "F_AUDIT_TYPE", 10), "F_CATEGORY_TYPE", 10);
    }
    return sqlStr;
  }

  protected override void ConfigureQueryBuilder(ConditionStructure[] conditions)
  {
    base.ConfigureQueryBuilder(conditions);
    this.UserSession.QueryBuilder.TypeFilter = string.Empty;
  }

  public DataTable Select(DBRecordSetParams paramSet, bool translateValues)
  {
    if (this._NeedSelectCheckAccess)
      this.CheckAccess(ActionType.List);
    this.UserSession.QueryBuilder.OptimizedTypeID = -1;
    this.UserSession.QueryBuilder.SystemTableName = this._DBObjectTableName;
    this._TranslateValues = translateValues;
    int capacity = 0;
    if (translateValues && paramSet.Columns != null)
    {
      capacity = paramSet.Columns.Length;
      List<int> intList = new List<int>(capacity);
      for (int index = 0; index < capacity; ++index)
        intList.Add(this.UserSession.EventLogHelper.GetAttributeID(paramSet.Columns[index]));
      if (intList.Contains(-39))
      {
        if (!intList.Contains(-31))
          intList.Add(-31);
        if (!intList.Contains(-32))
          intList.Add(-32);
      }
      if (capacity != intList.Count)
      {
        List<object> objectList = new List<object>(intList.Count);
        for (int index = 0; index < intList.Count; ++index)
          objectList.Add((object) intList[index]);
        paramSet.Columns = objectList.ToArray();
      }
    }
    if (paramSet.RecordCount == -1 || paramSet.RecordCount > ServerConsts.MaxDataTableRowsCount)
      paramSet.RecordCount = ServerConsts.MaxDataTableRowsCount;
    DataTable tbl = base.Select(paramSet);
    ServerConsts.ValidateTableMaxRows(tbl);
    if (translateValues && tbl.Rows.Count > 0)
    {
      IDBAttributeTypeCollection attributeTypeCollection = this.UserSession.GetAttributeTypeCollection(-1);
      int columnIndex1 = tbl.Columns.IndexOf(attributeTypeCollection.GetAttributeType((object) ObligatoryObjectAttributes.F_EVENT_TYPE, true).Name);
      int columnIndex2 = tbl.Columns.IndexOf(attributeTypeCollection.GetAttributeType((object) ObligatoryObjectAttributes.F_AUDIT_TYPE, true).Name);
      int columnIndex3 = tbl.Columns.IndexOf(attributeTypeCollection.GetAttributeType((object) ObligatoryObjectAttributes.F_CATEGORY_TYPE, true).Name);
      int columnIndex4 = tbl.Columns.IndexOf(attributeTypeCollection.GetAttributeType((object) ObligatoryObjectAttributes.F_CATEGORY_ID, true).Name);
      foreach (DataRow row in (InternalDataCollectionBase) tbl.Rows)
      {
        if (columnIndex2 > -1)
          row[columnIndex2] = (object) EventlogRecordTypeHelper.GetCaption((EventlogRecordType) Convert.ToInt32(row[columnIndex2]));
        if (columnIndex1 > -1 && columnIndex3 > -1 && columnIndex4 > -1)
          row[columnIndex1] = (object) this._EventLogHelper.GetActionName(Convert.ToInt32(row[columnIndex3]), Convert.ToInt64(row[columnIndex4]), (ActionType) Convert.ToInt32(row[columnIndex1]));
        if (columnIndex3 > -1)
          row[columnIndex3] = (object) Consts.GetCategoryName(Convert.ToInt32(row[columnIndex3]));
      }
    }
    if (capacity > 0)
    {
      while (tbl.Columns.Count > capacity)
        tbl.Columns.RemoveAt(tbl.Columns.Count - 1);
    }
    return tbl;
  }

  public override DataTable Select(DBRecordSetParams paramSet) => this.Select(paramSet, false);

  protected override IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    if (pars.Columns == null || pars.Columns.Length == 0)
      pars.Columns = new object[13]
      {
        (object) ObligatoryObjectAttributes.F_AUDIT_TYPE,
        (object) ObligatoryObjectAttributes.F_BEGIN_DATE,
        (object) ObligatoryObjectAttributes.F_EVENT_ID,
        (object) ObligatoryObjectAttributes.F_EVENT_TYPE,
        (object) ObligatoryObjectAttributes.F_OBJECT_NAME,
        (object) ObligatoryObjectAttributes.F_USER_ID,
        (object) ObligatoryObjectAttributes.F_COMPUTER_NAME,
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_RELATION_ID,
        (object) ObligatoryObjectAttributes.F_NOTE,
        (object) ObligatoryObjectAttributes.F_CATEGORY_TYPE,
        (object) ObligatoryObjectAttributes.F_CATEGORY_ID,
        (object) ObligatoryObjectAttributes.F_END_DATE
      };
    return base.GetColumnsCollection(ref pars, failIfNotFound);
  }

  public int AddToTrace(string EventStr, int TraceLevel, string TraceFileName)
  {
    string EventStr1;
    if (EventStr != "")
      EventStr1 = $", SID: {this.UserSession.SessionID.ToString()}, User: {this.UserSession.UserName},  Computer: {this.UserSession.ComputerName} => {EventStr}";
    else
      EventStr1 = "";
    return this._EventLogHelper.AddToTrace(EventStr1, TraceLevel, TraceFileName);
  }

  public long AddEvent(
    long ObjectID,
    long RelationID,
    int CategoryType,
    long CategoryID,
    string ObjectName,
    string Note,
    ActionType EventType,
    EventlogRecordType AuditType)
  {
    return this._EventLogHelper.AddEvent(ObjectID, RelationID, CategoryType, CategoryID, ObjectName, Note, EventType, AuditType, this.UserSession.UserID, this.UserSession.ComputerName, (IUserSession) this.UserSession);
  }

  public long LastEventID => this._LastEventID;
}
