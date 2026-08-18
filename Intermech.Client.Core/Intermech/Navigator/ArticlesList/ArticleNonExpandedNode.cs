
// Type: Intermech.Navigator.ArticlesList.ArticleNonExpandedNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Navigator.ArticlesList;

/// <summary>Создать узел</summary>
/// <param name="objTypeID">Тип</param>
/// <param name="objID">Идентификатор версии объекта</param>
internal sealed class ArticleNonExpandedNode(int objTypeID, long objID) : ObjectNode(objTypeID, objID)
{
  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;
}
