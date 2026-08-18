
// Type: Intermech.Navigator.ContextMenuEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Редактор коллекции настраиваемых команд контекстных меню
/// </summary>
public sealed class ContextMenuEditor : UserControl, ISupportInitialize
{
  private ContextMenuSearchForm _contextMenuSearchForm;
  private AdjustableMenuCommands _adjustableMenuCommands;
  private AdjustableMenuCommands _adjustableMenuCommandsBackup;
  private List<AdjustableMenuCommand> _selectedAdjustableMenuCommands = new List<AdjustableMenuCommand>();
  /// <summary>Для быстрого поиска узлов команд</summary>
  private Dictionary<AdjustableMenuCommand, TreeListNode> _treeListNodeByAdjustableMenuCommandDictionary = new Dictionary<AdjustableMenuCommand, TreeListNode>();
  private LazyService<ICurrentUserAndRole> _currentUserAndRole = new LazyService<ICurrentUserAndRole>();
  private LazyService<ICategoryTypeIconService> _categoryTypeIconService = new LazyService<ICategoryTypeIconService>();
  private LazyService<INamedImageList> _namedImageList = new LazyService<INamedImageList>();
  private LazyService<INavGraphicsCache> _navGraphicsCache = new LazyService<INavGraphicsCache>();
  /// <summary>Есть ли изменения в контексте</summary>
  private bool _isChanged;
  /// <summary>
  /// Выполняется ли работа внутри обработчиков событий, меняющих структуру дерева
  /// </summary>
  private bool _inEditor;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imagesMenus;
  private Intermech.Bars.ToolBar menuEditingContextsBar;
  private ImageList imagesState;
  private ButtonItem _removeGroupButtonItem;
  private ButtonItem _addGroupButtonItem;
  private ButtonItem _upButtonItem;
  private ButtonItem _downButtonItem;
  private ButtonItem _findButtonItem;
  private ButtonItem _warningButtonItem;
  private LabelItem _warningLabel;
  private TreeList _treeList;
  private ButtonItem _topButtonItem;
  private ButtonItem _bottomButtonItem;
  private TreeListColumn columnCommand;
  private TreeListColumn columnHint;

  public ContextMenuEditor() => this.InitializeComponent();

  public event EventHandler Changed;

  /// <summary>Коллекция настраиваемых команд контекстных меню</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public AdjustableMenuCommands AdjustableMenuCommands
  {
    get
    {
      if (this._adjustableMenuCommands == null)
        this._adjustableMenuCommands = new AdjustableMenuCommands();
      if (this._adjustableMenuCommandsBackup == null)
        this._adjustableMenuCommandsBackup = new AdjustableMenuCommands();
      return this._adjustableMenuCommands;
    }
    set
    {
      if (this._adjustableMenuCommands == null)
        this._adjustableMenuCommands = new AdjustableMenuCommands();
      this._adjustableMenuCommands.Assign(value);
      this._adjustableMenuCommands.Sort();
      if (this._adjustableMenuCommandsBackup == null)
        this._adjustableMenuCommandsBackup = new AdjustableMenuCommands();
      this._adjustableMenuCommandsBackup.Assign(this._adjustableMenuCommands);
      this._isChanged = false;
      this.CreateMenusTree();
    }
  }

  /// <summary>Есть ли изменения в редакторе</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public bool IsChanged
  {
    get => this._isChanged;
    set
    {
      this._isChanged = value;
      this.OnChanged();
    }
  }

  /// <summary>Зафиксировать изменения в редакторе</summary>
  public void Fix()
  {
    if (this._adjustableMenuCommandsBackup == null)
      this._adjustableMenuCommandsBackup = new AdjustableMenuCommands();
    this._adjustableMenuCommandsBackup.Assign(this._adjustableMenuCommands);
    this._isChanged = false;
    this.OnChanged();
  }

  /// <summary>
  /// Отменить изменения в редакторе
  /// (в базу данных при этом ничего не вносится)
  /// </summary>
  public void Undo()
  {
    if (this._adjustableMenuCommandsBackup == null)
      this._adjustableMenuCommandsBackup = new AdjustableMenuCommands();
    this._adjustableMenuCommands.Assign(this._adjustableMenuCommandsBackup);
    this._isChanged = false;
    this.OnChanged();
  }

