
// Type: Intermech.Search.NotificationSelections.IncludedObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Search.NotificationSelections;

public sealed class IncludedObjectsDescriptor : HiveDescriptor
{
  private long[] _objectVersionIds;

  public IncludedObjectsDescriptor(long[] objectVersionIds)
    : base(Intermech.Navigator.Consts.NotificationSelectionsCategoryID, 1, "Включенные объекты")
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException();
    this._objectVersionIds = !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds) ? objectVersionIds : throw new ArgumentException();
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ObjectsListNode((IList) this._objectVersionIds)
    {
      LocalTypesMode = true,
      ShowAllModifications = true
    };
  }
}
