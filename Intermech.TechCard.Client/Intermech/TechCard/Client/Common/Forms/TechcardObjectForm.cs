// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Common.Forms.TechcardObjectForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Configuration;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Common.Forms;

/// <summary>Форма для отображения списка объектов</summary>
public class TechcardObjectForm : Form
{
  /// <summary>
  /// 
  /// </summary>
  private IDescriptor _descriptor;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  /// <summary>
  /// 
  /// </summary>
  protected internal Button btnApply;
  /// <summary>
  /// 
  /// </summary>
  protected internal Button btnCancel;
  /// <summary>
  /// 
  /// </summary>
  protected internal TechCardNavTreeViewControl tolcTechObjList;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem1;
  private MenuButtonItem mbiRefresh;
  private MenuButtonItem mbiOpenInNewWindow;
  private MenuButtonItem mbiProperty;
  private MenuButtonItem mbiSearch;
  private MenuButtonItem mbiExpandTree;
  private MenuButtonItem mbiCollapseTree;
  private MenuButtonItem mbiSetupColumns;

  /// <summary>
  /// 
  /// </summary>
  private void UpdateControlCommands()
  {
    IDBTypedObjectID typedObjId;
    this.GetCurrentObject(out typedObjId);
    this.mbiOpenInNewWindow.Enabled = this.mbiOpenInNewWindow.Visible = this.mbiProperty.Enabled = this.mbiProperty.Visible = this.mbiSearch.Enabled = this.mbiSearch.Visible = typedObjId != null;
    this.mbiExpandTree.Enabled = this.mbiCollapseTree.Enabled = this.tolcTechObjList.RootNode?.Children != null && this.tolcTechObjList.RootNode.Children.Count > 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandName"></param>
  private bool NavigatorContextMenuInvoke(string commandName)
  {
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.tolcTechObjList.SelectedItems, this.tolcTechObjList.Services);
    if (commandsTable == null || !commandsTable.Contains(commandName))
      return false;
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, this.tolcTechObjList.Services);
    return true;
  }

  /// <summary>Инициализация служб</summary>
  private void InitializeServices()
  {
    this.tolcTechObjList.Services = (System.IServiceProvider) new ServiceContainer();
  }

  /// <summary>Де-инициализация служб</summary>
  private void UnInitializeServices()
  {
    if (this.tolcTechObjList == null)
      return;
    this.tolcTechObjList.Services = (System.IServiceProvider) null;
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomControls()
  {
    this.InitializeServices();
    this.tolcTechObjList.OnGetSupportedColumnsEventHandler += new GetSupportedColumnsEventHandler(TechCardNavTreeViewUtils.GetObjectColumnsOnly);
    string caption = "";
    this._descriptor = (IDescriptor) new HiveDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, caption);
    NodeColumnCollection columns = Intermech.Navigator.Utils.VersionColumns(NodeColumnSortOrder.Ascending, false);
    ServiceUtils.GetService<IColumnSchemes>((object) ApplicationServices.Container, false);
    this.tolcTechObjList.SetColumns(columns, this._descriptor);
  }

  /// <summary>Initialize custom settings</summary>
  private void InitializeCustomSettings() => this.LoadSettings(true);

  /// <summary>Загрузка расположения и размеров формы</summary>
  protected virtual void LoadSettings(bool loadFormPosition)
  {
    string name = !string.IsNullOrEmpty(this.Name) ? this.Name : this.GetType().ToString();
    if (loadFormPosition)
      TechCardFormUtils.LoadSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfigurationManager service = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    IConfiguration config = service.Open(name);
    if (this.tolcTechObjList == null)
      return;
    TechCardNavTreeViewUtils.LoadSettings(config, (NavigatorTreeView) this.tolcTechObjList);
  }

  /// <summary>Сохранение расположения и размеров формы</summary>
  protected virtual void SaveSettings(bool saveFormPosition)
  {
    string name = !string.IsNullOrEmpty(this.Name) ? this.Name : this.GetType().ToString();
    if (saveFormPosition)
      TechCardFormUtils.SaveSettings((Control) this, TechCardFormUtils.Mode.All);
    IConfiguration config = ServiceUtils.GetService<IConfigurationManager>((object) ApplicationServices.Container, false)?.Create(name);
    if (config == null)
      return;
    TechCardNavTreeViewUtils.SaveSettings(config, (NavigatorTreeView) this.tolcTechObjList);
  }

  /// <summary>Get current object info</summary>
  /// <param name="typedObjId"></param>
  /// <returns></returns>
  protected virtual bool GetCurrentObject(out IDBTypedObjectID typedObjId)
  {
    typedObjId = (IDBTypedObjectID) null;
    IFocusedItem focusedItem = this.tolcTechObjList.FocusedItem;
    if (focusedItem == null)
      return false;
    typedObjId = focusedItem.GetItemData(typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    return typedObjId != null;
  }

  /// <summary>Конструктор</summary>
  public TechcardObjectForm()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.InitializeCustomControls();
    this.InitializeCustomSettings();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="caption"></param>
  /// <param name="descriptor"></param>
  /// <returns></returns>
  public bool LoadData(string caption, IDescriptor descriptor)
  {
    this.Text = caption;
    if (descriptor == null)
      return false;
    this._descriptor = descriptor;
    this.tolcTechObjList.Build(this._descriptor);
    return true;
  }

  /// <summary>Управление доступностью кнопок</summary>
  public bool EnableBtnOk
  {
    get => this.btnApply.Enabled;
    set => this.btnApply.Enabled = value;
  }

  /// <summary>Управление отображением кнопок</summary>
  public bool ShowBtnOk
  {
    get => this.btnApply.Visible;
    set => this.btnApply.Visible = value;
  }

  /// <summary>Управление отображением кнопок</summary>
  public bool ShowBtnCancel
  {
    get => this.btnCancel.Visible;
    set => this.btnCancel.Visible = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechcardObjectForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings(true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechcardObjectForm_Load(object sender, EventArgs e) => this.LoadSettings(true);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiRefresh_Click(object sender, EventArgs e)
  {
    this.tolcTechObjList.Build(this._descriptor);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiCommonButton_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem))
      return;
    this.NavigatorContextMenuInvoke(menuButtonItem.CommandName);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiOpenInNewWindow_Click(object sender, EventArgs e)
  {
    IDBTypedObjectID typedObjId;
    if (!(sender is MenuButtonItem menuButtonItem) || this.NavigatorContextMenuInvoke(menuButtonItem.CommandName) || !this.GetCurrentObject(out typedObjId))
      return;
    TechCardClientConst.OpenObjectInNewWindow(typedObjId.ObjectID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiExpandTree_Click(object sender, EventArgs e)
  {
    if (this.tolcTechObjList.RootNode == null || !this.tolcTechObjList.RootNode.HasChildren)
      return;
    ServiceUtils.GetService<INavigatorTreeViewClientService>((object) ApplicationServices.Container, false).ExpandAll(this.tolcTechObjList.RootNode);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbiCollapseTree_Click(object sender, EventArgs e)
  {
    if (this.tolcTechObjList?.RootNode == null || !(this.tolcTechObjList.RootNode is TechcardNavTreeNode rootNode))
      return;
    rootNode.CollapseNode(true);
    rootNode.ExpandNode(false);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void contextMenuBarItem1_Click(object sender, EventArgs e)
  {
    this.UpdateControlCommands();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.components.Dispose();
      this.UnInitializeServices();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechcardObjectForm));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.btnApply = new Button();
    this.btnCancel = new Button();
    this.tolcTechObjList = new TechCardNavTreeViewControl();
    this.contextMenuBarItem1 = new ContextMenuBarItem();
    this.mbiRefresh = new MenuButtonItem();
    this.mbiOpenInNewWindow = new MenuButtonItem();
    this.mbiProperty = new MenuButtonItem();
    this.mbiSearch = new MenuButtonItem();
    this.mbiExpandTree = new MenuButtonItem();
    this.mbiCollapseTree = new MenuButtonItem();
    this.mbiSetupColumns = new MenuButtonItem();
    this.menuBar1 = new MenuBar();
    this.tableLayoutPanel1.SuspendLayout();
    this.tolcTechObjList.BeginInit();
    this.SuspendLayout();
    this.tableLayoutPanel1.AutoSize = true;
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
    this.tableLayoutPanel1.Controls.Add((Control) this.btnApply, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 1, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Bottom;
    this.tableLayoutPanel1.Location = new Point(0, 287);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 1;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Size = new Size(472, 29);
    this.tableLayoutPanel1.TabIndex = 5;
    this.btnApply.Anchor = AnchorStyles.Right;
    this.btnApply.DialogResult = DialogResult.OK;
    this.btnApply.ImeMode = ImeMode.NoControl;
    this.btnApply.Location = new Point(313, 3);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(75, 23);
    this.btnApply.TabIndex = 9;
    this.btnApply.Text = "ОК";
    this.btnCancel.Anchor = AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(394, 3);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 8;
    this.btnCancel.Text = "Отмена";
    this.tolcTechObjList.AllowDrop = true;
    this.tolcTechObjList.AllowMultiSelect = false;
    this.tolcTechObjList.AllowUserPinnedColumns = false;
    this.tolcTechObjList.CheckedNodesStates = (IDictionary<NodeIDPath, TechcardNavTreeNode.NodeStateKeeper>) componentResourceManager.GetObject("tolcTechObjList.CheckedNodesStates");
    this.tolcTechObjList.CheckoutMode = TechCheckoutMode.Manual;
    this.tolcTechObjList.CheckRootNode = false;
    this.tolcTechObjList.ContextMenuBarItem = this.contextMenuBarItem1;
    this.tolcTechObjList.DisableCheckedOutColumn = true;
    this.tolcTechObjList.DisableIMContextMenu = true;
    this.tolcTechObjList.DisableKeyDownEvents = true;
    this.tolcTechObjList.DisableKeyUpEvents = true;
    this.tolcTechObjList.DisablePacketsReading = false;
    this.tolcTechObjList.Dock = DockStyle.Fill;
    this.tolcTechObjList.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.tolcTechObjList.ImageList = (ImageList) null;
    this.tolcTechObjList.LineStyle = LineStyle.Dot;
    this.tolcTechObjList.Location = new Point(0, 0);
    this.tolcTechObjList.Name = "tolcTechObjList";
    this.tolcTechObjList.RowEvenStyle.WordWrap = false;
    this.tolcTechObjList.RowOddStyle.WordWrap = false;
    this.tolcTechObjList.RowSelectedStyle.WordWrap = false;
    this.tolcTechObjList.RowStyle.BorderColor = SystemColors.Control;
    this.tolcTechObjList.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.tolcTechObjList.RowStyle.BorderWidth = 1;
    this.tolcTechObjList.RowStyle.WordWrap = false;
    this.tolcTechObjList.SelectBeforeEdit = true;
    this.tolcTechObjList.ShowRootRow = false;
    this.tolcTechObjList.Size = new Size(472, 287);
    this.tolcTechObjList.SuppressErrorMessages = true;
    this.tolcTechObjList.TabIndex = 6;
    this.tolcTechObjList.Tag = (object) " ";
    this.contextMenuBarItem1.CommandName = "contextMenuBarItem";
    this.contextMenuBarItem1.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.mbiRefresh,
      (ToolbarItemBase) this.mbiOpenInNewWindow,
      (ToolbarItemBase) this.mbiProperty,
      (ToolbarItemBase) this.mbiSearch,
      (ToolbarItemBase) this.mbiExpandTree,
      (ToolbarItemBase) this.mbiCollapseTree,
      (ToolbarItemBase) this.mbiSetupColumns
    });
    this.contextMenuBarItem1.ShowText = true;
    this.contextMenuBarItem1.Click += new EventHandler(this.contextMenuBarItem1_Click);
    this.mbiRefresh.CommandName = "Refresh";
    this.mbiRefresh.Shortcut = Shortcut.CtrlR;
    this.mbiRefresh.ShowText = true;
    this.mbiRefresh.Text = "Обновить";
    this.mbiRefresh.Click += new EventHandler(this.mbiRefresh_Click);
    this.mbiOpenInNewWindow.CommandName = "OpenInNewWindow";
    this.mbiOpenInNewWindow.ShowText = true;
    this.mbiOpenInNewWindow.Text = "Открыть в новом окне";
    this.mbiOpenInNewWindow.Click += new EventHandler(this.mbiOpenInNewWindow_Click);
    this.mbiProperty.CommandName = "ParametersCard";
    this.mbiProperty.Shortcut = Shortcut.F4;
    this.mbiProperty.ShowText = true;
    this.mbiProperty.Text = "Свойства (Карточка)";
    this.mbiProperty.Click += new EventHandler(this.mbiCommonButton_Click);
    this.mbiSearch.CommandName = "SeekInTree";
    this.mbiSearch.Shortcut = Shortcut.CtrlF;
    this.mbiSearch.ShowText = true;
    this.mbiSearch.Text = "Найти в списке";
    this.mbiSearch.Click += new EventHandler(this.mbiCommonButton_Click);
    this.mbiExpandTree.BeginGroup = true;
    this.mbiExpandTree.CommandName = "ExpandNode";
    this.mbiExpandTree.ShowText = true;
    this.mbiExpandTree.Text = "Развернуть все";
    this.mbiExpandTree.Click += new EventHandler(this.mbiExpandTree_Click);
    this.mbiCollapseTree.CommandName = "CollapseNode";
    this.mbiCollapseTree.ShowText = true;
    this.mbiCollapseTree.Text = "Свернуть все";
    this.mbiCollapseTree.Click += new EventHandler(this.mbiCollapseTree_Click);
    this.mbiSetupColumns.BeginGroup = true;
    this.mbiSetupColumns.CommandName = "SetupColumns";
    this.mbiSetupColumns.ShowText = true;
    this.mbiSetupColumns.Text = "Настройка отображения ...";
    this.mbiSetupColumns.Click += new EventHandler(this.mbiCommonButton_Click);
    this.menuBar1.Guid = new Guid("4287165a-32c8-49f9-a71f-0696e541cb31");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem1
    });
    this.menuBar1.Location = new Point(0, 0);
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) null;
    this.menuBar1.Size = new Size(472, 26);
    this.menuBar1.TabIndex = 7;
    this.menuBar1.Text = "menuBar1";
    this.menuBar1.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(472, 316);
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.tolcTechObjList);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (TechcardObjectForm);
    this.Text = "Put caption here";
    this.FormClosed += new FormClosedEventHandler(this.TechcardObjectForm_FormClosed);
    this.Load += new EventHandler(this.TechcardObjectForm_Load);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tolcTechObjList.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
