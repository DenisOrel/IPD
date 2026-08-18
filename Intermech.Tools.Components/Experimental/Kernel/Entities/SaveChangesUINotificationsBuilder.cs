// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.SaveChangesUINotificationsBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Kernel.Entities;

public sealed class SaveChangesUINotificationsBuilder : 
  IDBEntityBatchUpdateLog,
  IEntityBatchUpdateLog
{
  private DBModelConfiguration modelConfiguration;
  private UINotificationsBuilder uiBuilder;

  public SaveChangesUINotificationsBuilder(IModelRoot modelRoot)
  {
    this.modelConfiguration = modelRoot != null ? modelRoot.GetModelConfiguration() : throw new ArgumentNullException(nameof (modelRoot));
    this.uiBuilder = new UINotificationsBuilder();
  }

  public List<NotificationEventArgs> ToNotificationList() => this.uiBuilder.ToNotificationList();

  public void CheckoutEntity(object entity, object oldEntityKey)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.uiBuilder.AddCheckedOutObject((IDBObjectRef) new DirectDBObjectRef(this.modelConfiguration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor().GetKey(entity)));
  }

  public void Clear() => this.uiBuilder.Clear();

  public void CreateEntity(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.uiBuilder.AddCreatedObject((IDBObjectRef) new DirectDBObjectRef(this.modelConfiguration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor().GetKey(entity)));
  }

  public void UpdateEntity(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.uiBuilder.AddModifiedObject((IDBObjectRef) new DirectDBObjectRef(this.modelConfiguration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor().GetKey(entity)));
  }

  public void RemoveEntity(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.uiBuilder.AddRemovedObject((IDBObjectRef) new DirectDBObjectRef(this.modelConfiguration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor().GetKey(entity)));
  }

  public void AddChildEntity(EntityRelationQuickInfo entityRelation)
  {
    if (entityRelation == null)
      throw new ArgumentNullException(nameof (entityRelation));
    if (entityRelation.IsComplex)
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor1 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key1 = entityTypeDescriptor1.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor1.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      IDBRelationEntityTypeDescriptor entityTypeDescriptor2 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildOccurence).AsDBRelationDescriptor();
      long key2 = entityTypeDescriptor2.GetKey(entityRelation.ChildOccurence);
      this.uiBuilder.AddCreatedRelation((IDBRelationRef) new DirectDBRelationRef(entityTypeDescriptor2.GetGuid(entityRelation.ChildOccurence), key2, key1, id));
    }
    else
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key3 = entityTypeDescriptor.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildEntity).AsDBObjectDescriptor();
      long key4 = entityTypeDescriptor.GetKey(entityRelation.ChildEntity);
      this.uiBuilder.AddCreatedRelation((IDBRelationRef) new ProjectPartDBRelationRef(key3, key4, id));
    }
  }

  public void UpdateChildEntityOccurence(EntityRelationQuickInfo entityRelation)
  {
    if (entityRelation == null)
      throw new ArgumentNullException(nameof (entityRelation));
    if (entityRelation.IsComplex)
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor1 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key1 = entityTypeDescriptor1.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor1.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      IDBRelationEntityTypeDescriptor entityTypeDescriptor2 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildOccurence).AsDBRelationDescriptor();
      long key2 = entityTypeDescriptor2.GetKey(entityRelation.ChildOccurence);
      this.uiBuilder.AddModifiedRelation((IDBRelationRef) new DirectDBRelationRef(entityTypeDescriptor2.GetGuid(entityRelation.ChildOccurence), key2, key1, id));
    }
    else
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key3 = entityTypeDescriptor.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildEntity).AsDBObjectDescriptor();
      long key4 = entityTypeDescriptor.GetKey(entityRelation.ChildEntity);
      this.uiBuilder.AddModifiedRelation((IDBRelationRef) new ProjectPartDBRelationRef(key3, key4, id));
    }
  }

  public void RemoveChildEntity(EntityRelationQuickInfo entityRelation)
  {
    if (entityRelation == null)
      throw new ArgumentNullException(nameof (entityRelation));
    if (entityRelation.IsComplex)
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor1 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key1 = entityTypeDescriptor1.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor1.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      IDBRelationEntityTypeDescriptor entityTypeDescriptor2 = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildOccurence).AsDBRelationDescriptor();
      long key2 = entityTypeDescriptor2.GetKey(entityRelation.ChildOccurence);
      this.uiBuilder.AddRemovedRelation((IDBRelationRef) new DirectDBRelationRef(entityTypeDescriptor2.GetGuid(entityRelation.ChildOccurence), key2, key1, id));
    }
    else
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ParentEntity).AsDBObjectDescriptor();
      long key3 = entityTypeDescriptor.GetKey(entityRelation.ParentEntity);
      int id = entityTypeDescriptor.NavigationPropertiesMappings.GetByPropertyName(entityRelation.PropertyName, true).DBRelationType.Id;
      this.modelConfiguration.GetEntityTypeDescriptor(entityRelation.ChildEntity).AsDBObjectDescriptor();
      long key4 = entityTypeDescriptor.GetKey(entityRelation.ChildEntity);
      this.uiBuilder.AddRemovedRelation((IDBRelationRef) new ProjectPartDBRelationRef(key3, key4, id));
    }
  }
}
