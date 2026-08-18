
// Type: Intermech.Search.RecentObjects.RecentObjectsAccessSettingsControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Descriptos;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.RecentObjects;

public sealed class RecentObjectsAccessSettingsControl : UserControl, ISupportInitialize
{
  private List<long> _objectVersionIds = new List<long>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip toolStrip1;
  private ContextMenuStrip contextMenuStrip1;
  private ChildrenView _childrenView;
  private ToolStripButton _addToolStripButton;
  private ToolStripButton _removeToolStripButton;
  private ToolStripMenuItem _addToolStripMenuItem;
  private ToolStripMenuItem _removeToolStripMenuItem;

  public RecentObjectsAccessSettingsControl()
  {
    this.InitializeComponent();
    this._childrenView.ViewContentType = ContentType.NonFolders;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long[] ObjectVersionIds
  {
    get => this._objectVersionIds.ToArray();
    set
    {
      this._objectVersionIds = ((IEnumerable<long>) (value ?? new long[0])).ToList<long>();
      this.UpdateChildrenView();
      this.UpdateControls();
    }
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    this._childrenView.AllowEditing = false;
    this._childrenView.DisableIMContextMenu = true;
    this._childrenView.DisableHeaderContextMenu = true;
    this._childrenView.DisableManualSortingSetup = true;
  }

  private void AddToolStripButton_Click(object sender, EventArgs e) => this.Add();

  private void RemoveToolStripButton_Click(object sender, EventArgs e) => this.Remove();

  private void AddToolStripMenuItem_Click(object sender, EventArgs e) => this.Add();

  private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) => this.Remove();

  private void ChildrenView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void UpdateChildrenView(long mustBeSelectedObjectVersionID = 0)
  {
    long[] numArray;
    if (ObjectHelper.IsUnknownObjectVersionID(mustBeSelectedObjectVersionID))
      numArray = this.GetSelectedObjectVersionIds();
    else
      numArray = new long[1]
      {
        mustBeSelectedObjectVersionID
      };
    long[] objectVersionIds = numArray;
    try
    {
      this._childrenView.Deactivate((IView) null);
      this._childrenView.Initialize((IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, "", (IList) this._objectVersionIds), (System.IServiceProvider) ServicesManager.ServiceContainer);
      this._childrenView.Activate((IView) null);
    }
    finally
    {
      this.SetSelectedObjectVersionIds(objectVersionIds);
    }
  }

  private long[] GetSelectedObjectVersionIds()
  {
    return this._childrenView.SelectedNodeIDs.Where<INodeID>((Func<INodeID, bool>) (o => o is NodeID)).Select<INodeID, long>((Func<INodeID, long>) (o => ((NodeID) o).ObjectID)).ToArray<long>();
  }

  private void SetSelectedObjectVersionIds(long[] objectVersionIds)
  {
    this._childrenView.SelectNodes(((IEnumerable<long>) objectVersionIds).Select<long, INodeID>((Func<long, INodeID>) (o => this.FindNodeIDForObjectVersionID(o))).Where<INodeID>((Func<INodeID, bool>) (o => o != null)).ToList<INodeID>());
  }

  private INodeID FindNodeIDForObjectVersionID(long objectVersionID)
  {
    return (INodeID) this.GetAllObjectNodeIds().FirstOrDefault<NodeID>((Func<NodeID, bool>) (o => o.ObjectID == objectVersionID));
  }

  private IEnumerable<NodeID> GetAllObjectNodeIds()
  {
    foreach (iGRow row in (IEnumerable) this._childrenView.Grid.Rows)
    {
      INodeID nodeIdForRow = this._childrenView.GetNodeIDForRow(row);
      if (nodeIdForRow is NodeID)
        yield return (NodeID) nodeIdForRow;
    }
  }

  private void UpdateControls()
  {
    this._addToolStripButton.Enabled = this._addToolStripMenuItem.Enabled = this.CanAdd();
    this._removeToolStripButton.Enabled = this._removeToolStripMenuItem.Enabled = this.CanRemove();
  }

  private bool CanAdd() => true;

  private bool CanRemove()
  {
    List<INodeID> selectedNodeIds = this._childrenView.SelectedNodeIDs;
    return selectedNodeIds != null && selectedNodeIds.Count > 0;
  }

