// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECOLinkDeleter
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.ECO;
using System;

#nullable disable
namespace Intermech.ECO.Client;

public class ECOLinkDeleter : IDisposable
{
  private IECOServer iecos;
  private long _relId;

  public ECOLinkDeleter(IUserSession ius, long relId)
  {
    this.iecos = ius.GetCustomService(typeof (IECOServer)) as IECOServer;
    this._relId = relId;
    this.iecos.StartLinkDeletion(this._relId);
  }

  public void Dispose()
  {
    this.iecos.EndLinkDeletion(this._relId);
    this.iecos = (IECOServer) null;
  }
}
