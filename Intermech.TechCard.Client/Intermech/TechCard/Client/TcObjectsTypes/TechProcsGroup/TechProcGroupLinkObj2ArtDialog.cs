// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup.TechProcGroupLinkObj2ArtDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.ObjectTypeSupport.TechProcess.TechProcsGroup;
using Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcsGroup;

/// <summary>Диалог привязки изделий к элементу ГТП/ТТП</summary>
public class TechProcGroupLinkObj2ArtDialog : Form
{
  private List<TechCardUtils.SostavTreeItem> _gtpSostavItems;
  private ObjInfoItem _gtpObjectInfo;
  private RelInfoItem _gtpElemRelInfo;
  private List<ObjInfoItem> _procRouteObjs = new List<ObjInfoItem>();
  private Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>> _procRoute2RefData;
  /// <summary>Фоновая служба загрузки данных</summary>
  private TechProcGroupLinkObj2ArtLoadTask _backgroundTask;
  /// <summary>Контрол со списком МО</summary>
  internal TechProcGroupArtSimpleView _techProcGroupASV;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel;
  private Button btnApply;
  private Button btnCancel;
  private CheckBox chbLinkChildObjects;

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomComponent()
  {
    this._techProcGroupASV = new TechProcGroupArtSimpleView(true);
    this.tableLayoutPanel.Controls.Add((Control) this._techProcGroupASV, 0, 0);
    this.tableLayoutPanel.SetColumnSpan((Control) this._techProcGroupASV, 4);
    this.tableLayoutPanel.SetRowSpan((Control) this._techProcGroupASV, 1);
    this._techProcGroupASV.Dock = DockStyle.Fill;
    this._techProcGroupASV.tlArts.CheckBoxes = CheckBoxesStyle.ThreeState;
    this._techProcGroupASV.Name = "techProcGroupASV";
    this._techProcGroupASV.TabIndex = 0;
    this._techProcGroupASV.miArtLinkMode.Visible = false;
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this._techProcGroupASV.tlArts.StateImageList = service.ImageList;
      this._techProcGroupASV.tlArts.CheckedStateIndex = service.ImageIndex("imgChecked");
      this._techProcGroupASV.tlArts.UncheckedStateIndex = service.ImageIndex("imgUnchecked");
      this._techProcGroupASV.tlArts.GrayedStateIndex = service.ImageIndex("imgGrayed");
    }
    else
      this._techProcGroupASV.tlArts.StateImageList = (ImageList) null;
    this._techProcGroupASV.AfterLoadData += new LoadEventHandler(this.AfterLoadData_Event);
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControls() => this.btnApply.Enabled = this.ProcRoute2RefData != null;

