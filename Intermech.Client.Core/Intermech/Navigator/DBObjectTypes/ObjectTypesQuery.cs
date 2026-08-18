
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Реализует запрос к базе данных, возвращающий типы объектов, производных от
/// указанного типа. Результаты запроса возвращаются в унифицированном формате,
/// воспринимаемом навигатором, т.е. для каждого типа объекта предоставляется
/// его идентификатор, поддерживающий интерфейс INodeID, и значения указанных
/// виртуальных колонок.
/// </summary>
public class ObjectTypesQuery : BaseNodeQuery
{
  /// <summary>Контейнер сервисов</summary>
  private readonly IServiceProvider serviceProvider;
  /// <summary>Вспомогательный интерфейс для подготовки запросов</summary>
  private readonly INodeQuerySupport support;
  /// <summary>
  /// Тип объекта базы данны, для которого запрос должен вернуть информацию о
  /// производных от него типах.
  /// </summary>
  private readonly int parentObjTypeId;
  /// <summary>
  /// Массив записей из таблицы, полученной в результате запроса. Данные в ней
  /// представлены в том фомате, в котором их вернул сервер. Они будут
  /// использоваться для преобразования в унифицированный формат, понятный
  /// навигатору. Само преобразование будет выполняться методами базового
  /// класса.
  /// </summary>
  private List<DataRow> rows;
  private const string _asc = " ASC";
  private const string _desc = " DESC";

  /// <summary>
  /// Конструктор, позволяющий указать идентификатор типа объекта, для
  /// которого запрос должен вернуть производные типы. Если идентификатор
  /// равен -1, то запрос вернет типы объектов, находящиеся на верхнем
  /// уровне иерархии.
  /// </summary>
  /// <param name="support">Объект, помогающий подготовить запрос к выполнению</param>
  /// <param name="parentObjTypeId">Идентификатор типа объекта</param>
  public ObjectTypesQuery(INodeQuerySupport support, int parentObjTypeId)
  {
    this.support = support;
    this.parentObjTypeId = parentObjTypeId;
  }

  /// <summary>
  /// Конструктор, позволяющий указать идентификатор типа объекта, для
  /// которого запрос должен вернуть производные типы. Если идентификатор
  /// равен -1, то запрос вернет типы объектов, находящиеся на верхнем
  /// уровне иерархии.
  /// </summary>
  /// <param name="support">Объект, помогающий подготовить запрос к выполнению</param>
  /// <param name="parentObjTypeId">Идентификатор типа объекта</param>
  /// <param name="serviceProvider">Контейнер сервисов для дополнительной фильтрации результатов запросов</param>
  public ObjectTypesQuery(
    INodeQuerySupport support,
    int parentObjTypeId,
    IServiceProvider serviceProvider)
  {
    this.support = support;
    this.parentObjTypeId = parentObjTypeId;
    this.serviceProvider = serviceProvider;
  }

