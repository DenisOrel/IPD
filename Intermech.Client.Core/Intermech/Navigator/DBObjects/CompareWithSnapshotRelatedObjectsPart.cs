
// Type: Intermech.Navigator.DBObjects.CompareWithSnapshotRelatedObjectsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Snapshots;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjects;

/// <summary>Часть содержимого объекта, входящего в состав другого объекта сравниваемый с составом, сохранённым в итерации</summary>
internal class CompareWithSnapshotRelatedObjectsPart : 
  RelatedObjectsPart,
  INodePart,
  INodeItems,
  INodeQuerySupport,
  IContextAware,
  ISnapshotContext,
  IObjectInSnapshotContext
{
  /// <summary>Интерфейс итерации</summary>
  [CanBeNull]
  private ISnapshot _snapshot;
  /// <summary>Хэш гуидов связей актуального состава</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashActualPrjLinkGuid;
  /// <summary>Хэш гуидов связей элементов состава, сохранённого в итерации</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashSnapshotPrjLinkGuids;

  /// <summary>Конструктор</summary>
  /// <param name="services">Контекст</param>
  /// <param name="hashActualPrjLinkGuids">Хэш гуидов связей актуального состава</param>
  /// <param name="hashSnapshotPrjLinkGuids">Хэш гуидов связей элементов состава, сохранённого в итерации</param>
  /// <param name="objectVersionID">Версия объекта</param>
  /// <param name="relTypeID">Тип связи</param>
  /// <param name="objTypeID">Тип объекта</param>
  public CompareWithSnapshotRelatedObjectsPart(
    [NotNull] IServiceProvider services,
    [NotNull] HashSet<Guid> hashActualPrjLinkGuids,
    [NotNull] HashSet<Guid> hashSnapshotPrjLinkGuids,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relTypeID,
    [CanBeEmpty] int objTypeID = 0)
    : base(objTypeID != 0 ? objTypeID : Repository.ObjectVersions.GetObjectType(objectVersionID), objectVersionID, RelatedObjectsRole.Composition, relTypeID, services)
  {
    this._hashActualPrjLinkGuid = hashActualPrjLinkGuids;
    this._hashSnapshotPrjLinkGuids = hashSnapshotPrjLinkGuids;
  }

  /// <summary>Получить список служебных полей (которые загружаются в узел независимо от настройки вида)</summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  [NotNull]
  [ItemNotNull]
  public override List<object> GetSpecialFields()
  {
    List<object> collection = base.GetSpecialFields() ?? new List<object>();
    collection.SafeAdd<object>((object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT);
    return collection;
  }

  /// <summary>Отразить колонку "Навигатора" на поле</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Поле</returns>
  public override object MapColumnToField([NotNull] NodeColumn column)
  {
    return column.SchemeGuid.Equals(SnapshotConsts.SNAPSHOT_SCHEME_GUID) && column.ID != null && column.ID.Equals((object) SnapshotConsts.F_COMPARE_RESULT) ? (object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT : base.MapColumnToField(column);
  }

  /// <summary>Создание идентификатора ноды из подготовленный структуры с параметрами онного</summary>
  [NotNull]
  protected override INodeID CreateObjectNodeIdFromParams(
    [NotNull, ItemCanBeNull] object[] fieldValues,
    [NotNull] RecordAdapter adapter,
    [NotNull] CreateObjectNodeParams createObjectNodeParams)
  {
    object fieldValue = fieldValues[adapter.GetFieldIndex((object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT)];
    object obj;
    return (INodeID) new CompareWithSnapshotObjectNodeID(createObjectNodeParams, this.SnapshotID, !((obj = fieldValue) is CompositionCompareResult) ? CompositionCompareResult.NotCompared : (CompositionCompareResult) obj);
  }

  /// <summary>Создает элемент пространства навигации, представляющий указанный с помощью унифицированного идентификатора объект базы данных,
  /// и возвращает ссылку на основной интерфейс элемента.</summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Ссылка на основной интерфейс элемента</returns>
  [NotNull]
  public override INode GetChild([NotNull] INodeID nodeID)
  {
    return (INode) new CompareWithSnapshotObjectNode(nodeID as ICompareWithSnapshotObjectNodeID);
  }

  /// <summary>Создание объекта запроса с целью перекрытия в нём виртуальных методов</summary>
  /// <param name="conditions">Условия</param>
  /// <returns>Созданная Query, выбирающая состав дочерних объекта</returns>
  [NotNull]
  protected override RelatedObjectsQuery QueryConstruction([NotNull] ConditionStructure[] conditions)
  {
    return (RelatedObjectsQuery) new CompareWithSnapshotRelatedObjectsQuery((INodeQuerySupport) this, this._hashActualPrjLinkGuid, this._hashSnapshotPrjLinkGuids, this._objID, this._objTypeID, this._role, this._relTypeID, this._parentObjTypeID, conditions);
  }

  /// <summary>Возвращает данные указанного формата для объекта базы данных с указанным идентификатором.</summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Объект, представляющий данные указанного формата</returns>
  [CanBeNull]
  public override object GetData([NotNull] INodeID nodeID, [NotNull] Type dataFormat)
  {
    return !(dataFormat == typeof (IDBObjectFiltrationState)) || !(nodeID is ISavedObjectNodeID savedObjectNodeId) || savedObjectNodeId.ObjectExistInDB ? base.GetData(nodeID, dataFormat) : (object) new DBObjectFiltrationState(ObjectFiltrationState.fsCompositeVersionNotFound);
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
  public long ObjectVersionID => this._objID;
}