  private void Add()
  {
    long[] source = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор пользователей, групп, ролей", "Выберите пользователей, группы, роли, которым будет предостален доступ с списку недавних объектов.", (IDescriptor) new ObjectTypesDescriptor(new int[3]
    {
      Constants.UserObjectTypeID,
      Constants.UserGroupObjectTypeID,
      Constants.RoleObjectTypeID
    }, "Допустимые типы объектов"), SelectionOptions.SelectObjects);
    if (source == null || source.Length == 0)
      return;
    foreach (long num in source)
    {
      if (!this._objectVersionIds.Contains(num))
        this._objectVersionIds.Add(num);
    }
    this.UpdateChildrenView(((IEnumerable<long>) source).Last<long>());
    this.UpdateControls();
  }

  private void Remove()
  {
    long[] objectVersionIds = this.GetSelectedObjectVersionIds();
    long previousObjectVersionId = this.FindPreviousObjectVersionID(((IEnumerable<long>) objectVersionIds).First<long>());
    foreach (long num in objectVersionIds)
      this._objectVersionIds.Remove(num);
    this.UpdateChildrenView(previousObjectVersionId);
    this.UpdateControls();
  }

  private long FindPreviousObjectVersionID(long objectVersionID)
  {
    long previousObjectVersionId = 0;
    foreach (NodeID allObjectNodeId in this.GetAllObjectNodeIds())
    {
      if (allObjectNodeId.ObjectID == objectVersionID)
        return previousObjectVersionId;
      previousObjectVersionId = allObjectNodeId.ObjectID;
    }
    return 0;
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
    this.toolStrip1 = new ToolStrip();
    this._addToolStripButton = new ToolStripButton();
    this._removeToolStripButton = new ToolStripButton();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this._addToolStripMenuItem = new ToolStripMenuItem();
    this._removeToolStripMenuItem = new ToolStripMenuItem();
    this._childrenView = new ChildrenView();
    this.toolStrip1.SuspendLayout();
    this.contextMenuStrip1.SuspendLayout();
    this.SuspendLayout();
    this.toolStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addToolStripButton,
      (ToolStripItem) this._removeToolStripButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(493, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this._addToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addToolStripButton.Image = (Image) Resources.AddStandart;
    this._addToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addToolStripButton.Name = "_addToolStripButton";
    this._addToolStripButton.Size = new Size(23, 22);
    this._addToolStripButton.Text = "Добавить пользователя, группу, роль";
    this._addToolStripButton.Click += new EventHandler(this.AddToolStripButton_Click);
    this._removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeToolStripButton.Name = "_removeToolStripButton";
    this._removeToolStripButton.Size = new Size(23, 22);
    this._removeToolStripButton.Text = "Удалить";
    this._removeToolStripButton.Click += new EventHandler(this.RemoveToolStripButton_Click);
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addToolStripMenuItem,
      (ToolStripItem) this._removeToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(282, 48 /*0x30*/);
    this._addToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addToolStripMenuItem.Name = "_addToolStripMenuItem";
    this._addToolStripMenuItem.Size = new Size(281, 22);
    this._addToolStripMenuItem.Text = "Добавить пользователя, группу, роль";
    this._addToolStripMenuItem.Click += new EventHandler(this.AddToolStripMenuItem_Click);
    this._removeToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
    this._removeToolStripMenuItem.Size = new Size(281, 22);
    this._removeToolStripMenuItem.Text = "Удалить";
    this._removeToolStripMenuItem.Click += new EventHandler(this.RemoveToolStripMenuItem_Click);
    this._childrenView.AllowCustomGroupValues = true;
    this._childrenView.AllowEditing = true;
    this._childrenView.ContextMenuStrip = this.contextMenuStrip1;
    this._childrenView.Control = (object) this._childrenView;
    this._childrenView.DisableKeyDownEvents = false;
    this._childrenView.Dock = DockStyle.Fill;
    this._childrenView.EditingMode = false;
    this._childrenView.EmbeddedFocusAndSelection = (iFocusAndSelection) null;
    this._childrenView.Font = new Font("Tahoma", 8.25f);
    this._childrenView.Location = new Point(0, 25);
    this._childrenView.Name = "_childrenView";
    this._childrenView.Size = new Size(493, 303);
    this._childrenView.TabIndex = 2;
    this._childrenView.ViewContentType = ContentType.Folders | ContentType.NonFolders;
    this._childrenView.SelectedItemsChanged += new EventHandler(this.ChildrenView_SelectedItemsChanged);
    this.Controls.Add((Control) this._childrenView);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (RecentObjectsAccessSettingsControl);
    this.Size = new Size(493, 328);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.contextMenuStrip1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
