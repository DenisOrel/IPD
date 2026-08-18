
// Type: Intermech.Navigator.Views.ViewsTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Views;

/// <summary>Представляет таблицу сведений о закладках.</summary>
internal class ViewsTable
{
  /// <summary>
  /// Если имя закладки начинается с этого символа, то имя ищется не по
  /// его полной входимости в списке имён, а интерпретируется как начало
  /// строки в имени закладки
  /// </summary>
  private static char Wildcard = Convert.ToChar("@");
  /// <summary>
  /// Коллекция пар значений
  /// [(string)Название закладки] = [(ViewsTableEntry)Описание закладки]
  /// </summary>
  private SortedDictionary<string, ViewsTableEntry> _items = new SortedDictionary<string, ViewsTableEntry>();

  /// <summary>Коллекция названий закладок</summary>
  public string[] ViewNames
  {
    get
    {
      string[] array = this._items.Keys.Count == 0 ? (string[]) null : new string[this._items.Keys.Count];
      if (array != null)
        this._items.Keys.CopyTo(array, 0);
      return array;
    }
  }

  /// <summary>Очистить таблицу сведений о закладках</summary>
  public void Clear() => this._items.Clear();

  /// <summary>
  /// Содержит ли таблица закладку с указанным именем
  /// (также поддерживаются закладки с именами, начинающимися с символа "@")
  /// </summary>
  /// <param name="viewName">Название закладки (поддерживаются и закладки с именами, начинающимися с символа "@")</param>
  /// <returns>true, если закладка с указанным именем найдена в таблице описаний закладок</returns>
  public bool Contains(string viewName)
  {
    int num = viewName.Length <= 1 ? 0 : ((int) viewName[0] == (int) ViewsTable.Wildcard ? 1 : 0);
    bool flag = this._items.ContainsKey(viewName);
    if (num == 0)
      return flag;
    viewName = viewName.Substring(1, viewName.Length - 1);
    foreach (KeyValuePair<string, ViewsTableEntry> keyValuePair in this._items)
    {
      if (keyValuePair.Key.IndexOf(viewName) == 0)
        return true;
    }
    return false;
  }

  /// <summary>Получить описание закладки по её имени</summary>
  /// <param name="viewName">Имя закладки</param>
  /// <returns>Описание закладки или null</returns>
  public ViewsTableEntry this[string viewName]
  {
    get => !this._items.ContainsKey(viewName) ? (ViewsTableEntry) null : this._items[viewName];
  }

  /// <summary>Добавить закладку с указанным именем в таблицу</summary>
  /// <param name="viewName">Имя закладки</param>
  /// <param name="entry">Описание закладки</param>
  public void Add(string viewName, ViewsTableEntry entry) => this._items.Add(viewName, entry);

  /// <summary>
  /// Удалить закладку с указанным именем
  /// (также поддерживаются закладки с именами, начинающимися с символа "@")
  /// </summary>
  /// <param name="viewName">Имя закладки (также поддерживаются закладки с именами, начинающимися с символа "@")</param>
  public void Remove(string viewName)
  {
    if ((viewName.Length <= 1 ? 0 : ((int) viewName[0] == (int) ViewsTable.Wildcard ? 1 : 0)) == 0)
    {
      this._items.Remove(viewName);
    }
    else
    {
      List<string> stringList = new List<string>();
      viewName = viewName.Substring(1, viewName.Length - 1);
      foreach (KeyValuePair<string, ViewsTableEntry> keyValuePair in this._items)
      {
        if (keyValuePair.Key.IndexOf(viewName) == 0)
          stringList.Add(keyValuePair.Key);
      }
      for (int index = stringList.Count - 1; index >= 0; --index)
        this._items.Remove(stringList[index]);
    }
  }
}
