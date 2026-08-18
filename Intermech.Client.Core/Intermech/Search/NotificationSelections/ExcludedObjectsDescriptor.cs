
// Type: Intermech.Search.NotificationSelections.ExcludedObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.NotificationSelections;

public sealed class ExcludedObjectsDescriptor : HiveDescriptor
{
  private long[] _objectVersionIds;

  public ExcludedObjectsDescriptor(long[] objectVersionIds)
    : base(Intermech.Navigator.Consts.NotificationSelectionsCategoryID, 2, "Исключенные объекты")
  {
    if (objectVersionIds == null)
      throw new ArgumentNullException(nameof (objectVersionIds));
    this._objectVersionIds = !ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) objectVersionIds) ? ((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>() : throw new ArgumentException();
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new ExcludedObjectsNode(this._objectVersionIds);
  }
}
