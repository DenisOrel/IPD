
// Type: Intermech.Client.Core.Redline.RedlineToolBarPresenter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Redline;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Redline;

public class RedlineToolBarPresenter
{
  private static readonly INamedImageList _namedImageList = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);
  private ICommandManager _commandManager;
  private Intermech.Bars.ToolBar _toolBarRed;
  private Intermech.Bars.ToolBar _toolBarTreeView;
  private MenuBar _menuBarTreeView;
  private ContextMenuBarItem _contextMenuBarItemTree;
  private ButtonItem btnSave;
  private ButtonItem btnUndo;
  private ButtonItem btnRedo;
  private ComboBoxItem cbBoxRole;
  private ButtonItem btPointer;
  private ButtonItem btRedLine;
  private ButtonItem btRedPencil;
  private ButtonItem btRedNote;
  private ButtonItem btRedEllipse;
  private ButtonItem btRedEllipseFill;
  private ButtonItem btRedCircle;
  private ButtonItem btRedCircleFill;
  private ButtonItem btRedRectangle;
  private ButtonItem btRedRectangleFill;
  private MenuButtonItem mBtItem_Agreed;
  private MenuButtonItem mBtItem_Inconsistent;
  private MenuButtonItem mBtItem_Rejected;
  private MenuButtonItem mBtItem_Corrected;
  private MenuButtonItem mBtItem_Rename;
  private MenuButtonItem mBtItem_Remove;

  public static ImageList ImageList => RedlineToolBarPresenter._namedImageList.ImageList;

  public static int UserImageIndex => RedlineToolBarPresenter._namedImageList.ImageIndex("imgUser");

  public static int RoleImageIndex
  {
    get => RedlineToolBarPresenter._namedImageList.ImageIndex("imgUserRoles");
  }

  public RedlineToolBarPresenter(ICommandManager commandManager)
  {
    this._commandManager = commandManager;
  }

  public Intermech.Bars.ToolBar RedlineEditingToolbar
  {
    get
    {
      if (this._toolBarRed == null)
        this.InitRedlineEditingToolBar();
      return this._toolBarRed;
    }
  }

  public Intermech.Bars.ToolBar TreeViewToolbar
  {
    get
    {
      if (this._toolBarTreeView == null)
        this.InitTreeViewToolBar();
      return this._toolBarTreeView;
    }
  }

  public MenuBar TreeViewContextMenu
  {
    get
    {
      if (this._menuBarTreeView == null)
        this.InitTreeViewContextMenu();
      return this._menuBarTreeView;
    }
  }

  private void InitTreeViewContextMenu()
  {
    this._menuBarTreeView = new MenuBar();
    this._menuBarTreeView.Guid = new Guid("3c93b2c5-40bd-44ce-9e42-ef00b3cd2ba8");
    this._menuBarTreeView.Hidden = false;
    this._menuBarTreeView.Name = "menuBarTreeView";
    this._menuBarTreeView.OwnerForm = (Form) null;
    this._menuBarTreeView.Size = new Size(204, 26);
    this._menuBarTreeView.Text = "menuBarTree";
    this._menuBarTreeView.Visible = false;
    this._menuBarTreeView.ImageList = RedlineToolBarPresenter._namedImageList.ImageList;
    this._contextMenuBarItemTree = new ContextMenuBarItem();
    this._contextMenuBarItemTree.ShowText = true;
    this.InitContextMenuItems();
    this._contextMenuBarItemTree.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mBtItem_Agreed,
      (ToolbarItemBase) this.mBtItem_Inconsistent,
      (ToolbarItemBase) this.mBtItem_Rejected,
      (ToolbarItemBase) this.mBtItem_Corrected,
      (ToolbarItemBase) this.mBtItem_Rename,
      (ToolbarItemBase) this.mBtItem_Remove
    });
    this._contextMenuBarItemTree.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.OnBeforeContextMenuPopup);
    if (this._commandManager != null)
    {
      foreach (object obj in (CollectionBase) this._contextMenuBarItemTree.Items)
      {
        if (obj is ButtonItemBase buttonItemBase)
          this._commandManager.Add(buttonItemBase);
      }
    }
    this._menuBarTreeView.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this._contextMenuBarItemTree
    });
  }

  private void OnBeforeContextMenuPopup(object sender, MenuPopupEventArgs e)
  {
    if (this._commandManager == null)
      return;
    foreach (object obj in (CollectionBase) this._contextMenuBarItemTree.Items)
    {
      if (obj is MenuButtonItem menuButtonItem)
      {
        ICommandState command = this._commandManager.FindCommand(menuButtonItem.CommandName);
        if (command != null)
        {
          this._commandManager.QueryStatus(command);
          if (menuButtonItem.Enabled != command.Enabled)
            menuButtonItem.Enabled = command.Enabled;
        }
      }
    }
  }

  private void InitContextMenuItems()
  {
    string str = string.Format(LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.Item_CorrectedOrRejected") ?? string.Empty, (object) Redliner.Developed, (object) Redliner.Made);
    this.mBtItem_Agreed = new MenuButtonItem();
    this.mBtItem_Inconsistent = new MenuButtonItem();
    this.mBtItem_Rejected = new MenuButtonItem();
    this.mBtItem_Corrected = new MenuButtonItem();
    this.mBtItem_Rename = new MenuButtonItem();
    this.mBtItem_Remove = new MenuButtonItem();
    this.mBtItem_Agreed.CommandName = "eAgreed~E";
    this.mBtItem_Agreed.ShowText = true;
    ReportAttribute attribute1 = EStatusRemark.eAgreed.GetAttribute<ReportAttribute>();
    this.mBtItem_Agreed.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute1.ImgName);
    this.mBtItem_Agreed.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Agreed");
    this.mBtItem_Inconsistent.CommandName = "eInconsistent~E";
    this.mBtItem_Inconsistent.ShowText = true;
    ReportAttribute attribute2 = EStatusRemark.eInconsistent.GetAttribute<ReportAttribute>();
    this.mBtItem_Inconsistent.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute2.ImgName);
    this.mBtItem_Inconsistent.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Inconsistent");
    this.mBtItem_Inconsistent.Click += new EventHandler(this.ContextMenuItem_Click);
    this.mBtItem_Rejected.CommandName = "eRejected~E";
    this.mBtItem_Rejected.ShowText = true;
    ReportAttribute attribute3 = EStatusRemark.eRejected.GetAttribute<ReportAttribute>();
    this.mBtItem_Rejected.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute3.ImgName);
    this.mBtItem_Rejected.ToolTipText = str;
    this.mBtItem_Rejected.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Rejected");
    this.mBtItem_Rejected.Click += new EventHandler(this.ContextMenuItem_Click);
    this.mBtItem_Corrected.CommandName = "eCorrected~E";
    this.mBtItem_Corrected.ShowText = true;
    ReportAttribute attribute4 = EStatusRemark.eCorrected.GetAttribute<ReportAttribute>();
    this.mBtItem_Corrected.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute4.ImgName);
    this.mBtItem_Corrected.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Corrected");
    this.mBtItem_Corrected.ToolTipText = str;
    this.mBtItem_Corrected.Click += new EventHandler(this.ContextMenuItem_Click);
    this.mBtItem_Rename.CommandName = "RenameNote";
    this.mBtItem_Rename.ShowText = true;
    this.mBtItem_Rename.Text = LocalizationHolder.rm.GetString("Client.Core_1625");
    this.mBtItem_Rename.Click += new EventHandler(this.ContextMenuItem_Click);
    this.mBtItem_Remove.CommandName = "RemoveNote";
    this.mBtItem_Remove.ShowText = true;
    this.mBtItem_Remove.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgDelete");
    this.mBtItem_Remove.Text = LocalizationHolder.rm.GetString("Client.Core_1127");
    this.mBtItem_Remove.Click += new EventHandler(this.ContextMenuItem_Click);
  }

  private void ContextMenuItem_Click(object sender, EventArgs e)
  {
  }

  /// <summary>Собрать панель редактирования заметок</summary>
  private void InitRedlineEditingToolBar()
  {
    this._toolBarRed = new Intermech.Bars.ToolBar();
    this._toolBarRed.ImageList = RedlineToolBarPresenter._namedImageList.ImageList;
    this._toolBarRed.FullMenus = true;
    this._toolBarRed.Guid = new Guid("c95020a5-1bad-437e-b8e6-9e29251590a1");
    this._toolBarRed.Hidden = false;
    this._toolBarRed.Name = "toolBarRed";
    this._toolBarRed.Overflow = ToolBarOverflow.Wrap;
    this._toolBarRed.TabIndex = 0;
    this._toolBarRed.Text = LocalizationHolder.rm.GetString("EditNotes");
    this._toolBarRed.Visible = false;
    ToolbarItemBaseCollection items1 = this._toolBarRed.Items;
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgSave");
    buttonItem1.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_935");
    buttonItem1.CommandName = "RedSave";
    items1.Add((ToolbarItemBase) buttonItem1);
    ToolbarItemBaseCollection items2 = this._toolBarRed.Items;
    ButtonItem buttonItem2 = new ButtonItem();
    buttonItem2.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgUndo");
    buttonItem2.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_524");
    buttonItem2.CommandName = "RedUndo";
    items2.Add((ToolbarItemBase) buttonItem2);
    ToolbarItemBaseCollection items3 = this._toolBarRed.Items;
    ButtonItem buttonItem3 = new ButtonItem();
    buttonItem3.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedo");
    buttonItem3.ToolTipText = LocalizationHolder.rm.GetString("Redo");
    buttonItem3.CommandName = "RedRedo";
    items3.Add((ToolbarItemBase) buttonItem3);
    this.cbBoxRole = new ComboBoxItem();
    this.cbBoxRole.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbBoxRole.ComboBox.FlatStyle = FlatStyle.Flat;
    this.cbBoxRole.ToolTipText = LocalizationHolder.rm.GetString("Grafa");
    this.cbBoxRole.CommandName = "RedBoxRole";
    this.cbBoxRole.BeginGroup = true;
    this.cbBoxRole.Padding.Bottom = 0;
    this.cbBoxRole.Padding.Left = 1;
    this.cbBoxRole.Padding.Right = 1;
    this.cbBoxRole.Padding.Top = 0;
    this._toolBarRed.Items.Add((ToolbarItemBase) this.cbBoxRole);
    this.ClearRoleText();
    ToolbarItemBaseCollection items4 = this._toolBarRed.Items;
    ButtonItem buttonItem4 = new ButtonItem();
    buttonItem4.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedEdit");
    buttonItem4.ToolTipText = LocalizationHolder.rm.GetString("EditNotes");
    buttonItem4.CommandName = "btnRed";
    buttonItem4.BeginGroup = true;
    buttonItem4.Visible = false;
    buttonItem4.Enabled = false;
    items4.Add((ToolbarItemBase) buttonItem4);
    ToolbarItemBaseCollection items5 = this._toolBarRed.Items;
    ButtonItem buttonItem5 = new ButtonItem();
    buttonItem5.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgLinecolor");
    buttonItem5.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_334");
    buttonItem5.CommandName = "RedColor";
    buttonItem5.BeginGroup = true;
    buttonItem5.AutoToggle = AutoToggleType.Radio;
    items5.Add((ToolbarItemBase) buttonItem5);
    ToolbarItemBaseCollection items6 = this._toolBarRed.Items;
    ButtonItem buttonItem6 = new ButtonItem();
    buttonItem6.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgPointer");
    buttonItem6.ToolTipText = LocalizationHolder.rm.GetString("Pointer");
    buttonItem6.CommandName = "RedLinePointerTool";
    buttonItem6.AutoToggle = AutoToggleType.Radio;
    items6.Add((ToolbarItemBase) buttonItem6);
    ToolbarItemBaseCollection items7 = this._toolBarRed.Items;
    ButtonItem buttonItem7 = new ButtonItem();
    buttonItem7.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedLine");
    buttonItem7.ToolTipText = LocalizationHolder.rm.GetString("Line");
    buttonItem7.CommandName = "RedLineStrokeTool";
    buttonItem7.AutoToggle = AutoToggleType.Radio;
    items7.Add((ToolbarItemBase) buttonItem7);
    ToolbarItemBaseCollection items8 = this._toolBarRed.Items;
    ButtonItem buttonItem8 = new ButtonItem();
    buttonItem8.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedPencil");
    buttonItem8.ToolTipText = LocalizationHolder.rm.GetString("Pencil");
    buttonItem8.CommandName = "RedLinePencilTool";
    buttonItem8.AutoToggle = AutoToggleType.Radio;
    items8.Add((ToolbarItemBase) buttonItem8);
    ToolbarItemBaseCollection items9 = this._toolBarRed.Items;
    ButtonItem buttonItem9 = new ButtonItem();
    buttonItem9.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedNote");
    buttonItem9.ToolTipText = LocalizationHolder.rm.GetString("Note");
    buttonItem9.CommandName = "RedLineNoteTool";
    buttonItem9.AutoToggle = AutoToggleType.Radio;
    items9.Add((ToolbarItemBase) buttonItem9);
    ToolbarItemBaseCollection items10 = this._toolBarRed.Items;
    ButtonItem buttonItem10 = new ButtonItem();
    buttonItem10.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedEllipse");
    buttonItem10.ToolTipText = LocalizationHolder.rm.GetString("Ellipse");
    buttonItem10.CommandName = "RedLineEllipseTool";
    buttonItem10.AutoToggle = AutoToggleType.Radio;
    items10.Add((ToolbarItemBase) buttonItem10);
    ToolbarItemBaseCollection items11 = this._toolBarRed.Items;
    ButtonItem buttonItem11 = new ButtonItem();
    buttonItem11.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedEllipseFill");
    buttonItem11.ToolTipText = LocalizationHolder.rm.GetString("EllipseFill");
    buttonItem11.CommandName = "RedLineEllipseFillTool";
    buttonItem11.AutoToggle = AutoToggleType.Radio;
    items11.Add((ToolbarItemBase) buttonItem11);
    ToolbarItemBaseCollection items12 = this._toolBarRed.Items;
    ButtonItem buttonItem12 = new ButtonItem();
    buttonItem12.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedCircle");
    buttonItem12.ToolTipText = LocalizationHolder.rm.GetString("Circle");
    buttonItem12.CommandName = "RedLineCircleTool";
    buttonItem12.AutoToggle = AutoToggleType.Radio;
    items12.Add((ToolbarItemBase) buttonItem12);
    ToolbarItemBaseCollection items13 = this._toolBarRed.Items;
    ButtonItem buttonItem13 = new ButtonItem();
    buttonItem13.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedCircleFill");
    buttonItem13.ToolTipText = LocalizationHolder.rm.GetString("CircleFill");
    buttonItem13.CommandName = "RedLineCircleFillTool";
    buttonItem13.AutoToggle = AutoToggleType.Radio;
    items13.Add((ToolbarItemBase) buttonItem13);
    ToolbarItemBaseCollection items14 = this._toolBarRed.Items;
    ButtonItem buttonItem14 = new ButtonItem();
    buttonItem14.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedRectangle");
    buttonItem14.ToolTipText = LocalizationHolder.rm.GetString("Rectangle");
    buttonItem14.CommandName = "RedLineRectangleTool";
    buttonItem14.AutoToggle = AutoToggleType.Radio;
    items14.Add((ToolbarItemBase) buttonItem14);
    ToolbarItemBaseCollection items15 = this._toolBarRed.Items;
    ButtonItem buttonItem15 = new ButtonItem();
    buttonItem15.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgRedRectangleFill");
    buttonItem15.ToolTipText = LocalizationHolder.rm.GetString("RectangleFill");
    buttonItem15.CommandName = "RedLineRectangleFillTool";
    buttonItem15.AutoToggle = AutoToggleType.Radio;
    items15.Add((ToolbarItemBase) buttonItem15);
    if (this._commandManager == null)
      return;
    foreach (object obj in (CollectionBase) this._toolBarRed.Items)
    {
      if (obj is ButtonItemBase buttonItemBase)
        this._commandManager.Add(buttonItemBase);
    }
  }

  /// <summary>Комбобокс 'Роль'</summary>
  public ComboBoxItem GetRoleCombo() => this.cbBoxRole;

  /// <summary>Собрать панель дерева заметок</summary>
  private void InitTreeViewToolBar()
  {
    this._toolBarTreeView = new Intermech.Bars.ToolBar();
    this._toolBarTreeView.ImageList = RedlineToolBarPresenter._namedImageList.ImageList;
    this._toolBarTreeView.FullMenus = true;
    this._toolBarTreeView.Guid = new Guid("2fba31fc-0191-4e51-b781-36a5e0f478a9");
    this._toolBarTreeView.Hidden = false;
    this._toolBarTreeView.Name = "toolBarTreeView";
    this._toolBarTreeView.Overflow = ToolBarOverflow.Wrap;
    this._toolBarTreeView.TabIndex = 10;
    this._toolBarTreeView.Text = "toolBarTreeView";
    this._toolBarTreeView.Visible = true;
    ToolbarItemBaseCollection items1 = this._toolBarTreeView.Items;
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgNewRedDoc");
    buttonItem1.ToolTipText = LocalizationHolder.rm.GetString("NewNote");
    buttonItem1.CommandName = "RedNew";
    items1.Add((ToolbarItemBase) buttonItem1);
    DropDownMenuItem dropDownMenuItem1 = new DropDownMenuItem();
    dropDownMenuItem1.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgListView");
    dropDownMenuItem1.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllRemarks");
    dropDownMenuItem1.Tag = (object) ShowDocsMode.All;
    dropDownMenuItem1.CommandName = "ddCheckShowAll";
    dropDownMenuItem1.ShowText = true;
    MenuButtonItem menuButtonItem1 = new MenuButtonItem();
    menuButtonItem1.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgListView");
    menuButtonItem1.Text = menuButtonItem1.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllDoc");
    menuButtonItem1.Tag = (object) ShowDocsMode.All;
    menuButtonItem1.CommandName = "RedShowAll";
    menuButtonItem1.ShowText = true;
    MenuButtonItem menuButtonItem2 = new MenuButtonItem();
    menuButtonItem2.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgCopyListFromDoc");
    menuButtonItem2.Text = menuButtonItem1.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowDocWithRemarkOnly");
    menuButtonItem2.Tag = (object) ShowDocsMode.WithRemarksOnly;
    menuButtonItem2.CommandName = "RedShowWithRemarksOnly";
    menuButtonItem2.ShowText = true;
    dropDownMenuItem1.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) menuButtonItem1,
      (ToolbarItemBase) menuButtonItem2
    });
    this._toolBarTreeView.Items.Add((ToolbarItemBase) dropDownMenuItem1);
    dropDownMenuItem1.Visible = false;
    ToolbarItemBaseCollection items2 = this._toolBarTreeView.Items;
    ButtonItem buttonItem2 = new ButtonItem();
    buttonItem2.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgOutput");
    buttonItem2.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllComments");
    buttonItem2.Text = string.Empty;
    buttonItem2.Visible = dropDownMenuItem1.Checked;
    buttonItem2.CommandName = "RedComments";
    items2.Add((ToolbarItemBase) buttonItem2);
    ToolbarItemBaseCollection items3 = this._toolBarTreeView.Items;
    ButtonItem buttonItem3 = new ButtonItem();
    buttonItem3.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex("imgOutput");
    buttonItem3.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllComments");
    buttonItem3.Enabled = false;
    buttonItem3.IconSize = new Size(1, 1);
    buttonItem3.Importance = ToolBarItemImportance.Lowest;
    buttonItem3.MinimumSize = 1;
    buttonItem3.Padding.Left = 0;
    buttonItem3.Padding.Top = 0;
    buttonItem3.Padding.Right = 0;
    buttonItem3.Padding.Bottom = 0;
    buttonItem3.CommandName = "btnBlank";
    buttonItem3.Stretch = true;
    items3.Add((ToolbarItemBase) buttonItem3);
    ReportAttribute attribute1 = EStatusRemark.eAgreed.GetAttribute<ReportAttribute>();
    ToolbarItemBaseCollection items4 = this._toolBarTreeView.Items;
    ButtonItem buttonItem4 = new ButtonItem();
    buttonItem4.Tag = (object) EStatusRemark.eAgreed.GetName<EStatusRemark>();
    buttonItem4.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute1.ImgName);
    buttonItem4.ToolTipText = attribute1.TipText;
    buttonItem4.Text = string.Empty;
    buttonItem4.CommandName = "eAgreed~F";
    items4.Add((ToolbarItemBase) buttonItem4);
    ReportAttribute attribute2 = EStatusRemark.eCorrected.GetAttribute<ReportAttribute>();
    ToolbarItemBaseCollection items5 = this._toolBarTreeView.Items;
    ButtonItem buttonItem5 = new ButtonItem();
    buttonItem5.Tag = (object) EStatusRemark.eCorrected.GetName<EStatusRemark>();
    buttonItem5.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute2.ImgName);
    buttonItem5.ToolTipText = attribute2.TipText;
    buttonItem5.Text = string.Empty;
    buttonItem5.CommandName = "eCorrected~F";
    items5.Add((ToolbarItemBase) buttonItem5);
    ReportAttribute attribute3 = EStatusRemark.eInconsistent.GetAttribute<ReportAttribute>();
    ToolbarItemBaseCollection items6 = this._toolBarTreeView.Items;
    ButtonItem buttonItem6 = new ButtonItem();
    buttonItem6.Tag = (object) EStatusRemark.eInconsistent.GetName<EStatusRemark>();
    buttonItem6.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute3.ImgName);
    buttonItem6.ToolTipText = attribute3.TipText;
    buttonItem6.Text = string.Empty;
    buttonItem6.CommandName = "eInconsistent~F";
    items6.Add((ToolbarItemBase) buttonItem6);
    ReportAttribute attribute4 = EStatusRemark.eRejected.GetAttribute<ReportAttribute>();
    ToolbarItemBaseCollection items7 = this._toolBarTreeView.Items;
    ButtonItem buttonItem7 = new ButtonItem();
    buttonItem7.Tag = (object) EStatusRemark.eRejected.GetName<EStatusRemark>();
    buttonItem7.ImageIndex = RedlineToolBarPresenter._namedImageList.ImageIndex(attribute4.ImgName);
    buttonItem7.ToolTipText = attribute4.TipText;
    buttonItem7.Text = string.Empty;
    buttonItem7.CommandName = "eRejected~F";
    items7.Add((ToolbarItemBase) buttonItem7);
    if (this._commandManager == null)
      return;
    foreach (object obj1 in (CollectionBase) this._toolBarTreeView.Items)
    {
      if (obj1 is ButtonItemBase buttonItemBase1)
      {
        this._commandManager.Add(buttonItemBase1);
        if (obj1 is DropDownMenuItem dropDownMenuItem2 && dropDownMenuItem2.Items.Count > 0)
        {
          foreach (object obj2 in (CollectionBase) dropDownMenuItem2.Items)
          {
            if (obj2 is ButtonItemBase buttonItemBase)
              this._commandManager.Add(buttonItemBase);
          }
        }
      }
    }
  }

  /// <summary>Обновить статус кнопок фильтрации</summary>
  public void UpdateTreeToolbarFilterButtons(EStatusRemark filterFlags)
  {
    foreach (object obj in (CollectionBase) this._toolBarTreeView.Items)
    {
      if (obj is ButtonItem buttonItem && buttonItem.Tag is string tag)
      {
        EStatusRemark flag = tag.ToEnum<EStatusRemark>();
        buttonItem.Checked = filterFlags.HasFlag((Enum) flag);
        buttonItem.Invalidate();
      }
    }
  }

  public void ClearRoleText()
  {
    this.cbBoxRole.ComboBox.DropDownHeight = 1;
    this.cbBoxRole.ComboBox.BeginUpdate();
    this.cbBoxRole.ComboBox.Items.Clear();
    this.cbBoxRole.ComboBox.SelectedIndex = -1;
    this.cbBoxRole.ComboBox.EndUpdate();
  }

  public void SubscribeRoleComboEvents(
    EventHandler selectionChangedHandler,
    EventHandler dropDownHandler,
    EventHandler closedDropDownHandler)
  {
    if (selectionChangedHandler != null)
      this.cbBoxRole.SelectedValueChanged += selectionChangedHandler;
    if (dropDownHandler != null)
      this.cbBoxRole.ComboBox.DropDown += dropDownHandler;
    if (closedDropDownHandler == null)
      return;
    this.cbBoxRole.ComboBox.DropDownClosed += closedDropDownHandler;
  }

  public void UnSubscribeCbBoxRoleEvents(
    EventHandler selectionChangedHandler,
    EventHandler dropDownHandler,
    EventHandler closedDropDownHandler)
  {
    if (selectionChangedHandler != null)
      this.cbBoxRole.SelectedValueChanged -= selectionChangedHandler;
    if (dropDownHandler != null)
      this.cbBoxRole.ComboBox.DropDown -= dropDownHandler;
    if (closedDropDownHandler == null)
      return;
    this.cbBoxRole.ComboBox.DropDownClosed -= closedDropDownHandler;
  }

  public int GetImageIndexByName(string name)
  {
    return RedlineToolBarPresenter._namedImageList.ImageIndex(name);
  }
}
