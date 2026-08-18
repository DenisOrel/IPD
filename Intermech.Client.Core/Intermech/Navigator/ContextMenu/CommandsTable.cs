
// Type: Intermech.Navigator.ContextMenu.CommandsTable
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.Collections.Specialized;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Представляет таблицу команд контекстного меню, по которой
/// возможно построение самого контекстного меню.
/// </summary>
public class CommandsTable
{
  private static readonly string[] _emptyNames = new string[0];
  private IDictionary _items = (IDictionary) new HybridDictionary();
  private string[] _commandNames;

  /// <summary>Очищает таблицу команд.</summary>
  public void Clear()
  {
    this._commandNames = (string[]) null;
    this._items.Clear();
  }

  /// <summary>Добавляет новую команду в таблицу.</summary>
  /// <param name="commandName">Имя команды</param>
  /// <param name="commandLink">Список обработчиков команды</param>
  public void Add(string commandName, CommandLink commandLink)
  {
    Services.Check(commandName);
    Services.Check(commandLink);
    this._commandNames = (string[]) null;
    this._items.Add((object) commandName, (object) commandLink);
  }

  /// <summary>Удаляет команду из таблицы.</summary>
  /// <param name="commandName">Имя команды</param>
  public void Remove(string commandName)
  {
    Services.Check(commandName);
    this._commandNames = (string[]) null;
    this._items.Remove((object) commandName);
  }

  /// <summary>Возвращает признак наличия команды в таблице.</summary>
  /// <param name="commandName"></param>
  /// <returns></returns>
  public bool Contains(string commandName)
  {
    Services.Check(commandName);
    return this._items.Contains((object) commandName);
  }

  /// <summary>Возвращает количество команд в таблице.</summary>
  public int Count => this._items.Count;

  /// <summary>Возвращает массив имен команд, находящихся в таблице.</summary>
  public string[] CommandNames
  {
    get
    {
      if (this._commandNames == null)
      {
        this._commandNames = this._items.Keys.Count == 0 ? CommandsTable._emptyNames : new string[this._items.Keys.Count];
        if (this._commandNames != CommandsTable._emptyNames)
          this._items.Keys.CopyTo((Array) this._commandNames, 0);
      }
      return this._commandNames;
    }
  }

  /// <summary>Возвращает список обработчиков команды.</summary>
  public CommandLink this[string commandName]
  {
    get
    {
      Services.Check(commandName);
      return (CommandLink) this._items[(object) commandName];
    }
  }
}
