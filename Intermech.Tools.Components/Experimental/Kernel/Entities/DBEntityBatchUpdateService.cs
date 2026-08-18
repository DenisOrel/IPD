// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityBatchUpdateService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Interfaces;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityBatchUpdateService : IEntityBatchUpdateService
{
  private DBModelConfiguration configuration;
  private DBObjectCheckoutManager checkoutManager;
  private InternalDataService internalDataService;
  private DBEntityChangeLogBuilder changeLogBuilder;
  private IEntityBatchUpdateLog updateLog;
  private ICollection<object> removedDBObjects;
  private ICollection<object> checkedOutDBObjects;

  public DBEntityBatchUpdateService(InternalDataService internalDataService)
  {
    this.configuration = internalDataService != null ? internalDataService.Configuration : throw new ArgumentNullException(nameof (internalDataService));
    this.checkoutManager = new DBObjectCheckoutManager(internalDataService.Configuration, internalDataService.EntityLocalCache);
    this.internalDataService = internalDataService;
  }

  private DBModelConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.configuration;
  }

  private DBObjectCheckoutManager CheckoutManager
  {
    [DebuggerStepThrough] get => this.checkoutManager;
  }

  private InternalDataService InternalDataService
  {
    [DebuggerStepThrough] get => this.internalDataService;
  }

  public void SaveChanges(IEntityChangeTrackerBase changeTracker, IEntityBatchUpdateLog log = null)
  {
    if (changeTracker == null)
      throw new ArgumentNullException(nameof (changeTracker));
    try
    {
      this.updateLog = log != null ? log : (IEntityBatchUpdateLog) new NullEntityBatchUpdateLog();
      this.changeLogBuilder = new DBEntityChangeLogBuilder(this.Configuration);
      changeTracker.CaptureChanges((EntityChangeTrackerLogBuilder) this.changeLogBuilder);
      if (this.changeLogBuilder.IsEmpty)
        return;
      this.checkedOutDBObjects = this.CheckoutManager.FindDBObjects(this.changeLogBuilder);
      if (this.checkedOutDBObjects.Count != 0)
      {
        this.CheckoutManager.CheckoutDBObjects(this.checkedOutDBObjects, this.updateLog as IDBEntityBatchUpdateLog);
        this.changeLogBuilder.Clear();
        changeTracker.CaptureChanges((EntityChangeTrackerLogBuilder) this.changeLogBuilder);
      }
      this.removedDBObjects = (ICollection<object>) new HashSet<object>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBTransactions service = ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, true);
        service.StartTransaction();
        try
        {
          this.RemoveDBObjects();
          this.RemoveDBRelations();
          this.CreateDBObjects();
          this.UpdateDBObjectsAttributes();
          this.UpdateDBRelationsAttributes();
          this.CreateDBRelations();
          this.CommitDBObjects();
          service.Commit();
        }
        catch
        {
          SilentActionInvoker.Default.Invoke(new Action(this.updateLog.Clear));
          service.Rollback();
          throw;
        }
      }
    }
    finally
    {
      if (this.checkedOutDBObjects != null)
        this.checkedOutDBObjects = (ICollection<object>) null;
      if (this.removedDBObjects != null)
        this.removedDBObjects = (ICollection<object>) null;
      if (this.changeLogBuilder != null)
        this.changeLogBuilder = (DBEntityChangeLogBuilder) null;
      if (this.updateLog != null)
        this.updateLog = (IEntityBatchUpdateLog) null;
    }
  }

  private void RemoveDBObjects()
  {
    foreach (DBEntityChangeLogBuilder.RemovedDBEntityRecord removedDbObject in this.changeLogBuilder.RemovedDBObjects)
    {
      object entity = removedDbObject.Entity;
      IDBObjectEntityTypeDescriptor dbObjectDescriptor = removedDbObject.EntityTypeDescriptor.AsDBObjectDescriptor();
      this.InternalDataService.RemoveDBObject(entity, dbObjectDescriptor);
      this.removedDBObjects.Add(entity);
      this.updateLog.RemoveEntity(entity);
      this.AddRemovedDBRelationsToUpdateLog((RemovedEntityRecord) removedDbObject, entity);
    }
  }

  private void AddRemovedDBRelationsToUpdateLog(
    RemovedEntityRecord changeLogRecord,
    object parentEntity)
  {
    foreach (ModifiedNavigationPropertyRecord navigationProperty in changeLogRecord.ModifiedNavigationProperties)
    {
      foreach (NavigationPropertyModification modification in navigationProperty.Modifications)
      {
        if (modification.ModificationType == NavigationPropertyModificationType.Removed)
        {
          IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(modification.PropertyValue);
          DBEntityKind entityKind = entityTypeDescriptor.EntityKind;
          switch (entityKind)
          {
            case DBEntityKind.Object:
              object propertyValue1 = modification.PropertyValue;
              this.updateLog.RemoveChildEntity(new EntityRelationQuickInfo(parentEntity, navigationProperty.PropertyName, propertyValue1));
              continue;
            case DBEntityKind.Relation:
              object propertyValue2 = modification.PropertyValue;
              object relationEnd = entityTypeDescriptor.AsDBRelationDescriptor().GetRelationEnd(propertyValue2);
              this.updateLog.RemoveChildEntity(new EntityRelationQuickInfo(parentEntity, navigationProperty.PropertyName, relationEnd, propertyValue2));
              continue;
            default:
              throw new NotSupportedEnumException((Enum) entityKind);
          }
        }
      }
    }
  }

  private void RemoveDBRelations()
  {
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord dbObjectRelation in this.changeLogBuilder.ModifiedDBObjectRelations)
    {
      object entity = dbObjectRelation.Entity;
      IDBObjectEntityTypeDescriptor parentDescriptor = dbObjectRelation.EntityTypeDescriptor.AsDBObjectDescriptor();
      foreach (ModifiedNavigationPropertyRecord navigationProperty in dbObjectRelation.ModifiedNavigationProperties)
      {
        foreach (NavigationPropertyModification modification in navigationProperty.Modifications)
        {
          if (modification.ModificationType == NavigationPropertyModificationType.Removed)
            this.RemoveDBRelation(entity, parentDescriptor, navigationProperty, modification);
        }
      }
    }
  }

  private void RemoveDBRelation(
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    ModifiedNavigationPropertyRecord collectionRecord,
    NavigationPropertyModification collectionModification)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(collectionModification.PropertyValue);
    DBEntityKind entityKind = entityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        object propertyValue1 = collectionModification.PropertyValue;
        if (!this.removedDBObjects.Contains(propertyValue1))
          this.InternalDataService.RemoveSimpleDBRelation(parentEntity, collectionRecord.PropertyName, propertyValue1);
        this.updateLog.RemoveChildEntity(new EntityRelationQuickInfo(parentEntity, collectionRecord.PropertyName, propertyValue1));
        break;
      case DBEntityKind.Relation:
        object propertyValue2 = collectionModification.PropertyValue;
        IDBRelationEntityTypeDescriptor relationDescriptor = entityTypeDescriptor.AsDBRelationDescriptor();
        object relationEnd = relationDescriptor.GetRelationEnd(propertyValue2);
        if (!this.removedDBObjects.Contains(relationEnd))
        {
          if (this.checkedOutDBObjects.Contains(parentEntity))
            this.InternalDataService.UpdateComplexDBRelationKey(parentEntity, parentDescriptor, collectionRecord.PropertyName, relationEnd, propertyValue2, relationDescriptor);
          this.InternalDataService.RemoveComplexDBRelation(parentEntity, collectionRecord.PropertyName, relationEnd, propertyValue2, relationDescriptor);
        }
        this.updateLog.RemoveChildEntity(new EntityRelationQuickInfo(parentEntity, collectionRecord.PropertyName, relationEnd, propertyValue2));
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  private void CreateDBObjects()
  {
    foreach (DBEntityChangeLogBuilder.CreatedDBEntityRecord createdDbObject in this.changeLogBuilder.CreatedDBObjects)
      this.InternalDataService.CreateBlankDBObject(createdDbObject.Entity, createdDbObject.EntityTypeDescriptor.AsDBObjectDescriptor());
  }

  private void UpdateDBObjectsAttributes()
  {
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord modifiedDbObject in this.changeLogBuilder.ModifiedDBObjects)
    {
      object entity = modifiedDbObject.Entity;
      IDBObjectEntityTypeDescriptor dbObjectDescriptor = modifiedDbObject.EntityTypeDescriptor.AsDBObjectDescriptor();
      this.InternalDataService.UpdateDBObjectAttributes(entity, dbObjectDescriptor, (ICollection<string>) modifiedDbObject.ModifiedDataProperties);
      this.updateLog.UpdateEntity(entity);
    }
  }

  private void UpdateDBRelationsAttributes()
  {
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord modifiedDbRelation in this.changeLogBuilder.ModifiedDBRelations)
    {
      object entity = modifiedDbRelation.Entity;
      IDBRelationEntityTypeDescriptor relationDescriptor = modifiedDbRelation.EntityTypeDescriptor.AsDBRelationDescriptor();
      ParentEntityPropertyInfo entityPropertyInfo = modifiedDbRelation.ReferencedBy[0];
      IDBObjectEntityTypeDescriptor parentDescriptor = this.Configuration.GetEntityTypeDescriptor(entityPropertyInfo.Entity).AsDBObjectDescriptor();
      object relationEnd = relationDescriptor.GetRelationEnd(modifiedDbRelation.Entity);
      this.InternalDataService.UpdateDBRelationAttributes(entityPropertyInfo.Entity, parentDescriptor, entityPropertyInfo.PropertyName, entity, relationDescriptor, modifiedDbRelation.ModifiedDataProperties);
      this.updateLog.UpdateChildEntityOccurence(new EntityRelationQuickInfo(entityPropertyInfo.Entity, entityPropertyInfo.PropertyName, relationEnd, entity));
    }
  }

  private void CreateDBRelations()
  {
    foreach (DBEntityChangeLogBuilder.CreatedDBEntityRecord createdDbObject in this.changeLogBuilder.CreatedDBObjects)
    {
      if (createdDbObject.ModifiedNavigationProperties.Count != 0)
      {
        object entity = createdDbObject.Entity;
        IDBObjectEntityTypeDescriptor parentDescriptor = createdDbObject.EntityTypeDescriptor.AsDBObjectDescriptor();
        this.CreateDBRelations((EntityChangeTrackerLogRecord) createdDbObject, entity, parentDescriptor);
      }
    }
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord dbObjectRelation in this.changeLogBuilder.ModifiedDBObjectRelations)
    {
      object entity = dbObjectRelation.Entity;
      IDBObjectEntityTypeDescriptor parentDescriptor = dbObjectRelation.EntityTypeDescriptor.AsDBObjectDescriptor();
      this.CreateDBRelations((EntityChangeTrackerLogRecord) dbObjectRelation, entity, parentDescriptor);
    }
  }

  private void CreateDBRelations(
    EntityChangeTrackerLogRecord changeLogRecord,
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor)
  {
    foreach (ModifiedNavigationPropertyRecord navigationProperty in changeLogRecord.ModifiedNavigationProperties)
    {
      foreach (NavigationPropertyModification modification in navigationProperty.Modifications)
      {
        if (modification.ModificationType == NavigationPropertyModificationType.Added)
          this.CreateDBRelation(parentEntity, parentDescriptor, navigationProperty, modification);
      }
    }
  }

  private void CreateDBRelation(
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    ModifiedNavigationPropertyRecord collectionRecord,
    NavigationPropertyModification collectionModification)
  {
    IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(collectionModification.PropertyValue);
    DBEntityKind entityKind = entityTypeDescriptor.EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        object propertyValue1 = collectionModification.PropertyValue;
        this.InternalDataService.CreateSimpleDBRelation(parentEntity, collectionRecord.PropertyName, propertyValue1);
        this.updateLog.AddChildEntity(new EntityRelationQuickInfo(parentEntity, collectionRecord.PropertyName, propertyValue1));
        break;
      case DBEntityKind.Relation:
        object propertyValue2 = collectionModification.PropertyValue;
        object relationEnd = entityTypeDescriptor.AsDBRelationDescriptor().GetRelationEnd(propertyValue2);
        this.InternalDataService.CreateComplexDBRelation(parentEntity, collectionRecord.PropertyName, propertyValue2);
        this.updateLog.AddChildEntity(new EntityRelationQuickInfo(parentEntity, collectionRecord.PropertyName, relationEnd, propertyValue2));
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  private void CommitDBObjects()
  {
    foreach (DBEntityChangeLogBuilder.CreatedDBEntityRecord createdDbObject in this.changeLogBuilder.CreatedDBObjects)
    {
      object entity = createdDbObject.Entity;
      IDBObjectEntityTypeDescriptor dbObjectDescriptor = createdDbObject.EntityTypeDescriptor.AsDBObjectDescriptor();
      this.InternalDataService.CommitBlankDBObject(entity, dbObjectDescriptor);
      this.updateLog.CreateEntity(entity);
    }
  }
}
