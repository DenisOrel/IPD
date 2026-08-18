
// Type: Intermech.Search.ContextMenus.ContextMenuEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Properties;
using Intermech.Navigator.ContextMenu;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ContextMenus;

public sealed class ContextMenuEditorControl : UserControl, ISupportInitialize
{
  private long _contextMenuVersionID;
  private Intermech.Search.ContextMenus.ContextMenu _contextMenu;
  private Intermech.Search.ContextMenus.ContextMenu _contextMenuBackup;
  private ContextMenuItem _selectedContextMenuItem;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private FlowLayoutPanel flowLayoutPanel1;
  private Button _cancelButton;
  private Button _acceptButton;
  private ContextMenuStrip contextMenuStrip1;
  private Panel panel1;
  private ToolStrip toolStrip1;
  private Intermech.VirtualTreeView.VirtualTreeView _tree;
  private ToolStripButton _addButtonItemToolStripButton;
  private ToolStripButton _addDropDownItemToolStripButton;
  private ToolStripButton _removeToolStripButton;
  private ToolStripMenuItem _addButtonItemToolStripMenuItem;
  private ToolStripMenuItem _addDropDownItemToolStripMenuItem;
  private ToolStripMenuItem _removeToolStripMenuItem;
  private Column _commandColumn;
  private Column _textColumn;
  private CellEditor _textBoxCellEditor;
  private TextBox _textBox;
  private Column _beginGroupColumn;
  private CellEditor _checkBoxCellEditor;
  private CheckBox _checkBox;
  private ToolStripButton _moveTopToolStripButton;
  private ToolStripButton _moveUpToolStripButton;
  private ToolStripButton _moveDownToolStripButton;
  private ToolStripButton _moveBottomToolStripButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem _moveTopToolStripMenuItem;
  private ToolStripMenuItem _moveUpToolStripMenuItem;
  private ToolStripMenuItem _moveDownToolStripMenuItem;
  private ToolStripMenuItem _moveBottomToolStripMenuItem;

  public ContextMenuEditorControl() => this.InitializeComponent();

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ContextMenuVersionID
  {
    get => this._contextMenuVersionID;
    set
    {
      if (ObjectHelper.IsUnknownObjectVersionID(value))
        throw new ArgumentException();
      if (this._contextMenuVersionID == value)
        return;
      this._contextMenuVersionID = value;
      this.SetContextMenu(ServiceLocator.Get<IContextMenuClientService>().FindContextMenu(this._contextMenuVersionID) ?? new Intermech.Search.ContextMenus.ContextMenu());
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool HasChanges { get; private set; }

  public void Accept()
  {
    this._contextMenu.PropertyChanged -= new PropertyChangedEventHandler(this.ContextMenu_PropertyChanged);
    try
    {
      ServiceLocator.Get<IContextMenuClientService>().SaveContextMenu(this._contextMenuVersionID, this._contextMenu);
    }
    finally
    {
      this._contextMenu.PropertyChanged += new PropertyChangedEventHandler(this.ContextMenu_PropertyChanged);
    }
    this._contextMenuBackup = this._contextMenu.Clone();
    this.HasChanges = false;
    this.UpdateControls();
  }

  public void BeginInit()
  {
  }

  public void EndInit()
  {
    if (this.DesignMode)
      return;
    this._tree.RowBindings.Add((RowBinding) new ContextMenuEditorControl.ContextMenuRowBinding(this._textColumn));
    this._tree.RowBindings.Add((RowBinding) new ContextMenuEditorControl.ContextMenuItemRowBinding(this._commandColumn, this._textColumn, this._beginGroupColumn));
  }

  private void AddButtonItemToolStripButton_Click(object sender, EventArgs e)
  {
    this.AddButtonItem();
  }

  private void AddDropDownItemToolStripButton_Click(object sender, EventArgs e)
  {
    this.AddDropDownMenuItem();
  }

  private void RemoveToolStripButton_Click(object sender, EventArgs e) => this.Remove();

  private void MoveTopToolStripButton_Click(object sender, EventArgs e) => this.MoveTop();

  private void MoveUpToolStripButton_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveDownToolStripButton_Click(object sender, EventArgs e) => this.MoveDown();

  private void MoveBottomToolStripButton_Click(object sender, EventArgs e) => this.MoveBottom();

  private void AddButtonItemToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddButtonItem();
  }

  private void AddDropDownItemToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddDropDownMenuItem();
  }

