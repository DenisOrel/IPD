
// Type: Intermech.Navigator.DBObjectTypes.SelectionTypeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjectTypes.Implementation;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Класс, реализующий элемент тип объектов "Выборки" и "Классификаторы" из пространства навигации.
/// </summary>
public class SelectionTypeNode : ObjectTypeNode
{
  public SelectionTypeNode(int objTypeID, AccessRights accessRights)
    : base(objTypeID, accessRights)
  {
    this._showClassifiers = false;
  }

  public SelectionTypeNode(NodeID nodeID)
    : base(nodeID)
  {
    this._showClassifiers = false;
  }
}
