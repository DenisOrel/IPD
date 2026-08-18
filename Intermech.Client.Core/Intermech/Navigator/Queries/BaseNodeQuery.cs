
// Type: Intermech.Navigator.Queries.BaseNodeQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Navigator.Queries;

/// <summary>
/// Базовый класс для реализации всех запросов к источникам данных.
/// </summary>
/// <remarks>
/// Хотя в описании интерфейса INodeQuery используются типы NodeColumn и INodeID, сам
/// класс ими не оперирует. Такие операции совершаются с помощью объекта, реализующего
/// интерфейс INodeQuerySupport.
/// </remarks>
public abstract class BaseNodeQuery : INodeQuery
{
  protected RecordMapping mapping;
  protected bool executed;
  protected NodeQueryResult result;
  protected RecordAdapter adapter;
  protected NodeQueryOptions options;

  /// <summary>Создает запрос.</summary>
  public BaseNodeQuery()
  {
    this.mapping = new RecordMapping();
    this.adapter = (RecordAdapter) null;
    this.executed = false;
    this.options = NodeQueryOptions.None;
    this.result = NodeQueryResult.Empty;
  }

  /// <summary>
  /// Добавляет колонку, значение которой должно быть получено в
  /// результате выполнения запроса. Дополнительно может быть указано
  /// преобразование, которое должно быть применено к содержимому колонки.
  /// Если преобразовывать содержимое колонки не требуется, то в качестве
  /// преобразования следует указать null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <param name="transform">Преобразование содержимого колонки</param>
  public virtual void AddColumn(NodeColumn column, INodeColumnTransform transform)
  {
    this.CheckNotExecuted(nameof (AddColumn));
    this.mapping.RegisterColumn(column, this.Support.MapColumnToField(column), transform);
  }

  /// <summary>
  /// Выполняет запрос на чтение порции дочерних элементов. Позиция для
  /// чтения определяется закладкой (bookmark). Если закладка = null,
  /// то будет прочитана первая порция, иначе будет прочитана порция с
  /// позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  public void Execute(object bookmark, int count)
  {
    this.CheckNotExecuted(nameof (Execute));
    try
    {
      if (count == 0)
        return;
      this.RegisterSpecialFields();
      this.result = this.Execute(bookmark, count, this.mapping);
      if (this.result.RecordCount <= 0)
        return;
      this.adapter = this.CreateRecordAdapter(this.mapping, this.result.FieldsOrder);
    }
    finally
    {
      this.executed = true;
    }
  }

  /// <summary>
  /// Выполняет запрос на чтение значений колонок для указанных
  /// дочерних элементов. Этот метод используется навигатором при
  /// операциях обновления содержимого дерева и других элементов
  /// визуального интерфейса.
  /// </summary>
  /// <param name="nodeIDs">Коллекция идентификаторов дочерних элементов</param>
  public void Execute(NodeIDCollection nodeIDs)
  {
    this.CheckNotExecuted(nameof (Execute));
    try
    {
      if (nodeIDs == null || nodeIDs.Count <= 0)
        return;
      this.RegisterSpecialFields();
      this.result = this.Execute(this.GetRecordIds(nodeIDs), this.mapping);
      if (this.result.RecordCount <= 0)
        return;
      this.adapter = this.CreateRecordAdapter(this.mapping, this.result.FieldsOrder);
    }
    finally
    {
      this.executed = true;
    }
  }

  /// <summary>
  /// Возвращает закладку, определяющую позицию для чтения следующей
  /// порции дочерних элементов или null, если была прочитана
  /// последняя порция.
  /// </summary>
  public object Bookmark => this.result.Bookmark;

  /// <summary>
  /// Возвращает количество прочитанных в результате выполнения
  /// запроса дочерних элементов.
  /// </summary>
  public int RecordCount => this.result.RecordCount;

  /// <summary>Условия выполнения запросов</summary>
  public NodeQueryOptions Options
  {
    get => this.options;
    set => this.options = value;
  }

  /// <summary>
  /// Возвращает количество всех элементов, которые могут быть получены с помощью данного запроса.
  /// Значение свойства будет определено только после первого пакетного чтения, при условии, что
  /// в опциях задан флажок ReceiveTotalRecordsCount. Иначе свойство будет равно значению RecordCount.
  /// </summary>
  public long TotalRecordCount => this.result.TotalCount;

  /// <summary>
  /// Возвращает идентификатор дочернего элемента по его порядковому
  /// номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  public INodeID GetRecordNodeID(int index)
  {
    this.CheckExecuted(nameof (GetRecordNodeID));
    this.CheckRecordIndex(index);
    return this.Support.CreateNodeId(this.GetFieldValues(index), this.adapter);
  }

  /// <summary>
  /// Возвращает значения колонок дочернего элемента по его
  /// порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив значений колонок</returns>
  public object[] GetRecordValues(int index)
  {
    this.CheckExecuted(nameof (GetRecordValues));
    this.CheckRecordIndex(index);
    return this.adapter.GetRecordValues(this.GetFieldValues(index));
  }

  /// <summary>
  /// Возвращает исходные значения колонок дочернего элемента по его
  /// порядковому номеру.
  /// </summary>
  /// <param name="index">Порядковый номер дочернего элемента</param>
  /// <returns>Массив исходных значений колонок</returns>
  public object[] GetRawRecordValues(int index)
  {
    this.CheckExecuted("GetRecordValues");
    this.CheckRecordIndex(index);
    return this.adapter.GetRawRecordValues(this.GetFieldValues(index));
  }

