// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupLinkArt2ObjLoadTask
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.TechCard.Client.BackgroundTask;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>Класс загрузки составов ЕТП И ГТП</summary>
internal class TechProcGroupLinkArt2ObjLoadTask : TechCardBackgroundTask
{
  /// <summary>Правило подбора версий</summary>
  private string _filtrationOwnerId;
  /// <summary>Ид. версии маршрута обработки</summary>
  private ObjInfoItem _procRouteInfo;
  /// <summary>Ид. версии элемента ГТП</summary>
  private ObjInfoItem _tpGroupInfo;
  /// <summary>Диалог привязки элементов ГТП к МО</summary>
  private TechProcGroupLinkArt2ObjDialog _techProcGroupDlg;
  /// <summary>Gtp object's composition</summary>
  private List<TechCardUtils.SostavSortedTreeItem> _sostavItems;

  /// <summary>Загрузка полной структуры ГТП</summary>
  private void LoadGtpStructure()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._sostavItems = TechCardUtils.GetChildSostavTree(((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
      {
        this._tpGroupInfo
      }).ToList<ObjInfoItem>(1), sessionKeeper.Session, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, true, (ConditionStructure[]) null, new Dictionary<string, ColumnDescriptor>(3)
      {
        {
          "cad00344-306c-11d8-b4e9-00304f19f545",
          new ColumnDescriptor((object) -26, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Guid, SortOrders.NONE, 0)
        }
      }, filtrationOwnerId: this._filtrationOwnerId);
  }

