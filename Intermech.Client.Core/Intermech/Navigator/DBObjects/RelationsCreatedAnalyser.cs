
// Type: Intermech.Navigator.DBObjects.RelationsCreatedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.DBObjects;

public class RelationsCreatedAnalyser : IUpdateAnalyser
{
  private IDictionary _newRelationIDs;

  public RelationsCreatedAnalyser(IList<long> relationIDs)
  {
    this._newRelationIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < relationIDs.Count; ++index)
      this._newRelationIDs.Add((object) relationIDs[index], (object) null);
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID))
      return;
    this._newRelationIDs.Remove((object) ((NodeID) nodeID).PrjLinkID);
  }

  public void Postprocess(IUpdatePlan plan)
  {
    foreach (DictionaryEntry newRelationId in this._newRelationIDs)
      plan.Append((INodeID) new NodeID(new CreateObjectNodeParams(-1, 0L, 0L, 0L, (long) newRelationId.Key, -1, string.Empty, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L)));
  }
}
