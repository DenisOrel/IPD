
// Type: Intermech.Navigator.VirtualColumns.VirtualQueryResultColumn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Navigator.VirtualColumns;

/// <summary>Описание "виртуальной" колонки, значение которой не получается с сервера, а добавляется в результат запроса уже на клиенте</summary>
public class VirtualQueryResultColumn
{
  /// <summary>Имя поля, которым представляются значения в таблице результата запроса</summary>
  [NotNull]
  public readonly string FieldName;
  /// <summary>Тип данных значений поля, которым представляются значения в таблице результата запроса</summary>
  [NotNull]
  public readonly Type DataType;
  /// <summary>Значение по-умолчанию, которым должны быть заполнены соотв. поля в таблице результата запроса</summary>
  [CanBeNull]
  public readonly object DefaultValue;

  /// <summary>Конструктор</summary>
  /// <param name="fieldName">Имя поля, которым представляются значения в таблице результата запроса</param>
  /// <param name="dataType">Тип данных значений поля, которым представляются значения в таблице результата запроса</param>
  /// <param name="defaultValue">Значение по-умолчанию, которым должны быть заполнены соотв. поля в таблице результата запроса</param>
  public VirtualQueryResultColumn([NotNull] string fieldName, [NotNull] Type dataType, [CanBeNull] object defaultValue = null)
  {
    this.FieldName = fieldName;
    this.DataType = dataType;
    this.DefaultValue = defaultValue;
  }

  /// <summary>Добавить в результат запроса полученный с сервера виртуальные колонки, значения которых должны быть заполнены уже на клиенте</summary>
  public static void AddVirtualColumns([NotNull] DataTable datatable, [NotNull] RecordMapping mapping)
  {
    VirtualQueryResultColumn.AddVirtualColumns(datatable, mapping, (System.Func<VirtualQueryResultColumn, object>) (virtualColumn => virtualColumn.DefaultValue));
  }

  /// <summary>Добавить в результат запроса полученный с сервера виртуальные колонки, значения которых должны быть заполнены уже на клиенте</summary>
  public static void AddVirtualColumns(
    [NotNull] DataTable datatable,
    [NotNull] RecordMapping mapping,
    [NotNull] System.Func<VirtualQueryResultColumn, object> getColumnDefaultValue)
  {
    VirtualQueryResultColumn[] array = mapping.VirtualColumns().ToArray<VirtualQueryResultColumn>();
    if (array.Length != 0)
    {
      foreach (VirtualQueryResultColumn queryResultColumn in array)
      {
        DataColumn column = new DataColumn(queryResultColumn.FieldName, queryResultColumn.DataType);
        column.DefaultValue = getColumnDefaultValue(queryResultColumn);
        datatable.Columns.Add(column);
        int ordinal = ((IEnumerable<object>) mapping.Fields).IndexOf<object>((object) queryResultColumn);
        if (ordinal >= 0 && ordinal != datatable.Columns.IndexOf(column) && ordinal < datatable.Columns.Count)
          column.SetOrdinal(ordinal);
      }
    }
    datatable.AcceptChanges();
  }
}
