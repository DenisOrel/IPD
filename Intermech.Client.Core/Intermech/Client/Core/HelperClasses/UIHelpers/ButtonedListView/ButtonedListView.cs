
// Type: Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ButtonedListView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Actions;
using Intermech.Client.Core.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView;

/// <summary>ListView with buttons</summary>
public class ButtonedListView : UserControl
{
  /// <summary>Показывать встроенное меню</summary>
  private bool _allowInternalContextMenu = true;
  /// <summary>Показывать команду Свойства</summary>
  private bool _configButtonVisible;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ActionList actList;
  private Panel pnlBottom;
  private Panel pnlClient;
  private Panel pnlTop;
  protected ListView ListView;
  private Label lblCaption;
  protected Intermech.Actions.Action actAdd;
  protected Intermech.Actions.Action actRemove;
  protected Intermech.Actions.Action actConfigure;
  private ToolStrip toolStripVert;
  protected ToolStripButton toolStripButton1;
  protected ToolStripButton toolStripButton2;
  protected ToolStripButton toolStripButton3;
  private Panel pnlRight;
  private ToolStrip toolStripHor;
  private ToolStripButton toolStripButton4;
  private ToolStripButton toolStripButton5;
  private ToolStripButton toolStripButton6;
  private ToolStripButton toolStripButton7;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton btnAdd;
  private ToolStripButton btnRemove;
  private ToolStripButton btnProps;
  private ToolStripButton btnTop;
  private ToolStripButton btnUp;
  private ToolStripSeparator toolStripSeparatorRightPanel;
  private ToolStripButton btnDown;
  private ToolStripButton btnBottom;
  private Intermech.Actions.Action actTop;
  private Intermech.Actions.Action actUp;
  private Intermech.Actions.Action actDown;
  private Intermech.Actions.Action actBottom;
  private ImageList actImageList;
  private ContextMenuStrip internalContextMenu;
  private ToolStripMenuItem addToolStripMenuItem;
  private ToolStripMenuItem removeToolStripMenuItem;
  private ToolStripMenuItem configureToolStripMenuItem;
  private ContextMenuStrip commonContextMenuStrip;
  private ToolStripMenuItem MoveToolStripMenuItem;
  private ToolStripMenuItem TopToolStripMenuItem;
  private ToolStripMenuItem UpToolStripMenuItem;
  private ToolStripMenuItem DownToolStripMenuItem;
  private ToolStripMenuItem BottomToolStripMenuItem;
  private Intermech.Actions.Action actMove;
  protected ToolStrip toolStrip;

  public ButtonedListView() => this.InitializeComponent();

  /// <summary>До добавление элемента</summary>
  public event CancelEventHandler BeforeAddItem;

  /// <summary>После добавление элемента</summary>
  public event Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ButtonedListView.ButtonedListViewItemEventHandler AfterAddItem;

  /// <summary>Свойства элемента</summary>
  public event Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ButtonedListView.ButtonedListViewItemEventHandler ConfigureItem;

  /// <summary>До удаления элемента</summary>
  public event CancelEventHandler BeforeDeleteItem;

  /// <summary>После удаление элемента</summary>
  public event Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ButtonedListView.ButtonedListViewItemEventHandler AfterDeleteItem;

  /// <summary>Коллекция элементов</summary>
  public ListView.ListViewItemCollection Items => this.ListView.Items;

  /// <summary>Картинка для листа</summary>
  public ImageList ImageList
  {
    get => this.ListView.SmallImageList;
    set => this.ListView.SmallImageList = value;
  }

  /// <summary>Заголовок элемента</summary>
  public string Caption
  {
    get => this.lblCaption.Text;
    set
    {
      if (value.Length != 0)
      {
        this.pnlTop.Visible = true;
        this.lblCaption.Text = value;
      }
      else
      {
        this.pnlTop.Visible = false;
        this.lblCaption.Text = value;
      }
    }
  }

  /// <summary>Видимость правой панели</summary>
  [DefaultValue(false)]
  public bool RightPanel
  {
    get => this.pnlRight.Visible;
    set => this.pnlRight.Visible = this.actMove.Visible = value;
  }

  /// <summary>Разрешено ли встроенное контекстное меню</summary>
  [Description("Allow internal context menu")]
  [DefaultValue(true)]
  public bool AllowInternalContextMenu
  {
    get => this._allowInternalContextMenu;
    set => this._allowInternalContextMenu = value;
  }

