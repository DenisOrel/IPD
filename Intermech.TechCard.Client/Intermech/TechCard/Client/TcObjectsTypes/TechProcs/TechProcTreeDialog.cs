// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechProcs.TechProcTreeDialog
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechProcs;

/// <summary>Диалог привязки элементов ГТП/ТТП к изделию</summary>
public class TechProcTreeDialog : Form
{
  /// <summary>
  /// 
  /// </summary>
  private long _tpObjectId;
  /// <summary>
  /// 
  /// </summary>
  private ArrayList _nodeList;
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
  /// <summary>
  /// 
  /// </summary>
  private NavigatorTreeView _treeView;
  private Panel pnlClient;
  private Panel pnlBottom;
  private Button btnApply;
  private Button btnCancel;

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechProcTreeDialog));
    this.pnlClient = new Panel();
    this.pnlBottom = new Panel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pnlClient, "pnlClient");
    this.pnlClient.Name = "pnlClient";
    this.pnlBottom.Controls.Add((Control) this.btnApply);
    this.pnlBottom.Controls.Add((Control) this.btnCancel);
    componentResourceManager.ApplyResources((object) this.pnlBottom, "pnlBottom");
    this.pnlBottom.Name = "pnlBottom";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.Name = "btnApply";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.pnlBottom);
    this.Name = nameof (TechProcTreeDialog);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.TechProcGroupTreeDialog_Load);
    this.pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeServices()
  {
    this._nodeList = new ArrayList();
    this._services = (IServiceContainer) new ServiceContainer();
    this._services.AddService(typeof (IViewState), (object) new ViewStateService());
    this._commandManager = ServiceUtils.GetService<ICommandManager>((object) ApplicationServices.Container, false);
    if (this._commandManager != null)
      this._services.AddService(typeof (ICommandManager), (object) this._commandManager);
    this._notificationService = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (this._notificationService == null)
      return;
    this._services.AddService(typeof (INotificationService), (object) this._notificationService);
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomComponent()
  {
    this._treeView = new NavigatorTreeView();
    this.TreeView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.TreeView.Location = new Point(8, 8);
    this.TreeView.MultiSelect = false;
    this.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
    this.TreeView.Name = "treeView";
    this.TreeView.Size = new Size(400, 336);
    this.TreeView.TabIndex = 0;
    this.pnlClient.Controls.Add((Control) this.TreeView);
    this.TreeView.Dock = DockStyle.Fill;
    this.TreeView.AfterCreateNode += new EventHandler<NodeEventArgs>(this.Node_AfterCreateEvent);
    this.TreeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    this.TreeView.Services = (System.IServiceProvider) this._services;
    this.TreeView.SetColumns(Intermech.Navigator.Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
  }

  /// <summary>Конструктор</summary>
  public TechProcTreeDialog()
  {
    this.InitializeComponent();
    this.InitializeServices();
    this.InitializeCustomComponent();
  }

  /// <summary>TreeView</summary>
  public NavigatorTreeView TreeView => this._treeView;

  /// <summary>Ид. версии техпроцесса</summary>
  public long TpObjectId => this._tpObjectId;

  /// <summary>Список узлов дерева</summary>
  public ArrayList NodeList => this._nodeList;

  /// <summary>Вызов диалога</summary>
  /// <param name="objectId">Ид. версии техпроцесса</param>
  /// <returns></returns>
  public bool ShowDialog(long objectId)
  {
    if (objectId == 0L)
      return false;
    this._tpObjectId = objectId;
    this.TreeView.Build((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId));
    int num = (int) this.ShowDialog();
    return this.DialogResult == DialogResult.OK;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.TreeView != null)
      this.TreeView.Services = (System.IServiceProvider) null;
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Node_AfterCreateEvent(object sender, NodeEventArgs e)
  {
    this.TreeView.CheckStateChanging -= new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    try
    {
      NavigatorTreeNode node = e.Node;
      if (this._nodeList.IndexOf((object) node) == -1)
        this._nodeList.Add((object) node);
      if (node.Parent == null || node.Parent.CheckState != CheckState.Checked)
        return;
      node.CheckState = node.Parent.CheckState;
    }
    finally
    {
      this.TreeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Node_CheckStateChangingEvent(object sender, CheckStateEventArgs e)
  {
    if (e.OldValue == e.NewValue)
      return;
    NavigatorTreeNode node = e.Node;
    if (e.NewValue != CheckState.Checked && e.NewValue != CheckState.Unchecked)
      return;
    this.TreeView.CheckStateChanging -= new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    try
    {
      node.CheckState = e.NewValue;
    }
    finally
    {
      this.TreeView.CheckStateChanging += new EventHandler<CheckStateEventArgs>(this.Node_CheckStateChangingEvent);
    }
    foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
    {
      if (child.CheckState != e.NewValue)
        child.CheckState = e.NewValue;
    }
    if (node.Parent == null || e.NewValue != CheckState.Unchecked || node.Parent.CheckState == e.NewValue)
      return;
    node.Parent.CheckState = e.NewValue;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechProcGroupTreeDialog_Load(object sender, EventArgs e)
  {
  }
}
