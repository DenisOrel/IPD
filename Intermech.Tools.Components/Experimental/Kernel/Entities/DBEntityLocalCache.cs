// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityLocalCache
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityLocalCache
{
  private Dictionary<DBEntityLocalCacheKey, object> table;

  public DBEntityLocalCache() => this.table = new Dictionary<DBEntityLocalCacheKey, object>();

  public int Count => this.table.Count;

  public void Clear()
  {
    if (this.table.Count == 0)
      return;
    this.table.Clear();
  }

  public object TryGet(DBEntityLocalCacheKey key)
  {
    object obj;
    return this.table.TryGetValue(key, out obj) ? obj : (object) null;
  }

  public void AddOrUpdate(object entity, DBEntityLocalCacheKey key)
  {
    this.table[key] = entity != null ? entity : throw new ArgumentNullException(nameof (entity));
  }

  public void Remove(object entity, DBEntityLocalCacheKey key)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.table.Remove(key);
  }

  public List<object> GetEntities() => new List<object>((IEnumerable<object>) this.table.Values);
}
