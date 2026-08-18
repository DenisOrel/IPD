// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FiltersNodeItems
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.DatabaseConfigurator;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;

#nullable disable
namespace Intermech.Security.EventLog;

internal class FiltersNodeItems : INodeItems
{
  private const string PropFilterGuid = "FilterGuid";

  public ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.Folder;

  public INode GetChild(INodeID nodeID)
  {
    INode node = Holder.Factory.GetNode(nodeID.CategoryID, nodeID.TypeID);
    (node as IFilterNode).Initialize((nodeID as FilterNodeID).Guid);
    return node;
  }

  public string GetAddress(INodeID nodeID)
  {
    Filter filter = FiltersManager.Filters.FindFilter((nodeID as FilterNodeID).Guid);
    return filter != null ? filter.Name : string.Empty;
  }

  public INodeID ParseAddress(string address)
  {
    Filter filter = FiltersManager.Filters.FindFilter(address);
    return filter != null ? (INodeID) new FilterNodeID(filter.Guid) : (INodeID) null;
  }

  public PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("FilterGuid", (object) ((FilterNodeID) nodeID).Guid);
    return persistentState;
  }

  public INodeID Deserialize(PersistentState persistentNodeID)
  {
    object obj = persistentNodeID.GetValue("FilterGuid");
    return obj != null && obj is Guid filterGuid ? (INodeID) new FilterNodeID(filterGuid) : (INodeID) null;
  }

  public object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IFilterGuid) ? (object) nodeID : (object) null;
  }

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    if (dataFormat == typeof (IFilterGuid))
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
    if (e is FilterEventArgs filterEventArgs)
    {
      switch (e.EventName)
      {
        case "FilterCreated":
          return (IUpdateAnalyser) new FilterCreatedAnalyser((INodeItems) this, filterEventArgs.FilterGuid);
        case "FilterChanged":
          return (IUpdateAnalyser) new FilterChangedAnalyser((INodeItems) this, filterEventArgs.FilterGuid);
        case "FilterRemoved":
          return (IUpdateAnalyser) new FilterRemovedAnalyser((INodeItems) this, filterEventArgs.FilterGuid);
      }
    }
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;
}
