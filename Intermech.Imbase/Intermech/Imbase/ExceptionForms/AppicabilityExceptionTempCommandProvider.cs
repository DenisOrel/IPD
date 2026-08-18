// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ExceptionForms.AppicabilityExceptionTempCommandProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.ExceptionForms;

internal class AppicabilityExceptionTempCommandProvider : ICommandsProvider
{
  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo mergedCommands = new CommandsInfo();
    if (items.Count == 1)
      mergedCommands.Add("LocateInParent", new CommandInfo(6, new ClickEventHandler(this.LocateInParent)));
    return mergedCommands;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return new CommandsInfo();
  }

  private void LocateInParent(
    ISelectedItems items,
    IServiceProvider viewservices,
    object additionalinfo)
  {
    ImbaseApplicablityException service = viewservices.GetService<ImbaseApplicablityException>();
    IDBTypedObjectID childObjId;
    if (service == null || service.ParentObjectId == 0L || service.ChildObjectInfo.Length == 0 || (childObjId = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID) == null)
      return;
    int relTypeId = -1;
    if (((IEnumerable<Tuple<long, int>>) service.ChildObjectInfo).Any<Tuple<long, int>>((Func<Tuple<long, int>, bool>) (x => x.Item1 == childObjId.ObjectID)))
      relTypeId = ((IEnumerable<Tuple<long, int>>) service.ChildObjectInfo).First<Tuple<long, int>>((Func<Tuple<long, int>, bool>) (x => x.Item1 == childObjId.ObjectID)).Item2;
    NodeIDPath path = new NodeIDPath((IDescriptor) new Descriptor(service.ParentObjectId));
    NavigatorTreeView treeView = Utils.OpenNewWindow(path.RootDescriptor, viewservices, new GetSupportedColumnsEventHandler(Utils.DefaultSupportedColumnsObjects), path).TreeView;
    NavigatorTreeNode firstNode = treeView.FindFirstNode((Func<NavigatorTreeNode, bool>) (x =>
    {
      long? objectId1 = ((NodeID) x.NodeID)?.ObjectID;
      long objectId2 = childObjId.ObjectID;
      if (!(objectId1.GetValueOrDefault() == objectId2 & objectId1.HasValue))
        return false;
      NodeID nodeId = (NodeID) x.NodeID;
      // ISSUE: explicit non-virtual call
      return nodeId != null && __nonvirtual (nodeId.RelationTypeID) == relTypeId;
    }), (Func<NavigatorTreeNode, bool>) (x => true), true);
    if (firstNode == null || firstNode.HasFocus)
      return;
    NodeIDPath nodeIdPath = treeView.GetNodeIDPath(firstNode);
    treeView.TryBrowse(nodeIdPath);
  }
}
