
// Type: Intermech.Search.ButtonBars.ButtonBarsEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core.Properties;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Search.ComponentModel;
using Intermech.Search.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ButtonBars;

public sealed class ButtonBarsEditorControl : UserControl
{
  private const string CommandColumnKey = "Command";
  private const string TextColumnKey = "Text";
  private const string ToolTipTextColumnKey = "ToolTipText";
  private const string DisplayTypeColumnKey = "Type";
  private bool _readOnly;
  private BindingListBase<ButtonBar> _buttonBars = new BindingListBase<ButtonBar>();
  private ButtonBar[] _buttonBarsBackup;
  private bool _hasChanges;
  private ButtonBar[] _selectedButtonBars = new ButtonBar[0];
  private ButtonBarButton[] _selectedButtonBarButtons = new ButtonBarButton[0];
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Search.UI.VirtualTree.VirtualTree _tree;
  private ToolStrip toolStrip1;
  private ToolStripButton _newButtonBarToolStripButton;
  private ToolStripButton _addButtonsToolStripButton;
  private ToolStripButton _deleteToolStripButton;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton _moveTopToolStripButton;
  private ToolStripButton _moveUpToolStripButton;
  private ToolStripButton _moveDownToolStripButton;
  private ToolStripButton _moveBottomToolStripButton;
  private Column _commandColumn;
  private Column _textColumn;
  private Column _displayTypeColumn;
  private Panel panel2;
  private ContextMenuStrip _contextMenuStrip;
  private ToolStripMenuItem _newButtonBarToolStripMenuItem;
  private ToolStripMenuItem _addButtonsToolStripMenuItem;
  private ToolStripMenuItem _deleteToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator2;
  private ToolStripMenuItem _moveTopToolStripMenuItem;
  private ToolStripMenuItem _moveUpToolStripMenuItem;
  private ToolStripMenuItem _moveDownToolStripMenuItem;
  private ToolStripMenuItem _moveBottomToolStripMenuItem;
  private ToolStripSeparator toolStripSeparator3;
  private ToolStripButton _beginGroupToolStripButton;
  private ToolStripButton _shiftLeftToolStripButton;
  private ToolStripButton _shiftRightToolStripButton;
  private ToolStripSeparator toolStripSeparator4;
  private ToolStripMenuItem _beginGroupToolStripMenuItem;
  private ToolStripMenuItem _shiftLeftToolStripMenuItem;
  private ToolStripMenuItem _shiftRightToolStripMenuItem;
  private Column _toopTipTextColumn;

  public ButtonBarsEditorControl()
  {
    this.InitializeComponent();
    this._commandColumn.DataField = "Command";
    this._textColumn.DataField = "Text";
    this._toopTipTextColumn.DataField = "ToolTipText";
    this._displayTypeColumn.DataField = "Type";
    ObjectRowBinding objectRowBinding = new ObjectRowBinding(typeof (ButtonBar));
    objectRowBinding.ChildProperty = "Buttons";
    ObjectCellBinding objectCellBinding = new ObjectCellBinding(this._commandColumn, "Name");
    objectCellBinding.Editor = new CellEditor((Control) new TextBox());
    objectRowBinding.CellBindings.Add((CellBinding) objectCellBinding);
    this._tree.RowBindings.Add((RowBinding) objectRowBinding);
    this._tree.RowBindings.Add((RowBinding) new ButtonBarsEditorControl.ButtonBarButtonRowBinding());
    this._tree.DataSource = (object) this._buttonBars;
    this._buttonBars.ListChanged += new ListChangedEventHandler(this.ButtonBars_ListChanged);
  }

