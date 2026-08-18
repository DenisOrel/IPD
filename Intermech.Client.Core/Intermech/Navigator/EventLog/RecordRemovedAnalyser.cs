
// Type: Intermech.Navigator.EventLog.RecordRemovedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections;


namespace Intermech.Navigator.EventLog;

internal class RecordRemovedAnalyser : IUpdateAnalyser
{
  private INodeItems _owner;
  private IList _eventIDs;

  public RecordRemovedAnalyser(INodeItems owner, IList eventIDs)
  {
    this._owner = owner;
    this._eventIDs = eventIDs;
  }

  void IUpdateAnalyser.Preprocess(IUpdatePlan plan)
  {
  }

  void IUpdateAnalyser.Process(INodeID nodeID, IUpdatePlan plan)
  {
    IEventID data = (IEventID) this._owner.GetData(nodeID, typeof (IEventID));
    if (data == null || !this._eventIDs.Contains((object) data.Value))
      return;
    plan.Remove();
  }

  void IUpdateAnalyser.Postprocess(IUpdatePlan plan)
  {
  }
}
