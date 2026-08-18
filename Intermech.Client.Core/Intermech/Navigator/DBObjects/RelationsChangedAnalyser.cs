
// Type: Intermech.Navigator.DBObjects.RelationsChangedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

public class RelationsChangedAnalyser : IUpdateAnalyser
{
  private IDictionary _changedRelationIDs;

  public RelationsChangedAnalyser(IList<long> relationIDs)
  {
    this._changedRelationIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < relationIDs.Count; ++index)
    {
      if (!this._changedRelationIDs.Contains((object) relationIDs[index]))
        this._changedRelationIDs.Add((object) relationIDs[index], (object) null);
    }
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID) || !this._changedRelationIDs.Contains((object) ((NodeID) nodeID).PrjLinkID))
      return;
    plan.Update();
  }

  public void Postprocess(IUpdatePlan plan)
  {
  }
}
