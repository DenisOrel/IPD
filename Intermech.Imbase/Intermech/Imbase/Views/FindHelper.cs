// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FindHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Imbase.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public static class FindHelper
{
  private static NavigatorTreeNode InternalSearchNode(
    NavigatorTreeNode parentNode,
    long destObjID,
    DataTable table)
  {
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    if (parentNode != null)
    {
      NavigatorTreeView tree = parentNode.Tree;
      bool flag1 = !parentNode.Expanded;
      if (flag1)
      {
        tree.ExpandNextNode(parentNode, true);
        while (!parentNode.Full)
          Thread.Sleep(50);
      }
      INode handler = parentNode.Handler;
      NavigatorTreeNodes children = parentNode.Children;
      if (handler != null && children != null && children.Count > 0)
      {
        foreach (NavigatorTreeNode parentNode1 in (List<NavigatorTreeNode>) children)
        {
          tree.FocusedNode = parentNode1;
          bool flag2 = parentNode1.NodeID.CategoryID == Intermech.Imbase.Consts.RootNodeCategoryID;
          IDBObjectID objID = handler.GetData(parentNode1.NodeID, typeof (IDBObjectID)) as IDBObjectID;
          if (objID == null || objID.Value == 0L)
            navigatorTreeNode = FindHelper.InternalSearchNode(parentNode1, destObjID, table);
          else if (objID.Value == destObjID)
            navigatorTreeNode = parentNode1;
          else if (table.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt64(x["F_OBJECT_ID"]) == objID.Value)) != null)
            navigatorTreeNode = FindHelper.InternalSearchNode(parentNode1, destObjID, table);
          else
            continue;
          if (navigatorTreeNode != null | flag2)
            break;
        }
      }
      if (navigatorTreeNode == null & flag1)
        tree.SetNodeExpanded(parentNode, false);
    }
    return navigatorTreeNode;
  }

  private static TreeNode InternalSearchNode(TreeNode parentNode, long destNodeID, DataTable table)
  {
    TreeNodeCollection nodes = parentNode.Nodes;
    if (nodes.Count > 0)
    {
      foreach (TreeNode parentNode1 in nodes)
      {
        long objectId = (parentNode1.Tag as NodeInfo).ObjectId;
        if (objectId == destNodeID)
          return parentNode1;
        if (table.Select($"F_OBJECT_ID={objectId}").Length != 0)
        {
          parentNode1.Expand();
          return FindHelper.InternalSearchNode(parentNode1, destNodeID, table);
        }
      }
    }
    return parentNode;
  }

  public static NavigatorTreeNode SearchNodeByNodeID(NavigatorTreeNode startNode, long destObjID)
  {
    NavigatorTreeNode navigatorTreeNode = (NavigatorTreeNode) null;
    DataTable dataTable = FindHelper.GetDataTable(destObjID);
    NavigatorTreeNode parentNode = startNode;
    NavigatorTreeView tree = parentNode.Tree;
    bool backgroundTreeTasks = OptimizationSettings.BackgroundTreeTasks;
    bool autoScrollOnExpand = tree.AutoScrollOnExpand;
    try
    {
      tree.LockTreeEvents();
      tree.AutoScrollOnExpand = false;
      OptimizationSettings.BackgroundTreeTasks = false;
      do
      {
        navigatorTreeNode = FindHelper.InternalSearchNode(parentNode, destObjID, dataTable);
        if (navigatorTreeNode == null)
        {
          parentNode = parentNode.Parent;
          if (parentNode == null)
            break;
        }
        else
          break;
      }
      while (parentNode.Level > 0);
    }
    finally
    {
      OptimizationSettings.BackgroundTreeTasks = backgroundTreeTasks;
      tree.AutoScrollOnExpand = autoScrollOnExpand;
      tree.UnlockTreeEvents();
      if (navigatorTreeNode != null)
      {
        tree.FocusedNode = (NavigatorTreeNode) null;
        tree.FocusedNode = navigatorTreeNode;
      }
    }
    return navigatorTreeNode;
  }

  internal static DataTable GetDataTable(long destNodeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetFoldersForObjects(sessionKeeper.Session.SessionGUID, new long[1]
      {
        destNodeID
      }, (long[]) null);
  }

  public static TreeNode SearchNodeByNodeID(TreeNode parentNode, long destNodeID)
  {
    DataTable dataTable = FindHelper.GetDataTable(destNodeID);
    parentNode.Expand();
    return FindHelper.InternalSearchNode(parentNode, destNodeID, dataTable);
  }

  public static bool IsValidNode(NavigatorTreeNode node)
  {
    bool flag = false;
    if (node != null)
    {
      NavigatorTreeView tree = node.Tree;
      flag = tree != null && !tree.IsDisposed;
    }
    return flag;
  }
}
