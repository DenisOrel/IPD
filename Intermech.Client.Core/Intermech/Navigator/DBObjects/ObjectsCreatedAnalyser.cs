
// Type: Intermech.Navigator.DBObjects.ObjectsCreatedAnalyser
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

public class ObjectsCreatedAnalyser : IUpdateAnalyser
{
  private IDictionary _newObjectIDs;

  public ObjectsCreatedAnalyser(IList<long> objIDs)
  {
    this._newObjectIDs = (IDictionary) new HybridDictionary();
    for (int index = 0; index < objIDs.Count; ++index)
      this._newObjectIDs.Add((object) objIDs[index], (object) null);
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID))
      return;
    this._newObjectIDs.Remove((object) ((NodeID) nodeID).ObjectID);
  }

  public void Postprocess(IUpdatePlan plan)
  {
    foreach (DictionaryEntry newObjectId in this._newObjectIDs)
      plan.Append((INodeID) new NodeID(new CreateObjectNodeParams(-1, (long) newObjectId.Key, 0L, 0L, 0L, -1, string.Empty, -1, 0L, 0L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L)));
  }
}
