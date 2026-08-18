
// Type: Intermech.Navigator.Controls.VersionsNavWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Окно навигатора для отображения версий объектов.</summary>
internal sealed class VersionsNavWindow : NavWindow
{
  private IVersionsDescriptor _descriptor;
  private static readonly Guid _persistStateGuid = new Guid("45284CB2-8A87-4358-BE52-9F663F4E4AD2");
  /// <summary>
  /// Флаг того, что поддерживаемые колонки для дерева установлены
  /// </summary>
  private bool _treeSupportedAdded;
  private ButtonItem bTree;
  private ButtonItem bList;
  private LabelItem lDate;
  private ButtonItem bOpenCalendar;

  public VersionsNavWindow()
    : this((IVersionsDescriptor) null)
  {
  }

  public VersionsNavWindow(IVersionsDescriptor descriptor)
  {
    this.InitializeComponent();
    this.bOpenCalendar.ImageIndex = Holder.NamedImageList.ImageIndex("imgCalendarGoTo");
    this.bTree.ImageIndex = Holder.NamedImageList.ImageIndex("imgVersionsTree");
    this.bList.ImageIndex = Holder.NamedImageList.ImageIndex("imgVersionsList");
    this.Guid = VersionsNavWindow._persistStateGuid;
    if (descriptor != null)
      this.SetDescriptor(descriptor);
    else
      this.TreeView.BuildTree += new EventHandler(this.TreeView_BuildTree);
  }

  public override void Activated()
  {
    base.Activated();
    this.TreeView.Build(this._descriptor as IDescriptor);
    this.TreeView.Browse(this._descriptor.Path);
  }

  private void SetDescriptor(IVersionsDescriptor descriptor)
  {
    this._descriptor = descriptor;
    this.RefreshTree();
    this.RefreshDate();
    this.RefreshButtons();
  }

  private void TreeView_BuildTree(object sender, EventArgs e)
  {
    if (!(sender is NavigatorTreeView navigatorTreeView))
      return;
    this.TreeView.BuildTree -= new EventHandler(this.TreeView_BuildTree);
    this.SetDescriptor(navigatorTreeView.RootDescriptor as IVersionsDescriptor);
  }

  protected override int GetTabImageIndex(INodeID nodeID)
  {
    return Holder.IconService == null || !(nodeID is VersionsHiveNodeID versionsHiveNodeId) ? base.GetTabImageIndex(nodeID) : Holder.IconService.IndexOf(versionsHiveNodeId.CategoryID, (int) versionsHiveNodeId.Mode, (object) null);
  }

  /// <summary>
  /// Обработчик события нажатия на пункты меню "Дерево версий" и "Список версий"
  /// </summary>
  private void ShowViewChanged(object sender, EventArgs e)
  {
    if ((this._descriptor.VisualMode != VersionsWindowVisualModes.LIST || !(((ToolbarItemBase) sender).CommandName == "Tree")) && (this._descriptor.VisualMode != VersionsWindowVisualModes.TREE || !(((ToolbarItemBase) sender).CommandName == "List")))
      return;
    long objectID;
    long id;
    int objectTypeID;
    string caption;
    this.GetSelectedNodeData(out objectID, out id, out objectTypeID, out caption);
    switch (((ToolbarItemBase) sender).CommandName)
    {
      case "List":
        this._descriptor = (IVersionsDescriptor) new ListVersionsDescriptor(objectID, id, objectTypeID, caption, this._descriptor.CurrentDate);
        break;
      default:
        this._descriptor = (IVersionsDescriptor) new TreeVersionsDescriptor(objectID, id, objectTypeID, caption, this._descriptor.CurrentDate);
        break;
    }
    this.RefreshTree();
    this.RefreshButtons();
  }