  /// <summary>
  /// 
  /// </summary>
  private void LinkNodesToArticle()
  {
    if (this._procRoute2RefData == null)
      return;
    List<Gtp2EtpRefObjData> gtp2etpObjList1 = new List<Gtp2EtpRefObjData>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool recursive = this.chbLinkChildObjects.Checked;
      List<Gtp2EtpRefObjData> gtpItemList = new List<Gtp2EtpRefObjData>(this._techProcGroupASV.tlArts.Nodes.Count);
      foreach (TreeListNode node1 in this._techProcGroupASV.tlArts.Nodes)
      {
        if (node1.CheckState != CheckState.Indeterminate)
        {
          foreach (TreeListNode node2 in node1.Nodes)
          {
            if (node2.CheckState != CheckState.Indeterminate)
            {
              ObjInfoItem objProcRouteInfo = ((EtpProcRoute2ArtInfo) node2.Tag).ObjProcRouteInfo;
              if (this._procRoute2RefData.ContainsKey(objProcRouteInfo))
              {
                List<Gtp2EtpRefObjData> gtp2etpObjList2 = this._procRoute2RefData[objProcRouteInfo];
                Gtp2EtpRefObjData gtp2EtpRefObjData1 = (Gtp2EtpRefObjData) null;
                foreach (Gtp2EtpRefObjData gtp2EtpRefObjData2 in gtp2etpObjList2)
                {
                  if (gtp2EtpRefObjData2.ItemInfo.Equals((TypedInfoItem) this._gtpElemRelInfo))
                  {
                    gtp2EtpRefObjData1 = gtp2EtpRefObjData2;
                    break;
                  }
                }
                if (gtp2EtpRefObjData1 != null)
                {
                  if (gtp2EtpRefObjData1.ObjRefIDs.Count != 0 && node2.CheckState == CheckState.Unchecked)
                    gtp2etpObjList1.Add(gtp2EtpRefObjData1);
                  if (node2.CheckState == CheckState.Checked && gtp2EtpRefObjData1.ObjRefIDs.Count == 0 | recursive)
                  {
                    gtpItemList.Clear();
                    gtpItemList.Add(gtp2EtpRefObjData1);
                    TechProcGroupUtils.CreateEtpObject(gtpItemList, gtp2etpObjList2, sessionKeeper.Session, recursive);
                  }
                }
              }
            }
          }
        }
      }
      if (gtp2etpObjList1.Count <= 0)
        return;
      TechProcGroupUtils.RemoveEtpObjects(gtp2etpObjList1, sessionKeeper.Session);
    }
  }

  /// <summary>Конструктор</summary>
  public TechProcGroupLinkObj2ArtDialog()
  {
    this.InitializeComponent();
    this.InitializeCustomComponent();
    this.UpdateControls();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1425);
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="treeView"></param>
  /// <param name="gtpElemRelInfo"></param>
  /// <returns></returns>
  public bool ShowDialog(NavigatorTreeView treeView, RelInfoItem gtpElemRelInfo)
  {
    if (treeView == null || treeView.SelectedNodes.Length == 0 || (TypedInfoItem) gtpElemRelInfo == (TypedInfoItem) null || gtpElemRelInfo.RelationID == 0L)
      return false;
    List<TechCardUtils.SostavTreeItem> sostavItems = new List<TechCardUtils.SostavTreeItem>();
    NavigatorTreeNode node = treeView.SelectedNodes[0];
    TechCardUtils.SostavTreeItem sostavTreeItem1 = (TechCardUtils.SostavTreeItem) null;
    TechCardUtils.SostavTreeItem sostavTreeItem2 = (TechCardUtils.SostavTreeItem) null;
    for (; node != null; node = node.Parent)
    {
      NavigatorTreeNode navigatorTreeNode = node;
      if (navigatorTreeNode.Level != 0)
      {
        INode nodeHandler = treeView.GetNodeHandler(node);
        IDBTypedObjectID data1 = (IDBTypedObjectID) nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBTypedObjectID));
        if (sostavTreeItem1 != null)
          sostavTreeItem1.ProjID = data1.ObjectID;
        IDBRelationID data2 = (IDBRelationID) nodeHandler.GetData(navigatorTreeNode.NodeID, typeof (IDBRelationID));
        sostavTreeItem1 = new TechCardUtils.SostavTreeItem(0L, data1.ObjectID, data2 != null ? data2.Value : 0L, data2 != null ? data2.RelationType : -1, data1.ObjectType);
        sostavItems.Add(sostavTreeItem1);
        if (MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem1.ObjectTypeID, TechCardConsts.ObjectTypes.TechProcGroupID) || MetaDataHelper.IsObjectTypeChildOf(sostavTreeItem1.ObjectTypeID, TechCardConsts.ObjectTypes.TechProcTipovID))
        {
          sostavTreeItem2 = sostavTreeItem1;
          break;
        }
      }
      else
        break;
    }
    if (sostavTreeItem2 == null)
      return false;
    if (sostavItems.Count > 0)
    {
      long partId = sostavItems[0].PartID;
      if (partId != 0L)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<TechCardUtils.SostavSortedTreeItem> childSostavTree = TechCardUtils.GetChildSostavTree(partId, sessionKeeper.Session, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, true);
          sostavItems.Capacity = Math.Min(sostavItems.Capacity, sostavItems.Count + childSostavTree.Count);
          foreach (TechCardUtils.SostavTreeItem sostavTreeItem3 in childSostavTree)
            sostavItems.Add(sostavTreeItem3);
        }
      }
    }
    return this.ShowDialog(sostavItems, new ObjInfoItem(sostavTreeItem2.PartID, sostavTreeItem2.ObjectTypeID), gtpElemRelInfo);
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="sostavItems">Дерево навигатора</param>
  /// <param name="gtpObjectInfo">Ид. версии ГТП</param>
  /// <param name="gtpElemRelInfo">Cвязь с текущим элементом ГТП</param>
  /// <returns></returns>
  public bool ShowDialog(
    List<TechCardUtils.SostavTreeItem> sostavItems,
    ObjInfoItem gtpObjectInfo,
    RelInfoItem gtpElemRelInfo)
  {
    if (sostavItems == null || (TypedInfoItem) gtpObjectInfo == (TypedInfoItem) null || gtpObjectInfo.ObjectID == 0L || (TypedInfoItem) gtpElemRelInfo == (TypedInfoItem) null || gtpElemRelInfo.RelationID == 0L)
      return false;
    this._gtpSostavItems = sostavItems;
    this._gtpObjectInfo = gtpObjectInfo;
    this._gtpElemRelInfo = gtpElemRelInfo;
    this._techProcGroupASV.GtpObjectInfo = gtpObjectInfo;
    int num = (int) this.ShowDialog();
    return this.DialogResult == DialogResult.OK;
  }

  /// <summary>Ид. версии ГТП</summary>
  public ObjInfoItem GtpObjectInfo => this._gtpObjectInfo;

  /// <summary>Cвязь с текущим элементом ГТ</summary>
  public RelInfoItem GtpElemRelInfo => this._gtpElemRelInfo;

  /// <summary>Состав ГТП</summary>
  internal List<TechCardUtils.SostavTreeItem> GtpSostavItems => this._gtpSostavItems;

  /// <summary>Список Мо для текущего ГТП</summary>
  internal List<ObjInfoItem> ProcRouteIDs => this._procRouteObjs;

  /// <summary>Ccылки на етп, разбитые по маршрутам</summary>
  internal Dictionary<ObjInfoItem, List<Gtp2EtpRefObjData>> ProcRoute2RefData
  {
    get => this._procRoute2RefData;
    set
    {
      if (this._procRoute2RefData == value)
        return;
      this._procRoute2RefData = value;
      this.UpdateControls();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e) => this.LinkNodesToArticle();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="target"></param>
  private void AfterLoadData_Event(object target)
  {
    if (!(target is TreeList treeList))
      return;
    this._procRouteObjs.Clear();
    foreach (TreeListNode node1 in treeList.Nodes)
    {
      node1.CheckState = CheckState.Indeterminate;
      foreach (TreeListNode node2 in node1.Nodes)
      {
        node2.CheckState = CheckState.Indeterminate;
        ArtViewNode tag = (ArtViewNode) node2.Tag;
        if (tag != null)
          this._procRouteObjs.Add(tag.ObjProcRouteInfo);
      }
    }
    IBackgroundTaskView service = ServiceUtils.GetService<IBackgroundTaskView>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    this._backgroundTask = new TechProcGroupLinkObj2ArtLoadTask(this);
    service.AddTask((IBackgroundTask) this._backgroundTask);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcGroupLinkObj2ArtDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._backgroundTask != null && this._backgroundTask.Active)
      this._backgroundTask.Terminate();
    this._backgroundTask = (TechProcGroupLinkObj2ArtLoadTask) null;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcGroupLinkObj2ArtDialog));
    this.tableLayoutPanel = new TableLayoutPanel();
    this.chbLinkChildObjects = new CheckBox();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.tableLayoutPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
    this.tableLayoutPanel.Controls.Add((Control) this.chbLinkChildObjects, 1, 1);
    this.tableLayoutPanel.Controls.Add((Control) this.btnCancel, 3, 1);
    this.tableLayoutPanel.Controls.Add((Control) this.btnApply, 2, 1);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    componentResourceManager.ApplyResources((object) this.chbLinkChildObjects, "chbLinkChildObjects");
    this.chbLinkChildObjects.Name = "chbLinkChildObjects";
    this.chbLinkChildObjects.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel);
    this.Name = nameof (TechProcGroupLinkObj2ArtDialog);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.TechProcGroupLinkObj2ArtDialog_FormClosed);
    this.tableLayoutPanel.ResumeLayout(false);
    this.tableLayoutPanel.PerformLayout();
    this.ResumeLayout(false);
  }
}
