
// Type: Intermech.Navigator.VirtualNodes.ObjectInSnapshotDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Snapshots;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.Snapshots;
using System;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>Дескриптор объекта в составе итерации</summary>
public class ObjectInSnapshotDescriptor : 
  Intermech.Navigator.DBObjects.Descriptor,
  INodeItems,
  IContextAware,
  IDescriptor,
  IPersistable,
  ICloneable,
  IDescriptorElementStatuses,
  ISnapshotContext,
  IObjectInSnapshotContext
{
  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  protected internal readonly ISnapshot _snapshot;

  /// <summary>Создает дескриптор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="snapshot">Интерфейс итерации</param>
  /// <param name="objectVersionID">Идентификатор версии объекта</param>
  protected ObjectInSnapshotDescriptor(
    [NotNull] IServiceProvider ownerServices,
    [NotNull] ISnapshot snapshot,
    [CanBeEmpty] long objectVersionID = 0)
    : base(objectVersionID != 0L ? objectVersionID : snapshot.RootObjectVersionID, ObjectFiltrationState.fsNotRequired)
  {
    this.Services = ownerServices;
    this._snapshot = snapshot;
  }

  /// <summary>Статический метод-конструктор</summary>
  /// <param name="ownerServices">Контекст</param>
  /// <param name="objectVersionID">Идентификатор версии объекта. Если равен 0, то дескриптор будет создан для корневого объекта итерации</param>
  /// <returns>Созданный дескриптор объекта, сохранённого в итерации</returns>
  [NotNull]
  public static ObjectInSnapshotDescriptor Create(
    [NotNull] IServiceProvider ownerServices,
    [CanBeEmpty] long objectVersionID = 0)
  {
    ISnapshot service = ownerServices.GetService<ISnapshot>();
    return new ObjectInSnapshotDescriptor(ownerServices, service, objectVersionID == 0L ? service.RootObjectVersionID : objectVersionID);
  }

  /// <summary>Создает элемент пространства навигации, представляющий указанный с помощью унифицированного идентификатора объект базы данных,
  /// и возвращает ссылку на основной интерфейс элемента.</summary>
  /// <param name="nodeID">Идентификатор, описывающий объект базы данных</param>
  /// <returns>Ссылка на основной интерфейс элемента</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ObjectInSnapshotNode(nodeID as IObjectInSnapshotNodeID);
  }

  /// <summary>Виртуальный метод создания NodeID. Написано для того, чтобы потомки могли перекрыть и создавать свои, расширенные NodeID</summary>
  protected override INodeID CreateObjectNodeIdFromParams(
    CreateObjectNodeParams createObjectNodeParams)
  {
    return (INodeID) new ObjectSavedInSnapshotNodeID(createObjectNodeParams, this.SnapshotID);
  }

  /// <summary>Интерфейс итерации</summary>
  [NotNull]
  public ISnapshot Snapshot => this._snapshot;

  /// <summary>Идентификатор итерации</summary>
  [NotEmpty]
  public long SnapshotID => this._snapshot.ID;

  /// <summary>Идентификатор версии объекта</summary>
  [NotEmpty]
  public long ObjectVersionID => this._realObjID;
}
