
// Type: Intermech.Navigator.ArticlesList.ArticlesListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.ArticlesList;

internal sealed class ArticlesListNode(Dictionary<int, List<long>> objectIDs, bool expandNode) : 
  ObjectsDictNode(objectIDs, expandNode)
{
  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  protected override INodePart GetPart(
    IConditionsProvider conditionProvider,
    IList objectIDs,
    int objectTypeID)
  {
    return (INodePart) new ArticlesListPart(objectIDs, conditionProvider, this.Services, objectTypeID);
  }
}
