// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMObject
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Tools.Data;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.PDMTree;

public abstract class PDMObject : PDMSystemComponent, IDBObjectRef
{
  private long objectId;
  private int objectType;
  private long swarmId;
  private string captionCache;
  private string persistentIdCache;

  protected PDMObject(long objectId, PDMSystem pdmSystem)
    : base(pdmSystem)
  {
    this.objectId = !Consts.IsUndefinedObjectId(objectId) ? objectId : throw new ArgumentException("Не задан идентификатор версии объекта IPS.", nameof (objectId));
    this.objectType = -1;
    this.swarmId = 0L;
    this.captionCache = (string) null;
  }

  public virtual string GetPersistentID()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine($"{this.TraceTypeName}.GetPersistentID");
    this.PDMSystem.PrepareCall();
    try
    {
      if (this.persistentIdCache == null)
        this.persistentIdCache = this.ConvertToPersistentID(this.ObjectId);
      return this.persistentIdCache;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private string ConvertToPersistentID(long objectId)
  {
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(objectId);
    return PersistentIds.FromObjectVersion(objectInfo.VersionGuid);
  }

  long IDBObjectRef.GetObjectId() => this.objectId;

  internal long ObjectId
  {
    get => this.objectId;
    set
    {
      if (this.objectId == value)
        return;
      this.objectId = value;
      this.OnObjectIdChanged();
    }
  }

  protected virtual void OnObjectIdChanged()
  {
    this.captionCache = (string) null;
    if (this.ObjectIdChanged == null)
      return;
    this.ObjectIdChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler ObjectIdChanged;

  internal int ObjectType
  {
    get
    {
      if (this.objectType == -1)
        this.objectType = DBHelper.GetObjectType(this.objectId);
      return this.objectType;
    }
  }

  internal long ID
  {
    get
    {
      if (this.swarmId == 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.ObjectId);
          this.swarmId = !objectInfo.Empty ? objectInfo.ID : throw new ObjectNotFoundException(this.ObjectId);
        }
      }
      return this.swarmId;
    }
  }

  internal string Caption
  {
    get
    {
      if (this.captionCache == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this.ObjectId);
          this.captionCache = !objectInfo.Empty ? objectInfo.Caption : throw new ObjectNotFoundException(this.ObjectId);
        }
      }
      return this.captionCache;
    }
  }

  internal long GetBaseVersionId()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectBaseVersionByID(this.ID, true).ObjectID;
  }
}
