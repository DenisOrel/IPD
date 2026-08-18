
// Type: Intermech.Navigator.VirtualNodes.RelatedObjectsSelectionNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>NodeID ноды состава объекта, с фильтрацией по переданным в конструктор условиям</summary>
public class RelatedObjectsSelectionNodeID : ObjectsSelectionNodeID, INodeID
{
  /// <summary>Идентификатор версии объекта, с которыми связаны объекты, загружаемые в состав</summary>
  protected readonly long _ObjectVersionID;
  /// <summary>Указывает роль объектов, связанных с каким-либо объектом. Используется в
  /// <see cref="T:Intermech.Navigator.DBObjects.RelatedObjectsQuery" /> для указание, что должен вернуть запрос -
  /// состав или применяемость объекта.</summary>
  protected readonly RelatedObjectsRole _Role;
  /// <summary>Тип связи, по которой получается состав/входимость</summary>
  protected readonly int _RelationTypeID;

  public RelatedObjectsSelectionNodeID(
    [NotEmpty] long objectVersionID,
    [NotEmpty] int relationTypeID,
    RelatedObjectsRole role,
    [NotEmpty] int objectTypeID,
    [NotNull] IConditionsProvider conditionsProvider)
    : base(objectTypeID, conditionsProvider)
  {
    this._ObjectVersionID = objectVersionID;
    this._RelationTypeID = relationTypeID;
    this._Role = role;
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is RelatedObjectsSelectionNodeID objectsSelectionNodeId))
      return base.Equals(obj);
    return base.Equals(obj) && this._ObjectVersionID == objectsSelectionNodeId._ObjectVersionID && this._Role == objectsSelectionNodeId._Role && this._RelationTypeID == objectsSelectionNodeId._RelationTypeID;
  }

  public override int GetHashCode()
  {
    return (base.GetHashCode(), this._ObjectVersionID, this._Role, this._RelationTypeID).GetHashCode();
  }
}
