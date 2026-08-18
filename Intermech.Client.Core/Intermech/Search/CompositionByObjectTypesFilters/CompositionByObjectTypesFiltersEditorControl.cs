
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersEditorControl : UserControl, ISupportInitialize
{
  private long _objectVersionID;
  private long _selectedFilterVersionID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private NavigatorTreeView _navigatorTreeView;
  private ToolStrip _navigatorTreeViewToolStrip;
  private ContextMenuStrip _navigatorTreeViewContextMenuStrip;
  private ToolStripButton _addFilterToolStripButton;
  private ToolStripButton _removeFilterToolStripButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _loadFiltersToolStripButton;
  private ToolStripButton _saveFiltersToolStripButton;
  private PageViewsManager _pageViewsManager;
  private ToolStripMenuItem _addFilterToolStripMenuItem;
  private ToolStripMenuItem _removeFilterToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripMenuItem _loadFiltersToolStripMenuItem;
  private ToolStripMenuItem _saveFiltersToolStripMenuItem;
  private SplitContainer _splitContainer;

  public CompositionByObjectTypesFiltersEditorControl()
  {
    this.InitializeComponent();
    this._pageViewsManager.AllowedViews = new string[3]
    {
      "ObjectProperties",
      "RelationProperties",
      typeof (CompositionByObjectTypesFilterEditorView).Name
    };
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ObjectVersionID
  {
    get => this._objectVersionID;
    set
    {
      this._objectVersionID = value;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectVersionID);
        this._navigatorTreeView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
        this._navigatorTreeView.Build((IDescriptor) new CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersForObjectDescriptor(dbObject.ObjectType, this._objectVersionID));
      }
    }
  }

  public CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersEditorControlMemento GetMemento()
  {
    return new CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersEditorControlMemento()
    {
      TreeNodeColumns = this._navigatorTreeView.ReflectTreeColumsChanges(),
      SplitterPosition = (double) this._splitContainer.SplitterDistance / (double) this._splitContainer.Width
    };
  }

  public void SetMemento(
    CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersEditorControlMemento memento)
  {
    if (memento == null)
      throw new ArgumentNullException(nameof (memento));
    if (memento.TreeNodeColumns != null)
      this._navigatorTreeView.SetColumns(memento.TreeNodeColumns);
    this._splitContainer.SplitterDistance = (int) ((double) this._splitContainer.Width * memento.SplitterPosition);
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    this._navigatorTreeView.Services = (System.IServiceProvider) ServicesManager.ServiceContainer;
    this._pageViewsManager.Services = this._navigatorTreeView.Services;
  }

  private void AddFilterToolStripButton_Click(object sender, EventArgs e) => this.AddFilter();

  private void RemoveFilterToolStripButton_Click(object sender, EventArgs e) => this.RemoveFilter();

  private void LoadFiltersToolStripButton_Click(object sender, EventArgs e) => this.LoadFilters();

  private void SaveFiltersToolStripButton_Click(object sender, EventArgs e) => this.SaveFilters();

  private void NavigatorTreeView_SelectionChanged(object sender, EventArgs e)
  {
    if (this._navigatorTreeView.SelectedItem is NavigatorTreeNode)
    {
      NavigatorTreeNode selectedItem = (NavigatorTreeNode) this._navigatorTreeView.SelectedItem;
      if (selectedItem.NodeID is NodeID)
      {
        NodeID nodeId = (NodeID) selectedItem.NodeID;
        this._selectedFilterVersionID = nodeId.ObjectTypeID != CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeID ? 0L : nodeId.ObjectID;
      }
      else
        this._selectedFilterVersionID = 0L;
    }
    else
      this._selectedFilterVersionID = 0L;
    if (!ObjectHelper.IsUnknownObjectVersionID(this._selectedFilterVersionID))
      this._pageViewsManager.UpdateViews(this._navigatorTreeView.SelectedItems, false);
    else
      this._pageViewsManager.UpdateViews((ISelectedItems) new EmptySelectedItems(), false);
    this.UpdateControl();
  }

  private void AddFilterToolStripMenuItem_Click(object sender, EventArgs e) => this.AddFilter();

  private void RemoveFilterToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RemoveFilter();
  }

  private void LoadFiltersToolStripMenuItem_Click(object sender, EventArgs e) => this.LoadFilters();

  private void SaveFiltersToolStripMenuItem_Click(object sender, EventArgs e) => this.SaveFilters();

  private void UpdateControl()
  {
    this._removeFilterToolStripButton.Enabled = this._removeFilterToolStripMenuItem.Enabled = this.CanRemoveFilter();
  }

  private bool CanRemoveFilter()
  {
    return !ObjectHelper.IsUnknownObjectVersionID(this._selectedFilterVersionID);
  }

  private void AddFilter()
  {
    ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>().AddFiltersToObjectComposition(this._objectVersionID);
  }

  private void RemoveFilter()
  {
    NavigatorTreeNode previousSiblingOrParent = this._navigatorTreeView.FocusedNode.GetPreviousSiblingOrParent();
    try
    {
      ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>().RemoveFilterFromObjectComposition(this._selectedFilterVersionID, this._objectVersionID);
    }
    finally
    {
      this._navigatorTreeView.FocusedNode = previousSiblingOrParent;
    }
  }

  private void LoadFilters()
  {
    ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>().CreateFiltersFromFileAndAddToObjectComposition(this._objectVersionID);
  }

  private void SaveFilters()
  {
    ServiceLocator.Get<ICompositionByObjectTypesFiltersClientService>().SaveFiltersToFileFromObjectComposition(this._objectVersionID);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CompositionByObjectTypesFiltersEditorControl));
    this._pageViewsManager = new PageViewsManager();
    this._navigatorTreeViewToolStrip = new ToolStrip();
    this._addFilterToolStripButton = new ToolStripButton();
    this._removeFilterToolStripButton = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._loadFiltersToolStripButton = new ToolStripButton();
    this._saveFiltersToolStripButton = new ToolStripButton();
    this._navigatorTreeView = new NavigatorTreeView();
    this._navigatorTreeViewContextMenuStrip = new ContextMenuStrip(this.components);
    this._addFilterToolStripMenuItem = new ToolStripMenuItem();
    this._removeFilterToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._loadFiltersToolStripMenuItem = new ToolStripMenuItem();
    this._saveFiltersToolStripMenuItem = new ToolStripMenuItem();
    this._splitContainer = new SplitContainer();
    this._navigatorTreeViewToolStrip.SuspendLayout();
    this._navigatorTreeView.BeginInit();
    this._navigatorTreeViewContextMenuStrip.SuspendLayout();
    this._splitContainer.BeginInit();
    this._splitContainer.Panel1.SuspendLayout();
    this._splitContainer.Panel2.SuspendLayout();
    this._splitContainer.SuspendLayout();
    this.SuspendLayout();
    this._pageViewsManager.ActiveViewPage = (IViewPage) null;
    this._pageViewsManager.CausesValidation = false;
    this._pageViewsManager.Dock = DockStyle.Fill;
    this._pageViewsManager.Font = new Font("Tahoma", 8.25f);
    this._pageViewsManager.Location = new Point(0, 0);
    this._pageViewsManager.Name = "_pageViewsManager";
    this._pageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this._pageViewsManager.Size = new Size(449, 306);
    this._pageViewsManager.TabIndex = 3;
    this._navigatorTreeViewToolStrip.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this._addFilterToolStripButton,
      (ToolStripItem) this._removeFilterToolStripButton,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._loadFiltersToolStripButton,
      (ToolStripItem) this._saveFiltersToolStripButton
    });
    this._navigatorTreeViewToolStrip.Location = new Point(0, 0);
    this._navigatorTreeViewToolStrip.Name = "_navigatorTreeViewToolStrip";
    this._navigatorTreeViewToolStrip.Size = new Size(199, 25);
    this._navigatorTreeViewToolStrip.TabIndex = 2;
    this._navigatorTreeViewToolStrip.Text = "toolStrip1";
    this._addFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addFilterToolStripButton.Image = (Image) Resources.AddStandart;
    this._addFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addFilterToolStripButton.Name = "_addFilterToolStripButton";
    this._addFilterToolStripButton.Size = new Size(23, 22);
    this._addFilterToolStripButton.Text = "Добавить";
    this._addFilterToolStripButton.Click += new EventHandler(this.AddFilterToolStripButton_Click);
    this._removeFilterToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeFilterToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeFilterToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeFilterToolStripButton.Name = "_removeFilterToolStripButton";
    this._removeFilterToolStripButton.Size = new Size(23, 22);
    this._removeFilterToolStripButton.Text = "Удалить";
    this._removeFilterToolStripButton.Click += new EventHandler(this.RemoveFilterToolStripButton_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this._loadFiltersToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._loadFiltersToolStripButton.Image = (Image) componentResourceManager.GetObject("_loadFiltersToolStripButton.Image");
    this._loadFiltersToolStripButton.ImageTransparentColor = Color.Magenta;
    this._loadFiltersToolStripButton.Name = "_loadFiltersToolStripButton";
    this._loadFiltersToolStripButton.Size = new Size(23, 22);
    this._loadFiltersToolStripButton.Text = "Загрузить из файла";
    this._loadFiltersToolStripButton.Click += new EventHandler(this.LoadFiltersToolStripButton_Click);
    this._saveFiltersToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._saveFiltersToolStripButton.Image = (Image) Resources.Save;
    this._saveFiltersToolStripButton.ImageTransparentColor = Color.Magenta;
    this._saveFiltersToolStripButton.Name = "_saveFiltersToolStripButton";
    this._saveFiltersToolStripButton.Size = new Size(23, 22);
    this._saveFiltersToolStripButton.Text = "Сохранить в файл";
    this._saveFiltersToolStripButton.Click += new EventHandler(this.SaveFiltersToolStripButton_Click);
    this._navigatorTreeView.AllowDrop = true;
    this._navigatorTreeView.AllowMultiSelect = false;
    this._navigatorTreeView.AllowUserPinnedColumns = false;
    this._navigatorTreeView.ContextMenuStrip = this._navigatorTreeViewContextMenuStrip;
    this._navigatorTreeView.DisableCheckedOutColumn = true;
    this._navigatorTreeView.DisableDragAndDrop = true;
    this._navigatorTreeView.DisableIMContextMenu = true;
    this._navigatorTreeView.Dock = DockStyle.Fill;
    this._navigatorTreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._navigatorTreeView.ImageList = (ImageList) null;
    this._navigatorTreeView.LineStyle = LineStyle.Dot;
    this._navigatorTreeView.Location = new Point(0, 25);
    this._navigatorTreeView.Name = "_navigatorTreeView";
    this._navigatorTreeView.RowEvenStyle.WordWrap = false;
    this._navigatorTreeView.RowOddStyle.WordWrap = false;
    this._navigatorTreeView.RowSelectedStyle.WordWrap = false;
    this._navigatorTreeView.RowStyle.BorderColor = SystemColors.Control;
    this._navigatorTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._navigatorTreeView.RowStyle.BorderWidth = 1;
    this._navigatorTreeView.RowStyle.WordWrap = false;
    this._navigatorTreeView.SelectBeforeEdit = true;
    this._navigatorTreeView.ShowRootRow = false;
    this._navigatorTreeView.Size = new Size(199, 281);
    this._navigatorTreeView.SuppressErrorMessages = true;
    this._navigatorTreeView.TabIndex = 1;
    this._navigatorTreeView.SelectionChanged += new EventHandler(this.NavigatorTreeView_SelectionChanged);
    this._navigatorTreeViewContextMenuStrip.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this._addFilterToolStripMenuItem,
      (ToolStripItem) this._removeFilterToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._loadFiltersToolStripMenuItem,
      (ToolStripItem) this._saveFiltersToolStripMenuItem
    });
    this._navigatorTreeViewContextMenuStrip.Name = "_navigatorTreeViewContextMenuStrip";
    this._navigatorTreeViewContextMenuStrip.Size = new Size(182, 98);
    this._addFilterToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addFilterToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._addFilterToolStripMenuItem.Name = "_addFilterToolStripMenuItem";
    this._addFilterToolStripMenuItem.Size = new Size(181, 22);
    this._addFilterToolStripMenuItem.Text = "Добавить";
    this._addFilterToolStripMenuItem.Click += new EventHandler(this.AddFilterToolStripMenuItem_Click);
    this._removeFilterToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._removeFilterToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._removeFilterToolStripMenuItem.Name = "_removeFilterToolStripMenuItem";
    this._removeFilterToolStripMenuItem.Size = new Size(181, 22);
    this._removeFilterToolStripMenuItem.Text = "Удалить";
    this._removeFilterToolStripMenuItem.Click += new EventHandler(this.RemoveFilterToolStripMenuItem_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(178, 6);
    this._loadFiltersToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_loadFiltersToolStripMenuItem.Image");
    this._loadFiltersToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._loadFiltersToolStripMenuItem.Name = "_loadFiltersToolStripMenuItem";
    this._loadFiltersToolStripMenuItem.Size = new Size(181, 22);
    this._loadFiltersToolStripMenuItem.Text = "Загрузить из файла";
    this._loadFiltersToolStripMenuItem.Click += new EventHandler(this.LoadFiltersToolStripMenuItem_Click);
    this._saveFiltersToolStripMenuItem.Image = (Image) Resources.Save;
    this._saveFiltersToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._saveFiltersToolStripMenuItem.Name = "_saveFiltersToolStripMenuItem";
    this._saveFiltersToolStripMenuItem.Size = new Size(181, 22);
    this._saveFiltersToolStripMenuItem.Text = "Сохранить в файл";
    this._saveFiltersToolStripMenuItem.Click += new EventHandler(this.SaveFiltersToolStripMenuItem_Click);
    this._splitContainer.Dock = DockStyle.Fill;
    this._splitContainer.Location = new Point(0, 0);
    this._splitContainer.Name = "_splitContainer";
    this._splitContainer.Panel1.Controls.Add((Control) this._navigatorTreeView);
    this._splitContainer.Panel1.Controls.Add((Control) this._navigatorTreeViewToolStrip);
    this._splitContainer.Panel2.Controls.Add((Control) this._pageViewsManager);
    this._splitContainer.Size = new Size(652, 306);
    this._splitContainer.SplitterDistance = 199;
    this._splitContainer.TabIndex = 2;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._splitContainer);
    this.Name = nameof (CompositionByObjectTypesFiltersEditorControl);
    this.Size = new Size(652, 306);
    this._navigatorTreeViewToolStrip.ResumeLayout(false);
    this._navigatorTreeViewToolStrip.PerformLayout();
    this._navigatorTreeView.EndInit();
    this._navigatorTreeViewContextMenuStrip.ResumeLayout(false);
    this._splitContainer.Panel1.ResumeLayout(false);
    this._splitContainer.Panel1.PerformLayout();
    this._splitContainer.Panel2.ResumeLayout(false);
    this._splitContainer.EndInit();
    this._splitContainer.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class CompositionByObjectTypesFiltersForObjectDescriptor : Intermech.Navigator.DBObjects.Descriptor
  {
    private int _objectTypeID = -1;
    private long _objectVersionID;

    public CompositionByObjectTypesFiltersForObjectDescriptor(
      int objectTypeID,
      long objectVersionID)
      : base(objectVersionID)
    {
      if (ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this._objectTypeID = objectTypeID;
      this._objectVersionID = objectVersionID;
    }

    public override INode GetChild(INodeID nodeID)
    {
      return (INode) new CompositionByObjectTypesFiltersEditorControl.CompositionByObjectTypesFiltersForUserNode(this._objectTypeID, this._objectVersionID);
    }
  }

  private sealed class CompositionByObjectTypesFiltersForUserNode(
    int objectTypeID,
    long userVersionID) : ObjectNode(objectTypeID, userVersionID)
  {
    protected override List<PartSlot> CreateFolderSlots()
    {
      return new List<PartSlot>()
      {
        new PartSlot(Guid.NewGuid(), (INodePart) new RelatedObjectsPart(this._objTypeID, this._objID, RelatedObjectsRole.Composition, CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFiltersRelationTypeID, this.Services))
      };
    }
  }

  [Serializable]
  public sealed class CompositionByObjectTypesFiltersEditorControlMemento
  {
    public double SplitterPosition { get; set; }

    public NodeColumnCollection TreeNodeColumns { get; set; }
  }
}
