// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Process_Route.ProcRouteListViewDlg
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Process_Route;

/// <summary>
/// 
/// </summary>
public class ProcRouteListViewDlg : ProcRouteListBaseDlg
{
  /// <summary>
  /// Proc route objects list that contain ETP objects for current GTP
  /// </summary>
  private List<long> _procObjectIdList = new List<long>();

  /// <summary>Constructor</summary>
  /// <param name="artObjList">Ид. версий изделия</param>
  /// <param name="gtpObjId">Ид. версии дочернего объекта (ГТП)</param>
  /// <param name="procRouteId">Selected proc routes</param>
  public ProcRouteListViewDlg(List<long> artObjList, long gtpObjId, long[] procRouteId)
    : base(artObjList, gtpObjId, procRouteId)
  {
    this.InitializeData();
    this.LoadData();
  }

  /// <summary>Initialize data</summary>
  private void InitializeData()
  {
    this._objChildTypeID = MetaDataHelper.GetObjectTypeID(TechCardConsts.ObjectTypes.TechProcEdinGUID);
  }

  /// <summary>Load data</summary>
  private void LoadData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Gtp2EtpRefData etpObjIdList = TechProcGroupUtils.GetEtpObjIDList(new ObjInfoItem(this._objChildID, this._objChildTypeID), sessionKeeper.Session);
      if (etpObjIdList == null || etpObjIdList.ObjRefIDs == null || etpObjIdList.ObjRefIDs.Count == 0)
        return;
      List<TechCardUtils.SostavTreeItem> parentSostavTree = TechCardUtils.GetParentSostavTree(ObjInfoHelper.GetObjectInfoList((IEnumerable<TypedInfoItem>) new List<TypedInfoItem>((IEnumerable<TypedInfoItem>) etpObjIdList.ObjRefIDs.Values)), sessionKeeper.Session, new int[1]
      {
        TechCardConsts.RelTypes.TechRelationID
      }, false, new List<ConditionStructure>()
      {
        new ConditionStructure(-7, RelationalOperators.Equal, (object) TechCardConsts.ObjectTypes.ProcRoutingID, LogicalOperators.NONE, 0, false)
      }.ToArray(), (Dictionary<string, ColumnDescriptor>) null);
      this._procObjectIdList.Capacity = parentSostavTree.Count;
      foreach (TechCardUtils.SostavTreeItem sostavTreeItem in parentSostavTree)
      {
        if (sostavTreeItem != null)
          this._procObjectIdList.Add(sostavTreeItem.ProjID);
      }
    }
  }

  /// <summary>Create node event</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void DoCreateNode(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node1 = e?.Node;
    NavigatorTreeView navigatorTreeView = sender as NavigatorTreeView;
    if (node1 == null || navigatorTreeView == null || !(node1 is TechcardNavTreeNode node2))
      return;
    INode nodeHandler = navigatorTreeView.GetNodeHandler((NavigatorTreeNode) node2);
    if (nodeHandler == null)
      return;
    NodeID nodeId = node2.NodeID as NodeID;
    if (this._procObjectIdList.Count > 1)
    {
      IDBRelationID data = nodeId != null ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBRelationID)) as IDBRelationID : (IDBRelationID) null;
      if (data != null && data.Value == 0L)
      {
        node2.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
        return;
      }
    }
    IDBTypedObjectID data1 = nodeId != null ? nodeHandler.GetData((INodeID) nodeId, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    if (data1 == null || !MetaDataHelper.IsObjectTypeChildOf(data1.ObjectType, TechCardConsts.ObjectTypes.ProcRoutingID))
    {
      node2.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.None;
    }
    else
    {
      long objectId = data1.ObjectID;
      if (objectId == 0L)
        return;
      if (this._procObjectIdList.Contains(objectId))
        node2.SetCheckStateInternal(CheckState.Indeterminate);
      else
        node2.SetCheckStateInternal(this._procRouteIDList == null || !this._procRouteIDList.Contains(objectId) ? CheckState.Unchecked : CheckState.Checked);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtId"></param>
  /// <param name="objTpId"></param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  public static bool ShowDialog(long objArtId, long objTpId, ref long procRouteId)
  {
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcRouteListViewDlg), ref procRouteId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtId"></param>
  /// <param name="objTpId"></param>
  /// <param name="procRouteId"></param>
  /// <returns></returns>
  public static bool ShowDialog(long objArtId, long objTpId, ref long[] procRouteId)
  {
    return ProcRouteListBaseDlg.ShowDialog(objArtId, objTpId, typeof (ProcRouteListViewDlg), ref procRouteId, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objArtList"></param>
  /// <param name="objTpId"></param>
  /// <param name="procRouteId">Items to select</param>
  /// <param name="procRoute2ArtList">Selected route data with article info</param>
  /// <returns></returns>
  public static bool ShowDialog(
    List<long> objArtList,
    long objTpId,
    long[] procRouteId,
    out Dictionary<long, long> procRoute2ArtList)
  {
    return ProcRouteListBaseDlg.ShowDialog(objArtList, objTpId, typeof (ProcRouteListViewDlg), procRouteId, true, out procRoute2ArtList);
  }
}
