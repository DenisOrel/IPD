
// Type: Intermech.Navigator.Snapshots.SavedInSnapshotDeletedInActualCompositionPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Snapshots;

/// <summary>Часть содержимого объекта, сохранённого в итерации, отражающая часть состава объекта, содержащую сохранённое в итерации,
/// однако отсутствующее (удалённое?) актуальном составе содержимое объекта</summary>
public class SavedInSnapshotDeletedInActualCompositionPart : 
  RelatedObjectsSavedInSnapshotPart,
  ISnapshotContext,
  IObjectInSnapshotContext,
  INodePart,
  INodeItems,
  INodeQuerySupport,
  IContextAware
{
  /// <summary>Хэш гуидов связей актуального состава</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashActualPrjLinkGuids;
  /// <summary>Хэш гуидов связей элементов состава, сохранённого в итерации</summary>
  [NotNull]
  private readonly HashSet<Guid> _hashSnapshotPrjLinkGuids;

  /// <summary>Конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="objectVersionID">Версия объекта</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  /// <param name="hashActualPrjLinkGuids">Список частей актуального содержимого объекта</param>
  /// <param name="hashSnapshotPrjLinkGuids"></param>
  public SavedInSnapshotDeletedInActualCompositionPart(
    [NotNull] IServiceProvider ownerServices,
    [NotEmpty] int objTypeID,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relTypeID,
    [NotNull] HashSet<Guid> hashActualPrjLinkGuids,
    [NotNull] HashSet<Guid> hashSnapshotPrjLinkGuids)
    : base(ownerServices, objTypeID, objectVersionID, relTypeID)
  {
    this._hashActualPrjLinkGuids = hashActualPrjLinkGuids;
    this._hashSnapshotPrjLinkGuids = hashSnapshotPrjLinkGuids;
  }

  /// <summary>Получить список служебных полей (которые загружаются в узел независимо от настройки вида)</summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  [NotNull]
  public override List<object> GetSpecialFields()
  {
    List<object> collection = base.GetSpecialFields() ?? new List<object>();
    collection.SafeAdd<object>((object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT);
    return collection;
  }

  /// <summary>Отразить колонку "Навигатора" на поле</summary>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <returns>Поле</returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid.Equals(SnapshotConsts.SNAPSHOT_SCHEME_GUID) && column.ID != null && column.ID.Equals((object) SnapshotConsts.F_COMPARE_RESULT) ? (object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT : base.MapColumnToField(column);
  }

  [NotNull]
  protected override RelatedObjectsQuery QueryConstruction([NotNull] ConditionStructure[] conditions)
  {
    return (RelatedObjectsQuery) new SavedInSnapshotDeletedInActualCompositionQuery((INodeQuerySupport) this, this._objID, this._objTypeID, this._role, this._relTypeID, this._parentObjTypeID, conditions, this._hashActualPrjLinkGuids, this._hashSnapshotPrjLinkGuids);
  }

  /// <summary>Создание идентификатора ноды из подготовленный структуры с параметрами онного</summary>
  [NotNull]
  protected override INodeID CreateObjectNodeIdFromParams(
    [NotNull, ItemCanBeNull] object[] fieldValues,
    [NotNull] RecordAdapter adapter,
    [NotNull] CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new CompareWithSnapshotObjectNodeID(createObjectNodeParams, this.SnapshotID, CompositionCompareResult.Deleted);
  }

  /// <summary>Создает элемент пространства навигации, представляющий указанный с помощью унифицированного идентификатора объект базы данных,
  /// и возвращает ссылку на основной интерфейс элемента.</summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Ссылка на основной интерфейс элемента</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new CompareWithSnapshotObjectNode(nodeID as ICompareWithSnapshotObjectNodeID);
  }
}
