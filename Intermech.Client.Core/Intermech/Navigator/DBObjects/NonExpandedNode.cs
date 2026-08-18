
// Type: Intermech.Navigator.DBObjects.NonExpandedNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Специальный тип узла, состав которого не разворачиваеться
/// </summary>
/// <summary>Создать узел</summary>
/// <param name="objTypeID">Тип</param>
/// <param name="objID">Идентификатор версии объекта</param>
public class NonExpandedNode(int objTypeID, long objID) : ObjectNode(objTypeID, objID)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public override INodeQuery GetQuery(ContentType content) => (INodeQuery) null;
}