  /// <summary>
  /// Выполняет запрос на чтение первой/очередной порции информации. Позиция
  /// для чтения указывается с помощью закладки. Если она равна null, то
  /// будет прочитана первая порция, иначе - с позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, определяющая позицию для чтения</param>
  /// <param name="count">Количество читаемых записей</param>
  /// <param name="mapping">Схема отображения виртуальных колонок навигатора в поля базы данных</param>
  /// <returns>Объект-результат запроса, используемый базовым классом</returns>
  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    DataTable dataTable = this.GetDataTable(this.GetSortOrder(mapping));
    if (dataTable.Rows.Count > 0)
    {
      int position1 = bookmark != null ? (bookmark as PositionBookmark).Position : 0;
      if (position1 + count > dataTable.Rows.Count)
        count = dataTable.Rows.Count - position1;
      if (count > 0)
      {
        this.rows = new List<DataRow>();
        for (int index = 0; index < count; ++index)
          this.rows.Add(dataTable.Rows[position1 + index]);
        int position2 = position1 + count;
        return new NodeQueryResult(position2 < dataTable.Rows.Count ? (object) new PositionBookmark(position2) : (object) (PositionBookmark) null, count, this.TotalRecordCount, this.GetFieldsOrder(dataTable));
      }
    }
    return NodeQueryResult.Empty;
  }

  /// <summary>
  /// Выполняет запрос на чтение информации об указанных производных типах
  /// объектов. Интересующие типы идентифицируются с помощью массива
  /// унифицированных идентификаторов
  /// </summary>
  /// <param name="recordIds">?Массив унифицированных идентификаторов</param>
  /// <param name="mapping">Схема отображения виртуальных колонок навигатора в поля базы данных</param>
  /// <returns>Объект-результат запроса, используемый базовым классом</returns>
  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    DataTable dataTable = this.GetDataTable(string.Empty);
    if (dataTable.Rows.Count > 0)
    {
      object[] fieldsOrder = this.GetFieldsOrder(dataTable);
      int columnIndex = Array.IndexOf<object>(fieldsOrder, (object) "F_OBJECT_TYPE");
      this.rows = new List<DataRow>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        DataRow row = dataTable.Rows[index];
        int int32 = Convert.ToInt32(row[columnIndex]);
        if (Array.IndexOf<object>(recordIds, (object) int32) >= 0)
          this.rows.Add(row);
      }
      if (this.rows.Count > 0)
        return new NodeQueryResult(this.rows.Count, this.TotalRecordCount, fieldsOrder);
    }
    return NodeQueryResult.Empty;
  }

  /// <summary>
  /// Возвращает значения полей записи с указанным порядковым номером.
  /// Проверять валидность порядкового номера не нужно, т.к. это
  /// делается в базовом классе.
  /// </summary>
  /// <param name="index">Порядковый номер записи</param>
  /// <returns>Массив значений полей записи</returns>
  protected override object[] GetFieldValues(int index) => this.rows[index].ItemArray;

  protected override INodeQuerySupport Support => this.support;

  /// <summary>
  /// Возвращает строку условий сортировки, которая будет использоваться
  /// при выполнении запроса в базу данных. Поля, по которым выполняется
  /// сортировка, и направления сортировки берутся из схемы отображения.
  /// </summary>
  /// <param name="mapping">Схема отображения виртуальных колонок в поля базы данных</param>
  /// <returns>Строка условий сортировки</returns>
  private string GetSortOrder(RecordMapping mapping)
  {
    if (mapping.SortFields == null)
      return "F_OBJ_TYPE_NAME ASC";
    StringBuilder stringBuilder = new StringBuilder(mapping.SortFields.Length * 32 /*0x20*/);
    stringBuilder.Append((string) mapping.SortFields[0]);
    stringBuilder.Append(mapping.SortOrders[0] == NodeColumnSortOrder.Ascending ? " ASC" : " DESC");
    for (int index = 1; index < mapping.SortFields.Length; ++index)
    {
      stringBuilder.Append(',');
      stringBuilder.Append((string) mapping.SortFields[index]);
      stringBuilder.Append(mapping.SortOrders[index] == NodeColumnSortOrder.Ascending ? " ASC" : " DESC");
    }
    return stringBuilder.ToString();
  }

  /// <summary>
  /// Возвращает порядок следования полей базы данных в таблице, полученной
  /// с сервера, в виде массива идентификаторов полей. Эти сведения
  /// необходимы базовому классу для правильной трансформации
  /// результатов запроса в унифицированный формат, понятный навигатору.
  /// </summary>
  /// <param name="dataTable">Таблица с записями, полученная с сервера</param>
  /// <returns>Массив имен полей в порядке их следования в таблице</returns>
  private object[] GetFieldsOrder(DataTable dataTable)
  {
    object[] fieldsOrder = new object[dataTable.Columns.Count];
    for (int index = 0; index < fieldsOrder.Length; ++index)
      fieldsOrder[index] = (object) dataTable.Columns[index].ColumnName;
    return fieldsOrder;
  }

  /// <summary>
  /// Выполняет запрос в базу данных и возвращает таблицу, содержащую
  /// сведения о типа объектов, производных от указанного типа.
  /// </summary>
  /// <param name="sortOrder">Строка условий сортировки</param>
  /// <returns>Таблица с записями, полученная с сервера</returns>
  private DataTable GetDataTable(string sortOrder)
  {
    IDBObjectTypeInfoCollection objectTypeCollection = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(this.parentObjTypeId, true);
    IObjectTypeNodeFilter service = this.serviceProvider != null ? this.serviceProvider.GetService(typeof (IObjectTypeNodeFilter)) as IObjectTypeNodeFilter : (IObjectTypeNodeFilter) null;
    List<int> intList = new List<int>();
    if (service != null && service.EnabledObjectTypes != null)
    {
      intList.AddRange((IEnumerable<int>) service.EnabledObjectTypes);
      foreach (int enabledObjectType in service.EnabledObjectTypes)
        intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeParentsID(enabledObjectType));
    }
    DataTable dataTable1 = objectTypeCollection.Select(sortOrder);
    if (service != null && dataTable1 != null && (service.DisabledObjectTypes.Count > 0 || service.EnabledObjectTypes.Count > 0))
    {
      DataTable dataTable2 = dataTable1.Clone();
      int count = dataTable1.Columns.Count;
      object[] buffer = new object[count];
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
      {
        int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"].ToString());
        if (!service.DisabledObjectTypes.Contains(int32) && (service.EnabledObjectTypes.Count <= 0 || intList.Contains(int32)))
        {
          DataSetProcessor.CopyDataToBuffer(row, buffer, count);
          dataTable2.Rows.Add(buffer);
        }
      }
      dataTable2.AcceptChanges();
      dataTable1 = dataTable2;
    }
    return dataTable1;
  }
}
