
// Type: Intermech.Navigator.Queries.RelatedObjectsSavedInSnapshotQuery
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace Intermech.Navigator.Queries;

/// <summary>Класс для реализации всех запросов к данным о входимости друг в друга объектов, сохранённых в итерации</summary>
internal class RelatedObjectsSavedInSnapshotQuery : 
  RelatedObjectsQuery,
  IContextAware,
  IFiltrateVersionsLogHolder,
  INodeQuery,
  ISnapshotContext,
  IObjectInSnapshotContext
{
  /// <summary>Словарь "имя поля" =&gt; "NodeColumnID соотв. типа колонки таблицы"</summary>
  [NotNull]
  public static readonly BiDirectDictionary<string, object> SnapshotDataTableColumns = new BiDirectDictionary<string, object>()
  {
    {
      "F_ID",
      (object) ObjectsPartBase.ncF_ID
    },
    {
      "F_OBJECT_ID",
      (object) ObjectsPartBase.ncF_OBJECT_ID
    },
    {
      "F_OBJECT_TYPE",
      (object) ObjectsPartBase.ncF_OBJECT_TYPE
    },
    {
      "CAPTION",
      (object) ObjectsPartBase.ncCAPTION
    },
    {
      "F_LC_STEP",
      (object) ObjectsPartBase.ncF_LC_STEP
    },
    {
      "F_OWNER_ID",
      (object) ObjectsPartBase.ncOWNER
    },
    {
      "F_VERSION_ID",
      (object) ObjectsPartBase.ncVERSION
    },
    {
      "F_SITE_ID",
      (object) ObjectsPartBase.ncSITE_ID
    },
    {
      "F_MODIFICATION_ID",
      (object) ObjectsPartBase.ncMODIFICATION_ID
    },
    {
      "F_PRJLINK_ID",
      (object) RelatedPartBase.ncF_PRJLINK_ID
    },
    {
      "F_PROJ_ID",
      (object) RelatedPartBase.ncF_PROJ_ID
    },
    {
      "F_RELATION_TYPE",
      (object) RelatedPartBase.ncF_RELATION_TYPE
    },
    {
      "F_PRJ_GUID",
      (object) RelatedPartBase.ncF_PRJ_GUID
    }
  };
  /// <summary>Порядок полей в таблице данных. Инициализируется в момент первого получения таблицы и остаётся неизменным, т.к. таблица
  /// всегда одна и та же</summary>
  [CanBeNull]
  [ItemNotNull]
  protected object[] _FieldsOrder;
  /// <summary>Источник таблицы данных с составом объекта, сохранённом в итерации (без фильтрации по типам связей)</summary>
  public IDataTableSource CompositionDataTableSource;
  /// <summary>Номер колонки с идентификатором связи в полученном наборе данных</summary>
  protected internal int _PrjLinkIDColumnNum = -1;
  /// <summary>Номер колонки с гуидом связи в полученном наборе данных</summary>
  protected internal int _PrjLinkGuidColumnNum = -1;
  /// <summary>Номер колонки с идентификатором типа связи в полученном наборе данных</summary>
  protected internal int _PrjRelTypeIDColumnNum = -1;
  [CanBeNull]
  private ISnapshot _snapshot;

  /// <summary>Конструктор</summary>
  /// <param name="support">Контекст</param>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="role">Роль объектов (состав или входимость), данные которых запрашиваются</param>
  /// <param name="relTypeId">Тип связи</param>
  /// <param name="parentObjTypeID">Тип вышестоящего объекта</param>
  /// <param name="conditions"></param>
  public RelatedObjectsSavedInSnapshotQuery(
    [NotNull] INodeQuerySupport support,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int objTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int relTypeId,
    [NotEmpty] int parentObjTypeID,
    [NotNull] ConditionStructure[] conditions)
    : base(support, objectVersionID, objTypeID, role, relTypeId, parentObjTypeID, conditions)
  {
    this.mapping.GetFields = (Func<object[]>) (() => this._FieldsOrder ?? new object[0]);
  }

  /// <summary>Метод получения таблицы данных</summary>
  /// <returns>Таблица данных, являющаяся результатом запроса</returns>
  [CanBeNull]
  protected override DataTable GetDataTable(DBRecordSetParams queryParams)
  {
    Intermech.Diagnostics.Check.NotNull<IDataTableSource>(this.CompositionDataTableSource, "CompositionDataTableSource");
    DataTable compositionDataTable = this.CompositionDataTableSource.DataTable;
    LazyInitializer.EnsureInitialized<object[]>(ref this._FieldsOrder, (Func<object[]>) (() => this.GetRealFieldsOrder(compositionDataTable)));
    this._PrjLinkIDColumnNum = Array.FindIndex<object>(this._FieldsOrder, (Predicate<object>) (column => column == RelatedPartBase.ncF_PRJLINK_ID));
    if (this._PrjLinkIDColumnNum == -1)
      throw new Exception("Can`t find F_PRJLINK_ID field");
    this._PrjLinkGuidColumnNum = Array.FindIndex<object>(this._FieldsOrder, (Predicate<object>) (column => column == RelatedPartBase.ncF_PRJ_GUID));
    if (this._PrjLinkGuidColumnNum == -1)
      throw new Exception("Can`t find F_PRJ_GUID field");
    this._PrjRelTypeIDColumnNum = Array.FindIndex<object>(this._FieldsOrder, (Predicate<object>) (column => column == RelatedPartBase.ncF_RELATION_TYPE));
    if (this._PrjRelTypeIDColumnNum == -1)
      throw new Exception("Can`t find F_RELATION_TYPE field");
    DataRow[] array = compositionDataTable.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (dataRow => Convert.ToInt32(dataRow.ItemArray[this._PrjRelTypeIDColumnNum]) == this.relTypeId)).ToArray<DataRow>();
    compositionDataTable = array.Length != 0 ? ((IEnumerable<DataRow>) array).CopyToDataTable<DataRow>() : compositionDataTable.Clone();
    return compositionDataTable;
  }

  /// <summary>Получение списка колонок в том порядке, в котором они должны быть расположены в таблице, являющейся
  /// результатом выполнения запроса</summary>
  [NotNull]
  protected virtual BiDirectDictionary<string, object> GetSnapshotDataTableColumns()
  {
    return RelatedObjectsSavedInSnapshotQuery.SnapshotDataTableColumns;
  }

  /// <summary>Создаёт массив NodeColumnID в котором перечисленны колонки в том порядке, в котором они реально расположены в таблице данных</summary>
  /// <param name="dataTable">Набор данных</param>
  /// <returns>массив NodeColumnID в котором перечисленны колонки в том порядке, в котором они реально расположены в таблице данных</returns>
  [NotNull]
  [ItemNotNull]
  private object[] GetRealFieldsOrder([NotNull] DataTable dataTbl)
  {
    BiDirectDictionary<string, object> snapshotDataTableColumns = this.GetSnapshotDataTableColumns();
    return dataTbl.Columns.Cast<DataColumn>().Select<DataColumn, object>((System.Func<DataColumn, object>) (dataColumn => snapshotDataTableColumns[dataColumn.ColumnName])).ToArray<object>(dataTbl.Columns.Count);
  }

  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  public ISnapshot Snapshot
  {
    [DebuggerStepThrough] get => this.Services.EnsureInitialized<ISnapshot>(ref this._snapshot);
  }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this.Snapshot.ID;

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectVersionID => this.objId;
}
