
// Type: Intermech.Navigator.VirtualNodes.ObjectsSelectionNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>NodeID ноды списка объектов, удовлетворяющих некоторому условию</summary>
public class ObjectsSelectionNodeID : HiveNodeID, INodeID
{
  [NotNull]
  private readonly IConditionsProvider _conditionsProvider;

  public ObjectsSelectionNodeID(int objectTypeID, [NotNull] IConditionsProvider conditionsProvider)
    : base(Intermech.Navigator.Consts.CategoryMultipleObjectsNode, objectTypeID)
  {
    this._conditionsProvider = conditionsProvider;
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is ObjectsSelectionNodeID objectsSelectionNodeId))
      return base.Equals(obj);
    if (!base.Equals(obj))
      return false;
    return this._conditionsProvider == objectsSelectionNodeId._conditionsProvider || this._conditionsProvider.GetConditions().Equals((object) objectsSelectionNodeId._conditionsProvider.GetConditions());
  }

  public override int GetHashCode()
  {
    return (base.GetHashCode(), this._conditionsProvider.GetConditions()).GetHashCode();
  }
}
