// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Filters.ObjFilterCacheService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server.Filters;

internal class ObjFilterCacheService
{
  private readonly ObjFilterCache _objCache;
  private DateTime _reloadTime;
  private int _captionFilterAttrId;
  private readonly IDictionary<long, IMSLifeCycleStep> _obj2LcStepBefore = (IDictionary<long, IMSLifeCycleStep>) new ConcurrentDictionary<long, IMSLifeCycleStep>();

  private void InitializeData()
  {
    new ObjectFilterCacheThreadLoader(this._objCache).Execute(!ObjFilterCacheService.Consts.DelayedDataLoad);
    this._reloadTime = DateTime.Now;
  }

  private ObjFilterCacheItem GetCacheItem(long objectId, IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    ObjFilterCacheItem cacheItem1 = this._objCache.GetItem(objectId);
    if (cacheItem1 != null)
      return cacheItem1;
    IDBObject dbObject = session.GetObject(objectId, false);
    if (dbObject == null)
      return (ObjFilterCacheItem) null;
    if (!MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return (ObjFilterCacheItem) null;
    ImbaseObjFilterInfo filterInfo = ImbaseObjFilterDataHelper.GetFilterInfo(dbObject);
    ObjFilterCacheItem cacheItem2 = new ObjFilterCacheItem(filterInfo);
    ImbaseObjFilterData filterData;
    if (!ObjFilterCacheService.Consts.DelayedDataLoad && ImbaseObjFilterDataHelper.LoadFilterData(filterInfo.ObjectID, session, out filterData))
      cacheItem2.Data = filterData;
    this._objCache.AddItem(cacheItem2);
    return cacheItem2;
  }

  private void SynchCacheData(IUserSession session)
  {
    if (DateTime.Now.Subtract(this._reloadTime).TotalMinutes < (double) ObjFilterCacheService.Consts.CacheLifeTime)
      return;
    this._reloadTime = DateTime.Now;
    new ObjectFilterCacheThreadLoader(this._objCache).Execute(false);
  }

  public ObjFilterCacheService()
  {
    this._objCache = new ObjFilterCache();
    this.InitializeData();
  }

  internal void SubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.BeforeNextLCStepEvent += new NextLCStepHandler(this.DoBeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent += new NextLCStepHandler(this.DoAfterObjNextLCStepHandler);
    eventHelper.CommitCreationObjectEvent += new ObjectEventHandler(this.DoImObjectCommitCreationEvent);
    int attributeID = -50;
    eventHelper.AddAttributeWriteHandler((object) attributeID, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeID, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545");
    eventHelper.AddAttributeWriteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
    eventHelper.AddAttributeWriteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrGuid);
    eventHelper.AddAttributeWriteHandler((object) attributeTypeId3, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.AddAttributeDeleteHandler((object) attributeTypeId3, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    IMSObjectType objectType = MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseObjFilterTypeID);
    if (objectType != null && objectType.CaptionAttribute != 0)
    {
      this._captionFilterAttrId = objectType.CaptionAttribute;
      eventHelper.AddAttributeWriteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
      eventHelper.AddAttributeDeleteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    }
    eventHelper.AfterCacheReload += new CacheReloadHandler(this.AfterCacheReloadHandler);
  }

  internal void UnsubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.BeforeNextLCStepEvent -= new NextLCStepHandler(this.DoBeforeObjNextLCStepHandler);
    eventHelper.AfterNextLCStepEvent -= new NextLCStepHandler(this.DoAfterObjNextLCStepHandler);
    eventHelper.CommitCreationObjectEvent -= new ObjectEventHandler(this.DoImObjectCommitCreationEvent);
    int attributeID = -50;
    eventHelper.RemoveAttributeWriteHandler((object) attributeID, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeID, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545");
    eventHelper.RemoveAttributeWriteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeTypeId1, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545");
    eventHelper.RemoveAttributeWriteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeTypeId2, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    int attributeTypeId3 = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.ImbaseFilterOwnerAttrGuid);
    eventHelper.RemoveAttributeWriteHandler((object) attributeTypeId3, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
    eventHelper.RemoveAttributeDeleteHandler((object) attributeTypeId3, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    if (this._captionFilterAttrId != 0)
    {
      eventHelper.RemoveAttributeWriteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
      eventHelper.RemoveAttributeDeleteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
      this._captionFilterAttrId = 0;
    }
    eventHelper.AfterCacheReload -= new CacheReloadHandler(this.AfterCacheReloadHandler);
  }

  public List<ImbaseObjFilterInfo> GetFilterList(IUserSession session, int refObjTypeId)
  {
    this.SynchCacheData(session);
    List<ImbaseObjFilterInfo> filterList = new List<ImbaseObjFilterInfo>();
    foreach (ObjFilterCacheItem objFilterCacheItem in this._objCache.GetItems())
    {
      if (objFilterCacheItem != null && objFilterCacheItem.Info != null && (refObjTypeId == -2 || refObjTypeId == objFilterCacheItem.Info.RefObjTypeID))
        filterList.Add(objFilterCacheItem.Info);
    }
    return filterList;
  }

  public bool GetFilterData(
    IUserSession session,
    long filterObjId,
    out ImbaseObjFilterData filterData)
  {
    filterData = (ImbaseObjFilterData) null;
    if (session == null || filterObjId == 0L || filterObjId == -1L)
      return false;
    this.SynchCacheData(session);
    ObjFilterCacheItem cacheItem = this.GetCacheItem(filterObjId, session);
    if (cacheItem == null)
      return false;
    if (cacheItem.Data != null)
    {
      filterData = cacheItem.Data;
      return true;
    }
    IDBObject filterObject = session.GetObject(filterObjId, false);
    if (filterObject == null)
    {
      this._objCache.RemoveItem(cacheItem);
      return false;
    }
    if (!ImbaseObjFilterDataHelper.LoadFilterData(filterObject, out filterData))
      filterData = new ImbaseObjFilterData();
    cacheItem.Data = filterData;
    return true;
  }

  public bool SetFilterData(IUserSession session, long filterObjId, ImbaseObjFilterData filterData)
  {
    if (session == null || filterObjId == 0L || filterObjId == -1L)
      return false;
    this.SynchCacheData(session);
    ObjFilterCacheItem cacheItem = this.GetCacheItem(filterObjId, session);
    if (cacheItem == null)
      return false;
    if (!session.IsAdmin)
    {
      Guid guid = string.IsNullOrEmpty(cacheItem.Info.Owner) ? Guid.Empty : new Guid(cacheItem.Info.Owner);
      IDBObject dbObject = session.GetObject(session.UserID);
      if (dbObject == null || dbObject.GUID != guid && dbObject.ObjectGUID != guid)
        return false;
    }
    IDBObject filterObject = session.GetObject(filterObjId, false);
    if (filterObject == null)
      return false;
    int num = ImbaseObjFilterDataHelper.SaveFilterData(filterObject, filterData) ? 1 : 0;
    if (num == 0)
      return num != 0;
    cacheItem.Data = filterData;
    return num != 0;
  }

  private void DoImObjectDelete(IDBObject dbObject)
  {
    if (dbObject == null || !MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return;
    ObjFilterCacheItem cacheItem = this._objCache.GetItem(dbObject.ObjectID);
    if (cacheItem == null)
      return;
    this._objCache.RemoveItem(cacheItem);
  }

  private void AfterCacheReloadHandler(IDbManager db)
  {
    IEventLogHelper service = ServiceUtils.GetService<IEventLogHelper>((object) ServerServices.ServiceContainer, true);
    if (this._captionFilterAttrId != 0)
    {
      service.RemoveAttributeWriteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
      service.RemoveAttributeDeleteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
      this._captionFilterAttrId = 0;
    }
    IMSObjectType objectType = MetaDataHelper.GetObjectType(Intermech.Imbase.Consts.ImbaseObjFilterTypeID);
    if (objectType != null && objectType.CaptionAttribute != 0)
    {
      this._captionFilterAttrId = objectType.CaptionAttribute;
      service.AddAttributeWriteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.WriteAttributeValueHandler(this.WriteAttributeValueHandler));
      service.AddAttributeDeleteHandler((object) this._captionFilterAttrId, new Intermech.Interfaces.Server.DeleteAttributeHandler(this.DeleteAttributeHandler));
    }
    this._reloadTime = DateTime.Now;
    new ObjectFilterCacheThreadLoader(this._objCache).Execute(!ObjFilterCacheService.Consts.DelayedDataLoad);
  }

  private void DoBeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return;
    this._obj2LcStepBefore[sender.ObjectID] = MetaDataHelper.GetLCStep(sender.LCStep);
  }

  private void DoAfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || !MetaDataHelper.IsObjectTypeChildOf(sender.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return;
    session = session ?? sender.Session;
    IMSLifeCycleStep imsLifeCycleStep;
    if (!this._obj2LcStepBefore.TryGetValue(sender.ObjectID, out imsLifeCycleStep))
      return;
    try
    {
      if (imsLifeCycleStep == null || imsLifeCycleStep.LevelID != session.IdentHelper.DeletedID && nextstep.LevelID != session.IdentHelper.DeletedID)
        return;
      if (nextstep.LevelID == session.IdentHelper.DeletedID)
        this.DoImObjectDelete(sender);
      else
        this.DoImObjectCommitCreationEvent(sender, sender.Session);
    }
    finally
    {
      this._obj2LcStepBefore.Remove(sender.ObjectID);
    }
  }

  private void DoImObjectCommitCreationEvent(IDBObject dbObject, IUserSession session)
  {
    if (dbObject == null)
      return;
    if (!MetaDataHelper.IsObjectTypeChildOf(dbObject.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return;
    try
    {
      this.GetCacheItem(dbObject.ObjectID, session ?? dbObject.Session);
    }
    catch
    {
      throw;
    }
  }

  private void DeleteAttributeHandler(IDBAttribute attribute, AttributeDeleteEventArgs args)
  {
    this.WriteAttributeValueHandler(attribute, new AttributeValueEventArgs((object) null, attribute.Value, false, attribute.Session));
  }

  private void WriteAttributeValueHandler(IDBAttribute attribute, AttributeValueEventArgs args)
  {
    if (args == null || args.NewValue == args.OldValue || !(attribute is DBAttribute dbAttribute) || !dbAttribute.IsObjectAttribute || !(dbAttribute.ParentObject is DBObject parentObject) || parentObject.IsCreationMode || !MetaDataHelper.IsObjectTypeChildOf(parentObject.ObjectType, Intermech.Imbase.Consts.ImbaseObjFilterTypeID))
      return;
    ObjFilterCacheItem cacheItem = this.GetCacheItem(parentObject.ObjectID, parentObject.Session);
    if (cacheItem == null)
      return;
    string str = Convert.ToString(args.Value);
    if (attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545"))
    {
      this._objCache.RemoveItem(cacheItem);
      cacheItem.Info.RefObjTypeID = !(str != string.Empty) || !GuidHelper.IsGuid(str) ? -1 : MetaDataHelper.GetObjectTypeID(str);
      this._objCache.AddItem(cacheItem);
    }
    else if (attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cad0004b-306c-11d8-b4e9-00304f19f545"))
      cacheItem.Data = (ImbaseObjFilterData) null;
    else if (attribute.AttributeID == Intermech.Imbase.Consts.ImbaseFilterOwnerAttrID)
    {
      if (cacheItem.Info == null)
        return;
      cacheItem.Info.Owner = str;
    }
    else
    {
      if (attribute.AttributeID != -50 && attribute.AttributeID != this._captionFilterAttrId || cacheItem.Info == null)
        return;
      cacheItem.Info.Caption = str;
    }
  }

  public static class Consts
  {
    public static readonly bool DelayedDataLoad = false;
    public static readonly int CacheLifeTime = 10;
  }
}
