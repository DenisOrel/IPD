// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBRelationEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>Реализация является thread safe.</summary>
internal sealed class DBRelationEntityTypeDescriptor(Type childOccurenceType) : 
  DBEntityTypeDescriptor(DBEntityKind.Relation, childOccurenceType),
  IDBRelationEntityTypeDescriptor,
  IDBEntityTypeDescriptor,
  IEntityTypeDescriptor
{
  private DataPropertyDescriptors dataProperties;
  private DataPropertyDescriptor keyProperty;
  private DataPropertyDescriptor guidProperty;
  private NavigationPropertyDescriptors navigationProperties;
  private NavigationPropertyDescriptor relationStartProperty;
  private NavigationPropertyDescriptor relationEndProperty;

  public DataPropertyDescriptors DataProperties
  {
    [DebuggerStepThrough] get => this.dataProperties;
    set
    {
      this.RequireNotInitialized();
      this.dataProperties = value;
    }
  }

  public DataPropertyDescriptor KeyProperty
  {
    [DebuggerStepThrough] get => this.keyProperty;
    set
    {
      this.RequireNotInitialized();
      this.keyProperty = value;
    }
  }

  public DataPropertyDescriptor GuidProperty
  {
    [DebuggerStepThrough] get => this.guidProperty;
    set
    {
      this.RequireNotInitialized();
      this.guidProperty = value;
    }
  }

  public NavigationPropertyDescriptor RelationStartProperty
  {
    [DebuggerStepThrough] get => this.relationStartProperty;
    set
    {
      this.RequireNotInitialized();
      this.relationStartProperty = value;
    }
  }

  public NavigationPropertyDescriptor RelationEndProperty
  {
    [DebuggerStepThrough] get => this.relationEndProperty;
    set
    {
      this.RequireNotInitialized();
      this.relationEndProperty = value;
    }
  }

  public NavigationPropertyDescriptors NavigationProperties
  {
    [DebuggerStepThrough] get
    {
      this.RequireInitialized();
      return this.navigationProperties;
    }
  }

  /// <summary>
  /// Проверяет корректность свойств объекта перед инициализацией.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Одно из свойств объекта имеет некорректное значение</exception>
  protected override void DoValidateBeforeInitialize()
  {
    base.DoValidateBeforeInitialize();
    if (this.DataProperties == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DataProperties");
    if (this.KeyProperty == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "KeyProperty");
    if (this.GuidProperty == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "GuidProperty");
    if (this.RelationStartProperty == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "RelationStartProperty");
    if (this.RelationEndProperty == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "RelationEndProperty");
  }

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected override void DoPostInitialize()
  {
    base.DoPostInitialize();
    this.navigationProperties = new NavigationPropertyDescriptors(this.EntityType, (IDictionary<string, NavigationPropertyDescriptor>) new Dictionary<string, NavigationPropertyDescriptor>()
    {
      {
        this.RelationStartProperty.Definition.Name,
        this.RelationStartProperty
      },
      {
        this.RelationEndProperty.Definition.Name,
        this.RelationEndProperty
      }
    });
  }

  public override IDBObjectEntityTypeDescriptor AsDBObjectDescriptor()
  {
    this.RequireInitialized();
    throw new NotSupportedException();
  }

  public override IDBRelationEntityTypeDescriptor AsDBRelationDescriptor()
  {
    this.RequireInitialized();
    return (IDBRelationEntityTypeDescriptor) this;
  }

  public long GetKey(object relationEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    return (long) this.KeyProperty.GetValue(relationEntity).PropertyValue;
  }

  public void SetKey(object relationEntity, long newKey)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    this.KeyProperty.SetValue(relationEntity, (object) newKey);
  }

  public Guid GetGuid(object relationEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    return (Guid) this.GuidProperty.GetValue(relationEntity).PropertyValue;
  }

  public void SetGuid(object relationEntity, Guid newGuid)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    this.GuidProperty.SetValue(relationEntity, (object) newGuid);
  }

  public object GetRelationStart(object relationEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    return this.RelationStartProperty.GetValue(relationEntity).PropertyValue;
  }

  public void SetRelationStart(object relationEntity, object parentEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    this.RequireInitialized();
    this.CheckParentEntityType(parentEntity);
    this.RelationStartProperty.SetValue(relationEntity, parentEntity);
  }

  public object GetRelationEnd(object relationEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    this.RequireInitialized();
    return this.RelationEndProperty.GetValue(relationEntity).PropertyValue;
  }

  public void SetRelationEnd(object relationEntity, object childEntity)
  {
    if (relationEntity == null)
      throw new ArgumentNullException(nameof (relationEntity));
    if (childEntity == null)
      throw new ArgumentNullException(nameof (childEntity));
    this.RequireInitialized();
    this.CheckChildEntityType(childEntity);
    this.RelationEndProperty.SetValue(relationEntity, childEntity);
  }

  public object CreateInstance(object parentEntity, object childEntity)
  {
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (childEntity == null)
      throw new ArgumentNullException(nameof (childEntity));
    this.RequireInitialized();
    this.CheckParentEntityType(parentEntity);
    this.CheckChildEntityType(childEntity);
    object instance = Activator.CreateInstance(this.EntityType);
    this.RelationStartProperty.SetValue(instance, parentEntity);
    this.RelationEndProperty.SetValue(instance, childEntity);
    return instance;
  }

  private void CheckParentEntityType(object parentEntity)
  {
    Type type = parentEntity.GetType();
    if (!this.RelationStartProperty.Definition.PropertyType.IsAssignableFrom(type))
      throw new InvalidOperationException($"Дескриптор объекта-связки '{this.EntityType}' не поддерживает доменные объекты типа '{type}'.");
  }

  private void CheckChildEntityType(object childEntity)
  {
    Type type = childEntity.GetType();
    if (!this.RelationEndProperty.Definition.PropertyType.IsAssignableFrom(type))
      throw new InvalidOperationException($"Дескриптор объекта-связки '{this.EntityType}' не поддерживает доменные объекты типа '{type}'.");
  }
}
