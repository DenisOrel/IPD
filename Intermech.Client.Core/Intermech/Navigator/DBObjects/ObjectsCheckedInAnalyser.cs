
// Type: Intermech.Navigator.DBObjects.ObjectsCheckedInAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

public class ObjectsCheckedInAnalyser : IUpdateAnalyser
{
  private IDictionary _checkedInObjectIDs;

  public ObjectsCheckedInAnalyser(IList<long> objIDs)
  {
    this._checkedInObjectIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < objIDs.Count; ++index)
      this._checkedInObjectIDs.Add((object) objIDs[index], (object) null);
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID))
      return;
    NodeID nodeId = (NodeID) nodeID;
    if (nodeId.ObjectID < 0L)
    {
      if (!this._checkedInObjectIDs.Contains((object) nodeId.ObjectID))
        return;
      plan.Replace((INodeID) new NodeID(new CreateObjectNodeParams(nodeId.TypeID, -nodeId.ObjectID, nodeId.ID, nodeId.CheckedOutBy, nodeId.PrjLinkID, nodeId.LCStepID, nodeId.Caption, nodeId.RelationTypeID, nodeId.Owner, nodeId.Sorting, nodeId.State, nodeId.Version, nodeId.BaseVersion, nodeId.SiteID, nodeId.ProjID, nodeId.RelGuid, nodeId.ModificationID)));
    }
    else
    {
      if (!this._checkedInObjectIDs.Contains((object) Math.Abs(nodeId.ObjectID)))
        return;
      plan.Replace((INodeID) new NodeID(new CreateObjectNodeParams(nodeId.TypeID, nodeId.ObjectID, nodeId.ID, nodeId.CheckedOutBy, nodeId.PrjLinkID, nodeId.LCStepID, nodeId.Caption, nodeId.RelationTypeID, nodeId.Owner, nodeId.Sorting, nodeId.State, nodeId.Version, nodeId.BaseVersion, nodeId.SiteID, nodeId.ProjID, nodeId.RelGuid, nodeId.ModificationID)));
    }
  }

  public void Postprocess(IUpdatePlan plan)
  {
  }
}
