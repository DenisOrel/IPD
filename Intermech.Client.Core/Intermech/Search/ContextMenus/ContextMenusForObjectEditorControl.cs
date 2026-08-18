
// Type: Intermech.Search.ContextMenus.ContextMenusForObjectEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Queries;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenusForObjectEditorControl : UserControl, ISupportInitialize
{
  private long _objectVersionID;
  private long _selectedContextMenuVersionID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer1;
  private ToolStrip toolStrip1;
  private NavigatorTreeView _navigatorTreeView;
  private PageViewsManager _pageViewsManager;
  private ContextMenuStrip contextMenuStrip1;
  private ToolStripMenuItem _addToolStripMenuItem;
  private ToolStripMenuItem _removeToolStripMenuItem;
  private ToolStripButton _addToolStripButton;
  private ToolStripButton _removeToolStripButton;

  public ContextMenusForObjectEditorControl() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ObjectVersionID
  {
    get => this._objectVersionID;
    set
    {
      if (this._objectVersionID == value)
        return;
      this._objectVersionID = value;
      this._navigatorTreeView.Build((IDescriptor) new ContextMenusForObjectEditorControl.ObjectWithContextMenusDescriptor(this._objectVersionID));
    }
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    AdvancedServiceContainer serviceContainer1 = new AdvancedServiceContainer((System.IServiceProvider) ServicesManager.ServiceContainer);
    serviceContainer1.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInTree));
    this._navigatorTreeView.Services = (System.IServiceProvider) serviceContainer1;
    NodeColumnCollection nodeColumnCollection = (NodeColumnCollection) Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending).Clone();
    nodeColumnCollection.Add(new NodeColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) ContextMenuConstants.ObjectTypesGuidsAttributeTypeID, typeof (string), FieldTypes.ftGuid, "Типы объектов"));
    this._navigatorTreeView.SetColumns(nodeColumnCollection);
    this._navigatorTreeView.SupportedColumns = nodeColumnCollection;
    AdvancedServiceContainer serviceContainer2 = new AdvancedServiceContainer(this._navigatorTreeView.Services);
    serviceContainer2.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NodeInViews));
    this._pageViewsManager.Services = (System.IServiceProvider) serviceContainer2;
  }

  private void AddToolStripButton_Click(object sender, EventArgs e) => this.Add();

  private void RemoveToolStripButton_Click(object sender, EventArgs e) => this.Remove();

  private void AddToolStripMenuItem_Click(object sender, EventArgs e) => this.Add();

  private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) => this.Remove();

  private void NavigatorTreeView_GetRowData(object sender, GetRowDataEventArgs e)
  {
    e.RowData.AutoFitHeight = true;
  }

  private void NavigatorTreeView_SelectionChanged(object sender, EventArgs e)
  {
    if (this._navigatorTreeView.SelectedItem is NavigatorTreeNode)
    {
      NavigatorTreeNode selectedItem = (NavigatorTreeNode) this._navigatorTreeView.SelectedItem;
      if (selectedItem.NodeID is NodeID)
      {
        NodeID nodeId = (NodeID) selectedItem.NodeID;
        this._selectedContextMenuVersionID = nodeId.ObjectTypeID != ContextMenuConstants.ContextMenuObjectTypeID ? 0L : nodeId.ObjectID;
      }
      else
        this._selectedContextMenuVersionID = 0L;
    }
    else
      this._selectedContextMenuVersionID = 0L;
    if (!ObjectHelper.IsUnknownObjectVersionID(this._selectedContextMenuVersionID))
      this._pageViewsManager.UpdateViews(this._navigatorTreeView.SelectedItems, false);
    else
      this._pageViewsManager.UpdateViews((ISelectedItems) new EmptySelectedItems(), false);
    this.UpdateControl();
  }

  private void PageViewsManager_FilterViews(object sender, PageViewsManager.FilterViewsEventArgs e)
  {
    List<string> stringList = new List<string>()
    {
      "ObjectProperties",
      typeof (ContextMenuEditorView).Name
    };
    if (e.Views != null)
    {
      string str = ((IEnumerable<string>) e.Views).FirstOrDefault<string>((Func<string, bool>) (o => o != null && o.Contains("FormDesignerObject")));
      if (str != null)
        stringList.Add(str);
    }
    e.AllowedViews = stringList.ToArray();
  }

  private void UpdateControl()
  {
    this._removeToolStripButton.Enabled = this._removeToolStripMenuItem.Enabled = this.CanRemoveFilter();
  }

  private bool CanRemoveFilter()
  {
    return !ObjectHelper.IsUnknownObjectVersionID(this._selectedContextMenuVersionID);
  }

  private void Add()
  {
    long[] source = new long[0];
    try
    {
      try
      {
        source = ServiceLocator.Get<IContextMenuClientService>().AddContextMenusToObjectComposition(this._objectVersionID);
      }
      finally
      {
        this._navigatorTreeView.RefreshNode(this._navigatorTreeView.RootNode);
      }
    }
    finally
    {
      if (source.Length != 0)
        this._navigatorTreeView.FocusedNode = this.GetNavigatorTreeNodeForObjectVersionID(((IEnumerable<long>) source).Last<long>()) ?? this._navigatorTreeView.RootNode;
    }
  }

  private void Remove()
  {
    NavigatorTreeNode previousSiblingOrParent = this._navigatorTreeView.FocusedNode.GetPreviousSiblingOrParent();
    try
    {
      try
      {
        ServiceLocator.Get<IContextMenuClientService>().RemoveContextMenuFromObjectComposition(this._selectedContextMenuVersionID, this._objectVersionID);
      }
      finally
      {
        this._navigatorTreeView.RefreshNode(this._navigatorTreeView.RootNode);
      }
    }
    finally
    {
      this._navigatorTreeView.FocusedNode = this.GetNavigatorTreeNodeForObjectVersionID(this.GetObjectVerisonIDForNode(previousSiblingOrParent)) ?? this._navigatorTreeView.RootNode;
    }
  }

  private long GetObjectVerisonIDForNode(NavigatorTreeNode navigatorTreeNode)
  {
    return !(navigatorTreeNode.NodeID is NodeID) ? 0L : ((NodeID) navigatorTreeNode.NodeID).ObjectID;
  }

  private NavigatorTreeNode GetNavigatorTreeNodeForObjectVersionID(long objectVersionID)
  {
    return this._navigatorTreeView.RootNode.Children.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (o => o.NodeID is NodeID)).FirstOrDefault<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (o => ((NodeID) o.NodeID).ObjectID == objectVersionID));
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
    this.splitContainer1 = new SplitContainer();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this._addToolStripMenuItem = new ToolStripMenuItem();
    this._removeToolStripMenuItem = new ToolStripMenuItem();
    this.toolStrip1 = new ToolStrip();
    this._addToolStripButton = new ToolStripButton();
    this._removeToolStripButton = new ToolStripButton();
    this._navigatorTreeView = new NavigatorTreeView();
    this._pageViewsManager = new PageViewsManager();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this._navigatorTreeView.BeginInit();
    this.SuspendLayout();
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this._navigatorTreeView);
    this.splitContainer1.Panel1.Controls.Add((Control) this.toolStrip1);
    this.splitContainer1.Panel2.Controls.Add((Control) this._pageViewsManager);
    this.splitContainer1.Size = new Size(878, 357);
    this.splitContainer1.SplitterDistance = 245;
    this.splitContainer1.TabIndex = 0;
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addToolStripMenuItem,
      (ToolStripItem) this._removeToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size((int) sbyte.MaxValue, 48 /*0x30*/);
    this._addToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addToolStripMenuItem.Name = "_addToolStripMenuItem";
    this._addToolStripMenuItem.Size = new Size(126, 22);
    this._addToolStripMenuItem.Text = "Добавить";
    this._addToolStripMenuItem.ToolTipText = "Добавить";
    this._addToolStripMenuItem.Click += new EventHandler(this.AddToolStripMenuItem_Click);
    this._removeToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
    this._removeToolStripMenuItem.Size = new Size(126, 22);
    this._removeToolStripMenuItem.Text = "Удалить";
    this._removeToolStripMenuItem.ToolTipText = "Удалить";
    this._removeToolStripMenuItem.Click += new EventHandler(this.RemoveToolStripMenuItem_Click);
    this.toolStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addToolStripButton,
      (ToolStripItem) this._removeToolStripButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(245, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this._addToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addToolStripButton.Image = (Image) Resources.AddStandart;
    this._addToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addToolStripButton.Name = "_addToolStripButton";
    this._addToolStripButton.Size = new Size(23, 22);
    this._addToolStripButton.Text = "Добавить";
    this._addToolStripButton.Click += new EventHandler(this.AddToolStripButton_Click);
    this._removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeToolStripButton.Name = "_removeToolStripButton";
    this._removeToolStripButton.Size = new Size(23, 22);
    this._removeToolStripButton.Text = "Удалить";
    this._removeToolStripButton.Click += new EventHandler(this.RemoveToolStripButton_Click);
    this._navigatorTreeView.AllowDrop = true;
    this._navigatorTreeView.AllowMultiSelect = false;
    this._navigatorTreeView.AllowUserPinnedColumns = false;
    this._navigatorTreeView.ContextMenuStrip = this.contextMenuStrip1;
    this._navigatorTreeView.DisableCheckedOutColumn = true;
    this._navigatorTreeView.DisableDragAndDrop = true;
    this._navigatorTreeView.DisableIMContextMenu = true;
    this._navigatorTreeView.Dock = DockStyle.Fill;
    this._navigatorTreeView.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._navigatorTreeView.ImageList = (ImageList) null;
    this._navigatorTreeView.LineStyle = LineStyle.Dot;
    this._navigatorTreeView.Location = new Point(0, 25);
    this._navigatorTreeView.Name = "_navigatorTreeView";
    this._navigatorTreeView.RowEvenStyle.WordWrap = true;
    this._navigatorTreeView.RowOddStyle.WordWrap = true;
    this._navigatorTreeView.RowSelectedStyle.WordWrap = true;
    this._navigatorTreeView.RowStyle.BorderColor = SystemColors.Control;
    this._navigatorTreeView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._navigatorTreeView.RowStyle.BorderWidth = 1;
    this._navigatorTreeView.RowStyle.WordWrap = true;
    this._navigatorTreeView.SelectBeforeEdit = true;
    this._navigatorTreeView.ShowRootRow = false;
    this._navigatorTreeView.Size = new Size(245, 332);
    this._navigatorTreeView.SuppressErrorMessages = true;
    this._navigatorTreeView.TabIndex = 2;
    this._navigatorTreeView.GetRowData += new GetRowDataHandler(this.NavigatorTreeView_GetRowData);
    this._navigatorTreeView.SelectionChanged += new EventHandler(this.NavigatorTreeView_SelectionChanged);
    this._pageViewsManager.ActiveViewPage = (IViewPage) null;
    this._pageViewsManager.CausesValidation = false;
    this._pageViewsManager.Dock = DockStyle.Fill;
    this._pageViewsManager.Font = new Font("Tahoma", 8.25f);
    this._pageViewsManager.Location = new Point(0, 0);
    this._pageViewsManager.Name = "_pageViewsManager";
    this._pageViewsManager.Padding = new Padding(10, 0, 0, 0);
    this._pageViewsManager.Size = new Size(629, 357);
    this._pageViewsManager.TabIndex = 4;
    this._pageViewsManager.FilterViews += new EventHandler<PageViewsManager.FilterViewsEventArgs>(this.PageViewsManager_FilterViews);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (ContextMenusForObjectEditorControl);
    this.Size = new Size(878, 357);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.contextMenuStrip1.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this._navigatorTreeView.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class ObjectWithContextMenusDescriptor(long objectVersionID) : Intermech.Navigator.DBObjects.Descriptor(objectVersionID)
  {
    public override INode GetChild(INodeID nodeID)
    {
      return (INode) new ContextMenusForObjectEditorControl.ObjectWithContextMenusNode(this._objID, ContextMenusForObjectEditorControl.ObjectWithContextMenusDescriptor.GetObjectTypeID(this._objID));
    }

    private static int GetObjectTypeID(long objectVersionID)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetObject(objectVersionID).ObjectType;
    }
  }

  private sealed class ObjectWithContextMenusNode(long objectVersionID, int objectTypeID) : 
    ObjectNode(objectTypeID, objectVersionID)
  {
    protected override List<PartSlot> CreateFolderSlots()
    {
      return new List<PartSlot>()
      {
        new PartSlot(MetaDataHelper.GetObjectTypeGuid(this._objTypeID), (INodePart) new ContextMenusForObjectEditorControl.ObjectWithContextMenusNodePart(this._objID, this._objTypeID, this.Services))
      };
    }
  }

  private sealed class ObjectWithContextMenusNodePart(
    long objectVersionID,
    int objectTypeID,
    System.IServiceProvider serviceProvider) : RelatedObjectsPart(objectTypeID, objectVersionID, RelatedObjectsRole.Composition, serviceProvider)
  {
    protected override RelatedObjectsQuery QueryConstruction(ConditionStructure[] conditions)
    {
      return (RelatedObjectsQuery) new ContextMenusForObjectEditorControl.ObjectWithContextMenusNodeQuery((INodeQuerySupport) this, this._objID, this._objTypeID, conditions);
    }
  }

  private sealed class ObjectWithContextMenusNodeQuery(
    INodeQuerySupport nodeQuerySupport,
    long objectVersionID,
    int objectTypeID,
    ConditionStructure[] conditions) : RelatedObjectsQuery(nodeQuerySupport, objectVersionID, objectTypeID, RelatedObjectsRole.Composition, ContextMenuConstants.ContextMenusRelationTypeID, ContextMenuConstants.ContextMenuObjectTypeID, conditions)
  {
    protected override RecordAdapter CreateRecordAdapter(
      RecordMapping mapping,
      object[] fieldsOrder)
    {
      return (RecordAdapter) new ContextMenusForObjectEditorControl.ObjectWithContextMenusRecordAdapter(mapping, fieldsOrder);
    }
  }

  private sealed class ObjectWithContextMenusRecordAdapter(
    RecordMapping mapping,
    object[] fieldsOrder) : RecordAdapter(mapping, fieldsOrder)
  {
    public override object[] GetRecordValues(object[] fieldValues)
    {
      object[] recordValues = base.GetRecordValues(fieldValues);
      int fieldIndex = this.GetFieldIndex((object) new NodeColumnID((object) ContextMenuConstants.ObjectTypesGuidsAttributeTypeID, AttributeSourceTypes.Object));
      if (fieldIndex >= 0)
      {
        long objectVersionId = this.GetObjectVersionID(fieldValues);
        if (!ObjectHelper.IsUnknownObjectVersionID(objectVersionId))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionId, false);
            if (dbObject != null)
            {
              IDBAttribute attributeById = dbObject.GetAttributeByID(ContextMenuConstants.ObjectTypesGuidsAttributeTypeID);
              if (attributeById != null)
                recordValues[fieldIndex] = (object) string.Join(", ", ((IEnumerable<object>) attributeById.Values).Select<object, Guid>(new Func<object, Guid>(this.ConvertToGuid)).Where<Guid>((Func<Guid, bool>) (o => Guid.Empty != o)).Select<Guid, string>(new Func<Guid, string>(this.GetObjectTypeNameByGuid)));
            }
          }
        }
      }
      return recordValues;
    }

    private long GetObjectVersionID(object[] fieldValues)
    {
      int fieldIndex = this.GetFieldIndex((object) new NodeColumnID((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object));
      return fieldIndex < 0 ? 0L : Convert.ToInt64(fieldValues[fieldIndex]);
    }

    private Guid ConvertToGuid(object value)
    {
      switch (value)
      {
        case Guid guid:
          return guid;
        case string _:
          if (!string.IsNullOrEmpty((string) value))
            return Guid.Parse((string) value);
          break;
      }
      return Guid.Empty;
    }

    private string GetObjectTypeNameByGuid(Guid objectTypeGuid)
    {
      return MetaDataHelper.GetObjectTypeName(objectTypeGuid);
    }
  }
}
