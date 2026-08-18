// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityDataService`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Kernel.Entities;

internal class DBEntityDataService<TEntity> : IEntityDataService<TEntity> where TEntity : class
{
  private InternalDataService internalDataService;
  private Type dataServiceEntityType;
  private DBEntityReadWriteController readWriteController;

  public DBEntityDataService(
    InternalDataService internalDataService,
    DBEntityReadWriteController readWriteController)
  {
    if (internalDataService == null)
      throw new ArgumentNullException(nameof (internalDataService));
    if (readWriteController == null)
      throw new ArgumentNullException(nameof (readWriteController));
    this.internalDataService = internalDataService;
    this.dataServiceEntityType = typeof (TEntity);
    this.readWriteController = readWriteController;
  }

  private InternalDataService InternalDataService
  {
    [DebuggerStepThrough] get => this.internalDataService;
  }

  private DBEntityReadWriteController ReadWriteController
  {
    [DebuggerStepThrough] get => this.readWriteController;
  }

  public TEntity Load(object key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    this.ReadWriteController.CheckReadingIsAllowed();
    if (!(key is long entityKey))
      throw new EntityValidationException($"Ключ доменного объекта IPS '{this.dataServiceEntityType}' должен быть типа {typeof (long)}.");
    return (TEntity) this.InternalDataService.Load(entityKey, this.InternalDataService.Configuration.GetEntityTypeDescriptor(this.dataServiceEntityType).AsDBObjectDescriptor());
  }

  public List<TEntity> LoadAll()
  {
    this.ReadWriteController.CheckReadingIsAllowed();
    return this.InternalDataService.LoadAll<TEntity>();
  }

  public List<TEntity> LoadAll(Expression<Func<TEntity, bool>> condition)
  {
    if (condition == null)
      throw new ArgumentNullException(nameof (condition));
    this.ReadWriteController.CheckReadingIsAllowed();
    return this.InternalDataService.LoadAll<TEntity>(new InternalConditionCompiler<TEntity>(this.InternalDataService.Configuration.GetEntityTypeDescriptor(typeof (TEntity)).AsDBObjectDescriptor()).Compile(condition));
  }

  public void LoadReferences(TEntity entity, string propertyName)
  {
    this.ReadWriteController.CheckReadingIsAllowed();
    this.InternalDataService.LoadReferences((object) entity, propertyName);
  }

  public void LoadReferences<TProperty>(
    TEntity entity,
    Expression<Func<TEntity, TProperty>> propertySelector)
  {
    this.ReadWriteController.CheckReadingIsAllowed();
    this.InternalDataService.LoadReferences((object) entity, NameOf.PropertyName<TEntity, TProperty>(propertySelector));
  }

  public void CreateEntity(TEntity entity)
  {
    if ((object) entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBObjectEntityTypeDescriptor dbObjectDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor((object) entity).AsDBObjectDescriptor();
    this.InternalDataService.CreateBlankDBObject((object) entity, dbObjectDescriptor);
    this.InternalDataService.CommitBlankDBObject((object) entity, dbObjectDescriptor);
  }

  public bool UpdateEntity(TEntity entity, ICollection<string> modifiedProperties)
  {
    if ((object) entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (modifiedProperties == null)
      throw new ArgumentNullException(nameof (modifiedProperties));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBObjectEntityTypeDescriptor dbObjectDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor((object) entity).AsDBObjectDescriptor();
    return this.InternalDataService.UpdateDBObjectAttributes((object) entity, dbObjectDescriptor, modifiedProperties);
  }

  public void RemoveEntity(TEntity entity)
  {
    if ((object) entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBObjectEntityTypeDescriptor dbObjectDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor((object) entity).AsDBObjectDescriptor();
    this.InternalDataService.RemoveDBObject((object) entity, dbObjectDescriptor);
  }

  public void AddChildEntity(
    TEntity parentEntity,
    string propertyName,
    object childEntityOrOccurence)
  {
    if ((object) parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childEntityOrOccurence == null)
      throw new ArgumentNullException(nameof (childEntityOrOccurence));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBEntityTypeDescriptor entityTypeDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor(childEntityOrOccurence);
    switch (entityTypeDescriptor.EntityKind)
    {
      case DBEntityKind.Object:
        this.InternalDataService.CreateSimpleDBRelation((object) parentEntity, propertyName, childEntityOrOccurence);
        break;
      case DBEntityKind.Relation:
        this.InternalDataService.CreateComplexDBRelation((object) parentEntity, propertyName, childEntityOrOccurence);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeDescriptor.EntityKind);
    }
  }

  public void AddChildEntity<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childEntityOrOccurence)
  {
    if (propertySelector == null)
      throw new ArgumentNullException(nameof (propertySelector));
    this.AddChildEntity(parentEntity, NameOf.PropertyName<TEntity, TProperty>(propertySelector), childEntityOrOccurence);
  }

  public void UpdateChildEntityOccurence(
    TEntity parentEntity,
    string propertyName,
    object childOccurence,
    List<string> modifiedProperties)
  {
    if ((object) parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childOccurence == null)
      throw new ArgumentNullException(nameof (childOccurence));
    if (modifiedProperties == null)
      throw new ArgumentNullException(nameof (modifiedProperties));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBObjectEntityTypeDescriptor parentDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor((object) parentEntity).AsDBObjectDescriptor();
    IDBRelationEntityTypeDescriptor relationDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor(childOccurence).AsDBRelationDescriptor();
    this.InternalDataService.UpdateDBRelationAttributes((object) parentEntity, parentDescriptor, propertyName, childOccurence, relationDescriptor, modifiedProperties);
  }

  public void UpdateChildEntityOccurence<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childOccurence,
    List<string> modifiedProperties)
  {
    if (propertySelector == null)
      throw new ArgumentNullException(nameof (propertySelector));
    this.UpdateChildEntityOccurence(parentEntity, NameOf.PropertyName<TEntity, TProperty>(propertySelector), childOccurence, modifiedProperties);
  }

  public void RemoveChildEntity(
    TEntity parentEntity,
    string propertyName,
    object childEntityOrOccurence)
  {
    if ((object) parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childEntityOrOccurence == null)
      throw new ArgumentNullException(nameof (childEntityOrOccurence));
    this.ReadWriteController.CheckWritingIsAllowed();
    IDBEntityTypeDescriptor entityTypeDescriptor = this.InternalDataService.Configuration.GetEntityTypeDescriptor(childEntityOrOccurence);
    switch (entityTypeDescriptor.EntityKind)
    {
      case DBEntityKind.Object:
        this.InternalDataService.RemoveSimpleDBRelation((object) parentEntity, propertyName, childEntityOrOccurence);
        break;
      case DBEntityKind.Relation:
        IDBRelationEntityTypeDescriptor relationDescriptor = entityTypeDescriptor.AsDBRelationDescriptor();
        object relationEnd = relationDescriptor.GetRelationEnd(childEntityOrOccurence);
        this.InternalDataService.RemoveComplexDBRelation((object) parentEntity, propertyName, relationEnd, childEntityOrOccurence, relationDescriptor);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityTypeDescriptor.EntityKind);
    }
  }

  public void RemoveChildEntity<TProperty>(
    TEntity parentEntity,
    Expression<Func<TEntity, TProperty>> propertySelector,
    object childEntityOrOccurence)
  {
    if (propertySelector == null)
      throw new ArgumentNullException(nameof (propertySelector));
    this.RemoveChildEntity(parentEntity, NameOf.PropertyName<TEntity, TProperty>(propertySelector), childEntityOrOccurence);
  }
}
