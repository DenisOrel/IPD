// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.NullEntityChangeTracker
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public sealed class NullEntityChangeTracker : IEntityChangeTracker, IEntityChangeTrackerBase
{
  private static readonly NullEntityChangeTracker defaultInstance = new NullEntityChangeTracker();

  public IEntityChangeTrackerConfiguration Configuration => throw this.BatchUpdateIsNotStarted();

  public void Attach(object entity) => throw this.BatchUpdateIsNotStarted();

  public bool IsAttached(object entity) => throw this.BatchUpdateIsNotStarted();

  public void MarkToRemove(object entity) => throw this.BatchUpdateIsNotStarted();

  public void MarkToRemove(IEnumerable<object> entities) => throw this.BatchUpdateIsNotStarted();

  public ICollection<object> RecycleBin => throw this.BatchUpdateIsNotStarted();

  public List<EntityChangeTrackerLogRecord> GetChangeLog() => throw this.BatchUpdateIsNotStarted();

  public void CaptureChanges(EntityChangeTrackerLogBuilder changeLogBuilder)
  {
    throw this.BatchUpdateIsNotStarted();
  }

  private EntityException BatchUpdateIsNotStarted()
  {
    return new EntityException("Отслеживание изменений в доменных моделях не было активировано.");
  }

  public static NullEntityChangeTracker Default
  {
    [DebuggerStepThrough] get => NullEntityChangeTracker.defaultInstance;
  }
}
