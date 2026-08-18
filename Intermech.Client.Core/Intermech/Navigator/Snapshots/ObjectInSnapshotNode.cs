
// Type: Intermech.Navigator.Snapshots.ObjectInSnapshotNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Snapshots;

/// <summary>Элемент навигации объекта базы данных в контексте итерации</summary>
/// <summary>Создать узел ноды объекта сохранённого в итерации</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="objTypeID">Тип объекта</param>
public class ObjectInSnapshotNode([NotNull] IObjectInSnapshotNodeID objectInSnapshotNodeID) : 
  ObjectNode(objectInSnapshotNodeID.ObjTypeId != -1 ? objectInSnapshotNodeID.ObjTypeId : Repository.ObjectVersions.GetObjectType((long) objectInSnapshotNodeID.ObjTypeId), objectInSnapshotNodeID.ObjectVersionID),
  IContextAware,
  INodeNotifications,
  INodeIDCreator,
  IObjectTypeAndRelationFiltrationSupported,
  INode,
  INodeItems,
  ISnapshotContext,
  IObjectInSnapshotContext
{
  [NotNull]
  private static readonly Dictionary<int, Guid> _relType2PartGuid = new Dictionary<int, Guid>();
  [NotNull]
  private static readonly Dictionary<Guid, int> _partGuid2RelType = new Dictionary<Guid, int>();
  [NotNull]
  private static readonly object _syncPartGuidObj = new object();
  /// <summary>Интерфейс итерации</summary>
  [CanBeNull]
  private ISnapshot _snapshot;
  /// <summary>Содержимое объекта в виде списка слотов</summary>
  [CanBeNull]
  private List<PartSlot> _compositionInSnapshotSlots;

  /// <summary>Получить Guid слота части для переданного идентификатора типа связи</summary>
  public static Guid GetRelTypePartSlotGuid(int relTypeID)
  {
    Guid typePartSlotGuid;
    if (ObjectInSnapshotNode._relType2PartGuid.TryGetValue(relTypeID, out typePartSlotGuid))
      return typePartSlotGuid;
    lock (ObjectInSnapshotNode._syncPartGuidObj)
    {
      if (ObjectInSnapshotNode._relType2PartGuid.TryGetValue(relTypeID, out typePartSlotGuid))
        return typePartSlotGuid;
      Guid key = Guid.NewGuid();
      ObjectInSnapshotNode._relType2PartGuid[relTypeID] = key;
      ObjectInSnapshotNode._partGuid2RelType[key] = relTypeID;
      return key;
    }
  }

  /// <summary>Получить идентификатор типа связи по Guid-у слота части, загружающего состав, сохранённый в итерации по данному типу связи</summary>
  public static int GetRelTypeIdByPartSlotGuid(Guid partSlotGuid)
  {
    int num;
    return !ObjectInSnapshotNode._partGuid2RelType.TryGetValue(partSlotGuid, out num) ? -1 : num;
  }

  /// <summary>Содержимое объекта в виде списка слотов</summary>
  [NotNull]
  [ItemNotNull]
  public List<PartSlot> CompositionInSnapshotSlots
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._compositionInSnapshotSlots ?? (this._compositionInSnapshotSlots = this.CreateCompositionInSnapshotSlots());
    }
  }

  /// <summary>Функция-конструктор содержимого объекта в виде списка слотов</summary>
  /// <returns>Содержимое объекта в виде списка слотов</returns>
  [NotNull]
  [ItemNotNull]
  protected virtual List<PartSlot> CreateCompositionInSnapshotSlots()
  {
    List<int> visibleRelations = this.UserRole.Rule.GetObjectTypeVisibleRelations(this._objTypeID, true);
    return visibleRelations.EmptyIfNull<int>().Where<int>((Func<int, bool>) (relType => relType != -1)).Select<int, PartSlot>((Func<int, PartSlot>) (relType => new PartSlot(ObjectInSnapshotNode.GetRelTypePartSlotGuid(relType), (INodePart) new RelatedObjectsSavedInSnapshotPart(this.Services, this._objTypeID, this._objID, relType)))).ToList<PartSlot>(visibleRelations.Count);
  }

  /// <summary>Переопределяю метод-конструктор составной Query для того, чтобы иметь возможность создать единый источник данных
  /// о составе объекта, сохранённого в итерацию. Позволяет ликвидировать запрос в БД query каждой части ноды с получением
  /// полного набора данных о составе (фильтрация по типу связи возможна уже только на клиенте, API не позволяет иного).
  /// Составная Query будет выступать источником данных, получая данные о составе 1 раз при первом запросе
  /// любой из query частей ноды. query частей ноды будет обращаться за данными к этой составной Query</summary>
  /// <param name="subQueries">Список вложенных Query</param>
  /// <returns>Созданная составная Query содержащая в себе переданный список вложенных Query</returns>
  [NotNull]
  protected override INodeQuery CreateCompositeQuery([NotNull] List<QuerySlot> subQueries)
  {
    RelatedObjectsSavedInSnapshotCompositeQuery compositeQuery = new RelatedObjectsSavedInSnapshotCompositeQuery(subQueries, this.Snapshot, this._objID);
    foreach (RelatedObjectsSavedInSnapshotQuery savedInSnapshotQuery in subQueries.SelectNotNull<QuerySlot, RelatedObjectsSavedInSnapshotQuery>((Func<QuerySlot, RelatedObjectsSavedInSnapshotQuery>) (subQuerySlot => subQuerySlot.Object as RelatedObjectsSavedInSnapshotQuery)))
      savedInSnapshotQuery.CompositionDataTableSource = (IDataTableSource) compositeQuery;
    return (INodeQuery) compositeQuery;
  }

  /// <summary>Перекрываю алгоритм создания списка слотов-папок</summary>
  /// <returns>Созданный список слотов-папок</returns>
  [NotNull]
  protected override List<PartSlot> CreateFolderSlots() => this.CompositionInSnapshotSlots;

  /// <summary>Вызов оригинального ObjectNode.CreateFolderSlots() для получения актуального состава вместо сохранённого в итерации.
  /// Требуется для использования в потомках, в частности для сравнения актуального состава с сохранённым в итерации</summary>
  [NotNull]
  protected virtual List<PartSlot> CreateActualCompositionFolderSlots() => base.CreateFolderSlots();

  /// <summary>Возвращает коллекцию всех поддерживаемых данным элементом виртуальных колонок навигатора. Этот метод используется диалогом настройки отображения грида.</summary>
  /// <param name="content">Набор флагов, описывающих тип содержимого грида.</param>
  /// <param name="columnSetName">Название набора колонок. Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию.</param>
  /// <returns>Коллекция виртуальных колонок навигатора.</returns>
  [NotNull]
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    [NotNull] string columnSetName)
  {
    ServicesManager.GetService<IColumnSchemes>();
    NodeColumnCollection supportedColumns = new NodeColumnCollection();
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(supportedColumns, true, true);
    Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(supportedColumns);
    Intermech.Navigator.DBObjects.Helper.AddAllColumns(supportedColumns);
    Intermech.Navigator.DBObjects.Helper.AddAllColumnsRelation(supportedColumns);
    supportedColumns.SafeAddRange<NodeColumn>((IEnumerable<NodeColumn>) SnapshotConsts.SnapshotGridColumns());
    return supportedColumns;
  }

  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  public ISnapshot Snapshot
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Services.EnsureInitialized<ISnapshot>(ref this._snapshot);
    }
  }

  /// <summary>Идентификатор итерации</summary>
  [NotEmpty]
  public long SnapshotID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.Snapshot.ID;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectVersionID => this._objID;
}
