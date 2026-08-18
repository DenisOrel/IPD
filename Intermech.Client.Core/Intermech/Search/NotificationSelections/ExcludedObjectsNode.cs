
// Type: Intermech.Search.NotificationSelections.ExcludedObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.NotificationSelections;

public sealed class ExcludedObjectsNode : ObjectsListNode
{
  public ExcludedObjectsNode(long[] objectVersionIds)
    : base((IList) objectVersionIds)
  {
    this.LocalTypesMode = true;
    this.ShowAllModifications = true;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(Intermech.Navigator.Consts.CategoryMultipleObjectsGuid, (INodePart) new ExcludedObjectsNodePart(this.objectIDs.Cast<long>().ToArray<long>()))
    };
  }
}
