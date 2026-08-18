
// Type: Intermech.Search.ObjectGroups.ObjectGroupCreatedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupCreatedAnalyser : IUpdateAnalyser
{
  private List<ObjectGroupNodeID> _newObjectGroupNodeIds;

  public ObjectGroupCreatedAnalyser(ObjectGroupNodeID[] newObjectGroupNodeIds)
  {
    this._newObjectGroupNodeIds = newObjectGroupNodeIds != null && newObjectGroupNodeIds.Length != 0 ? ((IEnumerable<ObjectGroupNodeID>) newObjectGroupNodeIds).ToList<ObjectGroupNodeID>() : throw new ArgumentException();
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is ObjectGroupNodeID))
      return;
    ObjectGroupNodeID objectGroupNodeId = this._newObjectGroupNodeIds.FirstOrDefault<ObjectGroupNodeID>((Func<ObjectGroupNodeID, bool>) (o => o.Equals((object) (ObjectGroupNodeID) nodeID)));
    if (objectGroupNodeId == null)
      return;
    this._newObjectGroupNodeIds.Remove(objectGroupNodeId);
  }

  public void Postprocess(IUpdatePlan plan)
  {
    if (plan == null)
      throw new ArgumentNullException(nameof (plan));
    foreach (ObjectGroupNodeID objectGroupNodeId in this._newObjectGroupNodeIds)
      plan.Append((INodeID) objectGroupNodeId);
  }
}
