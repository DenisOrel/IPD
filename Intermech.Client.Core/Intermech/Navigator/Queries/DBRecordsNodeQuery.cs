
// Type: Intermech.Navigator.Queries.DBRecordsNodeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Базовый класс для реализации запросов к источникам данных,
/// поддерживающим интерфейс IDBRecords.
/// </summary>
public abstract class DBRecordsNodeQuery : BaseNodeQuery
{
  protected object keyField;
  protected DataTable dataTable;

  /// <summary>
  /// Создает запрос, позволяя указать идентификатор ключевого поля
  /// источника данных.
  /// </summary>
  /// <param name="keyField">Идентификатор ключевого поля</param>
  public DBRecordsNodeQuery(object keyField)
  {
    this.keyField = keyField;
    this.dataTable = (DataTable) null;
  }

  /// <summary>Читает следующую порцию данных.</summary>
  /// <param name="bookmark">Позиция начала порции данных</param>
  /// <param name="count">Количество читаемых записей</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    this.dataTable = this.GetDataTable(this.GetQueryParams(bookmark, count, mapping));
    return this.dataTable == null || this.dataTable.Rows.Count <= 0 ? NodeQueryResult.Empty : new NodeQueryResult(this.GetBookmark(this.dataTable, mapping), this.dataTable.Rows.Count, this.TotalRecordCount, mapping.Fields);
  }

  /// <summary>
  /// Читает сведения об указанных элементах источника данных.
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    this.dataTable = this.GetDataTable(this.GetQueryParams(recordIds, mapping));
    return this.dataTable == null || this.dataTable.Rows.Count <= 0 ? NodeQueryResult.Empty : new NodeQueryResult(this.dataTable.Rows.Count, this.TotalRecordCount, mapping.Fields);
  }

  /// <summary>
  /// Возвращает запись, полученную из источника данных в результате выполнения запроса.
  /// </summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  protected override object[] GetFieldValues(int index) => this.dataTable.Rows[index].ItemArray;

  /// <summary>
  /// Выполняет запрос и возвращает таблицу с результатами его выполнения.
  /// </summary>
  /// <param name="queryParams">Параметры выполнения запроса</param>
  /// <returns>Таблица с результатами</returns>
  protected abstract DataTable GetDataTable(DBRecordSetParams queryParams);

  /// <summary>
  /// Возвращает параметры запроса для чтения следующей порции данных.
  /// </summary>
  /// <param name="bookmark">Позиция начала порции данных</param>
  /// <param name="count">Количество читаемых записей</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Параметры выполнения запроса</returns>
  protected virtual DBRecordSetParams GetQueryParams(
    object bookmark,
    int count,
    RecordMapping mapping)
  {
    DBRecordSetParams queryParams = this.GetQueryParams(mapping, true);
    if (bookmark != null)
    {
      DBRecordsBookmark dbRecordsBookmark = (DBRecordsBookmark) bookmark;
      queryParams.LastKeyValue = dbRecordsBookmark.KeyValue;
      queryParams.LastOrderValue = (object) dbRecordsBookmark.OrderValue;
    }
    queryParams.RecordCount = count;
    return queryParams;
  }

  /// <summary>
  /// Возвращает параметры запроса для чтения сведений об указанных элементах
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Параметры выполнения запроса</returns>
  protected virtual DBRecordSetParams GetQueryParams(object[] recordIds, RecordMapping mapping)
  {
    return this.GetQueryParams(mapping, false) with
    {
      LastKeyValue = 0,
      LastOrderValue = (object) null,
      RecordCount = -1
    };
  }

  protected abstract DBRecordSetParams GetQueryParams(RecordMapping mapping, bool withSortInfo);

  private object GetBookmark(DataTable dataTable, RecordMapping mapping)
  {
    if (!Convert.ToBoolean(this.dataTable.ExtendedProperties[(object) "Eof"]))
    {
      int columnIndex = Array.IndexOf<object>(mapping.Fields, this.keyField);
      if (columnIndex < 0 && mapping.Fields.Length != 0 && mapping.Fields[0] is NodeColumnID field)
        columnIndex = Array.IndexOf<object>(mapping.Fields, (object) new NodeColumnID(this.keyField, field.AttrSource));
      if (columnIndex >= 0)
      {
        DataRow row = this.dataTable.Rows[dataTable.Rows.Count - 1];
        long int64 = Convert.ToInt64(row[columnIndex]);
        List<object> orderValue = (List<object>) null;
        if (mapping.SortFields != null && Array.IndexOf<object>(mapping.Fields, mapping.SortFields[0]) >= 0)
        {
          orderValue = new List<object>(mapping.SortFields.Length);
          for (int index = 0; index < mapping.SortFields.Length; ++index)
            orderValue.Add(row[Array.IndexOf<object>(mapping.Fields, mapping.SortFields[index])]);
        }
        return (object) new DBRecordsBookmark(int64, orderValue);
      }
    }
    return (object) null;
  }
}
