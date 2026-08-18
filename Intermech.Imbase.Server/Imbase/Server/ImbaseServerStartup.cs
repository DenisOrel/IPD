// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseServerStartup
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Filters;
using Intermech.Imbase.Server.Params;
using Intermech.Imbase.Server.Receptures;
using Intermech.Imbase.Server.Sync;
using Intermech.Imbase.Server.Sync.Services;
using Intermech.Imbase.Server.Sync.SheduledTask;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Imbase.Receptures;
using Intermech.Interfaces.Imbase.Sync;
using Intermech.Interfaces.Plugins;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseServerStartup : IPackage
{
  private void iELH_AfterDeleteAttributeTypeEvent(IDBAttributeType sender, IUserSession session)
  {
    if (!(ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service))
      return;
    try
    {
      service.UpdateAfterAttributeDelete(session.SessionGUID, sender.AttributeID);
    }
    catch (IndexingException ex)
    {
      throw new KernelException(ex.Message, ex.InnerException);
    }
  }

  private void iELH_BeforeDeleteAttributeTypeEvent(IDBAttributeType sender, IUserSession session)
  {
    if (ServerServices.GetService(typeof (IImbaseIndexingService)) is IImbaseIndexingService service && !service.CheckBeforeAttributeDelete(session.SessionGUID, sender.AttributeID))
      throw new KernelException(LocalizationHolder.rm.GetString("Imbase_Indexing_CanNotDeleteAttribute"));
  }

  private void iELH_BeforeDeleteObjectTypeEvent(IDBObjectType sender, IUserSession session)
  {
    Guid objectTypeGuid = sender.PropertiesStructure.ObjectTypeGuid;
    IDBObjectCollection objectCollection1 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    ConditionStructure conditionStructure = new ConditionStructure(Intermech.Imbase.Consts.CreatedObjectAttID, RelationalOperators.Equal, (object) objectTypeGuid, LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.CAPTION
    });
    paramSet.Contents = new ColumnContents[1]
    {
      ColumnContents.String
    };
    DataTable dataTable = objectCollection1.Select(paramSet);
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      string str = $"{LocalizationHolder.rm.GetString("Imbase.Server.Startup.DelObjType.CatalogsRef")}\r\n";
      for (int index = 0; index < dataTable.Rows.Count - 1; ++index)
        str = $"{str}{dataTable.Rows[index][0]},\r\n";
      throw new Exception($"{str}{dataTable.Rows[dataTable.Rows.Count - 1][0]}.");
    }
    IDBObjectCollection objectCollection2 = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID);
    paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[2]
    {
      (object) Intermech.Imbase.Consts.ClassifFolderKeyAttId,
      (object) ObligatoryObjectAttributes.CAPTION
    });
    paramSet.Contents = new ColumnContents[2]
    {
      ColumnContents.String,
      ColumnContents.String
    };
    DataTable dtSource = objectCollection2.Select(paramSet);
    if (dtSource == null || dtSource.Rows.Count == 0)
      return;
    dtSource.Columns[0].ColumnName = "Key";
    dtSource.Columns[1].ColumnName = "Path";
    string columnName = TableLoadHelper.BuildFullPathForObject(dtSource, session);
    if (dtSource.Rows.Count > 0)
    {
      string str = $"{LocalizationHolder.rm.GetString("Imbase.Server.Startup.DelObjType.FoldersRef")}\r\n";
      if (dtSource.Columns.IndexOf(columnName) != -1)
      {
        for (int index = 0; index < dtSource.Rows.Count - 1; ++index)
          str = $"{str}{dtSource.Rows[index][columnName]},\r\n";
      }
      throw new Exception($"{str}{dtSource.Rows[dtSource.Rows.Count - 1][columnName]}.");
    }
  }

  public void Unload()
  {
  }

  public string Name => LocalizationHolder.rm.GetString("Imbase.Server_21");

  public void Load(IServiceProvider serviceProvider)
  {
    IUserSession userSession = (IUserSession) null;
    try
    {
      ICustomServices service1 = ServiceUtils.GetService<ICustomServices>((object) ServerServices.ServiceContainer, true);
      IDBTimedEvents service2 = ServiceUtils.GetService<IDBTimedEvents>((object) ServerServices.ServiceContainer, true);
      userSession = service2.GetSystemSessionTemporaryClone("imbase.startup");
      if (userSession == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullSession"));
      IMetaDataHelper service3 = ServiceUtils.GetService<IMetaDataHelper>((object) ServerServices.ServiceContainer, true);
      Intermech.Imbase.Consts.Initialize(userSession, service3);
      ImbaseParamsService serviceInstance1 = new ImbaseParamsService();
      service1.AddService(typeof (IImbaseParamsService), (object) serviceInstance1);
      ServerServices.AddService(typeof (IImbaseParamsService), (object) serviceInstance1);
      ImbaseExtendedService serviceInstance2 = new ImbaseExtendedService(userSession.SessionGUID);
      service1.AddService(typeof (IImbaseExtendedService), (object) serviceInstance2);
      ServerServices.AddService(typeof (IImbaseExtendedService), (object) serviceInstance2);
      ImbaseServer serviceInstance3 = new ImbaseServer();
      service1.AddService(typeof (IImbaseServer), (object) serviceInstance3);
      ServerServices.AddService(typeof (IImbaseServer), (object) serviceInstance3);
      ITablesCache serviceInstance4 = (ITablesCache) new TablesCache();
      TableLoadHelper.TablesCache = serviceInstance4;
      service1.AddService(typeof (ITablesCache), (object) serviceInstance4);
      ImbaseIndexingService serviceInstance5 = new ImbaseIndexingService();
      ServerServices.AddService(typeof (IImbaseIndexingService), (object) serviceInstance5);
      service1.AddService(typeof (IImbaseIndexingService), (object) serviceInstance5);
      SynchronizationObjService serviceInstance6 = new SynchronizationObjService();
      ServerServices.AddService(typeof (ISynchronizationObjService), (object) serviceInstance6);
      service1.AddService(typeof (ISynchronizationObjService), (object) serviceInstance6);
      ImbaseUpdatingService serviceInstance7 = new ImbaseUpdatingService();
      ServerServices.AddService(typeof (IImbaseUpdatingService), (object) serviceInstance7);
      service1.AddService(typeof (IImbaseUpdatingService), (object) serviceInstance7);
      TablesMergingService serviceInstance8 = new TablesMergingService();
      service1.AddService(typeof (ITablesMergingService), (object) serviceInstance8);
      ServerServices.AddService(typeof (ITablesMergingService), (object) serviceInstance8);
      ImbaseObjInfoService serviceInstance9 = new ImbaseObjInfoService();
      service1.AddService(typeof (IImbaseObjInfoService), (object) serviceInstance9);
      ServerServices.AddService(typeof (IImbaseObjInfoService), (object) serviceInstance9);
      CustomUsersTableFilterService serviceInstance10 = new CustomUsersTableFilterService();
      service1.AddService(typeof (ICustomUsersTableFilterService), (object) serviceInstance10);
      ServerServices.AddService(typeof (ICustomUsersTableFilterService), (object) serviceInstance10);
      RecepturesService serviceInstance11 = new RecepturesService();
      service1.AddService(typeof (IRecepturesService), (object) serviceInstance11);
      ServerServices.AddService(typeof (IRecepturesService), (object) serviceInstance11);
      FolderFilterService serviceInstance12 = new FolderFilterService();
      service1.AddService(typeof (IFolderFilterService), (object) serviceInstance12);
      ObjectFilterService serviceInstance13 = new ObjectFilterService();
      service1.AddService(typeof (IObjectFilterService), (object) serviceInstance13);
      service1.AddService(typeof (ITablesIndexer), (object) TablesIndexer.Instance);
      service1.AddService(typeof (IKeyConverter), (object) new KeyConverterService());
      service1.AddService(typeof (IImbaseSynchObjectsService), (object) new ImbaseSynchObjectsService());
      service1.AddService(typeof (IInverseImbaseSynchObjectsService), (object) new InverseImbaseSynchObjectsService());
      service1.AddService(typeof (IImbaseRestructuringTablesService), (object) new ImbaseRestructuringTablesService());
      service1.AddService(typeof (IUpdateObjectsFromImbaseService), (object) new UpdateObjectsFromImbaseService());
      service1.AddService(typeof (ITablesDisplayService), (object) new TablesDisplayService(userSession));
      service1.AddService(typeof (ITablesIndexerService), (object) new TablesIndexerService());
      service1.AddService(typeof (IImbaseSyncService), (object) new ImbaseSyncTaskService());
      service1.AddService(typeof (IImbaseTableMixPumpService), (object) new ImbaseTableMixPumpService());
      service1.AddService(typeof (IReplaceAttributeTaskService), (object) new ReplaceAttributeTaskService());
      ImbaseRestrictiveCache serviceInstance14 = new ImbaseRestrictiveCache();
      service1.AddService(typeof (IImbaseRestrictiveCache), (object) serviceInstance14);
      ServerServices.AddService(typeof (IImbaseRestrictiveCache), (object) serviceInstance14);
      SyncServices.RegisterServices();
      if (ServiceUtils.GetService<IDBObjectService>((object) ServerServices.ServiceContainer, true) is ICreatorContainer service4)
      {
        IDBObjectCreator creatorInstance = (IDBObjectCreator) new ImbaseObjectsCreator();
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseCatalogTypeGUID, (object) creatorInstance);
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseFolderTypeGUID, (object) creatorInstance);
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseTableRefTypeGUID, (object) creatorInstance);
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseTableTypeGUID, (object) creatorInstance);
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID, (object) creatorInstance);
        service4.AddCreator((object) Intermech.Imbase.Consts.ImbaseTableMixTypeGUID, (object) creatorInstance);
      }
      IEventLogHelper service5 = ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true);
      TablesIndexer.SubscribeOnSystemEvents(service5);
      ImbaseApplicabilityEventHandler.SubscribeOnSystemEvents(service5);
      serviceInstance11.SubscribeOnSystemEvents(service5);
      service5.BeforeDeleteObjectTypeEvent += new DeleteObjectTypeHandler(this.iELH_BeforeDeleteObjectTypeEvent);
      service5.BeforeDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(this.iELH_BeforeDeleteAttributeTypeEvent);
      service5.AfterDeleteAttributeTypeEvent += new DeleteAttributeTypeHandler(this.iELH_AfterDeleteAttributeTypeEvent);
      serviceInstance2.SubscribeOnSystemlEvents(service5);
      serviceInstance9.SubscribeOnSystemlEvents(service5);
      serviceInstance12.SubscribeOnSystemlEvents(service5);
      serviceInstance13.SubscribeOnSystemlEvents(service5);
      ImbaseClearDisplayTablesCacheService tablesCacheService = new ImbaseClearDisplayTablesCacheService();
      service5.StartTransactionEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) tablesCacheService).StartTransaction);
      service5.CommitEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) tablesCacheService).CommitTransaction);
      service5.RollbackEvent += new TransactionHandler(((ImbaseEventsSupportBaseService) tablesCacheService).RollBackTransaction);
      service5.BeforeNextLCStepEvent += new NextLCStepHandler(((ImbaseEventsSupportBaseService) tablesCacheService).BeforeObjNextLCStepHandler);
      service5.AfterNextLCStepEvent += new NextLCStepHandler(((ImbaseEventsSupportBaseService) tablesCacheService).AfterObjNextLCStepHandler);
      service5.BeforeCombineAttributesEvent += new CombineAttributesHandler(this.EventLogHelper_BeforeCombineAttributesEvent);
      service5.AfterCombineAttributesEvent += new CombineAttributesHandler(this.EventLogHelper_AfterCombineAttributesEvent);
      service5.AfterCacheReload += new CacheReloadHandler(this.IELH_AfterCacheReload);
      serviceInstance14.SubcribeEvents(service5);
      ICategoryExportManager service6 = ServiceUtils.GetService<ICategoryExportManager>((object) ServerServices.ServiceContainer, false);
      if (service6 != null)
      {
        ICategoryExport iCategoryExport = (ICategoryExport) new BriefcaseSupport();
        service6.RegisterCategoryExport(3, iCategoryExport);
      }
      ServiceUtils.GetService<ILinkedObjectsService>((object) ServerServices.ServiceContainer, false)?.RegisterHandler((ILinkedObjectsHandler) new PublishTableLinksHandler());
      ISpecHandleAttributes service7 = ServiceUtils.GetService<ISpecHandleAttributes>((object) ServerServices.ServiceContainer, false);
      if (service7 != null)
        service7.SpecHandleObjectAttributeEvent += new SpecHandleAttributeEventHandler(ImportTableBlobHandler.SpecHandleObjectAttributeEvent);
      IPortalEventsService service8 = ServiceUtils.GetService<IPortalEventsService>((object) ServerServices.ServiceContainer, false);
      if (service8 != null)
      {
        service8.ObjectImportedEvent += new ObjectImportedEventHandler(ImportTableBlobHandler.ObjectImportedEvent);
        service8.ImportTaskCompletedEvent += new ImportTaskCompletedEventHandler(ImportTableBlobHandler.ImportTaskCompletedEvent);
        service8.CheckPublishCompositionEvent += new CheckPublishCompositionEventHandler(CheckPublishComposition.CheckPublishCompositionEvent);
      }
      SyncSheduledTask timedService = new SyncSheduledTask();
      service2.RegisterService((object) timedService);
      TablesIndexer.Instance.CheckUpdateIndexes(userSession, false);
      ServiceUtils.GetService<IPluginManager>((object) ServerServices.ServiceContainer, true).LoadComplete += new EventHandler(this.Plugins_LoadComplete);
      ImbaseFolderStatusesProvider.Load(userSession.SessionGUID);
      this.CorrectScriptObjectFolderKey(userSession);
    }
    finally
    {
      userSession?.Logout("imbase.startup");
    }
  }

  private void CorrectScriptObjectFolderKey(IUserSession session)
  {
    string[] strArray = new string[5]
    {
      "cadd9bc4-306c-11d8-b4e9-00304f19f545",
      "cadd99ff-306c-11d8-b4e9-00304f19f545",
      "cadd9a2c-306c-11d8-b4e9-00304f19f545",
      "cadd9a30-306c-11d8-b4e9-00304f19f545",
      "cadd9a2e-306c-11d8-b4e9-00304f19f545"
    };
    ISelectionsService customService = session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
    foreach (string g in strArray)
    {
      IDBObject dbObject = session.GetObject(new Guid(g), false);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0014d-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid.AsString.StartsWith("!!"))
          attributeByGuid.Value = (object) customService.GenerateNextClassifierKey((object) session, dbObject.ObjectType, dbObject.ID);
      }
    }
  }

  private void Plugins_LoadComplete(object sender, EventArgs e)
  {
    ImbaseServer.Instance.CheckDBVersion();
    ServiceUtils.GetService<IImbaseIndexingService>((object) ServerServices.ServiceContainer, false)?.UpdateBase();
    ServiceUtils.GetService<IRecepturesService>((object) ServerServices.ServiceContainer, false)?.InitCache();
  }

  private void EventLogHelper_BeforeCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    try
    {
      IImbaseServer customService = (IImbaseServer) (session.GetCustomService(typeof (IImbaseServer)) as ImbaseServer);
      List<long> tablesWithAtt1 = customService.GetTablesWithAtt(session.SessionGUID, fromAttribute.AttributeID);
      if (tablesWithAtt1 == null || tablesWithAtt1.Count <= 0)
        return;
      DataTable tablesData = this.GetTablesData(session, tablesWithAtt1);
      if (tablesData == null)
        return;
      if (this.HasTablesCheckOutOtherUser(session.UserID, tablesData))
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_CheckOutTables_OtherUser"), (object) fromAttribute.Name));
      List<long> tablesWithAtt2 = customService.GetTablesWithAtt(session.SessionGUID, toAttribute.AttributeID);
      if (tablesWithAtt2 != null && tablesWithAtt2.Count > 0 && tablesWithAtt1.Intersect<long>((IEnumerable<long>) tablesWithAtt2).Count<long>() > 0 && combineMode == CombineAttributeMode.CancelOperation)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Attrs_InSameTables"), (object) fromAttribute.Name, (object) toAttribute.Name));
      if (fromAttribute.AttributeType != FieldTypes.ftMeasured)
        return;
      long int64_1 = Convert.ToInt64(fromAttribute.SizeType);
      long int64_2 = Convert.ToInt64(toAttribute.SizeType);
      long num = int64_2;
      if (int64_1 != num && int64_2 != -1L)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Attrs_DifferentPhysQuantities"), (object) fromAttribute.Name, (object) toAttribute.Name));
    }
    catch (Exception ex)
    {
      log = log ?? new List<string>();
      string str = string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Exception"), (object) fromAttribute.Name, (object) toAttribute.Name);
      log.Add($"{str}\r\n  {ex.Message}");
      throw;
    }
  }

  private DataTable GetTablesData(IUserSession session, List<long> objIDs)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableTypeID);
    if (objectCollection == null)
      throw new Exception(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Tables_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) objIDs.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -6, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  private bool HasTablesCheckOutOtherUser(long currentUserID, DataTable dt)
  {
    bool flag = false;
    Convert.ToString((object) ObligatoryObjectAttributes.F_OBJECT_ID);
    string columnName = Convert.ToString((object) ObligatoryObjectAttributes.F_CHKOUT_BY);
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      long int64 = Convert.ToInt64(row[columnName]);
      if (int64 != 0L && int64 != currentUserID)
      {
        flag = true;
        break;
      }
    }
    return flag;
  }

  private void EventLogHelper_AfterCombineAttributesEvent(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    List<string> messages1 = (List<string>) null;
    List<long> changedTableIDs = this.UpdateTables(session, fromAttribute, toAttribute, combineMode, out messages1);
    List<string> messages2 = (List<string>) null;
    this.UpdateIndexes(session, fromAttribute, toAttribute, changedTableIDs, out messages2);
    List<string> collection = new List<string>();
    if (messages1.Count > 0)
      collection.AddRange((IEnumerable<string>) messages1);
    if (messages2.Count > 0)
      collection.AddRange((IEnumerable<string>) messages2);
    if (collection.Count <= 0)
      return;
    if (log != null)
      log.AddRange((IEnumerable<string>) collection);
    else
      log = collection;
  }

  private void IELH_AfterCacheReload(IDbManager db)
  {
    if (!(ServerServices.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service))
      return;
    IUserSession sessionTemporaryClone = service.GetSystemSessionTemporaryClone("ImbaseServer.CacheReload");
    try
    {
      ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true).ResetSettings(sessionTemporaryClone, "common");
    }
    finally
    {
      sessionTemporaryClone?.Logout("ImbaseServer.CacheReload");
    }
  }

  private List<long> UpdateTables(
    IUserSession session,
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    CombineAttributeMode combineMode,
    out List<string> messages)
  {
    List<long> longList1 = new List<long>();
    messages = new List<string>();
    IImbaseServer customService = (IImbaseServer) (session.GetCustomService(typeof (IImbaseServer)) as ImbaseServer);
    try
    {
      List<long> tablesWithAtt = customService.GetTablesWithAtt(session.SessionGUID, fromAttribute.AttributeID);
      if (tablesWithAtt != null)
      {
        if (tablesWithAtt.Count > 0)
        {
          List<long> longList2 = customService.GetTablesWithAtt(session.SessionGUID, toAttribute.AttributeID) ?? new List<long>(0);
          Guid guid = fromAttribute.GUID;
          string strOldAttrGuid = guid.ToString();
          guid = toAttribute.GUID;
          string strNewAttrGuid = guid.ToString();
          string str = combineMode == CombineAttributeMode.LeaveData ? strOldAttrGuid : strNewAttrGuid;
          foreach (long tableID in tablesWithAtt)
          {
            try
            {
              this.ReplaceAttribute(session, tableID, longList2.Contains(tableID) ? str : string.Empty, strOldAttrGuid, strNewAttrGuid);
              longList1.Add(tableID);
            }
            catch (Exception ex)
            {
              messages.Add(ex.Message);
            }
          }
        }
      }
    }
    catch (Exception ex)
    {
      messages.Add(ex.Message);
    }
    if (messages.Count > 0)
      messages.Insert(0, string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Tables_UpdateInfo"), (object) fromAttribute.Name, (object) toAttribute.Name));
    return longList1.Count <= 0 ? (List<long>) null : longList1;
  }

  private void ReplaceAttribute(
    IUserSession session,
    long tableID,
    string strAttrGuid,
    string strOldAttrGuid,
    string strNewAttrGuid)
  {
    IDBObject tableObject = session.GetObjectActualCopy(tableID, false);
    if (tableObject == null)
      return;
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CantModifyMode"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    if (tableObject.ObjectModifyMode == ObjectModifyModes.CreateVersion)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CreateVersionMode"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    bool flag = false;
    if (tableObject.ObjectModifyMode == ObjectModifyModes.Checkout)
    {
      if (tableObject.CheckoutBy == 0L)
      {
        tableObject = tableObject.CheckOut();
        flag = true;
      }
      else if (tableObject.CheckoutBy != session.UserID)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Table_CheckOutOtherUser"), (object) tableObject.Caption, (object) tableObject.ObjectID.ToString()));
    }
    DataSet tablesInternal = TableLoadHelper.GetTablesInternal(tableObject);
    if (tablesInternal == null || !tablesInternal.Tables.Contains("IMS_DATA") || !tablesInternal.Tables.Contains("IMS_ATTR_TYPES"))
      return;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    DataTable table1 = tablesInternal.Tables["IMS_ATTR_TYPES"];
    DataTable table2 = tablesInternal.Tables["IMS_DATA"];
    if (!string.IsNullOrEmpty(strAttrGuid))
    {
      table1.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_ATTRIBUTE_GUID"]) == strAttrGuid))?.Delete();
      if (table2.Columns.Contains(strAttrGuid))
        table2.Columns.Remove(strAttrGuid);
    }
    foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
    {
      if (row.RowState != DataRowState.Deleted)
      {
        if (Convert.ToString(row["F_ATTRIBUTE_GUID"]) == strOldAttrGuid)
        {
          row["F_ATTRIBUTE_GUID"] = (object) strNewAttrGuid;
          if (table2.Columns.Contains(strOldAttrGuid))
            table2.Columns[strOldAttrGuid].ColumnName = strNewAttrGuid;
        }
        else
        {
          string str1 = Convert.ToString(row["F_FORMULA"]);
          if (!string.IsNullOrEmpty(str1))
          {
            string str2 = str1.Replace(strOldAttrGuid, strNewAttrGuid);
            if (!(str2 == str1))
              row["F_FORMULA"] = (object) str2;
          }
        }
      }
    }
    tablesInternal.AcceptChanges();
    TableLoadHelper.StoreData(session, tableID, tablesInternal, session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
    if (!flag)
      return;
    tableObject.CheckIn();
  }

  private void UpdateIndexes(
    IUserSession session,
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    List<long> changedTableIDs,
    out List<string> messages)
  {
    messages = new List<string>();
    IImbaseIndexingService customService = session.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    try
    {
      if (customService == null)
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_ImbaseIndexingSrv_Null"), (object) fromAttribute.Name));
      DataTable indexes = customService.GetIndexes(session.SessionGUID, (List<long>) null);
      if (indexes != null)
      {
        if (indexes.Rows.Count > 0)
        {
          List<string> collection = this.RemoveIndexes(session, customService, indexes, fromAttribute);
          if (collection != null)
            messages.AddRange((IEnumerable<string>) collection);
          if (changedTableIDs != null)
          {
            List<DataRow> list = indexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]) == toAttribute.AttributeID)).Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToList<DataRow>();
            if (list.Count > 0)
            {
              List<long> catalogIds = this.GetCatalogIDs(session, changedTableIDs);
              if (catalogIds != null)
              {
                string format = LocalizationHolder.rm.GetString("Imbase_CombineAttrs_NeedUpdateIndex");
                foreach (DataRow dataRow in list)
                {
                  long result = 0;
                  string fCatalogId = IndexesField.F_CATALOG_ID;
                  if (long.TryParse(Convert.ToString(dataRow[fCatalogId]), out result) && catalogIds.Contains(result))
                  {
                    QuickObjectInfo objectInfo = session.GetObjectInfo(result);
                    if (!objectInfo.Empty)
                      messages.Add(string.Format(format, (object) toAttribute.Name, (object) toAttribute.AttributeID.ToString(), (object) objectInfo.Caption, (object) result.ToString()));
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
      messages.Add(ex.Message);
    }
    if (messages.Count <= 0)
      return;
    messages.Insert(0, string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Indexes"), (object) fromAttribute.Name, (object) toAttribute.Name));
  }

  private List<string> RemoveIndexes(
    IUserSession session,
    IImbaseIndexingService iIIS,
    DataTable dtIndexes,
    IDBAttributeType attribute)
  {
    List<string> stringList = new List<string>();
    List<DataRow> list = dtIndexes.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x[IndexesField.F_ATTRIBUTE_ID]) == attribute.AttributeID)).Select<DataRow, DataRow>((System.Func<DataRow, DataRow>) (x => x)).ToList<DataRow>();
    if (list.Count > 0)
    {
      List<int> attrIDs = new List<int>()
      {
        attribute.AttributeID
      };
      foreach (DataRow dataRow in list)
      {
        long result = 0;
        string fCatalogId = IndexesField.F_CATALOG_ID;
        if (long.TryParse(Convert.ToString(dataRow[fCatalogId]), out result) && result != 0L)
        {
          Guid taskGuid = Guid.NewGuid();
          try
          {
            iIIS.Remove(session.SessionGUID, taskGuid, result, attrIDs);
            iIIS.RemoveAfterComplete(taskGuid);
          }
          catch (Exception ex)
          {
            stringList.Add(string.Format(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_Indexes_Remove"), (object) attribute.Name));
            stringList.Add(ex.Message);
          }
        }
      }
    }
    return stringList.Count <= 0 ? (List<string>) null : stringList;
  }

  private List<long> GetCatalogIDs(IUserSession session, List<long> tableIDs)
  {
    List<long> catalogIds = (List<long>) null;
    DataTable linksData = this.GetLinksData(session, tableIDs);
    if (linksData != null)
    {
      catalogIds = this.GetCatalogIDsFromClassifKeys(session, this.GetCatalogClassifKeys(linksData) ?? throw new Exception(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_ClassifKeys_Empty")));
      if (catalogIds == null)
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_CombineAttrs_CatalogIDs_Empty"));
    }
    return catalogIds;
  }

  private DataTable GetLinksData(IUserSession session, List<long> tableIDs)
  {
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    if (objectCollection == null)
      throw new Exception(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_TableRef_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseTableRefTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseTableRefAttID, RelationalOperators.In, (object) tableIDs.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    }, new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.ID, SortOrders.NONE, 0)
    });
    DataTable dataTable = objectCollection.Select(paramSet);
    return dataTable == null || dataTable.Rows.Count <= 0 ? (DataTable) null : dataTable;
  }

  private List<string> GetCatalogClassifKeys(DataTable dt)
  {
    List<string> source = new List<string>();
    string empty = string.Empty;
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
    {
      string str = Convert.ToString(row[SynchStrHelper.COLUMN_NAME_CLASSIF_KEY]);
      if (str.Length >= 2)
        source.Add(str.Substring(0, 2));
    }
    return source.Count <= 0 ? (List<string>) null : source.Distinct<string>().ToList<string>();
  }

  private List<long> GetCatalogIDsFromClassifKeys(IUserSession session, List<string> classifKeys)
  {
    List<long> longList = (List<long>) null;
    IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeID);
    if (objectCollection == null)
      throw new Exception(LocalizationHolder.rm.GetString("Imbase_ObjectCollection_Catalog_Error"));
    objectCollection.ObjectTypeID = Intermech.Imbase.Consts.ImbaseCatalogTypeID;
    DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.In, (object) classifKeys.ToArray(), LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    });
    DataTable source = objectCollection.Select(paramSet);
    if (source != null)
      longList = source.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))).ToList<long>();
    return longList == null || longList.Count <= 0 ? (List<long>) null : longList;
  }
}