  void ISupportInitialize.BeginInit()
  {
  }

  void ISupportInitialize.EndInit()
  {
    if (this.DesignMode)
      return;
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this._treeList.SelectImageList = this._namedImageList.Value.ImageList;
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.menuEditingContextsBar.Renderer = (sender as BarManager).Renderer;
  }

  private void TopButtonItem_Click(object sender, EventArgs e) => this.MoveTop();

  private void UpButtonItem_Click(object sender, EventArgs e) => this.Up();

  private void DownButtonItem_Click(object sender, EventArgs e) => this.Down();

  private void BottomButtonItem_Click(object sender, EventArgs e) => this.MoveBottom();

  private void AddGroupButtonItem_Click(object sender, EventArgs e) => this.AddGroup();

  private void RemoveGroupButtonItem_Click(object sender, EventArgs e) => this.RemoveGroup();

  private void FindButtonItem_Click(object sender, EventArgs e) => this.Find();

  /// <summary>Назначить стили ячейкам дерева</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void TreeList_GetCustomNodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
  {
    if (e.Node == null || e.Column == null)
      return;
    if (e.Node.Tag == null)
    {
      if (e.Node.TreeList.Selection.IndexOf(e.Node) >= 0)
        e.Style = e.Node.TreeList.Styles["RootCellSelected"];
      else
        e.Style = e.Node.TreeList.Styles["RootCell"];
    }
    else
    {
      if (!(e.Node.Tag is AdjustableMenuCommand tag) || e.Column != this.columnCommand)
        return;
      AdjustableMenuCommand prevCommand = tag.Parent.FindPrevCommand(tag);
      if (prevCommand != null && prevCommand.Group == tag.Group)
        return;
      if (e.Node.TreeList.Selection.IndexOf(e.Node) >= 0)
        e.Style = e.Node.TreeList.Styles["GroupCellSelected"];
      else
        e.Style = e.Node.TreeList.Styles["GroupCell"];
    }
  }

