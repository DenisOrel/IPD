// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityChangeTrackerDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public class EntityChangeTrackerDescriptor(Type entityType) : BasicEntityTypeDescriptor(entityType)
{
  private IDictionary<string, DataPropertyDescriptor> dataProperties;
  private ICollection<EntityPropertyDefinition> dataPropertyDefinitions;
  private IDictionary<string, NavigationPropertyDescriptor> navigationProperties;
  private ICollection<EntityPropertyDefinition> navigationPropertyDefinitions;

  public IDictionary<string, DataPropertyDescriptor> DataProperties
  {
    [DebuggerStepThrough] get => this.dataProperties;
    set
    {
      this.RequireNotInitialized();
      this.dataProperties = value;
    }
  }

  /// <summary>
  /// Возвращает коллекцию определений свойств объекта.
  /// Значение этого свойства доступно только после инициализации дескриптора.
  /// </summary>
  public ICollection<EntityPropertyDefinition> DataPropertyDefinitions
  {
    [DebuggerStepThrough] get => this.dataPropertyDefinitions;
  }

  public IDictionary<string, NavigationPropertyDescriptor> NavigationProperties
  {
    [DebuggerStepThrough] get => this.navigationProperties;
    set
    {
      this.RequireNotInitialized();
      this.navigationProperties = value;
    }
  }

  /// <summary>
  /// Возвращает коллекцию определений навигационных свойств объекта.
  /// Значение этого свойства доступно только после инициализации дескриптора.
  /// </summary>
  public ICollection<EntityPropertyDefinition> NavigationPropertyDefinitions
  {
    [DebuggerStepThrough] get => this.navigationPropertyDefinitions;
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
    if (this.NavigationProperties == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "NavigationProperties");
  }

  /// <summary>Выполняет инициализацию объекта.</summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.dataPropertyDefinitions = (ICollection<EntityPropertyDefinition>) new List<EntityPropertyDefinition>(this.DataProperties.Count);
    foreach (KeyValuePair<string, DataPropertyDescriptor> dataProperty in (IEnumerable<KeyValuePair<string, DataPropertyDescriptor>>) this.DataProperties)
      this.dataPropertyDefinitions.Add(dataProperty.Value.Definition);
    this.navigationPropertyDefinitions = (ICollection<EntityPropertyDefinition>) new List<EntityPropertyDefinition>(this.NavigationProperties.Count);
    foreach (KeyValuePair<string, NavigationPropertyDescriptor> navigationProperty in (IEnumerable<KeyValuePair<string, NavigationPropertyDescriptor>>) this.NavigationProperties)
      this.navigationPropertyDefinitions.Add(navigationProperty.Value.Definition);
  }

  /// <summary>
  /// Выполняет очистку текущего объекта в случае необработанного исключения в процессе инициализации текущего объекта.
  /// </summary>
  protected override void DoCleanupAfterInitializationError()
  {
    base.DoCleanupAfterInitializationError();
    this.dataPropertyDefinitions = (ICollection<EntityPropertyDefinition>) null;
    this.navigationPropertyDefinitions = (ICollection<EntityPropertyDefinition>) null;
  }

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected override void DoPostInitialize()
  {
    base.DoPostInitialize();
    this.dataProperties = this.dataProperties.IsReadOnly ? this.dataProperties : (IDictionary<string, DataPropertyDescriptor>) new ReadOnlyDictionary<string, DataPropertyDescriptor>(this.dataProperties);
    this.dataPropertyDefinitions = this.dataPropertyDefinitions.IsReadOnly ? this.dataPropertyDefinitions : (ICollection<EntityPropertyDefinition>) new ReadOnlyCollectionWrapper<EntityPropertyDefinition>(this.dataPropertyDefinitions);
    this.navigationProperties = this.navigationProperties.IsReadOnly ? this.navigationProperties : (IDictionary<string, NavigationPropertyDescriptor>) new ReadOnlyDictionary<string, NavigationPropertyDescriptor>(this.navigationProperties);
    this.navigationPropertyDefinitions = this.navigationPropertyDefinitions.IsReadOnly ? this.navigationPropertyDefinitions : (ICollection<EntityPropertyDefinition>) new ReadOnlyCollectionWrapper<EntityPropertyDefinition>(this.navigationPropertyDefinitions);
  }

  protected override ICollection<EntityPropertyDefinition> DoGetDataPropertyDefinitions()
  {
    return this.DataPropertyDefinitions;
  }

  protected override EntityPropertyData DoGetDataProperty(object entity, string propertyName)
  {
    DataPropertyDescriptor propertyDescriptor;
    return this.DataProperties.TryGetValue(propertyName, out propertyDescriptor) ? propertyDescriptor.GetValue(entity) : base.DoGetDataProperty(entity, propertyName);
  }

  protected override ICollection<EntityPropertyDefinition> DoGetNavigationPropertyDefinitions()
  {
    return this.NavigationPropertyDefinitions;
  }

  protected override EntityPropertyData DoGetNavigationProperty(object entity, string propertyName)
  {
    NavigationPropertyDescriptor propertyDescriptor;
    return this.NavigationProperties.TryGetValue(propertyName, out propertyDescriptor) ? propertyDescriptor.GetValue(entity) : base.DoGetNavigationProperty(entity, propertyName);
  }
}
