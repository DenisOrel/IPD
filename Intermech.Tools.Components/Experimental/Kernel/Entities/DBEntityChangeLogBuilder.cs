// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityChangeLogBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityChangeLogBuilder : EntityChangeTrackerLogBuilder
{
  private DBModelConfiguration configuration;
  private List<DBEntityChangeLogBuilder.CreatedDBEntityRecord> createdDBObjects;
  private List<DBEntityChangeLogBuilder.CreatedDBEntityRecord> createdDBRelations;
  private List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> modifiedDBObjects;
  private List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> modifiedDBRelations;
  private List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> modifiedDBObjectRelations;
  private List<DBEntityChangeLogBuilder.RemovedDBEntityRecord> removedDBObjects;
  private List<DBEntityChangeLogBuilder.RemovedDBEntityRecord> removedDBRelations;
  private List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord> unmodifiedDBObjects;
  private List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord> unmodifiedDBRelations;

  public DBEntityChangeLogBuilder(DBModelConfiguration configuration)
  {
    this.configuration = configuration;
    this.createdDBObjects = new List<DBEntityChangeLogBuilder.CreatedDBEntityRecord>();
    this.createdDBRelations = new List<DBEntityChangeLogBuilder.CreatedDBEntityRecord>();
    this.modifiedDBObjects = new List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord>();
    this.modifiedDBRelations = new List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord>();
    this.modifiedDBObjectRelations = new List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord>();
    this.removedDBObjects = new List<DBEntityChangeLogBuilder.RemovedDBEntityRecord>();
    this.removedDBRelations = new List<DBEntityChangeLogBuilder.RemovedDBEntityRecord>();
    this.unmodifiedDBObjects = new List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord>();
    this.unmodifiedDBRelations = new List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord>();
  }

  private DBModelConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.configuration;
  }

  internal override bool CanHandleUnmodifiedEntities
  {
    [DebuggerStepThrough] get => true;
  }

  public List<DBEntityChangeLogBuilder.CreatedDBEntityRecord> CreatedDBObjects
  {
    [DebuggerStepThrough] get => this.createdDBObjects;
  }

  public List<DBEntityChangeLogBuilder.CreatedDBEntityRecord> CreatedDBRelations
  {
    [DebuggerStepThrough] get => this.createdDBRelations;
  }

  public List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> ModifiedDBObjects
  {
    [DebuggerStepThrough] get => this.modifiedDBObjects;
  }

  public List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> ModifiedDBRelations
  {
    [DebuggerStepThrough] get => this.modifiedDBRelations;
  }

  public List<DBEntityChangeLogBuilder.ModifiedDBEntityRecord> ModifiedDBObjectRelations
  {
    [DebuggerStepThrough] get => this.modifiedDBObjectRelations;
  }

  public List<DBEntityChangeLogBuilder.RemovedDBEntityRecord> RemovedDBObjects
  {
    [DebuggerStepThrough] get => this.removedDBObjects;
  }

  public List<DBEntityChangeLogBuilder.RemovedDBEntityRecord> RemovedDBRelations
  {
    [DebuggerStepThrough] get => this.removedDBRelations;
  }

  public List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord> UnmodifiedDBObjects
  {
    [DebuggerStepThrough] get => this.unmodifiedDBObjects;
  }

  public List<DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord> UnmodifiedDBRelations
  {
    [DebuggerStepThrough] get => this.unmodifiedDBRelations;
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get
    {
      return this.createdDBObjects.Count == 0 && this.createdDBRelations.Count == 0 && this.modifiedDBObjects.Count == 0 && this.modifiedDBRelations.Count == 0 && this.modifiedDBObjectRelations.Count == 0 && this.removedDBObjects.Count == 0 && this.removedDBRelations.Count == 0;
    }
  }

  public void Clear()
  {
    this.createdDBObjects.Clear();
    this.createdDBRelations.Clear();
    this.modifiedDBObjects.Clear();
    this.modifiedDBObjectRelations.Clear();
    this.modifiedDBRelations.Clear();
    this.removedDBObjects.Clear();
    this.removedDBRelations.Clear();
    this.unmodifiedDBObjects.Clear();
    this.unmodifiedDBRelations.Clear();
  }

  public override CreatedEntityRecord CreateCreatedEntityRecord(object entity, bool isRootEntity)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(entity);
    return (CreatedEntityRecord) new DBEntityChangeLogBuilder.CreatedDBEntityRecord(entity, isRootEntity, entityTypeDescriptor);
  }

  public override ModifiedEntityRecord CreateModifiedEntityRecord(object entity, bool isRootEntity)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(entity);
    return (ModifiedEntityRecord) new DBEntityChangeLogBuilder.ModifiedDBEntityRecord(entity, isRootEntity, entityTypeDescriptor);
  }

  public override RemovedEntityRecord CreateRemovedEntityRecord(object entity, bool isRootEntity)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(entity);
    return (RemovedEntityRecord) new DBEntityChangeLogBuilder.RemovedDBEntityRecord(entity, isRootEntity, entityTypeDescriptor);
  }

  public override UnmodifiedEntityRecord CreateUnmodifiedEntityRecord(
    object entity,
    bool isRootEntity)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(entity);
    return (UnmodifiedEntityRecord) new DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord(entity, isRootEntity, entityTypeDescriptor);
  }

  protected override void DoAddCreatedEntity(CreatedEntityRecord record)
  {
    DBEntityChangeLogBuilder.CreatedDBEntityRecord createdDbEntityRecord = (DBEntityChangeLogBuilder.CreatedDBEntityRecord) record;
    DBEntityKind entityKind = createdDbEntityRecord.EntityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        this.createdDBObjects.Add(createdDbEntityRecord);
        break;
      case DBEntityKind.Relation:
        this.createdDBRelations.Add(createdDbEntityRecord);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  protected override void DoAddModifiedEntity(ModifiedEntityRecord record)
  {
    DBEntityChangeLogBuilder.ModifiedDBEntityRecord modifiedDbEntityRecord = (DBEntityChangeLogBuilder.ModifiedDBEntityRecord) record;
    DBEntityKind entityKind = modifiedDbEntityRecord.EntityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        if (modifiedDbEntityRecord.ModifiedDataProperties.Count != 0)
          this.modifiedDBObjects.Add(modifiedDbEntityRecord);
        if (modifiedDbEntityRecord.ModifiedNavigationProperties.Count == 0)
          break;
        this.modifiedDBObjectRelations.Add(modifiedDbEntityRecord);
        break;
      case DBEntityKind.Relation:
        if (modifiedDbEntityRecord.ModifiedDataProperties.Count == 0)
          break;
        this.modifiedDBRelations.Add(modifiedDbEntityRecord);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  protected override void DoAddRemovedEntity(RemovedEntityRecord record)
  {
    DBEntityChangeLogBuilder.RemovedDBEntityRecord removedDbEntityRecord = (DBEntityChangeLogBuilder.RemovedDBEntityRecord) record;
    DBEntityKind entityKind = removedDbEntityRecord.EntityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        this.removedDBObjects.Add(removedDbEntityRecord);
        break;
      case DBEntityKind.Relation:
        this.removedDBRelations.Add(removedDbEntityRecord);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  protected override void DoAddUnmodifiedEntity(UnmodifiedEntityRecord record)
  {
    DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord unmodifiedDbEntityRecord = (DBEntityChangeLogBuilder.UnmodifiedDBEntityRecord) record;
    DBEntityKind entityKind = unmodifiedDbEntityRecord.EntityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        this.unmodifiedDBObjects.Add(unmodifiedDbEntityRecord);
        break;
      case DBEntityKind.Relation:
        this.unmodifiedDBRelations.Add(unmodifiedDbEntityRecord);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  public sealed class CreatedDBEntityRecord : CreatedEntityRecord
  {
    internal CreatedDBEntityRecord(
      object entity,
      bool isRootEntity,
      IDBEntityTypeDescriptor entityTypeDescriptor)
      : base(entity, isRootEntity)
    {
      this.EntityTypeDescriptor = entityTypeDescriptor;
    }

    public IDBEntityTypeDescriptor EntityTypeDescriptor { get; private set; }
  }

  public sealed class ModifiedDBEntityRecord : ModifiedEntityRecord
  {
    internal ModifiedDBEntityRecord(
      object entity,
      bool isRootEntity,
      IDBEntityTypeDescriptor entityTypeDescriptor)
      : base(entity, isRootEntity)
    {
      this.EntityTypeDescriptor = entityTypeDescriptor;
    }

    public IDBEntityTypeDescriptor EntityTypeDescriptor { get; private set; }
  }

  public sealed class RemovedDBEntityRecord : RemovedEntityRecord
  {
    internal RemovedDBEntityRecord(
      object entity,
      bool isRootEntity,
      IDBEntityTypeDescriptor entityTypeDescriptor)
      : base(entity, isRootEntity)
    {
      this.EntityTypeDescriptor = entityTypeDescriptor;
    }

    public IDBEntityTypeDescriptor EntityTypeDescriptor { get; private set; }
  }

  public sealed class UnmodifiedDBEntityRecord : UnmodifiedEntityRecord
  {
    internal UnmodifiedDBEntityRecord(
      object entity,
      bool isRootEntity,
      IDBEntityTypeDescriptor entityTypeDescriptor)
      : base(entity, isRootEntity)
    {
      this.EntityTypeDescriptor = entityTypeDescriptor;
    }

    public IDBEntityTypeDescriptor EntityTypeDescriptor { get; private set; }
  }
}
