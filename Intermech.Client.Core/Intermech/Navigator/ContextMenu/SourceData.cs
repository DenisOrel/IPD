
// Type: Intermech.Navigator.ContextMenu.SourceData
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует контейнер с всеми исходными данными, которые
/// доступны для процесса сборки команд контекстного меню.
/// </summary>
internal class SourceData : ISourceData
{
  private ISelectedItems _items;
  private IServiceProvider _viewServices;
  private IDictionary _categoryClusters;
  private IDictionary _typeClusters;

  /// <summary>Создает новый контейнер.</summary>
  /// <param name="items">Коллекция элементов навигации, выбранных пользователем</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  public SourceData(ISelectedItems items, IServiceProvider viewServices)
  {
    Services.Check(items);
    Services.Check(viewServices);
    this._items = items;
    this._viewServices = viewServices;
    this._categoryClusters = (IDictionary) null;
    this._typeClusters = (IDictionary) null;
  }

  /// <summary>
  /// Возвращает коллекцию выбранных пользователем элементов навигации.
  /// </summary>
  public ISelectedItems Items => this._items;

  /// <summary>Возвращает контейнер с дополнительными сервисами.</summary>
  public IServiceProvider ViewServices => this._viewServices;

  /// <summary>
  /// Возврашает словарь, содержащий коллекцию выбранных пользователем
  /// элементов навигации, разбитую на кластеры. В каждом кластере находятся
  /// элементы, принадлежащие одной и той же категории. Ключем в словаре
  /// служит идентификатор категории.
  /// </summary>
  public IDictionary CategoryClusters
  {
    get
    {
      if (this._categoryClusters == null)
        this.ClusterItems();
      return this._categoryClusters;
    }
  }

  /// <summary>
  /// Возврашает словарь, содержащий коллекцию выбранных пользователем
  /// элементов навигации, разбитую на кластеры. В каждом кластере находятся
  /// элементы, принадлежащие одной и той же категории и типу. Ключем в словаре
  /// служит CategoryTypeKey - пара идентификатов.
  /// </summary>
  public IDictionary TypeClusters
  {
    get
    {
      if (this._typeClusters == null)
        this.ClusterItems();
      return this._typeClusters;
    }
  }

  /// <summary>
  /// Разбивает исходную коллекцию элементов навигации на кластеры.
  /// </summary>
  private void ClusterItems()
  {
    this._categoryClusters = (IDictionary) new HybridDictionary();
    this._typeClusters = (IDictionary) new HybridDictionary();
    for (int index = 0; index < this._items.Count; ++index)
    {
      INodeID itemId = this._items.GetItemID(index);
      IndexedItems indexedItems1 = (IndexedItems) this._categoryClusters[(object) itemId.CategoryID];
      if (indexedItems1 == null)
      {
        indexedItems1 = new IndexedItems(this._items);
        this._categoryClusters.Add((object) itemId.CategoryID, (object) indexedItems1);
      }
      indexedItems1.QueueIndex(index);
      CategoryTypeKey key = new CategoryTypeKey(itemId.CategoryID, itemId.TypeID);
      IndexedItems indexedItems2 = (IndexedItems) this._typeClusters[(object) key];
      if (indexedItems2 == null)
      {
        indexedItems2 = new IndexedItems(this._items);
        this._typeClusters.Add((object) key, (object) indexedItems2);
      }
      indexedItems2.QueueIndex(index);
    }
  }
}