  private void RefreshTree()
  {
    if (!this._treeSupportedAdded)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this.TreeView.SupportedColumns.AddRange((IEnumerable<NodeColumn>) VersionsNode.VersionsTreeSupportedColumns(VersionsHelper.GetVersionsObjectTypes(sessionKeeper.Session, this._descriptor.ID), this._descriptor.VisualMode));
      this._treeSupportedAdded = true;
    }
    if (this.RootDescriptor == null || this.RootDescriptor != this._descriptor)
      this.RootDescriptor = this._descriptor as IDescriptor;
    this.SetTreeViewColumns();
    this.TreeView.Build(this._descriptor as IDescriptor);
    this.TreeView.Browse(this._descriptor.Path);
    this.Text = $"{LocalizationHolder.rm.GetString("Client.Core_1350")} {this._descriptor.ObjectCaption}";
  }

  private void RefreshDate()
  {
    this.lDate.Text = this._descriptor.CurrentDate == DateTime.MaxValue ? LocalizationHolder.rm.GetString("Client.Core_329") : LocalizationHolder.rm.GetString("Client.Core_330") + this._descriptor.CurrentDate.ToString("dd.MM.yyyy");
  }

  private void RefreshButtons()
  {
    this.bTree.Checked = this._descriptor.VisualMode == VersionsWindowVisualModes.TREE;
    this.bList.Checked = this._descriptor.VisualMode == VersionsWindowVisualModes.LIST;
  }

  private IDBTypedObjectID GetSelectedNodeData(
    out long objectID,
    out long id,
    out int objectTypeID,
    out string caption)
  {
    IDBTypedObjectID itemData = this.TreeView.SelectedItems.Count > 0 ? this.TreeView.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    objectID = itemData != null ? itemData.ObjectID : this._descriptor.ObjectID;
    id = itemData != null ? itemData.ID : this._descriptor.ID;
    caption = itemData != null ? itemData.Caption : this._descriptor.ObjectCaption;
    objectTypeID = itemData != null ? itemData.ObjectType : this._descriptor.ObjectTypeID;
    return itemData;
  }

  private void CurrentDate_Click(object sender, EventArgs e)
  {
    Intermech.Bars.ToolBar toolBar = this.bOpenCalendar.ToolBar;
    Rectangle buttonBounds = this.bOpenCalendar.ButtonBounds;
    int x1 = buttonBounds.X;
    buttonBounds = this.bOpenCalendar.ButtonBounds;
    int y1 = buttonBounds.Y + this.bOpenCalendar.ToolBar.Height + 5;
    Point p = new Point(x1, y1);
    Point screen = toolBar.PointToScreen(p);
    int num1 = screen.X + (int) byte.MaxValue;
    Rectangle workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int x2 = workingArea1.X;
    workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int width = workingArea1.Width;
    int num2 = x2 + width;
    if (num1 > num2)
      screen.X = Screen.PrimaryScreen.WorkingArea.X + Screen.PrimaryScreen.WorkingArea.Width - (int) byte.MaxValue;
    if (screen.X < Screen.PrimaryScreen.WorkingArea.X)
      screen.X = Screen.PrimaryScreen.WorkingArea.X;
    int num3 = screen.Y + 205;
    Rectangle workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y2 = workingArea2.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int height1 = workingArea2.Height;
    int num4 = y2 + height1;
    if (num3 > num4)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y3 = workingArea2.Y;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int height2 = workingArea2.Height;
      int num5 = y3 + height2 - 205;
      local.Y = num5;
    }
    int y4 = screen.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y5 = workingArea2.Y;
    if (y4 < y5)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y6 = workingArea2.Y;
      local.Y = y6;
    }
    DateTime dateTime = VersionsSelectDateForm.ShowDateForm(this._descriptor.CurrentDate, screen);
    if (this._descriptor.CurrentDate.Equals(dateTime))
      return;
    this._descriptor.CurrentDate = dateTime;
    this.RefreshTree();
    this.RefreshDate();
  }

  public new static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    if (guid != VersionsNavWindow._persistStateGuid)
      return (DockControl) null;
    NavWindowBase.OverrideTreeViewClass = typeof (VersionsNavigatorTreeView);
    return NavWindow.RestoreWindow((NavWindow) new VersionsNavWindow(), guid, persistString);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionsNavWindow));
    this.bTree = new ButtonItem();
    this.bList = new ButtonItem();
    this.lDate = new LabelItem();
    this.bOpenCalendar = new ButtonItem();
    this.pnTreeView.SuspendLayout();
    this.TreeView.BeginInit();
    this.SuspendLayout();
    int index = this.labelSpace.Index;
    this.tbTreePanel.Items.Insert(index, (ToolbarItemBase) this.bTree);
    this.tbTreePanel.Items.Insert(index + 1, (ToolbarItemBase) this.bList);
    this.tbTreePanel.Items.Insert(index + 2, (ToolbarItemBase) this.lDate);
    this.tbTreePanel.Items.Insert(index + 3, (ToolbarItemBase) this.bOpenCalendar);
    this.tbTreePanel.Size = new Size(475, 24);
    this.pnTreeView.Size = new Size(475, 352);
    this.ViewsManager.Location = new Point(478, 0);
    this.ViewsManager.Size = new Size(112 /*0x70*/, 352);
    this.spTreeView.Location = new Point(475, 0);
    this.TreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.TreeView.RowEvenStyle.WordWrap = false;
    this.TreeView.RowOddStyle.WordWrap = false;
    this.TreeView.RowSelectedStyle.BackColor = SystemColors.Highlight;
    this.TreeView.RowSelectedStyle.WordWrap = false;
    this.TreeView.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.TreeView.RowStyle.BorderColor = SystemColors.Control;
    this.TreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.TreeView.RowStyle.BorderWidth = 1;
    this.TreeView.RowStyle.WordWrap = false;
    this.TreeView.Size = new Size(475, 125);
    this.TreeViewControl.ViewsInTree.Location = new Point(0, 152);
    this.TreeViewControl.ViewsInTree.Size = new Size(475, 200);
    this.bTree.CommandName = "Tree";
    this.bTree.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1363");
    this.bTree.Click += new EventHandler(this.ShowViewChanged);
    this.bList.CommandName = "List";
    this.bList.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1364");
    this.bList.Click += new EventHandler(this.ShowViewChanged);
    this.lDate.BeginGroup = true;
    this.lDate.CommandName = "lDate";
    this.lDate.Text = "Label";
    this.bOpenCalendar.CommandName = "bOpenCalendar";
    this.bOpenCalendar.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1365");
    this.bOpenCalendar.Click += new EventHandler(this.CurrentDate_Click);
    this.History = (List<long>) componentResourceManager.GetObject("$this.History");
    this.Name = nameof (VersionsNavWindow);
    this.Size = new Size(590, 352);
    this.TreeListColumns = (NodeColumnCollection) componentResourceManager.GetObject("$this.TreeListColumns");
    this.pnTreeView.ResumeLayout(false);
    this.TreeView.EndInit();
    this.ResumeLayout(false);
  }
}
