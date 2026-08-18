
// Type: Intermech.Navigator.VirtualNodes.CompareWithSnapshotObjectDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Snapshots;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>Дескриптор объекта, чьи параметры сравниваются с таким же объектом, сохранённым в итерации</summary>
public class CompareWithSnapshotObjectDescriptor : 
  ObjectInSnapshotDescriptor,
  INodeItems,
  IContextAware,
  IDescriptor,
  IPersistable,
  ICloneable,
  IDescriptorElementStatuses,
  ISnapshotContext,
  IObjectInSnapshotContext,
  ISpecialFieldsSupported
{
  /// <summary>Создает дескриптор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="snapshot">Интерфейс итерации</param>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  protected CompareWithSnapshotObjectDescriptor(
    [NotNull] IServiceProvider ownerServices,
    [NotNull] ISnapshot snapshot,
    long objectVersionID = 0)
    : base(ownerServices, snapshot, objectVersionID)
  {
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="objectVersionID">Идентификатор версии объекта. Если равен 0, то дескриптор будет создан для корневого объекта итерации</param>
  /// <returns>Созданный дескриптор объекта, сохранённого в итерации</returns>
  [NotNull]
  public static CompareWithSnapshotObjectDescriptor Create(
    [NotNull] IServiceProvider ownerServices,
    long objectVersionID = 0)
  {
    ISnapshot service = ownerServices.GetService<ISnapshot>();
    return new CompareWithSnapshotObjectDescriptor(ownerServices, service, objectVersionID == 0L ? service.RootObjectVersionID : objectVersionID);
  }

  /// <summary>Creates object node identifier from parameters</summary>
  /// <param name="createObjectNodeParams">A variable-length parameters list containing create object node parameters</param>
  /// <returns>The new object node identifier from parameters</returns>
  [NotNull]
  protected override INodeID CreateObjectNodeIdFromParams(
    [NotNull] CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new CompareWithSnapshotObjectNodeID(createObjectNodeParams, this.SnapshotID, !Session.Invoke<bool>((Session.SessionHandler<bool>) (session => session.GetObjectInfo(createObjectNodeParams.ObjectID).Empty)) ? CompositionCompareResult.NotChanged : CompositionCompareResult.Deleted);
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

  /// <summary>Получение из дескриптора значения поля неизвестного типа. Предназначено для перекрытия и обработки кастомных (напр. виртуальных,
  /// чьё значение рассчитывается уже на клиенте) полей в потомках</summary>
  protected override object GetUnknownFieldValue(object field, INodeID nodeID)
  {
    if (field != CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT)
      return base.GetUnknownFieldValue(field, nodeID);
    return !(nodeID is ICompareWithSnapshotObjectNodeID snapshotObjectNodeId) ? (object) null : (object) snapshotObjectNodeId.CompareResult;
  }

  /// <summary>Отобразить колонку в поле</summary>
  /// <param name="column">Колонка</param>
  /// <returns>Поле</returns>
  [CanBeNull]
  object IDescriptor.MapColumnToField([NotNull] NodeColumn column)
  {
    return column.SchemeGuid == SnapshotConsts.SNAPSHOT_SCHEME_GUID && object.Equals(column.ID, (object) SnapshotConsts.F_COMPARE_RESULT) ? (object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT : Intermech.Navigator.DBObjects.Helper.MapColumnToFieldName(column);
  }

  /// <summary>Возвращает список идентификаторов полей источника данных, значения которых обязательно должны быть получены в результате
  /// выполнения запроса.</summary>
  /// <returns>Список идентификаторов полей источника данных</returns>
  [NotNull]
  [ItemNotNull]
  public List<object> GetSpecialFields()
  {
    return ListExtensions.CreateFromSingle<object>((object) CompareWithSnapshotObjectNode.ncF_COMPARE_RESULT);
  }
}
