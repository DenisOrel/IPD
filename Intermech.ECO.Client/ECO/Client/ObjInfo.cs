// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ObjInfo
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class ObjInfo : ICloneable
{
  public long verId;
  public Guid verGuid = Guid.Empty;
  public string design = "";

  public ObjInfo(Guid aGuid)
  {
    this.verGuid = aGuid;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      PendingLink.InitVars(sessionKeeper.Session.GetObject(this.verGuid, false), ref this.verId, ref this.verGuid, ref this.design);
  }

  public ObjInfo(long vId)
  {
    this.verId = vId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      PendingLink.InitVars(sessionKeeper.Session.GetObject(this.verId, false), ref this.verId, ref this.verGuid, ref this.design);
  }

  public ObjInfo(long vId, IUserSession session)
  {
    this.verId = vId;
    PendingLink.InitVars(session.GetObject(this.verId, false), ref this.verId, ref this.verGuid, ref this.design);
  }

  public ObjInfo(IDBObject idbO)
  {
    PendingLink.InitVars(idbO, ref this.verId, ref this.verGuid, ref this.design);
  }

  public ObjInfo(long vId, Guid vGuid, string des)
  {
    this.verId = vId;
    this.verGuid = vGuid;
    this.design = des;
  }

  public object Clone() => (object) new ObjInfo(this.verId, this.verGuid, this.design);
}
