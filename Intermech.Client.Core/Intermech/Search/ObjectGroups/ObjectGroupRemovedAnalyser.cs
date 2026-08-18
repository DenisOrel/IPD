
// Type: Intermech.Search.ObjectGroups.ObjectGroupRemovedAnalyser
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.ObjectGroups;

public sealed class ObjectGroupRemovedAnalyser : IUpdateAnalyser, INavigatorTreeViewUpdateAnalyzer
{
  private long[] _removedRelationIds;

  public ObjectGroupRemovedAnalyser(long[] removedRelationIds)
  {
    this._removedRelationIds = removedRelationIds != null ? removedRelationIds : throw new ArgumentNullException();
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
  }

  public void Postprocess(IUpdatePlan plan)
  {
  }

  public void Process(NavigatorTreeNode node, IUpdatePlan updatePlan)
  {
    if (node == null)
      throw new ArgumentNullException();
    if (updatePlan == null)
      throw new ArgumentNullException();
    if (!(node.NodeID is ObjectGroupNodeID))
      return;
    if (!node.Full)
      node.Fetch();
    if (node.Children.Count == 0)
      updatePlan.Remove();
    if (node.Children.Count != 1 || !(node.Children[0].NodeID is NodeID) || !((IEnumerable<long>) this._removedRelationIds).Contains<long>(((NodeID) node.Children[0].NodeID).PrjLinkID))
      return;
    updatePlan.Remove();
  }
}