  /// <summary>
  /// Возвращает объект, помогающий подготовить запрос к выполнению и обработать
  /// его результаты.
  /// </summary>
  protected abstract INodeQuerySupport Support { get; }

  /// <summary>Читает следующую порцию данных.</summary>
  /// <param name="bookmark">Позиция начала порции данных</param>
  /// <param name="count">Количество читаемых записей</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected abstract NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping);

  /// <summary>
  /// Читает сведения об указанных элементах источника данных.
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected abstract NodeQueryResult Execute(object[] recordIds, RecordMapping mapping);

  /// <summary>
  /// Возвращает запись, полученную из источника данных в результате выполнения запроса.
  /// </summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  protected abstract object[] GetFieldValues(int index);

  private void CheckNotExecuted(string methodName)
  {
    if (this.executed)
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString(sc_4296.ssp_imclient_4297()), (object) methodName));
  }

  private void CheckExecuted(string methodName)
  {
    if (!this.executed)
      throw new InvalidOperationException(string.Format(LocalizationHolder.rm.GetString("Client.Core_634"), (object) methodName));
  }

  private void CheckRecordIndex(int index)
  {
    if (index < 0 || index >= this.result.RecordCount)
      throw new IndexOutOfRangeException();
  }

  /// <summary>
  /// Регистрирует имена полей источника данных, которые обязательно должны быть
  /// получены в результате выполнения запроса. Такие поля используются для создания
  /// унифицированных идентификаторов элементов навигации.
  /// </summary>
  private void RegisterSpecialFields()
  {
    List<object> specialFields = this.Support.GetSpecialFields();
    for (int index = 0; index < specialFields.Count; ++index)
      this.mapping.RegisterSpecialField(specialFields[index]);
  }

  /// <summary>Виртуальный метод создания адаптера для преобразования результатов, полученных у источника данных, к пригодному для
  /// использования виду.</summary>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <param name="fieldsOrder"></param>
  /// <returns>The new record adapter</returns>
  protected virtual RecordAdapter CreateRecordAdapter(RecordMapping mapping, object[] fieldsOrder)
  {
    return new RecordAdapter(mapping, fieldsOrder);
  }

  /// <summary>
  /// Преобразует коллекцию унифицированных идентификаторов элементов навигации в
  /// массив идентификаторов элементов источника данных. Этот массив затем используется
  /// для создания условия запроса, позволяющего получить информацию только об указанных
  /// элементах источника данных.
  /// </summary>
  /// <param name="nodeIds">Коллекция унифицированных идентификаторов элементов навигации</param>
  /// <returns>Массив индентификаторов элементов источника данных</returns>
  private object[] GetRecordIds(NodeIDCollection nodeIds)
  {
    object[] recordIds = new object[nodeIds.Count];
    for (int index = 0; index < nodeIds.Count; ++index)
      recordIds[index] = this.Support.CreateRecordId(nodeIds[index]);
    return recordIds;
  }

  /// <summary>Позволяет отсортировать таблицу на клиенте</summary>
  protected virtual DataTable FilterTable(RecordMapping mapping, DataTable resultTable)
  {
    if (resultTable == null)
      return (DataTable) null;
    if (mapping == null || mapping.SortFields == null || mapping.SortFields.Length == 0)
      return resultTable;
    List<string> needDeleted = new List<string>();
    StringBuilder sortString = new StringBuilder();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    for (int index1 = 0; index1 < mapping.SortFields.Length; ++index1)
    {
      if (mapping.SortFields[index1] is NodeColumnID)
      {
        NodeColumnSortOrder sortOrder = mapping.SortOrders[index1];
        NodeColumnID sortField = (NodeColumnID) mapping.SortFields[index1];
        if (sortField.AttributeID != 0)
        {
          bool flag = false;
          int num = -1;
          for (int index2 = 0; index2 < mapping.Fields.Length; ++index2)
          {
            if (((NodeColumnID) mapping.Fields[index2]).AttributeID == sortField.AttributeID)
            {
              IDBAttributeTypeInfo attributeType = service.GetAttributeType(sortField.AttributeID);
              string sourceColumn = index2.ToString();
              if (attributeType.AttributeType == FieldTypes.ftMeasured)
              {
                DataTableSortHelper.GetMeasuredColumnFilter(resultTable, sortString, sourceColumn, needDeleted, sortOrder);
                flag = true;
                break;
              }
              if (attributeType.GUID == new Guid("cad00270-306c-11d8-b4e9-00304f19f545"))
              {
                DataTableSortHelper.GetPositionColumnFilter(resultTable, sortString, sourceColumn, needDeleted, sortOrder);
                flag = true;
                break;
              }
              num = index2;
              break;
            }
          }
          if (!flag)
            sortString.Append($"[{num}] {(sortOrder == NodeColumnSortOrder.Ascending ? (object) " ASC" : (object) " DESC")},");
        }
      }
    }
    if (sortString.Length > 1)
      sortString.Remove(sortString.Length - 1, 1);
    DataRow[] fromRows = resultTable.Select(string.Empty, sortString.ToString());
    DataTable toTable = resultTable.Clone();
    DataSetProcessor.AssignRows(toTable, (IEnumerable<DataRow>) fromRows);
    foreach (string name in needDeleted)
      toTable.Columns.Remove(name);
    toTable.AcceptChanges();
    return toTable;
  }
}
