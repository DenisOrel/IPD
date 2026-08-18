
// Type: Intermech.Navigator.EventLog.EventsNodeItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Navigator.EventLog;

public class EventsNodeItems : INodeItems
{
  private const string PropEventId = "EventId";
  private const string PropObjectId = "PropObjectId";

  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.None;

  public INode GetChild(INodeID nodeID) => (INode) null;

  public string GetAddress(INodeID nodeID) => (string) null;

  public INodeID ParseAddress(string address) => (INodeID) null;

  public PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("EventId", (object) ((EventNodeID) nodeID).EventID);
    persistentState.AddValue("PropObjectId", (object) ((EventNodeID) nodeID).ObjectID);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistNodeID)
  {
    object eventID = persistNodeID.GetValue("EventId");
    object obj = persistNodeID.GetValue("PropObjectId");
    return eventID != null && eventID is long && obj != null && obj is long objectID ? (INodeID) new EventNodeID((long) eventID, objectID) : (INodeID) null;
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IEventID))
      return (object) nodeID;
    EventNodeID eventNodeId = nodeID as EventNodeID;
    if (dataFormat == typeof (IDescriptor) || dataFormat == typeof (ICanOpenInNewWindow))
    {
      long objectId = eventNodeId.ObjectID;
      if (eventNodeId != null && objectId != 0L)
      {
        if (dataFormat == typeof (IDescriptor))
          return (object) new Descriptor(objectId);
        if (dataFormat == typeof (ICanOpenInNewWindow))
          return (object) new CanOpenInNewWindow();
      }
    }
    return (object) null;
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    if (dataFormat == typeof (IEventID))
    {
      for (int index = 0; index < nodeIDs.Count; ++index)
        data[index] = (object) nodeIDs[index];
    }
    return data;
  }

  public IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return e is RecordEventArgs recordEventArgs && e.EventName == "RecordRemoved" ? (IUpdateAnalyser) new RecordRemovedAnalyser((INodeItems) this, recordEventArgs.EventIDs) : (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;
}
