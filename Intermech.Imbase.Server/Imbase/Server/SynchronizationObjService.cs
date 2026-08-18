// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.SynchronizationObjService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Synchronization;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Localization;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server;

internal class SynchronizationObjService : LongLifeObject, ISynchronizationObjService
{
  private HashSet<int> _systemAttributeIds = new HashSet<int>();

  public SynchronizationObjService() => this.Init();

  private void Init() => this.PopulateSystemAttrs();

  private void PopulateSystemAttrs()
  {
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTableViewAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.CatalogTypeAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ObjectSortOrderAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTableRefAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTableDataAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseObjectRefAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTableRecordOwnerAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.CreatedObjectAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.CreateNewObjectAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTemplateRefAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseTemplateDataAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseInternalOldKeyAttID);
    this._systemAttributeIds.Add(Intermech.Imbase.Consts.ImbaseNTDLinkAttId);
    this._systemAttributeIds.Add(MetaDataHelper.GetAttributeTypeID("cad0062f-306c-11d8-b4e9-00304f19f545"));
    this._systemAttributeIds.Add(MetaDataHelper.GetAttributeTypeID("cad0013a-306c-11d8-b4e9-00304f19f545"));
  }

  public SynchObjectsStatus Synchronize(
    IUserSession session,
    long objId,
    long imbaseObjId,
    long recId,
    bool createVersion,
    out string message)
  {
    string str = string.Empty;
    message = string.Empty;
    ILogSupport log = (ILogSupport) new LogSupport(ApplicationServices.Container.GetService<IImbaseParamsService>().GetUserParams(session.SessionGUID).UseExtendedLog);
    SynchObjectsStatus synchObjectsStatus;
    try
    {
      IDBObject objectActualCopy = session.GetObjectActualCopy(objId, true);
      str = objectActualCopy.NameInMessages;
      log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Extended, $"Синхронизация объекта {str} [{objectActualCopy.ObjectID}]:");
      SynchronizationAttributesAnalyzer attributesAnalyzer = new SynchronizationAttributesAnalyzer(session, this._systemAttributeIds, objectActualCopy, imbaseObjId, recId, log);
      attributesAnalyzer.Analyze();
      attributesAnalyzer.State = (IAttributeAnalyzerState) new CheckBaseMaterialAttributeState();
      attributesAnalyzer.Analyze();
      synchObjectsStatus = new SynchronizationAttributesUpdater(session, objectActualCopy, imbaseObjId, recId, attributesAnalyzer.DifferentAttributeValues, createVersion, log).Update();
      this.LinkNormativeDoc(session, objId, imbaseObjId, recId, log);
      message = log.GetLog();
    }
    catch (Exception ex)
    {
      message = message != string.Empty ? ex.Message + Environment.NewLine + message : ex.Message;
      synchObjectsStatus = SynchObjectsStatus.NotSynchronized;
    }
    message = $"Объект {str} [{objId}] {EnumTypeHelper.GetCaption((Enum) synchObjectsStatus)}{Environment.NewLine}{Environment.NewLine}{message}";
    return synchObjectsStatus;
  }

  private void LinkNormativeDoc(
    IUserSession session,
    long objId,
    long imbaseObjId,
    long recId,
    ILogSupport log)
  {
    if (!ImbaseNtdDocLink.CheckNtdObjects(session, imbaseObjId, recId, objId))
      return;
    string message = Environment.NewLine + LocalizationHolder.rm.GetString("Imbase.Server_39");
    log.AddMessage(Intermech.Imbase.Server.Synchronization.MessageType.Extended, message);
  }
}
