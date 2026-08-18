
// Type: Intermech.Navigator.Snapshots.SnapshotsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Navigator.Snapshots;

public class SnapshotsQuery : BaseNodeQuery
{
  /// <summary>Подготовка запроса к выполнению</summary>
  private INodeQuerySupport support;
  /// <summary>
  /// Версия объекта (выделена в дереве навигатора.
  /// именно в эту версию при случае будем восстанавливать итерации)
  /// </summary>
  private long objectID;
  /// <summary>id объекта, для которого будем показывать итерации</summary>
  private long id;
  /// <summary>Поля для сортировки</summary>
  public static readonly object[] FieldsOrder = new object[1]
  {
    (object) "F_SNAPSHOT_ID"
  };
  private DataTable snapshotsTable = new DataTable();
  private string _asc = " ASC";
  private string _desc = " DESC";

  public SnapshotsQuery(long objectID, long id, INodeQuerySupport support)
  {
    this.support = support;
    this.objectID = objectID;
    this.id = id;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBSnapshotCollection snapshotCollection = sessionKeeper.Session.GetSnapshotCollection();
      if (SnapshotsView.Mode == SnapshotMode.ObjectVersion)
      {
        this.snapshotsTable = snapshotCollection.GetObjectVersionSnapshots(-SnapshotsView.SelObjectID, "F_SNAPSHOT_ID");
        this.snapshotsTable.Merge(snapshotCollection.GetObjectVersionSnapshots(SnapshotsView.SelObjectID, "F_SNAPSHOT_ID"));
      }
      else
        this.snapshotsTable = snapshotCollection.GetObjectSnapshots(id, "F_SNAPSHOT_ID");
    }
  }

  /// <summary>
  /// Возвращает объект, помогающий подготовить запрос к выполнению и обработать
  /// его результаты.
  /// </summary>
  protected override INodeQuerySupport Support => this.support;

  /// <summary>
  /// Выполняет запрос на чтение порции дочерних элементов. Позиция для
  /// чтения определяется закладкой (bookmark). Если закладка = null,
  /// то будет прочитана первая порция, иначе будет прочитана порция с
  /// позиции, указанной в закладке.
  /// </summary>
  /// <param name="bookmark">Закладка, указывающая позицию для чтения</param>
  /// <param name="count">Количество записей в порции.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object bookmark, int count, RecordMapping mapping)
  {
    this.snapshotsTable.DefaultView.Sort = this.GetSortOrder(mapping);
    return new NodeQueryResult(this.snapshotsTable.Rows.Count, this.TotalRecordCount, this.GetFieldsOrder(this.snapshotsTable));
  }

  private string GetSortOrder(RecordMapping mapping)
  {
    if (mapping.SortFields == null)
      return string.Empty;
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < mapping.SortFields.Length; ++index)
    {
      if (index > 0)
        stringBuilder.Append(',');
      if (mapping.SortFields[index].ToString() == SnapshotConsts.SNAPSHOT_ID.ToString())
        stringBuilder.Append("F_SNAPSHOT_ID");
      else if (mapping.SortFields[index].ToString() == SnapshotConsts.SNAPSHOT_DATE.ToString())
        stringBuilder.Append("F_SNAPSHOT_DATE");
      else if (mapping.SortFields[index].ToString() == SnapshotConsts.F_NAME.ToString())
        stringBuilder.Append("F_NAME");
      else
        stringBuilder.Append(mapping.SortFields[index].ToString());
      stringBuilder.Append(mapping.SortOrders[index] == NodeColumnSortOrder.Ascending ? this._asc : this._desc);
    }
    return stringBuilder.ToString();
  }

  private object[] GetFieldsOrder(DataTable dataTable)
  {
    return new List<object>(dataTable.Columns.Count)
    {
      (object) SnapshotConsts.SNAPSHOT_ID,
      (object) SnapshotConsts.F_NAME,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.F_ID,
      (object) ObligatoryObjectAttributes.F_USER_ID,
      (object) SnapshotConsts.SNAPSHOT_DATE,
      (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
    }.ToArray();
  }

  /// <summary>
  /// Читает сведения об указанных элементах источника данных.
  /// </summary>
  /// <param name="recordIds">Идентификаторы элементов источника данных.</param>
  /// <param name="mapping">Схема отображения виртуальных колонок</param>
  /// <returns>Описатель результата выполнения запроса</returns>
  protected override NodeQueryResult Execute(object[] recordIds, RecordMapping mapping)
  {
    return new NodeQueryResult(this.snapshotsTable.Rows.Count, this.TotalRecordCount, mapping.Fields);
  }

  /// <summary>
  /// Возвращает запись, полученную из источника данных в результате выполнения запроса.
  /// </summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  protected override object[] GetFieldValues(int index)
  {
    return this.snapshotsTable.DefaultView[index].Row.ItemArray;
  }
}
