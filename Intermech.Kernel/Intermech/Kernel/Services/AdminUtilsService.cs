// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.AdminUtilsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Kernel;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Objects;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.GlobalIndex;
using Intermech.Interfaces.Snapshots;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services.ScheduledTasks;
using Intermech.Kernel.Snapshots;
using Intermech.Ldap;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.Search.EditingContexts;
using Intermech.Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;


namespace Intermech.Kernel.Services;

public class AdminUtilsService : LongLifeObject, IAdminUtilsService
{
  private bool _Clearing;
  private string _ClearingUserName = string.Empty;
  private string _ClearingComputerName = string.Empty;
  private OperationStateInfo _ClearingState = new OperationStateInfo("");
  private bool _Indexing;
  private string _IndexingUserName = string.Empty;
  private string _IndexingComputerName = string.Empty;
  private OperationStateInfo _IndexingState = new OperationStateInfo("");
  private bool _RelationsSearch;
  private string _RelationsSearchUserName = string.Empty;
  private string _RelationsSearchComputerName = string.Empty;
  private OperationStateInfo _RelationsSearchState = new OperationStateInfo("");
  public static ServerRunModes ServerRunMode = ServerRunModes.None;
  internal static bool OptimizerStatisticsON = true;
  public const int BlanksLifetimeHours = 24;

  public void PrepareScheduledTasks()
  {
    IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    service.RegisterService((object) new RepairDataTask(this));
    service.RegisterService((object) new DeleteTrashTask(this));
    service.RegisterService((object) new RebuildViewsTask(this));
    service.RegisterService((object) new SyncronizeDirectoryTask());
    service.RegisterService((object) new RemoveBlobsTask());
    service.RegisterService((object) new LicenseStatisticsTask());
    SystemDiagnosticsSettings diagnosticsSettings = new SystemDiagnosticsSettings();
    SystemDiagnosticsTask systemDiagnosticsTask = new SystemDiagnosticsTask(diagnosticsSettings);
    service.RegisterService((object) systemDiagnosticsTask);
    ServerServices.AddService(typeof (ISystemDiagnosticsTask), (object) systemDiagnosticsTask);
    (ServerServices.GetService(typeof (ICustomServices)) as ICustomServices).AddService(typeof (ISystemDiagnosticsSettings), (object) diagnosticsSettings);
    ServerServices.AddService(typeof (ISystemDiagnosticsSettings), (object) diagnosticsSettings);
  }

  public bool GetOptimizerStatisticsFlag() => AdminUtilsService.OptimizerStatisticsON;

