
// Type: Intermech.Navigator.Controls.DataHelperEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Navigator.Controls;

/// <summary>Аргументы события от источника данных</summary>
public class DataHelperEventArgs : EventArgs
{
  /// <summary>Таблица источника данных</summary>
  private DataTable _table;
  /// <summary>Коллекция колонок</summary>
  private NodeColumnCollection _columns;

  /// <summary>Таблица источника данных</summary>
  public DataTable Table
  {
    [DebuggerStepThrough] get => this._table;
  }

  /// <summary>Коллекция колонок</summary>
  public NodeColumnCollection Columns
  {
    [DebuggerStepThrough] get => this._columns;
  }

  /// <summary>Создать аргументы события</summary>
  /// <param name="table">Таблица источника данных</param>
  /// <param name="columns">Коллекция колонок</param>
  public DataHelperEventArgs(DataTable table, NodeColumnCollection columns)
  {
    this._table = table;
    this._columns = columns;
  }
}
