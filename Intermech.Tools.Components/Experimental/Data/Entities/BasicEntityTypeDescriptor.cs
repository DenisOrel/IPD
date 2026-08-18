// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.BasicEntityTypeDescriptor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Простейший класс дескриптора для доменных объектов. Он позволяет получать имена и значения свойств доменных объектов.
/// Реализация является thread safe.
/// </summary>
/// <summary>Создает объект</summary>
/// <param name="entityType">Тип доменных объектов</param>
/// <exception cref="T:ArgumentNullException">Параметр <paramref name="entityType" /> не должен быть равен null</exception>
public abstract class BasicEntityTypeDescriptor(Type entityType) : 
  EntityTypeDescriptor(entityType),
  IBasicEntityTypeDescriptor,
  IEntityTypeDescriptor
{
  private static readonly ICollection<EntityPropertyDefinition> emtpyDataDefinitions = (ICollection<EntityPropertyDefinition>) new ReadOnlyCollectionWrapper<EntityPropertyDefinition>((ICollection<EntityPropertyDefinition>) new EntityPropertyDefinition[0]);
  private static readonly ICollection<EntityPropertyDefinition> emptyReferenceDefinitions = (ICollection<EntityPropertyDefinition>) new ReadOnlyCollectionWrapper<EntityPropertyDefinition>((ICollection<EntityPropertyDefinition>) new EntityPropertyDefinition[0]);
  private IDictionary<string, EntityPropertyDefinition> dataDefinitionsByName;
  private IDictionary<string, EntityPropertyDefinition> referenceDefinitionsByName;

  /// <summary>
  /// Вызывается только в случае успешной инициализации объекта и используется кэшей, ускорителей и др.
  /// Метод не должен бросать исключений.
  /// </summary>
  protected override void DoPostInitialize()
  {
    base.DoPostInitialize();
    this.CreateDataDefinitionsByName();
    this.CreateReferenceDefinitionsByName();
  }

  private void CreateDataDefinitionsByName()
  {
    ICollection<EntityPropertyDefinition> propertyDefinitions = this.GetDataPropertyDefinitions();
    this.dataDefinitionsByName = (IDictionary<string, EntityPropertyDefinition>) new Dictionary<string, EntityPropertyDefinition>(propertyDefinitions.Count);
    foreach (EntityPropertyDefinition propertyDefinition in (IEnumerable<EntityPropertyDefinition>) propertyDefinitions)
      this.dataDefinitionsByName.Add(propertyDefinition.Name, propertyDefinition);
    this.dataDefinitionsByName = (IDictionary<string, EntityPropertyDefinition>) new ReadOnlyDictionary<string, EntityPropertyDefinition>(this.dataDefinitionsByName);
  }

  private void CreateReferenceDefinitionsByName()
  {
    ICollection<EntityPropertyDefinition> propertyDefinitions = this.GetNavigationPropertyDefinitions();
    this.referenceDefinitionsByName = (IDictionary<string, EntityPropertyDefinition>) new Dictionary<string, EntityPropertyDefinition>(propertyDefinitions.Count);
    foreach (EntityPropertyDefinition propertyDefinition in (IEnumerable<EntityPropertyDefinition>) propertyDefinitions)
      this.referenceDefinitionsByName.Add(propertyDefinition.Name, propertyDefinition);
    this.referenceDefinitionsByName = (IDictionary<string, EntityPropertyDefinition>) new ReadOnlyDictionary<string, EntityPropertyDefinition>(this.referenceDefinitionsByName);
  }

  public ICollection<EntityPropertyDefinition> GetDataPropertyDefinitions()
  {
    this.RequireInitialized();
    return this.DoGetDataPropertyDefinitions();
  }

  protected virtual ICollection<EntityPropertyDefinition> DoGetDataPropertyDefinitions()
  {
    return BasicEntityTypeDescriptor.emtpyDataDefinitions;
  }

  public EntityPropertyDefinition GetDataPropertyDefinition(
    string propertyName,
    bool throwIfNotFound = true)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    this.RequireInitialized();
    EntityPropertyDefinition propertyDefinition;
    if (this.dataDefinitionsByName.TryGetValue(propertyName, out propertyDefinition))
      return propertyDefinition;
    if (!throwIfNotFound)
      return (EntityPropertyDefinition) null;
    throw new InvalidOperationException($"У доменного объекта '{this.EntityType}' отсутствует свойство '{propertyName}'.");
  }

  public EntityPropertyData GetDataProperty(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    this.RequireInitialized();
    this.CheckEntityType(entity);
    return this.DoGetDataProperty(entity, propertyName);
  }

  protected virtual EntityPropertyData DoGetDataProperty(object entity, string propertyName)
  {
    throw new InvalidOperationException($"У доменного объекта '{entity}' отсутствует свойство '{propertyName}'.");
  }

  public ICollection<EntityPropertyDefinition> GetNavigationPropertyDefinitions()
  {
    this.RequireInitialized();
    return this.DoGetNavigationPropertyDefinitions();
  }

  protected virtual ICollection<EntityPropertyDefinition> DoGetNavigationPropertyDefinitions()
  {
    return BasicEntityTypeDescriptor.emptyReferenceDefinitions;
  }

  public EntityPropertyDefinition GetNavigationPropertyDefinition(
    string propertyName,
    bool throwIfNotFound = true)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    EntityPropertyDefinition propertyDefinition;
    if (this.referenceDefinitionsByName.TryGetValue(propertyName, out propertyDefinition))
      return propertyDefinition;
    if (!throwIfNotFound)
      return (EntityPropertyDefinition) null;
    throw new InvalidOperationException($"У доменного объекта '{this.EntityType}' отсутствует свойство '{propertyName}'.");
  }

  public EntityPropertyData GetNavigationProperty(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    this.RequireInitialized();
    this.CheckEntityType(entity);
    return this.DoGetNavigationProperty(entity, propertyName);
  }

  protected virtual EntityPropertyData DoGetNavigationProperty(object entity, string propertyName)
  {
    throw new InvalidOperationException($"У доменного объекта '{entity}' отсутствует свойство '{propertyName}'.");
  }
}