  public event EventHandler Changed;

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.UpdateView();
    }
  }

  public ButtonBar[] ButtonBars
  {
    get => this._buttonBars.ToArray<ButtonBar>();
    set
    {
      this._buttonBarsBackup = value == null ? (ButtonBar[]) null : ((IEnumerable<ButtonBar>) value).Select<ButtonBar, ButtonBar>((Func<ButtonBar, ButtonBar>) (o => o.Clone())).ToArray<ButtonBar>();
      this._buttonBars.Clear();
      if (value != null)
        this._buttonBars.AddRange((IEnumerable<ButtonBar>) value);
      this.HasChanges = false;
    }
  }

  public bool HasChanges
  {
    get => this._hasChanges;
    private set
    {
      if (this._hasChanges == value)
        return;
      this._hasChanges = value;
      this.UpdateView();
      this.OnChanged();
    }
  }

  public void ApplyChanges()
  {
    this._buttonBarsBackup = this._buttonBars.Select<ButtonBar, ButtonBar>((Func<ButtonBar, ButtonBar>) (o => o.Clone())).ToArray<ButtonBar>();
    this.HasChanges = false;
  }

  public void CancelChanges()
  {
    this.ButtonBars = this._buttonBarsBackup;
    this.HasChanges = false;
  }

  private void ButtonBars_ListChanged(object sender, ListChangedEventArgs e)
  {
    this.HasChanges = true;
    this.UpdateView();
  }

  private void NewButtonBarToolStripButton_Click(object sender, EventArgs e) => this.NewButtonBar();

  private void AddButtonsToolStripButton_Click(object sender, EventArgs e) => this.AddButtons();

  private void DeleteToolStripButton_Click(object sender, EventArgs e) => this.Delete();

  private void MoveTopToolStripButton_Click(object sender, EventArgs e) => this.MoveTop();

  private void MoveUpToolStripButton_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveDownToolStripButton_Click(object sender, EventArgs e) => this.MoveDown();

  private void MoveBottomToolStripButton_Click(object sender, EventArgs e) => this.MoveBottom();

  private void BeginGroupToolStripButton_Click(object sender, EventArgs e) => this.BeginGroup();

  private void ShiftLeftToolStripButton_Click(object sender, EventArgs e) => this.ShiftLeft();

  private void ShiftRightToolStripButton_Click(object sender, EventArgs e) => this.ShiftRight();

  private void NewButtonBarToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.NewButtonBar();
  }

  private void AddButtonsToolStripMenuItem_Click(object sender, EventArgs e) => this.AddButtons();

  private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) => this.Delete();

  private void MoveTopToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveTop();

  private void MoveUpToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveUp();

  private void MoveDownToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveDown();

  private void MoveBottomToolStripMenuItem_Click(object sender, EventArgs e) => this.MoveBottom();

  private void BeginGroupToolStripMenuItem_Click(object sender, EventArgs e) => this.BeginGroup();

  private void ShiftLeftToolStripMenuItem_Click(object sender, EventArgs e) => this.ShiftLeft();

  private void ShiftRightToolStripMenuItem_Click(object sender, EventArgs e) => this.ShiftRight();

  private void Tree_SelectionChanged(object sender, EventArgs e)
  {
    this.SetSelectedButtonBars();
    this.SetSelectedButtonBarButtons();
    this.UpdateView();
  }

  private void NewButtonBar()
  {
    this._buttonBars.Add(new ButtonBar(Guid.NewGuid())
    {
      Name = "Новая кнопочная панель"
    });
  }

  private void AddButtons()
  {
    using (SelectCommandDialog selectCommandDialog = new SelectCommandDialog())
    {
      if (selectCommandDialog.ShowDialog() != DialogResult.OK)
        return;
      foreach (string selectedCommand in selectCommandDialog.SelectedCommands)
      {
        MenuTemplateNode templateNodeForCommand = ContextMenuHelper.GetContextMenuTemplateNodeForCommand(selectedCommand);
        if (templateNodeForCommand != null)
        {
          ButtonBarButton menuTemplateNode = this.CreateButtonBarButtonFromMenuTemplateNode(templateNodeForCommand);
          foreach (ButtonBar selectedButtonBar in this._selectedButtonBars)
            selectedButtonBar.Buttons.Add(menuTemplateNode);
          foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
            selectedButtonBarButton.Buttons.Add(menuTemplateNode);
        }
      }
    }
  }

  private void Delete()
  {
    foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
      selectedButtonBarButton.Parent.Buttons.Remove(selectedButtonBarButton);
    foreach (ButtonBar selectedButtonBar in this._selectedButtonBars)
      this._buttonBars.Remove(selectedButtonBar);
  }

  private void MoveTop()
  {
    foreach (ButtonBarButton buttonBarButton in ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Reverse<ButtonBarButton>())
      buttonBarButton.Parent.Buttons.MoveTop(buttonBarButton);
    foreach (ButtonBar buttonBar in ((IEnumerable<ButtonBar>) this._selectedButtonBars).Reverse<ButtonBar>())
      this._buttonBars.MoveTop(buttonBar);
  }

  private void MoveUp()
  {
    foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
      selectedButtonBarButton.Parent.Buttons.MoveUp(selectedButtonBarButton);
    foreach (ButtonBar selectedButtonBar in this._selectedButtonBars)
      this._buttonBars.MoveUp(selectedButtonBar);
  }

  private void MoveDown()
  {
    foreach (ButtonBarButton buttonBarButton in ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Reverse<ButtonBarButton>())
      buttonBarButton.Parent.Buttons.MoveDown(buttonBarButton);
    foreach (ButtonBar buttonBar in ((IEnumerable<ButtonBar>) this._selectedButtonBars).Reverse<ButtonBar>())
      this._buttonBars.MoveDown(buttonBar);
  }

  private void MoveBottom()
  {
    foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
      selectedButtonBarButton.Parent.Buttons.MoveBottom(selectedButtonBarButton);
    foreach (ButtonBar selectedButtonBar in this._selectedButtonBars)
      this._buttonBars.MoveBottom(selectedButtonBar);
  }

  private void BeginGroup()
  {
    foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
      selectedButtonBarButton.BeginGroup = !selectedButtonBarButton.BeginGroup;
  }

  private void ShiftLeft()
  {
    foreach (ButtonBarButton buttonBarButton in ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Reverse<ButtonBarButton>())
    {
      if (buttonBarButton.Parent is ButtonBarButton)
      {
        ButtonBarButton parent = (ButtonBarButton) buttonBarButton.Parent;
        int index = parent.Parent.Buttons.IndexOf(parent);
        bool listChangedEvents = parent.Buttons.RaiseListChangedEvents;
        parent.Buttons.RaiseListChangedEvents = false;
        try
        {
          parent.Buttons.Remove(buttonBarButton);
          parent.Parent.Buttons.Insert(index, buttonBarButton);
        }
        finally
        {
          parent.Buttons.RaiseListChangedEvents = listChangedEvents;
          parent.Buttons.ResetBindings();
        }
      }
    }
  }

  private void ShiftRight()
  {
    foreach (ButtonBarButton selectedButtonBarButton in this._selectedButtonBarButtons)
    {
      int num = selectedButtonBarButton.Parent.Buttons.IndexOf(selectedButtonBarButton);
      if (num != 0)
      {
        IButtonBarButtonCollectionOwner parent = selectedButtonBarButton.Parent;
        bool listChangedEvents = parent.Buttons.RaiseListChangedEvents;
        parent.Buttons.RaiseListChangedEvents = false;
        try
        {
          parent.Buttons.Remove(selectedButtonBarButton);
          parent.Buttons[num - 1].Buttons.Add(selectedButtonBarButton);
        }
        finally
        {
          parent.Buttons.RaiseListChangedEvents = listChangedEvents;
          parent.Buttons.ResetBindings();
        }
      }
    }
  }

  private void UpdateView()
  {
    this.SetSelectedButtonBars();
    this.SetSelectedButtonBarButtons();
    this._newButtonBarToolStripButton.Enabled = this.CanNewButtonBar();
    this._addButtonsToolStripButton.Enabled = this.CanAddButton();
    this._deleteToolStripButton.Enabled = this.CanDelete();
    this._moveTopToolStripButton.Enabled = this.CanMoveTop();
    this._moveUpToolStripButton.Enabled = this.CanMoveUp();
    this._moveDownToolStripButton.Enabled = this.CanMoveDown();
    this._moveBottomToolStripButton.Enabled = this.CanMoveBottom();
    this._beginGroupToolStripButton.Enabled = this.CanBeginGroup();
    this._beginGroupToolStripButton.Checked = this.IsAllSelectedButtonBarButtonsBeginGroup();
    this._shiftLeftToolStripButton.Enabled = this.CanShiftLeft();
    this._shiftRightToolStripButton.Enabled = this.CanShiftRight();
    this._newButtonBarToolStripMenuItem.Enabled = this.CanNewButtonBar();
    this._addButtonsToolStripMenuItem.Enabled = this.CanAddButton();
    this._deleteToolStripMenuItem.Enabled = this.CanDelete();
    this._moveTopToolStripMenuItem.Enabled = this.CanMoveTop();
    this._moveUpToolStripMenuItem.Enabled = this.CanMoveUp();
    this._moveDownToolStripMenuItem.Enabled = this.CanMoveDown();
    this._moveBottomToolStripMenuItem.Enabled = this.CanMoveBottom();
    this._beginGroupToolStripMenuItem.Enabled = this.CanBeginGroup();
    this._beginGroupToolStripMenuItem.Checked = this.IsAllSelectedButtonBarButtonsBeginGroup();
    this._shiftLeftToolStripMenuItem.Enabled = this.CanShiftLeft();
    this._shiftRightToolStripMenuItem.Enabled = this.CanShiftRight();
  }

  private bool CanNewButtonBar() => !this.ReadOnly;

  private bool CanAddButton()
  {
    if (this.ReadOnly)
      return false;
    return this._selectedButtonBars.Length != 0 || this._selectedButtonBarButtons.Length != 0;
  }

  private bool CanDelete()
  {
    if (this.ReadOnly)
      return false;
    return this._selectedButtonBars.Length != 0 || this._selectedButtonBarButtons.Length != 0;
  }

  private bool CanMoveTop()
  {
    if (this.ReadOnly || this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length == 0)
      return false;
    return ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent.Buttons.CanMoveTop(o))) || ((IEnumerable<ButtonBar>) this._selectedButtonBars).Any<ButtonBar>((Func<ButtonBar, bool>) (o => this._buttonBars.CanMoveTop(o)));
  }

  private bool CanMoveUp()
  {
    if (this.ReadOnly || this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length == 0)
      return false;
    return ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent.Buttons.CanMoveUp(o))) || ((IEnumerable<ButtonBar>) this._selectedButtonBars).Any<ButtonBar>((Func<ButtonBar, bool>) (o => this._buttonBars.CanMoveUp(o)));
  }

  private bool CanMoveDown()
  {
    if (this.ReadOnly || this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length == 0)
      return false;
    return ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent.Buttons.CanMoveDown(o))) || ((IEnumerable<ButtonBar>) this._selectedButtonBars).Any<ButtonBar>((Func<ButtonBar, bool>) (o => this._buttonBars.CanMoveDown(o)));
  }

  private bool CanMoveBottom()
  {
    if (this.ReadOnly || this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length == 0)
      return false;
    return ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent.Buttons.CanMoveBottom(o))) || ((IEnumerable<ButtonBar>) this._selectedButtonBars).Any<ButtonBar>((Func<ButtonBar, bool>) (o => this._buttonBars.CanMoveBottom(o)));
  }

  private bool CanBeginGroup()
  {
    return !this.ReadOnly && this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length != 0;
  }

  private bool CanShiftLeft()
  {
    return !this.ReadOnly && this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length != 0 && ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent is ButtonBarButton));
  }

  private bool CanShiftRight()
  {
    return !this.ReadOnly && this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length != 0 && ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).Any<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.Parent.Buttons.IndexOf(o) != 0));
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  private bool IsAllSelectedButtonBarButtonsBeginGroup()
  {
    return this._selectedButtonBars.Length == 0 && this._selectedButtonBarButtons.Length != 0 && ((IEnumerable<ButtonBarButton>) this._selectedButtonBarButtons).All<ButtonBarButton>((Func<ButtonBarButton, bool>) (o => o.BeginGroup));
  }

  private void SetSelectedButtonBars()
  {
    this._selectedButtonBars = this._tree.SelectedItems.Cast<object>().Where<object>((Func<object, bool>) (o => o is ButtonBar)).Select<object, ButtonBar>((Func<object, ButtonBar>) (o => (ButtonBar) o)).Distinct<ButtonBar>().OrderBy<ButtonBar, int>((Func<ButtonBar, int>) (o => this._buttonBars.IndexOf(o))).ToArray<ButtonBar>();
  }

  private void SetSelectedButtonBarButtons()
  {
    this._selectedButtonBarButtons = this._tree.SelectedItems.Cast<object>().Where<object>((Func<object, bool>) (o => o is ButtonBarButton && ((ButtonBarButton) o).Parent != null)).Select<object, ButtonBarButton>((Func<object, ButtonBarButton>) (o => (ButtonBarButton) o)).OrderBy<ButtonBarButton, int>((Func<ButtonBarButton, int>) (o => o.Parent.Buttons.IndexOf(o))).ToArray<ButtonBarButton>();
  }

  private ButtonBarButton CreateButtonBarButtonFromMenuTemplateNode(
    MenuTemplateNode menuTemplateNode)
  {
    ButtonBarButton menuTemplateNode1 = new ButtonBarButton(menuTemplateNode.Name);
    menuTemplateNode1.Text = menuTemplateNode.Text;
    menuTemplateNode1.ToolTipText = menuTemplateNode.Text;
    foreach (MenuTemplateNode node in menuTemplateNode.Nodes)
      menuTemplateNode1.Buttons.Add(this.CreateButtonBarButtonFromMenuTemplateNode(node));
    return menuTemplateNode1;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ButtonBarsEditorControl));
    this._commandColumn = new Column();
    this._textColumn = new Column();
    this._displayTypeColumn = new Column();
    this._contextMenuStrip = new ContextMenuStrip(this.components);
    this._newButtonBarToolStripMenuItem = new ToolStripMenuItem();
    this._addButtonsToolStripMenuItem = new ToolStripMenuItem();
    this._deleteToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator2 = new ToolStripSeparator();
    this._moveTopToolStripMenuItem = new ToolStripMenuItem();
    this._moveUpToolStripMenuItem = new ToolStripMenuItem();
    this._moveDownToolStripMenuItem = new ToolStripMenuItem();
    this._moveBottomToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripSeparator4 = new ToolStripSeparator();
    this._beginGroupToolStripMenuItem = new ToolStripMenuItem();
    this._shiftLeftToolStripMenuItem = new ToolStripMenuItem();
    this._shiftRightToolStripMenuItem = new ToolStripMenuItem();
    this.toolStrip1 = new ToolStrip();
    this._newButtonBarToolStripButton = new ToolStripButton();
    this._addButtonsToolStripButton = new ToolStripButton();
    this._deleteToolStripButton = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this._moveTopToolStripButton = new ToolStripButton();
    this._moveUpToolStripButton = new ToolStripButton();
    this._moveDownToolStripButton = new ToolStripButton();
    this._moveBottomToolStripButton = new ToolStripButton();
    this.toolStripSeparator3 = new ToolStripSeparator();
    this._beginGroupToolStripButton = new ToolStripButton();
    this._shiftLeftToolStripButton = new ToolStripButton();
    this._shiftRightToolStripButton = new ToolStripButton();
    this.panel2 = new Panel();
    this._tree = new Intermech.Search.UI.VirtualTree.VirtualTree();
    this._toopTipTextColumn = new Column();
    this._contextMenuStrip.SuspendLayout();
    this.toolStrip1.SuspendLayout();
    this.panel2.SuspendLayout();
    this._tree.BeginInit();
    this.SuspendLayout();
    this._commandColumn.Caption = "Панель/Команда";
    this._commandColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._commandColumn.Name = "_commandColumn";
    this._commandColumn.Width = 200;
    this._textColumn.Caption = "Текст";
    this._textColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._textColumn.Name = "_textColumn";
    this._textColumn.Width = 250;
    this._displayTypeColumn.Caption = "Тип отображаемой информации";
    this._displayTypeColumn.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this._displayTypeColumn.Name = "_displayTypeColumn";
    this._displayTypeColumn.Width = 200;
    this._contextMenuStrip.Items.AddRange(new ToolStripItem[12]
    {
      (ToolStripItem) this._newButtonBarToolStripMenuItem,
      (ToolStripItem) this._addButtonsToolStripMenuItem,
      (ToolStripItem) this._deleteToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator2,
      (ToolStripItem) this._moveTopToolStripMenuItem,
      (ToolStripItem) this._moveUpToolStripMenuItem,
      (ToolStripItem) this._moveDownToolStripMenuItem,
      (ToolStripItem) this._moveBottomToolStripMenuItem,
      (ToolStripItem) this.toolStripSeparator4,
      (ToolStripItem) this._beginGroupToolStripMenuItem,
      (ToolStripItem) this._shiftLeftToolStripMenuItem,
      (ToolStripItem) this._shiftRightToolStripMenuItem
    });
    this._contextMenuStrip.Name = "_contextMenuStrip";
    this._contextMenuStrip.Size = new Size(278, 236);
    this._newButtonBarToolStripMenuItem.Image = (Image) Resources.AddFile;
    this._newButtonBarToolStripMenuItem.ImageTransparentColor = Color.Magenta;
    this._newButtonBarToolStripMenuItem.Name = "_newButtonBarToolStripMenuItem";
    this._newButtonBarToolStripMenuItem.Size = new Size(277, 22);
    this._newButtonBarToolStripMenuItem.Text = "Новая кнопочная панель";
    this._newButtonBarToolStripMenuItem.ToolTipText = "Новая кнопочная панель";
    this._newButtonBarToolStripMenuItem.Click += new EventHandler(this.NewButtonBarToolStripMenuItem_Click);
    this._addButtonsToolStripMenuItem.Image = (Image) Resources.AddStandart;
    this._addButtonsToolStripMenuItem.Name = "_addButtonsToolStripMenuItem";
    this._addButtonsToolStripMenuItem.Size = new Size(277, 22);
    this._addButtonsToolStripMenuItem.Text = "Добавить кнопки";
    this._addButtonsToolStripMenuItem.Click += new EventHandler(this.AddButtonsToolStripMenuItem_Click);
    this._deleteToolStripMenuItem.Image = (Image) Resources.DeleteStandart;
    this._deleteToolStripMenuItem.Name = "_deleteToolStripMenuItem";
    this._deleteToolStripMenuItem.Size = new Size(277, 22);
    this._deleteToolStripMenuItem.Text = "Удалить";
    this._deleteToolStripMenuItem.ToolTipText = "Удалить";
    this._deleteToolStripMenuItem.Click += new EventHandler(this.DeleteToolStripMenuItem_Click);
    this.toolStripSeparator2.Name = "toolStripSeparator2";
    this.toolStripSeparator2.Size = new Size(274, 6);
    this._moveTopToolStripMenuItem.Image = (Image) Resources.arrow_top_blue;
    this._moveTopToolStripMenuItem.Name = "_moveTopToolStripMenuItem";
    this._moveTopToolStripMenuItem.Size = new Size(277, 22);
    this._moveTopToolStripMenuItem.Text = "Переместить вверх списка";
    this._moveTopToolStripMenuItem.Click += new EventHandler(this.MoveTopToolStripMenuItem_Click);
    this._moveUpToolStripMenuItem.Image = (Image) Resources.arrow_up_blue;
    this._moveUpToolStripMenuItem.Name = "_moveUpToolStripMenuItem";
    this._moveUpToolStripMenuItem.Size = new Size(277, 22);
    this._moveUpToolStripMenuItem.Text = "Переместить вверх на одну позицию";
    this._moveUpToolStripMenuItem.Click += new EventHandler(this.MoveUpToolStripMenuItem_Click);
    this._moveDownToolStripMenuItem.Image = (Image) Resources.arrow_down_blue;
    this._moveDownToolStripMenuItem.Name = "_moveDownToolStripMenuItem";
    this._moveDownToolStripMenuItem.Size = new Size(277, 22);
    this._moveDownToolStripMenuItem.Text = "Переместить вниз на одну позицию";
    this._moveDownToolStripMenuItem.Click += new EventHandler(this.MoveDownToolStripMenuItem_Click);
    this._moveBottomToolStripMenuItem.Image = (Image) Resources.arrow_bottom_blue;
    this._moveBottomToolStripMenuItem.Name = "_moveBottomToolStripMenuItem";
    this._moveBottomToolStripMenuItem.Size = new Size(277, 22);
    this._moveBottomToolStripMenuItem.Text = "Переместить вниз списка";
    this._moveBottomToolStripMenuItem.Click += new EventHandler(this.MoveBottomToolStripMenuItem_Click);
    this.toolStripSeparator4.Name = "toolStripSeparator4";
    this.toolStripSeparator4.Size = new Size(274, 6);
    this._beginGroupToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_beginGroupToolStripMenuItem.Image");
    this._beginGroupToolStripMenuItem.Name = "_beginGroupToolStripMenuItem";
    this._beginGroupToolStripMenuItem.Size = new Size(277, 22);
    this._beginGroupToolStripMenuItem.Text = "Начать группу";
    this._beginGroupToolStripMenuItem.Click += new EventHandler(this.BeginGroupToolStripMenuItem_Click);
    this._shiftLeftToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_shiftLeftToolStripMenuItem.Image");
    this._shiftLeftToolStripMenuItem.Name = "_shiftLeftToolStripMenuItem";
    this._shiftLeftToolStripMenuItem.Size = new Size(277, 22);
    this._shiftLeftToolStripMenuItem.Text = "Сместить влево";
    this._shiftLeftToolStripMenuItem.Click += new EventHandler(this.ShiftLeftToolStripMenuItem_Click);
    this._shiftRightToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("_shiftRightToolStripMenuItem.Image");
    this._shiftRightToolStripMenuItem.Name = "_shiftRightToolStripMenuItem";
    this._shiftRightToolStripMenuItem.Size = new Size(277, 22);
    this._shiftRightToolStripMenuItem.Text = "Сместить вправо";
    this._shiftRightToolStripMenuItem.Click += new EventHandler(this.ShiftRightToolStripMenuItem_Click);
    this.toolStrip1.Items.AddRange(new ToolStripItem[12]
    {
      (ToolStripItem) this._newButtonBarToolStripButton,
      (ToolStripItem) this._addButtonsToolStripButton,
      (ToolStripItem) this._deleteToolStripButton,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this._moveTopToolStripButton,
      (ToolStripItem) this._moveUpToolStripButton,
      (ToolStripItem) this._moveDownToolStripButton,
      (ToolStripItem) this._moveBottomToolStripButton,
      (ToolStripItem) this.toolStripSeparator3,
      (ToolStripItem) this._beginGroupToolStripButton,
      (ToolStripItem) this._shiftLeftToolStripButton,
      (ToolStripItem) this._shiftRightToolStripButton
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.Size = new Size(758, 25);
    this.toolStrip1.TabIndex = 1;
    this.toolStrip1.Text = "toolStrip1";
    this._newButtonBarToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._newButtonBarToolStripButton.Image = (Image) Resources.AddFile;
    this._newButtonBarToolStripButton.ImageTransparentColor = Color.Magenta;
    this._newButtonBarToolStripButton.Name = "_newButtonBarToolStripButton";
    this._newButtonBarToolStripButton.Size = new Size(23, 22);
    this._newButtonBarToolStripButton.Text = "Новая кнопочная панель";
    this._newButtonBarToolStripButton.Click += new EventHandler(this.NewButtonBarToolStripButton_Click);
    this._addButtonsToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._addButtonsToolStripButton.Image = (Image) Resources.AddStandart;
    this._addButtonsToolStripButton.ImageTransparentColor = Color.Magenta;
    this._addButtonsToolStripButton.Name = "_addButtonsToolStripButton";
    this._addButtonsToolStripButton.Size = new Size(23, 22);
    this._addButtonsToolStripButton.Text = "Добавить кнопки";
    this._addButtonsToolStripButton.Click += new EventHandler(this.AddButtonsToolStripButton_Click);
    this._deleteToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._deleteToolStripButton.Image = (Image) Resources.DeleteStandart;
    this._deleteToolStripButton.ImageTransparentColor = Color.Magenta;
    this._deleteToolStripButton.Name = "_deleteToolStripButton";
    this._deleteToolStripButton.Size = new Size(23, 22);
    this._deleteToolStripButton.Text = "Удалить";
    this._deleteToolStripButton.Click += new EventHandler(this.DeleteToolStripButton_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this._moveTopToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveTopToolStripButton.Image = (Image) Resources.arrow_top_blue;
    this._moveTopToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveTopToolStripButton.Name = "_moveTopToolStripButton";
    this._moveTopToolStripButton.Size = new Size(23, 22);
    this._moveTopToolStripButton.Text = "Переместить вверх списка";
    this._moveTopToolStripButton.Click += new EventHandler(this.MoveTopToolStripButton_Click);
    this._moveUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveUpToolStripButton.Image = (Image) Resources.arrow_up_blue;
    this._moveUpToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveUpToolStripButton.Name = "_moveUpToolStripButton";
    this._moveUpToolStripButton.Size = new Size(23, 22);
    this._moveUpToolStripButton.Text = "Переместить вверх на одну позицию";
    this._moveUpToolStripButton.Click += new EventHandler(this.MoveUpToolStripButton_Click);
    this._moveDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveDownToolStripButton.Image = (Image) Resources.arrow_down_blue;
    this._moveDownToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveDownToolStripButton.Name = "_moveDownToolStripButton";
    this._moveDownToolStripButton.Size = new Size(23, 22);
    this._moveDownToolStripButton.Text = "Переметить винз на одну позицию";
    this._moveDownToolStripButton.Click += new EventHandler(this.MoveDownToolStripButton_Click);
    this._moveBottomToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._moveBottomToolStripButton.Image = (Image) Resources.arrow_bottom_blue;
    this._moveBottomToolStripButton.ImageTransparentColor = Color.Magenta;
    this._moveBottomToolStripButton.Name = "_moveBottomToolStripButton";
    this._moveBottomToolStripButton.Size = new Size(23, 22);
    this._moveBottomToolStripButton.Text = "Переместить вниз списка";
    this._moveBottomToolStripButton.Click += new EventHandler(this.MoveBottomToolStripButton_Click);
    this.toolStripSeparator3.Name = "toolStripSeparator3";
    this.toolStripSeparator3.Size = new Size(6, 25);
    this._beginGroupToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._beginGroupToolStripButton.Image = (Image) componentResourceManager.GetObject("_beginGroupToolStripButton.Image");
    this._beginGroupToolStripButton.ImageTransparentColor = Color.Magenta;
    this._beginGroupToolStripButton.Name = "_beginGroupToolStripButton";
    this._beginGroupToolStripButton.Size = new Size(23, 22);
    this._beginGroupToolStripButton.Text = "Начало группы";
    this._beginGroupToolStripButton.Click += new EventHandler(this.BeginGroupToolStripButton_Click);
    this._shiftLeftToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._shiftLeftToolStripButton.Image = (Image) componentResourceManager.GetObject("_shiftLeftToolStripButton.Image");
    this._shiftLeftToolStripButton.ImageTransparentColor = Color.Magenta;
    this._shiftLeftToolStripButton.Name = "_shiftLeftToolStripButton";
    this._shiftLeftToolStripButton.Size = new Size(23, 22);
    this._shiftLeftToolStripButton.Text = "Сместить влево";
    this._shiftLeftToolStripButton.Click += new EventHandler(this.ShiftLeftToolStripButton_Click);
    this._shiftRightToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this._shiftRightToolStripButton.Image = (Image) componentResourceManager.GetObject("_shiftRightToolStripButton.Image");
    this._shiftRightToolStripButton.ImageTransparentColor = Color.Magenta;
    this._shiftRightToolStripButton.Name = "_shiftRightToolStripButton";
    this._shiftRightToolStripButton.Size = new Size(23, 22);
    this._shiftRightToolStripButton.Text = "Сместить вправо";
    this._shiftRightToolStripButton.Click += new EventHandler(this.ShiftRightToolStripButton_Click);
    this.panel2.Controls.Add((Control) this._tree);
    this.panel2.Dock = DockStyle.Fill;
    this.panel2.Location = new Point(0, 25);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(758, 305);
    this.panel2.TabIndex = 3;
    this._tree.AllowDrop = true;
    this._tree.Columns.Add(this._commandColumn);
    this._tree.Columns.Add(this._textColumn);
    this._tree.Columns.Add(this._toopTipTextColumn);
    this._tree.Columns.Add(this._displayTypeColumn);
    this._tree.ContextMenuStrip = this._contextMenuStrip;
    this._tree.Dock = DockStyle.Fill;
    this._tree.IconWidth = 0;
    this._tree.ImageList = (ImageList) null;
    this._tree.LineStyle = LineStyle.Dot;
    this._tree.Location = new Point(0, 0);
    this._tree.MainColumn = this._commandColumn;
    this._tree.Name = "_tree";
    this._tree.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this._tree.RowStyle.BorderWidth = 1;
    this._tree.ShowRootRow = false;
    this._tree.Size = new Size(758, 305);
    this._tree.TabIndex = 0;
    this._tree.SelectionChanged += new EventHandler(this.Tree_SelectionChanged);
    this._toopTipTextColumn.Caption = "Подсказка";
    this._toopTipTextColumn.HeaderStyle.VertAlignment = StringAlignment.Near;
    this._toopTipTextColumn.Name = "_toopTipTextColumn";
    this._toopTipTextColumn.Width = 250;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (ButtonBarsEditorControl);
    this.Size = new Size(758, 330);
    this._contextMenuStrip.ResumeLayout(false);
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.panel2.ResumeLayout(false);
    this._tree.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public sealed class ButtonBarButtonRowBinding : ObjectRowBinding
  {
    private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
    private LazyService<INamedImageList> _namedImageList = new LazyService<INamedImageList>();
    private CellEditor _textBoxCellEditor = new CellEditor((Control) new TextBox());
    private CellEditor _comboBoxCellEditor = new CellEditor((Control) new ComboBox());

    public ButtonBarButtonRowBinding()
      : base(typeof (ButtonBarButton))
    {
      this.ChildProperty = "Buttons";
      this._comboBoxCellEditor.InitializeControl += new CellEditorInitializeHandler(this.ComboBoxCellEditor_InitializeControl);
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      if (row == null)
        throw new ArgumentNullException(nameof (row));
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      if (cellData == null)
        throw new ArgumentNullException(nameof (cellData));
      base.GetCellData(row, column, cellData);
      ButtonBarButton buttonBarButton = (ButtonBarButton) row.Item;
      if (column.DataField == "Command")
      {
        MenuTemplateNode templateNodeForCommand = ContextMenuHelper.GetContextMenuTemplateNodeForCommand(buttonBarButton.CommandName);
        cellData.Value = templateNodeForCommand != null ? (object) templateNodeForCommand.Text : (object) buttonBarButton.CommandName;
      }
      else if (column.DataField == "Text")
      {
        cellData.Editor = this._textBoxCellEditor;
        cellData.Value = (object) buttonBarButton.Text;
      }
      else if (column.DataField == "ToolTipText")
      {
        cellData.Editor = this._textBoxCellEditor;
        cellData.Value = (object) buttonBarButton.ToolTipText;
      }
      else
      {
        if (!(column.DataField == "Type"))
          return;
        cellData.Editor = this._comboBoxCellEditor;
        cellData.Value = (object) buttonBarButton.DisplayType.GetDescription<ButtonBarButtonDisplayType>();
      }
    }

    public override void GetRowData(Row row, RowData rowData)
    {
      if (row == null)
        throw new ArgumentNullException(nameof (row));
      if (rowData == null)
        throw new ArgumentNullException(nameof (rowData));
      base.GetRowData(row, rowData);
      MenuTemplateNode templateNodeForCommand = ContextMenuHelper.GetContextMenuTemplateNodeForCommand(((ButtonBarButton) row.Item).CommandName);
      if (templateNodeForCommand == null)
        return;
      rowData.ImageSize = 32 /*0x20*/;
      if (templateNodeForCommand.ImageListSource == ImageListSource.CategoryImageList)
        rowData.ImageList = this._categoryTypeIconService.Value.ImageList;
      else if (templateNodeForCommand.ImageListSource == ImageListSource.NamedImageList)
        rowData.ImageList = this._namedImageList.Value.ImageList;
      rowData.ImageIndex = templateNodeForCommand.ImageIndex;
    }

    public override bool SetCellValue(Row row, Column column, object oldValue, object newValue)
    {
      if (row == null)
        throw new ArgumentNullException(nameof (row));
      if (column == null)
        throw new ArgumentNullException(nameof (column));
      ButtonBarButton buttonBarButton = (ButtonBarButton) row.Item;
      string str = newValue as string;
      if (column.DataField == "Text")
        buttonBarButton.Text = str;
      else if (column.DataField == "ToolTipText")
        buttonBarButton.ToolTipText = str;
      else if (column.DataField == "Type")
      {
        if (str == ButtonBarButtonDisplayType.Image.GetDescription<ButtonBarButtonDisplayType>())
          buttonBarButton.DisplayType = ButtonBarButtonDisplayType.Image;
        else if (str == ButtonBarButtonDisplayType.ImageAndText.GetDescription<ButtonBarButtonDisplayType>())
          buttonBarButton.DisplayType = ButtonBarButtonDisplayType.ImageAndText;
        else if (str == ButtonBarButtonDisplayType.Text.GetDescription<ButtonBarButtonDisplayType>())
          buttonBarButton.DisplayType = ButtonBarButtonDisplayType.Text;
      }
      return true;
    }

    private void ComboBoxCellEditor_InitializeControl(
      object sender,
      CellEditorInitializeEventArgs e)
    {
      ComboBox control = (ComboBox) e.Control;
      control.BeginUpdate();
      control.Items.Clear();
      control.Items.Add((object) ButtonBarButtonDisplayType.Image.GetDescription<ButtonBarButtonDisplayType>());
      control.Items.Add((object) ButtonBarButtonDisplayType.ImageAndText.GetDescription<ButtonBarButtonDisplayType>());
      control.Items.Add((object) ButtonBarButtonDisplayType.Text.GetDescription<ButtonBarButtonDisplayType>());
      control.EndUpdate();
    }
  }
}