  /// <summary>Видимость кнопки/команды контекстного меню Свойства</summary>
  [Description("Show config command for items")]
  [DefaultValue(false)]
  public bool ConfigButtonVisible
  {
    get => this._configButtonVisible;
    set => this._configButtonVisible = this.btnProps.Visible = value;
  }

  /// <summary>Колонки</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public ListView.ColumnHeaderCollection Columns => this.ListView.Columns;

  /// <summary>Стиль колонок</summary>
  public ColumnHeaderStyle HeaderStyle
  {
    get => this.ListView.HeaderStyle;
    set => this.ListView.HeaderStyle = value;
  }

  /// <summary>Дополнительная панель внизу</summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public ToolStrip ToolStrip => this.toolStrip;

  /// <summary>Показывать линии сетки</summary>
  public bool GridLines
  {
    get => this.ListView.GridLines;
    set => this.ListView.GridLines = value;
  }

  /// <summary>Тип отображения ListView</summary>
  public View View
  {
    get => this.ListView.View;
    set => this.ListView.View = value;
  }

  /// <summary>Вверх</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actTop_Execute(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    this.ListView.Items.Remove(selectedItem);
    this.ListView.Items.Insert(0, selectedItem);
  }

  /// <summary>Выше</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actUp_Execute(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    int index = selectedItem.Index - 1;
    this.ListView.Items.Remove(selectedItem);
    this.ListView.Items.Insert(index, selectedItem);
  }

  /// <summary>Ниже</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actDown_Execute(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    int index = selectedItem.Index + 1;
    this.ListView.Items.Remove(selectedItem);
    this.ListView.Items.Insert(index, selectedItem);
  }

  /// <summary>Вниз</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actBottom_Execute(object sender, EventArgs e)
  {
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    this.ListView.Items.Remove(selectedItem);
    this.ListView.Items.Insert(this.ListView.Items.Count, selectedItem);
  }

  /// <summary>Вверх</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actTop_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    ((Intermech.Actions.Action) sender).Enabled = this.ListView.SelectedItems.Count > 0 && this.ListView.SelectedItems[0].Index > 0;
  }

  /// <summary>Вниз</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actBottom_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    ((Intermech.Actions.Action) sender).Enabled = this.ListView.SelectedItems.Count > 0 && this.ListView.SelectedItems[0].Index < this.ListView.Items.Count - 1;
  }

  /// <summary>Add item</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actAdd_Execute(object sender, EventArgs e)
  {
    CancelEventArgs e1 = new CancelEventArgs();
    if (this.BeforeAddItem != null)
      this.BeforeAddItem((object) this, e1);
    if (e1.Cancel)
      return;
    ListViewItem listViewItem = this.ListView.Items.Add(string.Empty);
    listViewItem.Selected = true;
    if (this.AfterAddItem == null)
      return;
    this.AfterAddItem((object) this, new ItemEventArgs(listViewItem));
  }

  /// <summary>Configure item</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actConfigure_Execute(object sender, EventArgs e)
  {
    if (this.ListView.SelectedItems[0] == null)
      return;
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    if (this.ConfigureItem == null)
      return;
    this.ConfigureItem((object) this, new ItemEventArgs(selectedItem));
  }

  /// <summary>Remove item</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public virtual void actRemove_Execute(object sender, EventArgs e)
  {
    CancelEventArgs e1 = new CancelEventArgs();
    if (this.ListView.SelectedItems[0] == null)
      return;
    if (this.BeforeDeleteItem != null)
      this.BeforeDeleteItem((object) this, e1);
    if (e1.Cancel)
      return;
    ListViewItem selectedItem = this.ListView.SelectedItems[0];
    this.ListView.Items.Remove(selectedItem);
    if (this.AfterDeleteItem == null)
      return;
    this.AfterDeleteItem((object) this, new ItemEventArgs(selectedItem));
  }

  /// <summary>Update remove or configure item</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actRemove_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    ((Intermech.Actions.Action) sender).Enabled = this.ListView.SelectedItems.Count > 0;
  }

  /// <summary>Update configure item</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void actConfigure_Update(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    if (this.ConfigButtonVisible)
    {
      ((Intermech.Actions.Action) sender).Visible = true;
      ((Intermech.Actions.Action) sender).Enabled = this.ListView.SelectedItems.Count > 0;
    }
    else
      ((Intermech.Actions.Action) sender).Visible = false;
  }

  private void commonContextMenuStrip_Opening(object sender, CancelEventArgs e)
  {
    if (!this.AllowInternalContextMenu && this.ContextMenuStrip == null)
      return;
    if (this.AllowInternalContextMenu)
    {
      ToolStripItem[] array = this.internalContextMenu.Items.OfType<ToolStripItem>().ToArray<ToolStripItem>();
      if (array.Length != 0)
        this.commonContextMenuStrip.Items.AddRange(array);
    }
    if (this.ContextMenuStrip == null)
      return;
    ToolStripItem[] array1 = this.ContextMenuStrip.Items.OfType<ToolStripItem>().ToArray<ToolStripItem>();
    if (array1.Length == 0)
      return;
    this.commonContextMenuStrip.Items.AddRange(array1);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Intermech.Client.Core.HelperClasses.UIHelpers.ButtonedListView.ButtonedListView));
    this.actList = new ActionList(this.components);
    this.actAdd = new Intermech.Actions.Action(this.components);
    this.actRemove = new Intermech.Actions.Action(this.components);
    this.actConfigure = new Intermech.Actions.Action(this.components);
    this.actTop = new Intermech.Actions.Action(this.components);
    this.actUp = new Intermech.Actions.Action(this.components);
    this.actDown = new Intermech.Actions.Action(this.components);
    this.actBottom = new Intermech.Actions.Action(this.components);
    this.actMove = new Intermech.Actions.Action(this.components);
    this.addToolStripMenuItem = new ToolStripMenuItem();
    this.removeToolStripMenuItem = new ToolStripMenuItem();
    this.configureToolStripMenuItem = new ToolStripMenuItem();
    this.MoveToolStripMenuItem = new ToolStripMenuItem();
    this.TopToolStripMenuItem = new ToolStripMenuItem();
    this.UpToolStripMenuItem = new ToolStripMenuItem();
    this.DownToolStripMenuItem = new ToolStripMenuItem();
    this.BottomToolStripMenuItem = new ToolStripMenuItem();
    this.btnTop = new ToolStripButton();
    this.btnUp = new ToolStripButton();
    this.btnDown = new ToolStripButton();
    this.btnBottom = new ToolStripButton();
    this.btnAdd = new ToolStripButton();
    this.btnRemove = new ToolStripButton();
    this.btnProps = new ToolStripButton();
    this.actImageList = new ImageList(this.components);
    this.pnlBottom = new Panel();
    this.toolStrip = new ToolStrip();
    this.toolStripVert = new ToolStrip();
    this.pnlClient = new Panel();
    this.ListView = new ListView();
    this.commonContextMenuStrip = new ContextMenuStrip(this.components);
    this.internalContextMenu = new ContextMenuStrip(this.components);
    this.pnlTop = new Panel();
    this.lblCaption = new Label();
    this.pnlRight = new Panel();
    this.toolStripHor = new ToolStrip();
    this.toolStripSeparatorRightPanel = new ToolStripSeparator();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.pnlBottom.SuspendLayout();
    this.toolStripVert.SuspendLayout();
    this.pnlClient.SuspendLayout();
    this.internalContextMenu.SuspendLayout();
    this.pnlTop.SuspendLayout();
    this.pnlRight.SuspendLayout();
    this.toolStripHor.SuspendLayout();
    this.SuspendLayout();
    this.actList.Actions.AddRange(new Intermech.Actions.Action[8]
    {
      this.actAdd,
      this.actRemove,
      this.actConfigure,
      this.actTop,
      this.actUp,
      this.actDown,
      this.actBottom,
      this.actMove
    });
    this.actList.ImageList = (ImageList) null;
    this.actList.ShowTextOnToolBar = false;
    this.actList.Tag = (object) null;
    this.actAdd.Hint = (string) null;
    this.actAdd.Text = "Добавить";
    this.actAdd.Execute += new EventHandler(this.actAdd_Execute);
    this.actRemove.Hint = (string) null;
    this.actRemove.Text = "Удалить";
    this.actRemove.Execute += new EventHandler(this.actRemove_Execute);
    this.actRemove.Update += new EventHandler(this.actRemove_Update);
    this.actConfigure.Hint = (string) null;
    this.actConfigure.Text = "Свойства";
    this.actConfigure.Execute += new EventHandler(this.actConfigure_Execute);
    this.actConfigure.Update += new EventHandler(this.actConfigure_Update);
    this.actTop.Hint = (string) null;
    this.actTop.Text = "Переместить вверх";
    this.actTop.Execute += new EventHandler(this.actTop_Execute);
    this.actTop.Update += new EventHandler(this.actTop_Update);
    this.actUp.Hint = (string) null;
    this.actUp.Text = "Переместить выше";
    this.actUp.Execute += new EventHandler(this.actUp_Execute);
    this.actUp.Update += new EventHandler(this.actTop_Update);
    this.actDown.Hint = (string) null;
    this.actDown.Text = "Переместить ниже";
    this.actDown.Execute += new EventHandler(this.actDown_Execute);
    this.actDown.Update += new EventHandler(this.actBottom_Update);
    this.actBottom.Hint = (string) null;
    this.actBottom.Text = "Переместить вниз";
    this.actBottom.Execute += new EventHandler(this.actBottom_Execute);
    this.actBottom.Update += new EventHandler(this.actBottom_Update);
    this.actMove.Hint = (string) null;
    this.actMove.Text = "Переместить";
    this.actMove.Update += new EventHandler(this.actRemove_Update);
    this.actList.SetAction((Component) this.addToolStripMenuItem, this.actAdd);
    this.addToolStripMenuItem.MergeIndex = 0;
    this.addToolStripMenuItem.Name = "addToolStripMenuItem";
    this.addToolStripMenuItem.Size = new Size(146, 22);
    this.addToolStripMenuItem.Text = "Добавить";
    this.actList.SetAction((Component) this.removeToolStripMenuItem, this.actRemove);
    this.removeToolStripMenuItem.MergeIndex = 1;
    this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
    this.removeToolStripMenuItem.Size = new Size(146, 22);
    this.removeToolStripMenuItem.Text = "Удалить";
    this.actList.SetAction((Component) this.configureToolStripMenuItem, this.actConfigure);
    this.configureToolStripMenuItem.MergeIndex = 2;
    this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
    this.configureToolStripMenuItem.Size = new Size(146, 22);
    this.configureToolStripMenuItem.Text = "Свойства";
    this.actList.SetAction((Component) this.MoveToolStripMenuItem, this.actMove);
    this.MoveToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.TopToolStripMenuItem,
      (ToolStripItem) this.UpToolStripMenuItem,
      (ToolStripItem) this.DownToolStripMenuItem,
      (ToolStripItem) this.BottomToolStripMenuItem
    });
    this.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem";
    this.MoveToolStripMenuItem.Size = new Size(146, 22);
    this.MoveToolStripMenuItem.Text = "Переместить";
    this.actList.SetAction((Component) this.TopToolStripMenuItem, this.actTop);
    this.TopToolStripMenuItem.Name = "TopToolStripMenuItem";
    this.TopToolStripMenuItem.Size = new Size(181, 22);
    this.TopToolStripMenuItem.Text = "Переместить вверх";
    this.actList.SetAction((Component) this.UpToolStripMenuItem, this.actUp);
    this.UpToolStripMenuItem.Name = "UpToolStripMenuItem";
    this.UpToolStripMenuItem.Size = new Size(181, 22);
    this.UpToolStripMenuItem.Text = "Переместить выше";
    this.actList.SetAction((Component) this.DownToolStripMenuItem, this.actDown);
    this.DownToolStripMenuItem.Name = "DownToolStripMenuItem";
    this.DownToolStripMenuItem.Size = new Size(181, 22);
    this.DownToolStripMenuItem.Text = "Переместить ниже";
    this.actList.SetAction((Component) this.BottomToolStripMenuItem, this.actBottom);
    this.BottomToolStripMenuItem.Name = "BottomToolStripMenuItem";
    this.BottomToolStripMenuItem.Size = new Size(181, 22);
    this.BottomToolStripMenuItem.Text = "Переместить вниз";
    this.actList.SetAction((Component) this.btnTop, this.actTop);
    this.btnTop.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnTop.Image = (Image) Resources.arrow_top_blue;
    this.btnTop.ImageTransparentColor = Color.Magenta;
    this.btnTop.Name = "btnTop";
    this.btnTop.Size = new Size(22, 20);
    this.btnTop.Text = "Переместить вверх";
    this.actList.SetAction((Component) this.btnUp, this.actUp);
    this.btnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnUp.Image = (Image) Resources.arrow_up_blue;
    this.btnUp.ImageTransparentColor = Color.Magenta;
    this.btnUp.Name = "btnUp";
    this.btnUp.Size = new Size(22, 20);
    this.btnUp.Text = "Переместить выше";
    this.actList.SetAction((Component) this.btnDown, this.actDown);
    this.btnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnDown.Image = (Image) Resources.arrow_down_blue;
    this.btnDown.ImageTransparentColor = Color.Magenta;
    this.btnDown.Name = "btnDown";
    this.btnDown.Size = new Size(22, 20);
    this.btnDown.Text = "Переместить ниже";
    this.actList.SetAction((Component) this.btnBottom, this.actBottom);
    this.btnBottom.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnBottom.Image = (Image) Resources.arrow_bottom_blue;
    this.btnBottom.ImageTransparentColor = Color.Magenta;
    this.btnBottom.Name = "btnBottom";
    this.btnBottom.Size = new Size(22, 20);
    this.btnBottom.Text = "Переместить вниз";
    this.actList.SetAction((Component) this.btnAdd, this.actAdd);
    this.btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnAdd.Image = (Image) Resources.AddStandart;
    this.btnAdd.ImageTransparentColor = Color.Magenta;
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(23, 20);
    this.btnAdd.Text = "Добавить";
    this.actList.SetAction((Component) this.btnRemove, this.actRemove);
    this.btnRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnRemove.Image = (Image) Resources.DeleteStandart;
    this.btnRemove.ImageTransparentColor = Color.Magenta;
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Size = new Size(23, 20);
    this.btnRemove.Text = "Удалить";
    this.actList.SetAction((Component) this.btnProps, this.actConfigure);
    this.btnProps.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnProps.Image = (Image) Resources.EditStandart;
    this.btnProps.ImageTransparentColor = Color.Magenta;
    this.btnProps.Name = "btnProps";
    this.btnProps.Size = new Size(23, 20);
    this.btnProps.Text = "Свойства";
    this.actImageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("actImageList.ImageStream");
    this.actImageList.TransparentColor = Color.Transparent;
    this.actImageList.Images.SetKeyName(0, "AddStandart.png");
    this.actImageList.Images.SetKeyName(1, "DeleteStandart.png");
    this.actImageList.Images.SetKeyName(2, "EditStandart.png");
    this.actImageList.Images.SetKeyName(3, "arrow_top_blue.ico");
    this.actImageList.Images.SetKeyName(4, "arrow_up_blue.ico");
    this.actImageList.Images.SetKeyName(5, "arrow_down_blue.ico");
    this.actImageList.Images.SetKeyName(6, "arrow_bottom_blue.ico");
    this.actImageList.Images.SetKeyName(7, "arrow_all_left_blue.ico");
    this.actImageList.Images.SetKeyName(8, "arrow_left_blue.ico");
    this.actImageList.Images.SetKeyName(9, "arrow_all_right_blue.ico");
    this.actImageList.Images.SetKeyName(10, "arrow_right_blue.ico");
    this.pnlBottom.Controls.Add((Control) this.toolStrip);
    this.pnlBottom.Controls.Add((Control) this.toolStripVert);
    this.pnlBottom.Dock = DockStyle.Bottom;
    this.pnlBottom.Location = new Point(0, 121);
    this.pnlBottom.Name = "pnlBottom";
    this.pnlBottom.Size = new Size(198, 25);
    this.pnlBottom.TabIndex = 0;
    this.toolStrip.Dock = DockStyle.Fill;
    this.toolStrip.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
    this.toolStrip.Location = new Point(70, 0);
    this.toolStrip.Name = "toolStrip";
    this.toolStrip.Size = new Size(128 /*0x80*/, 25);
    this.toolStrip.TabIndex = 1;
    this.toolStripVert.Dock = DockStyle.Left;
    this.toolStripVert.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.btnAdd,
      (ToolStripItem) this.btnRemove,
      (ToolStripItem) this.btnProps
    });
    this.toolStripVert.LayoutStyle = ToolStripLayoutStyle.Flow;
    this.toolStripVert.Location = new Point(0, 0);
    this.toolStripVert.Name = "toolStripVert";
    this.toolStripVert.Size = new Size(70, 25);
    this.toolStripVert.TabIndex = 0;
    this.toolStripVert.Text = "toolStrip1";
    this.pnlClient.Controls.Add((Control) this.ListView);
    this.pnlClient.Dock = DockStyle.Fill;
    this.pnlClient.Location = new Point(0, 15);
    this.pnlClient.Name = "pnlClient";
    this.pnlClient.Size = new Size(174, 106);
    this.pnlClient.TabIndex = 1;
    this.ListView.ContextMenuStrip = this.commonContextMenuStrip;
    this.ListView.Dock = DockStyle.Fill;
    this.ListView.FullRowSelect = true;
    this.ListView.HeaderStyle = ColumnHeaderStyle.None;
    this.ListView.HideSelection = false;
    this.ListView.Location = new Point(0, 0);
    this.ListView.MultiSelect = false;
    this.ListView.Name = "ListView";
    this.ListView.ShowGroups = false;
    this.ListView.Size = new Size(174, 106);
    this.ListView.TabIndex = 0;
    this.ListView.UseCompatibleStateImageBehavior = false;
    this.ListView.View = View.List;
    this.ListView.DoubleClick += new EventHandler(this.actConfigure_Execute);
    this.commonContextMenuStrip.Name = "commonContextMenuStrip";
    this.commonContextMenuStrip.Size = new Size(61, 4);
    this.commonContextMenuStrip.Opening += new CancelEventHandler(this.commonContextMenuStrip_Opening);
    this.internalContextMenu.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.addToolStripMenuItem,
      (ToolStripItem) this.removeToolStripMenuItem,
      (ToolStripItem) this.configureToolStripMenuItem,
      (ToolStripItem) this.MoveToolStripMenuItem
    });
    this.internalContextMenu.Name = "contextMenuStrip";
    this.internalContextMenu.Size = new Size(147, 92);
    this.pnlTop.Controls.Add((Control) this.lblCaption);
    this.pnlTop.Dock = DockStyle.Top;
    this.pnlTop.Location = new Point(0, 0);
    this.pnlTop.Name = "pnlTop";
    this.pnlTop.Size = new Size(198, 15);
    this.pnlTop.TabIndex = 2;
    this.pnlTop.Visible = false;
    this.lblCaption.AutoSize = true;
    this.lblCaption.Dock = DockStyle.Left;
    this.lblCaption.Location = new Point(0, 0);
    this.lblCaption.Name = "lblCaption";
    this.lblCaption.Size = new Size(0, 13);
    this.lblCaption.TabIndex = 0;
    this.pnlRight.AutoSize = true;
    this.pnlRight.Controls.Add((Control) this.toolStripHor);
    this.pnlRight.Dock = DockStyle.Right;
    this.pnlRight.Location = new Point(174, 15);
    this.pnlRight.Name = "pnlRight";
    this.pnlRight.Size = new Size(24, 106);
    this.pnlRight.TabIndex = 3;
    this.pnlRight.Visible = false;
    this.toolStripHor.Dock = DockStyle.Fill;
    this.toolStripHor.GripStyle = ToolStripGripStyle.Hidden;
    this.toolStripHor.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.btnTop,
      (ToolStripItem) this.btnUp,
      (ToolStripItem) this.toolStripSeparatorRightPanel,
      (ToolStripItem) this.btnDown,
      (ToolStripItem) this.btnBottom
    });
    this.toolStripHor.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
    this.toolStripHor.Location = new Point(0, 0);
    this.toolStripHor.Name = "toolStripHor";
    this.toolStripHor.Size = new Size(24, 106);
    this.toolStripHor.TabIndex = 0;
    this.toolStripHor.Text = "toolStrip1";
    this.toolStripSeparatorRightPanel.Name = "toolStripSeparatorRightPanel";
    this.toolStripSeparatorRightPanel.Size = new Size(22, 6);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(22, 6);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.pnlClient);
    this.Controls.Add((Control) this.pnlRight);
    this.Controls.Add((Control) this.pnlBottom);
    this.Controls.Add((Control) this.pnlTop);
    this.Name = nameof (ButtonedListView);
    this.Size = new Size(198, 146);
    this.pnlBottom.ResumeLayout(false);
    this.pnlBottom.PerformLayout();
    this.toolStripVert.ResumeLayout(false);
    this.toolStripVert.PerformLayout();
    this.pnlClient.ResumeLayout(false);
    this.internalContextMenu.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    this.pnlTop.PerformLayout();
    this.pnlRight.ResumeLayout(false);
    this.pnlRight.PerformLayout();
    this.toolStripHor.ResumeLayout(false);
    this.toolStripHor.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>
  /// Represents the method that will handle the AddItem event of a ButtonedListView control.
  /// </summary>
  /// <param name="sender">The source of the event</param>
  /// <param name="e">A ItemsEventArgs that contains the event data.</param>
  public delegate void ButtonedListViewItemEventHandler(object sender, ItemEventArgs e);

  public delegate void ButtonedListViewItemsEventHandler(object sender, ItemsEventArgs e);
}