  public void SetOptimizerStatisticsFlag(bool flag, Guid sessionGUID)
  {
    if (AdminUtilsService.OptimizerStatisticsON == flag)
      return;
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13687(1343090606));
    string Note = !flag ? LocalizationHolder.rm.GetString("StatisticOFF") : LocalizationHolder.rm.GetString("StatisticON");
    sessionById.EventLogHelper.AddEvent(0L, 0L, 14, 0L, LocalizationHolder.rm.GetString("StatisticEvent"), Note, ActionType.EditProperties, EventlogRecordType.Information, sessionById.UserID, sessionById.ComputerName, (IUserSession) sessionById);
    lock (this)
      AdminUtilsService.OptimizerStatisticsON = flag;
    sessionById.Configurations.WriteBool("KERNEL", "PERFORMANCE", "OPTIM_STAT", flag, 0L);
  }

  public DataTable GetOptimizerStatistics(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13688(166403988));
    sessionById.DataManager.ExecuteNonQuery("DELETE FROM IMS_OPTIMIZER_STAT WHERE F_OBJECT_TYPE > -1 AND (NOT EXISTS(SELECT * FROM IMS_OBJECT_TYPES WHERE IMS_OBJECT_TYPES.F_OBJECT_TYPE = IMS_OPTIMIZER_STAT.F_OBJECT_TYPE))");
    sessionById.DataManager.ExecuteNonQuery("DELETE FROM IMS_OPTIMIZER_STAT WHERE F_RELATION_TYPE > -1 AND (NOT EXISTS(SELECT * FROM IMS_RELATION_TYPES WHERE IMS_RELATION_TYPES.F_RELATION_TYPE = IMS_OPTIMIZER_STAT.F_RELATION_TYPE))");
    DataTable datatable = sessionById.DataManager.ExecuteDataTable("SELECT F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, SUM(F_READ) F_READ, SUM(F_SEEK) F_SEEK, SUM(F_WRITE) F_WRITE, SUM(F_READ_DURATION) F_READ_DURATION, SUM(F_SEEK_DURATION) F_SEEK_DURATION, SUM(F_WRITE_DURATION) F_WRITE_DURATION FROM IMS_OPTIMIZER_STAT GROUP BY F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE");
    datatable.TableName = "IMS_OPTIMIZER_STAT";
    datatable.Columns.Add("F_OPTIMIZED", typeof (int));
    for (int index = datatable.Rows.Count - 1; index >= 0; --index)
    {
      int int32_1 = Convert.ToInt32(datatable.Rows[index]["F_ATTRIBUTE_ID"]);
      int int32_2 = Convert.ToInt32(datatable.Rows[index]["F_OBJECT_TYPE"]);
      int int32_3 = Convert.ToInt32(datatable.Rows[index]["F_RELATION_TYPE"]);
      IDBAttributeType dbAttributeType = (IDBAttributeType) null;
      if (int32_2 > -1)
        dbAttributeType = (IDBAttributeType) sessionById.GetObjectType(int32_2).Attributes.GetAttributeByID(int32_1, false);
      if (int32_3 > -1)
        dbAttributeType = (IDBAttributeType) sessionById.GetRelationType(int32_3).Attributes.GetAttributeByID(int32_1, false);
      if (dbAttributeType == null)
      {
        dbAttributeType = sessionById.GetAttributeType(int32_1, false);
        if (dbAttributeType == null)
        {
          sessionById.DataManager.ExecuteNonQuery($"DELETE FROM IMS_OPTIMIZER_STAT WHERE F_ATTRIBUTE_ID = {int32_1} AND F_OBJECT_TYPE = {int32_2} AND F_RELATION_TYPE = {int32_3}");
          continue;
        }
      }
      datatable.Rows[index][datatable.Columns.Count - 1] = (object) (int) dbAttributeType.OptimizationMode;
    }
    datatable.AcceptChanges();
    DataSetProcessor.FillCaptions(datatable);
    return datatable;
  }

  public void ClearStatistics(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13689(2123293389));
    sessionById.DataManager.ExecuteNonQuery("DELETE FROM IMS_OPTIMIZER_STAT");
  }

  public void ReloadCache(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13690(720224066));
    (sessionById.DBCache as CacheDataset).CacheLoaded = false;
    (sessionById.DBCache as CacheDataset).LoadTables(sessionById.DataManager);
    sessionById.DBCache.ClearUsersCache();
    sessionById.DBCache.LoadFilePrototypes((IUserSession) sessionById, -1);
    sessionById.DBCache.ReloadPossibleValuesCache((IUserSession) sessionById);
    DBMeasureObject.LoadMeasuresList((IUserSession) sessionById);
    (UserSession.Sessions as UserSessionCollection).SetDBSecurityClearCacheFlag(0L);
    RelationTypeSecurity.InitDontCacheAccess4Types(sessionById);
    (sessionById.DBCache as CacheDataset).FillSyncParentObjectTypes(sessionById.DataManager);
    (ServerServices.GetService(typeof (IContainerService)) as IContainerService).ReloadCache((IUserSession) sessionById);
    this.ReloadServerSwitches(sessionGUID);
    (ServerServices.GetService(typeof (IDelayedUpdaterService)) as IDelayedUpdaterService).ReloadRolesCache();
  }

  public void ReloadServerSwitches(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13691(596185090));
    ServerConsts.CheckAttributeLCStepSecurity = sessionById.Configurations.ReadInteger("KERNEL", "SECURITY", "CHECK_ATTR_LCACCESS", 0L, DBConfigMode.GlobalOnly) != 0L;
    ServerConsts.WrongPasswordsLimit = Convert.ToInt32(sessionById.Configurations.ReadInteger("KERNEL", "SECURITY", "WRONG_PSW_COUNT", 0L, DBConfigMode.GlobalOnly));
    ServerConsts.CryptMethod = Convert.ToChar(sessionById.Configurations.ReadString("KERNEL", "SECURITY", "CRYPTO_METHOD", Convert.ToString(CryptHelper.SHA1Crypt), DBConfigMode.GlobalOnly));
    if (!(ServerServices.GetService(typeof (IEventLogHelper)) is EventLogHelper service))
      return;
    service.OnServerSettingsReload((IUserSession) sessionById);
  }

  public string[] GetServerConfigInfo()
  {
    ArrayList arrayList = new ArrayList();
    string empty = string.Empty;
    foreach (string allKey in ConfigurationManager.AppSettings.AllKeys)
    {
      if (allKey.ToString().ToLower().IndexOf("password") < 0)
        arrayList.Add((object) $"\t<add key=\"{allKey.ToString()}\" value=\"{ConfigurationManager.AppSettings.Get(allKey)}\"/>");
    }
    return (string[]) arrayList.ToArray(typeof (string));
  }

  public void ReloadMeasuresList(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13692(1241946316));
    DBMeasureObject.LoadMeasuresList((IUserSession) sessionById);
  }

  internal void RebuildAllViews()
  {
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (RebuildAllViews)) as UserSession;
    try
    {
      this.WriteLine("Начато перестроение всех представлений данных...", UtilsOutputMode.Both, sessionTemporaryClone);
      this.RebuildObjectsView(sessionTemporaryClone.SessionGUID, -1);
      this.WriteLine("Перестроение представлений данных для типов объектов...", UtilsOutputMode.Both, sessionTemporaryClone);
      DataTable dataTable1 = sessionTemporaryClone.GetObjectTypeCollection(-2).Select(string.Empty);
      for (int index = 0; index < dataTable1.Rows.Count; ++index)
      {
        IDBObjectType objectType = sessionTemporaryClone.GetObjectType(Convert.ToInt32(dataTable1.Rows[index]["F_OBJECT_TYPE"]), false);
        if (objectType != null)
        {
          try
          {
            objectType.RebuildView();
          }
          catch (Exception ex)
          {
            this.WriteLine($"Ошибка перестроения представления данных для типа объектов {objectType.ObjectTypeName}: {ex.Message}", UtilsOutputMode.Both, sessionTemporaryClone);
          }
        }
      }
      this.WriteLine("Перестроение представлений данных для типов связей...", UtilsOutputMode.Both, sessionTemporaryClone);
      DataTable dataTable2 = sessionTemporaryClone.GetRelationTypeCollection().Select(string.Empty);
      for (int index = 0; index < dataTable2.Rows.Count; ++index)
      {
        IDBRelationType relationType = sessionTemporaryClone.GetRelationType(Convert.ToInt32(dataTable2.Rows[index]["F_RELATION_TYPE"]), false);
        if (relationType != null)
        {
          try
          {
            relationType.RebuildView();
          }
          catch (Exception ex)
          {
            this.WriteLine($"Ошибка перестроения представления данных для типа связей {relationType.Description}: {ex.Message}", UtilsOutputMode.Both, sessionTemporaryClone);
          }
        }
      }
      this.WriteLine("Перестроение представлений данных завершено.", UtilsOutputMode.Both, sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (RebuildAllViews));
    }
  }

  public void RebuildObjectsView(Guid sessionGUID) => this.RebuildObjectsView(sessionGUID, -1);

  public void RebuildObjectsView(Guid sessionGUID, int objectTypeID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager db = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13693(264754815));
    string str1;
    if (objectTypeID < 0)
    {
      StringBuilder stringBuilder = new StringBuilder();
      DataTable table = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES");
      for (int index = 0; index < table.Rows.Count; ++index)
      {
        if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
          stringBuilder.Append(table.Rows[index]["F_OBJECT_TYPE"].ToString() + ",");
      }
      if (stringBuilder.Length == 0)
      {
        str1 = string.Empty;
      }
      else
      {
        stringBuilder[stringBuilder.Length - 1] = ')';
        str1 = " AND F_OBJECT_TYPE NOT IN (" + stringBuilder.ToString();
      }
    }
    else
      str1 = " AND F_OBJECT_TYPE = " + objectTypeID.ToString();
    try
    {
      DataTable table = sessionById.DBCache.GetTable("IMS_ATTRIBUTES");
      List<string> indexes = new List<string>();
      if (objectTypeID < 0)
        sessionById.QueryBuilder.RebuildTypedView("IMS_OBJECTS_VIEW", table, AttributeSourceTypes.Object, db, false, true, true, indexes);
      DataTable dataTable = db.ExecuteDataTable("SELECT * FROM IMS_OBJECTS_VIEW WHERE F_OBJECT_ID = -1");
      sessionById.StartTransaction();
      try
      {
        db.SetAdminCommandTimeout();
        if (objectTypeID < 0)
          db.ExecuteNonQuery("DELETE FROM IMS_OBJECTS_VIEW");
        db.ExecuteNonQuery("INSERT INTO IMS_OBJECTS_VIEW (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID)SELECT F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, (SELECT F_GUID FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID),(SELECT CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = IMS_OBJECTS.F_OBJECT_ID), F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID > 0" + str1);
        db.ExecuteNonQuery("INSERT INTO IMS_OBJECTS_VIEW (F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, F_GUID, CAPTION, F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID)SELECT F_OBJECT_ID, F_ID, F_LC_STEP, F_VERSION_ID, F_CHKOUT_BY, F_OBJECT_VER_TYPE, F_OBJECT_TYPE, F_OWNER_ID, F_LEVEL_ID, (SELECT F_GUID FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = -IMS_OBJECTS.F_OBJECT_ID),(SELECT F_WORK_CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = -IMS_OBJECTS.F_OBJECT_ID), F_OBJ_CREATE, F_PROJECT_ID, F_MODIFICATION_ID, F_BASE_VERSION, F_SITE_ID, F_ACCESS, F_CREATOR_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID < 0 AND F_OBJECT_VER_TYPE > -1" + str1);
        foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
        {
          if (Convert.ToInt32(row["F_INVIEW"]) != 0)
          {
            IDBAttributeType attributeType = sessionById.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
            if (attributeType.AttributeID > 0)
            {
              string str2 = "F" + attributeType.AttributeID.ToString();
              IDbManager dbManager = db;
              string commandText;
              if (!(db.DataProvider.Name != "Linter"))
                commandText = string.Format("UPDATE {0} JOIN IMS_OBJECT_ATTRS SET {1} = {2}  WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {3} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND {4} IS NOT NULL", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) SqlHelper.MakeCASTString("IMS_OBJECT_ATTRS", attributeType.TextFieldName, attributeType, db.DataProvider), (object) attributeType.AttributeID, (object) attributeType.TextFieldName);
              else
                commandText = string.Format("UPDATE {0} SET {1} = (SELECT {2} FROM IMS_OBJECT_ATTRS WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {3} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND {2} IS NOT NULL)", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.TextFieldName, (object) attributeType.AttributeID);
              dbManager.ExecuteNonQuery(commandText);
              if (dataTable.Columns.IndexOf(str2 + "ID") > -1)
                db.ExecuteNonQuery(db.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID = (SELECT F_INTEGER_VALUE FROM IMS_OBJECT_ATTRS WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND F_INTEGER_VALUE IS NOT NULL)", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_OBJECT_ATTRS SET {1}ID = IMS_OBJECT_ATTRS.F_INTEGER_VALUE WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND IMS_OBJECT_ATTRS.F_INTEGER_VALUE IS NOT NULL", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID));
              if (dataTable.Columns.IndexOf(str2 + "ID2") > -1)
                db.ExecuteNonQuery(db.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID2 = (SELECT F_DOUBLE_VALUE FROM IMS_OBJECT_ATTRS WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND F_DOUBLE_VALUE IS NOT NULL)", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_OBJECT_ATTRS SET {1}ID2 = IMS_OBJECT_ATTRS.F_DOUBLE_VALUE WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND IMS_OBJECT_ATTRS.F_DOUBLE_VALUE IS NOT NULL", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID));
              if (dataTable.Columns.IndexOf(str2 + "ID3") > -1)
                db.ExecuteNonQuery(db.DataProvider.Name != "Linter" ? string.Format("UPDATE {0} SET {1}ID3 = (SELECT F_DATE_VALUE FROM IMS_OBJECT_ATTRS WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND F_DATE_VALUE IS NOT NULL)", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID) : string.Format("UPDATE {0} JOIN IMS_OBJECT_ATTRS SET {1}ID3 = IMS_OBJECT_ATTRS.F_DATE_VALUE WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID = {0}.F_OBJECT_ID AND IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID = {2} AND IMS_OBJECT_ATTRS.F_INLIST_ID = 0 AND IMS_OBJECT_ATTRS.F_DATE_VALUE IS NOT NULL", (object) "IMS_OBJECTS_VIEW", (object) str2, (object) attributeType.AttributeID));
            }
          }
        }
        sessionById.Commit();
      }
      catch
      {
        sessionById.Rollback();
        throw;
      }
      finally
      {
        db.SetNormalCommandTimeout();
      }
    }
    catch (Exception ex)
    {
      sessionById.EventLog.AddToTrace(LocalizationHolder.rm.GetString("Kernel_594") + ex.Message, Consts.traceAlways, string.Empty);
      throw;
    }
  }

  private void Test(Guid sessionGUID)
  {
    int length1 = 10000;
    int num = 1337;
    Random random = new Random();
    char[] sourceArray = new char[length1];
    IDBObject dbObject = (UserSession.GetSessionByID(sessionGUID) as UserSession).GetObject(75070L);
    IDBAttribute attributeById = dbObject.GetAttributeByID(num);
    (attributeById as IMemoWriter).OpenMemo(length1);
    int length2 = random.Next(1, length1);
    int sourceIndex = 0;
    char[] chArray1 = new char[length2];
    while (true)
    {
      if (length2 > length1 - sourceIndex)
        length2 = length1 - sourceIndex;
      char[] chArray2 = new char[length2];
      Array.Copy((Array) sourceArray, sourceIndex, (Array) chArray2, 0, length2);
      if ((attributeById as IMemoWriter).WriteDataBlock(chArray2))
        sourceIndex += length2;
      else
        break;
    }
    long objectId = dbObject.ObjectID;
    IDBAttribute byId = dbObject.Attributes.FindByID(num);
    int length3 = random.Next(1, length1);
    int destinationIndex = 0;
    (byId as IMemoReader).OpenMemo(length3);
    chArray1 = new char[length3];
    char[] destinationArray = new char[length1];
    while (destinationIndex < length1)
    {
      if (length3 > length1 - destinationIndex)
        length3 = length1 - destinationIndex;
      Array.Copy((Array) (byId as IMemoReader).ReadDataBlock(length3), 0, (Array) destinationArray, destinationIndex, length3);
      destinationIndex += length3;
      if (destinationIndex == length1)
        break;
    }
  }

  private void RepairCaptions(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    DataTable dataTable = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES").Copy();
    int num = 0;
    foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
    {
      sessionById.StartTransaction();
      try
      {
        int int32 = Convert.ToInt32(row1["F_CAPTION_ATTRIBUTE"]);
        if (int32 > 0)
        {
          IDBObjectType objectType = sessionById.GetObjectType(Convert.ToInt32(row1["F_OBJECT_TYPE"]));
          objectType.CaptionAttribute = 0;
          foreach (DataRow row2 in (InternalDataCollectionBase) sessionById.DataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + objectType.ObjectType.ToString()).Rows)
          {
            DBObject dbObject = sessionById.GetObject(Convert.ToInt64(row2[0])) as DBObject;
            try
            {
              dbObject.SetCaption(string.Empty);
            }
            catch (Exception ex)
            {
              Console.WriteLine("Error: {0} on object N{1}", (object) ex.Message, (object) dbObject.ObjectID);
            }
          }
          objectType.CaptionAttribute = int32;
        }
        sessionById.Commit();
        Console.WriteLine("{0} from {1}", (object) ++num, (object) dataTable.Rows.Count);
      }
      catch
      {
        sessionById.Rollback();
        throw;
      }
    }
  }

  private int PurgeObjectByType(UserSession session1, string typeGuid)
  {
    int num = 0;
    IDBObjectType objectType = session1.GetObjectType(new Guid(typeGuid), true);
    DataTable dataTable = session1.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = {objectType.ObjectType} ORDER BY F_OBJECT_ID");
    Console.WriteLine("Deleting {0} object(s) of '{1}' type..", (object) dataTable.Rows.Count, (object) objectType.ObjectTypeName);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (session1.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBObject dbObject)
      {
        dbObject.Purge((long) (Consts.PurgeMode | 16 /*0x10*/));
        ++num;
      }
    }
    return num;
  }

  private void SaveData(string number, IDBObject obj)
  {
    string filename = "G:\\Mail\\Файлы\\attributes" + number;
    obj.Caption = "Док-т" + number;
    IDBAttribute attributeByGuid = obj.GetAttributeByGuid(new Guid("cad01493-306c-11d8-b4e9-00304f19f545"), true);
    if (attributeByGuid.ValuesCount > 0)
      attributeByGuid.ClearValues();
    attributeByGuid.Index = 0;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(filename);
    IPackedStream service = ServiceUtils.GetService<IPackedStream>((object) ApplicationServices.Container, true);
    using (MemoryStream memoryStream = new MemoryStream())
    {
      xmlDocument.Save((Stream) memoryStream);
      memoryStream.Position = 0L;
      using (MemoryStream outStream = new MemoryStream())
      {
        service.PackStream((Stream) outStream, (Stream) memoryStream, 9);
        IBlobWriter blobWriter = attributeByGuid as IBlobWriter;
        blobWriter.OpenBlob(new BlobInformation(memoryStream.Length, outStream.Length, DateTime.Now, "Типа атрибуты", ArcMethods.ZLibPacked, string.Empty), false);
        blobWriter.WriteDataBlock(outStream.ToArray());
      }
    }
  }

  public void FixECO_Context(IUserSession session)
  {
    Console.WriteLine("Исправление контекстов извещений начато...");
    IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"));
    IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545"));
    IDBEditingContextsServerService service = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
    DBRecordSetParams paramSet1 = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -6
    }, 0L, (object) null, -1);
    DataTable dataTable1 = objectCollection.Select(paramSet1);
    int num1 = 0;
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      if (Convert.ToInt64(dataTable1.Rows[index1][1]) != 0L)
      {
        ++num1;
      }
      else
      {
        long num2 = Math.Abs(Convert.ToInt64(dataTable1.Rows[index1][0]));
        DataTable dataTable2 = (session as UserSession).DataManager.ExecuteDataTable("SELECT * FROM IMS_VERSIONS_CONTEXT WHERE F_CONTEXT_ID = " + num2.ToString());
        DBRecordSetParams paramSet2 = new DBRecordSetParams((ConditionStructure[]) null, new object[2]
        {
          (object) -22,
          (object) session.IdentHelper.CompositionVersionID
        }, 0L, (object) null, -1);
        DataTable dataTable3 = relationCollection.ConsistFrom(paramSet2, num2);
        for (int index2 = 0; index2 < dataTable3.Rows.Count; ++index2)
        {
          if (dataTable3.Rows[index2][1] != null && !(dataTable3.Rows[index2][1].ToString() == string.Empty) && dataTable2.Select("F_OBJECT_ID = " + dataTable3.Rows[index2][1].ToString()).Length == 0)
          {
            DataRow[] dataRowArray = dataTable2.Select("F_ID = " + dataTable3.Rows[index2][0].ToString());
            if (dataRowArray.Length == 0)
            {
              Console.WriteLine("В контексте {0} нет версий объекта {1}.", (object) num2, dataTable3.Rows[index2][0]);
              EditingContextsObjectContainer editingContextsObject = DBObject.EditingContextsServerService.GetEditingContextsObject((object) session, num2, false, false);
              try
              {
                if (!service.AddToContext((object) session, num2, editingContextsObject.ModificationID, Convert.ToInt64(dataTable3.Rows[index2][0]), Convert.ToInt64(dataTable3.Rows[index2][1]), true, true))
                  Console.WriteLine("Не получилось добавить версию объекта {1} в контекст {0}.", (object) num2, dataTable3.Rows[index2][1]);
              }
              catch (Exception ex)
              {
                Console.WriteLine("Ошибка вставки в контекст {0} версии объекта {1}: {2}", (object) num2, dataTable3.Rows[index2][1], (object) ex.Message);
              }
            }
            else if (dataRowArray.Length == 1)
            {
              Console.WriteLine("В контексте {0} находится версия {1} вместо версии {2}.", (object) num2, dataRowArray[0]["F_OBJECT_ID"], dataTable3.Rows[index2][1]);
              EditingContextsObjectContainer editingContextsObject = DBObject.EditingContextsServerService.GetEditingContextsObject((object) session, num2, false, false);
              try
              {
                if (!service.DeleteFromContext((object) session, num2, Convert.ToInt64(dataRowArray[0]["F_OBJECT_ID"]), true, true))
                  Console.WriteLine("Ошибка удаления из контекста {0} версии объекта {1}.", (object) num2, dataRowArray[0]["F_OBJECT_ID"]);
              }
              catch (Exception ex)
              {
                Console.WriteLine("Ошибка удаления из контекста {0} версии объекта {1}: {2}", (object) num2, dataRowArray[0]["F_OBJECT_ID"], (object) ex.Message);
              }
              try
              {
                if (!service.AddToContext((object) session, num2, editingContextsObject.ModificationID, Convert.ToInt64(dataTable3.Rows[index2][0]), Convert.ToInt64(dataTable3.Rows[index2][1]), true, true))
                  Console.WriteLine("Не получилось добавить версию объекта {1} в контекст {0}.", (object) num2, dataTable3.Rows[index2][1]);
              }
              catch (Exception ex)
              {
                Console.WriteLine("Ошибка вставки в контекст {0} версии объекта {1}: {2}", (object) num2, dataTable3.Rows[index2][1], (object) ex.Message);
              }
            }
            else
              Console.WriteLine("В контексте {0} более одной версии объекта {1}.", (object) num2, dataTable3.Rows[index2][0]);
          }
        }
      }
    }
    (session.GetCustomService(typeof (IEditingContextServerService)) as IEditingContextServerService).RemoveNotVersionedObjectsFromAllEditingContexts(session.SessionGUID);
    if (num1 > 0)
      Console.WriteLine("Исправление контекстов извещений завершено. Пропущено взятых на изменение извещений: {0}", (object) num1);
    else
      Console.WriteLine("Исправление контекстов извещений завершено.");
  }

  public string[] RepairData(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    CacheDataset cacheDataset = sessionById.IsAdmin ? sessionById.DBCache as CacheDataset : throw new KernelExceptionID(sc_13686.ssp_appserver_13694(1507387941));
    IDbManager dataManager = sessionById.DataManager;
    sessionById.EventLog.AddToTrace("\r\n\r\nНачата проверка базы данных на наличие ошибок...\r\n", Consts.traceAlways, "RepairData.log");
    IDatabaseLocker service1 = ServerServices.GetService(typeof (IDatabaseLocker)) as IDatabaseLocker;
    DatabaseLockInfo databaseLockInfo = service1.Lock((IUserSession) sessionById, nameof (RepairData), TimeSpan.FromDays(2.0));
    if (databaseLockInfo.Success)
    {
      try
      {
        sessionById.EventLog.AddToTrace("Check relations...", Consts.traceAlways, "RepairData.log");
        dataManager.SetAdminCommandTimeout();
        DataTable dataTable1;
        try
        {
          dataTable1 = dataManager.ExecuteDataTable("select A.F_PRJLINK_ID from IMS_RELATIONS A where EXISTS(SELECT * FROM IMS_RELATIONS B WHERE B.F_PRJLINK_ID = -A.F_PRJLINK_ID AND ABS(A.F_PROJ_ID) <> ABS(B.F_PROJ_ID)) AND (A.F_PRJLINK_ID < 0)");
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        if (dataTable1.Rows.Count > 0)
        {
          sessionById.EventLog.AddToTrace($"Found {dataTable1.Rows.Count} relations with wrong ProjID...", Consts.traceAlways, "RepairData.log");
          for (int index = 0; index < dataTable1.Rows.Count; ++index)
          {
            if (sessionById.GetRelation(Convert.ToInt64(dataTable1.Rows[index][0]), false) is DBRelation relation)
              relation.GenNewRelationID();
          }
        }
        dataManager.SetAdminCommandTimeout();
        try
        {
          dataTable1 = dataManager.ExecuteDataTable("select F_PRJLINK_ID from IMS_RELATIONS R WHERE NOT EXISTS(SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_ID = R.F_PART_ID)");
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        if (dataTable1.Rows.Count > 0)
        {
          sessionById.EventLog.AddToTrace($"Found {dataTable1.Rows.Count} relations with bad F_PART_ID...", Consts.traceAlways, "RepairData.log");
          for (int index = 0; index < dataTable1.Rows.Count; ++index)
          {
            if (sessionById.GetRelation(Convert.ToInt64(dataTable1.Rows[index][0]), false) is DBRelation relation)
              relation.DeleteWithoutCheck((long) Consts.PurgeMode);
          }
        }
        sessionById.EventLog.AddToTrace("Reload tables...", Consts.traceAlways, "RepairData.log");
        cacheDataset.ReloadTables((IUserSession) sessionById, dataManager, new string[4]
        {
          "IMS_OBJECT_TYPES",
          "IMS_OBJTYPES_TREE",
          "IMS_ATTR4OBJ_TYPES",
          "IMS_ATTR_GROUPS"
        });
        DataTable dataTable2 = cacheDataset.GetTable("IMS_ATTR4OBJ_TYPES").Copy();
        DataTable dataTable3 = cacheDataset.GetTable("IMS_OBJTYPES_TREE").Copy();
        sessionById.EventLog.AddToTrace("Public flag rebuild...", Consts.traceAlways, "RepairData.log");
        List<Tuple<int, int, int>> tupleList = new List<Tuple<int, int, int>>();
        int columnIndex = dataTable2.Columns.IndexOf("F_PUBLIC");
        for (int index1 = 0; index1 < dataTable2.Rows.Count; ++index1)
        {
          if (Convert.ToInt32(dataTable2.Rows[index1][columnIndex]) == 2)
          {
            int int32_1 = Convert.ToInt32(dataTable2.Rows[index1]["F_OBJECT_TYPE"]);
            int objectTypeParentId = cacheDataset.GetObjectTypeParentID(int32_1);
            int int32_2 = Convert.ToInt32(dataTable2.Rows[index1]["F_ATTRIBUTE_ID"]);
            DataRow[] dataRowArray = dataTable3.Select("F_PARENT_ID = " + int32_1.ToString());
            if (dataRowArray.Length != 0)
            {
              for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
              {
                IDBObjectType objectType = sessionById.GetObjectType(Convert.ToInt32(dataRowArray[index2]["F_OBJECT_TYPE"]));
                if (!(objectType.Attributes.GetAttributeByID(int32_2, false) is IDBAttributeType4Object))
                  tupleList.Add(Tuple.Create<int, int, int>(objectType.ObjectType, int32_2, int32_1));
              }
            }
            if (objectTypeParentId <= 0 || !(sessionById.GetObjectType(objectTypeParentId).Attributes.GetAttributeByID(int32_2, false) is IDBAttributeType4Object attributeById) || attributeById.InheritMode == InheritModes.Private)
            {
              dataManager.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_PUBLIC = :inhID WHERE F_OBJECT_TYPE = :otID AND F_ATTRIBUTE_ID = :atID", dataManager.Parameter("inhID", (object) 1), dataManager.Parameter("otID", (object) int32_1), dataManager.Parameter("atID", (object) int32_2));
              cacheDataset.ChangeTableValue($"F_OBJECT_TYPE = {int32_1} AND F_ATTRIBUTE_ID = {int32_2}", "IMS_ATTR4OBJ_TYPES", "F_PUBLIC", (object) 2, (IUserSession) sessionById);
            }
          }
        }
        if (tupleList.Count > 0)
        {
          for (int index = 0; index < tupleList.Count; ++index)
          {
            Tuple<int, int, int> tuple = tupleList[index];
            if (sessionById.GetObjectType(tuple.Item3).Attributes.GetAttributeByID(tuple.Item2, false) is DBAttributeType4Object attributeById)
            {
              sessionById.EventLog.AddToTrace("Производится добавление атрибута: " + attributeById.ObjectName, Consts.traceAlways, "RepairData.log");
              sessionById.StartTransaction();
              try
              {
                attributeById.AddInheritAttribute(-1, false);
                sessionById.Commit();
              }
              catch (Exception ex)
              {
                sessionById.Rollback();
                sessionById.EventLog.AddToTrace("Ошибка: " + ex.Message, Consts.traceAlways, "RepairData.log");
                sessionById.EventLog.AddToTrace(ex.StackTrace, Consts.traceAlways, "RepairData.log");
              }
            }
          }
        }
        if (cacheDataset.GetTable("IMS_ATTR_GROUPS").Columns.IndexOf("F_PARENT_ID") < 0)
        {
          if (dataManager.DataProvider.Name == "Sql")
            dataManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ADD F_PARENT_ID INTEGER NOT NULL DEFAULT 0");
          else if (dataManager.DataProvider.Name == "Oracle")
            dataManager.ExecuteNonQuery("ALTER TABLE IMS_ATTR_GROUPS ADD F_PARENT_ID INTEGER DEFAULT 0 NOT NULL");
        }
        sessionById.EventLog.AddToTrace("Clearing IMS_ATTR_IN_GROUPS...", Consts.traceAlways, "RepairData.log");
        dataManager.ExecuteNonQuery("delete from IMS_ATTR_IN_GROUPS where not exists(select F_ATTRIBUTE_ID from IMS_ATTRIBUTES WHERE IMS_ATTRIBUTES.F_ATTRIBUTE_ID = IMS_ATTR_IN_GROUPS.F_ATTRIBUTE_ID)");
        sessionById.EventLog.AddToTrace("Formula links rebuild...", Consts.traceAlways, "RepairData.log");
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
        {
          if (Convert.ToInt32(row["F_COMPUTED"]) != 0)
          {
            DBAttributeType attributeType = sessionById.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"])) as DBAttributeType;
            try
            {
              attributeType.SaveFormulaLinks(Convert.ToInt32(row["F_OBJECT_TYPE"]), -1, row["F_FORMULA"].ToString(), Consts.Attribute4Formula, false);
            }
            catch (Exception ex)
            {
              sessionById.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_596"), (object) attributeType.Name, (object) row["F_OBJECT_TYPE"].ToString(), (object) ex.Message), Consts.traceAlways, "RepairData.log");
            }
          }
        }
        DataTable table1 = cacheDataset.GetTable("IMS_ATTR4RELATION_TYPES");
        sessionById.DBCache.EnterReadLocker();
        try
        {
          foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
          {
            if (Convert.ToInt32(row["F_COMPUTED"]) != 0)
            {
              DBAttributeType attributeType = sessionById.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"])) as DBAttributeType;
              try
              {
                attributeType.SaveFormulaLinks(-1, Convert.ToInt32(row["F_RELATION_TYPE"]), row["F_FORMULA"].ToString(), Consts.Attribute4Formula, false);
              }
              catch (Exception ex)
              {
                sessionById.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_597"), (object) attributeType.Name, (object) row["F_RELATION_TYPE"].ToString(), (object) ex.Message), Consts.traceAlways, "RepairData.log");
              }
            }
          }
        }
        finally
        {
          sessionById.DBCache.ExitReadLocker();
        }
        DataTable table2 = cacheDataset.GetTable("IMS_ATTRIBUTES");
        sessionById.DBCache.EnterReadLocker();
        try
        {
          foreach (DataRow row in (InternalDataCollectionBase) table2.Rows)
          {
            if (Convert.ToInt32(row["F_COMPUTED"]) != 0)
            {
              DBAttributeType attributeType = sessionById.GetAttributeType(Convert.ToInt32(row["F_ATTRIBUTE_ID"])) as DBAttributeType;
              try
              {
                attributeType.SaveFormulaLinks(-1, -1, row["F_FORMULA"].ToString(), Consts.Attribute4Formula, false);
              }
              catch (Exception ex)
              {
                sessionById.EventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_598"), (object) attributeType.Name, (object) ex.Message), Consts.traceAlways, "RepairData.log");
              }
            }
          }
        }
        finally
        {
          sessionById.DBCache.ExitReadLocker();
        }
        cacheDataset.ReloadTables((IUserSession) sessionById, dataManager, new string[1]
        {
          "IMS_FORMULA_ATTRS"
        });
        dataManager.ExecuteNonQuery($"UPDATE IMS_ATTRIBUTES SET F_SIZE_TYPE = -1 WHERE (F_SIZE_TYPE = 0 OR F_SIZE_TYPE IS NULL) AND (F_ATTRIBUTE_TYPE = {Convert.ToInt32((object) FieldTypes.ftMeasured).ToString()})");
        sessionById.EventLog.AddToTrace("Blanks fixing...", Consts.traceAlways, "RepairData.log");
        dataManager.SetAdminCommandTimeout();
        try
        {
          dataManager.ExecuteNonQuery(sc_13686.ssp_appserver_13695());
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        string str1 = ConfigurationManager.AppSettings.Get("CheckDB.FixCaptions");
        int num1;
        switch (str1)
        {
          case null:
            num1 = 0;
            break;
          case "1":
            num1 = 1;
            break;
          default:
            num1 = str1.ToLower() == "true" ? 1 : 0;
            break;
        }
        bool flag = num1 != 0;
        foreach (DataRow dataRow in cacheDataset.GetTable("IMS_OBJECT_TYPES").Copy().Select(string.Empty))
        {
          string attributesTableName = cacheDataset.GetAttributesTableName(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
          if (attributesTableName != "IMS_OBJECT_ATTRS")
          {
            try
            {
              dataManager.ExecuteScalar($"SELECT * FROM {attributesTableName} WHERE F_OBJECT_ID = -1");
            }
            catch
            {
              ObjectTypeOptions int32 = (ObjectTypeOptions) Convert.ToInt32(dataRow["F_OPTIONS"]);
              dataManager.DataProvider.CreateObjectsTypeAttrView(attributesTableName, dataManager);
              dataManager.DataProvider.CreateObjectsTypeAttrIndexes(attributesTableName, dataManager, (int32 & ObjectTypeOptions.AttributesIndex) == ObjectTypeOptions.AttributesIndex);
              sessionById.StartTransaction();
              try
              {
                dataManager.SetAdminCommandTimeout();
                dataManager.ExecuteNonQuery($"INSERT INTO {attributesTableName} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE) SELECT F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID, F_INTEGER_VALUE, F_STRING_VALUE, F_DOUBLE_VALUE, F_DATE_VALUE FROM IMS_OBJECT_ATTRS A WHERE A.F_OBJECT_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = {dataRow["F_OBJECT_TYPE"].ToString()})");
                sessionById.Commit();
              }
              catch
              {
                sessionById.Rollback();
                throw;
              }
              finally
              {
                dataManager.SetNormalCommandTimeout();
              }
            }
          }
          if (Convert.ToInt32(dataRow["F_CAPTION_ATTRIBUTE"]) > 0 & flag)
          {
            sessionById.StartTransaction();
            try
            {
              dataManager.SetAdminCommandTimeout();
              dataManager.ExecuteNonQuery($"update IMS_GUID set CAPTION = (SELECT F_STRING_VALUE FROM {attributesTableName} A WHERE A.F_ATTRIBUTE_ID = :attrID AND A.F_OBJECT_ID = IMS_GUID.F_OBJECT_ID AND A.F_INLIST_ID = 0)" + "WHERE IMS_GUID.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_GUID.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_TYPE = :typeID)", dataManager.Parameter("attrID", (object) Convert.ToInt32(dataRow["F_CAPTION_ATTRIBUTE"])), dataManager.Parameter("typeID", (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"])));
              dataManager.ExecuteNonQuery($"update IMS_GUID set F_WORK_CAPTION = (SELECT F_STRING_VALUE FROM {attributesTableName} A WHERE A.F_ATTRIBUTE_ID = :attrID AND A.F_OBJECT_ID = IMS_GUID.F_OBJECT_ID AND A.F_INLIST_ID = 0)" + "WHERE IMS_GUID.F_OBJECT_ID IN (SELECT -F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = -IMS_GUID.F_OBJECT_ID AND IMS_OBJECTS.F_OBJECT_TYPE = :typeID)", dataManager.Parameter("attrID", (object) Convert.ToInt32(dataRow["F_CAPTION_ATTRIBUTE"])), dataManager.Parameter("typeID", (object) Convert.ToInt32(dataRow["F_OBJECT_TYPE"])));
              sessionById.Commit();
            }
            catch
            {
              sessionById.Rollback();
              throw;
            }
            finally
            {
              dataManager.SetNormalCommandTimeout();
            }
          }
        }
        sessionById.EventLog.AddToTrace("Owner ID fixing...", Consts.traceAlways, "RepairData.log");
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_GUID WHERE F_GUID = :guid", dataManager.Parameter("guid", (object) new Guid("cad0000d-306c-11d8-b4e9-00304f19f545")));
        sessionById.StartTransaction();
        try
        {
          dataManager.SetAdminCommandTimeout();
          dataManager.ExecuteNonQuery(string.Format(sc_13686.ssp_appserver_13696(), obj));
          dataManager.ExecuteNonQuery(string.Format(sc_13686.ssp_appserver_13697(), obj));
          sessionById.EventLog.AddToTrace("CheckoutBy fixing...", Consts.traceAlways, "RepairData.log");
          dataManager.ExecuteNonQuery("update IMS_OBJECTS SET F_CHKOUT_BY = (SELECT B.F_CHKOUT_BY FROM IMS_OBJECTS B WHERE B.F_OBJECT_ID = -IMS_OBJECTS.F_OBJECT_ID) where IMS_OBJECTS.F_OBJECT_ID < 0 AND (EXISTS(SELECT * FROM IMS_OBJECTS E WHERE E.F_OBJECT_ID = -IMS_OBJECTS.F_OBJECT_ID AND E.F_CHKOUT_BY <> IMS_OBJECTS.F_CHKOUT_BY))");
          sessionById.Commit();
        }
        catch
        {
          sessionById.Rollback();
          throw;
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        sessionById.EventLog.AddToTrace("Blanks deleting from views...", Consts.traceAlways, "RepairData.log");
        dataManager.ExecuteNonQuery(sc_13686.ssp_appserver_13698());
        DataTable dataTable4 = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES");
        for (int index3 = 0; index3 < dataTable4.Rows.Count; ++index3)
        {
          int int32 = Convert.ToInt32(dataTable4.Rows[index3]["F_OBJECT_TYPE"]);
          string[] updateTables = sessionById.DBCache.GetUpdateTables(-1, int32, -1);
          if (updateTables != null)
          {
            for (int index4 = 0; index4 < updateTables.Length; ++index4)
            {
              try
              {
                dataManager.ExecuteNonQuery($"delete from {updateTables[index4]} where F_OBJECT_VER_TYPE = -1");
              }
              catch
              {
              }
            }
          }
        }
        sessionById.EventLog.AddToTrace("Workspace fixing...", Consts.traceAlways, "RepairData.log");
        IDBObjectCollection objectCollection = sessionById.GetObjectCollection(sessionById.IdentHelper.WorkspaceTypeID);
        bool showPersonalObjects = sessionById.ShowPersonalObjects;
        sessionById.ShowPersonalObjects = true;
        try
        {
          DataTable dataTable5 = sessionById.GetObjectCollection(sessionById.IdentHelper.UsersTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
          {
            (object) -2
          }));
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(-7, RelationalOperators.Equal, (object) sessionById.IdentHelper.WorkspaceTypeID, LogicalOperators.NONE, 0, true)
          }, new ColumnDescriptor[2]
          {
            new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -8, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          });
          dataTable4 = objectCollection.Select(paramSet);
          foreach (DataRow row in (InternalDataCollectionBase) dataTable5.Rows)
          {
            if (dataTable4.Select("F_OWNER_ID = " + row[0].ToString()).Length == 0)
            {
              IDBObject dbObject = sessionById.GetObject(Convert.ToInt64(row[0]));
              ServerWorkspace serverWorkspace = objectCollection.Create() as ServerWorkspace;
              serverWorkspace._CanCreate = true;
              serverWorkspace.Caption = LocalizationHolder.rm.GetString("WorkspaceCaption");
              serverWorkspace.OwnerID = dbObject.ObjectID;
              serverWorkspace.CommitCreation(true);
              serverWorkspace.CreateSamples();
            }
          }
          foreach (DataRow row in (InternalDataCollectionBase) dataTable4.Rows)
          {
            if (sessionById.GetObject(Convert.ToInt64(row[0])) is IServerWorkspace serverWorkspace)
              serverWorkspace.CreateSamples();
          }
        }
        finally
        {
          sessionById.ShowPersonalObjects = showPersonalObjects;
        }
        sessionById.EventLog.AddToTrace("IMS_FORMULA_ATTRS cleaning...", Consts.traceAlways, "RepairData.log");
        dataManager.ExecuteNonQuery("DELETE FROM IMS_FORMULA_ATTRS WHERE F_OBJECT_TYPE > 0 AND (NOT EXISTS(SELECT IMS_OBJECT_TYPES.F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE IMS_OBJECT_TYPES.F_OBJECT_TYPE = IMS_FORMULA_ATTRS.F_OBJECT_TYPE))");
        dataManager.ExecuteNonQuery("DELETE FROM IMS_FORMULA_ATTRS WHERE F_RELATION_TYPE > 0 AND (NOT EXISTS(SELECT IMS_RELATION_TYPES.F_RELATION_TYPE FROM IMS_RELATION_TYPES WHERE IMS_RELATION_TYPES.F_RELATION_TYPE = IMS_FORMULA_ATTRS.F_RELATION_TYPE))");
        cacheDataset.ReloadTables((IUserSession) sessionById, dataManager, new string[1]
        {
          "IMS_FORMULA_ATTRS"
        });
        dataManager.ExecuteNonQuery("DELETE FROM IMS_LCSTART_DATE WHERE F_OBJECT_ID < 0 AND EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = ABS(IMS_LCSTART_DATE.F_OBJECT_ID))");
        sessionById.EventLog.AddToTrace("IMS_ATTRIBUTES cleaning...", Consts.traceAlways, "RepairData.log");
        foreach (DataRow dataRow in sessionById.DBCache.GetTable("IMS_ATTRIBUTES").Select($"F_ATTRIBUTE_TYPE = {8}"))
        {
          if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != string.Empty)
          {
            if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != Consts.CurrentUserFunction)
            {
              try
              {
                long int64 = Convert.ToInt64(dataRow["F_DEFAULT_VALUE"]);
                if (sessionById.GetObject(int64, false) == null)
                  dataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = " + dataRow["F_ATTRIBUTE_ID"].ToString());
              }
              catch
              {
                dataManager.ExecuteNonQuery("UPDATE IMS_ATTRIBUTES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = " + dataRow["F_ATTRIBUTE_ID"].ToString());
              }
            }
          }
        }
        foreach (DataRow dataRow in dataManager.ExecuteDataTable($"SELECT * FROM IMS_ATTR4OBJTYPE_VIEW WHERE F_ATTRIBUTE_TYPE = {8}").Select(""))
        {
          if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != string.Empty)
          {
            if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != Consts.CurrentUserFunction)
            {
              try
              {
                long int64 = Convert.ToInt64(dataRow["F_DEFAULT_VALUE"]);
                if (sessionById.GetObject(int64, false) == null)
                  dataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4OBJ_TYPES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = {dataRow["F_ATTRIBUTE_ID"]} AND F_OBJECT_TYPE = {dataRow["F_OBJECT_TYPE"]}");
              }
              catch
              {
                dataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4OBJ_TYPES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = {dataRow["F_ATTRIBUTE_ID"]} AND F_OBJECT_TYPE = {dataRow["F_OBJECT_TYPE"]}");
              }
            }
          }
        }
        foreach (DataRow dataRow in dataManager.ExecuteDataTable($"SELECT * FROM IMS_ATTR4RELTYPE_VIEW WHERE F_ATTRIBUTE_TYPE = {8}").Select(""))
        {
          if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != string.Empty)
          {
            if (dataRow["F_DEFAULT_VALUE"].ToString().Trim() != Consts.CurrentUserFunction)
            {
              try
              {
                long int64 = Convert.ToInt64(dataRow["F_DEFAULT_VALUE"]);
                if (sessionById.GetObject(int64, false) == null)
                  dataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = {dataRow["F_ATTRIBUTE_ID"]} AND F_RELATION_TYPE = {dataRow["F_RELATION_TYPE"]}");
              }
              catch
              {
                dataManager.ExecuteNonQuery($"UPDATE IMS_ATTR4RELATION_TYPES SET F_DEFAULT_VALUE = NULL WHERE F_ATTRIBUTE_ID = {dataRow["F_ATTRIBUTE_ID"]} AND F_RELATION_TYPE = {dataRow["F_RELATION_TYPE"]}");
              }
            }
          }
        }
        sessionById.DBCache.ReloadTables((IUserSession) sessionById, dataManager, "IMS_ATTRIBUTES", "IMS_ATTR4OBJ_TYPES", "IMS_ATTR4RELATION_TYPES");
        sessionById.EventLog.AddToTrace("Base versions checking...", Consts.traceAlways, "RepairData.log");
        dataManager.SetAdminCommandTimeout();
        try
        {
          dataTable4 = dataManager.ExecuteDataTable("SELECT DISTINCT F_ID FROM IMS_OBJECTS A WHERE (A.F_BASE_VERSION = 0) AND (A.F_OBJECT_ID > 0) AND (NOT EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS B WHERE B.F_ID = A.F_ID AND B.F_BASE_VERSION > 0 AND B.F_OBJECT_ID > 0))");
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        for (int index = 0; index < dataTable4.Rows.Count; ++index)
        {
          DataTable dataTable6 = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :fID1 ORDER BY F_MODIFICATION_ID ASC, F_OBJECT_ID DESC", dataManager.Parameter("fID1", dataTable4.Rows[index][0]));
          if (dataTable6.Rows.Count > 0 && sessionById.GetObject(Convert.ToInt64(dataTable6.Rows[0][0]), false) is DBObject dbObject)
            dbObject.SetBaseVersion(1L);
        }
        sessionById.EventLog.AddToTrace("IMS_OBJECT_LINKS rebuild...", Consts.traceAlways, "RepairData.log");
        KernelUpdate.RepairObjectLinksTable(dataManager, sessionById.EventLogHelper);
        sessionById.EventLog.AddToTrace("Repair object links...", Consts.traceAlways, "RepairData.log");
        List<string> objectAttrsTables = sessionById.DBCache.GetObjectAttrsTables();
        int num2 = 0;
        dataManager.SetAdminCommandTimeout();
        try
        {
          foreach (string str2 in objectAttrsTables)
          {
            foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT AO.F_OBJECT_ID, AO.F_ATTRIBUTE_ID, AO.F_INLIST_ID, AO.F_STRING_VALUE FROM {str2} AO, IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID = AO.F_ATTRIBUTE_ID AND A.F_ATTRIBUTE_TYPE = {8} AND (AO.F_INTEGER_VALUE IS NOT NULL) AND (AO.F_INTEGER_VALUE <> 0) AND (not exists(select * from IMS_OBJECTS O where O.F_OBJECT_ID = AO.F_INTEGER_VALUE))").Rows)
            {
              IDBObject dbObject = sessionById.GetObject(Convert.ToInt64(row[0]), false);
              if (dbObject != null)
              {
                if (dbObject.GetAttributeByID(Convert.ToInt32(row[1])) is DBAdditionalAttribute attributeById)
                {
                  try
                  {
                    ++num2;
                    attributeById.ValidatingOn = false;
                    attributeById.Index = Convert.ToInt32(row[2]);
                    attributeById.InternalClear();
                  }
                  catch (Exception ex)
                  {
                    sessionById.EventLog.AddToTrace($"Ошибка очистки битой ссылки в атрибуте {attributeById.Name} у объекта {dbObject.NameInMessages}: {ex.Message}", Consts.traceAlways, "RepairData.log");
                  }
                }
              }
            }
          }
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        if (num2 > 0)
          sessionById.EventLog.AddToTrace($"Очищено {num2} ссылок на несуществующие объекты.", Consts.traceAlways, "RepairData.log");
        sessionById.EventLog.AddToTrace("IMS_TIMED_EVENTS rebuild...", Consts.traceAlways, "RepairData.log");
        try
        {
          dataManager.ExecuteScalar("SELECT F_KEY FROM IMS_TIMED_EVENTS WHERE F_KEY = 0");
        }
        catch
        {
          try
          {
            KernelUpdate.CreateTimedEventsTable(dataManager);
          }
          catch (Exception ex)
          {
            sessionById.EventLog.AddToTrace(string.Format("Error creating table IMS_TIMED_EVENTS: ", (object) ex.Message), Consts.traceAlways, "RepairData.log");
          }
        }
        sessionById.EventLog.AddToTrace("IMS_FILENAMES fixing...", Consts.traceAlways, "RepairData.log");
        dataManager.SetAdminCommandTimeout();
        try
        {
          DataTable dataTable7 = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_STRING_VALUE, (SELECT IMS_OBJECTS.F_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_OBJECT_ATTRS.F_OBJECT_ID) F_ID FROM IMS_OBJECT_ATTRS WHERE (F_ATTRIBUTE_ID = {sessionById.IdentHelper.FileAttributeID.ToString()}) AND (F_STRING_VALUE IS NOT NULL) AND (NOT EXISTS(SELECT F_KEY FROM IMS_FILENAMES WHERE F_KEY = F_OBJECT_ID))");
          for (int index = 0; index < dataTable7.Rows.Count; ++index)
          {
            if (dataTable7.Rows[index][2] != null && dataTable7.Rows[index][2] != DBNull.Value)
            {
              long int64 = Convert.ToInt64(dataTable7.Rows[index][2]);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_FILENAMES (F_KEY, F_FILENAME, F_ID) VALUES (:fkey, :ffilename, :fid1)", dataManager.Parameter("fkey", (object) Convert.ToInt64(dataTable7.Rows[index][0])), dataManager.Parameter("ffilename", (object) dataTable7.Rows[index][1].ToString().Trim().ToUpper()), dataManager.Parameter("fid1", (object) int64));
            }
          }
          dataManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE NOT EXISTS(SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_FILENAMES.F_KEY)");
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        sessionById.EventLog.AddToTrace("Trash attributes deleting...", Consts.traceAlways, "RepairData.log");
        this.DeleteWrongAttrs(sessionById, dataManager, "IMS_OBJECT_ATTRS");
        DataTable dataTable8 = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES");
        for (int index = 0; index < dataTable8.Rows.Count; ++index)
        {
          if ((Convert.ToInt32(dataTable8.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/ && sessionById.GetObjectType(Convert.ToInt32(dataTable8.Rows[index]["F_OBJECT_TYPE"])) is DBObjectType objectType)
            this.DeleteWrongAttrs(sessionById, dataManager, objectType.AttributesTableName);
        }
        dataManager.SetAdminCommandTimeout();
        try
        {
          dataTable8 = dataManager.ExecuteDataTable("select A.F_PRJLINK_ID, A.F_ATTRIBUTE_ID from IMS_RELATION_ATTRS A where NOT EXISTS(SELECT * FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PRJLINK_ID = A.F_PRJLINK_ID)");
        }
        finally
        {
          dataManager.SetNormalCommandTimeout();
        }
        if (dataTable8.Rows.Count > 0)
        {
          List<int> intList = new List<int>();
          for (int index = 0; index < dataTable8.Rows.Count; ++index)
          {
            int int32 = Convert.ToInt32(dataTable8.Rows[index][1]);
            if (intList.IndexOf(int32) < 0)
              intList.Add(int32);
            dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = :relID AND F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("relID", (object) Convert.ToInt64(dataTable8.Rows[index][0])), dataManager.Parameter("attrID", (object) int32));
          }
          StringBuilder stringBuilder = new StringBuilder();
          for (int index = 0; index < intList.Count; ++index)
            stringBuilder.AppendFormat("{0},", (object) intList[index]);
          --stringBuilder.Length;
          sessionById.EventLog.AddToTrace($"Из таблицы IMS_RELATION_ATTRS удалено {dataTable8.Rows.Count} записи(ей). Идентификаторы удаленных атрибутов: {stringBuilder.ToString()}", Consts.traceAlways, "RepairData.log");
        }
        if (dataManager.DataProvider.Name == "Sql")
        {
          this.DeleteDESCIndexes(dataManager);
          this.SetDisableLOCK_ESCALATION(dataManager);
        }
        try
        {
          dataManager.ExecuteNonQuery(string.Format("UPDATE IMV_R1 SET F{0} = 0 where F{0} is NULL", (object) sessionById.IdentHelper.GetAttributeID("cad00651-306c-11d8-b4e9-00304f19f545")));
        }
        catch
        {
        }
        IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("DeleteFormN1389313");
        try
        {
          sessionTemporaryClone.GetObject(new Guid("cadd95e5-306c-11d8-b4e9-00304f19f545"), false)?.Delete((long) (Consts.PurgeMode | 16 /*0x10*/));
        }
        finally
        {
          sessionTemporaryClone.Logout("DeleteFormN1389313");
        }
        if (dataManager.DataProvider.Name == "Oracle")
        {
          IDBVersionUpdater service2 = ServerServices.GetService(typeof (IDBVersionUpdater)) as IDBVersionUpdater;
          if (service2.IsNeedUpdateModule(dataManager, sessionById.EventLogHelper, "IMV_A.FIX", "Перестройка первичных ключей IMV_A", 1))
          {
            DataTable dataTable9 = dataManager.ExecuteDataTable("select t.INDEX_NAME, t.TABLE_NAME from sys.user_indexes t where T.TABLE_NAME like 'IMV_A%' and UPPER(T.UNIQUENESS) = 'UNIQUE'");
            for (int index = 0; index < dataTable9.Rows.Count; ++index)
            {
              dataManager.ExecuteNonQuery($"ALTER TABLE {dataTable9.Rows[index][1].ToString()} DROP CONSTRAINT {dataTable9.Rows[index][0].ToString()}");
              dataManager.ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD CONSTRAINT {0}_PK PRIMARY KEY (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID)", (object) dataTable9.Rows[index][1].ToString()));
            }
            service2.UpdateModuleVersion(dataManager, sessionById.EventLogHelper, "IMV_A.FIX", "Перестройка первичных ключей IMV_A", 1);
          }
        }
        DataTable dataTable10 = dataManager.ExecuteDataTable("select distinct P.F_ATTRIBUTE_ID from IMS_POSSIBLE_VALUES P where exists(select * from IMS_ATTRIBUTES A WHERE F_MULTIPLE_VALUED IN (0, 1) and A.F_ATTRIBUTE_ID = P.F_ATTRIBUTE_ID)");
        if (dataTable10.Rows.Count > 0)
        {
          sessionById.EventLog.AddToTrace($"Found {dataTable10.Rows.Count} attributes with wrong possible values...", Consts.traceAlways, "RepairData.log");
          foreach (DataRow row in (InternalDataCollectionBase) dataTable10.Rows)
            dataManager.ExecuteNonQuery("DELETE FROM IMS_POSSIBLE_VALUES WHERE F_ATTRIBUTE_ID = :attrID", dataManager.Parameter("attrID", (object) Convert.ToInt32(row[0])));
          sessionById.DBCache.ReloadTables((IUserSession) sessionById, dataManager, "IMS_POSSIBLE_VALUES");
        }
        List<string> reportList = new List<string>();
        this.DeleteFilenamesDuplicates(dataManager, reportList);
        foreach (string EventStr in reportList)
          sessionById.EventLog.AddToTrace(EventStr, Consts.traceAlways, "RepairData.log");
        sessionById.EventLog.AddToTrace("Attribute values F_INLIST_ID check...", Consts.traceAlways, "RepairData.log");
        foreach (string objectAttrsTable in sessionById.DBCache.GetObjectAttrsTables())
        {
          int num3 = 0;
          string[] strArray = new string[3]
          {
            objectAttrsTable,
            "IMS_OBJECT_LINKS",
            "IMS_GLOBAL_INDEX"
          };
          while (++num3 < 1000)
          {
            DataTable dataTable11 = dataManager.ExecuteDataTable(string.Format("select F_OBJECT_ID, F_ATTRIBUTE_ID from {2} A1 WHERE A1.F_INLIST_ID = {0} AND NOT EXISTS(select * from {2} A2 WHERE A2.F_INLIST_ID = {1} AND A2.F_OBJECT_ID = A1.F_OBJECT_ID AND A2.F_ATTRIBUTE_ID = A1.F_ATTRIBUTE_ID)", (object) num3, (object) (num3 - 1), (object) objectAttrsTable));
            if (dataTable11.Rows.Count != 0)
            {
              sessionById.EventLog.AddToTrace($"Найдено {dataTable11.Rows.Count} пропущенных значений атрибутов в таблице {objectAttrsTable}. Порядковый номер значения {num3 - 1}.", Consts.traceAlways, "RepairData.log");
              foreach (DataRow row in (InternalDataCollectionBase) dataTable11.Rows)
              {
                sessionById.StartTransaction();
                try
                {
                  IDbDataParameter dbDataParameter1 = dataManager.Parameter("objID1", (object) Convert.ToInt64(row[0]));
                  IDbDataParameter dbDataParameter2 = dataManager.Parameter("attrID1", (object) Convert.ToInt32(row[1]));
                  foreach (string str3 in strArray)
                    dataManager.ExecuteNonQuery($"UPDATE {str3} SET F_INLIST_ID = :inlist_id_m1 WHERE F_OBJECT_ID = :objID1 AND F_ATTRIBUTE_ID = :attrID1 AND F_INLIST_ID = :inlist_id", dbDataParameter1, dbDataParameter2, dataManager.Parameter("inlist_id_m1", (object) (num3 - 1)), dataManager.Parameter("inlist_id", (object) num3));
                  if (num3 == 1)
                  {
                    IDBObject dbObject = sessionById.GetObject(Convert.ToInt64(row[0]), false);
                    if (dbObject != null && dbObject.GetAttributeByID(Convert.ToInt32(row[1])) is DBAdditionalAttribute attributeById)
                    {
                      string[] fieldNames = attributeById.AttributeType.FieldNames;
                      if (fieldNames != null)
                      {
                        foreach (string fldName in fieldNames)
                          attributeById.UpdateViewValue(fldName, (object) DBNull.Value, dbObject.ObjectID);
                      }
                      attributeById.InsertIntoView(1, false);
                    }
                  }
                  sessionById.Commit();
                }
                catch (Exception ex)
                {
                  sessionById.Rollback();
                  sessionById.EventLog.AddToTrace($"Ошибка сдвига значения номер {num3} в таблице {objectAttrsTable} для атрибута {row[1]} у объекта номер {row[0]}: " + ex.Message, Consts.traceAlways, "RepairData.log");
                  throw;
                }
              }
            }
            else
              break;
          }
        }
        sessionById.EventLog.AddToTrace("\r\n\r\nПроверка базы данных завершена.\r\n", Consts.traceAlways, "RepairData.log");
      }
      finally
      {
        service1.UnLock((IUserSession) sessionById, nameof (RepairData));
      }
      return (string[]) null;
    }
    return new string[1]
    {
      databaseLockInfo.GetErrorMessage(sc_13686.ssp_appserver_13699())
    };
  }

  internal void DeleteFilenamesDuplicates(IDbManager db, List<string> reportList)
  {
    db.SetAdminCommandTimeout();
    DataTable dataTable;
    try
    {
      dataTable = db.ExecuteDataTable("SELECT F_FILENAME, F_KEY, F_ID FROM IMS_FILENAMES A where (select count(*) from IMS_FILENAMES B WHERE B.F_FILENAME = A.F_FILENAME AND B.F_KEY = A.F_KEY) > 1 ORDER BY F_KEY, F_FILENAME");
    }
    finally
    {
      db.SetNormalCommandTimeout();
    }
    if (dataTable.Rows.Count <= 0)
      return;
    List<Tuple<string, long, long>> tupleList = new List<Tuple<string, long, long>>();
    string str1 = string.Empty;
    long num = 0;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      string str2 = dataTable.Rows[index][0].ToString();
      long int64 = Convert.ToInt64(dataTable.Rows[index][1]);
      if (str1 != str2 || num != int64)
      {
        tupleList.Add(new Tuple<string, long, long>(str2, int64, Convert.ToInt64(dataTable.Rows[index][2])));
        str1 = str2;
        num = int64;
      }
    }
    reportList.Add("В индексе имен файлов IMS_FILENAMES найдены записей с дубликатами, шт: " + tupleList.Count.ToString());
    db.BeginTransaction();
    try
    {
      foreach (Tuple<string, long, long> tuple in tupleList)
      {
        db.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE F_FILENAME = :flName AND F_KEY = :objID", db.Parameter("flName", (object) tuple.Item1), db.Parameter("objID", (object) tuple.Item2));
        db.ExecuteNonQuery("INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:flName, :objID, :id1)", db.Parameter("flName", (object) tuple.Item1), db.Parameter("objID", (object) tuple.Item2), db.Parameter("id1", (object) tuple.Item3));
      }
      db.Commit();
    }
    catch (Exception ex)
    {
      reportList.Add("Ошибка удаления дубликатов в индексе имен файлов: " + ex.Message);
      db.Rollback();
    }
  }

  private void DeleteWrongAttrs(UserSession sys_session, IDbManager db, string tableName)
  {
    db.SetAdminCommandTimeout();
    try
    {
      DataTable dataTable = db.ExecuteDataTable($"select A.F_OBJECT_ID, A.F_ATTRIBUTE_ID from {tableName} A where NOT EXISTS(SELECT * FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = A.F_OBJECT_ID)");
      if (dataTable.Rows.Count <= 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
        db.ExecuteNonQuery($"DELETE FROM {tableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID", db.Parameter("objID", (object) Convert.ToInt64(dataTable.Rows[index][0])), db.Parameter("attrID", (object) Convert.ToInt32(dataTable.Rows[index][1])));
      sys_session.EventLog.AddToTrace($"Из таблицы {tableName} удалено {dataTable.Rows.Count} записи(ей).", Consts.traceAlways, "RepairData.log");
    }
    finally
    {
      db.SetNormalCommandTimeout();
    }
  }

  private string[] GetClearStoppedLog(List<string> loglist, UserSession sys_session)
  {
    loglist.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13700()), (object) this._ClearingUserName, (object) this._ClearingComputerName));
    for (int index = 0; index < loglist.Count; ++index)
      sys_session.EventLog.AddToTrace(loglist[index].ToString(), Consts.traceAlways, "ClearTrash.log");
    return loglist.ToArray();
  }

  public string[] ClearTrash(Guid sessionGUID)
  {
    Monitor.Enter((object) this._ClearingState);
    try
    {
      this._Clearing = !this._Clearing ? true : throw new KernelExceptionID(sc_13686.ssp_appserver_13701(677886973), (object) this._ClearingUserName, (object) this._ClearingComputerName);
    }
    finally
    {
      Monitor.Exit((object) this._ClearingState);
    }
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13702(993347964));
    List<string> stringList = new List<string>();
    UserSession userSession = sessionById.Clone(nameof (ClearTrash)) as UserSession;
    try
    {
      long num1 = 0;
      this._ClearingUserName = userSession.UserName;
      this._ClearingComputerName = userSession.ComputerName;
      ICacheDataset dbCache = userSession.DBCache;
      IDbManager dataManager = userSession.DataManager;
      stringList.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13703()), (object) this._ClearingUserName, (object) this._ClearingComputerName, (object) (DateTime.UtcNow + userSession.TimeZoneOffset)));
      Monitor.Enter((object) this._ClearingState);
      try
      {
        this._ClearingState.CurrentUnit = 0;
        this._ClearingState.MaxUnits = 100;
        this._ClearingState.OperationName = LocalizationHolder.rm.GetString("Kernel_601");
        this._ClearingState.StartTime = DateTime.UtcNow;
        this._ClearingState.State = OperationStates.Processing;
        this._ClearingState.SessionGuid = sessionGUID;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
      DataTable toTable = dataManager.ExecuteDataTable(sc_13686.ssp_appserver_13704(), dataManager.Parameter("mdate", (object) (DateTime.UtcNow - TimeSpan.FromHours(24.0))));
      DataRow[] fromRows = dataManager.ExecuteDataTable($"select DISTINCT S.F_OBJECT_ID from IMS_LCSTART_DATE S where S.F_LC_STEP IN (SELECT O.F_LC_STEP FROM IMS_OBJECTS O WHERE O.F_LEVEL_ID = 1 AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_OBJECT_ID = S.F_OBJECT_ID AND S.F_START_DATE < (SELECT {dataManager.DataProvider.Now}{string.Format(" - {1} FROM IMS_OBJECT_TYPES T WHERE T.F_DEL_TIME <> {0} AND T.F_OBJECT_TYPE = O.F_OBJECT_TYPE))", (object) int.MaxValue, (object) dataManager.DataProvider.GetSQL_TimestampField("T.F_DEL_TIME", TimedEventKinds.Daily))}").Select("");
      SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
      string lower = userSession.Configurations.ReadString("KERNEL", "EVENTS", "AUTO", "0", DBConfigMode.GlobalOnly).ToLower();
      if (lower == "1" || lower == "true")
      {
        try
        {
          Monitor.Enter((object) this._ClearingState);
          try
          {
            if (this._ClearingState.State == OperationStates.Stopped)
              return this.GetClearStoppedLog(stringList, userSession);
            this._ClearingState.CurrentUnit = 0;
            this._ClearingState.MaxUnits = 0;
            this._ClearingState.OperationName = LocalizationHolder.rm.GetString("Kernel_602");
          }
          finally
          {
            Monitor.Exit((object) this._ClearingState);
          }
          long num2 = userSession.Configurations.ReadInteger("KERNEL", "EVENTS", "DAYS", 365L, DBConfigMode.GlobalOnly);
          userSession.EventLog.ClearEvents(DateTime.UtcNow + userSession.TimeZoneOffset - TimeSpan.FromDays((double) num2));
          userSession.EventLogArchive.ClearEvents(DateTime.UtcNow + userSession.TimeZoneOffset - TimeSpan.FromDays((double) num2));
        }
        catch (Exception ex)
        {
          stringList.Add(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13705()) + ex.Message);
        }
      }
      if (userSession.Configurations.ReadBool("KERNEL", "EVENTS", "ARCHIVE", false, DBConfigMode.GlobalOnly))
      {
        try
        {
          Monitor.Enter((object) this._ClearingState);
          try
          {
            if (this._ClearingState.State == OperationStates.Stopped)
              return this.GetClearStoppedLog(stringList, userSession);
            this._ClearingState.CurrentUnit = 0;
            this._ClearingState.MaxUnits = 0;
            this._ClearingState.OperationName = "Архивация журнала событий...";
          }
          finally
          {
            Monitor.Exit((object) this._ClearingState);
          }
          long num3 = userSession.Configurations.ReadInteger("KERNEL", "EVENTS", "ARC_DAYS", 90L, DBConfigMode.GlobalOnly);
          userSession.EventLog.ArchiveEvents(DateTime.UtcNow + userSession.TimeZoneOffset - TimeSpan.FromDays((double) num3));
        }
        catch (Exception ex)
        {
          stringList.Add("Ошибка архивации журнала событий: " + ex.Message);
        }
      }
      Monitor.Enter((object) this._ClearingState);
      try
      {
        if (this._ClearingState.State == OperationStates.Stopped)
          return this.GetClearStoppedLog(stringList, userSession);
        this._ClearingState.CurrentUnit = 0;
        this._ClearingState.MaxUnits = toTable.Rows.Count;
        this._ClearingState.OperationName = LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13706());
        this._ClearingState.StartTime = DateTime.UtcNow;
        this._ClearingState.State = OperationStates.Processing;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
      foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
      {
        IDBObject dbObject;
        try
        {
          dbObject = userSession.GetObject(Convert.ToInt64(row[0]));
        }
        catch (Exception ex)
        {
          stringList.Add(string.Format(LocalizationHolder.rm.GetString("Kernel_605"), row[0], (object) ex.Message));
          continue;
        }
        try
        {
          dbObject.Delete(0L);
          ++num1;
          Interlocked.Increment(ref this._ClearingState.CurrentUnit);
          if (this._ClearingState.State == OperationStates.Stopped)
            return this.GetClearStoppedLog(stringList, userSession);
        }
        catch (Exception ex)
        {
          stringList.Add(string.Format(LocalizationHolder.rm.GetString("Kernel_606"), row[0], (object) ex.Message));
        }
      }
      IBlobStoragesPool service1 = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
      DataTable dataTable1 = userSession.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
      {
        (object) -2,
        (object) -50
      }));
      int num4 = 0;
      for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
      {
        IBlobStorage storage = service1.GetStorage(Convert.ToInt64(dataTable1.Rows[index1][0]), (IUserSession) userSession);
        try
        {
          DataTable dataTable2 = storage.DataManager.ExecuteDataTable($"SELECT F_FILE_ID FROM {storage.StorageName} WHERE F_ATTRIBUTE_ID = {-2000}");
          Monitor.Enter((object) this._ClearingState);
          try
          {
            if (this._ClearingState.State == OperationStates.Stopped)
              return this.GetClearStoppedLog(stringList, userSession);
            this._ClearingState.CurrentUnit = 0;
            this._ClearingState.MaxUnits = dataTable2.Rows.Count;
            this._ClearingState.OperationName = string.Format(LocalizationHolder.rm.GetString("ClearStorageData"), dataTable1.Rows[index1][1]);
            this._ClearingState.StartTime = DateTime.UtcNow;
            this._ClearingState.State = OperationStates.Processing;
          }
          finally
          {
            Monitor.Exit((object) this._ClearingState);
          }
          for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
          {
            userSession.StartTransaction();
            try
            {
              storage.DeleteFile(Convert.ToInt64(dataTable2.Rows[index2][0]));
              ++num4;
              Interlocked.Increment(ref this._ClearingState.CurrentUnit);
              userSession.Commit();
              if (this._ClearingState.State == OperationStates.Stopped)
                return this.GetClearStoppedLog(stringList, userSession);
            }
            catch (Exception ex)
            {
              stringList.Add(string.Format(LocalizationHolder.rm.GetString("DeleteBlobError"), dataTable2.Rows[index2][0], (object) ex.Message));
              userSession.Rollback();
            }
          }
          storage.DeleteTemporaryData();
        }
        finally
        {
          service1.ReleaseStorage(storage);
        }
      }
      Monitor.Enter((object) this._ClearingState);
      try
      {
        if (this._ClearingState.State == OperationStates.Stopped)
          return this.GetClearStoppedLog(stringList, userSession);
        this._ClearingState.CurrentUnit = 1;
        this._ClearingState.MaxUnits = 1;
        this._ClearingState.OperationName = LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13707());
        this._ClearingState.StartTime = DateTime.UtcNow;
        this._ClearingState.State = OperationStates.Processing;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(sc_13686.ssp_appserver_13708()).Rows)
      {
        int int32 = Convert.ToInt32(row[0]);
        userSession.StartTransaction();
        try
        {
          dataManager.ExecuteNonQuery(string.Format("DELETE FROM IMS_LC_LINKS WHERE F_FROM_STEP = {0} OR F_TO_STEP = {0}", (object) int32));
          userSession.DBCache.DeleteRecords("IMS_LC_LINKS", string.Format("F_FROM_STEP = {0} OR F_TO_STEP = {0}", (object) int32), (IUserSession) userSession);
          dataManager.ExecuteNonQuery("DELETE FROM IMS_LC_STEPS WHERE F_LC_STEP = " + int32.ToString());
          userSession.DBCache.DeleteRecords("IMS_LC_STEPS", $"F_LC_STEP = {int32}", (IUserSession) userSession);
          userSession.Commit();
        }
        catch (Exception ex)
        {
          stringList.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13709()), row[1], (object) int32, (object) ex.Message));
          userSession.Rollback();
        }
      }
      dataManager.ExecuteNonQuery(string.Format(sc_13686.ssp_appserver_13710(), (object) dataManager.DataProvider.Now));
      if (dataManager.DataProvider.Name == "Sql")
      {
        if (dataManager.DataProvider.RDBMSVersion >= 12)
        {
          object obj1 = dataManager.ExecuteScalar("SELECT F_VERSION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'MSSQL'");
          if ((obj1 == null ? 1 : (obj1 == DBNull.Value ? 1 : 0)) != 0)
          {
            try
            {
              object obj2 = dataManager.ExecuteScalar("SELECT SERVERPROPERTY ('edition')");
              string str1 = obj2 == null || obj2 == DBNull.Value ? string.Empty : obj2.ToString().ToLower();
              if (str1.IndexOf("enterprise") < 0)
              {
                if (str1.IndexOf("developer") < 0)
                  goto label_94;
              }
              object obj3 = dataManager.ExecuteScalar("SELECT TOP 1 physical_name FROM sys.database_files WHERE type = 0");
              if (obj3 == null || obj3 == DBNull.Value)
                throw new KernelException("SELECT FROM sys.database_files error");
              string str2 = $"{Path.GetDirectoryName(obj3.ToString())}\\ips_{Guid.NewGuid().ToString()}";
              try
              {
                dataManager.ExecuteNonQuery($"ALTER DATABASE {dataManager.DataProvider.DatabaseName} ADD FILEGROUP ips_memory_tables CONTAINS MEMORY_OPTIMIZED_DATA");
              }
              catch (Exception ex)
              {
                stringList.Add(string.Format("In-Memory filegroup creation error: " + ex.Message));
              }
              try
              {
                dataManager.ExecuteNonQuery($"ALTER DATABASE {dataManager.DataProvider.DatabaseName} ADD FILE (name='ips_memory_tables', filename='{str2}') TO FILEGROUP ips_memory_tables");
              }
              catch (Exception ex)
              {
                stringList.Add(string.Format("In-Memory filegroup creation error: " + ex.Message));
              }
              dataManager.ExecuteNonQuery($"ALTER DATABASE {dataManager.DataProvider.DatabaseName} SET MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT=ON");
              this.RebuildTempTables(userSession);
              dataManager.ExecuteNonQuery("INSERT INTO IMS_DBVERSION (F_VERSION_ID, F_MODULE_NAME) VALUES (1, 'MSSQL')");
              stringList.Add("Im-Memory temporary tables created...");
            }
            catch (Exception ex)
            {
              stringList.Add(string.Format("In-Memory tables creation error: " + ex.Message));
            }
          }
        }
        else
          this.TruncateTemporaryTables(dataManager, stringList);
      }
      else if (dataManager.DataProvider.Name == "PostgreSQL")
        this.TruncateTemporaryTables(dataManager, stringList);
