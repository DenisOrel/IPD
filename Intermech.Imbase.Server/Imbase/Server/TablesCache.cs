// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TablesCache
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TablesCache : LongLifeObject, ITablesCache
{
  private Dictionary<long, DataSet> _cache = new Dictionary<long, DataSet>(32 /*0x20*/);
  internal static ITablesCache _instance;
  private bool _enabled;

  public TablesCache()
  {
    this._enabled = false;
    TablesCache._instance = (ITablesCache) this;
  }

  public DataSet Load(IUserSession session, long tableId)
  {
    if (this._cache.ContainsKey(tableId))
      return this._cache[tableId];
    DataSet tablesInternal = TableLoadHelper.GetTablesInternal(session, tableId);
    if (this._enabled)
    {
      lock (this._cache)
        this._cache[tableId] = tablesInternal;
    }
    return tablesInternal;
  }

  public void Remove(long tableId)
  {
    lock (this._cache)
    {
      if (!this._cache.ContainsKey(tableId))
        return;
      this._cache.Remove(tableId);
    }
  }

  public void Clear()
  {
    lock (this._cache)
      this._cache.Clear();
  }

  public DataSet Load(Guid sessionGuid, long tableId)
  {
    return this.Load(ImbaseServer.GetSession(sessionGuid), tableId);
  }

  public bool Enabled
  {
    get => this._enabled;
    set => this._enabled = value;
  }
}
