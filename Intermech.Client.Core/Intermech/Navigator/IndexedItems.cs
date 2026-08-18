
// Type: Intermech.Navigator.IndexedItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>
/// Реализует коллекцию элементов навигации, представляющую
/// собой подмножество элементов другой такой коллекции. Элементы
/// навигации, входящие в подмножество, указываются с помощью
/// индексов или ключей в исходной коллекции.
/// </summary>
public class IndexedItems : ISelectedItems, ISimpleSelectedItems
{
  private ISelectedItems _sourceItems;
  private IList<object> _indexes;

  /// <summary>
  /// Создает коллекцию элементов навигации с пустым множеством индексов.
  /// </summary>
  /// <param name="items">Исходная коллекция</param>
  public IndexedItems(ISelectedItems items)
  {
    this._sourceItems = items;
    this._indexes = (IList<object>) new List<object>();
  }

  /// <summary>
  /// Добавляет индекс элемента из исходной коллекции,
  /// принадлжещий подмножеству.
  /// </summary>
  /// <param name="index"></param>
  public void QueueIndex(int index)
  {
    this._indexes.Add((this._sourceItems is IKeyedSelectedItems sourceItems ? (object) sourceItems.GetItemKey(index) : (object) index) ?? (object) index.ToString());
  }

  public bool IsCollage => this._sourceItems.IsCollage;

  public int Count => this._indexes.Count;

  public object GetItemData(int index, Type dataFormat)
  {
    int index1 = this._sourceItems is IKeyedSelectedItems sourceItems ? sourceItems.GetItemIndex((string) this._indexes[index]) : (int) this._indexes[index];
    if (index1 < 0 && sourceItems != null)
      return (object) null;
    return sourceItems == null ? this._sourceItems.GetItemData(index1, dataFormat) : sourceItems.GetItemData(index1, dataFormat);
  }

  public INodeID GetItemID(int index)
  {
    return !(this._sourceItems is IKeyedSelectedItems sourceItems) ? this._sourceItems.GetItemID((int) this._indexes[index]) : sourceItems.GetItemID(sourceItems.GetItemIndex((string) this._indexes[index]));
  }

  public object GetParentData(int index, Type dataFormat)
  {
    return !(this._sourceItems is IKeyedSelectedItems sourceItems) ? this._sourceItems.GetParentData((int) this._indexes[index], dataFormat) : sourceItems.GetParentData(sourceItems.GetItemIndex((string) this._indexes[index]), dataFormat);
  }

  public NodeIDPath GetParentPath(int index)
  {
    return !(this._sourceItems is IKeyedSelectedItems sourceItems) ? this._sourceItems.GetParentPath((int) this._indexes[index]) : sourceItems.GetParentPath(sourceItems.GetItemIndex((string) this._indexes[index]));
  }
}
