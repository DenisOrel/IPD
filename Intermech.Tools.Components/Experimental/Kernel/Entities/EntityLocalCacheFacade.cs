// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.EntityLocalCacheFacade
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class EntityLocalCacheFacade : IEntityLocalCache
{
  private DBEntityLocalCache internalCache;
  private DBModelConfiguration configuration;

  public EntityLocalCacheFacade(
    DBEntityLocalCache internalCache,
    DBModelConfiguration configuration)
  {
    if (internalCache == null)
      throw new ArgumentNullException(nameof (internalCache));
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    this.internalCache = internalCache;
    this.configuration = configuration;
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.internalCache.Count == 0;
  }

  public void Clear() => this.internalCache.Clear();

  public bool Contains(object entity)
  {
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = entity != null ? this.configuration.GetEntityTypeDescriptor(entity.GetType()).AsDBObjectDescriptor() : throw new ArgumentNullException(nameof (entity));
    long key = entityTypeDescriptor.GetKey(entity);
    return this.internalCache.TryGet(new DBEntityLocalCacheKey(entityTypeDescriptor.EntityType, (object) key)) != null;
  }
}
