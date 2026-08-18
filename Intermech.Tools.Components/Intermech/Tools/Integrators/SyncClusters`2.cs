// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.SyncClusters`2
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует контейнер для классификации сущностей в процессе сравнения списков сущностей, полученных из локального источника и из базы данных PDM-системы.
/// </summary>
/// <typeparam name="TLocalItem">Тип для представления сущностей, полученных из локального источника</typeparam>
/// <typeparam name="TDbItem">Тип для представления сущностей, полученных из базы данных PDM-системы</typeparam>
internal class SyncClusters<TLocalItem, TDbItem>
{
  private readonly List<TLocalItem> newItems;
  private readonly List<Tuple<TLocalItem, TDbItem>> existingItems;
  private readonly List<TDbItem> deletedItems;

  /// <summary>Создает контейнер.</summary>
  public SyncClusters()
    : this(8)
  {
  }

  /// <summary>Создает контейнер.</summary>
  /// <param name="capacity">Начальная емкость контейнера</param>
  public SyncClusters(int capacity)
  {
    this.newItems = new List<TLocalItem>(capacity);
    this.existingItems = new List<Tuple<TLocalItem, TDbItem>>(capacity);
    this.deletedItems = new List<TDbItem>(capacity);
  }

  /// <summary>
  /// Возвращает список локальных сущностей, для которых нет пары в базе данных PDM-системы.
  /// </summary>
  public List<TLocalItem> NewItems => this.newItems;

  /// <summary>
  /// Возвращает список пар из локальных сущностей и соответствующих им объектов в базе данных PDM-системы.
  /// </summary>
  public List<Tuple<TLocalItem, TDbItem>> ExistingItems => this.existingItems;

  /// <summary>
  /// Возвращает список объектов базы данных PDM-системы, для которых в локальном источнике нет исходных сущностей.
  /// </summary>
  public List<TDbItem> DeletedItems => this.deletedItems;
}
