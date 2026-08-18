
// Type: Intermech.Navigator.DBObjects.CompareWithSnapshotRelatedObjectsQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.DBObjects;

/// <summary>Выборка данных, использующаяся для выбора данных актуального состава в ноде сравнения составов</summary>
public class CompareWithSnapshotRelatedObjectsQuery : 
  AdvRelatedObjectsQuery,
  IContextAware,
  IFiltrateVersionsLogHolder,
  INodeQuery,
  ICompareObjectsQuery
{
  /// <summary>Номер колонки в полученном наборе данных, в котором должен хранятся результаты сравнения составов</summary>
  private int _compareResultColumnNum = -1;
  /// <summary>Хэш гуидов связей актуального состава</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashActualPrjLinkGuids;
  /// <summary>Хэш гуидов связей элементов состава, сохранённого в итерации</summary>
  private HashSet<Guid> _hashSnapshotPrjLinkGuids;

  /// <summary>Конструктор запроса, в результате выполнения которого будет прочитана информация о всех объектах, связанных с указанным
  /// объектом заданным типом связи и удовлетворяющих указанным условиям.</summary>
  /// <param name="support"></param>
  /// <param name="hashActualPrjLinkGuids">Хэш идентификаторов связей актуального состава</param>
  /// <param name="hashSnapshotPrjLinkGuids">Хэш идентификаторов связей элементов состава, сохранённого в итерации</param>
  /// <param name="objId">Идентификатор версии объекта</param>
  /// <param name="objTypeID"></param>
  /// <param name="role">Роль связанных объектов</param>
  /// <param name="relTypeId">Идентификатор типа связи</param>
  /// <param name="parentObjTypeID">Идентификатор родительского типа объектов для типизированного запроса в коллекцию связей</param>
  /// <param name="conditions">Массив условий, которым должны удовлетворять связанные объекты</param>
  public CompareWithSnapshotRelatedObjectsQuery(
    [NotNull] INodeQuerySupport support,
    [NotNull] HashSet<Guid> hashActualPrjLinkGuids,
    [NotNull] HashSet<Guid> hashSnapshotPrjLinkGuids,
    [NotEmpty] long objId,
    [NotEmpty] int objTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int relTypeId,
    [CanBeEmpty] int parentObjTypeID,
    [NotNull] ConditionStructure[] conditions)
    : base(support, objId, objTypeID, role, relTypeId, parentObjTypeID, conditions)
  {
    this._hashActualPrjLinkGuids = hashActualPrjLinkGuids;
    this._hashSnapshotPrjLinkGuids = hashSnapshotPrjLinkGuids;
  }

  /// <summary>Полученный набор данных</summary>
  [CanBeNull]
  public DataTable DataTable
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.dataTable;
  }

  /// <summary>Номер колонки с идентификатором связи в полученном наборе данных</summary>
  public int PrjLinkIDColumnNum
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._prjLinkIDColumnNum;
    }
  }

  /// <summary>Номер колонки с гуидом связи в полученном наборе данных</summary>
  public int PrjLinkGuidColumnNum
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._prjLinkGuidColumnNum;
    }
  }

  /// <summary>Номер колонки в полученном наборе данных, в котором должен хранится результат сравнения составов</summary>
  public int CompareResultColumnNum
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._compareResultColumnNum;
    }
  }

  /// <summary>True, если выборка выбирает данные из актуального состава, иначе - из сохранённого</summary>
  public bool ActualComposition
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => true;
  }

  /// <summary>Возвращает таблицу, содержащую результаты запроса. Базовый класс вызывает этот метод, чтобы получить результаты запроса в
  /// формате источника данных, а затем транслирует их в унифицированный формат, понятный навигатору.</summary>
  /// <exception cref="T:System.Exception">Thrown when an exception error condition occurs.</exception>
  /// <param name="queryParams">Параметры запроса к базе данных</param>
  /// <returns>Таблица с значениями атрибутов объектов</returns>
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    DataTable dataTable = base.GetDataTable(queryParams);
    if (dataTable != null)
      this._hashActualPrjLinkGuids.AddRange<Guid>((IEnumerable<Guid>) dataTable.AsEnumerable().Select<DataRow, Guid>((System.Func<DataRow, Guid>) (dataRow => dataRow.FieldAsGuid(this._prjLinkGuidColumnNum))));
    this.mapping.CheckFieldIndex(ref this._compareResultColumnNum, CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT, "F_COMPARE_RESULT");
    return dataTable;
  }
}
