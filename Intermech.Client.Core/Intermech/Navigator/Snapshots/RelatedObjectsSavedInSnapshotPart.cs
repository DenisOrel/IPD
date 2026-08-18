
// Type: Intermech.Navigator.Snapshots.RelatedObjectsSavedInSnapshotPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.Snapshots;

/// <summary>Часть объекта, сохранённого в итерации, позволяющая возвращать объекты, сохранённые в итерации внутри состава данного объекты
/// (несохранённые не возвращаются)</summary>
public class RelatedObjectsSavedInSnapshotPart : 
  RelatedObjectsPart,
  INodePart,
  INodeItems,
  INodeQuerySupport,
  IContextAware,
  ISnapshotContext,
  IObjectInSnapshotContext
{
  /// <summary>Идентификатор итерации</summary>
  public static readonly NodeColumnID ncF_SNAPSHOT_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_SNAPSHOT_ID, AttributeSourceTypes.Snapshot);
  /// <summary>Дата создания итерации</summary>
  public static readonly NodeColumnID ncF_SNAPSHOT_DATE = new NodeColumnID((object) ObligatoryObjectAttributes.F_SNAPSHOT_DATE, AttributeSourceTypes.Snapshot);
  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  private readonly Lazy<ISnapshot> _lazySnapshot;

  /// <summary>Конструктор части, позволяющий указать обрабатываемый объект и роль связанных с ним объектов. Созданная часть будет
  /// возвращать объекты, сохранённые в составе итерации сразу всех типов связей</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="objectVersionID">Идентификатор версии обрабатываемого объекта</param>
  /// <param name="relTypeID">Идентификатор типа связи</param>
  public RelatedObjectsSavedInSnapshotPart(
    [NotNull] IServiceProvider ownerServices,
    [NotEmpty] int objTypeID,
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relTypeID)
    : base(objTypeID, objectVersionID, RelatedObjectsRole.Composition, relTypeID, ownerServices)
  {
    this._lazySnapshot = new Lazy<ISnapshot>((Func<ISnapshot>) (() => this.Services.GetService<ISnapshot>()));
  }

  [NotNull]
  protected override RelatedObjectsQuery QueryConstruction([NotNull] ConditionStructure[] conditions)
  {
    return (RelatedObjectsQuery) new RelatedObjectsSavedInSnapshotQuery((INodeQuerySupport) this, this._objID, this._objTypeID, this._role, this._relTypeID, this._parentObjTypeID, conditions);
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <param name="fieldValues">Значения полей</param>
  /// <param name="adapter">Адаптер</param>
  /// <returns>Описание корневого узла</returns>
  [NotNull]
  public override INodeID CreateNodeId([NotNull, ItemCanBeNull] object[] fieldValues, [NotNull] RecordAdapter adapter)
  {
    int fieldIndex = adapter.GetFieldIndex((object) RelatedPartBase.ncF_ELEMENT_STATUSES);
    byte[] fieldValue = fieldIndex < 0 || fieldValues[fieldIndex] == DBNull.Value ? (byte[]) null : fieldValues[fieldIndex] as byte[];
    ObjectFiltrationState state = ObjectFiltrationState.fsNotRequired;
    if (fieldValue != null)
      state = (ObjectFiltrationState) ServicesManager.GetService<IElementStatusesClientService>().GetElementStatuses32("cad005f2-306c-11d8-b4e9-00304f19f545", fieldValue);
    return this.CreateObjectNodeIdFromParams(fieldValues, adapter, new CreateObjectNodeParams(Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]), Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]), Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]), 0L, Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJLINK_ID)]), Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]), Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]), this._relTypeID, Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]), 0L, state, Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]), 0L, Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]), Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PROJ_ID)]), DataSetProcessor.GetGuidValue(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID)], Guid.Empty), Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)])));
  }

  /// <summary>Создание идентификатора ноды из подготовленный структуры с параметрами онного</summary>
  [NotNull]
  protected override INodeID CreateObjectNodeIdFromParams(
    [NotNull, ItemCanBeNull] object[] fieldValues,
    [NotNull] RecordAdapter adapter,
    [NotNull] CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new ObjectSavedInSnapshotNodeID(createObjectNodeParams, this.SnapshotID);
  }

  /// <summary>Создает элемент пространства навигации, представляющий указанный с помощью унифицированного идентификатора объект базы данных,
  /// и возвращает ссылку на основной интерфейс элемента.</summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Ссылка на основной интерфейс элемента</returns>
  [NotNull]
  public override INode GetChild([NotNull] INodeID nodeID)
  {
    return (INode) new ObjectInSnapshotNode(nodeID as IObjectInSnapshotNodeID);
  }

  /// <summary>Получить список служебных полей (которые загружаются в узел независимо от настройки вида)</summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  [NotNull]
  public override List<object> GetSpecialFields()
  {
    return new List<object>((IEnumerable<object>) RelatedObjectsSavedInSnapshotQuery.SnapshotDataTableColumns.Values);
  }

  /// <summary>Получение сервисов Перекрыто для корректного отображения нод удалённых из состава объектов зачёркнутыми</summary>
  /// <param name="service">Тип сервиса</param>
  /// <returns>Сервис</returns>
  public override object GetService(Type service)
  {
    return !(service == typeof (INodeStatusesInfo)) ? (object) null : (object) ObjectsPartBase.StatusesInfoService;
  }

  /// <summary>Возвращает данные указанного формата для объекта базы данных с указанным идентификатором.</summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Объект, представляющий данные указанного формата</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return !(dataFormat == typeof (IDBObjectFiltrationState)) || !(nodeID is ISavedObjectNodeID savedObjectNodeId) || savedObjectNodeId.ObjectExistInDB ? base.GetData(nodeID, dataFormat) : (object) new DBObjectFiltrationState(ObjectFiltrationState.fsCompositeVersionNotFound);
  }

  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  public ISnapshot Snapshot
  {
    [DebuggerStepThrough] get => this._lazySnapshot.Value;
  }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this.Snapshot.ID;

  /// <summary>Что отображается в содержимом итерации</summary>
  public SnapshotDescriptor.Content SnapshotContent
  {
    [DebuggerStepThrough] get => SnapshotDescriptor.GetContentFromContext(this.Services);
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectVersionID => this._objID;
}
