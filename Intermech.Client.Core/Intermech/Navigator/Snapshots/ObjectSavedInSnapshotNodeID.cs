
// Type: Intermech.Navigator.Snapshots.ObjectSavedInSnapshotNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Nodes;
using System.Diagnostics;


namespace Intermech.Navigator.Snapshots;

/// <summary>Идентификатор ноды объекта, сохранённого в итерации и входящего там в состав другого объекта, сохранённого в итерации</summary>
public class ObjectSavedInSnapshotNodeID : 
  SavedObjectNodeID,
  INodeID,
  IRelatedObjectNodeID,
  IObjectNodeID,
  ISavedObjectNodeID,
  IObjectInSnapshotNodeID
{
  /// <summary>Идентификатор итерации</summary>
  [NotEmpty]
  protected long _SnapshotID;

  /// <summary>Конструктор идентификатора ноды сохранённого (напр. в итерации, возможно отсутствующего в БД) объекта</summary>
  /// <param name="createObjectNodeParams">Структура с параметрами для создания идентификатора ноды</param>
  /// <param name="snapshotID">Идентификатор итерации</param>
  public ObjectSavedInSnapshotNodeID([NotNull] CreateObjectNodeParams createObjectNodeParams, [NotEmpty] long snapshotID)
    : base(createObjectNodeParams)
  {
    this._SnapshotID = snapshotID;
  }

  /// <summary>Проверим, равен ли один объект другому</summary>
  /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
  /// <returns>true if the objects are considered equal, false if they are not</returns>
  public override bool Equals(object obj)
  {
    return obj is ObjectSavedInSnapshotNodeID inSnapshotNodeId && base.Equals(obj) && this._SnapshotID == inSnapshotNodeId._SnapshotID;
  }

  /// <summary>Вернуть хэш-код для объекта</summary>
  /// <returns>A hash code for this object</returns>
  public override int GetHashCode() => base.GetHashCode() ^ this._SnapshotID.GetHashCode();

  /// <summary>Возвращает идентификатор категории описываемого элемента.</summary>
  public override int CategoryID
  {
    [DebuggerStepThrough] get => !this.ObjectExistInDB ? 24 : base.CategoryID;
  }

  /// <summary>Идентификатор итерации</summary>
  public long SnapshotID => this._SnapshotID;
}
