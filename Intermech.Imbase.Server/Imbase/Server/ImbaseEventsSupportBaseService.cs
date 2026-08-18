// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseEventsSupportBaseService
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Server;

internal abstract class ImbaseEventsSupportBaseService : LongLifeObject
{
  private Dictionary<Guid, ImEventSession> _eventSessionData = new Dictionary<Guid, ImEventSession>();

  public virtual void ProceedEventSession(ImEventSession eventSession)
  {
    if (eventSession == null || eventSession.EventDataList == null)
      return;
    foreach (ImEventBaseData eventData in eventSession.EventDataList)
    {
      switch (eventData.EventType)
      {
        case ImEventType.ietAttrDelete:
          if (eventData is ImEventAttrData imEventAttrData1)
          {
            this.DoDeleteAttributeValueHandler(imEventAttrData1.Attribute, imEventAttrData1.EventArg as AttributeDeleteEventArgs);
            continue;
          }
          continue;
        case ImEventType.ietAttrModify:
          if (eventData is ImEventAttrData imEventAttrData2)
          {
            this.DoWriteAttributeValueHandler(imEventAttrData2.Attribute, imEventAttrData2.EventArg as AttributeValueEventArgs);
            continue;
          }
          continue;
        case ImEventType.ietObjLCStepBeforeChange:
          if (eventData is ImEventObjLCStepData eventObjLcStepData1)
          {
            this.DoBeforeObjNextLCStepHandler(eventObjLcStepData1.Object, eventObjLcStepData1.LcStep, eventObjLcStepData1.Object.Session);
            continue;
          }
          continue;
        case ImEventType.ietObjLCStepAfterChange:
          if (eventData is ImEventObjLCStepData eventObjLcStepData2)
          {
            this.DoAfterObjNextLCStepHandler(eventObjLcStepData2.Object, eventObjLcStepData2.LcStep, eventObjLcStepData2.Object.Session);
            continue;
          }
          continue;
        case ImEventType.ietRelAfterDelete:
          if (eventData is ImEventRelData imEventRelData)
          {
            this.DoDeleteRelationHandler(imEventRelData.Relation, imEventRelData.Relation.Session);
            continue;
          }
          continue;
        default:
          continue;
      }
    }
  }

  public virtual void WriteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeValueEventArgs args)
  {
    if (attribute == null || args == null || args.OldValue == args.Value)
      return;
    IUserSession session = attribute.Session;
    if (session == null)
      return;
    bool flag = true;
    ImEventSession eventSession = (ImEventSession) null;
    if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
    {
      flag = false;
      eventSession = new ImEventSession(session.SessionGUID);
    }
    eventSession.EventDataList.Add((ImEventBaseData) new ImEventAttrData(attribute, (EventArgs) args, ImEventType.ietAttrModify));
    if (flag)
      return;
    this.ProceedEventSession(eventSession);
  }

  public virtual void DeleteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeDeleteEventArgs args)
  {
    if (attribute == null)
      return;
    IUserSession session = attribute.Session;
    if (session == null)
      return;
    bool flag = true;
    ImEventSession eventSession = (ImEventSession) null;
    if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
    {
      flag = false;
      eventSession = new ImEventSession(session.SessionGUID);
    }
    eventSession.EventDataList.Add((ImEventBaseData) new ImEventAttrData(attribute, (EventArgs) args, ImEventType.ietAttrDelete));
    if (flag)
      return;
    this.ProceedEventSession(eventSession);
  }

  public virtual void BeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null)
      return;
    bool flag = true;
    ImEventSession eventSession = (ImEventSession) null;
    if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
    {
      flag = false;
      eventSession = new ImEventSession(session.SessionGUID);
    }
    eventSession.EventDataList.Add((ImEventBaseData) new ImEventObjLCStepData(sender, nextstep, ImEventType.ietObjLCStepBeforeChange));
    if (flag)
      return;
    this.ProceedEventSession(eventSession);
  }

  public virtual void AfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session)
  {
    if (sender == null || nextstep == null || session == null)
      return;
    bool flag = true;
    ImEventSession eventSession = (ImEventSession) null;
    if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
    {
      flag = false;
      eventSession = new ImEventSession(session.SessionGUID);
    }
    eventSession.EventDataList.Add((ImEventBaseData) new ImEventObjLCStepData(sender, nextstep, ImEventType.ietObjLCStepAfterChange));
    if (flag)
      return;
    this.ProceedEventSession(eventSession);
  }

  public virtual void DeleteRelationHandler(
    IDBRelation sender,
    long deleteMode,
    IUserSession session)
  {
    if (sender == null || session == null)
      return;
    bool flag = true;
    ImEventSession eventSession = (ImEventSession) null;
    if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
    {
      flag = false;
      eventSession = new ImEventSession(session.SessionGUID);
    }
    eventSession.EventDataList.Add((ImEventBaseData) new ImEventRelData(sender, ImEventType.ietRelAfterDelete));
    if (flag)
      return;
    this.ProceedEventSession(eventSession);
  }

  public virtual void StartTransaction(IUserSession session)
  {
  }

  public virtual void CommitTransaction(IUserSession session)
  {
    if (session == null)
      return;
    ImEventSession eventSession = (ImEventSession) null;
    lock (this._eventSessionData)
    {
      if (!this._eventSessionData.TryGetValue(session.SessionGUID, out eventSession))
        return;
      if (eventSession.RefCount > 0)
      {
        --eventSession.RefCount;
      }
      else
      {
        try
        {
          this.ProceedEventSession(eventSession);
        }
        catch (Exception ex)
        {
        }
        finally
        {
          this._eventSessionData.Remove(session.SessionGUID);
        }
      }
    }
  }

  public virtual void RollBackTransaction(IUserSession session)
  {
    if (session == null)
      return;
    lock (this._eventSessionData)
      this._eventSessionData.Remove(session.SessionGUID);
  }

  public virtual void AfterCacheReloadHandler(IDbManager db)
  {
  }

  protected abstract void DoDeleteRelationHandler(IDBRelation sender, IUserSession session);

  protected abstract void DoBeforeObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session);

  protected abstract void DoAfterObjNextLCStepHandler(
    IDBObject sender,
    IDBLifecycleStep nextstep,
    IUserSession session);

  protected abstract void DoWriteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeValueEventArgs args);

  protected abstract void DoDeleteAttributeValueHandler(
    IDBAttribute attribute,
    AttributeDeleteEventArgs args);

  public static int GetObjTypeId(object objectType)
  {
    int result = -1;
    switch (objectType)
    {
      case null:
        return result;
      case int _:
      case long _:
        int.TryParse(objectType.ToString(), out result);
        break;
      default:
        pattern_0 = Guid.Empty;
        switch (objectType)
        {
          case string _:
            string str = objectType.ToString();
            pattern_0 = GuidHelper.IsGuid(str) ? new Guid(str) : Guid.Empty;
            break;
        }
        result = MetaDataHelper.GetObjectTypeID(pattern_0);
        break;
    }
    return result;
  }
}
