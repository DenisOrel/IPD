// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.EventLogHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Services;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Kernel.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;


namespace Intermech.Kernel;

public sealed class EventLogHelper : KernelRoot, IEventLogHelper, IServerEventLogService
{
  private int _CurrentTraceLevel;
  private readonly string _DefaultLogFileName = "imserver.log";
  private readonly string _LogFilePath;
  internal bool _TraceOn = true;
  internal ConcurrentDictionary<int, bool> NotLoggedTypes = new ConcurrentDictionary<int, bool>();
  internal ConcurrentDictionary<long, bool> NotLoggedObjects = new ConcurrentDictionary<long, bool>();
  internal bool _AutoClear = true;
  internal int _RecordKeepDays = 90;
  private static object thisLock = new object();
  private IEventLogWriter _systemEventLogWriter;
  private IEventHandlerSet<IDBAttribute, AttributeValueEventArgs> _AttributesWriteHandlers = EventHandlerSet<IDBAttribute, AttributeValueEventArgs>.CreateSynchronized();
  private IEventHandlerSet<IDBAttribute, AttributeValuesEventArgs> _AttributeValuesWriteHandlers = EventHandlerSet<IDBAttribute, AttributeValuesEventArgs>.CreateSynchronized();
  private IEventHandlerSet<IDBAttribute, AttributeDeleteEventArgs> _AttributeDeleteHandlers = EventHandlerSet<IDBAttribute, AttributeDeleteEventArgs>.CreateSynchronized();
  private IEventHandlerSet<IDBAttribute, AttributeDeleteValueEventArgs> _AttributeDeleteValueHandlers = EventHandlerSet<IDBAttribute, AttributeDeleteValueEventArgs>.CreateSynchronized();
  private TraceLoggerService _DelayedTraceLogger;
  private const string UnknownName = "<unknown>";

  public event BeforeAddEventHandler BeforeAddEvent;

  public event AfterAddEventHandler AfterAddEvent;

  public event OnCloseEventHandler OnCloseEvent;

  public event OnCloseEventHandler2 OnCloseEvent2;

