
// Type: Intermech.Navigator.Queries.SavedInSnapshotDeletedInActualCompositionQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using Intermech.Navigator.VirtualColumns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Queries;

/// <summary>Класс для реализации всех запросов к данным о входимости друг в друга объектов, сохранённых в итерации</summary>
internal class SavedInSnapshotDeletedInActualCompositionQuery : 
  RelatedObjectsSavedInSnapshotQuery,
  INodeQuery,
  IContextAware,
  ISnapshotContext,
  IObjectInSnapshotContext,
  ICompareObjectsQuery
{
  /// <summary>Словарь "имя поля" =&gt; "NodeColumnID соотв. типа колонки таблицы"</summary>
  [NotNull]
  public new static readonly BiDirectDictionary<string, object> SnapshotDataTableColumns = new BiDirectDictionary<string, object>();
  /// <summary>Номер колонки в полученном наборе данных, в котором должен хранится результат сравнения составов</summary>
  protected int _CompareResultColumnNum = -1;
  /// <summary>Хэш гуидов связей актуального состава</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashActualPrjLinkGuids;
  /// <summary>Хэш гуидов связей элементов состава, сохранённого в итерации</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashSnapshotPrjLinkGuids;

  /// <summary>Статический конструктор</summary>
  static SavedInSnapshotDeletedInActualCompositionQuery()
  {
    SavedInSnapshotDeletedInActualCompositionQuery.SnapshotDataTableColumns.AddRange<string, object>((IEnumerable<KeyValuePair<string, object>>) RelatedObjectsSavedInSnapshotQuery.SnapshotDataTableColumns);
    SavedInSnapshotDeletedInActualCompositionQuery.SnapshotDataTableColumns.Add("F_COMPARE_RESULT", (object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT);
  }

  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  /// <param name="role">Роль объектов (состав или входимость), данные которых запрашиваются</param>
  public SavedInSnapshotDeletedInActualCompositionQuery(
    [NotNull] INodeQuerySupport support,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int objTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int relTypeId,
    [CanBeEmpty] int parentObjTypeID,
    [NotNull] ConditionStructure[] conditions,
    [NotNull] HashSet<Guid> hashActualPrjLinkGuids,
    [NotNull] HashSet<Guid> hashSnapshotPrjLinkGuids)
    : base(support, objectVersionID, objTypeID, role, relTypeId, parentObjTypeID, conditions)
  {
    this._hashActualPrjLinkGuids = hashActualPrjLinkGuids;
    this._hashSnapshotPrjLinkGuids = hashSnapshotPrjLinkGuids;
  }

  /// <summary>Полученный набор данных</summary>
  public DataTable DataTable
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.dataTable;
  }

  /// <summary>Номер колонки с идентификатором связи в полученном наборе данных</summary>
  public int PrjLinkIDColumnNum
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._PrjLinkIDColumnNum;
    }
  }

  /// <summary>Номер колонки в полученном наборе данных, в котором должен хранится результат сравнения составов</summary>
  public int CompareResultColumnNum
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._CompareResultColumnNum;
    }
  }

  /// <summary>True, если выборка выбирает данные из актуального состава, иначе - из сохранённого</summary>
  public bool ActualComposition
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => false;
  }

  /// <summary>Возвращает таблицу, содержащую результаты запроса. Базовый класс вызывает этот метод, чтобы получить результаты запроса в
  /// формате источника данных, а затем транслирует их в унифицированный формат, понятный навигатору.</summary>
  /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
  /// <returns>Таблица с значениями атрибутов объектов</returns>
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    DataTable dataTable = base.GetDataTable(queryParams);
    if (dataTable != null)
    {
      this._hashSnapshotPrjLinkGuids.AddRange<Guid>(dataTable.Rows.Cast<DataRow>().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuid(this._PrjLinkGuidColumnNum))));
      dataTable.DeleteRows((System.Func<DataRow, bool>) (dataRow => this._hashActualPrjLinkGuids.Contains(dataRow.FieldAsGuid(this._PrjLinkGuidColumnNum))));
      VirtualQueryResultColumn.AddVirtualColumns(dataTable, this.mapping, (System.Func<VirtualQueryResultColumn, object>) (virtualColumn => virtualColumn != CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT ? virtualColumn?.DefaultValue ?? (object) CompositionCompareResult.NotCompared : (object) CompositionCompareResult.Deleted));
      this._FieldsOrder = ((IEnumerable<object>) this._FieldsOrder).Append<object>((object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT).ToArray<object>(this._FieldsOrder.Length + 1);
      this._PrjLinkIDColumnNum = Array.FindIndex<object>(this._FieldsOrder, (Predicate<object>) (column => column == CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT));
      if (this._PrjLinkIDColumnNum == -1)
        throw new Exception("Can`t find F_COMPARE_RESULT field");
    }
    return dataTable;
  }

  /// <summary>Получение списка колонок в том порядке, в котором они должны быть расположены в таблице, являющейся
  /// результатом выполнения запроса</summary>
  protected override BiDirectDictionary<string, object> GetSnapshotDataTableColumns()
  {
    return SavedInSnapshotDeletedInActualCompositionQuery.SnapshotDataTableColumns;
  }

  /// <summary>Возвращает запись, полученную из источника данных в результате выполнения запроса.</summary>
  /// <param name="index">Порядковый номер записи в порции</param>
  /// <returns>Массив значений полей записи</returns>
  [CanBeNull]
  [ItemCanBeNull]
  protected override object[] GetFieldValues(int index)
  {
    object[] fieldValues1 = base.GetFieldValues(index);
    if (fieldValues1 == null)
      return (object[]) null;
    object[] fieldValues2 = new object[fieldValues1.Length + 1];
    fieldValues1.CopyTo((Array) fieldValues2, 0);
    fieldValues2[fieldValues2.Length - 1] = (object) CompositionCompareResult.Deleted;
    return fieldValues2;
  }
}
