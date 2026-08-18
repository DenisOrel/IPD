
// Type: Intermech.Navigator.Snapshots.CompareWithSnapshotObjectNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.VirtualColumns;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;


namespace Intermech.Navigator.Snapshots;

/// <summary>Элемент навигации объекта базы данных, находящийся в актуальном составе объекта, чей состав сравнивается с сохранённым в итерации</summary>
public class CompareWithSnapshotObjectNode : 
  ObjectInSnapshotNode,
  ISnapshotContext,
  IObjectInSnapshotContext,
  IContextAware,
  INodeNotifications,
  INodeIDCreator,
  IObjectTypeAndRelationFiltrationSupported,
  INode,
  INodeItems,
  INodeCustomUI
{
  /// <summary>GUID части содержимого объекта, сохранённого в итерации, отражающая часть состава объекта, содержащую сохранённое в итерации,
  /// однако отсутствующее (удалённое?) актуальном составе содержимое объекта</summary>
  protected static readonly Guid SavedInSnapshotDeletedInActualObjectPartGuid = new Guid("A5C15B0F-50B3-48BD-A958-56B56B711F19");
  /// <summary>"Виртуальная" (то есть добавляемая в результат запроса уже на клиенте) колонка "результат сравнения"</summary>
  [NotNull]
  public static VirtualQueryResultColumn ncF_COMPARE_RESULT = new VirtualQueryResultColumn("F_COMPARE_RESULT", typeof (CompositionCompareResult), (object) CompositionCompareResult.NotChecked);
  [CanBeNull]
  private static Image _imageStatus_New_16X16;
  private static Image _imageStatus_Deleted_16X16;
  /// <summary>Хэш гуидов связей элементов актуального состава</summary>
  [NotNull]
  private readonly HashSet<Guid> _actualPrjLinkGuidsHash = new HashSet<Guid>();
  /// <summary>Хэш гуидов связей элементов состава, сохранённого в итерации</summary>
  [NotNull]
  private readonly HashSet<Guid> _snapshotPrjLinkGuidsHash = new HashSet<Guid>();
  /// <summary>Результат сравнения актуального состава с сохранённым в итерации для данной ноды</summary>
  public CompositionCompareResult CompareResult;

  /// <summary>Иконка статуса сравнения структур объектов "новый" (объект отсутствует в итерации, однако присутствует в актуальном составе)</summary>
  [NotNull]
  public static Image ImageStatus_New_16X16
  {
    [DebuggerStepThrough] get
    {
      return NavigatorImages.GetImage(ref CompareWithSnapshotObjectNode._imageStatus_New_16X16, "CompareResultNew.bmp");
    }
  }

  /// <summary>Иконка статуса сравнения структур объектов "Не изменён" (объект присутствует и в итерации, и в актуальном составе, его
  /// параметры не изменились)</summary>
  [NotNull]
  public static Image ImageStatus_NotChanged_16X16
  {
    [DebuggerStepThrough] get => NavigatorImages.HorizontalDottedTreeLine;
  }

  /// <summary>Иконка статуса сравнения структур объектов "удалён" (объект присутствует в итерации, однако отсутствует в актуальном составе)</summary>
  [NotNull]
  public static Image ImageStatus_Deleted_16X16
  {
    [DebuggerStepThrough] get
    {
      return NavigatorImages.GetImage(ref CompareWithSnapshotObjectNode._imageStatus_Deleted_16X16, "CompareResultDeleted.bmp");
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="nodeID">Интерфейс идентификатора ноды объекта сравниваемого в контексте итерации с актуальным объектом</param>
  public CompareWithSnapshotObjectNode([NotNull] ICompareWithSnapshotObjectNodeID nodeID)
    : base((IObjectInSnapshotNodeID) nodeID)
  {
    this.CompareResult = nodeID.CompareResult;
  }

  /// <summary>Создать список слотов-папок</summary>
  /// <returns>Список слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> source = this.CompareResult != CompositionCompareResult.Deleted ? this.CreateActualCompositionFolderSlots() : new List<PartSlot>();
    source.AddRange((IEnumerable<PartSlot>) source.Select<PartSlot, INodePart>((System.Func<PartSlot, INodePart>) (partSlot => partSlot.Object)).OfType<RelatedObjectsPart>().Select<RelatedObjectsPart, int>((System.Func<RelatedObjectsPart, int>) (relatedObjectsPart => relatedObjectsPart.RelationTypeID)).Select<int, PartSlot>((System.Func<int, PartSlot>) (relTypeID => new PartSlot(ObjectInSnapshotNode.GetRelTypePartSlotGuid(relTypeID), (INodePart) new SavedInSnapshotDeletedInActualCompositionPart(this.Services, this._objTypeID, this._objID, relTypeID, this._actualPrjLinkGuidsHash, this._snapshotPrjLinkGuidsHash)))).ToArray<PartSlot>(source.Count));
    return source;
  }

  /// <summary>Перекрываю алгоритм создания слота содержимого, загружающего состав объекта по определённому типу связи</summary>
  /// <param name="relTypeId">Тип связи</param>
  /// <returns>Созданный слот содержимого, загружающий состав объекта по определённому типу связи</returns>
  protected override INodePart CreateFolderPart(int relTypeId)
  {
    return (INodePart) new CompareWithSnapshotRelatedObjectsPart(this.Services, this._actualPrjLinkGuidsHash, this._snapshotPrjLinkGuidsHash, this._objID, relTypeId, this._objTypeID);
  }

  /// <summary>Переопределяю метод-конструктор составной Query для того, чтобы иметь возможность обработать результаты выбора данных из БД
  /// в частности в данном случае - для того, чтобы заполнить виртуальное поле "результат сравнения".
  /// Для получения результатов сравнения нам надо иметь результаты выбора данных из всех Query, входящих в составную,
  /// поэтому приходится работать через соотв. событие составной Query, а не просто переписать каждую Query</summary>
  /// <param name="subQueries">Список вложенных Query</param>
  /// <returns>Созданная составная Query содержащая в себе переданный список вложенных Query</returns>
  protected override INodeQuery CreateCompositeQuery(List<QuerySlot> subQueries)
  {
    this._actualPrjLinkGuidsHash.Clear();
    this._snapshotPrjLinkGuidsHash.Clear();
    INodeQuery compositeQuery = base.CreateCompositeQuery(subQueries);
    (compositeQuery as AdvCompositeQuery).AfterExecute += new AdvCompositeQuery.delegateResultQueriesPostProcessing(this.QueriesPostProcessing);
    return compositeQuery;
  }

  /// <summary>Получение основной иконки ноды</summary>
  /// <returns>Иконка, либо null, если должен использоваться стандартный механизм получения иконки по категории/типу.</returns>
  public Image GetMainIcon() => (Image) null;

  /// <summary>Получение иконки, которая отобразится перед "стандартной иконкой" категории-типа у данной ноды.
  /// Может быть использована для    визуального отображения ключевого статуса ноды в данном конкретном контексте
  /// (например для сравнения состава - результат сравнения, удалено, новое, изменено, или без изменений).</summary>
  /// <returns>Иконка, либо null, если отображение не требуется.
  /// В том случае если у некоторых нод в данном контексте статус отображается, а у других нет, для тех нод, у которых иконок
  /// не будет можно вернут стандартную иконку "пунктирная горизонтальная линия 16x16".</returns>
  public Image GetPrefixIcon()
  {
    switch (this.CompareResult)
    {
      case CompositionCompareResult.NotChanged:
        return CompareWithSnapshotObjectNode.ImageStatus_NotChanged_16X16;
      case CompositionCompareResult.New:
        return CompareWithSnapshotObjectNode.ImageStatus_New_16X16;
      case CompositionCompareResult.Deleted:
        return CompareWithSnapshotObjectNode.ImageStatus_Deleted_16X16;
      default:
        return (Image) null;
    }
  }

  /// <summary>Получить специальный виджет для колонки.</summary>
  /// <param name="rowWidget">Виджет строки дерева</param>
  /// <param name="column">Колонка</param>
  /// <returns>Созданный виджет, либо null, если для этой колонки всё должно быть по-умолчанию</returns>
  public CellWidget GetCustomCellWidget(RowWidget rowWidget, NavigatorTreeColumn column)
  {
    return (CellWidget) null;
  }

  /// <summary>Пост обработка выбора дочерних данных у ноды</summary>
  /// <param name="resultQueries">Список Query, вернувших результат</param>
  private void QueriesPostProcessing([CanBeNull, ItemNotNull] List<QuerySlot> resultQueries)
  {
    if (resultQueries == null)
      return;
    foreach (CompareWithSnapshotRelatedObjectsQuery relatedObjectsQuery in resultQueries.Select<QuerySlot, CompareWithSnapshotRelatedObjectsQuery>((System.Func<QuerySlot, CompareWithSnapshotRelatedObjectsQuery>) (slot => slot.Object as CompareWithSnapshotRelatedObjectsQuery)).Where<CompareWithSnapshotRelatedObjectsQuery>((System.Func<CompareWithSnapshotRelatedObjectsQuery, bool>) (compareQuery => compareQuery?.DataTable != null && compareQuery.DataTable.IsInitialized)))
    {
      int linkGuidColumnNum = relatedObjectsQuery.PrjLinkGuidColumnNum;
      int compareResultColumnNum = relatedObjectsQuery.CompareResultColumnNum;
      object obj1;
      foreach (DataRow row in relatedObjectsQuery.DataTable.Where((System.Func<DataRow, bool>) (dataRow => (obj1 = dataRow[compareResultColumnNum]) is CompositionCompareResult && (CompositionCompareResult) obj1 == CompositionCompareResult.NotChecked)))
      {
        object obj2 = row[linkGuidColumnNum];
        switch (obj2)
        {
          case null:
          case DBNull _:
            throw new Exception("PrjGuid can`t be null");
          default:
            Guid guid = new Guid(obj2.ToString());
            if (!(guid != Guid.Empty))
              throw new Exception("PrjGuid can`t be null");
            row.SetField<CompositionCompareResult>(compareResultColumnNum, this._snapshotPrjLinkGuidsHash.Contains(guid) ? CompositionCompareResult.NotChanged : CompositionCompareResult.New);
            continue;
        }
      }
    }
  }
}