  private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) => this.Remove();

  private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveTopToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveTop();

  private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveDown();

  private void MoveBottomToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveBottom();

  private void AcceptButton_Click(object sender, EventArgs e) => this.Accept();

  private void CancelButton_Click(object sender, EventArgs e) => this.Cancel();

  private void Tree_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  private void ContextMenu_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    this.HasChanges = true;
    this.UpdateControls();
  }

  private void SetContextMenu(Intermech.Search.ContextMenus.ContextMenu contextMenu)
  {
    if (this._contextMenu != null)
      this._contextMenu.PropertyChanged -= new PropertyChangedEventHandler(this.ContextMenu_PropertyChanged);
    this._contextMenu = contextMenu;
    if (this._contextMenu != null)
      this._contextMenu.PropertyChanged += new PropertyChangedEventHandler(this.ContextMenu_PropertyChanged);
    this._contextMenuBackup = contextMenu.Clone();
    this._tree.DataSource = (object) this._contextMenu;
    this.HasChanges = false;
    this.UpdateControls();
  }

  private void UpdateControls()
  {
    this._selectedContextMenuItem = this._tree.SelectedItem as ContextMenuItem;
    this._addButtonItemToolStripButton.Enabled = this._addButtonItemToolStripMenuItem.Enabled = this.CanAddButtonItem();
    this._addDropDownItemToolStripButton.Enabled = this._addDropDownItemToolStripMenuItem.Enabled = this.CanAddDropDownMenuItem();
    this._removeToolStripButton.Enabled = this._removeToolStripMenuItem.Enabled = this.CanRemove();
    this._moveTopToolStripButton.Enabled = this._moveTopToolStripMenuItem.Enabled = this.CanMoveTop();
    this._moveUpToolStripButton.Enabled = this._moveUpToolStripMenuItem.Enabled = this.CanMoveUp();
    this._moveDownToolStripButton.Enabled = this._moveDownToolStripMenuItem.Enabled = this.CanMoveDown();
    this._moveBottomToolStripButton.Enabled = this._moveBottomToolStripMenuItem.Enabled = this.CanMoveBottom();
    this._acceptButton.Enabled = this.CanAccept();
    this._cancelButton.Enabled = this.CanCancel();
  }

  private bool CanAddButtonItem() => true;

  private bool CanAddDropDownMenuItem() => true;

  private bool CanRemove() => this._selectedContextMenuItem != null;

  private bool CanAccept() => this.HasChanges;

  private bool CanCancel() => this.HasChanges;

  private bool CanMoveTop()
  {
    return this._selectedContextMenuItem != null && this._selectedContextMenuItem.Parent != null && this._selectedContextMenuItem.Parent.Items.CanMoveTop(this._selectedContextMenuItem);
  }

  private bool CanMoveUp()
  {
    return this._selectedContextMenuItem != null && this._selectedContextMenuItem.Parent != null && this._selectedContextMenuItem.Parent.Items.CanMoveUp(this._selectedContextMenuItem);
  }

  private bool CanMoveDown()
  {
    return this._selectedContextMenuItem != null && this._selectedContextMenuItem.Parent != null && this._selectedContextMenuItem.Parent.Items.CanMoveDown(this._selectedContextMenuItem);
  }

  private bool CanMoveBottom()
  {
    return this._selectedContextMenuItem != null && this._selectedContextMenuItem.Parent != null && this._selectedContextMenuItem.Parent.Items.CanMoveBottom(this._selectedContextMenuItem);
  }

  private void AddButtonItem()
  {
    using (SelectCommandDialog selectCommandDialog = new SelectCommandDialog())
    {
      if (selectCommandDialog.ShowDialog() != DialogResult.OK)
        return;
      List<ContextMenuItem> contextMenuItemList = new List<ContextMenuItem>();
      foreach (string selectedCommand in selectCommandDialog.SelectedCommands)
      {
        MenuTemplateNode templateNodeForCommand = ContextMenuClientHelper.GetMenuTemplateNodeForCommand(selectedCommand);
        if (templateNodeForCommand != null)
          contextMenuItemList.Add(this.CreateContextMenuItemFromMenuTemplateNode(templateNodeForCommand));
      }
      this.GetContainerForAdding().Items.AddRange((IEnumerable<ContextMenuItem>) contextMenuItemList);
      if (contextMenuItemList.Count <= 0)
        return;
      this.ShowAndSelectContextMenuItem(contextMenuItemList.LastOrDefault<ContextMenuItem>());
    }
  }

  private ContextMenuItem CreateContextMenuItemFromMenuTemplateNode(
    MenuTemplateNode menuTemplateNode)
  {
    ContextMenuItem menuTemplateNode1 = new ContextMenuItem(menuTemplateNode.Name);
    menuTemplateNode1.Text = menuTemplateNode.Text;
    foreach (MenuTemplateNode node in menuTemplateNode.Nodes)
      menuTemplateNode1.Items.Add(this.CreateContextMenuItemFromMenuTemplateNode(node));
    return menuTemplateNode1;
  }

  private IContextMenuItemContainer GetContainerForAdding()
  {
    IContextMenuItemContainer containerForAdding = (IContextMenuItemContainer) null;
    if (this._tree.SelectedItem is ContextMenuItem selectedItem)
      containerForAdding = selectedItem.GetAncestorsAndSelf().FirstOrDefault<IContextMenuItemContainer>((Func<IContextMenuItemContainer, bool>) (o => o is ContextMenuItem && string.IsNullOrEmpty(((ContextMenuItem) o).CommandName)));
    if (containerForAdding == null)
      containerForAdding = (IContextMenuItemContainer) this._contextMenu;
    return containerForAdding;
  }

  private void ShowAndSelectContextMenuItem(ContextMenuItem contextMenuItem)
  {
    IContextMenuItemContainer[] array = contextMenuItem.GetAncestorsAndSelf().Reverse<IContextMenuItemContainer>().ToArray<IContextMenuItemContainer>();
    Row row = this._tree.RootRow;
    foreach (ContextMenuItem contextMenuItem1 in ((IEnumerable<IContextMenuItemContainer>) array).Skip<IContextMenuItemContainer>(1))
    {
      int childIndex = contextMenuItem1.Parent.Items.IndexOf(contextMenuItem1);
      row = row.ChildRowByIndex(childIndex);
      row.EnsureVisible();
      row.Expand();
      this._tree.SelectedRow = row;
    }
  }

  private void AddDropDownMenuItem()
  {
    IContextMenuItemContainer containerForAdding = this.GetContainerForAdding();
    this._tree.SuspendDataUpdate();
    ContextMenuItem contextMenuItem = new ContextMenuItem()
    {
      Text = "Новый пункт контекстного меню"
    };
    try
    {
      containerForAdding.Items.Add(contextMenuItem);
    }
    finally
    {
      this._tree.ResumeDataUpdate();
    }
    this.ShowAndSelectContextMenuItem(contextMenuItem);
  }

  private void Remove()
  {
    ContextMenuItem selectedItem = this._tree.SelectedItem as ContextMenuItem;
    IContextMenuItemContainer menuItemContainer = (IContextMenuItemContainer) selectedItem.GetPreviousSiblings().LastOrDefault<ContextMenuItem>() ?? selectedItem.Parent;
    this._tree.SuspendDataUpdate();
    try
    {
      selectedItem.Parent.Items.Remove(selectedItem);
    }
    finally
    {
      this._tree.ResumeDataUpdate();
      if (menuItemContainer is ContextMenuItem)
        this.ShowAndSelectContextMenuItem((ContextMenuItem) menuItemContainer);
      else
        this._tree.RootRow.Selected = true;
    }
  }

  private void MoveTop()
  {
    this._selectedContextMenuItem.Parent.Items.MoveTop(this._selectedContextMenuItem);
  }

  private void MoveUp()
  {
    this._selectedContextMenuItem.Parent.Items.MoveUp(this._selectedContextMenuItem);
  }

  private void MoveDown()
  {
    this._selectedContextMenuItem.Parent.Items.MoveDown(this._selectedContextMenuItem);
  }

  private void MoveBottom()
  {
    this._selectedContextMenuItem.Parent.Items.MoveBottom(this._selectedContextMenuItem);
  }

  private void Cancel()
  {
    if (MessageBox.Show("Контекстное меню было изменено. Отменить изменения?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.SetContextMenu(this._contextMenuBackup);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextMenuEditorControl));
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.panel1 = new Panel();
    this._tree = new Intermech.VirtualTreeView.VirtualTreeView();
    this._textColumn = new Column();
    this._textBoxCellEditor = new CellEditor();
    this._textBox = new TextBox();
    this._commandColumn = new Column();
    this._beginGroupColumn = new Column();
    this._checkBoxCellEditor = new CellEditor();
    this._checkBox = new CheckBox();
    this.contextMenuStrip1 = new ContextMenuStrip(this.components);
    this._addButtonItemToolStripMenuItem = new ToolStripMenuItem();
    this._addDropDownItemToolStripMenuItem = new ToolStripMenuItem();
    this._removeToolStripMenuItem = new ToolStripMenuItem();
    this.toolStrip1 = new ToolStrip();
    this._addButtonItemToolStripButton = new ToolStripButton();
    this._addDropDownItemToolStripButton = new ToolStripButton();
    this._removeToolStripButton = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._moveTopToolStripButton = new ToolStripButton();
    this._moveUpToolStripButton = new ToolStripButton();
    this._moveDownToolStripButton = new ToolStripButton();
    this._moveBottomToolStripButton = new ToolStripButton();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._moveTopToolStripMenuItem = new ToolStripMenuItem();
    this._moveUpToolStripMenuItem = new ToolStripMenuItem();
    this._moveDownToolStripMenuItem = new ToolStripMenuItem();
    this._moveBottomToolStripMenuItem = new ToolStripMenuItem();
    this.tableLayoutPanel1.SuspendLayout();
    this.flowLayoutPanel1.SuspendLayout();
    this.panel1.SuspendLayout();
    this._tree.BeginInit();
    this.contextMenuStrip1.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.flowLayoutPanel1, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 0);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
    this.tableLayoutPanel1.Size = new Size(755, 358);
    this.tableLayoutPanel1.TabIndex = 0;
    this.flowLayoutPanel1.Controls.Add((Control) this._cancelButton);
    this.flowLayoutPanel1.Controls.Add((Control) this._acceptButton);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
    this.flowLayoutPanel1.Location = new Point(3, 311);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(749, 44);
    this.flowLayoutPanel1.TabIndex = 0;
    this._cancelButton.Location = new Point(671, 3);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Отмена";
    this._cancelButton.UseVisualStyleBackColor = true;
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    this._acceptButton.Location = new Point(590, 3);
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Size = new Size(75, 23);
    this._acceptButton.TabIndex = 1;
    this._acceptButton.Text = "Применить";
    this._acceptButton.UseVisualStyleBackColor = true;
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    this.panel1.Controls.Add((Control) this._tree);
    this.panel1.Controls.Add((Control) this.toolStrip1);
    this.panel1.Controls.Add((Control) this._textBox);
    this.panel1.Controls.Add((Control) this._checkBox);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(3, 3);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(749, 302);
    this.panel1.TabIndex = 1;
    this._tree.AllowDrop = true;
    this._tree.AllowIndividualRowResize = false;
    this._tree.AllowMultiSelect = false;
    this._tree.AllowRowResize = false;
    this._tree.AllowUserPinnedColumns = false;
    this._tree.AutoFitColumns = true;
    this._tree.Columns.Add(this._textColumn);
    this._tree.Columns.Add(this._commandColumn);
    this._tree.Columns.Add(this._beginGroupColumn);
    this._tree.ContextMenuStrip = this.contextMenuStrip1;
    this._tree.DisableHeaderContextMenu = true;
    this._tree.Dock = DockStyle.Fill;
    this._tree.Editors.Add(this._textBoxCellEditor);
    this._tree.Editors.Add(this._checkBoxCellEditor);
    this._tree.ImageList = (ImageList) null;
    this._tree.LineStyle = LineStyle.Dot;
    this._tree.Location = new Point(0, 25);
    this._tree.MinRowHeight = 21;
    this._tree.Name = "_tree";
    this._tree.RowHeight = 21;
    this._tree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this._tree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this._tree.SelectBeforeEdit = true;
    this._tree.Size = new Size(749, 277);
    this._tree.SuppressErrorMessages = true;
    this._tree.TabIndex = 4;
    this._tree.SelectionChanged += new EventHandler(this.Tree_SelectionChanged);
    this._textColumn.Caption = "Текст";
    this._textColumn.CellEditor = this._textBoxCellEditor;
    this._textColumn.Name = "_textColumn";
    this._textColumn.Sortable = false;
    this._textColumn.ToolTip = "Текст пункта контекстного меню";
    this._textColumn.Width = 248;
    this._textBoxCellEditor.Control = (Control) this._textBox;
    this._textBox.Location = new Point(8, 33);
    this._textBox.Name = "_textBox";
    this._textBox.Size = new Size(100, 20);
    this._textBox.TabIndex = 5;
    this._textBox.Visible = false;
    this._commandColumn.Caption = "Команда";
    this._commandColumn.Name = "_commandColumn";
    this._commandColumn.Sortable = false;
    this._commandColumn.ToolTip = "Команда контекстного меню";
    this._commandColumn.Width = 248;
    this._beginGroupColumn.Caption = "Начало группы";
    this._beginGroupColumn.CellEditor = this._checkBoxCellEditor;
    this._beginGroupColumn.Name = "_beginGroupColumn";
    this._beginGroupColumn.Sortable = false;
    this._beginGroupColumn.ToolTip = "Начало группы";
    this._beginGroupColumn.Width = 248;
    this._checkBoxCellEditor.CellAlignment = ContentAlignment.MiddleCenter;
    this._checkBoxCellEditor.Control = (Control) this._checkBox;
    this._checkBoxCellEditor.DisplayMode = CellEditorDisplayMode.Always;
    this._checkBoxCellEditor.UseCellHeight = false;
    this._checkBoxCellEditor.UseCellWidth = false;
    this._checkBox.AutoSize = true;
    this._checkBox.Location = new Point(262, 7);
    this._checkBox.Name = "_checkBox";
    this._checkBox.Size = new Size(15, 14);
    this._checkBox.TabIndex = 6;
    this._checkBox.UseVisualStyleBackColor = true;
    this._checkBox.Visible = false;
    this.contextMenuStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this._addButtonItemToolStripMenuItem,
      (ToolStripItem) this._addDropDownItemToolStripMenuItem,
      (ToolStripItem) this._removeToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._moveTopToolStripMenuItem,
      (ToolStripItem) this._moveUpToolStripMenuItem,
      (ToolStripItem) this._moveDownToolStripMenuItem,
      (ToolStripItem) this._moveBottomToolStripMenuItem
    });
    this.contextMenuStrip1.Name = "contextMenuStrip1";
    this.contextMenuStrip1.Size = new Size(199, 186);
    this._addButtonItemToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addButtonItemToolStripMenuItem.Name = "_addButtonItemToolStripMenuItem";
    this._addButtonItemToolStripMenuItem.Size = new Size(198, 22);
    this._addButtonItemToolStripMenuItem.Text = "Добавить команду";
    this._addButtonItemToolStripMenuItem.Click += new EventHandler(this.AddButtonItemToolStripMenuItem_Click);
    this._addDropDownItemToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_addDropDownItemToolStripMenuItem.Image");
    this._addDropDownItemToolStripMenuItem.Name = "_addDropDownItemToolStripMenuItem";
    this._addDropDownItemToolStripMenuItem.Size = new Size(198, 22);
    this._addDropDownItemToolStripMenuItem.Text = "Добавить подменю";
    this._addDropDownItemToolStripMenuItem.Click += new EventHandler(this.AddDropDownItemToolStripMenuItem_Click);
    this._removeToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
    this._removeToolStripMenuItem.Size = new Size(198, 22);
    this._removeToolStripMenuItem.Text = "Удалить";
    this._removeToolStripMenuItem.Click += new EventHandler(this.RemoveToolStripMenuItem_Click);
    this.toolStrip1.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this._addButtonItemToolStripButton,
      (ToolStripItem) this._addDropDownItemToolStripButton,
      (ToolStripItem) this._removeToolStripButton,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._moveTopToolStripButton,
      (ToolStripItem) this._moveUpToolStripButton,
      (ToolStripItem) this._moveDownToolStripButton,
      (ToolStripItem) this._moveBottomToolStripButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(749, 25);
    this.toolStrip1.TabIndex = 0;
    this.toolStrip1.Text = "toolStrip1";
    this._addButtonItemToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addButtonItemToolStripButton.Image = (Image) Resources.AddStandart;
    this._addButtonItemToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addButtonItemToolStripButton.Name = "_addButtonItemToolStripButton";
    this._addButtonItemToolStripButton.Size = new Size(23, 22);
    this._addButtonItemToolStripButton.Text = "Добавить команду";
    this._addButtonItemToolStripButton.Click += new EventHandler(this.AddButtonItemToolStripButton_Click);
    this._addDropDownItemToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addDropDownItemToolStripButton.Image = (Image) componentResourceManager.GetObject("_addDropDownItemToolStripButton.Image");
    this._addDropDownItemToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addDropDownItemToolStripButton.Name = "_addDropDownItemToolStripButton";
    this._addDropDownItemToolStripButton.Size = new Size(23, 22);
    this._addDropDownItemToolStripButton.Text = "Добавить подменю";
    this._addDropDownItemToolStripButton.Click += new EventHandler(this.AddDropDownItemToolStripButton_Click);
    this._removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._removeToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._removeToolStripButton.ImageTransparentColor = Color.Magenta;
    this._removeToolStripButton.Name = "_removeToolStripButton";
    this._removeToolStripButton.Size = new Size(23, 22);
    this._removeToolStripButton.Text = "Удалить";
    this._removeToolStripButton.Click += new EventHandler(this.RemoveToolStripButton_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this._moveTopToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveTopToolStripButton.Image = (Image) Resources.arrow_top_blue;
    this._moveTopToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveTopToolStripButton.Name = "_moveTopToolStripButton";
    this._moveTopToolStripButton.Size = new Size(23, 22);
    this._moveTopToolStripButton.Text = "Переместить в начало";
    this._moveTopToolStripButton.Click += new EventHandler(this.MoveTopToolStripButton_Click);
    this._moveUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveUpToolStripButton.Image = (Image) Resources.arrow_up_blue;
    this._moveUpToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveUpToolStripButton.Name = "_moveUpToolStripButton";
    this._moveUpToolStripButton.Size = new Size(23, 22);
    this._moveUpToolStripButton.Text = "Переместить вверх";
    this._moveUpToolStripButton.Click += new EventHandler(this.MoveUpToolStripButton_Click);
    this._moveDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveDownToolStripButton.Image = (Image) Resources.arrow_down_blue;
    this._moveDownToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveDownToolStripButton.Name = "_moveDownToolStripButton";
    this._moveDownToolStripButton.Size = new Size(23, 22);
    this._moveDownToolStripButton.Text = "Переместить вниз";
    this._moveDownToolStripButton.Click += new EventHandler(this.MoveDownToolStripButton_Click);
    this._moveBottomToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveBottomToolStripButton.Image = (Image) Resources.arrow_bottom_blue;
    this._moveBottomToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveBottomToolStripButton.Name = "_moveBottomToolStripButton";
    this._moveBottomToolStripButton.Size = new Size(23, 22);
    this._moveBottomToolStripButton.Text = "Переместить в конец";
    this._moveBottomToolStripButton.Click += new EventHandler(this.MoveBottomToolStripButton_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(195, 6);
    this._moveTopToolStripMenuItem.Image = (Image) Resources.arrow_top_blue;
    this._moveTopToolStripMenuItem.Name = "_moveTopToolStripMenuItem";
    this._moveTopToolStripMenuItem.Size = new Size(198, 22);
    this._moveTopToolStripMenuItem.Text = "Переместить в начало";
    this._moveTopToolStripMenuItem.Click += new EventHandler(this.MoveTopToolStripMenuItem_Click);
    this._moveUpToolStripMenuItem.Image = (Image) Resources.arrow_up_blue;
    this._moveUpToolStripMenuItem.Name = "_moveUpToolStripMenuItem";
    this._moveUpToolStripMenuItem.Size = new Size(198, 22);
    this._moveUpToolStripMenuItem.Text = "Переместить вверх";
    this._moveUpToolStripMenuItem.Click += new EventHandler(this.MoveUpToolStripMenuItem_Click);
    this._moveDownToolStripMenuItem.Image = (Image) Resources.arrow_down_blue;
    this._moveDownToolStripMenuItem.Name = "_moveDownToolStripMenuItem";
    this._moveDownToolStripMenuItem.Size = new Size(198, 22);
    this._moveDownToolStripMenuItem.Text = "Переместить вниз";
    this._moveDownToolStripMenuItem.Click += new EventHandler(this.MoveDownToolStripMenuItem_Click);
    this._moveBottomToolStripMenuItem.Image = (Image) Resources.arrow_bottom_blue;
    this._moveBottomToolStripMenuItem.Name = "_moveBottomToolStripMenuItem";
    this._moveBottomToolStripMenuItem.Size = new Size(198, 22);
    this._moveBottomToolStripMenuItem.Text = "Переместить в конец";
    this._moveBottomToolStripMenuItem.Click += new EventHandler(this.MoveBottomToolStripMenuItem_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (ContextMenuEditorControl);
    this.Size = new Size(755, 358);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this._tree.EndInit();
    this.contextMenuStrip1.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
  }

  private sealed class ContextMenuRowBinding : ObjectRowBinding
  {
    private Column _textColumn;

    public ContextMenuRowBinding(Column textColumn)
      : base(typeof (Intermech.Search.ContextMenus.ContextMenu))
    {
      this._textColumn = textColumn != null ? textColumn : throw new ArgumentNullException(nameof (textColumn));
      this.ChildProperty = "Items";
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      base.GetCellData(row, column, cellData);
      if (column != this._textColumn)
        return;
      cellData.Value = (object) "Контекстное меню";
    }
  }

  private sealed class ContextMenuItemRowBinding : ObjectRowBinding
  {
    public ContextMenuItemRowBinding(Column commandColumn, Column textColumn, Column beginGroup)
      : base(typeof (ContextMenuItem))
    {
      if (commandColumn == null)
        throw new ArgumentNullException(nameof (commandColumn));
      if (textColumn == null)
        throw new ArgumentNullException(nameof (textColumn));
      if (beginGroup == null)
        throw new ArgumentNullException(nameof (beginGroup));
      this.CellBindings.Add((CellBinding) new ObjectCellBinding(commandColumn, "CommandName"));
      this.CellBindings.Add((CellBinding) new ObjectCellBinding(textColumn, "Text"));
      this.CellBindings.Add((CellBinding) new ObjectCellBinding(beginGroup, "BeginGroup"));
      this.ChildProperty = "Items";
    }

    public override void GetRowData(Row row, RowData rowData)
    {
      base.GetRowData(row, rowData);
      ContextMenuItem contextMenuItem = (ContextMenuItem) row.Item;
      if (string.IsNullOrEmpty(contextMenuItem.CommandName))
        return;
      MenuTemplateNode templateNodeForCommand = ContextMenuClientHelper.GetMenuTemplateNodeForCommand(contextMenuItem.CommandName);
      if (templateNodeForCommand == null)
        return;
      if (templateNodeForCommand.Image != null)
      {
        rowData.Image = templateNodeForCommand.Image;
      }
      else
      {
        if (templateNodeForCommand.ImageIndex < 0)
          return;
        rowData.ImageIndex = templateNodeForCommand.ImageIndex;
        rowData.ImageList = ContextMenuClientHelper.GetImageListForImageListSource(templateNodeForCommand.ImageListSource);
      }
    }
  }
}
