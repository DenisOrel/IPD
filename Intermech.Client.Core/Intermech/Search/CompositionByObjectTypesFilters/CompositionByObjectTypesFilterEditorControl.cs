
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFilterEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFilterEditorControl : UserControl, ISupportInitialize
{
  private const string ProjectTypeNameColumnKey = "ProjectTypeName";
  private const string PartTypeCheckBoxColumnKey = "PartTypeCheckBox";
  private const string PartTypeNameColumnKey = "PartTypeName";
  private CompositionByObjectTypesFilter _filter;
  private CompositionByObjectTypesFilterProjectType _selectedProjectType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolStrip _partTypesToolStrip;
  private ToolStrip _projectTypesToolStrip;
  private ContextMenuStrip _projectTypesContextMenuStrip;
  private ContextMenuStrip _partTypesContextMenuStrip;
  private Intermech.VirtualTreeView.VirtualTreeView _partTypesTree;
  private Intermech.VirtualTreeView.VirtualTreeView _projectTypesTree;
  private ToolStripButton _checkAllPartTypesToolStripButton;
  private ToolStripButton _uncheckAllPartTypesToolStripButton;
  private ToolStripButton _addProjectTypeToolStripButton;
  private ToolStripButton _removeProjectTypeToolStripButton;
  private Column _partTypeCheckBoxColumn;
  private Column _partTypeNameColumn;
  private ToolStripMenuItem _checkAllPartTypesToolStripMenuItem;
  private ToolStripMenuItem _uncheckAllPartTypesToolStripMenuItem;
  private CellEditor _partTypesTreeCheckBoxCellEditor;
  private Column _projectTypeNameColumn;
  private ToolStripMenuItem _addProjectTypeToolStripMenuItem;
  private ToolStripMenuItem _removeProjectTypeToolStripMenuItem;
  private CheckBox _partTypesTreeCheckBox;
  private SplitContainer splitContainer1;

  public CompositionByObjectTypesFilterEditorControl()
  {
    this.InitializeComponent();
    this._projectTypeNameColumn.DataField = "ProjectTypeName";
    this._partTypeCheckBoxColumn.DataField = "PartTypeCheckBox";
    this._partTypeNameColumn.DataField = "PartTypeName";
    this._projectTypesTree.RowBindings.Add((RowBinding) new CompositionByObjectTypesFilterEditorControl.CompositionByObjectTypesFilterProjectTypeRowBinding());
    this._partTypesTree.RowBindings.Add((RowBinding) new CompositionByObjectTypesFilterEditorControl.CompositionByObjectTypesFilterPartTypeRowBinding());
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public CompositionByObjectTypesFilter Filter
  {
    get => this._filter;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (this._filter == value)
        return;
      this._filter = value;
      this._partTypesTree.DataSource = (object) null;
      this._projectTypesTree.DataSource = (object) this._filter.ProjectTypes;
      this.UpdateControl();
    }
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
  }

  private void AddProjectTypeToolStripButton_Click(object sender, EventArgs e)
  {
    this.AddProjectType();
  }

  private void RemoveProjectTypeToolStripButton_Click(object sender, EventArgs e)
  {
    this.RemoveProjectType();
  }

  private void ProjectTypesTree_SelectionChanged(object sender, EventArgs e)
  {
    this._selectedProjectType = this._projectTypesTree.SelectedItem as CompositionByObjectTypesFilterProjectType;
    if (this._selectedProjectType != null)
      this._partTypesTree.DataSource = (object) this._selectedProjectType.PartTypes;
    this.UpdateControl();
  }

  private void PartTypesTree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!(e.Row.Item is CompositionByObjectTypesFilterPartType typesFilterPartType) || e.Column != this._partTypeCheckBoxColumn)
      return;
    this._partTypesTree.SuspendDataUpdate();
    try
    {
      typesFilterPartType.Checked = (bool) e.NewValue;
    }
    finally
    {
      this._partTypesTree.ResumeDataUpdate();
    }
  }

  private void AddProjectTypeToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddProjectType();
  }

  private void RemoveProjectTypeToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RemoveProjectType();
  }

  private void CheckAllPartTypesToolStripButton_Click(object sender, EventArgs e)
  {
    this.CheckAllPartTypes();
  }

  private void UncheckAllPartTypesToolStripButton_Click(object sender, EventArgs e)
  {
    this.UncheckAllPartTypes();
  }

  private void CheckAllPartTypesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.CheckAllPartTypes();
  }

  private void UncheckAllPartTypesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.UncheckAllPartTypes();
  }

  private void UpdateControl()
  {
    this._removeProjectTypeToolStripButton.Enabled = this._removeProjectTypeToolStripMenuItem.Enabled = this.CanRemoveProjectType();
    this._checkAllPartTypesToolStripButton.Enabled = this._checkAllPartTypesToolStripMenuItem.Enabled = this.CanCheckAllPartTypes();
    this._uncheckAllPartTypesToolStripButton.Enabled = this._uncheckAllPartTypesToolStripMenuItem.Enabled = this.CanUncheckAllPartTypes();
  }

  private bool CanRemoveProjectType() => this._selectedProjectType != null;

  private bool CanCheckAllPartTypes()
  {
    return this._selectedProjectType != null && this._selectedProjectType.PartTypes.Count > 0;
  }

  private bool CanUncheckAllPartTypes()
  {
    return this._selectedProjectType != null && this._selectedProjectType.PartTypes.Count > 0;
  }

  private void AddProjectType()
  {
    using (TreeViewWithButtonsForm viewWithButtonsForm = new TreeViewWithButtonsForm())
    {
      viewWithButtonsForm.DisableGroupCheckedNodes = true;
      viewWithButtonsForm.Nodes.Add(this.CreateTreeSelectDialogNodeForObjectTypes());
      ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
      if (categoryTypeIconService != null)
        viewWithButtonsForm.ImageList = categoryTypeIconService.ImageList;
      if (viewWithButtonsForm.ShowDialog() != DialogResult.OK || viewWithButtonsForm.CheckedTags == null)
        return;
      int[] array = viewWithButtonsForm.CheckedTags.Cast<int>().ToArray<int>();
      List<CompositionByObjectTypesFilterProjectType> filterProjectTypeList = new List<CompositionByObjectTypesFilterProjectType>();
      foreach (int num in array)
      {
        int objectTypeID = num;
        if (!this._filter.ProjectTypes.Any<CompositionByObjectTypesFilterProjectType>((Func<CompositionByObjectTypesFilterProjectType, bool>) (o => o.ProjectTypeID == objectTypeID)))
          filterProjectTypeList.Add(CompositionByObjectTypesFiltersHelper.CreateProjectType(objectTypeID));
      }
      List<CompositionByObjectTypesFilterProjectType> list1 = this._filter.ProjectTypes.ToList<CompositionByObjectTypesFilterProjectType>();
      list1.AddRange((IEnumerable<CompositionByObjectTypesFilterProjectType>) filterProjectTypeList);
      List<CompositionByObjectTypesFilterProjectType> list2 = list1.OrderBy<CompositionByObjectTypesFilterProjectType, string>((Func<CompositionByObjectTypesFilterProjectType, string>) (o => MetaDataHelper.GetObjectType(o.ProjectTypeID).ObjectTypeName)).ToList<CompositionByObjectTypesFilterProjectType>();
      this._filter.ProjectTypes.Clear();
      this._filter.ProjectTypes.AddRange((IEnumerable<CompositionByObjectTypesFilterProjectType>) list2);
      this._projectTypesTree.SelectedItem = (object) filterProjectTypeList.OrderBy<CompositionByObjectTypesFilterProjectType, string>((Func<CompositionByObjectTypesFilterProjectType, string>) (o => MetaDataHelper.GetObjectType(o.ProjectTypeID).ObjectTypeName)).LastOrDefault<CompositionByObjectTypesFilterProjectType>();
    }
  }

  private TreeNode CreateTreeSelectDialogNodeForObjectTypes()
  {
    TreeNode nodeForObjectTypes = new TreeNode("Список типов объектов");
    foreach (IMSObjectType objectType in MetaDataHelper.GetObjectTypesList().Where<IMSObjectType>((Func<IMSObjectType, bool>) (o => MetaDataHelper.GetObjectTypeLevel(o.ObjectTypeID) == 0)).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)).ToArray<IMSObjectType>())
    {
      TreeNode nodeForObjectType = this.CreateTreeSelectedDialogNodeForObjectType(objectType);
      nodeForObjectTypes.Nodes.Add(nodeForObjectType);
    }
    return nodeForObjectTypes;
  }

  private TreeNode CreateTreeSelectedDialogNodeForObjectType(IMSObjectType objectType)
  {
    TreeNode nodeForObjectType = new TreeNode(objectType.ObjectTypeName)
    {
      Tag = (object) objectType.ObjectTypeID
    };
    ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
    nodeForObjectType.ImageIndex = nodeForObjectType.SelectedImageIndex = categoryTypeIconService.IndexOf(4, objectType.ObjectTypeID);
    foreach (IMSObjectType objectType1 in MetaDataHelper.GetObjectTypeChildrenID(objectType.ObjectTypeID).Select<int, IMSObjectType>((Func<int, IMSObjectType>) (o => MetaDataHelper.GetObjectType(o))).OrderBy<IMSObjectType, string>((Func<IMSObjectType, string>) (o => o.ObjectTypeName)).ToArray<IMSObjectType>())
      nodeForObjectType.Nodes.Add(this.CreateTreeSelectedDialogNodeForObjectType(objectType1));
    return nodeForObjectType;
  }

  private void RemoveProjectType()
  {
    int num = this._filter.ProjectTypes.IndexOf(this._selectedProjectType);
    this._filter.ProjectTypes.Remove(this._selectedProjectType);
    if (num > 0)
    {
      this._projectTypesTree.SelectedItem = (object) this._filter.ProjectTypes[num - 1];
    }
    else
    {
      if (num != 0 || this._filter.ProjectTypes.Count <= 0)
        return;
      this._projectTypesTree.SelectedItem = (object) this._filter.ProjectTypes[0];
    }
  }

  private void CheckAllPartTypes()
  {
    this._partTypesTree.SuspendDataUpdate();
    try
    {
      this._selectedProjectType.CheckPartTypesAndDescendants();
    }
    finally
    {
      this._partTypesTree.ResumeDataUpdate();
    }
  }

  private void UncheckAllPartTypes()
  {
    this._partTypesTree.SuspendDataUpdate();
    try
    {
      this._selectedProjectType.UncheckPartTypesAndDescendants();
    }
    finally
    {
      this._partTypesTree.ResumeDataUpdate();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CompositionByObjectTypesFilterEditorControl));
    this._partTypesTree = new Intermech.VirtualTreeView.VirtualTreeView();
    this._partTypeCheckBoxColumn = new Column();
    this._partTypesTreeCheckBoxCellEditor = new CellEditor();
    this._partTypesTreeCheckBox = new CheckBox();
    this._partTypeNameColumn = new Column();
    this._partTypesContextMenuStrip = new ContextMenuStrip(this.components);
    this._checkAllPartTypesToolStripMenuItem = new ToolStripMenuItem();
    this._uncheckAllPartTypesToolStripMenuItem = new ToolStripMenuItem();
    this._partTypesToolStrip = new ToolStrip();
    this._checkAllPartTypesToolStripButton = new ToolStripButton();
    this._uncheckAllPartTypesToolStripButton = new ToolStripButton();
    this._projectTypesTree = new Intermech.VirtualTreeView.VirtualTreeView();
    this._projectTypeNameColumn = new Column();
    this._projectTypesContextMenuStrip = new ContextMenuStrip(this.components);
    this._addProjectTypeToolStripMenuItem = new ToolStripMenuItem();
    this._removeProjectTypeToolStripMenuItem = new ToolStripMenuItem();
    this._projectTypesToolStrip = new ToolStrip();
    this._addProjectTypeToolStripButton = new ToolStripButton();
    this._removeProjectTypeToolStripButton = new ToolStripButton();
    this.splitContainer1 = new SplitContainer();
    this._partTypesTree.BeginInit();
    this._partTypesContextMenuStrip.SuspendLayout();
    this._partTypesToolStrip.SuspendLayout();
    this._projectTypesTree.BeginInit();
    this._projectTypesContextMenuStrip.SuspendLayout();
    this._projectTypesToolStrip.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this._partTypesTree.AllowDrop = true;
    this._partTypesTree.AllowIndividualRowResize = false;
    this._partTypesTree.AllowMultiSelect = false;
    this._partTypesTree.AllowRowResize = false;
    this._partTypesTree.AllowUserPinnedColumns = false;
    this._partTypesTree.AutoFitColumns = true;
    this._partTypesTree.Columns.Add(this._partTypeCheckBoxColumn);
    this._partTypesTree.Columns.Add(this._partTypeNameColumn);
    this._partTypesTree.ContextMenuStrip = this._partTypesContextMenuStrip;
    this._partTypesTree.DisableHeaderContextMenu = true;
    this._partTypesTree.Dock = DockStyle.Fill;
    this._partTypesTree.Editors.Add(this._partTypesTreeCheckBoxCellEditor);
    this._partTypesTree.ImageList = (ImageList) null;
    this._partTypesTree.LineStyle = LineStyle.Dot;
    this._partTypesTree.Location = new Point(0, 25);
    this._partTypesTree.MainColumn = this._partTypeNameColumn;
    this._partTypesTree.MinRowHeight = 21;
    this._partTypesTree.Name = "_partTypesTree";
    this._partTypesTree.PrefixColumn = this._partTypeCheckBoxColumn;
    this._partTypesTree.RowHeight = 21;
    this._partTypesTree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._partTypesTree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._partTypesTree.SelectBeforeEdit = true;
    this._partTypesTree.ShowRootRow = false;
    this._partTypesTree.Size = new Size(278, 336);
    this._partTypesTree.SuppressErrorMessages = true;
    this._partTypesTree.TabIndex = 3;
    this._partTypesTree.SetCellValue += new SetCellValueHandler(this.PartTypesTree_SetCellValue);
    this._partTypeCheckBoxColumn.Caption = (string) null;
    this._partTypeCheckBoxColumn.CellEditor = this._partTypesTreeCheckBoxCellEditor;
    this._partTypeCheckBoxColumn.Name = "_partTypeCheckBoxColumn";
    this._partTypeCheckBoxColumn.Sortable = false;
    this._partTypeCheckBoxColumn.Width = 84;
    this._partTypesTreeCheckBoxCellEditor.CellAlignment = ContentAlignment.MiddleCenter;
    this._partTypesTreeCheckBoxCellEditor.Control = (Control) this._partTypesTreeCheckBox;
    this._partTypesTreeCheckBoxCellEditor.DisplayMode = CellEditorDisplayMode.Always;
    this._partTypesTreeCheckBoxCellEditor.UseCellHeight = false;
    this._partTypesTreeCheckBoxCellEditor.UseCellWidth = false;
    this._partTypesTreeCheckBox.FlatStyle = FlatStyle.System;
    this._partTypesTreeCheckBox.Location = new Point(3, 28);
    this._partTypesTreeCheckBox.Name = "_partTypesTreeCheckBox";
    this._partTypesTreeCheckBox.Size = new Size(13, 13);
    this._partTypesTreeCheckBox.TabIndex = 0;
    this._partTypesTreeCheckBox.UseVisualStyleBackColor = true;
    this._partTypeNameColumn.Caption = "Скрываемые дочерние типы объектов";
    this._partTypeNameColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._partTypeNameColumn.Name = "_partTypeNameColumn";
    this._partTypeNameColumn.Sortable = false;
    this._partTypeNameColumn.Width = 274;
    this._partTypesContextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._checkAllPartTypesToolStripMenuItem,
      (ToolStripItem) this._uncheckAllPartTypesToolStripMenuItem
    });
    this._partTypesContextMenuStrip.Name = "_partTypesContextMenuStrip";
    this._partTypesContextMenuStrip.Size = new Size(176 /*0xB0*/, 48 /*0x30*/);
    this._checkAllPartTypesToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_checkAllPartTypesToolStripMenuItem.Image");
    this._checkAllPartTypesToolStripMenuItem.Name = "_checkAllPartTypesToolStripMenuItem";
    this._checkAllPartTypesToolStripMenuItem.Size = new Size(175, 22);
    this._checkAllPartTypesToolStripMenuItem.Text = "Отметить все";
    this._checkAllPartTypesToolStripMenuItem.Click += new EventHandler(this.CheckAllPartTypesToolStripMenuItem_Click);
    this._uncheckAllPartTypesToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_uncheckAllPartTypesToolStripMenuItem.Image");
    this._uncheckAllPartTypesToolStripMenuItem.Name = "_uncheckAllPartTypesToolStripMenuItem";
    this._uncheckAllPartTypesToolStripMenuItem.Size = new Size(175, 22);
    this._uncheckAllPartTypesToolStripMenuItem.Text = "Снять все отметки";
    this._uncheckAllPartTypesToolStripMenuItem.Click += new EventHandler(this.UncheckAllPartTypesToolStripMenuItem_Click);
    this._partTypesToolStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._checkAllPartTypesToolStripButton,
      (ToolStripItem) this._uncheckAllPartTypesToolStripButton
    });
    this._partTypesToolStrip.Location = new Point(0, 0);
    this._partTypesToolStrip.Name = "_partTypesToolStrip";
    this._partTypesToolStrip.Size = new Size(278, 25);
    this._partTypesToolStrip.TabIndex = 0;
    this._partTypesToolStrip.Text = "toolStrip1";
    this._checkAllPartTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._checkAllPartTypesToolStripButton.Image = (Image) componentResourceManager.GetObject("_checkAllPartTypesToolStripButton.Image");
    this._checkAllPartTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._checkAllPartTypesToolStripButton.Name = "_checkAllPartTypesToolStripButton";
    this._checkAllPartTypesToolStripButton.Size = new Size(23, 22);
    this._checkAllPartTypesToolStripButton.Text = "Отметить все";
    this._checkAllPartTypesToolStripButton.Click += new EventHandler(this.CheckAllPartTypesToolStripButton_Click);
    this._uncheckAllPartTypesToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._uncheckAllPartTypesToolStripButton.Image = (Image) componentResourceManager.GetObject("_uncheckAllPartTypesToolStripButton.Image");
    this._uncheckAllPartTypesToolStripButton.ImageTransparentColor = Color.Magenta;
    this._uncheckAllPartTypesToolStripButton.Name = "_uncheckAllPartTypesToolStripButton";
    this._uncheckAllPartTypesToolStripButton.Size = new Size(23, 22);
    this._uncheckAllPartTypesToolStripButton.Text = "Убрать все отметки";
    this._uncheckAllPartTypesToolStripButton.Click += new EventHandler(this.UncheckAllPartTypesToolStripButton_Click);
    this._projectTypesTree.AllowDrop = true;
    this._projectTypesTree.AllowIndividualRowResize = false;
    this._projectTypesTree.AllowMultiSelect = false;
    this._projectTypesTree.AllowRowResize = false;
    this._projectTypesTree.AllowUserPinnedColumns = false;
    this._projectTypesTree.AutoFitColumns = true;
    this._projectTypesTree.Columns.Add(this._projectTypeNameColumn);
    this._projectTypesTree.ContextMenuStrip = this._projectTypesContextMenuStrip;
    this._projectTypesTree.DisableHeaderContextMenu = true;
    this._projectTypesTree.Dock = DockStyle.Fill;
    this._projectTypesTree.ImageList = (ImageList) null;
    this._projectTypesTree.LineStyle = LineStyle.Dot;
    this._projectTypesTree.Location = new Point(0, 25);
    this._projectTypesTree.MainColumn = this._projectTypeNameColumn;
    this._projectTypesTree.MinRowHeight = 21;
    this._projectTypesTree.Name = "_projectTypesTree";
    this._projectTypesTree.RowHeight = 21;
    this._projectTypesTree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._projectTypesTree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._projectTypesTree.SelectBeforeEdit = true;
    this._projectTypesTree.ShowRootRow = false;
    this._projectTypesTree.Size = new Size(239, 336);
    this._projectTypesTree.SuppressErrorMessages = true;
    this._projectTypesTree.TabIndex = 3;
    this._projectTypesTree.SelectionChanged += new EventHandler(this.ProjectTypesTree_SelectionChanged);
    this._projectTypeNameColumn.Caption = "Родительские типы объектов";
    this._projectTypeNameColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._projectTypeNameColumn.Name = "_projectTypeNameColumn";
    this._projectTypeNameColumn.Sortable = false;
    this._projectTypeNameColumn.Width = 235;
    this._projectTypesContextMenuStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addProjectTypeToolStripMenuItem,
      (ToolStripItem) this._removeProjectTypeToolStripMenuItem
    });
    this._projectTypesContextMenuStrip.Name = "_projectTypesContextMenuStrip";
    this._projectTypesContextMenuStrip.Size = new Size((int) sbyte.MaxValue, 48 /*0x30*/);
    this._addProjectTypeToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addProjectTypeToolStripMenuItem.Name = "_addProjectTypeToolStripMenuItem";
    this._addProjectTypeToolStripMenuItem.Size = new Size(126, 22);
    this._addProjectTypeToolStripMenuItem.Text = "Добавить";
    this._addProjectTypeToolStripMenuItem.Click += new EventHandler(this.AddProjectTypeToolStripMenuItem_Click);
    this._removeProjectTypeToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._removeProjectTypeToolStripMenuItem.Name = "_removeProjectTypeToolStripMenuItem";
    this._removeProjectTypeToolStripMenuItem.Size = new Size(126, 22);
    this._removeProjectTypeToolStripMenuItem.Text = "Удалить";
    this._removeProjectTypeToolStripMenuItem.Click += new EventHandler(this.RemoveProjectTypeToolStripMenuItem_Click);
    this._projectTypesToolStrip.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this._addProjectTypeToolStripButton,
      (ToolStripItem) this._removeProjectTypeToolStripButton
    });
    this._projectTypesToolStrip.Location = new Point(0, 0);
    this._projectTypesToolStrip.Name = "_projectTypesToolStrip";
    this._projectTypesToolStrip.Size = new Size(239, 25);
    this._projectTypesToolStrip.TabIndex = 0;
    this._projectTypesToolStrip.Text = "toolStrip2";
    this._addProjectTypeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addProjectTypeToolStripButton.Image = (Image) Resources.AddStandart;
    this._addProjectTypeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addProjectTypeToolStripButton.Name = "_addProjectTypeToolStripButton";
    this._addProjectTypeToolStripButton.Size = new Size(23, 22);
    this._addProjectTypeToolStripButton.Text = "Добавить";
    this._addProjectTypeToolStripButton.Click += new EventHandler(this.AddProjectTypeToolStripButton_Click);
    this._removeProjectTypeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeProjectTypeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeProjectTypeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeProjectTypeToolStripButton.Name = "_removeProjectTypeToolStripButton";
    this._removeProjectTypeToolStripButton.Size = new Size(23, 22);
    this._removeProjectTypeToolStripButton.Text = "Удалить";
    this._removeProjectTypeToolStripButton.Click += new EventHandler(this.RemoveProjectTypeToolStripButton_Click);
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.Location = new Point(0, 0);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Panel1.Controls.Add((Control) this._projectTypesTree);
    this.splitContainer1.Panel1.Controls.Add((Control) this._projectTypesToolStrip);
    this.splitContainer1.Panel2.Controls.Add((Control) this._partTypesTree);
    this.splitContainer1.Panel2.Controls.Add((Control) this._partTypesTreeCheckBox);
    this.splitContainer1.Panel2.Controls.Add((Control) this._partTypesToolStrip);
    this.splitContainer1.Size = new Size(521, 361);
    this.splitContainer1.SplitterDistance = 239;
    this.splitContainer1.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Name = nameof (CompositionByObjectTypesFilterEditorControl);
    this.Size = new Size(521, 361);
    this._partTypesTree.EndInit();
    this._partTypesContextMenuStrip.ResumeLayout(false);
    this._partTypesToolStrip.ResumeLayout(false);
    this._partTypesToolStrip.PerformLayout();
    this._projectTypesTree.EndInit();
    this._projectTypesContextMenuStrip.ResumeLayout(false);
    this._projectTypesToolStrip.ResumeLayout(false);
    this._projectTypesToolStrip.PerformLayout();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.Panel2.PerformLayout();
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class CompositionByObjectTypesFilterProjectTypeRowBinding : ObjectRowBinding
  {
    public CompositionByObjectTypesFilterProjectTypeRowBinding()
      : base(typeof (CompositionByObjectTypesFilterProjectType))
    {
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      base.GetCellData(row, column, cellData);
      if (!(row.Item is CompositionByObjectTypesFilterProjectType filterProjectType) || !(column.DataField == "ProjectTypeName"))
        return;
      cellData.Value = (object) MetaDataHelper.GetObjectTypeName(filterProjectType.ProjectTypeID);
    }

    public override void GetRowData(Row row, RowData rowData)
    {
      base.GetRowData(row, rowData);
      if (!(row.Item is CompositionByObjectTypesFilterProjectType filterProjectType))
        return;
      ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
      rowData.ImageList = categoryTypeIconService.ImageList;
      rowData.ImageIndex = categoryTypeIconService.IndexOf(4, filterProjectType.ProjectTypeID);
    }
  }

  private sealed class CompositionByObjectTypesFilterPartTypeRowBinding : ObjectRowBinding
  {
    public CompositionByObjectTypesFilterPartTypeRowBinding()
      : base(typeof (CompositionByObjectTypesFilterPartType))
    {
      this.ChildProperty = "Children";
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      base.GetCellData(row, column, cellData);
      if (!(row.Item is CompositionByObjectTypesFilterPartType typesFilterPartType))
        return;
      if (column.DataField == "PartTypeCheckBox")
      {
        cellData.Value = (object) typesFilterPartType.Checked;
      }
      else
      {
        if (!(column.DataField == "PartTypeName"))
          return;
        IMSObjectType objectType = MetaDataHelper.GetObjectType(typesFilterPartType.PartTypeID);
        if (objectType.VersionsMode == ObjectVersionModes.Abstract)
        {
          cellData.EvenStyle = new Style(cellData.EvenStyle, new StyleDelta()
          {
            ForeColor = SystemColors.GrayText
          });
          cellData.OddStyle = new Style(cellData.OddStyle, new StyleDelta()
          {
            ForeColor = SystemColors.GrayText
          });
        }
        cellData.Value = (object) objectType.ObjectTypeName;
      }
    }

    public override void GetRowData(Row row, RowData rowData)
    {
      base.GetRowData(row, rowData);
      if (!(row.Item is CompositionByObjectTypesFilterPartType typesFilterPartType))
        return;
      ICategoryTypeIconService categoryTypeIconService = ServiceLocator.Get<ICategoryTypeIconService>();
      rowData.ImageList = categoryTypeIconService.ImageList;
      rowData.ImageIndex = categoryTypeIconService.IndexOf(4, typesFilterPartType.PartTypeID);
    }
  }
}
