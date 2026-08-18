// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.ColumnPack
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Search.Navigator;

/// <summary>Пакет колонок</summary>
[Serializable]
public class ColumnPack : 
  IEnumerable<KeyValuePair<NavigatorColumnsKey, NodeColumnCollection>>,
  IEnumerable
{
  private Dictionary<NavigatorColumnsKey, NodeColumnCollection> _columnDictionary = new Dictionary<NavigatorColumnsKey, NodeColumnCollection>();

  /// <summary>Индексатор</summary>
  /// <param name="key">Ключ коллекции колонок</param>
  /// <returns>Коллекция колонок</returns>
  public NodeColumnCollection this[NavigatorColumnsKey key]
  {
    get
    {
      NodeColumnCollection columnCollection = (NodeColumnCollection) null;
      this._columnDictionary.TryGetValue(key, out columnCollection);
      return columnCollection;
    }
    set
    {
      if (this._columnDictionary.ContainsKey(key))
        this._columnDictionary[key] = value;
      else
        this._columnDictionary.Add(key, value);
    }
  }

  /// <summary>Удалить коллекцию колонок из пакета</summary>
  /// <param name="key">Ключ коллекции колонок</param>
  public void Remove(NavigatorColumnsKey key)
  {
    if (!this._columnDictionary.ContainsKey(key))
      return;
    this._columnDictionary.Remove(key);
  }

  public IEnumerator<KeyValuePair<NavigatorColumnsKey, NodeColumnCollection>> GetEnumerator()
  {
    return (IEnumerator<KeyValuePair<NavigatorColumnsKey, NodeColumnCollection>>) this._columnDictionary.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
}
