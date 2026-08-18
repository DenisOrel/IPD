// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ProviderInfo
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Класс, служащий базой для создания классов-контейнеров сведений, получаемых от провайдеров
/// </summary>
public class ProviderInfo
{
  /// <summary>Допустимые элементы</summary>
  private Dictionary<string, object> _possibleItemsTable;
  /// <summary>Названия допустимых элементов</summary>
  private string[] _possibleItems;
  /// <summary>Подавляемые элементы</summary>
  private ArrayList _suppressedItemsList;
  /// <summary>Названия подавляемых элементов</summary>
  private string[] _suppressedItems;

  /// <summary>Создать экземпляр класса</summary>
  public ProviderInfo()
  {
    this._possibleItemsTable = new Dictionary<string, object>();
    this._possibleItems = (string[]) null;
    this._suppressedItemsList = new ArrayList();
    this._suppressedItems = (string[]) null;
  }

  /// <summary>Получить список допустимых элементов</summary>
  protected string[] PossibleItems
  {
    get
    {
      if (this._possibleItems == null && this._possibleItemsTable.Count > 0)
      {
        this._possibleItems = new string[this._possibleItemsTable.Count];
        this._possibleItemsTable.Keys.CopyTo(this._possibleItems, 0);
      }
      return this._possibleItems;
    }
  }

  /// <summary>Получить список подавляемых элементов</summary>
  protected string[] SuppressedItems
  {
    get
    {
      if (this._suppressedItems == null && this._suppressedItemsList.Count > 0)
        this._suppressedItems = (string[]) this._suppressedItemsList.ToArray(typeof (string));
      return this._suppressedItems;
    }
  }

  /// <summary>Добавить допустимый элемент</summary>
  /// <param name="itemName">Название допустимого элемента</param>
  /// <param name="item">Допустимый элемент</param>
  protected void AddPossibleItem(string itemName, object item)
  {
    if (this._possibleItemsTable.ContainsKey(itemName))
      return;
    this._possibleItemsTable[itemName] = item;
    this._possibleItems = (string[]) null;
  }

  /// <summary>Удалить допустимый элемент</summary>
  /// <param name="itemName">Название удаляемого допустимого элемента</param>
  protected void RemovePossibleItem(string itemName)
  {
    if (!this._possibleItemsTable.ContainsKey(itemName))
      return;
    this._possibleItemsTable.Remove(itemName);
    this._possibleItems = (string[]) null;
  }

  /// <summary>Добавить подавляемый элемент</summary>
  /// <param name="itemName">Название подавляемого элемента</param>
  protected void AddSuppressedItem(string itemName)
  {
    if (this._suppressedItemsList.Contains((object) itemName))
      return;
    this._suppressedItemsList.Add((object) itemName);
    this._suppressedItems = (string[]) null;
  }

  /// <summary>Является ли указанный элемент допустимым</summary>
  /// <param name="itemName">Название проверяемого элемента</param>
  /// <returns>true, если указанный элемент является допустимым</returns>
  protected bool IsItemPossible(string itemName) => this._possibleItemsTable.ContainsKey(itemName);

  /// <summary>Является ли указанный элемент подавляемым</summary>
  /// <param name="itemName">Название проверяемого элемента</param>
  /// <returns>true, если указанный элемент является подавляемым</returns>
  protected bool IsItemSuppressed(string itemName)
  {
    return this._suppressedItemsList.Contains((object) itemName);
  }

  /// <summary>Получить элемент по его имени</summary>
  /// <param name="itemName">Имя элемента</param>
  /// <returns>Элемент или null</returns>
  protected object GetPossibleItem(string itemName)
  {
    return !this._possibleItemsTable.ContainsKey(itemName) ? (object) null : this._possibleItemsTable[itemName];
  }
}
