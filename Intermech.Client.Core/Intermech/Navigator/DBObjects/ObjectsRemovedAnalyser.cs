
// Type: Intermech.Navigator.DBObjects.ObjectsRemovedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

public class ObjectsRemovedAnalyser : IUpdateAnalyser
{
  private Hashtable _removedObjectIDs;

  public ObjectsRemovedAnalyser(IList<long> objIDs)
  {
    this._removedObjectIDs = new Hashtable();
    for (int index = 0; index < objIDs.Count; ++index)
      this._removedObjectIDs.Add((object) objIDs[index], (object) null);
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID nodeId) || !this._removedObjectIDs.Contains((object) nodeId.ObjectID))
      return;
    plan.Remove();
  }

  public void Postprocess(IUpdatePlan plan)
  {
  }
}
