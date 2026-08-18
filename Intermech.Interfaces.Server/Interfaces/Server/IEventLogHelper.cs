// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IEventLogHelper
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IEventLogHelper : IServerEventLogService
{
  int GetAttributeID(object attributeID, bool failIfNotFound);

  int GetAttributeID(object attributeID);

  long AddEvent(
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
    IUserSession aSession);

  long CloseEvent(long EventID, EventlogRecordType AuditType, string Note, IUserSession aSession);

  long CloseEvent(
    long EventID,
    long ObjectID,
    long CategoryID,
    string ObjectName,
    string Note,
    EventlogRecordType AuditType,
    IUserSession aSession);

  long CloseEvent(
    long EventID,
    long ObjectID,
    long RelationID,
    long CategoryID,
    string ObjectName,
    string Note,
    EventlogRecordType AuditType,
    IUserSession aSession);

  void AddActionName(int categoryType, long categoryID, ActionType actType, string actionName);

  string GetActionName(int categoryType, long categoryID, ActionType actType);

  int TruncateTraceFile(string tracefileName, int tracefileSize);

  int TruncateTraceFile(string tracefileName);

  int AddToTrace(string EventStr, int TraceLevel, string TraceFileName);

  int AddToTrace(
    string EventStr,
    int TraceLevel,
    string TraceFileName,
    string ComputerName,
    string UserName);

  void TraceExeption(string caption, Exception e, string TraceFileName);

  event BeforeAddEventHandler BeforeAddEvent;

  event AfterAddEventHandler AfterAddEvent;

  event OnCloseEventHandler OnCloseEvent;

  event OnCloseEventHandler2 OnCloseEvent2;

  void AddAttributeWriteHandler(object attributeID, WriteAttributeValueHandler attributeHandler);

  void RemoveAttributeWriteHandler(object attributeID, WriteAttributeValueHandler attributeHandler);

  void AddAttributeDeleteHandler(object attributeID, DeleteAttributeHandler attributeHandler);

  void RemoveAttributeDeleteHandler(object attributeID, DeleteAttributeHandler attributeHandler);

  event DeleteRelationHandler BeforeDeleteRelationEvent;

  event DeleteRelationHandler AfterDeleteRelationEvent;

  [Obsolete]
  event CreateRelationHandler AfterCreateRelationEvent;

  event CreateRelationExHandler AfterCreateRelationExEvent;

  event BeforeCreateRelationHandler BeforeCreateRelationEvent;

  event ReplacePartObjectHandler BeforeReplacePartObjectEvent;

  event ReplacePartObjectHandler AfterReplacePartObjectEvent;

  event GetRecordsListHandler GetRecordsListEvent;

  event GetAttributeDefaultValueHandler GetAttributeDefaultValueEvent;

  event AfterCreateAttributeTypeHandler AfterCreateAttributeTypeEvent;

  event AfterCreateObjectTypeHandler AfterCreateObjectTypeEvent;

  event DeleteAttributeTypeHandler BeforeDeleteAttributeTypeEvent;

  event ChangeAttributeDataTypeHandler BeforeChangeAttributeDataTypeEvent;

  event ChangeAttributeDataTypeHandler ChangeAttributeDataTypeEvent;

  event DeleteAttributeTypeHandler AfterDeleteAttributeTypeEvent;

  event DeleteObjectTypeHandler BeforeDeleteObjectTypeEvent;

  event DeleteObjectTypeHandler AfterDeleteObjectTypeEvent;

  event CreateAttributeHandler CreateAttributeEvent;

  event ObjectEventHandler CreateObjectEvent;

  event ObjectEventHandler CommitCreationObjectEvent;

  event ObjectEventHandler AfterCommitCreationObjectEvent;

  event ObjectEventHandler BeforeUndoCheckoutEvent;

  event ObjectEventHandler AfterUndoCheckoutEvent;

  event CheckoutEventHandler AfterUndoCheckoutExEvent;

  event ObjectEventHandler BeforeCheckoutEvent;

  event ObjectEventHandler AfterCheckoutEvent;

  event ObjectEventHandler BeforeCheckinEvent;

  event ObjectEventHandler AfterCheckinEvent;

  event ObjectEventHandler BeforeSaveChangesEvent;

  event ObjectEventHandler AfterSaveChangesEvent;

  event ObjectEventHandler BeforeSaveToArcCopy;

  event ObjectEventHandler AfterSaveToArcCopy;

  event NextLCStepHandler BeforeNextLCStepEvent;

  event NextLCStepHandler AfterNextLCStepEvent;

  event GetObjectSecurityHandler GetObjectSecurity;

  event ObjectTypeChangeHandler BeforeChangeObjectTypeEvent;

  event ObjectTypeChangeHandler AfterChangeObjectTypeEvent;

  event LoginHandler AfterLoginEvent;

  event LoginHandler AfterLogoutEvent;

  event TransactionHandler StartTransactionEvent;

  event TransactionHandler CommitEvent;

  event TransactionHandler RollbackEvent;

  event BeforeRecordsSelectHandler BeforeRecordsSelectEvent;

  event CacheReloadHandler AfterCacheReload;

  event ServerSettingsReloadHandler AfterServerSettingsReload;

  event ObjectEventHandler BeforeObjectPrintEvent;

  event ObjectEventHandler BeforeObjectSaveToDiskEvent;

  event BoundaryCreateObjectHandler BeginCreateObjectEvent;

  event AfterCreateObjectHandler AfterCreateObjectEvent;

  event AfterCreateObjectHandler EndCreateObjectEvent;

  event BoundaryCreateObjectHandler CancelCreateObjectEvent;

  event ObjectEventHandler AfterPurgeObjectEvent;

  event ObjectEventHandler BeforePurgeObjectEvent;

  event ObjectDeleteEventHandler BeforePurgeObjectExtendedEvent;

  event CombineAttributesHandler BeforeCombineAttributesEvent;

  event CombineAttributesHandler AfterCombineAttributesEvent;

  event AttributeGroupIncludeExcludeHandler AfterIncludeAttributeToGroup;

  event AttributeGroupIncludeExcludeHandler AfterExcludeAttributeFromGroup;

  event RemoveRelationHandler BeforeRemoveRelationEvent;

  event RemoveRelationHandler AfterRemoveRelationEvent;

  event SetAttributesValuesHandler BeforeSetObjectAttributesValuesEvent;

  event SetAttributesValuesHandler BeforeSetRelationAttributesValuesEvent;

  event SetAttributesValuesHandler AfterSetObjectAttributesValuesEvent;

  event ObligatoryAttributeWriteHandler ObligatoryAttributeWrite;

  event GetUsedAttributesHandler GetUsedAttributesEvent;

  event SnapshotHandler AfterRestoreSnapshot;

  event SnapshotHandler BeforeRestoreSnapshot;
}
