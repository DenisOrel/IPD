// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>Реализация является thread safe.</summary>
internal class DBObjectEntityTypeDescriptor(Type entityType) : 
  DBEntityTypeDescriptor(DBEntityKind.Object, entityType),
  IDBObjectEntityTypeDescriptor,
  IDBEntityTypeDescriptor,
  IEntityTypeDescriptor
{
  private DBObjectTypeMapping dbObjectType;
  private DataPropertyDescriptors dataProperties;
  private DataPropertyMappings dataPropertiesMappings;
  private DataPropertyDescriptor keyProperty;
  private NavigationPropertyDescriptors navigationProperties;
  private DBObjectNavigationPropertyMappings navigationPropertiesMappings;

  public DBObjectTypeMapping DBObjectType
  {
    [DebuggerStepThrough] get => this.dbObjectType;
    set
    {
      this.RequireNotInitialized();
      this.dbObjectType = value;
    }
  }

  public DataPropertyDescriptors DataProperties
  {
    [DebuggerStepThrough] get => this.dataProperties;
    set
    {
      this.RequireNotInitialized();
      this.dataProperties = value;
    }
  }

  public DataPropertyMappings DataPropertiesMappings
  {
    [DebuggerStepThrough] get => this.dataPropertiesMappings;
    set
    {
      this.RequireNotInitialized();
      this.dataPropertiesMappings = value;
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

  public NavigationPropertyDescriptors NavigationProperties
  {
    [DebuggerStepThrough] get => this.navigationProperties;
    set
    {
      this.RequireNotInitialized();
      this.navigationProperties = value;
    }
  }

  public DBObjectNavigationPropertyMappings NavigationPropertiesMappings
  {
    [DebuggerStepThrough] get => this.navigationPropertiesMappings;
    set
    {
      this.RequireNotInitialized();
      this.navigationPropertiesMappings = value;
    }
  }

  /// <summary>
  /// Проверяет корректность свойств объекта перед инициализацией.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Одно из свойств объекта имеет некорректное значение</exception>
  protected override void DoValidateBeforeInitialize()
  {
    base.DoValidateBeforeInitialize();
    if (this.DBObjectType == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DBObjectType");
    if (this.DataProperties == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DataProperties");
    if (this.DataPropertiesMappings == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "DataPropertiesMappings");
    if (this.KeyProperty == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "KeyProperty");
    if (!this.DataProperties.AsCollection.Contains(this.KeyProperty))
      throw PropertyExceptions.PropertyBadValueException((object) this, "KeyProperty", "Значение свойства должно быть элементом коллекции DataProperties.");
    if (this.NavigationProperties == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "NavigationProperties");
    if (this.NavigationPropertiesMappings == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "NavigationPropertiesMappings");
  }

  /// <summary>
  /// Проверяет корректность свойств объекта после инициализации.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Одно из свойств объекта имеет некорректное значение</exception>
  protected override void DoValidateAfterInitialize()
  {
    base.DoValidateAfterInitialize();
    this.DBObjectType.ValidateBeforeFreeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DataPropertyMapping>) this.DataPropertiesMappings.AsCollection)
      freezableObject.ValidateBeforeFreeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DBObjectNavigationPropertyMapping>) this.NavigationPropertiesMappings.AsCollection)
      freezableObject.ValidateBeforeFreeze();
  }

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected override void DoPostInitialize()
  {
    base.DoPostInitialize();
    this.DBObjectType.Freeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DataPropertyMapping>) this.DataPropertiesMappings.AsCollection)
      freezableObject.Freeze();
    foreach (FreezableObject freezableObject in (IEnumerable<DBObjectNavigationPropertyMapping>) this.NavigationPropertiesMappings.AsCollection)
      freezableObject.Freeze();
  }

  public override IDBObjectEntityTypeDescriptor AsDBObjectDescriptor()
  {
    this.RequireInitialized();
    return (IDBObjectEntityTypeDescriptor) this;
  }

  public override IDBRelationEntityTypeDescriptor AsDBRelationDescriptor()
  {
    this.RequireInitialized();
    throw new NotSupportedException();
  }

  public long GetKey(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.RequireInitialized();
    return (long) this.KeyProperty.GetValue(entity).PropertyValue;
  }

  public void SetKey(object entity, long newKey)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.RequireInitialized();
    this.KeyProperty.SetValue(entity, (object) newKey);
  }

  public object CreateInstance()
  {
    this.RequireInitialized();
    return Activator.CreateInstance(this.EntityType);
  }
}