label_94:
      Monitor.Enter((object) this._ClearingState);
      try
      {
        if (this._ClearingState.State == OperationStates.Stopped)
          return this.GetClearStoppedLog(stringList, userSession);
        this._ClearingState.CurrentUnit = 1;
        this._ClearingState.MaxUnits = 1;
        this._ClearingState.OperationName = LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13711());
        this._ClearingState.StartTime = DateTime.UtcNow;
        this._ClearingState.State = OperationStates.Processing;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
      (ServerServices.GetService(typeof (IGlobalIndexService)) as IGlobalIndexService).ClearTrash(dataManager);
      Monitor.Enter((object) this._ClearingState);
      try
      {
        if (this._ClearingState.State == OperationStates.Stopped)
          return this.GetClearStoppedLog(stringList, userSession);
        this._ClearingState.CurrentUnit = 1;
        this._ClearingState.MaxUnits = 1;
        this._ClearingState.OperationName = "Чистка итераций удалённых объектов...";
        this._ClearingState.StartTime = DateTime.UtcNow;
        this._ClearingState.State = OperationStates.Processing;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
      int num5 = 0;
      DataTable dataTable3 = dataManager.ExecuteDataTable("SELECT F_SNAPSHOT_ID FROM IMS_SNAPSHOTS S WHERE NOT EXISTS(SELECT * FROM IMS_OBJECTS O WHERE O.F_ID = S.F_ID)");
      for (int index = 0; index < dataTable3.Rows.Count; ++index)
      {
        IDBObjectSnapshot snapshot = userSession.GetSnapshot(Convert.ToInt64(dataTable3.Rows[index][0]), false);
        if (snapshot != null)
        {
          snapshot.Delete((long) Consts.PurgeMode);
          ++num5;
        }
      }
      KernelUpdate kernelUpdate = new KernelUpdate(userSession.EventLogHelper);
      if (kernelUpdate.IsNeedUpdateModule(userSession.DataManager, userSession.EventLogHelper, "KERNEL.CLEAR", "KERNEL.CLEAR", 500))
      {
        kernelUpdate.DeleteAttributeFromType(userSession, userSession.EventLogHelper, new Guid("cad00798-306c-11d8-b4e9-00304f19f545"), new Guid("cad00170-306c-11d8-b4e9-00304f19f545"), true, false);
        kernelUpdate.DeleteAttributeFromType(userSession, userSession.EventLogHelper, new Guid("cad00798-306c-11d8-b4e9-00304f19f545"), new Guid("cad00268-306c-11d8-b4e9-00304f19f545"), true, false);
        kernelUpdate.DeleteAttributeFromType(userSession, userSession.EventLogHelper, new Guid("cad00798-306c-11d8-b4e9-00304f19f545"), new Guid("cad00583-306c-11d8-b4e9-00304f19f545"), true, false);
        kernelUpdate.UpdateModuleVersion(userSession.DataManager, userSession.EventLogHelper, "KERNEL.CLEAR", "KERNEL.CLEAR", 500);
      }
      SnapshotService service2 = ServerServices.GetService(typeof (ISnapshotService)) as SnapshotService;
      int num6 = num5 + service2.DeleteOldSnapshots(userSession, stringList);
      dataManager.ExecuteNonQuery("delete from IMS_CONFIGS where F_USER_ID <> 0 AND not exists(select F_OBJECT_ID from IMS_OBJECTS where F_OBJECT_ID = F_USER_ID)");
      (ServerServices.GetService(typeof (IAppServers)) as IAppServers).DeleteDeadServers(dataManager);
      (userSession.EventLogHelper as EventLogHelper).OnClearTrash((IUserSession) userSession, stringList);
      stringList.Add(string.Format(LocalizationHolder.rm.GetString("Kernel_609"), (object) num1, (object) (DateTime.UtcNow + userSession.TimeZoneOffset), (object) num4, (object) num6));
    }
    finally
    {
      for (int index = 0; index < stringList.Count; ++index)
        userSession.EventLog.AddToTrace(stringList[index].ToString(), Consts.traceAlways, "ClearTrash.log");
      userSession.Logout(nameof (ClearTrash));
      Monitor.Enter((object) this._ClearingState);
      try
      {
        this._Clearing = false;
      }
      finally
      {
        Monitor.Exit((object) this._ClearingState);
      }
    }
    return stringList.ToArray();
  }

  public void SetClearingState(OperationStateInfo operationState)
  {
    Monitor.Enter((object) this._ClearingState);
    try
    {
      this._ClearingState.SetProperties(operationState);
    }
    finally
    {
      Monitor.Exit((object) this._ClearingState);
    }
  }

  private void TruncateTemporaryTables(IDbManager db, List<string> _ClearReport)
  {
    try
    {
      db.SetAdminCommandTimeout();
      db.ExecuteNonQuery("TRUNCATE TABLE IMS_TMP_INTEGER");
      db.ExecuteNonQuery("TRUNCATE TABLE IMS_TMP_DOUBLE");
      db.ExecuteNonQuery("TRUNCATE TABLE IMS_TMP_STRING");
      db.ExecuteNonQuery("TRUNCATE TABLE IMS_TMP_DATE");
    }
    catch (Exception ex)
    {
      _ClearReport.Add(string.Format("Temporary data deleting error: " + ex.Message));
      db.Rollback();
      throw;
    }
    finally
    {
      db.SetNormalCommandTimeout();
    }
  }

  private void RebuildTempTables(UserSession sys_session)
  {
    IDbManager dataManager = sys_session.DataManager;
    try
    {
      dataManager.ExecuteNonQuery("DROP TABLE IMS_TMP_INTEGER");
    }
    catch
    {
    }
    dataManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_INTEGER (F_KEY    INTEGER NOT NULL,F_VALUE  BIGINT NOT NULL,INDEX IMS_TMP_INT_NDX NONCLUSTERED HASH (F_KEY, F_VALUE) WITH (BUCKET_COUNT = 400000)) WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_ONLY)");
    try
    {
      dataManager.ExecuteNonQuery("DROP TABLE IMS_TMP_DOUBLE");
    }
    catch
    {
    }
    dataManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_DOUBLE (F_KEY    INTEGER NOT NULL,F_VALUE  FLOAT NOT NULL,INDEX IMS_TMP_DBL_NDX NONCLUSTERED HASH (F_KEY, F_VALUE) WITH (BUCKET_COUNT = 400000)) WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_ONLY)");
    try
    {
      dataManager.ExecuteNonQuery("DROP TABLE IMS_TMP_STRING");
    }
    catch
    {
    }
    dataManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_STRING (F_KEY    INTEGER NOT NULL,F_VALUE  NVARCHAR(450) COLLATE Cyrillic_General_BIN2 NOT NULL,INDEX IMS_TMP_STR_NDX NONCLUSTERED HASH (F_KEY, F_VALUE) WITH (BUCKET_COUNT = 400000)) WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_ONLY)");
    try
    {
      dataManager.ExecuteNonQuery("DROP TABLE IMS_TMP_DATE");
    }
    catch
    {
    }
    dataManager.ExecuteNonQuery("CREATE TABLE IMS_TMP_DATE (F_KEY    INTEGER NOT NULL,F_VALUE  datetime NOT NULL,INDEX IMS_TMP_DAT_NDX NONCLUSTERED HASH (F_KEY, F_VALUE) WITH (BUCKET_COUNT = 400000)) WITH (MEMORY_OPTIMIZED=ON, DURABILITY=SCHEMA_ONLY)");
  }

  public OperationStateInfo ClearingStateInfo => this._ClearingState;

  public void StopClearTrash(Guid sessionGUID)
  {
    if (this._ClearingState.SessionGuid == Guid.Empty || this._ClearingState.SessionGuid != sessionGUID)
    {
      string message = sc_13686.ssp_appserver_13712();
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"{message} Guid = {sessionGUID.ToString()}", Consts.traceAlways, string.Empty);
      throw new KernelException(message);
    }
    Monitor.Enter((object) this._ClearingState);
    try
    {
      this._ClearingState.State = OperationStates.Stopped;
    }
    finally
    {
      Monitor.Exit((object) this._ClearingState);
    }
  }

  public string[] RebuildIndexes(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13713(671855574));
    List<string> loglist = new List<string>();
    IDatabaseLocker service = ServerServices.GetService(typeof (IDatabaseLocker)) as IDatabaseLocker;
    DatabaseLockInfo databaseLockInfo = service.Lock((IUserSession) sessionById, "RebuildIndex", TimeSpan.FromDays(1.0));
    if (databaseLockInfo.Success)
    {
      try
      {
        Monitor.Enter((object) this._IndexingState);
        try
        {
          this._Indexing = !this._Indexing ? true : throw new KernelExceptionID(sc_13686.ssp_appserver_13714(2076091019), (object) this._IndexingUserName, (object) this._IndexingComputerName, (object) (this._IndexingState.CurrentUnit * 200 / this._IndexingState.MaxUnits));
          this._IndexingState.StartTime = DateTime.UtcNow;
          this._IndexingState.CurrentUnit = 0;
          this._IndexingState.OperationName = LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13715());
          this._IndexingState.State = OperationStates.Processing;
          this._IndexingState.SessionGuid = sessionGUID;
        }
        finally
        {
          Monitor.Exit((object) this._IndexingState);
        }
        try
        {
          IDbManager dataManager = sessionById.DataManager;
          long num1 = 0;
          this._IndexingUserName = sessionById.UserName;
          this._IndexingComputerName = sessionById.ComputerName;
          ICacheDataset dbCache = sessionById.DBCache;
          loglist.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13716()), (object) this._IndexingUserName, (object) this._IndexingComputerName, (object) (DateTime.UtcNow + sessionById.TimeZoneOffset)));
          DataRow[] dataRowArray = sessionById.DBCache.GetTable("IMS_ATTRIBUTES").Copy().Select(sc_13686.ssp_appserver_13717() + Convert.ToInt32((object) ComputeValueModes.IndexValue).ToString());
          Monitor.Enter((object) this._IndexingState);
          try
          {
            this._IndexingState.MaxUnits = 200 * dataRowArray.Length;
          }
          finally
          {
            Monitor.Exit((object) this._IndexingState);
          }
          int num2 = 0;
          foreach (DataRow dataRow in dataRowArray)
          {
            int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
            Monitor.Enter((object) this._IndexingState);
            try
            {
              this._IndexingState.OperationName = string.Format(LocalizationHolder.rm.GetString("Kernel_612"), (object) dataRow["F_NAME"].ToString());
            }
            finally
            {
              Monitor.Exit((object) this._IndexingState);
            }
            DataTable dataTable1 = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECT_ATTRS WHERE F_ATTRIBUTE_ID = :id AND F_INLIST_ID = 0", dataManager.Parameter("id", (object) int32));
            for (int index = 0; index < dataTable1.Rows.Count; ++index)
            {
              IDBObject dbObject;
              try
              {
                dbObject = sessionById.GetObject(Convert.ToInt64(dataTable1.Rows[index][0]));
              }
              catch
              {
                continue;
              }
              try
              {
                if ((dbObject as IDBLifecycleLevel).LevelID != sessionById.IdentHelper.DeletedID)
                {
                  if (dbObject.GetAttributeByID(int32) is DBAttribute attributeById)
                  {
                    attributeById.Compute(true);
                    ++num1;
                  }
                }
              }
              catch (Exception ex)
              {
                loglist.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13718()), dataRow["F_NAME"], dataTable1.Rows[index][0], (object) ex.Message));
              }
              Monitor.Enter((object) this._IndexingState);
              try
              {
                if (this._IndexingState.State == OperationStates.Stopped)
                  return this.GetIndexStoppedLog(loglist, sessionById);
                this._IndexingState.CurrentUnit = num2 + index * 100 / dataTable1.Rows.Count;
              }
              finally
              {
                Monitor.Exit((object) this._IndexingState);
              }
            }
            int num3 = num2 + 100;
            DataTable dataTable2 = dataManager.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :id AND F_INLIST_ID = 0", dataManager.Parameter("id", (object) int32));
            for (int index = 0; index < dataTable2.Rows.Count; ++index)
            {
              IDBRelation relation;
              try
              {
                relation = sessionById.GetRelation(Convert.ToInt64(dataTable2.Rows[index][0]));
              }
              catch
              {
                continue;
              }
              try
              {
                if (relation.GetAttributeByID(int32) is DBAttribute attributeById)
                {
                  attributeById.Compute(true);
                  ++num1;
                }
              }
              catch (Exception ex)
              {
                loglist.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13719()), dataRow["F_NAME"], dataTable2.Rows[index][0], (object) ex.Message));
              }
              try
              {
                if (this._IndexingState.State == OperationStates.Stopped)
                  return this.GetIndexStoppedLog(loglist, sessionById);
                this._IndexingState.CurrentUnit = num3 + index * 100 / dataTable2.Rows.Count;
              }
              finally
              {
                Monitor.Exit((object) this._IndexingState);
              }
            }
            num2 = num3 + 100;
          }
          loglist.Add(string.Format(LocalizationHolder.rm.GetString(sc_13686.ssp_appserver_13720()), (object) num1, (object) (DateTime.UtcNow + sessionById.TimeZoneOffset)));
        }
        finally
        {
          for (int index = 0; index < loglist.Count; ++index)
            sessionById.EventLog.AddToTrace(loglist[index].ToString(), Consts.traceAlways, "RebuildIndex.log");
          Monitor.Enter((object) this._IndexingState);
          try
          {
            this._Indexing = false;
          }
          finally
          {
            Monitor.Exit((object) this._IndexingState);
          }
        }
      }
      finally
      {
        service.UnLock((IUserSession) sessionById, "RebuildIndex");
      }
    }
    else
      loglist.Add(databaseLockInfo.GetErrorMessage(sc_13686.ssp_appserver_13721()));
    return loglist.ToArray();
  }

  public OperationStateInfo IndexingStateInfo => this._IndexingState;

  public void StopRebuildIndexes(Guid sessionGUID)
  {
    if (this._IndexingState.SessionGuid == Guid.Empty || this._IndexingState.SessionGuid != sessionGUID)
    {
      string message = sc_13686.ssp_appserver_13722();
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AddToTrace($"{message} Guid = {sessionGUID.ToString()}", Consts.traceAlways, string.Empty);
      throw new KernelException(message);
    }
    Monitor.Enter((object) this._IndexingState);
    try
    {
      this._IndexingState.State = OperationStates.Stopped;
      this._IndexingState.CurrentUnit = 0;
      this._IndexingState.MaxUnits = 100;
    }
    finally
    {
      Monitor.Exit((object) this._IndexingState);
    }
  }

  private string[] GetIndexStoppedLog(List<string> loglist, UserSession sys_session)
  {
    loglist.Add(string.Format(LocalizationHolder.rm.GetString("Kernel_616"), (object) this._IndexingUserName, (object) this._IndexingComputerName));
    for (int index = 0; index < loglist.Count; ++index)
      sys_session.EventLog.AddToTrace(loglist[index].ToString(), Consts.traceAlways, "RebuildIndex.log");
    return loglist.ToArray();
  }

  public void ReloadIndexSettings()
  {
    (ServerServices.GetService(typeof (IStringNormalizer)) as IStringNormalizer).LoadSettings();
  }

  public void CloseApplicationServer(Guid sessionGUID)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) as UserSession).IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13723(466629968));
    if (AdminUtilsService.ServerRunMode != ServerRunModes.Console)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13724(1929989570));
    Process.GetCurrentProcess().Kill();
  }

  public int FindInvalidObjectAttributes(int objectTypeID, Guid sessionGUID, out DataTable tbl)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13725(190151979));
    IDbDataParameter dbDataParameter = sessionById.DataManager.Parameter("otID", (object) objectTypeID);
    int int32 = Convert.ToInt32(sessionById.DataManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otID", dbDataParameter));
    tbl = sessionById.DataManager.ExecuteDataTable("SELECT F_ATTRIBUTE_ID, COUNT(F_OBJECT_ID) QUANTITY from IMS_OBJECT_ATTRS WHERE (F_INLIST_ID = 0) AND EXISTS(select F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_TYPE = :otID AND IMS_OBJECTS.F_OBJECT_ID = IMS_OBJECT_ATTRS.F_OBJECT_ID) AND (NOT EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES WHERE IMS_ATTR4OBJ_TYPES.F_OBJECT_TYPE = :otID AND IMS_ATTR4OBJ_TYPES.F_ATTRIBUTE_ID = IMS_OBJECT_ATTRS.F_ATTRIBUTE_ID)) GROUP BY F_ATTRIBUTE_ID", dbDataParameter);
    return int32;
  }

  private void WriteLine(string line, UtilsOutputMode outMode, UserSession session)
  {
    if (outMode == UtilsOutputMode.Console || outMode == UtilsOutputMode.Both)
      Console.WriteLine(line);
    if (outMode != UtilsOutputMode.LogFile && outMode != UtilsOutputMode.Both)
      return;
    session.EventLog.AddToTrace(line, Consts.traceAlways, "admin_utils.log");
  }

  public void PrepareSourceDatabase(int mode, UtilsOutputMode outMode)
  {
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (PrepareSourceDatabase)) as UserSession;
    try
    {
      IDbManager dataManager = sessionTemporaryClone.DataManager;
      int num1 = 0;
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_618") + (object) DateTime.Now, outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_619"), outMode, sessionTemporaryClone);
      foreach (string line in this.ClearTrash(sessionTemporaryClone.SessionGUID))
        this.WriteLine(line, outMode, sessionTemporaryClone);
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_623"), outMode, sessionTemporaryClone);
      DataTable dataTable = dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_OWNER_ID, (SELECT IMS_GUID.F_GUID FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS(IMS_OBJECTS.F_OBJECT_ID)) GUID_FIELD FROM IMS_OBJECTS WHERE F_OWNER_ID <> {sessionTemporaryClone.IdentHelper.SysdbaID} AND F_OWNER_ID <> {sessionTemporaryClone.UserID}");
      string empty = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string aGUID = row[2].ToString();
        if (!(aGUID != string.Empty) || !SystemGUIDs.IsSystemGUID(aGUID) && !SystemGUIDs.IsUsersGUID(aGUID))
        {
          try
          {
            if (sessionTemporaryClone.GetObject(Convert.ToInt64(row[0]), false) is DBObject dbObject)
            {
              dbObject.OwnerID = sessionTemporaryClone.IdentHelper.SysdbaID;
              if (dbObject.CreatorID != sessionTemporaryClone.UserID && dbObject.CreatorID != 0L)
                dbObject.SetCreatorID(sessionTemporaryClone.IdentHelper.SysdbaID);
              ++num1;
            }
          }
          catch (Exception ex)
          {
            this.WriteLine(string.Format(LocalizationHolder.rm.GetString("Kernel_624"), (object) Convert.ToInt64(row[0]), (object) ex.Message), outMode, sessionTemporaryClone);
          }
        }
      }
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_625") + num1.ToString(), outMode, sessionTemporaryClone);
      int num2 = 0;
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine("Замена создателей объектов...", outMode, sessionTemporaryClone);
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT F_OBJECT_ID, F_CREATOR_ID, (SELECT IMS_GUID.F_GUID FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS(IMS_OBJECTS.F_OBJECT_ID)) GUID_FIELD FROM IMS_OBJECTS WHERE F_CREATOR_ID NOT IN ({sessionTemporaryClone.IdentHelper.SysdbaID}, {sessionTemporaryClone.UserID}, 0)").Rows)
      {
        string aGUID = row[2].ToString();
        if (!(aGUID != string.Empty) || !SystemGUIDs.IsSystemGUID(aGUID) && !SystemGUIDs.IsUsersGUID(aGUID))
        {
          try
          {
            if (sessionTemporaryClone.GetObject(Convert.ToInt64(row[0]), false) is DBObject dbObject)
            {
              dbObject.SetCreatorID(sessionTemporaryClone.IdentHelper.SysdbaID);
              ++num2;
            }
          }
          catch (Exception ex)
          {
            this.WriteLine($"Ошибка изменения создателя объекта N{Convert.ToInt64(row[0])}: {ex.Message}", outMode, sessionTemporaryClone);
          }
        }
      }
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_625") + num2.ToString(), outMode, sessionTemporaryClone);
      int num3 = 0;
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine("Замена создателей связей...", outMode, sessionTemporaryClone);
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT F_PRJLINK_ID, F_REL_CREATOR, F_PRJ_GUID  FROM IMS_RELATIONS WHERE F_REL_CREATOR NOT IN ({sessionTemporaryClone.IdentHelper.SysdbaID}, {sessionTemporaryClone.UserID}, 0)").Rows)
      {
        string aGUID = row[2].ToString();
        if (!(aGUID != string.Empty) || !SystemGUIDs.IsSystemGUID(aGUID) && !SystemGUIDs.IsUsersGUID(aGUID))
        {
          try
          {
            if (sessionTemporaryClone.GetRelation(Convert.ToInt64(row[0]), false) is DBRelation relation)
            {
              relation.SetCreatorID(sessionTemporaryClone.IdentHelper.SysdbaID);
              ++num3;
            }
          }
          catch (Exception ex)
          {
            this.WriteLine($"Ошибка изменения создателя связи N{Convert.ToInt64(row[0])}: {ex.Message}", outMode, sessionTemporaryClone);
          }
        }
      }
      this.WriteLine("Изменено связей: " + num3.ToString(), outMode, sessionTemporaryClone);
      int num4 = 0;
      dataManager.ExecuteNonQuery(string.Format("UPDATE IMS_CATEGORY_ACCESS SET F_OWNER_ID = {0} WHERE F_OWNER_ID <> {0} AND F_OWNER_ID <> {1}", (object) sessionTemporaryClone.IdentHelper.SysdbaID, (object) sessionTemporaryClone.UserID));
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_620"), outMode, sessionTemporaryClone);
      int[] objectTypeIDs = new int[51]
      {
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00003-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0057f-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00293-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad008ef-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad009ec-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00147-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cadd951d-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00175-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0017e-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad005c0-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00164-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad001da-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00165-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0134b-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00168-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0016c-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00174-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00178-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00179-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad0017d-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00181-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cae06a3d-309e-47e6-a292-15a52fa836a4"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad001e5-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad001fd-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00184-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("2da6e0ee-dca6-4bde-9e8a-04cf743856fd"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00185-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad001ff-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00193-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("25c81c1f-e8be-403c-a3b0-34922e163e46"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00580-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad015b1-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00199-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cadd9364-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00170-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00e90-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00592-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad01489-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cadd92e9-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00880-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00629-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00583-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cadd950b-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00137-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545"),
        sessionTemporaryClone.IdentHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")
      };
      this.PurgeObjectsByType(sessionTemporaryClone.SessionGUID, objectTypeIDs);
      foreach (string line in this.PurgeObjectsByType(sessionTemporaryClone.SessionGUID, objectTypeIDs))
        this.WriteLine(line, outMode, sessionTemporaryClone);
      try
      {
        if (sessionTemporaryClone.GetObject(new Guid("caa01569-306c-11d8-b4e9-00304f19f545"), false) is DBObject dbObject1)
          dbObject1.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
        if (sessionTemporaryClone.GetObject(new Guid("caa01563-306c-11d8-b4e9-00304f19f545"), false) is DBObject dbObject2)
          dbObject2.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
        if (sessionTemporaryClone.GetObject(new Guid("caa01565-306c-11d8-b4e9-00304f19f545"), false) is DBObject dbObject3)
          dbObject3.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
        if (sessionTemporaryClone.GetObject(new Guid("caad92e2-306c-11d8-b4e9-00304f19f545"), false) is DBObject dbObject4)
          dbObject4.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
      }
      catch
      {
      }
      num4 = 0;
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_634"), outMode, sessionTemporaryClone);
      dataManager.ExecuteNonQuery(sc_13686.ssp_appserver_13726());
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_635"), outMode, sessionTemporaryClone);
      dataManager.ExecuteNonQuery("TRUNCATE TABLE IMS_ATTR_HISTORY");
      this.WriteLine("", outMode, sessionTemporaryClone);
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_1135"), outMode, sessionTemporaryClone);
      string[] strArray = this.RepairData(sessionTemporaryClone.SessionGUID);
      if (strArray != null)
      {
        for (int index = 0; index < strArray.Length; ++index)
          this.WriteLine(strArray[index], outMode, sessionTemporaryClone);
      }
      this.WriteLine(LocalizationHolder.rm.GetString("Kernel_636") + (object) DateTime.Now, outMode, sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone?.Logout(nameof (PrepareSourceDatabase));
    }
  }

  public void SetSysdbaOwner()
  {
  }

  public IDBRelationCollection GetRelationCollection(Guid sessionGUID, int relationTypeID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    DBRelationCollection relationCollection = sessionById.IsAdmin ? sessionById.GetRelationCollection(relationTypeID) as DBRelationCollection : throw new KernelExceptionID(sc_13686.ssp_appserver_13727(1216777812));
    relationCollection._CheckCreateRules = false;
    return (IDBRelationCollection) relationCollection;
  }

  public static void RebuildOracleIndexes(IDbManager db, IEventLogHelper events)
  {
    if (!(db.DataProvider.Name == "Oracle") || !(ConfigurationManager.AppSettings.Get(nameof (RebuildOracleIndexes)) == "1"))
      return;
    if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
      Console.WriteLine(LocalizationHolder.rm.GetString(nameof (RebuildOracleIndexes)));
    events.AddToTrace(LocalizationHolder.rm.GetString(nameof (RebuildOracleIndexes)), Consts.traceAlways, string.Empty);
    DataTable dataTable = db.ExecuteDataTable("select INDEX_NAME from user_indexes where INDEX_NAME LIKE 'IM%'");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      try
      {
        db.ExecuteNonQuery($"alter index {dataTable.Rows[index][0].ToString()} rebuild");
      }
      catch (Exception ex)
      {
        events.AddToTrace($"Error rebuild index {dataTable.Rows[index][0]}: {ex.Message}", Consts.traceAlways, "RebuildOracleIndexes.log");
      }
    }
    System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    configuration.AppSettings.Settings.Remove(nameof (RebuildOracleIndexes));
    configuration.AppSettings.Settings.Add(nameof (RebuildOracleIndexes), "0");
    configuration.Save(ConfigurationSaveMode.Full);
    if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
      Console.WriteLine(LocalizationHolder.rm.GetString("RebuildOracleIndexesEnd"));
    events.AddToTrace(LocalizationHolder.rm.GetString("RebuildOracleIndexesEnd"), Consts.traceAlways, string.Empty);
  }

  private void DeleteMemosBlobs(IDbManager db, StringBuilder sb, string tableName)
  {
    --sb.Length;
    db.ExecuteNonQuery($"DELETE FROM {tableName} WHERE F_KEY IN ({sb.ToString()})");
    sb.Clear();
  }

  private void ClearBigData(UserSession session, string findSQL, int objectTypeID)
  {
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    IDbManager dataManager = session.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("otypeID", (object) objectTypeID);
    DataTable dataTable = dataManager.ExecuteDataTable(findSQL, dbDataParameter);
    StringBuilder sb1 = new StringBuilder();
    int num1 = 0;
    StringBuilder sb2 = new StringBuilder();
    int num2 = 0;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      switch (Convert.ToInt32(row[0]))
      {
        case 5:
          if (row[1].ToString().Trim() != string.Empty)
          {
            sb1.Append(row[1].ToString() + ",");
            if (++num1 >= dataManager.DataProvider.MaximumINOperands)
            {
              this.DeleteMemosBlobs(dataManager, sb1, "IMS_BLOBS");
              num1 = 0;
              continue;
            }
            continue;
          }
          continue;
        case 6:
        case 11:
          IBlobStorage storage = service.GetStorage(Convert.ToInt64(row[2]), (IUserSession) session);
          try
          {
            storage.DataManager.ExecuteNonQuery($"UPDATE {storage.StorageName} SET F_ATTRIBUTE_ID = {-2000} WHERE F_FILE_ID = :blobID", storage.DataManager.Parameter("blobID", (object) Convert.ToInt64(row[1])));
            continue;
          }
          finally
          {
            service.ReleaseStorage(storage);
          }
        case 10:
          if (row[1].ToString().Trim() != string.Empty)
          {
            sb2.Append(row[1].ToString() + ",");
            if (++num2 >= dataManager.DataProvider.MaximumINOperands)
            {
              this.DeleteMemosBlobs(dataManager, sb2, "IMS_MEMOS");
              num2 = 0;
              continue;
            }
            continue;
          }
          continue;
        default:
          continue;
      }
    }
    if (num1 > 0)
      this.DeleteMemosBlobs(dataManager, sb1, "IMS_BLOBS");
    if (num2 <= 0)
      return;
    this.DeleteMemosBlobs(dataManager, sb2, "IMS_MEMOS");
  }

  public void PurgeObjectTypes(string settingsFileName)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (PurgeObjectTypes)) as UserSession;
    IDbManager dataManager = sessionTemporaryClone.DataManager;
    try
    {
      dataManager.SetAdminCommandTimeout();
      this.WriteLine($"Запущена процедура чистки объектов, типы которых указаны в файле '{settingsFileName}'.", UtilsOutputMode.Both, sessionTemporaryClone);
      string[] strArray = File.ReadAllLines(settingsFileName, Encoding.Default);
      for (int index1 = 0; index1 < strArray.Length; ++index1)
      {
        IDBObjectType objectType1 = sessionTemporaryClone.GetObjectType(strArray[index1], false);
        if (objectType1 == null)
        {
          this.WriteLine($"Тип объектов '{strArray[index1]}' не найден.", UtilsOutputMode.Both, sessionTemporaryClone);
        }
        else
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType1.ObjectType);
          for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
          {
            IDBObjectType objectType2 = sessionTemporaryClone.GetObjectType(childrenIdRecursive[index2]);
            string[] updateTables = sessionTemporaryClone.DBCache.GetUpdateTables(-2, childrenIdRecursive[index2], -1);
            IDbDataParameter dbDataParameter = dataManager.Parameter("otypeID", (object) childrenIdRecursive[index2]);
            this.WriteLine($"Удаляются объекты типа '{MetaDataHelper.GetObjectTypeFullName(childrenIdRecursive[index2])}'...", UtilsOutputMode.Both, sessionTemporaryClone);
            try
            {
              if (updateTables != null)
              {
                foreach (string str in updateTables)
                {
                  if (str == "IMV_O" + childrenIdRecursive[index2].ToString())
                  {
                    dataManager.ExecuteNonQuery("TRUNCATE TABLE " + str);
                    stringList1.Remove(str);
                    stringList2.Add(str);
                  }
                  else if (stringList1.IndexOf(str) < 0 && stringList2.IndexOf(str) < 0)
                    stringList1.Add(str);
                }
              }
              foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT S.F_SNAPSHOT_ID, S.F_NAME, S.F_OBJECT_ID FROM IMS_SNAPSHOTS S WHERE S.F_OBJECT_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = :otypeID)", dbDataParameter).Rows)
              {
                IDBObjectSnapshot snapshot = sessionTemporaryClone.GetSnapshot(Convert.ToInt64(row[0]), false);
                if (snapshot != null)
                {
                  try
                  {
                    snapshot.Delete((long) (Consts.PurgeMode | 16 /*0x10*/));
                  }
                  catch (Exception ex)
                  {
                    this.WriteLine($"Ошибка удаления итерации '{row[1]}' для объекта номер {row[2]}: {ex.Message}", UtilsOutputMode.Both, sessionTemporaryClone);
                  }
                }
              }
              this.WriteLine("Поиск и удаление двоичных данных...", UtilsOutputMode.Both, sessionTemporaryClone);
              this.ClearBigData(sessionTemporaryClone, $"SELECT AA.F_ATTRIBUTE_TYPE, A.F_INTEGER_VALUE, A.F_DOUBLE_VALUE FROM {sessionTemporaryClone.DBCache.GetAttributesTableName(childrenIdRecursive[index2])} A, IMS_ATTRIBUTES AA WHERE (A.F_OBJECT_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = :otypeID)) AND (AA.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND (AA.F_ATTRIBUTE_TYPE IN (5,6,10,11))", childrenIdRecursive[index2]);
              this.ClearBigData(sessionTemporaryClone, "SELECT AA.F_ATTRIBUTE_TYPE, A.F_INTEGER_VALUE, A.F_DOUBLE_VALUE FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A, IMS_ATTRIBUTES AA WHERE (R.F_PROJ_ID IN (SELECT O.F_OBJECT_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = :otypeID)) AND (A.F_PRJLINK_ID = R.F_PRJLINK_ID) AND (AA.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND (AA.F_ATTRIBUTE_TYPE IN (5,6,10,11))", childrenIdRecursive[index2]);
              this.ClearBigData(sessionTemporaryClone, "SELECT AA.F_ATTRIBUTE_TYPE, A.F_INTEGER_VALUE, A.F_DOUBLE_VALUE FROM IMS_RELATIONS R, IMS_RELATION_ATTRS A, IMS_ATTRIBUTES AA WHERE (R.F_PART_ID IN (SELECT O.F_ID FROM IMS_OBJECTS O WHERE O.F_OBJECT_TYPE = :otypeID)) AND (A.F_PRJLINK_ID = R.F_PRJLINK_ID) AND (AA.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND (AA.F_ATTRIBUTE_TYPE IN (5,6,10,11))", childrenIdRecursive[index2]);
              this.WriteLine("Удаление связей...", UtilsOutputMode.Both, sessionTemporaryClone);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PROJ_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PART_ID IN (SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              this.WriteLine("Чистка системных таблиц...", UtilsOutputMode.Both, sessionTemporaryClone);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_IMBASE_OBJ_LINKS WHERE IMS_IMBASE_OBJ_LINKS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_IMBASE_OBJ_LINKS WHERE IMS_IMBASE_OBJ_LINKS.F_TABLE_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_IMBASE_INDEXES WHERE IMS_IMBASE_INDEXES.F_CATALOG_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_IMBASE_ATTRS WHERE IMS_IMBASE_ATTRS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTRFILTER_VALUE WHERE IMS_ATTRFILTER_VALUE.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE IMS_CATEGORY_ACCESS.F_CATEGORY_TYPE = :catType AND IMS_CATEGORY_ACCESS.F_CATEGORY_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter, dataManager.Parameter("catType", (object) 1));
              dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE IMS_CATEGORY_ACCESS.F_USER_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE IMS_ATTR_HISTORY.F_OBJECT_TYPE = :otypeID", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_LCSTART_DATE WHERE IMS_LCSTART_DATE.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_FILENAMES WHERE IMS_FILENAMES.F_KEY IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE IMS_GLOBAL_INDEX.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_INDEX_RESULT WHERE IMS_INDEX_RESULT.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_PROJECT_TEAM WHERE IMS_PROJECT_TEAM.F_PROJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_PROJECT_TEAM WHERE IMS_PROJECT_TEAM.F_USER_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_VERSIONS_TREE WHERE IMS_VERSIONS_TREE.F_PARENT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_VERSIONS_TREE WHERE IMS_VERSIONS_TREE.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_VERSIONS_CONTEXT WHERE IMS_VERSIONS_CONTEXT.F_CONTEXT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_VERSIONS_CONTEXT WHERE IMS_VERSIONS_CONTEXT.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE IMS_OBJECT_LINKS.F_TOOBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE IMS_OBJECT_LINKS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE IMS_ID_LINKS.F_TO_ID IN (SELECT F_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE IMS_ID_LINKS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_SELECTIONS WHERE IMS_SELECTIONS.F_FOLDER_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_SELECTIONS WHERE IMS_SELECTIONS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_TIMED_EVENTS WHERE IMS_TIMED_EVENTS.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID IN (SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID)", dbDataParameter);
              dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :otypeID", dbDataParameter);
              if (objectType2.IsLocalType)
                dataManager.ExecuteNonQuery("TRUNCATE TABLE " + sessionTemporaryClone.DBCache.GetAttributesTableName(objectType2.ObjectType));
            }
            catch (Exception ex)
            {
              this.WriteLine($"Ошибка удаления объектов типа {MetaDataHelper.GetObjectTypeFullName(childrenIdRecursive[index2])}: {ex.Message}", UtilsOutputMode.Both, sessionTemporaryClone);
              throw;
            }
          }
        }
      }
      this.WriteLine("Удаление общих данных...", UtilsOutputMode.Both, sessionTemporaryClone);
      dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_ATTRS WHERE IMS_OBJECT_ATTRS.F_OBJECT_ID NOT IN (SELECT IMS_OBJECTS.F_OBJECT_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_OBJECT_ID = IMS_OBJECT_ATTRS.F_OBJECT_ID)");
      dataManager.ExecuteNonQuery("DELETE FROM IMS_RELATION_ATTRS WHERE IMS_RELATION_ATTRS.F_PRJLINK_ID NOT IN (SELECT IMS_RELATIONS.F_PRJLINK_ID FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PRJLINK_ID = IMS_RELATION_ATTRS.F_PRJLINK_ID)");
      dataManager.ExecuteNonQuery("DELETE FROM IMS_GUID_RESOLVE WHERE IMS_GUID_RESOLVE.F_CATEGORY_TYPE = 2 AND IMS_GUID_RESOLVE.F_ID NOT IN (SELECT F_ID FROM IMS_OBJECTS WHERE IMS_OBJECTS.F_ID = IMS_GUID_RESOLVE.F_ID)");
      dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_RELATION_TYPE > -1 AND F_ID NOT IN (SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PRJLINK_ID = IMS_ATTR_HISTORY.F_ID)");
      this.WriteLine("Удаление данных из оптимизационных таблиц связей...", UtilsOutputMode.Both, sessionTemporaryClone);
      foreach (DataRow dataRow in sessionTemporaryClone.DBCache.GetTable("IMS_RELATION_TYPES").Select())
      {
        string[] updateTables = sessionTemporaryClone.DBCache.GetUpdateTables(-20, -1, Convert.ToInt32(dataRow["F_RELATION_TYPE"]));
        if (updateTables != null)
        {
          foreach (string str in updateTables)
            dataManager.ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE {0}.F_PRJLINK_ID NOT IN (SELECT IMS_RELATIONS.F_PRJLINK_ID FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_PRJLINK_ID = {0}.F_PRJLINK_ID)", (object) str));
        }
      }
      this.WriteLine("Перегенерация представлений данных для типов объектов...", UtilsOutputMode.Both, sessionTemporaryClone);
      foreach (string str in stringList1)
      {
        if (str == "IMS_OBJECTS_VIEW")
        {
          this.RebuildObjectsView(sessionTemporaryClone.SessionGUID);
        }
        else
        {
          int int32 = Convert.ToInt32(str.Substring(5));
          sessionTemporaryClone.GetObjectType(int32).RebuildView();
        }
      }
      this.WriteLine("Процесс чистки базы данных завершён. Рекомендуется также запустить процедуру удаления устаревших данных для полного удаления двоичных объектов из файловых шкафов.", UtilsOutputMode.Both, sessionTemporaryClone);
    }
    catch (Exception ex)
    {
      this.WriteLine("Процесс чистки базы данных прерван с ошибкой. Устраните ошибку и повторно запустите процедуру удаления объектов.", UtilsOutputMode.Both, sessionTemporaryClone);
      this.WriteLine(ex.Message, UtilsOutputMode.Both, sessionTemporaryClone);
      this.WriteLine(ex.StackTrace, UtilsOutputMode.Both, sessionTemporaryClone);
    }
    finally
    {
      dataManager.SetNormalCommandTimeout();
      sessionTemporaryClone?.Logout(nameof (PurgeObjectTypes));
    }
  }

  public string[] PurgeObjectsByType(Guid sessionGUID, int[] objectTypeIDs)
  {
    int num = 0;
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13728(861312766));
    IDbManager dbManager = sessionById.DeveloperMode ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13729(1164322755));
    List<string> stringList = new List<string>();
    for (int index1 = 0; index1 < objectTypeIDs.Length; ++index1)
    {
      IDBObjectType objectType = sessionById.GetObjectType(objectTypeIDs[index1], false);
      if (objectType != null)
      {
        List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType.ObjectType);
        for (int index2 = 0; index2 < childrenIdRecursive.Count; ++index2)
        {
          DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = " + childrenIdRecursive[index2].ToString());
          for (int index3 = 0; index3 < dataTable.Rows.Count; ++index3)
          {
            try
            {
              if (sessionById.GetObject(Convert.ToInt64(dataTable.Rows[index3][0]), false) is DBObject dbObject)
              {
                if (!SystemGUIDs.IsSystemGUID(dbObject.ObjectGUID))
                {
                  dbObject.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
                  ++num;
                }
              }
            }
            catch (Exception ex)
            {
              stringList.Add(string.Format(LocalizationHolder.rm.GetString("Kernel_621"), (object) Convert.ToInt64(dataTable.Rows[index3][0]), (object) ex.Message));
            }
          }
        }
      }
    }
    stringList.Add($"Процесс чистки объектов указанных типов завершён. Удалено объектов: {num}");
    return stringList.ToArray();
  }

  public string[] PurgeIMBASECatalog(Guid sessionGUID, long catalogID, bool deleteSelf)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13730(139689041));
    IDbManager dbManager = sessionById.DeveloperMode ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13731(322519343));
    List<string> stringList = new List<string>();
    DBObject dbObject1 = sessionById.GetObject(catalogID, true) as DBObject;
    string asString = dbObject1.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545")).AsString;
    IDBObjectCollection objectCollection = sessionById.GetObjectCollection(new Guid("cad00227-306c-11d8-b4e9-00304f19f545"));
    ConditionStructure[] conditions = new ConditionStructure[1]
    {
      new ConditionStructure(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"), RelationalOperators.StartString, (object) asString, LogicalOperators.NONE, 0)
    };
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2),
      new ColumnDescriptor((object) new Guid("cad0020b-306c-11d8-b4e9-00304f19f545"), ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, 0)
    };
    DataTable dataTable1 = objectCollection.Select(new DBRecordSetParams(conditions, columns));
    for (int index = 0; index < dataTable1.Rows.Count; ++index)
    {
      if (sessionById.GetObject(Convert.ToInt64(dataTable1.Rows[index][0]), false) is DBObject dbObject2)
      {
        try
        {
          dbObject2.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
        }
        catch (Exception ex)
        {
          stringList.Add($"Ошибка удаления ярлыка '{dbObject2.NameInMessages}': {ex.Message}");
        }
      }
      object obj = dbManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECT_LINKS WHERE F_TOOBJECT_ID = :objID", dbManager.Parameter("objID", (object) Convert.ToInt64(dataTable1.Rows[index][1])));
      if (obj == null || obj == DBNull.Value)
      {
        if (sessionById.GetObject(Convert.ToInt64(dataTable1.Rows[index][1]), false) is DBObject dbObject3)
        {
          try
          {
            dbObject3.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
          }
          catch (Exception ex)
          {
            stringList.Add($"Ошибка удаления таблицы '{dbObject3.NameInMessages}': {ex.Message}");
          }
        }
      }
    }
    objectCollection.ObjectTypeID = sessionById.IdentHelper.GetAttributeID("cad00222-306c-11d8-b4e9-00304f19f545");
    DataTable dataTable2 = objectCollection.Select(new DBRecordSetParams(conditions, new object[1]
    {
      (object) -2
    }));
    for (int index = 0; index < dataTable2.Rows.Count; ++index)
    {
      if (sessionById.GetObject(Convert.ToInt64(dataTable2.Rows[index][0]), false) is DBObject dbObject4)
      {
        try
        {
          dbObject4.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
        }
        catch (Exception ex)
        {
          stringList.Add($"Ошибка удаления папки '{dbObject4.NameInMessages}': {ex.Message}");
        }
      }
    }
    if (deleteSelf)
      dbObject1.Purge((long) (16 /*0x10*/ | Consts.PurgeMode));
    return stringList.ToArray();
  }

  public DataTable GetAttributeApplicability(Guid sessionGUID, int attributeID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13732(1192452575));
    IDbManager dbManager = sessionById.DeveloperMode ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13733(61310121));
    IDbDataParameter dbDataParameter = dbManager.Parameter("attrID", (object) attributeID);
    DataTable toTable = dbManager.ExecuteDataTable("SELECT O.F_OBJECT_ID, O.F_OBJECT_TYPE, O.F_LEVEL_ID, O.F_OBJECT_VER_TYPE, (SELECT CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS(A.F_OBJECT_ID)) CAPTION FROM IMS_OBJECTS O, IMS_OBJECT_ATTRS A WHERE A.F_ATTRIBUTE_ID = :attrID AND A.F_INLIST_ID = 0 AND O.F_OBJECT_ID = A.F_OBJECT_ID", dbDataParameter);
    DataTable table = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES");
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
      {
        IDBObjectType objectType = sessionById.GetObjectType(Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]));
        if (objectType.HasAttribute(attributeID))
        {
          DataTable dataTable = dbManager.ExecuteDataTable($"SELECT O.F_OBJECT_ID, O.F_OBJECT_TYPE, O.F_LEVEL_ID, O.F_OBJECT_VER_TYPE, (SELECT CAPTION FROM IMS_GUID WHERE IMS_GUID.F_OBJECT_ID = ABS(A.F_OBJECT_ID)) CAPTION FROM IMS_OBJECTS O, {(objectType as DBObjectType).AttributesTableName} A WHERE A.F_ATTRIBUTE_ID = :attrID AND A.F_INLIST_ID = 0 AND O.F_OBJECT_ID = A.F_OBJECT_ID", dbDataParameter);
          if (dataTable.Rows.Count > 0)
            SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable.Select());
        }
      }
    }
    return toTable;
  }

  private void ValidateCombinedObjects(DBObject obj)
  {
    if (obj.CheckoutBy != 0L)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13734(405615015));
    if (MetaDataHelper.IsObjectTypeChildOf(obj.ObjectType, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545")))
      throw new KernelExceptionID(sc_13686.ssp_appserver_13735(479616187), (object) obj.ObjectTypeClass.ObjectTypeName);
  }

  public void CombineObjects(Guid sessionGUID, long[] objectIDs, long toObjectID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13736(1108437012));
    DBObject part_obj = sessionById.GetObject(toObjectID) as DBObject;
    this.ValidateCombinedObjects(part_obj);
    StringBuilder stringBuilder1 = new StringBuilder();
    for (int index = 0; index < objectIDs.Length; ++index)
      stringBuilder1.Append(objectIDs[index].ToString() + ",");
    --stringBuilder1.Length;
    long EventID = part_obj.AddEvent(toObjectID, ActionType.CombineData, EventlogRecordType.AccessGranted, $"{LocalizationHolder.rm.GetString(nameof (CombineObjects))} {stringBuilder1.ToString()}");
    DataTable dataTable1 = dbManager.ExecuteDataTable("SELECT F_PROJ_ID, F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PART_ID = :partID", dbManager.Parameter("partID", (object) part_obj.ID));
    sessionById.StartTransaction();
    try
    {
      for (int index1 = 0; index1 < objectIDs.Length; ++index1)
      {
        if (objectIDs[index1] != toObjectID)
        {
          DBObject dbObject1 = sessionById.GetObject(objectIDs[index1]) as DBObject;
          this.ValidateCombinedObjects(dbObject1);
          if (dbObject1.IsBaseVersion)
          {
            DataTable dataTable2 = dbManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id1 AND F_OBJECT_ID <> :objID", dbManager.Parameter("id1", (object) dbObject1.ID), dbManager.Parameter("objID", (object) dbObject1.ObjectID));
            if (dataTable2.Rows.Count > 0)
            {
              for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
              {
                bool flag = false;
                for (int index3 = 0; index3 < objectIDs.Length; ++index3)
                {
                  if (Convert.ToInt64(dataTable2.Rows[index2][0]) == objectIDs[index3])
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                  throw new KernelExceptionID(sc_13686.ssp_appserver_13737(45219862), (object) dbObject1.NameInMessages, (object) dbObject1.ObjectID);
              }
            }
          }
          DataTable dataTable3 = dbManager.ExecuteDataTable("SELECT F_PRJLINK_ID, F_PROJ_ID, F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PART_ID = :partID", dbManager.Parameter("partID", (object) dbObject1.ID));
          for (int index4 = 0; index4 < dataTable3.Rows.Count; ++index4)
          {
            bool flag = true;
            foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
            {
              if (Convert.ToInt64(row[0]) == Convert.ToInt64(dataTable3.Rows[index4][1]) && Convert.ToInt32(row[1]) == Convert.ToInt32(dataTable3.Rows[index4][2]))
              {
                flag = false;
                break;
              }
            }
            if (flag)
              (sessionById.GetRelation(Convert.ToInt64(dataTable3.Rows[index4][0])) as DBRelation).ReplacePartObjectInternal((IDBObject) part_obj);
          }
          DataTable dataTable4 = dbManager.ExecuteDataTable(sc_13686.ssp_appserver_13738(), dbManager.Parameter("obj_id", (object) objectIDs[index1]));
          for (int index5 = 0; index5 < dataTable4.Rows.Count; ++index5)
          {
            IDBObject dbObject2 = sessionById.GetObject(Convert.ToInt64(dataTable4.Rows[index5][0]), false);
            if (dbObject2 != null && dbObject2.GetAttributeByID(Convert.ToInt32(dataTable4.Rows[index5][1])) is DBAdditionalAttribute attributeById)
            {
              attributeById.Index = Convert.ToInt32(dataTable4.Rows[index5][2]);
              attributeById.DirectSetValues((object) part_obj.Caption, (object) toObjectID, (object) null, (object) null);
            }
          }
          dbManager.ExecuteNonQuery("UPDATE IMS_OBJECT_LINKS SET F_TOOBJECT_ID = :to_id WHERE F_TOOBJECT_ID = :from_id", dbManager.Parameter("from_id", (object) objectIDs[index1]), dbManager.Parameter("to_id", (object) toObjectID));
          DataTable dataTable5 = dbManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID FROM IMS_ID_LINKS WHERE F_TO_ID = :obj_id", dbManager.Parameter("obj_id", (object) dbObject1.ID));
          for (int index6 = 0; index6 < dataTable5.Rows.Count; ++index6)
          {
            IDBObject dbObject3 = sessionById.GetObject(Convert.ToInt64(dataTable5.Rows[index6][0]), false);
            if (dbObject3 != null && dbObject3.GetAttributeByID(Convert.ToInt32(dataTable5.Rows[index6][1])) is DBAdditionalAttribute attributeById)
            {
              attributeById.Index = Convert.ToInt32(dataTable5.Rows[index6][2]);
              attributeById.DirectSetValues((object) part_obj.Caption, (object) part_obj.ID, (object) null, (object) null);
            }
          }
          dbManager.ExecuteNonQuery("UPDATE IMS_ID_LINKS SET F_TO_ID = :to_id WHERE F_TO_ID = :from_id", dbManager.Parameter("from_id", (object) dbObject1.ID), dbManager.Parameter("to_id", (object) part_obj.ID));
          DataRow[] dataRowArray = sessionById.DBCache.GetTable("IMS_ATTRIBUTES").Select(string.Format("F_ATTRIBUTE_TYPE IN ({0}, {1}) AND (F_SIZE_TYPE <= 0 OR F_SIZE_TYPE = {1})", (object) 8, (object) 17, (object) dbObject1.ObjectType));
          StringBuilder stringBuilder2 = new StringBuilder();
          for (int index7 = 0; index7 < dataRowArray.Length; ++index7)
            stringBuilder2.Append(dataRowArray[index7]["F_ATTRIBUTE_ID"].ToString() + ",");
          --stringBuilder2.Length;
          DataTable dataTable6 = dbManager.ExecuteDataTable($"SELECT F_PRJLINK_ID, F_ATTRIBUTE_ID, F_INLIST_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID IN ({stringBuilder2.ToString()}) AND F_INTEGER_VALUE IN ({objectIDs[index1]}, {dbObject1.ID})");
          for (int index8 = 0; index8 < dataTable6.Rows.Count; ++index8)
          {
            IDBRelation relation = sessionById.GetRelation(Convert.ToInt64(dataTable6.Rows[index8][0]), false);
            if (relation != null && relation.GetAttributeByID(Convert.ToInt32(dataTable6.Rows[index8][1])) is DBAdditionalAttribute attributeById)
            {
              attributeById.Index = Convert.ToInt32(dataTable6.Rows[index8][2]);
              long intValue = attributeById.AttributeType.AttributeType != FieldTypes.ftObjectLink ? part_obj.ID : toObjectID;
              attributeById.DirectSetValues((object) part_obj.Caption, (object) intValue, (object) null, (object) null);
            }
          }
          dbObject1.Purge((long) (Consts.PurgeMode | 16 /*0x10*/));
        }
      }
      sessionById.Commit();
    }
    catch (Exception ex)
    {
      sessionById.Rollback();
      string Note = $"{LocalizationHolder.rm.GetString(nameof (CombineObjects))}: {ex.Message}";
      part_obj.CloseEvent(EventID, EventlogRecordType.Error, Note);
      throw;
    }
  }

  public string[] RetrieveDBStatistics(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13739(1426305185));
    List<string> stringList = new List<string>();
    DataTable dataTable1 = dbManager.ExecuteDataTable(sc_13686.ssp_appserver_13740());
    stringList.Add("==================== Количество объектов по типам: =======================");
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      ArrayList objsTreeList = new ArrayList();
      StringBuilder stringBuilder = new StringBuilder();
      sessionById.GetObjectType(Convert.ToInt32(dataTable1.Rows[index1][0])).FillChildrenList(objsTreeList);
      for (int index2 = 0; index2 < objsTreeList.Count; ++index2)
        stringBuilder.Append(objsTreeList[index2].ToString() + ",");
      --stringBuilder.Length;
      object obj1 = dbManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_OBJECT_ID > 0");
      object obj2 = dbManager.ExecuteScalar($"SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE IN ({stringBuilder.ToString()}) AND F_OBJECT_ID > 0 AND F_BASE_VERSION = 1");
      stringList.Add($"{dataTable1.Rows[index1][1].ToString()}: {obj2.ToString()} объектов, {obj1.ToString()} версий");
    }
    DataTable dataTable2 = dbManager.ExecuteDataTable("select F_DESCRIPTION, (SELECT COUNT(*) FROM IMS_RELATIONS WHERE IMS_RELATIONS.F_RELATION_TYPE = IMS_RELATION_TYPES.F_RELATION_TYPE) FROM IMS_RELATION_TYPES ORDER BY F_DESCRIPTION");
    stringList.Add("");
    stringList.Add("======================= Количество связей по типам: =======================");
    for (int index = 0; index < dataTable2.Rows.Count; ++index)
      stringList.Add($"{dataTable2.Rows[index][0].ToString()}: {dataTable2.Rows[index][1].ToString()}");
    string[] strArray = new string[54]
    {
      "IMS_OBJECTS",
      "IMS_OBJECT_ATTRS",
      "IMS_RELATIONS",
      "IMS_RELATION_ATTRS",
      "IMS_ATTR_HISTORY",
      "IMS_BLOBS",
      "IMS_BLOBS_SNAPSHOT",
      "IMS_CATEGORY_ACCESS",
      "IMS_CONFIGS",
      "IMS_EVENTLOG",
      "IMS_FILENAMES",
      "IMS_GLOBAL_INDEX",
      "IMS_GUID",
      "IMS_GUID_RESOLVE",
      "IMS_INDEX_QUEUE",
      "IMS_INDEX_RESULT",
      "IMS_INDEX_WORDS",
      "IMS_MEMOS",
      "IMS_MEMOS_SNAPSHOT",
      "IMS_OBJ_SNAPATTRS",
      "IMS_OBJ_SNAPSHOT",
      "IMS_OBJECT_LINKS",
      "IMS_ID_LINKS",
      "IMS_OBJECTS_VIEW",
      "IMS_OPTIMIZER_STAT",
      "IMS_PROJECT_TEAM",
      "IMS_REL_SNAPATTRS",
      "IMS_REL_SNAPSHOT",
      "IMS_SELECTIONS",
      "IMS_SNAPSHOTS",
      "IMS_TIMED_EVENTS",
      "IMS_TMP_INTEGER",
      "IMS_VERSIONS_CONTEXT",
      "IMS_VERSIONS_TREE",
      "IMS_ATTR_GROUPS",
      "IMS_ATTRIBUTES",
      "IMS_ATTR_IN_GROUPS",
      "IMS_DBVERSION",
      "IMS_LANGUAGES",
      "IMS_LC_STEPS",
      "IMS_LEVELS",
      "IMS_OBJECT_TYPES",
      "IMS_OBJTYPES_TREE",
      "IMS_RELATION_TYPES",
      "IMS_SUBJECT_AREAS",
      "IMS_TYPES_APPLICABILITY",
      "IMS_ATTR4OBJ_TYPES",
      "IMS_ATTR4RELATION_TYPES",
      "IMS_LC_LINKS",
      "IMS_FORMULA_ATTRS",
      "IMS_METADATA",
      "IMS_POSSIBLE_VALUES",
      "IMS_LC_SCHEMAS",
      "IMS_MD_EXTENSIONS"
    };
    stringList.Add("");
    stringList.Add("======================= Количество по таблицам: =======================");
    for (int index = 0; index < strArray.Length; ++index)
    {
      object obj = dbManager.ExecuteScalar("SELECT COUNT(*) FROM " + strArray[index]);
      stringList.Add($"{strArray[index]}: {(object) Convert.ToInt64(obj)}");
    }
    stringList.Add("");
    stringList.Add("Формирование статистики закончено.");
    stringList.Add("");
    stringList.Add("");
    for (int index = 0; index < stringList.Count; ++index)
      sessionById.EventLog.AddToTrace(stringList[index].ToString(), Consts.traceAlways, "DBStat.log");
    return stringList.ToArray();
  }

  public string[] GetAccessReport(Guid sessionGUID, long userID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13741(244112168));
    ArrayList arrayList = new ArrayList();
    DataTable dataTable = dbManager.ExecuteDataTable(sc_13686.ssp_appserver_13742(), dbManager.Parameter("usrID", (object) userID));
    long num1 = 0;
    int num2 = 0;
    int columnIndex1 = dataTable.Columns.IndexOf("F_CATEGORY_ID");
    int columnIndex2 = dataTable.Columns.IndexOf("F_CATEGORY_TYPE");
    int columnIndex3 = dataTable.Columns.IndexOf("F_RIGHT_ID");
    int columnIndex4 = dataTable.Columns.IndexOf("F_RIGHT_TYPE");
    IDBSecurity dbSecurity = (IDBSecurity) null;
    arrayList.Add((object) string.Empty);
    arrayList.Add((object) (sessionById.GetObject(userID, true).NameInMessages + ":"));
    arrayList.Add((object) string.Empty);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][columnIndex1]);
      int int32_1 = Convert.ToInt32(dataTable.Rows[index][columnIndex2]);
      if (int64 != num1 || int32_1 != num2)
      {
        switch (int32_1)
        {
          case 1:
            dbSecurity = sessionById.GetObject(int64, false) as IDBSecurity;
            break;
          case 3:
            dbSecurity = sessionById.GetAttributeType(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 4:
            dbSecurity = sessionById.GetObjectType(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 6:
            dbSecurity = sessionById.GetRelationType(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 7:
            int int32_2 = Convert.ToInt32(int64 >> 32 /*0x20*/);
            dbSecurity = sessionById.GetObjectType(int32_2, false) == null ? (IDBSecurity) null : sessionById.GetLifecycleStep(Convert.ToInt32(int64 & (long) uint.MaxValue), false, int32_2) as IDBSecurity;
            break;
          case 8:
            dbSecurity = sessionById.GetLifecycleLevel(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 9:
            dbSecurity = sessionById.GetLanguageCollection() as IDBSecurity;
            break;
          case 10:
            dbSecurity = sessionById.EventLog as IDBSecurity;
            break;
          case 11:
            dbSecurity = sessionById.GetSubjectAreaCollection() as IDBSecurity;
            break;
          case 12:
            dbSecurity = sessionById.GetAttributesGroup(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 14:
            dbSecurity = (IDBSecurity) sessionById.DBSecurity;
            break;
          case 16 /*0x10*/:
            dbSecurity = sessionById.GetLCSchema(Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 17:
            dbSecurity = sessionById.GetObject((long) Convert.ToInt32(int64), false) as IDBSecurity;
            break;
          case 18:
            dbSecurity = sessionById.GetObject(int64, false) as IDBSecurity;
            break;
          default:
            dbSecurity = (IDBSecurity) null;
            break;
        }
        if (dbSecurity != null)
        {
          string str = string.Empty;
          if (dataTable.Rows[index]["F_END_DATE"] != DBNull.Value && dataTable.Rows[index]["F_BEGIN_DATE"] != DBNull.Value)
          {
            DateTime dateTime1 = Convert.ToDateTime(dataTable.Rows[index]["F_BEGIN_DATE"]);
            DateTime dateTime2 = Convert.ToDateTime(dataTable.Rows[index]["F_END_DATE"]);
            if (dateTime2 >= DateTime.UtcNow)
              str = $"Права действуют с {dateTime1.ToShortDateString()} по {dateTime2.ToShortDateString()}";
            else
              dbSecurity = (IDBSecurity) null;
          }
          if (dbSecurity != null)
          {
            arrayList.Add((object) "");
            arrayList.Add((object) "-----------------------------------");
            arrayList.Add((object) "");
            arrayList.Add((object) $"{dbSecurity.ObjectName} :");
            arrayList.Add((object) str);
          }
        }
        else
          arrayList.Add((object) string.Format(LocalizationHolder.rm.GetString("Kernel_1136"), (object) int32_1, (object) int64));
        arrayList.Add((object) "");
      }
      if (dbSecurity != null)
        arrayList.Add((object) $"{sessionById.EventLogHelper.GetActionName(int32_1, Math.Abs(int64), (ActionType) Convert.ToInt32(dataTable.Rows[index][columnIndex3]))}: {EnumTypeHelper.GetCaption((Enum) (AccessType) Convert.ToInt32(dataTable.Rows[index][columnIndex4]))}");
      num1 = int64;
      num2 = int32_1;
    }
    arrayList.Add((object) "===================================");
    return (string[]) arrayList.ToArray(typeof (string));
  }

  [Obsolete("Следует использовать int SynchronizeDirectoryReadConfig(Guid sessionGUID, out bool multiDomainSyncEnabled...", false)]
  public int SynchronizeDirectoryReadConfig(
    Guid sessionGUID,
    out string catalogName,
    out List<string> exclusionUserSIDs)
  {
    return SynchronizeDirectoryService.ReadSyncSettings(sessionGUID, out catalogName, out exclusionUserSIDs);
  }

  public int SynchronizeDirectoryReadConfig(
    Guid sessionGUID,
    out string defaultCatalog,
    out HybridDictionary catalogsAndExclusionUsers)
  {
    return SynchronizeDirectoryService.ReadSyncSettings(sessionGUID, out defaultCatalog, out catalogsAndExclusionUsers);
  }

  [Obsolete("Следует использовать SynchronizeDirectoryWriteConfig(Guid sessionGUID, bool multiCatalogSyncEnabled...", false)]
  public int SynchronizeDirectoryWriteConfig(
    Guid sessionGUID,
    string catalogName,
    List<string> exclusionUsers,
    bool withSync)
  {
    return SynchronizeDirectoryService.WriteSyncSettings(sessionGUID, catalogName, exclusionUsers, withSync);
  }

  public int SynchronizeDirectoryWriteConfig(
    Guid sessionGUID,
    string defaultCatalog,
    HybridDictionary catalogsAndExclusionUsers,
    bool withSync)
  {
    return SynchronizeDirectoryService.WriteSyncSettings(sessionGUID, defaultCatalog, catalogsAndExclusionUsers, withSync);
  }

  public int SynchronizeDirectoryProcess(Guid sessionGUID)
  {
    return SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID);
  }

  public int SynchronizeDirectoryProcess(Guid sessionGUID, string domainName)
  {
    return SynchronizeDirectoryService.SynchronizeDirectory(sessionGUID, domainName);
  }

  public int ReadDBUsers(Guid sessionGUID, out HybridDictionary users)
  {
    return SynchronizeDirectoryService.ReadDBUsers(sessionGUID, out users);
  }

  public int DeleteDESCIndexes(IDbManager db)
  {
    if (db.DataProvider.Name != "Sql")
      return 0;
    DataTable dataTable = db.ExecuteDataTable("SELECT b.name, a.name FROM sysindexes a, sysobjects b WHERE a.id = b.id AND a.name LIKE '%_DESC' AND  b.name LIKE 'IM%'");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      db.ExecuteNonQuery($"DROP INDEX {dataTable.Rows[index][0]}.{dataTable.Rows[index][1]}");
    return dataTable.Rows.Count;
  }

  public int SetDisableLOCK_ESCALATION(IDbManager db)
  {
    if (db.DataProvider.Name != "Sql")
      return 0;
    DataTable dataTable = db.ExecuteDataTable("SELECT name FROM sysobjects b WHERE xtype = 'U' AND name LIKE 'IM%'");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if (dataTable.Rows[index][0].ToString().IndexOf("IMS_TMP") < 0)
        db.ExecuteNonQuery($"ALTER TABLE {dataTable.Rows[index][0]} SET (LOCK_ESCALATION = DISABLE)");
    }
    return dataTable.Rows.Count;
  }

  internal void DropMVAIndexes(IDbManager db)
  {
    DataTable dataTable = db.ExecuteDataTable("SELECT F_OBJECT_TYPE, F_OPTIONS FROM IMS_OBJECT_TYPES");
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if ((Convert.ToInt32(dataTable.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 16 /*0x10*/)
      {
        string tableName = "IMV_A" + dataTable.Rows[index]["F_OBJECT_TYPE"].ToString();
        try
        {
          db.ExecuteNonQuery(db.DataProvider.GetDropIndexSQL(tableName, "F_INTEGER_VALUE", SortOrders.ASC));
        }
        catch
        {
        }
        try
        {
          db.ExecuteNonQuery(db.DataProvider.GetDropIndexSQL(tableName, "F_STRING_VALUE", SortOrders.ASC));
        }
        catch
        {
        }
        try
        {
          db.ExecuteNonQuery(db.DataProvider.GetDropIndexSQL(tableName, "F_DOUBLE_VALUE", SortOrders.ASC));
        }
        catch
        {
        }
        try
        {
          db.ExecuteNonQuery(db.DataProvider.GetDropIndexSQL(tableName, "F_DATE_VALUE", SortOrders.ASC));
        }
        catch
        {
        }
      }
    }
  }

  public string[] FixLCSteps(Guid sessionGUID, int objectTypeID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13743(1908437433));
    IDBObjectType objectType1 = sessionById.GetObjectType(objectTypeID);
    List<string> stringList = new List<string>();
    stringList.Add(string.Empty);
    stringList.Add($"Процесс исправления шагов ЖЦ для объектов типа '{objectType1.ObjectTypeName}' начат {DateTime.Now}.");
    ArrayList objsTreeList = new ArrayList();
    objectType1.FillChildrenList(objsTreeList);
    int num = 0;
    try
    {
      for (int index1 = 0; index1 < objsTreeList.Count; ++index1)
      {
        IDBObjectType objectType2 = sessionById.GetObjectType(Convert.ToInt32(objsTreeList[index1]));
        IDBLifecycleStepCollection stepsCollection = sessionById.GetLCSchema(objectType2.SchemaID).GetStepsCollection();
        DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_OBJECT_ID, F_LC_STEP FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :objType AND F_LC_STEP NOT IN (SELECT IMS_LC_STEPS.F_LC_STEP FROM IMS_LC_STEPS WHERE IMS_LC_STEPS.F_SCHEMA_ID = :schemaID1)", dbManager.Parameter("objType", (object) objectType2.ObjectType), dbManager.Parameter("schemaID1", (object) objectType2.SchemaID));
        for (int index2 = 0; index2 < dataTable.Rows.Count; ++index2)
        {
          string errorMsg = string.Empty;
          if (sessionById.GetObject(Convert.ToInt64(dataTable.Rows[index2][0]), false) is DBObject dbObject)
          {
            IDBLifecycleStep lifecycleStep = sessionById.GetLifecycleStep(Convert.ToInt32(dataTable.Rows[index2][1]), objectType2.ObjectType);
            IDBLifecycleStep nextstep = !lifecycleStep.IsFirstStep ? stepsCollection.FindSameStep(lifecycleStep, out errorMsg) : sessionById.GetLifecycleStep(stepsCollection.GetFirstStep());
            if (errorMsg != string.Empty)
              stringList.Add($"{dbObject.NameInMessages} на шаге '{lifecycleStep.LCName}': {errorMsg}");
            if (nextstep != null)
            {
              try
              {
                dbObject.DoSetLCStep(nextstep, false);
                ++num;
              }
              catch (Exception ex)
              {
                stringList.Add($"Ошибка перевода объекта '{dbObject.NameInMessages}' на шаг ЖЦ '{nextstep.LCName}': {ex.Message}");
              }
            }
          }
        }
      }
      stringList.Add($"Процесс исправления шагов ЖЦ закончен в {DateTime.Now}. Исправлено объектов: {num}");
      stringList.Add(string.Empty);
    }
    finally
    {
      for (int index = 0; index < stringList.Count; ++index)
        sessionById.EventLog.AddToTrace(stringList[index].ToString(), Consts.traceAlways, "FixLCSteps.log");
    }
    return stringList.ToArray();
  }

  public string[] CombineAttributes(
    Guid sessionGUID,
    int[] attributeIDs,
    int toAttributeID,
    CombineAttributeMode combineMode)
  {
    string TraceFileName = "CombineAttributes.log";
    List<string> log = new List<string>(1);
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager db = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13744(2065822086));
    IDBAttributeType attributeType1 = sessionById.GetAttributeType(toAttributeID);
    IDbDataParameter dbDataParameter1 = db.Parameter("toAttrID", (object) toAttributeID);
    this.ValidateAttributeType(attributeType1);
    try
    {
      for (int index1 = 0; index1 < attributeIDs.Length; ++index1)
      {
        IDBAttributeType attributeType2 = sessionById.GetAttributeType(attributeIDs[index1]);
        string note = $"Объединение атрибута '{attributeType2.Name}' в атрибут '{attributeType1.Name}'.";
        log.Add(note);
        (attributeType1 as DBAttributeType).AddEvent(0L, ActionType.CombineData, EventlogRecordType.Information, note);
        if (attributeType1.AttributeType != attributeType2.AttributeType)
          throw new KernelExceptionID(sc_13686.ssp_appserver_13745(1917426810), (object) attributeType2.Name, (object) attributeType1.Name);
        if (!sessionById.CanChangeObject(3, (object) attributeIDs[index1]))
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_908"), (object) attributeType2.Name));
        if ((attributeType2 as IDBGuid).IsSystemGUID)
          throw new KernelExceptionID(sc_13686.ssp_appserver_13746(702898448), (object) attributeType2.Name, (object) (attributeType2 as IDBGuid).GUID);
        this.ValidateAttributeType(attributeType2);
        if ((attributeType1.MultipleValued == MultiValueModes.SingleValue || attributeType1.MultipleValued == MultiValueModes.SingleValueFromList) && (attributeType2.MultipleValued == MultiValueModes.MultiValues || attributeType2.MultipleValued == MultiValueModes.MultiValuesFromList))
          throw new KernelException(string.Format(sc_13686.ssp_appserver_13747(), (object) attributeType2.Name, (object) attributeType1.Name));
        if ((attributeType1.MultipleValued == MultiValueModes.MultiValuesFromList || attributeType1.MultipleValued == MultiValueModes.SingleValueFromList) && (attributeType2.MultipleValued == MultiValueModes.MultiValues || attributeType2.MultipleValued == MultiValueModes.SingleValue))
          throw new KernelException(string.Format(sc_13686.ssp_appserver_13748(), (object) attributeType2.Name, (object) attributeType1.Name));
        DataRow[] dataRowArray = sessionById.DBCache.GetTable("IMS_OBJECT_TYPES").Select("F_CAPTION_ATTRIBUTE = " + attributeType2.AttributeID.ToString());
        if (dataRowArray.Length != 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          for (int index2 = 0; index2 < dataRowArray.Length; ++index2)
            stringBuilder.Append(dataRowArray[index2]["F_OBJ_TYPE_NAME"].ToString() + ", ");
          stringBuilder.Length -= 2;
          throw new KernelException(string.Format(sc_13686.ssp_appserver_13749(), (object) attributeType2.Name, (object) stringBuilder.ToString()));
        }
        if (combineMode == CombineAttributeMode.CancelOperation)
        {
          DataTable dataTable1 = db.ExecuteDataTable("select A.F_OBJECT_TYPE from IMS_ATTR4OBJ_TYPES A WHERE A.F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES B WHERE B.F_OBJECT_TYPE = A.F_OBJECT_TYPE AND B.F_ATTRIBUTE_ID = :toAttrID)", db.Parameter("attrID", (object) attributeType2.AttributeID), db.Parameter("toAttrID", (object) attributeType1.AttributeID));
          if (dataTable1.Rows.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index3 = 0; index3 < dataTable1.Rows.Count; ++index3)
              stringBuilder.Append(sessionById.GetObjectType(Convert.ToInt32(dataTable1.Rows[index3][0])).ObjectTypeName + ", ");
            stringBuilder.Length -= 2;
            throw new KernelException(string.Format(sc_13686.ssp_appserver_13750(), (object) attributeType2.Name, (object) attributeType1.Name, (object) stringBuilder.ToString()));
          }
          DataTable dataTable2 = db.ExecuteDataTable("select A.F_RELATION_TYPE from IMS_ATTR4RELATION_TYPES A WHERE A.F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT * FROM IMS_ATTR4RELATION_TYPES B WHERE B.F_RELATION_TYPE = A.F_RELATION_TYPE AND B.F_ATTRIBUTE_ID = :toAttrID)", db.Parameter("attrID", (object) attributeType2.AttributeID), db.Parameter("toAttrID", (object) attributeType1.AttributeID));
          if (dataTable2.Rows.Count > 0)
          {
            StringBuilder stringBuilder = new StringBuilder();
            for (int index4 = 0; index4 < dataTable2.Rows.Count; ++index4)
              stringBuilder.Append(sessionById.GetRelationType(Convert.ToInt32(dataTable2.Rows[index4][0])).Description + ", ");
            stringBuilder.Length -= 2;
            throw new KernelException(string.Format(sc_13686.ssp_appserver_13751(), (object) attributeType2.Name, (object) attributeType1.Name, (object) stringBuilder.ToString()));
          }
        }
        if (attributeType2.SizeType != attributeType1.SizeType)
        {
          if ((attributeType1.AttributeType == FieldTypes.ftMemo || attributeType1.AttributeType == FieldTypes.ftShortBlob || attributeType1.AttributeType == FieldTypes.ftString) && attributeType2.SizeType > attributeType1.SizeType)
            throw new KernelException(string.Format(sc_13686.ssp_appserver_13752(), (object) attributeType2.Name, (object) attributeType1.Name, (object) attributeType2.SizeType));
          if (attributeType1.AttributeType == FieldTypes.ftObjectLink && attributeType1.SizeType > 0L)
          {
            if (attributeType2.SizeType < 0L)
              throw new KernelException(string.Format(sc_13686.ssp_appserver_13753(), (object) attributeType2.Name, (object) attributeType1.Name, (object) sessionById.GetObjectType(Convert.ToInt32(attributeType1.SizeType)).ObjectTypeName));
            if (attributeType2.SizeType > 0L && !MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(attributeType2.SizeType), Convert.ToInt32(attributeType1.SizeType)))
              throw new KernelException($"Нельзя объединить ссылочный атрибут '{attributeType2.Name}' с атрибутом '{attributeType1.Name}', т.к. эти атрибуты имеют несовместимые настройки допустимых типов объектов.");
          }
        }
        (sessionById.EventLogHelper as EventLogHelper).OnBeforeCombineAttributes(attributeType2, attributeType1, (IUserSession) sessionById, combineMode, log);
      }
    }
    catch (Exception ex)
    {
      for (int index = 0; index < log.Count; ++index)
        sessionById.EventLogHelper.AddToTrace(log[index], Consts.traceAlways, TraceFileName);
      string str = $"Ошибка на этапе анализа объединения атрибутов: {ex.Message}";
      (attributeType1 as DBAttributeType).AddEvent(0L, ActionType.CombineData, EventlogRecordType.Error, str);
      sessionById.EventLogHelper.AddToTrace(str, Consts.traceAlways, TraceFileName);
      sessionById.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, TraceFileName);
      throw;
    }
    IDBAttributeType dbAttributeType = (IDBAttributeType) null;
    sessionById.StartTransaction();
    try
    {
      for (int index5 = 0; index5 < attributeIDs.Length; ++index5)
      {
        dbAttributeType = sessionById.GetAttributeType(attributeIDs[index5]);
        List<string> objectAttrsTables = sessionById.DBCache.GetObjectAttrsTables();
        objectAttrsTables.Add("IMS_RELATION_ATTRS");
        objectAttrsTables.Add("IMS_OBJ_SNAPATTRS");
        objectAttrsTables.Add("IMS_REL_SNAPATTRS");
        for (int index6 = 0; index6 < objectAttrsTables.Count; ++index6)
          this.ReplaceDataInTable(objectAttrsTables[index6], dbAttributeType, attributeType1, combineMode, sessionById);
        IDbDataParameter dbDataParameter2 = db.Parameter("attrID", (object) attributeIDs[index5]);
        DataTable dataTable3 = db.ExecuteDataTable("select A.F_OBJECT_TYPE from IMS_ATTR4OBJ_TYPES A WHERE A.F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT * FROM IMS_ATTR4OBJ_TYPES B WHERE B.F_OBJECT_TYPE = A.F_OBJECT_TYPE AND B.F_ATTRIBUTE_ID = :toAttrID)", dbDataParameter2, dbDataParameter1);
        for (int index7 = 0; index7 < dataTable3.Rows.Count; ++index7)
          db.ExecuteNonQuery(sc_13686.ssp_appserver_13754(), db.Parameter("objType", dataTable3.Rows[index7][0]), dbDataParameter2);
        db.ExecuteNonQuery("UPDATE IMS_ATTR4OBJ_TYPES SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        DataTable dataTable4 = db.ExecuteDataTable("select A.F_RELATION_TYPE from IMS_ATTR4RELATION_TYPES A WHERE A.F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT * FROM IMS_ATTR4RELATION_TYPES B WHERE B.F_RELATION_TYPE = A.F_RELATION_TYPE AND B.F_ATTRIBUTE_ID = :toAttrID)", dbDataParameter2, dbDataParameter1);
        for (int index8 = 0; index8 < dataTable4.Rows.Count; ++index8)
          db.ExecuteNonQuery("DELETE FROM IMS_ATTR4RELATION_TYPES WHERE F_RELATION_TYPE = :relType AND F_ATTRIBUTE_ID = :attrID", db.Parameter("relType", dataTable4.Rows[index8][0]), dbDataParameter2);
        db.ExecuteNonQuery(sc_13686.ssp_appserver_13755(), dbDataParameter2, dbDataParameter1);
        DataTable dataTable5 = db.ExecuteDataTable("SELECT F_GROUP_ID FROM IMS_ATTR_IN_GROUPS A WHERE A.F_ATTRIBUTE_ID = :attrID AND EXISTS(SELECT * FROM IMS_ATTR_IN_GROUPS B WHERE B.F_ATTRIBUTE_ID = :toAttrID AND B.F_GROUP_ID = A.F_GROUP_ID)", dbDataParameter2, dbDataParameter1);
        for (int index9 = 0; index9 < dataTable5.Rows.Count; ++index9)
          db.ExecuteNonQuery("DELETE FROM IMS_ATTR_IN_GROUPS WHERE F_GROUP_ID = :grpID AND F_ATTRIBUTE_ID = :attrID", dbDataParameter2, db.Parameter("grpID", dataTable5.Rows[index9][0]));
        db.ExecuteNonQuery(sc_13686.ssp_appserver_13756(), dbDataParameter2, dbDataParameter1);
        db.ExecuteNonQuery("UPDATE IMS_FORMULA_ATTRS SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        db.ExecuteNonQuery("UPDATE IMS_FORMULA_ATTRS SET F_FORMULA_ID = :toAttrID WHERE F_FORMULA_ID = :attrID", dbDataParameter2, dbDataParameter1);
        db.ExecuteNonQuery("UPDATE IMS_MD_EXTENSIONS SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        bool flag1 = false;
        DataTable dataTable6 = db.ExecuteDataTable("SELECT * FROM IMS_POSSIBLE_VALUES WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2);
        if (dataTable6.Rows.Count > 0)
        {
          DataTable possibleValues = attributeType1.GetPossibleValues();
          int count = possibleValues.Rows.Count;
          for (int index10 = 0; index10 < dataTable6.Rows.Count; ++index10)
          {
            bool flag2 = false;
            for (int index11 = 0; index11 < possibleValues.Rows.Count; ++index11)
            {
              if (dataTable6.Rows[index10][attributeType1.PossibleValueFieldName].Equals(possibleValues.Rows[index11][attributeType1.PossibleValueFieldName]))
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              db.ExecuteDataTable($"INSERT INTO IMS_POSSIBLE_VALUES (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, F_INLIST_ID, {attributeType1.PossibleValueFieldName}, F_DESCRIPTION) VALUES (:toAttrID, -1, -1, :inlistID, :ps_value, :descr)", dbDataParameter1, db.Parameter("inlistID", (object) count++), db.Parameter("ps_value", dataTable6.Rows[index10][attributeType1.PossibleValueFieldName]), db.Parameter("descr", dataTable6.Rows[index10]["F_DESCRIPTION"]));
              flag1 = true;
            }
          }
        }
        db.ExecuteNonQuery("DELETE FROM IMS_POSSIBLE_VALUES WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2);
        if (flag1)
          sessionById.DBCache.ReloadTables((IUserSession) sessionById, db, "IMS_POSSIBLE_VALUES");
        if (dbAttributeType.AttributeType == FieldTypes.ftFile || dbAttributeType.AttributeType == FieldTypes.ftBlob)
        {
          IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
          DataTable dataTable7 = sessionById.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
          {
            (object) -2,
            (object) -50
          }));
          for (int index12 = 0; index12 < dataTable7.Rows.Count; ++index12)
          {
            IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable7.Rows[index12][0]), (IUserSession) sessionById);
            try
            {
              storage.ChangeAttributeID(dbAttributeType.AttributeID, toAttributeID);
            }
            catch
            {
            }
            finally
            {
              service.ReleaseStorage(storage);
            }
          }
        }
        sessionById.DBCache.ReloadTables((IUserSession) sessionById, db, "IMS_ATTR_IN_GROUPS", "IMS_FORMULA_ATTRS", "IMS_ATTR4OBJ_TYPES", "IMS_ATTR4RELATION_TYPES");
        db.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2);
        db.ExecuteNonQuery(sc_13686.ssp_appserver_13757(), dbDataParameter2);
        if (attributeType1.AttributeType == FieldTypes.ftObjectLink)
          db.ExecuteNonQuery("UPDATE IMS_OBJECT_LINKS SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        else if (attributeType1.AttributeType == FieldTypes.ftObjectLinkByID)
          db.ExecuteNonQuery("UPDATE IMS_ID_LINKS SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        else if (attributeType1.AttributeType == FieldTypes.ftString || attributeType1.AttributeType == FieldTypes.ftMemo)
          db.ExecuteNonQuery("UPDATE IMS_GLOBAL_INDEX SET F_ATTRIBUTE_ID = :toAttrID WHERE F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1);
        (sessionById.EventLogHelper as EventLogHelper).OnAfterCombineAttributes(dbAttributeType, attributeType1, (IUserSession) sessionById, combineMode, log);
        dbAttributeType.Delete(1L);
      }
      sessionById.Commit();
    }
    catch (Exception ex)
    {
      sessionById.Rollback();
      for (int index = 0; index < log.Count; ++index)
        sessionById.EventLogHelper.AddToTrace(log[index], Consts.traceAlways, TraceFileName);
      if (dbAttributeType != null)
      {
        string str = $"Ошибка объединения атрибута '{dbAttributeType.Name}' с атрибутом '{attributeType1.Name}': {ex.Message}";
        (attributeType1 as DBAttributeType).AddEvent(0L, ActionType.CombineData, EventlogRecordType.Error, str);
        sessionById.EventLogHelper.AddToTrace(str, Consts.traceAlways, TraceFileName);
        sessionById.EventLogHelper.AddToTrace(ex.StackTrace, Consts.traceAlways, TraceFileName);
      }
      throw;
    }
    for (int index = 0; index < log.Count; ++index)
      sessionById.EventLogHelper.AddToTrace(log[index], Consts.traceAlways, TraceFileName);
    return log.ToArray();
  }

  private void ValidateAttributeType(IDBAttributeType attr)
  {
    if (attr.AttributeType == FieldTypes.ftSystem)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13758(309224364), (object) attr.Name);
    if (attr.AttributeType == FieldTypes.ftAutoInc)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13759(1236661954), (object) attr.Name);
  }

  private void ReplaceDataInTable(
    string tableName,
    IDBAttributeType fromAttr,
    IDBAttributeType toAttr,
    CombineAttributeMode combineMode,
    UserSession sys_session)
  {
    IDbManager dataManager = sys_session.DataManager;
    string columnName = "F_OBJECT_ID";
    string str1 = string.Empty;
    if (tableName == "IMS_RELATION_ATTRS" || tableName == "IMS_REL_SNAPATTRS")
      columnName = "F_PRJLINK_ID";
    if (tableName == "IMS_OBJ_SNAPATTRS" || tableName == "IMS_REL_SNAPATTRS")
      str1 = " AND B.F_SNAPSHOT_ID = A.F_SNAPSHOT_ID";
    DataTable dataTable = dataManager.ExecuteDataTable(string.Format("SELECT A.* FROM {0} A WHERE (A.F_ATTRIBUTE_ID = :fromAttr) AND EXISTS(SELECT * FROM {0} B WHERE B.F_ATTRIBUTE_ID = :toAttr AND B.{1} = A.{1}{2})", (object) tableName, (object) columnName, (object) str1), dataManager.Parameter(nameof (fromAttr), (object) fromAttr.AttributeID), dataManager.Parameter(nameof (toAttr), (object) toAttr.AttributeID));
    switch (combineMode)
    {
      case CombineAttributeMode.CancelOperation:
        if (dataTable.Rows.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          string str2 = !(str1 != string.Empty) ? (!(columnName == "F_PRJLINK_ID") ? "объекты" : "связи") : "итерации";
          long num = 0;
          for (int index = 0; index < dataTable.Rows.Count; ++index)
          {
            long int64 = Convert.ToInt64(dataTable.Rows[index][columnName]);
            if (int64 != num)
            {
              stringBuilder.Append(int64.ToString() + ", ");
              num = int64;
            }
            if (stringBuilder.Length > 50)
            {
              stringBuilder.Length -= 2;
              stringBuilder.Append("...");
              break;
            }
          }
          throw new KernelException($"Ошибка объединения атрибутов '{fromAttr.Name}' и '{toAttr.Name}'. В базе данных найдены {str2}, в которых присутствуют оба атрибута (Ид. = {stringBuilder.ToString()})");
        }
        break;
      case CombineAttributeMode.LeaveData:
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          string str3 = !(str1 != string.Empty) ? string.Empty : " AND F_SNAPSHOT_ID = " + dataTable.Rows[index]["F_SNAPSHOT_ID"].ToString();
          dataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE F_ATTRIBUTE_ID = :attrID AND {columnName} = :keyID1 AND F_INLIST_ID = :inlistID{str3}", dataManager.Parameter("attrID", (object) fromAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
          if (tableName != "IMS_RELATION_ATTRS" && tableName != "IMS_REL_SNAPATTRS" && tableName != "IMS_OBJ_SNAPATTRS")
          {
            if (toAttr.AttributeType == FieldTypes.ftObjectLink)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) fromAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
            else if (toAttr.AttributeType == FieldTypes.ftObjectLinkByID)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) fromAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
            if (toAttr.AttributeType == FieldTypes.ftString || toAttr.AttributeType == FieldTypes.ftMemo || toAttr.AttributeType == FieldTypes.ftObjectLinkByID || toAttr.AttributeType == FieldTypes.ftObjectLink)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) fromAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
          }
        }
        break;
      default:
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          string str4 = !(str1 != string.Empty) ? string.Empty : " AND F_SNAPSHOT_ID = " + dataTable.Rows[index]["F_SNAPSHOT_ID"].ToString();
          dataManager.ExecuteNonQuery($"DELETE FROM {tableName} WHERE F_ATTRIBUTE_ID = :attrID AND {columnName} = :keyID1 AND F_INLIST_ID = :inlistID{str4}", dataManager.Parameter("attrID", (object) toAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
          if (tableName != "IMS_RELATION_ATTRS" && tableName != "IMS_REL_SNAPATTRS" && tableName != "IMS_OBJ_SNAPATTRS")
          {
            if (toAttr.AttributeType == FieldTypes.ftObjectLink)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) toAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
            else if (toAttr.AttributeType == FieldTypes.ftObjectLinkByID)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) toAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
            if (toAttr.AttributeType == FieldTypes.ftString || toAttr.AttributeType == FieldTypes.ftMemo || toAttr.AttributeType == FieldTypes.ftObjectLink || toAttr.AttributeType == FieldTypes.ftObjectLinkByID)
              dataManager.ExecuteNonQuery("DELETE FROM IMS_GLOBAL_INDEX WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :keyID1 AND F_INLIST_ID = :inlistID", dataManager.Parameter("attrID", (object) toAttr.AttributeID), dataManager.Parameter("keyID1", dataTable.Rows[index][columnName]), dataManager.Parameter("inlistID", dataTable.Rows[index]["F_INLIST_ID"]));
          }
        }
        break;
    }
    dataManager.ExecuteNonQuery($"UPDATE {tableName} SET F_ATTRIBUTE_ID = {toAttr.AttributeID} WHERE F_ATTRIBUTE_ID = {fromAttr.AttributeID}");
  }

  public void RebuidGlobalIndex(IUserSession session)
  {
    Console.WriteLine("Идет формирование очереди индексации атрибутов...");
    UserSession userSession = session as UserSession;
    if (!userSession.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13760(2084127315));
    IDBTimedEvents service1 = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
    IGlobalIndexService service2 = ServerServices.GetService(typeof (IGlobalIndexService)) as IGlobalIndexService;
    IDbManager dataManager = userSession.DataManager;
    long num = 0;
    dataManager.ExecuteNonQuery("TRUNCATE TABLE IMS_INDEX_QUEUE");
    DataTable table = userSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES");
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      int int32_1 = Convert.ToInt32(table.Rows[index]["F_OBJECT_TYPE"]);
      int int32_2 = Convert.ToInt32(table.Rows[index]["F_ATTRIBUTE_ID"]);
      if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 1048576 /*0x100000*/) == 1048576 /*0x100000*/)
        service2.AddToQueue(userSession.GetObjectType(int32_1).GetAttributeType(int32_2));
    }
    userSession.DBCache.GetTable("IMS_ATTRIBUTES");
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(table.Rows[index]["F_ATTRIBUTE_ID"]);
      if ((Convert.ToInt32(table.Rows[index]["F_OPTIONS"]) & 1048576 /*0x100000*/) == 1048576 /*0x100000*/)
        service2.AddToQueue(userSession.GetAttributeType(int32));
    }
    service1.AddEvent(new TimedEventProperties(0, DateTime.UtcNow + TimeSpan.FromMinutes(1.0), DateTime.UtcNow + TimeSpan.FromHours(1.0), new Guid("cadd93c7-306c-11d8-b4e9-00304f19f545"), 0L, 0L, string.Empty, 0, 1), dataManager);
    Console.WriteLine($"Формирование очереди завершено. Значений в очереди: {num}. Через минуту будет запущена фоновая задача индексации атрибутов.");
  }

  public string[] GetSessionsList(Guid sessionGUID)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) as UserSession).IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13761(856969415));
    return (ServerServices.GetService(typeof (IUserSessionCollection)) as IUserSessionCollection).PrintSessions(string.Empty, false);
  }

  public void ClearSiteIDs(Guid sessionGUID, long[] objectIDs)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13762(1098335938));
    sessionById.EventLogHelper.AddToTrace("Начата очистка значений SiteID пользователем " + sessionById.UserName, Consts.traceAlways, "RepairData.log");
    for (int index = 0; index < objectIDs.Length; ++index)
    {
      DBObject dbObject = sessionById.GetObject(objectIDs[index]) as DBObject;
      try
      {
        string siteId = dbObject.SiteID;
        if (siteId != null)
        {
          if (siteId != string.Empty)
          {
            dbObject.SetSiteID(string.Empty);
            sessionById.EventLogHelper.AddToTrace($"Очищено значение SiteID у объекта '{dbObject.NameInMessages}'. Предыдущее значение: {siteId}", Consts.traceAlways, "RepairData.log");
            dbObject.GetAttributeByGuid(PortalConsts.attributePublicationNecessary, false)?.Delete(0L);
          }
        }
      }
      catch (Exception ex)
      {
        sessionById.EventLogHelper.AddToTrace($"Ошибка очистка значения SiteID у объекта '{dbObject.NameInMessages}': {ex.Message}", Consts.traceAlways, "RepairData.log");
        throw;
      }
    }
  }

  public void SetAccessCacheLifetime(Guid sessionGUID, int aclf)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) as UserSession).IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13763(1705738331));
    Consts.CacheClearPeriod = TimeSpan.FromMinutes((double) aclf);
  }

  public void ArtAttrsSync(IUserSession session)
  {
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13764(201383876));
    List<int> intList1 = new List<int>();
    List<int> intList2 = new List<int>();
    string str1 = session.Configurations.ReadString("PDM", "AttrSyncSection", "Attributes", string.Empty, DBConfigMode.GlobalOnly);
    if (str1 != string.Empty)
    {
      string str2 = str1;
      char[] chArray = new char[1]{ ',' };
      foreach (string str3 in str2.Split(chArray))
        intList1.Add(Convert.ToInt32(str3));
    }
    string str4 = session.Configurations.ReadString("PDM", "AttrSyncSection", "MainDocs", string.Empty, DBConfigMode.GlobalOnly);
    if (str4 != string.Empty)
    {
      string str5 = str4;
      char[] chArray = new char[1]{ ',' };
      foreach (string str6 in str5.Split(chArray))
        intList2.Add(Convert.ToInt32(str6));
    }
    string TraceFileName = "ArtAttrsSync.log";
    if (intList2.Count > 0 && intList1.Count > 0)
    {
      Console.Write("Данная команда синхронизирует атрибуты изделий с атрибутами их главных конструкторских документов. Продолжить (да/нет)?");
      string str7 = Console.ReadLine();
      if (str7.ToLower() != "да" && str7.ToLower() != "y")
      {
        Console.Write("Выполнение команды прервано пользователем.");
      }
      else
      {
        UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (ArtAttrsSync)) as UserSession;
        try
        {
          IDBObjectCollection objectCollection = sessionTemporaryClone.GetObjectCollection(new Guid("cad00268-306c-11d8-b4e9-00304f19f545"));
          objectCollection.ShowAllModifications = true;
          IDBRelationCollection relationCollection = sessionTemporaryClone.GetRelationCollection(sessionTemporaryClone.IdentHelper.DocRelationTypeID, "cad005aa-306c-11d8-b4e9-00304f19f545");
          int num = 0;
          for (int index1 = 0; index1 < intList2.Count; ++index1)
          {
            string EventStr1 = "Синхронизация атрибутов изделий с документами типа " + sessionTemporaryClone.GetObjectType(intList2[index1]).ObjectTypeName;
            sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr1, Consts.traceAlways, TraceFileName);
            Console.WriteLine(EventStr1);
            ConditionStructure conditionStructure1 = new ConditionStructure(0, RelationalOperators.ConsistFromType, (object) intList2[index1], LogicalOperators.AND, 0, false);
            ConditionStructure conditionStructure2 = new ConditionStructure(-6, RelationalOperators.Equal, (object) 0, LogicalOperators.NONE, 0, false);
            conditionStructure1.TypeID = (object) sessionTemporaryClone.IdentHelper.DocRelationTypeID;
            DataTable dataTable1 = objectCollection.Select(new DBRecordSetParams(new ConditionStructure[2]
            {
              conditionStructure1,
              conditionStructure2
            }, new object[1]{ (object) -2 }));
            string EventStr2 = $"Найдено {dataTable1.Rows.Count} изделий.";
            sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr2, Consts.traceAlways, TraceFileName);
            Console.WriteLine(EventStr2);
            for (int index2 = 0; index2 < dataTable1.Rows.Count; ++index2)
            {
              IDBObject dbObject1 = sessionTemporaryClone.GetObject(Convert.ToInt64(dataTable1.Rows[index2][0]), false);
              IDBObject dbObject2 = (IDBObject) null;
              if (dbObject1 != null)
              {
                for (int index3 = 0; index3 < intList1.Count; ++index3)
                {
                  IDBAttribute byId = dbObject1.Attributes.FindByID(intList1[index3]);
                  if (dbObject2 == null)
                  {
                    DataTable dataTable2 = relationCollection.ConsistFrom(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
                    {
                      (object) -2,
                      (object) -7
                    }), dbObject1.ObjectID);
                    for (int index4 = 0; index4 < dataTable2.Rows.Count; ++index4)
                    {
                      if (MetaDataHelper.IsObjectTypeChildOf(Convert.ToInt32(dataTable2.Rows[index4][1]), intList2[index1]))
                      {
                        dbObject2 = sessionTemporaryClone.GetObject(Convert.ToInt64(dataTable2.Rows[index4][0]), false);
                        break;
                      }
                    }
                    if (dbObject2 == null)
                      break;
                  }
                  IDBAttribute attributeById = dbObject2.GetAttributeByID(intList1[index3]);
                  if (attributeById != null)
                  {
                    if (!attributeById.IsNull)
                    {
                      try
                      {
                        if (byId == null)
                        {
                          dbObject1.Attributes.AddAttribute(intList1[index3], false, attributeById.Values);
                          ++num;
                        }
                        else if (!byId.ReadOnly)
                        {
                          byId.Values = attributeById.Values;
                          ++num;
                        }
                        else
                          sessionTemporaryClone.EventLogHelper.AddToTrace($"Невозможно записать атрибут '{byId.Name}' объекта '{dbObject1.NameInMessages}' ({dbObject1.ObjectID}), т.к. атрибут не доступен для записи.", Consts.traceAlways, TraceFileName);
                      }
                      catch (Exception ex)
                      {
                        sessionTemporaryClone.EventLogHelper.AddToTrace($"Ошибка записи атрибут '{sessionTemporaryClone.GetAttributeType(intList1[index3]).Name}' объекту '{dbObject1.NameInMessages}' ({dbObject1.ObjectID}): {ex.Message}", Consts.traceAlways, TraceFileName);
                      }
                    }
                  }
                }
              }
            }
          }
          string EventStr = $"Операция завершена. Синхронизировано атрибутов изделий: {num}";
          sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr, Consts.traceAlways, TraceFileName);
          Console.WriteLine(EventStr);
        }
        finally
        {
          sessionTemporaryClone.Logout(nameof (ArtAttrsSync));
        }
      }
    }
    else
      Console.WriteLine("Настройки синхронизации не найдены.");
  }

  public void DeleteInvalidRelations(IUserSession session)
  {
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13765(993967242));
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (DeleteInvalidRelations)) as UserSession;
    try
    {
      string TraceFileName = "DeleteRelations.log";
      Console.WriteLine("Внимание! Эта команда удалит из базы данных недопустимые связи между объектами указанных вами типов, а также их дочерних подтипов.");
      Console.Write("Введите наименование родительского типа объектов:");
      string anObjectTypeName1 = Console.ReadLine();
      IDBObjectType objectType1 = sessionTemporaryClone.GetObjectType(anObjectTypeName1, false);
      if (objectType1 == null)
      {
        Console.WriteLine($"Тип объектов '{anObjectTypeName1}' не найден.");
      }
      else
      {
        int objectType2 = objectType1.ObjectType;
        Console.Write("Введите наименование дочернего типа объектов:");
        string anObjectTypeName2 = Console.ReadLine();
        IDBObjectType objectType3 = sessionTemporaryClone.GetObjectType(anObjectTypeName2, false);
        if (objectType3 == null)
        {
          Console.WriteLine($"Тип объектов '{anObjectTypeName2}' не найден.");
        }
        else
        {
          int objectType4 = objectType3.ObjectType;
          Console.Write("Введите наименование типа связей:");
          string rtypeDescription = Console.ReadLine();
          IDBRelationType relationType1 = sessionTemporaryClone.GetRelationType(rtypeDescription, false);
          if (relationType1 == null)
          {
            Console.WriteLine($"Тип связей '{rtypeDescription}' не найден.");
          }
          else
          {
            int relationType2 = relationType1.RelationType;
            IDbManager dataManager = sessionTemporaryClone.DataManager;
            List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType2);
            List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(objectType4);
            StringBuilder stringBuilder1 = new StringBuilder();
            for (int index1 = 0; index1 < childrenIdRecursive1.Count; ++index1)
            {
              stringBuilder1.Append(childrenIdRecursive1[index1].ToString() + ",");
              for (int index2 = 0; index2 < childrenIdRecursive2.Count; ++index2)
              {
                if (MetaDataHelper.HasApplicability(childrenIdRecursive1[index1], childrenIdRecursive2[index2], relationType2))
                {
                  Console.WriteLine($"Объекты типа '{sessionTemporaryClone.GetObjectType(childrenIdRecursive2[index2]).ObjectTypeName}' могут входить в объекты типа '{sessionTemporaryClone.GetObjectType(childrenIdRecursive1[index1]).ObjectTypeName}' связью '{relationType1.Description}'. Операция удаления прервана.");
                  return;
                }
              }
            }
            --stringBuilder1.Length;
            string EventStr1 = $"Начата процедура удаления недопустимых связей типа '{relationType1.Description}'. Родительский тип: '{objectType1.ObjectTypeName}', дочерний тип: '{objectType3.ObjectTypeName}'";
            sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr1, Consts.traceAlways, TraceFileName);
            Console.WriteLine(EventStr1);
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 0; index < childrenIdRecursive2.Count; ++index)
              stringBuilder2.Append(childrenIdRecursive2[index].ToString() + ",");
            --stringBuilder2.Length;
            int num = 0;
            DataTable dataTable = dataManager.ExecuteDataTable($"SELECT R.F_PRJLINK_ID, R.F_PART_ID FROM IMS_RELATIONS R, IMS_OBJECTS O WHERE R.F_RELATION_TYPE = {relationType2} AND O.F_OBJECT_ID = R.F_PROJ_ID AND O.F_OBJECT_TYPE IN ({stringBuilder1.ToString()})");
            string EventStr2;
            if (dataTable.Rows.Count > 0)
            {
              for (int index = 0; index < dataTable.Rows.Count; ++index)
              {
                long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
                object obj = dataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :partID AND F_OBJECT_TYPE NOT IN ({stringBuilder2.ToString()})", dataManager.Parameter("partID", (object) Convert.ToInt64(dataTable.Rows[index][1])));
                if (obj == null || obj == DBNull.Value)
                {
                  if (sessionTemporaryClone.GetRelation(int64, false) is DBRelation relation)
                  {
                    try
                    {
                      relation.Delete((long) Consts.PurgeMode);
                      ++num;
                    }
                    catch (Exception ex)
                    {
                      sessionTemporaryClone.EventLogHelper.AddToTrace($"Ошибка удаления связи '{relation.ObjectName}': {ex.Message}", Consts.traceAlways, TraceFileName);
                    }
                  }
                }
              }
              EventStr2 = $"Удалено {num} связей.";
            }
            else
              EventStr2 = "Указанных связей в базе данных не найдено.";
            sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr2, Consts.traceAlways, TraceFileName);
            Console.WriteLine(EventStr2);
          }
        }
      }
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (DeleteInvalidRelations));
    }
  }

  public void DeleteEmptyGraphSigns(IUserSession session)
  {
    if (!session.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13766(1564188989));
    UserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (DeleteEmptyGraphSigns)) as UserSession;
    try
    {
      string TraceFileName = "DeleteSigns.log";
      IDBAttributeType attributeType = sessionTemporaryClone.GetAttributeType(new Guid("cad00141-306c-11d8-b4e9-00304f19f545"));
      IDBObjectType objectType = sessionTemporaryClone.GetObjectType(new Guid("cad00137-306c-11d8-b4e9-00304f19f545"));
      DataTable dataTable = sessionTemporaryClone.DataManager.ExecuteDataTable($"SELECT F_OBJECT_ID FROM IMV_A{objectType.ObjectType} WHERE F_ATTRIBUTE_ID = {attributeType.AttributeID} AND ((F_STRING_VALUE IS NULL) OR (F_STRING_VALUE = ''))");
      string EventStr1 = $"Начата процедура удаления {dataTable.Rows.Count} подписей с пустой графой...";
      sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr1, Consts.traceAlways, TraceFileName);
      Console.WriteLine(EventStr1);
      int num = 0;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (sessionTemporaryClone.GetObject(Convert.ToInt64(dataTable.Rows[index][0]), false) is DBObject dbObject)
        {
          try
          {
            dbObject.Purge((long) Consts.PurgeMode);
            ++num;
          }
          catch (Exception ex)
          {
            sessionTemporaryClone.EventLogHelper.AddToTrace($"Ошибка удаления подписи '{dbObject.Caption}': {ex.Message}", Consts.traceAlways, TraceFileName);
          }
        }
      }
      string EventStr2 = "Удаление подписей завершено. Удалено подписей: " + num.ToString();
      sessionTemporaryClone.EventLogHelper.AddToTrace(EventStr2, Consts.traceAlways, TraceFileName);
      Console.WriteLine(EventStr2);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (DeleteEmptyGraphSigns));
    }
  }

  public void DeleteDublicateFiles(IUserSession session, bool deleteMode)
  {
    UserSession UsrSession = session as UserSession;
    IDbManager dbManager = UsrSession.IsAdmin ? UsrSession.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13768(1757440064));
    IBlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as IBlobStoragesPool;
    string TraceFileName = "StorageErrors.log";
    string EventStr1;
    if (deleteMode)
    {
      Console.Write("Начать процедуру удаления ошибочных файлов в файловых шкафах (да/нет)?");
      if (Console.ReadLine().ToLower() != "да")
        return;
      EventStr1 = "Начата процедура удаления ошибочных записей в файловых шкафах...";
    }
    else
      EventStr1 = "Начата процедура поиска ошибочных записей в файловых шкафах...";
    UsrSession.EventLogHelper.AddToTrace(EventStr1, Consts.traceAlways, TraceFileName);
    Console.WriteLine(EventStr1);
    DataTable dataTable1 = UsrSession.GetObjectCollection(new Guid("cad00014-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[2]
    {
      (object) -2,
      (object) -50
    }));
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable1.Rows[index1][0]), (IUserSession) UsrSession);
      try
      {
        DataTable dataTable2 = storage.DataManager.ExecuteDataTable($"SELECT F_FILE_ID, F_OBJECTLINK_ID, F_FILENAME  FROM {storage.StorageName} WHERE F_ATTRIBUTE_ID = {UsrSession.IdentHelper.FileAttributeID}");
        string EventStr2 = $"В файловом шкафу {storage.StorageCaption} найдено {dataTable2.Rows.Count} файлов...";
        UsrSession.EventLogHelper.AddToTrace(EventStr2, Consts.traceAlways, TraceFileName);
        Console.WriteLine(EventStr2);
        for (int index2 = 0; index2 < dataTable2.Rows.Count; ++index2)
        {
          long int64_1 = Convert.ToInt64(dataTable2.Rows[index2][1]);
          long int64_2 = Convert.ToInt64(dataTable2.Rows[index2][0]);
          object obj1 = dbManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS where F_OBJECT_ID = :objID", dbManager.Parameter("objID", (object) int64_1));
          if (obj1 != null && obj1 != DBNull.Value)
          {
            string attributesTableName = UsrSession.DBCache.GetAttributesTableName(Convert.ToInt32(obj1));
            object obj2 = dbManager.ExecuteScalar($"SELECT F_INTEGER_VALUE FROM {attributesTableName} WHERE F_ATTRIBUTE_ID = {UsrSession.IdentHelper.FileAttributeID} AND F_OBJECT_ID = :objID AND F_INTEGER_VALUE = :fileID", dbManager.Parameter("objID", (object) int64_1), dbManager.Parameter("fileID", (object) int64_2));
            if (obj2 == null || obj2 == DBNull.Value)
            {
              string EventStr3 = $"Не найден файл N{int64_2} с именем {dataTable2.Rows[index2][2]} у объекта N{int64_1}.";
              UsrSession.EventLogHelper.AddToTrace(EventStr3, Consts.traceAlways, TraceFileName);
              Console.WriteLine(EventStr3);
              if (deleteMode)
              {
                storage.DeleteFile(int64_2);
                string EventStr4 = $"Файл N{int64_2} удален.";
                UsrSession.EventLogHelper.AddToTrace(EventStr4, Consts.traceAlways, TraceFileName);
                Console.WriteLine(EventStr4);
              }
            }
          }
        }
      }
      finally
      {
        service.ReleaseStorage(storage);
      }
    }
  }

  public DataTable GetIdleAttributes(Guid sessionGUID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13769(208753627));
    int groupId = sessionById.GetAttributesGroup(new Guid("cad0034e-306c-11d8-b4e9-00304f19f545")).GroupID;
    DataTable idleAttributes = dbManager.ExecuteDataTable($"select * from IMS_ATTRIBUTES A WHERE A.F_ATTRIBUTE_ID > 0 AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_ATTR4OBJ_TYPES AOT WHERE AOT.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_ATTR4RELATION_TYPES ART WHERE ART.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_OBJECT_ATTRS AO WHERE AO.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_RELATION_ATTRS AR WHERE AR.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_ATTR_HISTORY AHIST WHERE AHIST.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_FORMULA_ATTRS AFRM WHERE AFRM.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_MD_EXTENSIONS AMDEXT WHERE AMDEXT.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_IMH_INDEX AIMH WHERE AIMH.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_OBJ_SNAPATTRS ASNAPO WHERE ASNAPO.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_OBJECT_LINKS AOLNK WHERE AOLNK.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_ID_LINKS AOLNK WHERE AOLNK.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_REL_SNAPATTRS ASNAPR WHERE ASNAPR.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_IMBASE_ATTRS AIMB WHERE AIMB.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID) AND NOT EXISTS(SELECT F_ATTRIBUTE_ID FROM IMS_ATTR_IN_GROUPS AIGRP WHERE AIGRP.F_GROUP_ID = {groupId} AND AIGRP.F_ATTRIBUTE_ID = A.F_ATTRIBUTE_ID)");
    List<string> objectAttrsTables = sessionById.DBCache.GetObjectAttrsTables();
    for (int index = 0; index < objectAttrsTables.Count; ++index)
    {
      if (objectAttrsTables[index] == "IMS_OBJECT_ATTRS")
      {
        objectAttrsTables.RemoveAt(index);
        break;
      }
    }
    DataTable dataTable = sessionById.GetObjectTypeCollection(-2).Select(string.Empty);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      if ((Convert.ToInt32(dataTable.Rows[index]["F_OPTIONS"]) & 16 /*0x10*/) == 0)
      {
        bool flag = false;
        string tableName = "IMV_A" + dataTable.Rows[index]["F_OBJECT_TYPE"].ToString();
        try
        {
          dbManager.DataProvider.CheckTableExists(tableName, "F_OBJECT_ID", dbManager);
          flag = true;
        }
        catch
        {
        }
        if (flag)
          objectAttrsTables.Add(tableName);
      }
    }
    UsedAttributesEventArgs args = new UsedAttributesEventArgs();
    (sessionById.EventLogHelper as EventLogHelper).OnGetUsedAttributes((IUserSession) sessionById, args);
    for (int index1 = idleAttributes.Rows.Count - 1; index1 >= 0; --index1)
    {
      if (SystemGUIDs.IsSystemGUID(idleAttributes.Rows[index1]["F_GUID"].ToString()))
      {
        idleAttributes.Rows.RemoveAt(index1);
      }
      else
      {
        int int32 = Convert.ToInt32(idleAttributes.Rows[index1]["F_ATTRIBUTE_ID"]);
        if (args.UsedAttributes.Contains(int32))
        {
          idleAttributes.Rows.RemoveAt(index1);
        }
        else
        {
          IDbDataParameter[] parameters = new IDbDataParameter[1]
          {
            dbManager.Parameter("attrID", (object) int32)
          };
          for (int index2 = 0; index2 < objectAttrsTables.Count; ++index2)
          {
            if (dbManager.DataProvider.IsRecordsExists(dbManager, objectAttrsTables[index2], "F_OBJECT_ID", "F_ATTRIBUTE_ID = :attrID", parameters))
            {
              idleAttributes.Rows.RemoveAt(index1);
              break;
            }
          }
        }
      }
    }
    idleAttributes.AcceptChanges();
    return idleAttributes;
  }

  public string[] GetNULLAttributes(Guid sessionGUID, int minCount, bool toScreen)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager dbManager = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13770(1500700311));
    List<string> stringList = new List<string>();
    foreach (DataRow row1 in (InternalDataCollectionBase) dbManager.ExecuteDataTable($"SELECT F_OBJECT_TYPE FROM IMS_OBJECT_TYPES WHERE F_VERSIONABLE <> {0}").Rows)
    {
      IDBObjectType objectType = sessionById.GetObjectType(Convert.ToInt32(row1[0]));
      object obj = dbManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_OBJECTS WHERE F_OBJECT_TYPE = :oType", dbManager.Parameter("oType", (object) objectType.ObjectType));
      if (Convert.ToInt32(obj) >= minCount)
      {
        if (toScreen)
          Console.WriteLine("Анализируются атрибуты для объектов типа '{0}'...", (object) objectType.ObjectTypeName);
        stringList.Add($"Количество объектов типа '{objectType.ObjectTypeName}': {obj}");
        string attributesTableName = sessionById.DBCache.GetAttributesTableName(objectType.ObjectType);
        foreach (DataRow row2 in (InternalDataCollectionBase) objectType.Attributes.Select(string.Empty).Rows)
        {
          int int32 = Convert.ToInt32(row2["F_ATTRIBUTE_ID"]);
          if (Convert.ToInt32(dbManager.ExecuteScalar($"SELECT COUNT(*) FROM {attributesTableName} WHERE F_ATTRIBUTE_ID = :attrID", dbManager.Parameter("attrID", (object) int32))) == 0)
            stringList.Add($"Отсутствуют значения атрибута '{MetaDataHelper.GetAttributeTypeName(int32)}'");
        }
        stringList.Add(string.Empty);
      }
    }
    foreach (DataRow row3 in (InternalDataCollectionBase) dbManager.ExecuteDataTable(string.Format("SELECT F_RELATION_TYPE FROM IMS_RELATION_TYPES", (object) 0)).Rows)
    {
      IDBRelationType relationType = sessionById.GetRelationType(Convert.ToInt32(row3[0]));
      object obj = dbManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_RELATIONS WHERE F_RELATION_TYPE = :rType", dbManager.Parameter("rType", (object) relationType.RelationType));
      if (Convert.ToInt32(obj) >= minCount)
      {
        if (toScreen)
          Console.WriteLine("Анализируются атрибуты для связей типа '{0}'...", (object) relationType.Description);
        stringList.Add($"Количество связей типа '{relationType.Description}': {obj}");
        foreach (DataRow row4 in (InternalDataCollectionBase) relationType.Attributes.Select(string.Empty).Rows)
        {
          int int32 = Convert.ToInt32(row4["F_ATTRIBUTE_ID"]);
          if (Convert.ToInt32(dbManager.ExecuteScalar("SELECT COUNT(*) FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :attrID", dbManager.Parameter("attrID", (object) int32))) == 0)
            stringList.Add($"Отсутствуют значения атрибута '{MetaDataHelper.GetAttributeTypeName(int32)}'");
        }
        stringList.Add(string.Empty);
      }
    }
    return stringList.ToArray();
  }

  public void RepairViews4Objects(Guid sessionGUID, long[] objectIDs)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13771(2050744000));
    foreach (long objectId in objectIDs)
    {
      if (sessionById.GetObject(objectId, false) is DBObject dbObject1)
      {
        dbObject1.RepairViews();
        if (dbObject1.CheckoutBy != 0L && sessionById.GetObject(-objectId, false) is DBObject dbObject)
          dbObject.RepairViews();
      }
    }
  }

  public void RepairViews4Relations(Guid sessionGUID, long[] relationIDs)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGUID);
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13772(1309426618));
    foreach (long relationId in relationIDs)
    {
      if (sessionById.GetRelation(relationId, false) is DBRelation relation)
        relation.RepairViews();
    }
  }

  public string[] FindCycleRelations(Guid sessionGUID, long[] IDs)
  {
    IUserSession sys_session = UserSession.GetSessionByID(sessionGUID);
    IDbManager db = sys_session.IsAdmin ? (sys_session as UserSession).DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13773(870564068));
    List<string> result = new List<string>();
    foreach (long id in IDs)
    {
      IDBRelationsApplicabilityCollection applicabilityCollection = sys_session.GetRelationsApplicabilityCollection();
      DataTable dataTable = db.ExecuteDataTable("SELECT DISTINCT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_ID = :id1", db.Parameter("id1", (object) id));
      List<int> intList = new List<int>();
      foreach (DataRow row1 in (InternalDataCollectionBase) dataTable.Rows)
      {
        foreach (DataRow row2 in (InternalDataCollectionBase) applicabilityCollection.GetApplicabilitiesList(-1, -1, Convert.ToInt32(row1[0])).Rows)
        {
          int int32 = Convert.ToInt32(row2["F_RELATION_TYPE"]);
          if (!intList.Contains(int32))
            intList.Add(int32);
        }
      }
      for (int index = 0; index < intList.Count; ++index)
        ProcessObject(id, intList[index], new List<AdminUtilsService.ObjectIdentifiers4Pair>());
    }
    return result.ToArray();

    bool ProcessObject(
      long part_id,
      int relTypeID,
      List<AdminUtilsService.ObjectIdentifiers4Pair> chain)
    {
      if (AdminUtilsService.ObjectIdentifiers4Pair.Contains(chain, part_id))
      {
        result.Add($"Найдена петля по связи '{sys_session.GetRelationType(relTypeID).Description}':");
        for (int index = 0; index < chain.Count; ++index)
          result.Add(sys_session.GetObject(chain[index].ObjectID).NameInMessages);
        result.Add(sys_session.GetObjectByID(part_id, true).NameInMessages);
        result.Add("---------------------------------------------------------");
        return true;
      }
      foreach (DataRow row in (InternalDataCollectionBase) db.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id_par", db.Parameter("id_par", (object) part_id)).Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        chain.Add(new AdminUtilsService.ObjectIdentifiers4Pair(int64, part_id));
        ProcessObjectVersion(int64, new List<AdminUtilsService.ObjectIdentifiers4Pair>((IEnumerable<AdminUtilsService.ObjectIdentifiers4Pair>) chain));
      }
      return false;

      bool ProcessObjectVersion(
        long objectID,
        List<AdminUtilsService.ObjectIdentifiers4Pair> sub_chain)
      {
        DataTable dataTable = db.ExecuteDataTable("SELECT F_PART_ID FROM IMS_RELATIONS WHERE F_PROJ_ID = :projID AND F_RELATION_TYPE = :relType", db.Parameter("projID", (object) objectID), db.Parameter("relType", (object) relTypeID));
        for (int index = 0; index < dataTable.Rows.Count; ++index)
          ProcessObject(Convert.ToInt64(dataTable.Rows[index][0]), relTypeID, new List<AdminUtilsService.ObjectIdentifiers4Pair>((IEnumerable<AdminUtilsService.ObjectIdentifiers4Pair>) sub_chain));
        return false;
      }
    }
  }

  public long ConvertVersions2Object(Guid sessionGUID, long[] objectIDs)
  {
    if (objectIDs.Length == 0)
      throw new KernelException("Подан пустой массив версий объектов.");
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    IDbManager db = sessionById.IsAdmin ? sessionById.DataManager : throw new KernelExceptionID(sc_13686.ssp_appserver_13774(411176372));
    bool flag = false;
    long newValue = db.DataProvider.NextGeneratorValue("IMS_OBJECTS_GEN", db);
    StringBuilder stringBuilder = new StringBuilder();
    string traceFileName = "ConvertVersions2Object.log";
    sessionById.StartTransaction();
    try
    {
      DBObject dbObject = (DBObject) null;
      foreach (long objectId in objectIDs)
      {
        stringBuilder.Append(objectId.ToString() + ",");
        DBObject part_obj = sessionById.GetObject(objectId) as DBObject;
        long id = part_obj.ID;
        if (dbObject == null)
          dbObject = part_obj;
        else if ((part_obj.LCStepObject.Options & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion)
          dbObject = part_obj;
        if (part_obj.CheckoutBy != 0L)
          throw new KernelException($"Операция прервана, т.к. объект '{part_obj.NameInMessages}' взят на изменение пользователем {sessionById.GetObjectInfo(part_obj.CheckoutBy).Caption}");
        if (part_obj.IsBaseVersion)
        {
          if (flag)
            part_obj.SetBaseVersion(0L);
          else
            flag = true;
        }
        part_obj.SetSystemField("F_ID", 121, (object) newValue);
        foreach (DataRow row in (InternalDataCollectionBase) db.ExecuteDataTable("SELECT F_PRJLINK_ID FROM IMS_RELATIONS WHERE F_PART_ID = :partID", db.Parameter("partID", (object) id)).Rows)
        {
          if (sessionById.GetRelation(Convert.ToInt64(row[0])) is DBRelation relation)
          {
            IDBAttribute attributeById = relation.GetAttributeByID(sessionById.IdentHelper.CompositionVersionID);
            if (attributeById != null && !attributeById.IsNull && attributeById.AsInteger == objectId)
              relation.ReplacePartObjectInternal((IDBObject) part_obj);
          }
        }
      }
      if (!flag)
        dbObject.SetBaseVersion(1L);
      --stringBuilder.Length;
      db.ExecuteNonQuery(string.Format("DELETE FROM IMS_VERSIONS_TREE WHERE (F_PARENT_ID IN ({0})) AND (F_OBJECT_ID NOT IN ({0}))", (object) stringBuilder.ToString()));
      db.ExecuteNonQuery(string.Format("DELETE FROM IMS_VERSIONS_TREE WHERE (F_OBJECT_ID IN ({0})) AND (F_PARENT_ID NOT IN ({0}))", (object) stringBuilder.ToString()));
      sessionById.Commit();
      sessionById.EventLogHelper.AddToTrace($"Версии объектов с ид. {stringBuilder.ToString()} перенесены в объект с ид. {newValue}", traceFileName);
    }
    catch (Exception ex)
    {
      sessionById.Rollback();
      sessionById.EventLogHelper.AddToTrace($"Ошибка переноса объектов {stringBuilder.ToString()} : {ex.Message}", traceFileName);
      sessionById.EventLogHelper.AddToTrace(ex.StackTrace, traceFileName);
      throw;
    }
    return newValue;
  }

  public void ChangeObjectCreateDate(Guid sessionGUID, long objectID, DateTime createDate)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13775(1023375856));
    (sessionById.GetObject(objectID) as DBObject).SetCreateDate(createDate - sessionById.TimeZoneOffset);
  }

  public void CheckAdminProcedureAccess(Guid sessionGUID, string procName)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGUID) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13686.ssp_appserver_13776(613797160));
    try
    {
      sessionById.GetSystemSecurity().CheckAccess(ActionType.AdminProcedure, false, true);
      sessionById.EventLogHelper.AddEvent(-1L, -1L, 14, -1L, "Вызов административной процедуры", procName, ActionType.AdminProcedure, EventlogRecordType.AccessGranted, sessionById.UserID, sessionById.ComputerName, (IUserSession) sessionById);
    }
    catch (AccessDeniedException ex)
    {
      sessionById.EventLogHelper.AddEvent(-1L, -1L, 14, -1L, "Вызов административной процедуры", procName, ActionType.AdminProcedure, EventlogRecordType.AccessDenied, sessionById.UserID, sessionById.ComputerName, (IUserSession) sessionById);
      throw;
    }
  }

  private class ObjectIdentifiers4Pair
  {
    public long ObjectID { get; private set; }

    public long ID { get; private set; }

    public ObjectIdentifiers4Pair(long objectID, long id)
    {
      this.ObjectID = objectID;
      this.ID = id;
    }

    public static bool Contains(
      List<AdminUtilsService.ObjectIdentifiers4Pair> chain,
      long id)
    {
      foreach (AdminUtilsService.ObjectIdentifiers4Pair identifiers4Pair in chain)
      {
        if (identifiers4Pair.ID == id)
          return true;
      }
      return false;
    }
  }
}
