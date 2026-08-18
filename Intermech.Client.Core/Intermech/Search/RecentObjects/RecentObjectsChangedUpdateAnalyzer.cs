
// Type: Intermech.Search.RecentObjects.RecentObjectsChangedUpdateAnalyzer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsChangedUpdateAnalyzer : IUpdateAnalyser
{
  private CurrentUserRecentObjectsNode _currentUserRecentObjectsNode;
  private RecentObjectsChangedEventArgs _recentObjectsChangedEventArgs;
  private List<long> _addedObjects = new List<long>();

  public RecentObjectsChangedUpdateAnalyzer(
    CurrentUserRecentObjectsNode currentUserRecentObjectsNode,
    RecentObjectsChangedEventArgs recentObjectsChangedEventArgs)
  {
    if (currentUserRecentObjectsNode == null)
      throw new ArgumentNullException(nameof (currentUserRecentObjectsNode));
    if (recentObjectsChangedEventArgs == null)
      throw new ArgumentNullException(nameof (recentObjectsChangedEventArgs));
    this._currentUserRecentObjectsNode = currentUserRecentObjectsNode;
    this._recentObjectsChangedEventArgs = recentObjectsChangedEventArgs;
    this._addedObjects.AddRange((IEnumerable<long>) this._recentObjectsChangedEventArgs.AddedRecentObjects);
  }

  public void Postprocess(IUpdatePlan plan)
  {
    foreach (long addedObject in this._addedObjects)
    {
      NodeID partialNodeID = new NodeID(-1, addedObject, 0L, 0L, 0L, -1, string.Empty, -1, 0L, -1L, ObjectFiltrationState.fsNotRequired, 0L, 0L, string.Empty, 0L, Guid.Empty, 0L);
      if (this._currentUserRecentObjectsNode.FolderSlots.Count > 0)
        partialNodeID.Cookie = (object) new PartCookie(this._currentUserRecentObjectsNode.FolderSlots[0].UniqueId);
      plan.Append((INodeID) partialNodeID);
    }
  }

  public void Preprocess(IUpdatePlan plan)
  {
  }

  public void Process(INodeID nodeID, IUpdatePlan plan)
  {
    if (!(nodeID is NodeID nodeId))
      return;
    if (((IEnumerable<long>) this._recentObjectsChangedEventArgs.RemovedRecentObjects).Contains<long>(nodeId.ObjectID) || ((IEnumerable<long>) this._recentObjectsChangedEventArgs.RemovedRecentObjects).Contains<long>(-nodeId.ObjectID))
    {
      plan.Remove();
    }
    else
    {
      if (!this._addedObjects.Contains(nodeId.ObjectID) && !this._addedObjects.Contains(-nodeId.ObjectID))
        return;
      this._addedObjects.Remove(nodeId.ObjectID);
      this._addedObjects.Remove(-nodeId.ObjectID);
    }
  }
}
