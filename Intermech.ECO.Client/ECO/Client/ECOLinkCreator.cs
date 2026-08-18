// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOLinkCreator
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.ECO;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOLinkCreator : IDisposable
{
  private IECOServer iecos;
  private long _rootId;
  private long _childId;

  public ECOLinkCreator(IUserSession ius, long rootId, long childId)
  {
    this.iecos = ius.GetCustomService(typeof (IECOServer)) as IECOServer;
    this._rootId = rootId;
    this._childId = childId;
    this.iecos.StartLinkCreation(this._rootId, this._childId);
  }

  public void Dispose()
  {
    this.iecos.EndLinkCreation(this._rootId, this._childId);
    this.iecos = (IECOServer) null;
  }
}