  /// <summary>Загрузка структуры ЕТП</summary>
  private void LoadEtpStructure()
  {
    if (this._sostavItems == null || this._sostavItems.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Dictionary<Guid, RelInfoItem> relGuid2InfoCache = new Dictionary<Guid, RelInfoItem>(this._sostavItems.Count);
      foreach (TechCardUtils.SostavSortedTreeItem sostavItem in this._sostavItems)
      {
        object obj;
        if (sostavItem != null && sostavItem.LinkID != 0L && sostavItem.Values.TryGetValue("cad00344-306c-11d8-b4e9-00304f19f545", out obj) && obj != DBNull.Value && GuidHelper.IsGuid(obj.ToString()))
          relGuid2InfoCache.Add(new Guid(obj.ToString()), new RelInfoItem(sostavItem.LinkID, sostavItem.LinkTypeID));
      }
      List<Gtp2EtpRefData> etpRelIdList = TechProcGroupUtils.GetEtpRelIDList(relGuid2InfoCache, this._procRouteInfo, sessionKeeper.Session, this._filtrationOwnerId);
      Dictionary<TypedInfoItem, Gtp2EtpRefData> dictionary = new Dictionary<TypedInfoItem, Gtp2EtpRefData>();
      foreach (Gtp2EtpRefData gtp2EtpRefData in etpRelIdList)
      {
        if (gtp2EtpRefData != null && !dictionary.ContainsKey(gtp2EtpRefData.ItemInfo))
          dictionary.Add(gtp2EtpRefData.ItemInfo, gtp2EtpRefData);
      }
      Gtp2EtpRefData refData = (Gtp2EtpRefData) null;
      Dictionary<RelInfoItem, Gtp2EtpRefObjData> data = new Dictionary<RelInfoItem, Gtp2EtpRefObjData>();
      foreach (TechCardUtils.SostavSortedTreeItem sostavItem in this._sostavItems)
      {
        if (sostavItem != null && sostavItem.LinkID != 0L)
        {
          RelInfoItem relInfoItem = new RelInfoItem(sostavItem.LinkID, sostavItem.LinkTypeID);
          if (dictionary.TryGetValue((TypedInfoItem) relInfoItem, out refData))
            data.Add(relInfoItem, new Gtp2EtpRefObjData(refData, (TechCardUtils.SostavTreeItem) sostavItem));
          else
            data.Add(relInfoItem, new Gtp2EtpRefObjData((TypedInfoItem) relInfoItem, GtpRefDataType.gritGtpRelation, (Dictionary<TypedInfoItem, TypedInfoItem>) null, (TechCardUtils.SostavTreeItem) sostavItem));
        }
      }
      this.UpdateEtpStructute((object) data);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="data"></param>
  private void UpdateEtpStructute(object data)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new TechProcGroupLinkArt2ObjLoadTask.InvokeObjCallback(this.UpdateEtpStructute), data);
    }
    else
    {
      if (!(data is Dictionary<RelInfoItem, Gtp2EtpRefObjData>))
        return;
      this._techProcGroupDlg.GtpObjList = data as Dictionary<RelInfoItem, Gtp2EtpRefObjData>;
    }
  }

  /// <summary>Clear node's state</summary>
  private void ClearCheckState()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new TechProcGroupLinkArt2ObjLoadTask.InvokeCallback(this.ClearCheckState));
    }
    else
    {
      foreach (NavigatorTreeNode node in (List<NavigatorTreeNode>) this._techProcGroupDlg.TreeView.Nodes)
        this.ClearCheckState(node);
    }
  }

  /// <summary>Clear node's state</summary>
  /// <param name="node"></param>
  private void ClearCheckState(NavigatorTreeNode node)
  {
    if (node is TechcardNavTreeNode node1)
    {
      node1.SetCheckStateInternal(CheckState.Indeterminate);
      this._techProcGroupDlg.TreeView.UpdateTreeNode((NavigatorTreeNode) node1);
    }
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      this.ClearCheckState(child);
  }

  /// <summary>Update node states</summary>
  private void UpdateCheckState()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new TechProcGroupLinkArt2ObjLoadTask.InvokeCallback(this.UpdateCheckState));
    }
    else
    {
      long num = 0;
      foreach (NavigatorTreeNode node in (List<NavigatorTreeNode>) this._techProcGroupDlg.TreeView.Nodes)
      {
        this.Value = (object) ++num;
        this.UpdateCheckState(node);
      }
    }
  }

  /// <summary>Update node states</summary>
  /// <param name="node"></param>
  private void UpdateCheckState(NavigatorTreeNode node)
  {
    if (node is TechcardNavTreeNode node1 && node1.NodeID != null && node1.NodeID.CategoryID == 1 && this._techProcGroupDlg.TreeView.GetNodeHandler((NavigatorTreeNode) node1).GetData(node1.NodeID, typeof (IDBRelationID)) is IDBRelationID data)
    {
      CheckState state = TechProcGroupLinkArt2ObjLoadTask.Obj_CalcState(new RelInfoItem(data.Value, data.RelationType), this._techProcGroupDlg.GtpObjList);
      node1.SetCheckStateInternal(state);
      this._techProcGroupDlg.TreeView.UpdateTreeNode((NavigatorTreeNode) node1);
    }
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      this.UpdateCheckState(child);
  }

  /// <summary>Основная процедура потока</summary>
  protected override void CustomThreadProc()
  {
    this._event.WaitOne();
    this._minValue = 0;
    this._maxValue = 3;
    this.Value = (object) 0;
    this.Name = LocalizationHolder.rm.GetString("TechCard.Client_346");
    this._event.WaitOne();
    this.LoadGtpStructure();
    this.Value = (object) 1;
    this.Name = LocalizationHolder.rm.GetString("TechCard.Client_345");
    this._event.WaitOne();
    this.LoadEtpStructure();
    this.Value = (object) 2;
    this.Name = LocalizationHolder.rm.GetString("TechCard.Client_347");
    this._event.WaitOne();
    this.UpdateCheckState();
    this.Value = (object) 3;
    this._event.WaitOne();
  }

  /// <summary>Constructor</summary>
  /// <param name="techProcGroupDlg"></param>
  public TechProcGroupLinkArt2ObjLoadTask(TechProcGroupLinkArt2ObjDialog techProcGroupDlg)
    : base(LocalizationHolder.rm.GetString("TechCard.Client_282"))
  {
    this._canTerminate = true;
    this._tpGroupInfo = techProcGroupDlg.TpGroupInfo;
    this._procRouteInfo = techProcGroupDlg.ProcRouteInfo;
    this._techProcGroupDlg = techProcGroupDlg;
    this._filtrationOwnerId = VersionsRuleSources.GetCurrentWindowRule().OwnerId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._tpGroupInfo.ObjectID);
      this._name = string.Format(LocalizationHolder.rm.GetString("TechCard.Client_283"), (object) dbObject.Caption);
      this._minValue = 0;
      this._maxValue = 1;
    }
    using (FixEditingContext fixEditingContext = new FixEditingContext())
    {
      this._thread = new Thread(fixEditingContext.SendEditingContextToThread(new ThreadStart(((CustomThreadBackgroundTask) this).ThreadProc)));
      this.Start();
    }
  }

  /// <summary>Расчет статуса дерева</summary>
  /// <param name="gtpRelID"></param>
  /// <param name="gtpObjList"></param>
  /// <returns></returns>
  public static CheckState Obj_CalcState(
    RelInfoItem gtpRelID,
    Dictionary<RelInfoItem, Gtp2EtpRefObjData> gtpObjList)
  {
    CheckState checkState1 = CheckState.Unchecked;
    if ((TypedInfoItem) gtpRelID == (TypedInfoItem) null || gtpRelID.RelationID == 0L || !gtpObjList.ContainsKey(gtpRelID))
      return checkState1;
    Gtp2EtpRefObjData gtpObj = gtpObjList[gtpRelID];
    CheckState checkState2 = gtpObj.ObjRefIDs.Count > 0 ? CheckState.Indeterminate : checkState1;
    if (checkState2 != CheckState.Indeterminate || gtpObjList.Count == 0)
      return checkState2;
    foreach (Gtp2EtpRefObjData gtp2EtpRefObjData in gtpObjList.Values)
    {
      if (gtp2EtpRefObjData.SostavItem.ProjID == gtpObj.SostavItem.PartID && TechProcGroupLinkArt2ObjLoadTask.Obj_CalcState(gtp2EtpRefObjData.ItemInfo as RelInfoItem, gtpObjList) != CheckState.Checked)
        return checkState2;
    }
    return CheckState.Checked;
  }

  /// <summary>Internal delegate for invoke</summary>
  internal delegate void InvokeCallback();

  /// <summary>Internal delegate for invoke</summary>
  /// <param name="data"></param>
  internal delegate void InvokeObjCallback(object data);
}
