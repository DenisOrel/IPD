// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityChangeTrackerLogBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities;

public abstract class EntityChangeTrackerLogBuilder
{
  internal abstract bool CanHandleUnmodifiedEntities { get; }

  public void Add(CreatedEntityRecord record)
  {
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    this.DoAddCreatedEntity(record);
  }

  public void Add(ModifiedEntityRecord record)
  {
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    this.DoAddModifiedEntity(record);
  }

  public void Add(RemovedEntityRecord record)
  {
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    this.DoAddRemovedEntity(record);
  }

  public void Add(UnmodifiedEntityRecord record)
  {
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    if (!this.CanHandleUnmodifiedEntities)
      return;
    this.DoAddUnmodifiedEntity(record);
  }

  public virtual CreatedEntityRecord CreateCreatedEntityRecord(object entity, bool isRootEntity)
  {
    return new CreatedEntityRecord(entity, isRootEntity);
  }

  public virtual ModifiedEntityRecord CreateModifiedEntityRecord(object entity, bool isRootEntity)
  {
    return new ModifiedEntityRecord(entity, isRootEntity);
  }

  public virtual RemovedEntityRecord CreateRemovedEntityRecord(object entity, bool isRootEntity)
  {
    return new RemovedEntityRecord(entity, isRootEntity);
  }

  public virtual UnmodifiedEntityRecord CreateUnmodifiedEntityRecord(
    object entity,
    bool isRootEntity)
  {
    return new UnmodifiedEntityRecord(entity, isRootEntity);
  }

  public virtual NavigationPropertyModification CreateNavigationPropertyModification(
    string propertyName,
    NavigationPropertyModificationType itemState,
    object itemEntity)
  {
    return new NavigationPropertyModification(itemState, itemEntity);
  }

  protected abstract void DoAddCreatedEntity(CreatedEntityRecord record);

  protected abstract void DoAddModifiedEntity(ModifiedEntityRecord record);

  protected abstract void DoAddRemovedEntity(RemovedEntityRecord record);

  protected abstract void DoAddUnmodifiedEntity(UnmodifiedEntityRecord record);
}