  /// <summary>Управление видимостью команд контекстного меню</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void TreeList_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (this._inEditor || e == null || e.Node == null || !this._currentUserAndRole.Value.BlockedMenus)
      return;
    e.NewValue = e.OldValue;
  }

  /// <summary>Управление видимостью команд контекстного меню</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void TreeList_CheckStateChanged(object sender, NodeEventArgs e)
  {
    if (this._inEditor || this._currentUserAndRole.Value.BlockedMenus || e == null || e.Node == null || !(e.Node.Tag is AdjustableMenuCommand))
      return;
    (e.Node.Tag as AdjustableMenuCommand).Visible = e.Node.CheckState == CheckState.Checked || e.Node.CheckState == CheckState.Indeterminate;
    this.IsChanged = true;
  }

  private void TreeList_SelectionChanged(object sender, EventArgs e) => this.UpdateControl();

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  private void UpdateControl()
  {
    this.SetSelectedAdjustableMenuCommands();
    this.SetTopButtonItemEnabled();
    this.SetUpButtonItemEnabled();
    this.SetDownButtonItemEnabled();
    this.SetBottomButtonItemEnabled();
    this.SetAddGroupButtonItemEnabled();
    this.SetRemoveGroupButtonItemEnabled();
  }

  private void MoveTop()
  {
    foreach (TreeListNode node in ((IEnumerable<TreeListNode>) this.GetSelectedTreeListNodes()).Reverse<TreeListNode>())
    {
      AdjustableMenuCommand tag = (AdjustableMenuCommand) node.Tag;
      tag.Parent.MoveTop(tag);
      this._treeList.SetNodeIndex(node, 0);
    }
    this.MakeFirstSelectedNodeVisible();
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void MakeFirstSelectedNodeVisible()
  {
    TreeListNode[] selectedTreeListNodes = this.GetSelectedTreeListNodes();
    if (selectedTreeListNodes.Length == 0)
      return;
    this._treeList.MakeNodeVisible(((IEnumerable<TreeListNode>) selectedTreeListNodes).First<TreeListNode>());
  }

  private void Up()
  {
    foreach (TreeListNode selectedTreeListNode in this.GetSelectedTreeListNodes())
    {
      AdjustableMenuCommand tag = (AdjustableMenuCommand) selectedTreeListNode.Tag;
      if (tag.Parent.MoveUp(tag, false))
        this._treeList.SetNodeIndex(selectedTreeListNode, selectedTreeListNode.ParentNode.Nodes.IndexOf(selectedTreeListNode) - 1);
    }
    this.MakeFirstSelectedNodeVisible();
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void Down()
  {
    foreach (TreeListNode node in ((IEnumerable<TreeListNode>) this.GetSelectedTreeListNodes()).Reverse<TreeListNode>())
    {
      AdjustableMenuCommand tag = (AdjustableMenuCommand) node.Tag;
      if (tag.Parent.MoveDown(tag, false))
        this._treeList.SetNodeIndex(node, node.ParentNode.Nodes.IndexOf(node) + 1);
    }
    this.MakeLastSelectedNodeVisible();
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void MakeLastSelectedNodeVisible()
  {
    TreeListNode[] selectedTreeListNodes = this.GetSelectedTreeListNodes();
    if (selectedTreeListNodes.Length == 0)
      return;
    this._treeList.MakeNodeVisible(((IEnumerable<TreeListNode>) selectedTreeListNodes).Last<TreeListNode>());
  }

  private void MoveBottom()
  {
    foreach (TreeListNode selectedTreeListNode in this.GetSelectedTreeListNodes())
    {
      AdjustableMenuCommand tag = (AdjustableMenuCommand) selectedTreeListNode.Tag;
      tag.Parent.MoveBottom(tag);
      this._treeList.SetNodeIndex(selectedTreeListNode, selectedTreeListNode.ParentNode.Nodes.Count - 1);
    }
    this.MakeLastSelectedNodeVisible();
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void AddGroup()
  {
    TreeListNode node = this._treeList.Selection.Count > 0 ? this._treeList.Selection[0] : (TreeListNode) null;
    AdjustableMenuCommand tag = node != null ? node.Tag as AdjustableMenuCommand : (AdjustableMenuCommand) null;
    if (this._currentUserAndRole.Value.BlockedMenus || tag == null || tag.Parent.IsCommandFirstInGroup(tag) || !tag.Parent.AddGroup(tag))
      return;
    this._treeList.SetNodeIndex(node, node.ParentNode.Nodes.IndexOf(node));
    this._treeList.MakeNodeVisible(node);
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void RemoveGroup()
  {
    TreeListNode node = this._treeList.Selection.Count > 0 ? this._treeList.Selection[0] : (TreeListNode) null;
    AdjustableMenuCommand tag = node != null ? node.Tag as AdjustableMenuCommand : (AdjustableMenuCommand) null;
    if (this._currentUserAndRole.Value.BlockedMenus || tag == null || !tag.Parent.CanRemoveGroup(tag) || !tag.Parent.RemoveGroup(tag))
      return;
    this._treeList.SetNodeIndex(node, node.ParentNode.Nodes.IndexOf(node));
    this._treeList.MakeNodeVisible(node);
    this.IsChanged = true;
    this.UpdateControl();
  }

  private void Find()
  {
    if (this._contextMenuSearchForm == null || this._contextMenuSearchForm.IsDisposed)
    {
      this._contextMenuSearchForm = new ContextMenuSearchForm();
      this._contextMenuSearchForm.Owner = this.ParentForm;
    }
    this._contextMenuSearchForm.ShowForm(this._treeList);
  }

  private void CreateMenusTree()
  {
    if (this._adjustableMenuCommands == null)
      return;
    this._inEditor = true;
    try
    {
      this._treeListNodeByAdjustableMenuCommandDictionary.Clear();
      this._treeList.BeginUpdate();
      this._treeList.BeginSort();
      this._treeList.ClearNodes();
      TreeListNode parentNode = this._treeList.AppendNode((object) new object[2]
      {
        (object) LocalizationHolder.rm.GetString("Client.Core_574"),
        (object) LocalizationHolder.rm.GetString("Client.Core_575")
      }, (TreeListNode) null);
      parentNode.ImageIndex = this._namedImageList.Value.ImageIndex("imgHome");
      parentNode.SelectImageIndex = parentNode.ImageIndex;
      for (int index = 0; index < this._adjustableMenuCommands.Count; ++index)
        this.AddMenuItem(parentNode, this._adjustableMenuCommands[index]);
      parentNode.Expanded = true;
    }
    finally
    {
      this._treeList.EndSort();
      this._treeList.EndUpdate();
      this._inEditor = false;
    }
  }

  /// <summary>Добавить коллекцию меню</summary>
  /// <param name="parentNode">Родительский узел</param>
  /// <param name="menu">Команда меню</param>
  /// <returns>Узел в дереве</returns>
  private TreeListNode AddMenuItem(TreeListNode parentNode, AdjustableMenuCommand menu)
  {
    string caption = menu.Caption;
    TreeListNode treeListNode;
    if (!this._treeListNodeByAdjustableMenuCommandDictionary.ContainsKey(menu))
      treeListNode = this._treeList.AppendNode((object) new object[2]
      {
        (object) caption,
        (object) menu.Hint
      }, parentNode);
    else
      treeListNode = this._treeListNodeByAdjustableMenuCommandDictionary[menu];
    TreeListNode parentNode1 = treeListNode;
    parentNode1.CheckState = menu.Visible ? CheckState.Checked : CheckState.Unchecked;
    parentNode1[(object) this.columnCommand] = (object) caption;
    if (!this._treeListNodeByAdjustableMenuCommandDictionary.ContainsKey(menu))
    {
      this._treeListNodeByAdjustableMenuCommandDictionary[menu] = parentNode1;
      parentNode1.ImageIndex = parentNode1.SelectImageIndex = menu.ImageListSource == ImageListSource.NamedImageList ? menu.ImageIndex : -1;
      parentNode1.Tag = (object) menu;
    }
    for (int index = 0; index < menu.Items.Count; ++index)
      this.AddMenuItem(parentNode1, menu.Items[index]);
    return parentNode1;
  }

  private void SetTopButtonItemEnabled() => this._topButtonItem.Enabled = this.CanTop();

  private void SetUpButtonItemEnabled() => this._upButtonItem.Enabled = this.CanUp();

  private void SetDownButtonItemEnabled() => this._downButtonItem.Enabled = this.CanDown();

  private void SetBottomButtonItemEnabled() => this._bottomButtonItem.Enabled = this.CanBottom();

  private void SetAddGroupButtonItemEnabled()
  {
    this._addGroupButtonItem.Enabled = this.CanAddGroup();
  }

  private void SetRemoveGroupButtonItemEnabled()
  {
    this._removeGroupButtonItem.Enabled = this.CanRemoveGroup();
  }

  private void SetWarningButtonItemAndLabelItemVisible()
  {
    this._warningButtonItem.Visible = this._warningLabel.Visible = this._currentUserAndRole.Value.BlockedMenus;
  }

  private bool CanTop()
  {
    return this._selectedAdjustableMenuCommands.Count > 0 && this._selectedAdjustableMenuCommands.Where<AdjustableMenuCommand>((Func<AdjustableMenuCommand, bool>) (o => !o.Parent.CanMoveTop(o))).Count<AdjustableMenuCommand>() == 0;
  }

  private bool CanUp()
  {
    return this._selectedAdjustableMenuCommands.Count > 0 && this._selectedAdjustableMenuCommands.Where<AdjustableMenuCommand>((Func<AdjustableMenuCommand, bool>) (o => !o.Parent.CanMoveUp(o, false))).Count<AdjustableMenuCommand>() == 0;
  }

  private bool CanDown()
  {
    return this._selectedAdjustableMenuCommands.Count > 0 && this._selectedAdjustableMenuCommands.Where<AdjustableMenuCommand>((Func<AdjustableMenuCommand, bool>) (o => !o.Parent.CanMoveDown(o, false))).Count<AdjustableMenuCommand>() == 0;
  }

  private bool CanBottom()
  {
    return this._selectedAdjustableMenuCommands.Count > 0 && this._selectedAdjustableMenuCommands.Where<AdjustableMenuCommand>((Func<AdjustableMenuCommand, bool>) (o => !o.Parent.CanMoveBottom(o))).Count<AdjustableMenuCommand>() == 0;
  }

  private bool CanAddGroup()
  {
    return this._selectedAdjustableMenuCommands.Count == 1 && !this._selectedAdjustableMenuCommands[0].Parent.IsCommandFirstInGroup(this._selectedAdjustableMenuCommands[0]);
  }

  private bool CanRemoveGroup()
  {
    return this._selectedAdjustableMenuCommands.Count == 1 && this._selectedAdjustableMenuCommands[0].Parent.CanRemoveGroup(this._selectedAdjustableMenuCommands[0]);
  }

  private TreeListNode[] GetSelectedTreeListNodes()
  {
    return this._treeList.Selection.Cast<TreeListNode>().OrderBy<TreeListNode, int>((Func<TreeListNode, int>) (o => o.ParentNode == null ? -1 : o.ParentNode.Nodes.IndexOf(o))).ToArray<TreeListNode>();
  }

  private void SetSelectedAdjustableMenuCommands()
  {
    this._selectedAdjustableMenuCommands.Clear();
    foreach (TreeListNode selectedTreeListNode in this.GetSelectedTreeListNodes())
    {
      if (!(selectedTreeListNode.Tag is AdjustableMenuCommand))
      {
        this._selectedAdjustableMenuCommands.Clear();
        break;
      }
      this._selectedAdjustableMenuCommands.Add((AdjustableMenuCommand) selectedTreeListNode.Tag);
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.menuEditingContextsBar.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ContextMenuEditor));
    this.imagesMenus = new ImageList();
    this.menuEditingContextsBar = new Intermech.Bars.ToolBar();
    this._topButtonItem = new ButtonItem();
    this._upButtonItem = new ButtonItem();
    this._downButtonItem = new ButtonItem();
    this._bottomButtonItem = new ButtonItem();
    this._addGroupButtonItem = new ButtonItem();
    this._removeGroupButtonItem = new ButtonItem();
    this._warningButtonItem = new ButtonItem();
    this._warningLabel = new LabelItem();
    this._findButtonItem = new ButtonItem();
    this._treeList = new TreeList();
    this.columnCommand = new TreeListColumn();
    this.columnHint = new TreeListColumn();
    this.imagesState = new ImageList();
    this._treeList.BeginInit();
    this.SuspendLayout();
    this.imagesMenus.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesMenus.ImageStream");
    this.imagesMenus.TransparentColor = Color.Transparent;
    this.imagesMenus.Images.SetKeyName(0, "arrow_up_blue.ico");
    this.imagesMenus.Images.SetKeyName(1, "arrow_down_blue.ico");
    this.imagesMenus.Images.SetKeyName(2, "add_menu_group.ico");
    this.imagesMenus.Images.SetKeyName(3, "delete_menu_group.ico");
    this.imagesMenus.Images.SetKeyName(4, "Intermech.Imbase.Resources.FindByName.ico");
    this.imagesMenus.Images.SetKeyName(5, "asterisk.ico");
    this.imagesMenus.Images.SetKeyName(6, "arrow_top_blue.ico");
    this.imagesMenus.Images.SetKeyName(7, "arrow_bottom_blue.ico");
    this.menuEditingContextsBar.AddRemoveButtonsVisible = false;
    this.menuEditingContextsBar.AllowHorizontalDock = false;
    this.menuEditingContextsBar.Closable = false;
    this.menuEditingContextsBar.DockLine = 3;
    this.menuEditingContextsBar.DrawActionsButton = false;
    this.menuEditingContextsBar.FullMenus = true;
    this.menuEditingContextsBar.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.menuEditingContextsBar.Hidden = false;
    this.menuEditingContextsBar.ImageList = this.imagesMenus;
    this.menuEditingContextsBar.Items.AddRange(new ToolbarItemBase[9]
    {
      (ToolbarItemBase) this._topButtonItem,
      (ToolbarItemBase) this._upButtonItem,
      (ToolbarItemBase) this._downButtonItem,
      (ToolbarItemBase) this._bottomButtonItem,
      (ToolbarItemBase) this._addGroupButtonItem,
      (ToolbarItemBase) this._removeGroupButtonItem,
      (ToolbarItemBase) this._warningButtonItem,
      (ToolbarItemBase) this._warningLabel,
      (ToolbarItemBase) this._findButtonItem
    });
    componentResourceManager.ApplyResources((object) this.menuEditingContextsBar, "menuEditingContextsBar");
    this.menuEditingContextsBar.MinimumFloatingSize = new Size(250, 30);
    this.menuEditingContextsBar.Movable = false;
    this.menuEditingContextsBar.Name = "menuEditingContextsBar";
    this.menuEditingContextsBar.Overflow = ToolBarOverflow.Wrap;
    this.menuEditingContextsBar.Stretch = true;
    this.menuEditingContextsBar.Tearable = false;
    componentResourceManager.ApplyResources((object) this._topButtonItem, "_topButtonItem");
    this._topButtonItem.ImageIndex = 6;
    this._topButtonItem.Click += new EventHandler(this.TopButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._upButtonItem, "_upButtonItem");
    this._upButtonItem.ImageIndex = 0;
    this._upButtonItem.Click += new EventHandler(this.UpButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._downButtonItem, "_downButtonItem");
    this._downButtonItem.ImageIndex = 1;
    this._downButtonItem.Click += new EventHandler(this.DownButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._bottomButtonItem, "_bottomButtonItem");
    this._bottomButtonItem.ImageIndex = 7;
    this._bottomButtonItem.Click += new EventHandler(this.BottomButtonItem_Click);
    this._addGroupButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._addGroupButtonItem, "_addGroupButtonItem");
    this._addGroupButtonItem.ImageIndex = 2;
    this._addGroupButtonItem.Click += new EventHandler(this.AddGroupButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._removeGroupButtonItem, "_removeGroupButtonItem");
    this._removeGroupButtonItem.ImageIndex = 3;
    this._removeGroupButtonItem.Click += new EventHandler(this.RemoveGroupButtonItem_Click);
    this._warningButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._warningButtonItem, "_warningButtonItem");
    this._warningButtonItem.ImageIndex = 5;
    this._warningButtonItem.Visible = false;
    componentResourceManager.ApplyResources((object) this._warningLabel, "_warningLabel");
    this._warningLabel.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
    this._warningLabel.Visible = false;
    this._findButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this._findButtonItem, "_findButtonItem");
    this._findButtonItem.ImageIndex = 4;
    this._findButtonItem.Click += new EventHandler(this.FindButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._treeList, "_treeList");
    this._treeList.CheckBoxes = CheckBoxesStyle.ThreeState;
    this._treeList.Columns.AddRange(new TreeListColumn[2]
    {
      this.columnCommand,
      this.columnHint
    });
    this._treeList.Name = "treeMenuCommands";
    this._treeList.StateImageList = this.imagesState;
    this._treeList.Styles.AddReplace("HideSelectionRow", (object) new ViewStyle("HideSelectionRow", "TreeList", new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeList.Styles.AddReplace("OddRow", (object) new ViewStyle("OddRow", "TreeList", new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.None, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LightGreen, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("GroupCellSelected", (object) new ViewStyle("GroupCellSelected", "", new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeList.Styles.AddReplace("RootCell", (object) new ViewStyle("RootCell", "", new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, Color.LemonChiffon, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("GroupCell", (object) new ViewStyle("GroupCell", (string) null, new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Window, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("RootCellSelected", (object) new ViewStyle("RootCellSelected", "", new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawEndEllipsis | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseHorzAlignment | StyleOptions.UseImage | StyleOptions.UseWordWrap | StyleOptions.UseVertAlignment, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.WindowText));
    this._treeList.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", "TreeList", new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this._treeList.GetCustomNodeCellStyle += new GetCustomNodeCellStyleEventHandler(this.TreeList_GetCustomNodeCellStyle);
    this._treeList.CheckStateChanging += new CheckStateChangingEventHandler(this.TreeList_CheckStateChanging);
    this._treeList.CheckStateChanged += new NodeEventHandler(this.TreeList_CheckStateChanged);
    this._treeList.SelectionChanged += new EventHandler(this.TreeList_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.columnCommand, "columnCommand");
    this.columnCommand.Name = "columnCommand";
    componentResourceManager.ApplyResources((object) this.columnHint, "columnHint");
    this.columnHint.Name = "columnHint";
    this.imagesState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesState.ImageStream");
    this.imagesState.TransparentColor = Color.Transparent;
    this.imagesState.Images.SetKeyName(0, "unchecked.ico");
    this.imagesState.Images.SetKeyName(1, "checked.ico");
    this.imagesState.Images.SetKeyName(2, "grayed.ico");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this._treeList);
    this.Controls.Add((Control) this.menuEditingContextsBar);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (ContextMenuEditor);
    this._treeList.EndInit();
    this.ResumeLayout(false);
  }
}
