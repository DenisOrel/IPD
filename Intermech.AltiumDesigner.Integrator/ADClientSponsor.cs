// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADClientSponsor
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using System;
using System.Runtime.Remoting.Lifetime;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADClientSponsor : IDisposable
{
  private MarshalByRefObject _sponsoredObject;
  private readonly ClientSponsor _sponsor;

  public ADClientSponsor()
  {
    this._sponsor = new ClientSponsor();
    this._sponsor.RenewalTime = TimeSpan.FromMinutes(2.0);
  }

  public void Register(object sponsoredObject)
  {
    this._sponsoredObject = sponsoredObject is MarshalByRefObject ? (MarshalByRefObject) sponsoredObject : throw new ArgumentException("sponsoredObject is not MarshalByRefObject!");
    this._sponsor.Register(this._sponsoredObject);
  }

  public void Dispose()
  {
    try
    {
      this._sponsor.Unregister(this._sponsoredObject);
      this._sponsor.Close();
    }
    catch
    {
    }
  }
}
