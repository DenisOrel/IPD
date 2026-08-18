// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.ReadOnlyPropertyMap`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

/// <summary>
/// Этот класс реализует коллекцию вспомгательных объектов для свойств доменного объекта или объекта-связки.
/// Реализация является immutable и thread safe.
/// </summary>
/// <typeparam name="TProperty">Тип вспомогательного объекта</typeparam>
public class ReadOnlyPropertyMap<TProperty> where TProperty : class
{
  private Type entityType;
  private ICollection<string> propertyNames;
  private int count;
  private IDictionary<string, TProperty> items;

  public ReadOnlyPropertyMap(Type entityType, IDictionary<string, TProperty> items)
  {
    if (entityType == (Type) null)
      throw new ArgumentNullException(nameof (entityType));
    if (items == null)
      throw new ArgumentNullException(nameof (items));
    this.entityType = entityType;
    this.propertyNames = (ICollection<string>) new ReadOnlyCollectionWrapper<string>((ICollection<string>) new List<string>((IEnumerable<string>) items.Keys));
    this.count = items.Count;
    this.items = items.IsReadOnly ? items : (IDictionary<string, TProperty>) new ReadOnlyDictionary<string, TProperty>(items);
  }

  protected Type EntityType
  {
    [DebuggerStepThrough] get => this.entityType;
  }

  public IDictionary<string, TProperty> AsDictionary
  {
    [DebuggerStepThrough] get => this.items;
  }

  public ICollection<TProperty> AsCollection
  {
    [DebuggerStepThrough] get => this.items.Values;
  }

  public ICollection<string> PropertyNames
  {
    [DebuggerStepThrough] get => this.propertyNames;
  }

  public int Count
  {
    [DebuggerStepThrough] get => this.count;
  }

  public TProperty GetByPropertyName(string propertyName, bool throwIfNotFound)
  {
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    TProperty byPropertyName;
    if (this.items.TryGetValue(propertyName, out byPropertyName))
      return byPropertyName;
    if (!throwIfNotFound)
      return default (TProperty);
    throw new InvalidOperationException($"У доменного объекта '{this.EntityType}' отсутствует свойство '{propertyName}'.");
  }
}
