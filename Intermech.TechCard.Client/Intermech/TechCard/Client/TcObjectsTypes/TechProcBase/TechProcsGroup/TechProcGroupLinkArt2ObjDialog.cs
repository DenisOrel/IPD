// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup.TechProcGroupLinkArt2ObjDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Configuration;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcBase.TechProcsGroup;

/// <summary>
/// Summary description for TechProcGroupLinkArt2ObjDialog
/// </summary>
public class TechProcGroupLinkArt2ObjDialog : Form
{
  /// <summary>Category for root descriptor</summary>
  private Guid _rootCategoryGuid = Guid.Empty;
  /// <summary>
  /// 
  /// </summary>
  private int _rootCategoryID;
  /// <summary>Proc route object id</summary>
  private ObjInfoItem _procRouteInfo;
  /// <summary>Group tech process object id</summary>
  private ObjInfoItem _tpGroupInfo;
  /// <summary>
  /// Gtp object's composition with links to etp
  /// Key - gtp relation's id
  /// </summary>
  private Dictionary<RelInfoItem, Gtp2EtpRefObjData> _gtpObjList;
  /// <summary>
  /// 
  /// </summary>
  private ICommandManager _commandManager;
  /// <summary>
  /// 
  /// </summary>
  private IServiceContainer _services;
  /// <summary>
  /// 
  /// </summary>
  private INotificationService _notificationService;
  /// <summary>Фоновая задача загрузки данных</summary>
  private IBackgroundTask _backgroundTask;
  /// <summary>Techcard treeview</summary>
  private TechCardNavTreeViewControl _treeView;
  private ContextMenuStrip cmsMain;
  private ToolStripMenuItem tsmiSelectAll;
  private ToolStripMenuItem tsmiClearAll;
  private ToolStripMenuItem tsmiInvertAll;
  private ToolStripSeparator tsmiSep1;
  private ToolStripMenuItem tsmiExpandAll;
  private ToolStripMenuItem tsmiCollapseAll;
  private Panel pnlBottom;
  private Button btnCancel;
  private Button btnApply;
  private IContainer components;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcGroupLinkArt2ObjDialog));
    this.cmsMain = new ContextMenuStrip(this.components);
    this.tsmiSelectAll = new ToolStripMenuItem();
    this.tsmiClearAll = new ToolStripMenuItem();
    this.tsmiInvertAll = new ToolStripMenuItem();
    this.tsmiSep1 = new ToolStripSeparator();
    this.tsmiExpandAll = new ToolStripMenuItem();
    this.tsmiCollapseAll = new ToolStripMenuItem();
    this.pnlBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.cmsMain.SuspendLayout();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    this.cmsMain.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.tsmiSelectAll,
      (ToolStripItem) this.tsmiClearAll,
      (ToolStripItem) this.tsmiInvertAll,
      (ToolStripItem) this.tsmiSep1,
      (ToolStripItem) this.tsmiExpandAll,
      (ToolStripItem) this.tsmiCollapseAll
    });
    this.cmsMain.Name = "cmsMain";
    componentResourceManager.ApplyResources((object) this.cmsMain, "cmsMain");
    this.cmsMain.Opening += new CancelEventHandler(this.cmsMain_Opening);
    this.tsmiSelectAll.Name = "tsmiSelectAll";
    componentResourceManager.ApplyResources((object) this.tsmiSelectAll, "tsmiSelectAll");
    this.tsmiSelectAll.Click += new EventHandler(this.tsmiSelectAll_Click);
    this.tsmiClearAll.Name = "tsmiClearAll";
    componentResourceManager.ApplyResources((object) this.tsmiClearAll, "tsmiClearAll");
    this.tsmiClearAll.Click += new EventHandler(this.tsmiClearAll_Click);
    this.tsmiInvertAll.Name = "tsmiInvertAll";
    componentResourceManager.ApplyResources((object) this.tsmiInvertAll, "tsmiInvertAll");
    this.tsmiInvertAll.Click += new EventHandler(this.tsmiInsertAll_Click);
    this.tsmiSep1.Name = "tsmiSep1";
    componentResourceManager.ApplyResources((object) this.tsmiSep1, "tsmiSep1");
    this.tsmiExpandAll.Name = "tsmiExpandAll";
    componentResourceManager.ApplyResources((object) this.tsmiExpandAll, "tsmiExpandAll");
    this.tsmiExpandAll.Click += new EventHandler(this.tsmiExpandAll_Click);
    this.tsmiCollapseAll.Name = "tsmiCollapseAll";
    componentResourceManager.ApplyResources((object) this.tsmiCollapseAll, "tsmiCollapseAll");
    this.tsmiCollapseAll.Click += new EventHandler(this.tsmiCollapseAll_Click);
    this.pnlBottom.Controls.Add((Control) this.btnCancel);
    this.pnlBottom.Controls.Add((Control) this.btnApply);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.Cancel;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (TechProcGroupLinkArt2ObjDialog);
    this.ShowInTaskbar = false;
    this.FormClosed += new FormClosedEventHandler(this.TechProcGroupLinkArt2ObjDialog_FormClosed);
    this.cmsMain.ResumeLayout(false);
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>Initialize services</summary>
  private void InitializeServices()
  {
    this._commandManager = ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false);
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._services = (IServiceContainer) new ServiceContainer();
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    if (this._notificationService == null)
      return;
    this._services.AddService(typeof (INotificationService), (object) this._notificationService);
  }

  /// <summary>Initialize custom controls</summary>
  private void InitializeCustomComponent()
  {
    this._rootCategoryGuid = Guid.NewGuid();
    this._rootCategoryID = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false).Register(this._rootCategoryGuid);
    this._treeView = new TechCardNavTreeViewControl();
    this._treeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this._treeView.Location = new Point(8, 8);
    this._treeView.MultiSelect = false;
    this._treeView.ContextMenuStrip = this.cmsMain;
    this._treeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this._treeView.Name = "treeView";
    this._treeView.TabIndex = 0;
    this._treeView.DisableColumnsSorting = true;
    this.Controls.Add((Control) this._treeView);
    this._treeView.Dock = DockStyle.Fill;
    this._treeView.BringToFront();
    this._treeView.AfterCreateNode += new EventHandler<NodeEventArgs>(this.Node_AfterCreateEvent);
    this._treeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    this._treeView.Services = (System.IServiceProvider) this._services;
    this._treeView.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(this._rootCategoryID, TechCardConsts.ObjectTypes.TechBaseObjectID, 0L, TechCardConsts.ObjectTypes.TechBaseObjectID, -1, "", RelatedObjectsRole.Composition, (ITechCompositionFilter) null);
    this._treeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending), descriptor);
  }

  /// <summary>Collect recursive nodes</summary>
  /// <param name="node"></param>
  /// <param name="nodeList"></param>
  /// <returns></returns>
  private bool CollectAllNodes(NavigatorTreeNode node, ref List<NavigatorTreeNode> nodeList)
  {
    if (nodeList == null)
      nodeList = new List<NavigatorTreeNode>();
    if (node == null)
      return false;
    if (!nodeList.Contains(node))
      nodeList.Add(node);
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      this.CollectAllNodes(child, ref nodeList);
    return nodeList.Count > 0;
  }

  /// <summary>Calculate node's status</summary>
  /// <param name="node"></param>
  /// <returns></returns>
  private CheckState Node_CalcState(NavigatorTreeNode node)
  {
    CheckState checkState = CheckState.Unchecked;
    NavigatorTreeNode navigatorTreeNode = node;
    if (navigatorTreeNode != null && navigatorTreeNode.NodeID.CategoryID == 1 && this._treeView.GetNodeHandler(node).GetData(navigatorTreeNode.NodeID, typeof (IDBRelationID)) is IDBRelationID data)
      checkState = TechProcGroupLinkArt2ObjLoadTask.Obj_CalcState(new RelInfoItem(data.Value, data.RelationType), this._gtpObjList);
    return checkState;
  }

  /// <summary>
  /// 
  /// </summary>
  private void LinkNodesToArticle()
  {
    List<NavigatorTreeNode> nodeList = new List<NavigatorTreeNode>();
    if (!this.CollectAllNodes(this._treeView.RootNode, ref nodeList) || nodeList == null)
      return;
    Dictionary<TypedInfoItem, bool> dictionary1 = new Dictionary<TypedInfoItem, bool>(nodeList.Count);
    Dictionary<TypedInfoItem, bool> dictionary2 = new Dictionary<TypedInfoItem, bool>(nodeList.Count);
    foreach (NavigatorTreeNode node in nodeList)
    {
      NavigatorTreeNode navigatorTreeNode = node;
      if (navigatorTreeNode != null && navigatorTreeNode.NodeID.CategoryID == 1 && this._treeView.GetNodeHandler(node).GetData(navigatorTreeNode.NodeID, typeof (IDBRelationID)) is IDBRelationID data)
      {
        RelInfoItem key = new RelInfoItem(data.Value, data.RelationType);
        if (node.CheckState == CheckState.Unchecked)
          dictionary2.Add((TypedInfoItem) key, node.HasChildren && node.Children.Count == 0);
        else
          dictionary1.Add((TypedInfoItem) key, node.HasChildren && node.Children.Count == 0);
      }
    }
    List<Gtp2EtpRefObjData> gtp2etpObjList1 = new List<Gtp2EtpRefObjData>();
    Dictionary<bool, List<Gtp2EtpRefObjData>> dictionary3 = new Dictionary<bool, List<Gtp2EtpRefObjData>>();
    dictionary3.Add(true, new List<Gtp2EtpRefObjData>(this._gtpObjList.Count));
    dictionary3.Add(false, new List<Gtp2EtpRefObjData>(this._gtpObjList.Count));
    foreach (KeyValuePair<RelInfoItem, Gtp2EtpRefObjData> gtpObj in this._gtpObjList)
    {
      Gtp2EtpRefObjData gtp2EtpRefObjData = gtpObj.Value;
      if (gtp2EtpRefObjData != null)
      {
        if (gtp2EtpRefObjData.ObjRefIDs == null || gtp2EtpRefObjData.ObjRefIDs.Count == 0)
        {
          if (dictionary1.ContainsKey(gtp2EtpRefObjData.ItemInfo))
            dictionary3[dictionary1[gtp2EtpRefObjData.ItemInfo]].Add(gtp2EtpRefObjData);
        }
        else if ((!((TypedInfoItem) this._tpGroupInfo != (TypedInfoItem) null) || gtp2EtpRefObjData.SostavItem == null || gtp2EtpRefObjData.SostavItem.PartID != this._tpGroupInfo.ObjectID) && !dictionary1.ContainsKey(gtp2EtpRefObjData.ItemInfo) && dictionary2.ContainsKey(gtp2EtpRefObjData.ItemInfo))
          gtp2etpObjList1.Add(gtp2EtpRefObjData);
      }
    }
    List<CategoryValue> categoryList = (List<CategoryValue>) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.StartLogHistory();
      try
      {
        if (gtp2etpObjList1.Count > 0)
          TechProcGroupUtils.RemoveEtpObjects(gtp2etpObjList1, sessionKeeper.Session);
        if (dictionary3.Count > 0)
        {
          Gtp2EtpRefData etpProcObjId = TechProcGroupUtils.GetEtpProcObjID(this._tpGroupInfo, this._procRouteInfo, sessionKeeper.Session);
          if (etpProcObjId != null)
          {
            List<Gtp2EtpRefObjData> gtp2etpObjList2 = new List<Gtp2EtpRefObjData>((IEnumerable<Gtp2EtpRefObjData>) this._gtpObjList.Values);
            gtp2etpObjList2.Add(new Gtp2EtpRefObjData(etpProcObjId, (TechCardUtils.SostavTreeItem) null));
            foreach (KeyValuePair<bool, List<Gtp2EtpRefObjData>> keyValuePair in dictionary3)
              TechProcGroupUtils.CreateEtpObject(keyValuePair.Value, gtp2etpObjList2, sessionKeeper.Session, keyValuePair.Key);
          }
        }
      }
      finally
      {
        sessionKeeper.Session.StopLogHistory();
        categoryList = sessionKeeper.Session.GetModificationsHistoryList();
      }
    }
    if (categoryList == null || categoryList.Count == 0)
      return;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    TechcardClientControlsUtils.FireNotificationEvents(service, (IEnumerable<CategoryValue>) categoryList, (object) null);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.TreeView.Services = (System.IServiceProvider) null;
      ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false).Unregister(this._rootCategoryID);
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected virtual void LoadSettings(bool loadFormPosition)
  {
    if (loadFormPosition)
    {
      Form form = new Form();
      form.Name = this.Name.Equals(string.Empty) ? this.GetType().ToString() : this.Name;
      FormStorage.LoadLayout((Control) form);
      this.Location = form.Location;
      this.Size = form.Size;
    }
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this._treeView);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected virtual void SaveSettings(bool saveFormPosition)
  {
    if (saveFormPosition)
    {
      Form form = new Form();
      form.Name = this.Name.Equals(string.Empty) ? this.GetType().ToString() : this.Name;
      form.Location = this.Location;
      form.Size = this.Size;
      FormStorage.SaveLayout((Control) form);
    }
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    string name = this.GetType().ToString();
    IConfiguration config = service.Open(name) ?? service.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this._treeView);
  }

  /// <summary>Конструктор</summary>
  public TechProcGroupLinkArt2ObjDialog()
  {
    this._procRouteInfo = (ObjInfoItem) null;
    this._tpGroupInfo = (ObjInfoItem) null;
    this._gtpObjList = new Dictionary<RelInfoItem, Gtp2EtpRefObjData>();
    this.InitializeComponent();
    this.InitializeServices();
    this.InitializeCustomComponent();
  }

  /// <summary>Вызов диалога</summary>
  /// <param name="procRouteInfo">Ид. версии маршрута обработки</param>
  /// <param name="tpGroupInfo">Ид. версии группового ТП</param>
  /// <returns></returns>
  public bool ShowDialog(ObjInfoItem procRouteInfo, ObjInfoItem tpGroupInfo)
  {
    this.LoadSettings(true);
    if ((TypedInfoItem) tpGroupInfo == (TypedInfoItem) null || (TypedInfoItem) procRouteInfo == (TypedInfoItem) null || tpGroupInfo.ObjectID == 0L || procRouteInfo.ObjectID == 0L)
      return false;
    this._procRouteInfo = procRouteInfo;
    this._tpGroupInfo = tpGroupInfo;
    this._treeView.Build((IDescriptor) new TechCompositionDescriptor(this._rootCategoryID, 0, this._tpGroupInfo.ObjectID, -1, (IEnumerable<int>) TechCardConsts.RelTypes.TechCompositionGtpRelations, "", RelatedObjectsRole.Composition, (ITechCompositionFilter) null, (IEnumerable<NodeColumnID>) null)
    {
      FiltrationOwnerId = VersionsRuleSources.GetCurrentWindowRule().OwnerId
    });
    IBackgroundTaskView service = ServiceUtils.GetService<IBackgroundTaskView>((object) ApplicationServices.Container, false);
    if (service != null)
    {
      this._backgroundTask = (IBackgroundTask) new TechProcGroupLinkArt2ObjLoadTask(this);
      service.AddTask(this._backgroundTask);
    }
    int num = (int) this.ShowDialog();
    this.SaveSettings(true);
    return this.DialogResult == DialogResult.OK;
  }

  /// <summary>
  /// 
  /// </summary>
  internal Dictionary<RelInfoItem, Gtp2EtpRefObjData> GtpObjList
  {
    get => this._gtpObjList;
    set => this._gtpObjList = value;
  }

  /// <summary>
  /// 
  /// </summary>
  internal TechCardNavTreeViewControl TreeView => this._treeView;

  /// <summary>Ид. версии маршрута обработки</summary>
  public ObjInfoItem ProcRouteInfo => this._procRouteInfo;

  /// <summary>Ид. версии группового ТП</summary>
  public ObjInfoItem TpGroupInfo => this._tpGroupInfo;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnApply_Click(object sender, EventArgs e) => this.LinkNodesToArticle();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Node_AfterCreateEvent(object sender, NodeEventArgs e)
  {
    if (!(e.Node is TechcardNavTreeNode node))
      return;
    if (node.Parent != null && (node.Parent.CheckState == CheckState.Checked || node.Parent.CheckState == CheckState.Unchecked))
      node.SetCheckStateInternal(node.Parent.CheckState);
    else
      node.SetCheckStateInternal(this.Node_CalcState((NavigatorTreeNode) node));
    this._treeView.UpdateTreeNode((NavigatorTreeNode) node);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  internal void Node_CheckStateChangingEvent(object sender, CheckStateEventArgs e)
  {
    if (!(e.Node is TechcardNavTreeNode node))
      return;
    if (e.NewValue == CheckState.Checked || e.NewValue == CheckState.Unchecked)
    {
      node.SetCheckStateInternal(e.NewValue);
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
      {
        if (child.CheckState != e.NewValue)
          child.CheckState = e.NewValue;
      }
      if (node.Parent == null)
        return;
      if (e.NewValue == CheckState.Unchecked && node.Parent.CheckState != CheckState.Indeterminate)
      {
        node.Parent.CheckState = CheckState.Indeterminate;
      }
      else
      {
        CheckState checkState = e.NewValue;
        foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Parent.Children)
        {
          if (child != node && child.CheckState != e.NewValue)
            checkState = CheckState.Indeterminate;
        }
        if (checkState == CheckState.Unchecked || checkState == node.Parent.CheckState)
          return;
        node.Parent.CheckState = checkState;
      }
    }
    else
    {
      if (node.Parent == null || node.Parent.CheckState == e.NewValue)
        return;
      node.Parent.CheckState = e.NewValue;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cmsMain_Opening(object sender, CancelEventArgs e)
  {
    bool flag = this._treeView != null && this._treeView.RootNode != null && this._treeView.RootNode.Children.Count > 0;
    this.tsmiExpandAll.Enabled = this.tsmiCollapseAll.Enabled = flag;
    this.tsmiClearAll.Enabled = this.tsmiSelectAll.Enabled = this.tsmiInvertAll.Enabled = flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiExpandAll_Click(object sender, EventArgs e)
  {
    if (this._treeView == null || this._treeView.RootNode == null || !(this._treeView.RootNode is TechcardNavTreeNode rootNode))
      return;
    this._treeView.AfterCreateNode -= new EventHandler<NodeEventArgs>(this.Node_AfterCreateEvent);
    try
    {
      rootNode.ExpandNode(true);
    }
    finally
    {
      this._treeView.AfterCreateNode += new EventHandler<NodeEventArgs>(this.Node_AfterCreateEvent);
    }
    foreach (NavigatorTreeNode node1 in (List<NavigatorTreeNode>) this._treeView.RootNode.ExtractNodes(false))
    {
      if (node1 is TechcardNavTreeNode node2)
        node2.SetCheckStateInternal(this.Node_CalcState((NavigatorTreeNode) node2));
    }
    this._treeView.UpdateRows();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiCollapseAll_Click(object sender, EventArgs e)
  {
    if (this._treeView == null || this._treeView.RootNode == null || !(this._treeView.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.CollapseNode(true);
    rootNode.ExpandNode(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiSelectAll_Click(object sender, EventArgs e)
  {
    if (this._treeView == null || this._treeView.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._treeView.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Select, true);
    }
    this._treeView.UpdateRows();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiClearAll_Click(object sender, EventArgs e)
  {
    if (this._treeView == null || this._treeView.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._treeView.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Clear, true);
    }
    this._treeView.UpdateRows();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsmiInsertAll_Click(object sender, EventArgs e)
  {
    if (this._treeView == null || this._treeView.RootNode == null)
      return;
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) this._treeView.RootNode.Children)
    {
      if (child is TechcardNavTreeNode techcardNavTreeNode)
        techcardNavTreeNode.SetCheckStateCommon(NavTreeNodeSelectMode.Invert, true);
    }
    this._treeView.UpdateRows();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcGroupLinkArt2ObjDialog_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._backgroundTask != null && this._backgroundTask.Active)
    {
      this._backgroundTask.Terminate();
      this._backgroundTask = (IBackgroundTask) null;
    }
    if (this._treeView == null)
      return;
    this._treeView.Services = (System.IServiceProvider) null;
  }
}