  public EventLogHelper()
  {
    this._systemEventLogWriter = EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, ServerDiagnosticsConsts.EventLogSourceName);
    this._CurrentTraceLevel = Consts.traceError;
    try
    {
      string str = ConfigurationManager.AppSettings.Get("TraceLevel");
      if (!string.IsNullOrEmpty(str))
        this._CurrentTraceLevel = Convert.ToInt32(str);
    }
    catch
    {
    }
    this._LogFilePath = Environment.CurrentDirectory;
    try
    {
      string path = ConfigurationManager.AppSettings.Get("LogPath");
      if (!string.IsNullOrEmpty(path))
      {
        if (path.IndexOfAny(Path.GetInvalidPathChars()) == -1)
          this._LogFilePath = Path.GetFullPath(path);
      }
    }
    catch
    {
    }
    if (Directory.Exists(this._LogFilePath))
      return;
    Directory.CreateDirectory(this._LogFilePath);
  }

  public void LoadSettings(IDbManager db)
  {
    lock (this)
    {
      this.NotLoggedTypes.Clear();
      this.NotLoggedObjects.Clear();
      foreach (DataRow row in (InternalDataCollectionBase) db.ExecuteDataTable($"SELECT F_PARAM_NAME, F_VALUE FROM IMS_CONFIGS WHERE F_MODULE_NAME = {SqlHelper.QString("KERNEL")} AND F_USER_ID = 0 AND F_SECTION_ID = {SqlHelper.QString("EVENTS")}").Rows)
      {
        string str = row[0].ToString();
        if (str.Length > 0)
        {
          if (str[0] == 'T')
            this.NotLoggedTypes.TryAdd(Convert.ToInt32(row[1]), true);
          else if (str[0] == 'O')
          {
            this.NotLoggedObjects.TryAdd(Convert.ToInt64(row[1]), true);
          }
          else
          {
            switch (str)
            {
              case "SWITCH":
                this._TraceOn = row[1].ToString() != "0";
                continue;
              case "AUTO":
                this._AutoClear = row[1].ToString() != "0";
                continue;
              case "DAYS":
                try
                {
                  this._RecordKeepDays = Convert.ToInt32(row[1]);
                  continue;
                }
                catch
                {
                  continue;
                }
              default:
                continue;
            }
          }
        }
      }
    }
  }

  internal void LoadSettings(EventlogSettings settings)
  {
    lock (this)
    {
      this.NotLoggedTypes.Clear();
      this.NotLoggedObjects.Clear();
      this._TraceOn = settings.LogOn;
      this._AutoClear = settings.AutoClear;
      this._RecordKeepDays = settings.RecordsKeepDays;
      for (int index = 0; index < settings.NotLoggedObjects.Length; ++index)
        this.NotLoggedObjects.TryAdd(settings.NotLoggedObjects[index], true);
      for (int index = 0; index < settings.NotLoggedTypes.Length; ++index)
        this.NotLoggedTypes.TryAdd(settings.NotLoggedTypes[index], true);
    }
  }

  public int GetAttributeID(object attributeID, bool failIfNotFound)
  {
    int attributeId1 = -10000;
    switch (attributeID)
    {
      case null:
        if (failIfNotFound)
          throw new ArgumentNullException(nameof (attributeID));
        return 0;
      case ObligatoryObjectAttributes _:
        return (int) attributeID;
      case int attributeId2:
        return attributeId2;
      case long _:
        return Convert.ToInt32(attributeID);
      default:
        if (MetaDataHelper.SyncDateTime == DateTime.MinValue)
        {
          IDBTimedEvents service = ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents;
          IUserSession userSession = (IUserSession) null;
          try
          {
            userSession = service.GetSystemSessionTemporaryClone("Eventlog.GetAttributeID");
            MetaDataHelper.SyncMetadata((userSession as IUserSessionCacheDataSet).CacheDataSet);
          }
          finally
          {
            userSession?.Logout("Eventlog.GetAttributeID");
          }
        }
        if (attributeID is Guid attrTypeGuid)
        {
          attributeId1 = MetaDataHelper.GetAttributeTypeID(attrTypeGuid);
          if (attributeId1 != -10000)
            return attributeId1;
        }
        if (attributeID is string)
        {
          attributeId1 = MetaDataHelper.GetAttributeByTypeNameID((string) attributeID);
          if (attributeId1 != -10000)
            return attributeId1;
        }
        if (attributeID is string)
        {
          DataRow[] dataRowArray = (ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset).GetTable("IMS_ATTRIBUTES").Select("F_NAME = " + SqlHelper.QString((string) attributeID));
          if (dataRowArray.Length != 0)
          {
            attributeId1 = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
          }
          else
          {
            if (failIfNotFound)
              throw new KernelExceptionID(sc_13059.ssp_appserver_13060(258636136), attributeID);
            return -1;
          }
        }
        else if (attributeID is Guid)
        {
          DataRow[] dataRowArray = (ServerServices.GetService(typeof (ICacheDataset)) as ICacheDataset).GetTable("IMS_ATTRIBUTES").Select("F_GUID = " + SqlHelper.QString(attributeID.ToString()));
          if (dataRowArray.Length != 0)
          {
            attributeId1 = Convert.ToInt32(dataRowArray[0]["F_ATTRIBUTE_ID"]);
          }
          else
          {
            if (failIfNotFound)
              throw new KernelExceptionID(sc_13059.ssp_appserver_13061(288594747), attributeID);
            return -1;
          }
        }
        return !failIfNotFound || attributeId1 != -10000 ? attributeId1 : throw new KernelException($"Тип атрибута '{attributeID}' не найден.");
    }
  }

  public int GetAttributeID(object attributeID) => this.GetAttributeID(attributeID, true);

  public event CombineAttributesHandler BeforeCombineAttributesEvent;

  internal void OnBeforeCombineAttributes(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    if (this.BeforeCombineAttributesEvent == null)
      return;
    this.BeforeCombineAttributesEvent(fromAttribute, toAttribute, session, combineMode, log);
  }

  public event CombineAttributesHandler AfterCombineAttributesEvent;

  internal void OnAfterCombineAttributes(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session,
    CombineAttributeMode combineMode,
    List<string> log)
  {
    if (this.AfterCombineAttributesEvent == null)
      return;
    this.AfterCombineAttributesEvent(fromAttribute, toAttribute, session, combineMode, log);
  }

  public void AddAttributeWriteHandler(
    object attributeID,
    WriteAttributeValueHandler attributeHandler)
  {
    this._AttributesWriteHandlers.AddHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeValueEventArgs>(attributeHandler.Invoke));
  }

  public void RemoveAttributeWriteHandler(
    object attributeID,
    WriteAttributeValueHandler attributeHandler)
  {
    this._AttributesWriteHandlers.RemoveHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeValueEventArgs>(attributeHandler.Invoke));
  }

  internal void OnAttributeWriteEvent(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    this._AttributesWriteHandlers.Fire((object) 0, attribute, args);
    this._AttributesWriteHandlers.Fire((object) attribute.AttributeID, attribute, args);
  }

  public event ObligatoryAttributeWriteHandler ObligatoryAttributeWrite;

  internal void OnObligatoryAttributeWrite(
    IDBObject sender,
    ObligatoryObjectAttributes attrID,
    ObligatoryAttributeValueEventArgs args)
  {
    if (this.ObligatoryAttributeWrite == null)
      return;
    this.ObligatoryAttributeWrite(sender, attrID, args);
  }

  public void AddAttributeValuesWriteHandler(
    object attributeID,
    WriteAttributeValuesHandler attributeHandler)
  {
    this._AttributeValuesWriteHandlers.AddHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeValuesEventArgs>(attributeHandler.Invoke));
  }

  public void RemoveAttributeValuesWriteHandler(
    object attributeID,
    WriteAttributeValuesHandler attributeHandler)
  {
    this._AttributeValuesWriteHandlers.RemoveHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeValuesEventArgs>(attributeHandler.Invoke));
  }

  internal void OnAttributeValuesWriteEvent(IDBAttribute attribute, AttributeValuesEventArgs args)
  {
    this._AttributeValuesWriteHandlers.Fire((object) 0, attribute, args);
    this._AttributeValuesWriteHandlers.Fire((object) attribute.AttributeID, attribute, args);
  }

  public void AddAttributeDeleteHandler(object attributeID, DeleteAttributeHandler attributeHandler)
  {
    this._AttributeDeleteHandlers.AddHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeDeleteEventArgs>(attributeHandler.Invoke));
  }

  public void RemoveAttributeDeleteHandler(
    object attributeID,
    DeleteAttributeHandler attributeHandler)
  {
    this._AttributeDeleteHandlers.RemoveHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeDeleteEventArgs>(attributeHandler.Invoke));
  }

  internal void OnAttributeDeleteEvent(IDBAttribute attribute, AttributeDeleteEventArgs args)
  {
    this._AttributeDeleteHandlers.Fire((object) 0, attribute, args);
    this._AttributeDeleteHandlers.Fire((object) attribute.AttributeID, attribute, args);
  }

  public void AddAttributeDeleteValueHandler(
    object attributeID,
    DeleteAttributeValueHandler attributeHandler)
  {
    this._AttributeDeleteValueHandlers.AddHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeDeleteValueEventArgs>(attributeHandler.Invoke));
  }

  public void RemoveAttributeDeleteValueHandler(
    object attributeID,
    DeleteAttributeValueHandler attributeHandler)
  {
    this._AttributeDeleteValueHandlers.RemoveHandler((object) this.GetAttributeID(attributeID), new Action<IDBAttribute, AttributeDeleteValueEventArgs>(attributeHandler.Invoke));
  }

  internal void OnAttributeDeleteValueEvent(
    IDBAttribute attribute,
    AttributeDeleteValueEventArgs args)
  {
    this._AttributeDeleteValueHandlers.Fire((object) 0, attribute, args);
    this._AttributeDeleteValueHandlers.Fire((object) attribute.AttributeID, attribute, args);
  }

  public event AttributeGroupIncludeExcludeHandler AfterIncludeAttributeToGroup;

  internal void OnAfterIncludeAttributeToGroup(IDBAttributesGroup sender, int attributeID)
  {
    if (this.AfterIncludeAttributeToGroup == null)
      return;
    this.AfterIncludeAttributeToGroup(sender, attributeID);
  }

  public event AttributeGroupIncludeExcludeHandler AfterExcludeAttributeFromGroup;

  internal void OnAfterExcludeAttributeFromGroup(IDBAttributesGroup sender, int attributeID)
  {
    if (this.AfterExcludeAttributeFromGroup == null)
      return;
    this.AfterExcludeAttributeFromGroup(sender, attributeID);
  }

  public event GetObjectSecurityHandler GetObjectSecurity;

  internal void OnGetObjectSecurity(
    IDBObject sender,
    GetObjectSecurityEventArgs args,
    IUserSession session)
  {
    if (this.GetObjectSecurity == null)
      return;
    this.GetObjectSecurity(sender, args, session);
  }

  public event ServerSettingsReloadHandler AfterServerSettingsReload;

  internal void OnServerSettingsReload(IUserSession session)
  {
    if (this.AfterServerSettingsReload == null)
      return;
    this.AfterServerSettingsReload(session);
  }

  public event CacheReloadHandler AfterCacheReload;

  internal void OnCacheReload(IDbManager db)
  {
    if (this.AfterCacheReload == null)
      return;
    this.AfterCacheReload(db);
  }

  public event SnapshotHandler AfterRestoreSnapshot;

  internal void OnAfterRestoreSnapshot(IDBObjectSnapshot sender, IDBObject dBObject)
  {
    SnapshotHandler afterRestoreSnapshot = this.AfterRestoreSnapshot;
    if (afterRestoreSnapshot == null)
      return;
    afterRestoreSnapshot(sender, dBObject);
  }

  public event SnapshotHandler BeforeRestoreSnapshot;

  internal void OnBeforeRestoreSnapshot(IDBObjectSnapshot sender, IDBObject dBObject)
  {
    SnapshotHandler beforeRestoreSnapshot = this.BeforeRestoreSnapshot;
    if (beforeRestoreSnapshot == null)
      return;
    beforeRestoreSnapshot(sender, dBObject);
  }

  public event ClearTrashHandler AfterClearTrash;

  internal void OnClearTrash(IUserSession session, List<string> clearLog)
  {
    if (this.AfterClearTrash == null)
      return;
    this.AfterClearTrash(session, clearLog);
  }

  public event RelationsApplicabilityHandler AfterCreateApplicability;

  internal void OnCreateApplicability(
    IUserSession session,
    RelationsApplicabilityProperties applicabilityProperties)
  {
    if (this.AfterCreateApplicability == null)
      return;
    this.AfterCreateApplicability(session, applicabilityProperties);
  }

  public event RelationsApplicabilityHandler BeforeDeleteApplicability;

  internal void OnBeforeDeleteApplicability(
    IUserSession session,
    RelationsApplicabilityProperties applicabilityProperties)
  {
    if (this.BeforeDeleteApplicability == null)
      return;
    this.BeforeDeleteApplicability(session, applicabilityProperties);
  }

  public event RelationsApplicabilityHandler AfterDeleteApplicability;

  internal void OnAfterDeleteApplicability(
    IUserSession session,
    RelationsApplicabilityProperties applicabilityProperties)
  {
    if (this.AfterDeleteApplicability == null)
      return;
    this.AfterDeleteApplicability(session, applicabilityProperties);
  }

  public event DeleteRelationHandler BeforeDeleteRelationEvent;

  public event DeleteRelationHandler AfterDeleteRelationEvent;

  internal void OnBeforeDeleteRelation(IDBRelation sender, long deleteMode, IUserSession session)
  {
    if (this.BeforeDeleteRelationEvent == null)
      return;
    this.BeforeDeleteRelationEvent(sender, deleteMode, session);
  }

  internal void OnAfterDeleteRelation(IDBRelation sender, long deleteMode, IUserSession session)
  {
    if (this.AfterDeleteRelationEvent == null)
      return;
    this.AfterDeleteRelationEvent(sender, deleteMode, session);
  }

  public event RemoveRelationHandler BeforeRemoveRelationEvent;

  public event RemoveRelationHandler AfterRemoveRelationEvent;

  internal void OnBeforeRemoveRelation(IDBRelation sender, IUserSession session)
  {
    if (this.BeforeRemoveRelationEvent == null)
      return;
    this.BeforeRemoveRelationEvent(sender, session);
  }

  internal void OnAfterRemoveRelation(IDBRelation sender, IUserSession session)
  {
    if (this.AfterRemoveRelationEvent == null)
      return;
    this.AfterRemoveRelationEvent(sender, session);
  }

  public event ReplacePartObjectHandler BeforeReplacePartObjectEvent;

  public event ReplacePartObjectHandler AfterReplacePartObjectEvent;

  internal void OnBeforeReplacePartObject(
    IDBRelation sender,
    long oldPartID,
    IDBObject newPart,
    IUserSession session)
  {
    if (this.BeforeReplacePartObjectEvent == null)
      return;
    this.BeforeReplacePartObjectEvent(sender, oldPartID, newPart, session);
  }

  internal void OnAfterReplacePartObject(
    IDBRelation sender,
    long oldPartID,
    IDBObject newPart,
    IUserSession session)
  {
    if (this.AfterReplacePartObjectEvent == null)
      return;
    this.AfterReplacePartObjectEvent(sender, oldPartID, newPart, session);
  }

  public event LoginHandler AfterLoginEvent;

  public event LoginHandler AfterLogoutEvent;

  internal void OnLogin(IUserSession session)
  {
    if (this.AfterLoginEvent == null)
      return;
    this.AfterLoginEvent(session);
  }

  internal void OnLogout(IUserSession session)
  {
    if (this.AfterLogoutEvent == null)
      return;
    this.AfterLogoutEvent(session);
  }

  public event TransactionHandler StartTransactionEvent;

  public event TransactionHandler CommitEvent;

  public event TransactionHandler RollbackEvent;

  internal void OnStartTransaction(IUserSession session)
  {
    if (this.StartTransactionEvent == null)
      return;
    this.StartTransactionEvent(session);
  }

  internal void OnCommit(IUserSession session)
  {
    if (this.CommitEvent == null)
      return;
    this.CommitEvent(session);
  }

  internal void OnRollback(IUserSession session)
  {
    if (this.RollbackEvent == null)
      return;
    this.RollbackEvent(session);
  }

  [Obsolete]
  public event CreateRelationHandler AfterCreateRelationEvent;

  internal void OnAfterCreateRelation(IDBRelation sender, IUserSession session)
  {
    if (this.AfterCreateRelationEvent == null)
      return;
    this.AfterCreateRelationEvent(sender, session);
  }

  public event CreateRelationExHandler AfterCreateRelationExEvent;

  internal void OnAfterCreateRelationEx(IDBRelation sender, IUserSession session, int assignMode)
  {
    if (this.AfterCreateRelationExEvent == null)
      return;
    this.AfterCreateRelationExEvent(sender, session, assignMode);
  }

  public event BeforeCreateRelationHandler BeforeCreateRelationEvent;

  internal void OnBeforeCreateRelation(
    IDBObject parentObject,
    long childID,
    DateTime beginDate,
    long prjlinkID,
    IUserSession session,
    IDBRelationCollection relations,
    DataTable versionsTable)
  {
    if (this.BeforeCreateRelationEvent == null)
      return;
    this.BeforeCreateRelationEvent(parentObject, childID, beginDate, prjlinkID, session, relations, versionsTable);
  }

  public event GetRecordsListHandler GetRecordsListEvent;

  internal void OnGetRecordsList(
    DataTable table,
    object sender,
    DBRecordSetParams parameters,
    IUserSession session)
  {
    if (this.GetRecordsListEvent == null)
      return;
    Delegate[] invocationList = this.GetRecordsListEvent.GetInvocationList();
    for (int index = invocationList.Length - 1; index >= 0; --index)
      ((GetRecordsListHandler) invocationList[index])(table, sender, parameters, session);
  }

  public event BeforeRecordsSelectHandler BeforeRecordsSelectEvent;

  internal void OnBeforeRecordsSelect(object sender, BeforeRecordsSelectEventArgs args)
  {
    if (this.BeforeRecordsSelectEvent == null)
      return;
    this.BeforeRecordsSelectEvent(sender, args);
  }

  public event GetAttributeDefaultValueHandler GetAttributeDefaultValueEvent;

  internal object OnGetAttributeDefaultValue(
    IDBAttribute sender,
    object defaultValue,
    IUserSession session)
  {
    if (this.GetAttributeDefaultValueEvent != null)
    {
      AttributeDefaultValueEventArgs args = new AttributeDefaultValueEventArgs(defaultValue);
      this.GetAttributeDefaultValueEvent(sender, args, session);
      if (args.NewValue != null)
        return args.NewValue;
    }
    return defaultValue;
  }

  public event AfterCreateAttributeTypeHandler AfterCreateAttributeTypeEvent;

  internal void OnAfterCreateAttributeType(IDBAttributeType sender, IUserSession session)
  {
    if (this.AfterCreateAttributeTypeEvent == null)
      return;
    this.AfterCreateAttributeTypeEvent(sender, session);
  }

  public event AfterCreateObjectTypeHandler AfterCreateObjectTypeEvent;

  internal void OnAfterCreateObjectType(IDBObjectType sender, IUserSession session)
  {
    if (this.AfterCreateObjectTypeEvent == null)
      return;
    this.AfterCreateObjectTypeEvent(sender, session);
  }

  public event BoundaryCreateObjectHandler BeginCreateObjectEvent;

  public event AfterCreateObjectHandler AfterCreateObjectEvent;

  public event AfterCreateObjectHandler EndCreateObjectEvent;

  public event BoundaryCreateObjectHandler CancelCreateObjectEvent;

  internal void OnBeginCreateObject(
    Guid objectVersionGuid,
    int objectType,
    IDBObject prototype,
    IUserSession session)
  {
    if (this.BeginCreateObjectEvent == null)
      return;
    this.BeginCreateObjectEvent(objectVersionGuid, objectType, prototype, session);
  }

  internal void OnAfterCreateObject(IDBObject newObject, IDBObject prototype, IUserSession session)
  {
    if (this.AfterCreateObjectEvent == null)
      return;
    this.AfterCreateObjectEvent(newObject, prototype, session);
  }

  internal void OnEndCreateObject(IDBObject newObject, IDBObject prototype, IUserSession session)
  {
    if (this.EndCreateObjectEvent == null)
      return;
    this.EndCreateObjectEvent(newObject, prototype, session);
  }

  internal void OnCancelCreateObject(
    Guid objectVersionGuid,
    int objectType,
    IDBObject prototype,
    IUserSession session)
  {
    if (this.CancelCreateObjectEvent == null)
      return;
    foreach (BoundaryCreateObjectHandler invocation in this.CancelCreateObjectEvent.GetInvocationList())
    {
      try
      {
        invocation(objectVersionGuid, objectType, prototype, session);
      }
      catch
      {
      }
    }
  }

  internal void OnAfterPurgeObjectEvent(IDBObject sender)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (this.AfterPurgeObjectEvent == null)
      return;
    this.AfterPurgeObjectEvent(sender, sender.Session);
  }

  internal void OnBeforePurgeObjectEvent(IDBObject sender)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (this.BeforePurgeObjectEvent == null)
      return;
    this.BeforePurgeObjectEvent(sender, sender.Session);
  }

  internal void OnBeforePurgeObjectExtendedEvent(IDBObject sender, ObjectDeleteEventArgs args)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (this.BeforePurgeObjectExtendedEvent == null)
      return;
    this.BeforePurgeObjectExtendedEvent(sender, args);
  }

  public event DeleteAttributeTypeHandler BeforeDeleteAttributeTypeEvent;

  internal void OnBeforeDeleteAttributeType(IDBAttributeType sender, IUserSession session)
  {
    if (this.BeforeDeleteAttributeTypeEvent == null)
      return;
    this.BeforeDeleteAttributeTypeEvent(sender, session);
  }

  public event DeleteAttributePossibleValueHandler DeleteAttributePossibleValueEvent;

  internal void OnDeleteAttributePossibleValue(IDBAttributeType sender, object deletedValue)
  {
    if (this.DeleteAttributePossibleValueEvent == null)
      return;
    this.DeleteAttributePossibleValueEvent(sender, deletedValue);
  }

  public event DeleteAttributeTypeHandler AfterDeleteAttributeTypeEvent;

  internal void OnAfterDeleteAttributeType(IDBAttributeType sender, IUserSession session)
  {
    if (this.AfterDeleteAttributeTypeEvent == null)
      return;
    this.AfterDeleteAttributeTypeEvent(sender, session);
  }

  public event ChangeAttributeDataTypeHandler BeforeChangeAttributeDataTypeEvent;

  internal void OnBeforeChangeAttributeDataType(
    IDBAttributeType sender,
    FieldTypes newDataType,
    IUserSession session)
  {
    if (this.BeforeChangeAttributeDataTypeEvent == null)
      return;
    this.BeforeChangeAttributeDataTypeEvent(sender, newDataType, session);
  }

  public event ChangeAttributeDataTypeHandler ChangeAttributeDataTypeEvent;

  internal void OnChangeAttributeDataType(
    IDBAttributeType sender,
    FieldTypes newDataType,
    IUserSession session)
  {
    if (this.ChangeAttributeDataTypeEvent == null)
      return;
    this.ChangeAttributeDataTypeEvent(sender, newDataType, session);
  }

  public event DeleteObjectTypeHandler BeforeDeleteObjectTypeEvent;

  internal void OnBeforeDeleteObjectType(IDBObjectType sender, IUserSession session)
  {
    if (this.BeforeDeleteObjectTypeEvent == null)
      return;
    this.BeforeDeleteObjectTypeEvent(sender, session);
  }

  public event DeleteObjectTypeHandler AfterDeleteObjectTypeEvent;

  internal void OnAfterDeleteObjectType(IDBObjectType sender, IUserSession session)
  {
    if (this.AfterDeleteObjectTypeEvent == null)
      return;
    this.AfterDeleteObjectTypeEvent(sender, session);
  }

  public event CreateAttributeHandler CreateAttributeEvent;

  internal void OnCreateAttribute(IDBAttribute sender, IUserSession session)
  {
    if (this.CreateAttributeEvent == null)
      return;
    this.CreateAttributeEvent(sender, session);
  }

  public event ObjectEventHandler CreateObjectEvent;

  public event ObjectEventHandler CommitCreationObjectEvent;

  public event ObjectEventHandler AfterCommitCreationObjectEvent;

  public event ObjectEventHandler BeforeUndoCheckoutEvent;

  public event ObjectEventHandler AfterUndoCheckoutEvent;

  public event CheckoutEventHandler AfterUndoCheckoutExEvent;

  public event ObjectEventHandler BeforeCheckoutEvent;

  public event ObjectEventHandler AfterCheckoutEvent;

  public event ObjectEventHandler BeforeCheckinEvent;

  public event ObjectEventHandler AfterCheckinEvent;

  public event ObjectEventHandler BeforeSaveChangesEvent;

  public event ObjectEventHandler AfterSaveChangesEvent;

  public event ObjectEventHandler BeforeSaveToArcCopy;

  public event ObjectEventHandler AfterSaveToArcCopy;

  public event NextLCStepHandler BeforeNextLCStepEvent;

  public event NextLCStepHandler AfterNextLCStepEvent;

  public event ObjectTypeChangeHandler BeforeChangeObjectTypeEvent;

  public event ObjectTypeChangeHandler AfterChangeObjectTypeEvent;

  public event GetUsedAttributesHandler GetUsedAttributesEvent;

  internal void OnGetUsedAttributes(IUserSession session, UsedAttributesEventArgs args)
  {
    if (this.GetUsedAttributesEvent == null)
      return;
    this.GetUsedAttributesEvent(session, args);
  }

  public event ObjectEventHandler BeforeObjectPrintEvent;

  public event ObjectEventHandler BeforeObjectSaveToDiskEvent;

  public event ObjectEventHandler AfterPurgeObjectEvent;

  public event ObjectEventHandler BeforePurgeObjectEvent;

  public event ObjectDeleteEventHandler BeforePurgeObjectExtendedEvent;

  public event SetAttributesValuesHandler BeforeSetObjectAttributesValuesEvent;

  public event SetAttributesValuesHandler BeforeSetRelationAttributesValuesEvent;

  public event SetAttributesValuesHandler AfterSetObjectAttributesValuesEvent;

  internal void OnAfterSetObjectAttributesValues(
    IDBAttributable sender,
    AttributesValuesEventArgs args)
  {
    if (this.AfterSetObjectAttributesValuesEvent == null)
      return;
    this.AfterSetObjectAttributesValuesEvent(sender, args);
  }

  internal void OnBeforeSetObjectAttributesValues(
    IDBAttributable sender,
    AttributesValuesEventArgs args)
  {
    if (this.BeforeSetObjectAttributesValuesEvent == null)
      return;
    this.BeforeSetObjectAttributesValuesEvent(sender, args);
  }

  internal void OnBeforeSetRelationAttributesValues(
    IDBAttributable sender,
    AttributesValuesEventArgs args)
  {
    if (this.BeforeSetRelationAttributesValuesEvent == null)
      return;
    this.BeforeSetRelationAttributesValuesEvent(sender, args);
  }

  internal void OnBeforeChangeObjectType(IDBObject sender, int objectTypeID, IUserSession session)
  {
    if (this.BeforeChangeObjectTypeEvent == null)
      return;
    this.BeforeChangeObjectTypeEvent(sender, objectTypeID, session);
  }

  internal void OnAfterChangeObjectType(IDBObject sender, int objectTypeID, IUserSession session)
  {
    if (this.AfterChangeObjectTypeEvent == null)
      return;
    this.AfterChangeObjectTypeEvent(sender, objectTypeID, session);
  }

  internal void OnBeforeNextLCStep(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    ((LCStepScriptService) ApplicationServices.Container.GetService(typeof (ILCScriptService)))?.ExecuteScript(sender, nextstep, session);
    if (this.BeforeNextLCStepEvent == null)
      return;
    this.BeforeNextLCStepEvent(sender, nextstep, session);
  }

  internal void OnAfterNextLCStep(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (this.AfterNextLCStepEvent == null)
      return;
    this.AfterNextLCStepEvent(sender, nextstep, session);
  }

  internal void OnCreateObject(IDBObject sender, IUserSession session)
  {
    if (this.CreateObjectEvent == null)
      return;
    this.CreateObjectEvent(sender, session);
  }

  internal void OnCommitCreationObject(IDBObject sender, IUserSession session)
  {
    if (this.CommitCreationObjectEvent == null)
      return;
    this.CommitCreationObjectEvent(sender, session);
  }

  internal void OnAfterCommitCreationObject(IDBObject sender, IUserSession session)
  {
    if (this.AfterCommitCreationObjectEvent == null)
      return;
    this.AfterCommitCreationObjectEvent(sender, session);
  }

  internal void OnBeforeUndoCheckout(IDBObject sender, IUserSession session)
  {
    if (this.BeforeUndoCheckoutEvent == null)
      return;
    this.BeforeUndoCheckoutEvent(sender, session);
  }

  internal void OnAfterUndoCheckout(IDBObject sender, IUserSession session)
  {
    if (this.AfterUndoCheckoutEvent == null)
      return;
    this.AfterUndoCheckoutEvent(sender, session);
  }

  internal void OnAfterUndoCheckoutEx(IDBObject sender, ObjectDeleteEventArgs args)
  {
    if (this.AfterUndoCheckoutExEvent == null)
      return;
    this.AfterUndoCheckoutExEvent(sender, args);
  }

  internal void OnBeforeCheckout(IDBObject sender, IUserSession session)
  {
    if (this.BeforeCheckoutEvent == null)
      return;
    this.BeforeCheckoutEvent(sender, session);
  }

  internal void OnAfterCheckout(IDBObject sender, IUserSession session)
  {
    if (this.AfterCheckoutEvent == null)
      return;
    this.AfterCheckoutEvent(sender, session);
  }

  internal void OnBeforeCheckin(IDBObject sender, IUserSession session)
  {
    if (this.BeforeCheckinEvent == null)
      return;
    this.BeforeCheckinEvent(sender, session);
  }

  internal void OnAfterCheckin(IDBObject sender, IUserSession session)
  {
    if (this.AfterCheckinEvent == null)
      return;
    this.AfterCheckinEvent(sender, session);
  }

  internal void OnBeforeSaveChanges(IDBObject sender, IUserSession session)
  {
    if (this.BeforeSaveChangesEvent == null)
      return;
    this.BeforeSaveChangesEvent(sender, session);
  }

  internal void OnAfterSaveChanges(IDBObject sender, IUserSession session)
  {
    if (this.AfterSaveChangesEvent == null)
      return;
    this.AfterSaveChangesEvent(sender, session);
  }

  internal void OnBeforeSaveToArcCopy(DBObject sender, UserSession session)
  {
    if (this.BeforeSaveToArcCopy == null)
      return;
    this.BeforeSaveToArcCopy((IDBObject) sender, (IUserSession) session);
  }

  internal void OnAfterSaveToArcCopy(DBObject sender, UserSession session)
  {
    if (this.AfterSaveToArcCopy == null)
      return;
    this.AfterSaveToArcCopy((IDBObject) sender, (IUserSession) session);
  }

  internal void OnBeforeObjectPrint(IDBObject sender, IUserSession session)
  {
    if (this.BeforeObjectPrintEvent == null)
      return;
    this.BeforeObjectPrintEvent(sender, session);
  }

  internal void OnBeforeObjectSaveToDiskEvent(IDBObject sender, IUserSession session)
  {
    if (this.BeforeObjectSaveToDiskEvent == null)
      return;
    this.BeforeObjectSaveToDiskEvent(sender, session);
  }

  public void AddActionName(
    int categoryType,
    long categoryID,
    ActionType actType,
    string actionName)
  {
    Intermech.Interfaces.EventLog.Helper.AddActionName(categoryType, categoryID, actType, actionName);
  }

  public string GetActionName(int categoryType, long categoryID, ActionType actType)
  {
    return ClientEventLogHelper.GetActionName(new CategoryValue(categoryType, categoryID, actType), Intermech.Interfaces.EventLog.Helper.ActionNames);
  }

  private IDbManager GetDbManager(IUserSession aSession, out bool doClose)
  {
    doClose = false;
    IDbManager dbManager = (IDbManager) null;
    if (aSession is UserSession userSession)
      dbManager = userSession.DataManager;
    if (dbManager == null)
    {
      doClose = true;
      dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager();
    }
    return dbManager;
  }

  public long AddEvent(
    long ObjectID,
    long RelationID,
    int CategoryType,
    long CategoryID,
    string ObjectName,
    string Note,
    ActionType EventType,
    EventlogRecordType AuditType,
    long UserID,
    string ComputerName,
    IUserSession aSession)
  {
    if (Note == null)
      Note = string.Empty;
    if (aSession != null && aSession.UserID == UserID && (aSession as UserSession).ActingUserID != 0L)
      UserID = (aSession as UserSession).ActingUserID;
    if (this.BeforeAddEvent != null)
      this.BeforeAddEvent(ObjectID, RelationID, CategoryType, CategoryID, ObjectName, Note, EventType, AuditType, UserID, ComputerName, aSession);
    if (!this._TraceOn || this.NotLoggedObjects.ContainsKey(UserID) && EventType != ActionType.Login)
      return 0;
    ObjectID = Math.Abs(ObjectID);
    CategoryID = Math.Abs(CategoryID);
    if (CategoryType == 4 && this.NotLoggedTypes.ContainsKey((int) CategoryID) && EventType != ActionType.Login || ObjectID > 0L && this.NotLoggedObjects.ContainsKey(ObjectID) && EventType != ActionType.Login)
      return 0;
    bool doClose = false;
    IDbManager dbManager = this.GetDbManager(aSession, out doClose);
    try
    {
      long num = 0;
      string str = (string) null;
      if (ObjectName.Length > Consts.MaxObjectNameLength)
        ObjectName = ObjectName.Substring(0, Consts.MaxObjectNameLength);
      if (Note.Length > Consts.MaxNoteLength)
        Note = Note.Substring(0, Consts.MaxNoteLength);
      if (aSession != null && aSession.IsDelayedEventlog)
        return (aSession as UserSession).AddEvent(new EventlogProperties(CategoryType, CategoryID, ObjectID, RelationID, ObjectName, UserID, ComputerName, Note, EventType, AuditType));
      if (ComputerName.Length > 20)
      {
        str = ComputerName;
        if (str.Length > 40)
          str = str.Substring(0, 40);
        ComputerName = ComputerName.Substring(0, 20);
      }
      dbManager.ExecuteSpNonQuery("IMS_ADD_EVENTLOG", dbManager.Parameter("inCATEGORY_TYPE", (object) CategoryType), dbManager.Parameter("inCATEGORY_ID", (object) CategoryID), dbManager.Parameter("inOBJECT_ID", (object) ObjectID), dbManager.Parameter("inRELATION_ID", (object) RelationID), dbManager.Parameter("inOBJECT_NAME", (object) ObjectName), dbManager.Parameter("inUSER_ID", (object) UserID), dbManager.Parameter("inCOMPUTER_NAME", (object) ComputerName), dbManager.Parameter("inNOTE", (object) Note), dbManager.Parameter("inEVENT_TYPE", (object) (int) EventType), dbManager.Parameter("inAUDIT_TYPE", (object) (int) AuditType), dbManager.OutputParameter("outEVENT_ID", (object) num));
      long int64 = Convert.ToInt64(dbManager.GetOutputParameterValue("outEVENT_ID"));
      if (str != null)
      {
        dbManager.ExecuteNonQuery("UPDATE IMS_EVENTLOG SET F_COMPUTER_NAME = :compName WHERE F_EVENT_ID = :evID", dbManager.Parameter("compName", (object) str), dbManager.Parameter("evID", (object) int64));
        ComputerName = str;
      }
      if (this.AfterAddEvent != null)
        this.AfterAddEvent(ObjectID, RelationID, CategoryType, CategoryID, ObjectName, Note, EventType, AuditType, UserID, ComputerName, aSession, int64);
      return int64;
    }
    finally
    {
      if (doClose)
        dbManager.Dispose();
    }
  }

  public long CloseEvent(
    long EventID,
    long ObjectID,
    long CategoryID,
    string ObjectName,
    string Note,
    EventlogRecordType AuditType,
    IUserSession aSession)
  {
    if (EventID != 0L)
    {
      ObjectID = Math.Abs(ObjectID);
      CategoryID = Math.Abs(CategoryID);
      if (ObjectName.Length > Consts.MaxObjectNameLength)
        ObjectName = ObjectName.Substring(0, Consts.MaxObjectNameLength);
      if (Note.Length > Consts.MaxNoteLength)
        Note = Note.Substring(0, Consts.MaxNoteLength);
      bool doClose = false;
      long num = aSession != null ? ((aSession as UserSession).ActingUserID != 0L ? (aSession as UserSession).ActingUserID : aSession.UserID) : 0L;
      if (aSession != null && aSession.IsDelayedEventlog && (aSession as UserSession).SessionStatus == UserSessionStatus.Logged)
        return (aSession as UserSession).CloseEvent(new EventlogProperties(EventID, CategoryID, ObjectID, 0L, ObjectName, Note, AuditType));
      IDbManager dbManager = this.GetDbManager(aSession, out doClose);
      try
      {
        dbManager.ExecuteNonQuery($"UPDATE IMS_EVENTLOG SET F_END_DATE = {dbManager.DataProvider.Now}, F_OBJECT_ID = :objID, F_CATEGORY_ID = :catID, F_OBJECT_NAME = :objName, F_NOTE = :note, F_AUDIT_TYPE = :auType, F_USER_ID = :usrID WHERE F_EVENT_ID = :evID", dbManager.Parameter("objID", (object) ObjectID), dbManager.Parameter("catID", (object) CategoryID), dbManager.Parameter("objName", (object) ObjectName), dbManager.Parameter("note", (object) Note), dbManager.Parameter("auType", (object) (int) AuditType), dbManager.Parameter("usrID", (object) num), dbManager.Parameter("evID", (object) EventID));
        if (this.OnCloseEvent2 != null)
          this.OnCloseEvent2(EventID, ObjectID, CategoryID, ObjectName, Note, AuditType, aSession);
      }
      finally
      {
        if (doClose)
          dbManager.Dispose();
      }
    }
    return EventID;
  }

  public long CloseEvent(
    long EventID,
    long ObjectID,
    long RelationID,
    long CategoryID,
    string ObjectName,
    string Note,
    EventlogRecordType AuditType,
    IUserSession aSession)
  {
    if (EventID != 0L)
    {
      ObjectID = Math.Abs(ObjectID);
      CategoryID = Math.Abs(CategoryID);
      if (ObjectName.Length > Consts.MaxObjectNameLength)
        ObjectName = ObjectName.Substring(0, Consts.MaxObjectNameLength);
      if (Note.Length > Consts.MaxNoteLength)
        Note = Note.Substring(0, Consts.MaxNoteLength);
      bool doClose = false;
      long num = aSession != null ? ((aSession as UserSession).ActingUserID != 0L ? (aSession as UserSession).ActingUserID : aSession.UserID) : 0L;
      if (aSession != null && aSession.IsDelayedEventlog && (aSession as UserSession).SessionStatus == UserSessionStatus.Logged)
        return (aSession as UserSession).CloseEvent(new EventlogProperties(EventID, CategoryID, ObjectID, RelationID, ObjectName, Note, AuditType));
      IDbManager dbManager = this.GetDbManager(aSession, out doClose);
      try
      {
        dbManager.ExecuteNonQuery($"UPDATE IMS_EVENTLOG SET F_END_DATE = {dbManager.DataProvider.Now}, F_OBJECT_ID = :objID, F_RELATION_ID = :relID, F_CATEGORY_ID = :catID, F_OBJECT_NAME = :objName, F_NOTE = :note, F_AUDIT_TYPE = :auType, F_USER_ID = :usrID WHERE F_EVENT_ID = :evID", dbManager.Parameter("objID", (object) ObjectID), dbManager.Parameter("relID", (object) RelationID), dbManager.Parameter("catID", (object) CategoryID), dbManager.Parameter("objName", (object) ObjectName), dbManager.Parameter("note", (object) Note), dbManager.Parameter("auType", (object) (int) AuditType), dbManager.Parameter("usrID", (object) num), dbManager.Parameter("evID", (object) EventID));
        if (this.OnCloseEvent2 != null)
          this.OnCloseEvent2(EventID, ObjectID, CategoryID, ObjectName, Note, AuditType, aSession);
      }
      finally
      {
        if (doClose)
          dbManager.Dispose();
      }
    }
    return EventID;
  }

  public long CloseEvent(
    long EventID,
    EventlogRecordType AuditType,
    string Note,
    IUserSession aSession)
  {
    if (EventID != 0L)
    {
      bool doClose = false;
      IDbManager dbManager = this.GetDbManager(aSession, out doClose);
      try
      {
        string str1 = Note;
        if (str1.Length > Consts.MaxStringSize)
          str1 = str1.Substring(0, Consts.MaxStringSize);
        if (aSession != null && aSession.IsDelayedEventlog)
          return (aSession as UserSession).CloseEvent(new EventlogProperties(EventID, str1, AuditType));
        string str2 = !(str1 == "$NO$") ? ", F_NOTE = " + SqlHelper.QString(str1) : "";
        dbManager.ExecuteNonQuery($"UPDATE IMS_EVENTLOG SET F_END_DATE = {dbManager.DataProvider.Now}, F_AUDIT_TYPE = {Convert.ToString((int) AuditType)}{str2} WHERE F_EVENT_ID = :evID", dbManager.Parameter("evID", (object) EventID));
        if (this.OnCloseEvent != null)
          this.OnCloseEvent(EventID, Note, AuditType, aSession);
      }
      finally
      {
        if (doClose)
          dbManager.Dispose();
      }
    }
    return EventID;
  }

  public int TruncateTraceFile(string tracefileName, int tracefileSize)
  {
    string fullTraceFileName = this.GetFullTraceFileName(tracefileName);
    if (!File.Exists(fullTraceFileName))
      return 0;
    FileInfo fileInfo = new FileInfo(fullTraceFileName);
    if (fileInfo.Length <= (long) tracefileSize)
      return Convert.ToInt32(fileInfo.Length);
    fileInfo.Delete();
    return 0;
  }

  public int TruncateTraceFile(string tracefileName)
  {
    return this.TruncateTraceFile(tracefileName, Consts.traceFileMaxSize);
  }

  internal string GetFullTraceFileName(string tracefileName)
  {
    if (string.IsNullOrEmpty(tracefileName))
      return Path.Combine(this._LogFilePath, this._DefaultLogFileName);
    return !Path.IsPathRooted(tracefileName) ? Path.Combine(this._LogFilePath, tracefileName) : tracefileName;
  }

  public int AddToTrace(string EventStr, int TraceLevel, string TraceFileName)
  {
    if (TraceLevel <= this._CurrentTraceLevel)
    {
      try
      {
        if (this._DelayedTraceLogger == null)
          this._DelayedTraceLogger = ServerServices.GetService(typeof (ITraceLoggerService)) as TraceLoggerService;
        if (this._DelayedTraceLogger != null)
        {
          this._DelayedTraceLogger.AddToTrace(EventStr, this.GetFullTraceFileName(TraceFileName));
        }
        else
        {
          using (StreamWriter streamWriter = new StreamWriter(this.GetFullTraceFileName(TraceFileName), true))
          {
            if (EventStr == "")
              streamWriter.WriteLine(EventStr);
            else
              streamWriter.WriteLine($"{DateTime.Now.ToString()}> {EventStr}");
          }
        }
        return 0;
      }
      catch (Exception ex)
      {
        if (AdminUtilsService.ServerRunMode == ServerRunModes.Console)
          Console.WriteLine("AddToTrace error for file '{0}': {1}", (object) TraceFileName, (object) ex.Message);
        else
          this._systemEventLogWriter.Write($"AddToTrace error for file '{TraceFileName}': {ex.Message}", EventLogItemType.Error);
      }
    }
    return 1;
  }

  public int AddToTrace(
    string EventStr,
    int TraceLevel,
    string TraceFileName,
    string ComputerName,
    string UserName)
  {
    string EventStr1;
    if (string.IsNullOrEmpty(EventStr))
    {
      EventStr1 = string.Empty;
    }
    else
    {
      if (string.IsNullOrEmpty(ComputerName))
        ComputerName = "<unknown>";
      if (string.IsNullOrEmpty(UserName))
        UserName = "<unknown>";
      EventStr1 = $"Workstation: {ComputerName}, User: {UserName}, {EventStr}";
    }
    if (!string.IsNullOrEmpty(TraceFileName))
    {
      TraceFileName = Path.GetFileName(TraceFileName);
      if (TraceFileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
        TraceFileName = string.Empty;
    }
    return this.AddToTrace(EventStr1, TraceLevel, TraceFileName);
  }

  public void TraceExeption(string caption, Exception e, string TraceFileName)
  {
    if (e == null)
      return;
    TraceFileName = TraceFileName ?? string.Empty;
    caption = caption ?? "Exception";
    this.AddToTrace($"{caption}: \"{e.Message}\"", Consts.traceAlways, TraceFileName);
    if (!string.IsNullOrEmpty(e.StackTrace))
      this.AddToTrace($"Exception Stack Trace: \"{e.StackTrace}\"", Consts.traceAlways, TraceFileName);
    else
      this.AddToTrace($"Environment Stack Trace: \"{Environment.StackTrace}\"", Consts.traceAlways, TraceFileName);
    e = e.InnerException;
    int num = 1;
    for (; e != null; e = e.InnerException)
    {
      this.AddToTrace($"[Inner exception {num}]: \"{e.Message}\"", Consts.traceAlways, TraceFileName);
      if (!string.IsNullOrEmpty(e.StackTrace))
        this.AddToTrace($"[Exception Stack Trace {num}]: \"{e.StackTrace}\"", Consts.traceAlways, TraceFileName);
    }
  }

  void IServerEventLogService.AddToTrace(string text, string traceFileName)
  {
    this.AddToTrace(text, Consts.traceAlways, traceFileName);
  }

  void IServerEventLogService.AddToTrace(string text, int traceLevel, string traceFileName)
  {
    this.AddToTrace(text, traceLevel, traceFileName);
  }
}
