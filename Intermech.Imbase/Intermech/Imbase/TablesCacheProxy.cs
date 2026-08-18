// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TablesCacheProxy
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase;

internal class TablesCacheProxy : ITablesCache
{
  private ITablesCache _proxy;

  private ITablesCache Proxy
  {
    get
    {
      if (this._proxy == null)
      {
        this._proxy = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ITablesCache)) as ITablesCache;
        if (this._proxy == null)
          return (ITablesCache) null;
      }
      try
      {
        int num = this._proxy.Enabled ? 1 : 0;
      }
      catch
      {
        this._proxy = (ITablesCache) null;
        return this.Proxy;
      }
      return this._proxy;
    }
  }

  public DataSet Load(IUserSession session, long tableId)
  {
    return this.Proxy.Load(session.SessionGUID, tableId);
  }

  public void Remove(long tableId) => this.Proxy.Remove(tableId);

  public void Clear() => this.Proxy.Clear();

  public DataSet Load(Guid session, long tableId) => this.Proxy.Load(session, tableId);

  public bool Enabled
  {
    get => this.Proxy.Enabled;
    set => this.Proxy.Enabled = value;
  }
}
