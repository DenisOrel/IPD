// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.SimpleLogBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public class SimpleLogBuilder : EntityChangeTrackerLogBuilder
{
  private List<EntityChangeTrackerLogRecord> records;

  public SimpleLogBuilder() => this.records = new List<EntityChangeTrackerLogRecord>();

  internal bool AllowUnchangedEntities { get; set; }

  internal override bool CanHandleUnmodifiedEntities
  {
    [DebuggerStepThrough] get => this.AllowUnchangedEntities;
  }

  protected override void DoAddCreatedEntity(CreatedEntityRecord record)
  {
    this.records.Add((EntityChangeTrackerLogRecord) record);
  }

  protected override void DoAddModifiedEntity(ModifiedEntityRecord record)
  {
    this.records.Add((EntityChangeTrackerLogRecord) record);
  }

  protected override void DoAddRemovedEntity(RemovedEntityRecord record)
  {
    this.records.Add((EntityChangeTrackerLogRecord) record);
  }

  protected override void DoAddUnmodifiedEntity(UnmodifiedEntityRecord record)
  {
    this.records.Add((EntityChangeTrackerLogRecord) record);
  }

  public List<EntityChangeTrackerLogRecord> ToChangeLog()
  {
    List<EntityChangeTrackerLogRecord> changeLog = new List<EntityChangeTrackerLogRecord>((IEnumerable<EntityChangeTrackerLogRecord>) this.records);
    this.records.Clear();
    return changeLog;
  }
}
