// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailItemsView
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Selections.Implementation;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Client;

internal class MailItemsView : ObjectsViewBase
{
  private ButtonItem SettingsBtn;
  private ButtonItem BackBtn;
  private ButtonItem RejectBtn;
  private ButtonItem AcceptBtn;
  private ButtonItem NextBtn;
  private ButtonItem DelBtn;
  private ButtonItem UndelBtn;
  private ButtonItem ViewProcBtn;
  private Font _boldFont;
  private Font _boldUnderlineFont;
  public static List<int> MailNodeCategories = new List<int>();
  private bool _inboxMode;
  private bool _disposing;
  private string _caption = "?";
  private bool _viewsActivated;
  private Timer _unreadTimer;
  private long _lastObjectID;
  private long _lastObjectType;

  [DllImport("User32.dll")]
  private static extern short GetAsyncKeyState(Keys vKey);

  public MailItemsView()
  {
    this._caption = base.Caption;
    BaseHolder.NotificationService.Subscribe("MailRefresh", new NotificationEventHandler(this.MailRefreshEvent));
    this.InitializeComponent();
    this.DisableCheckedOutColumn = true;
    this.AllowEditing = false;
    this.SettingsBtn.ImageIndex = BaseHolder.NamedList.ImageIndex("wfSettings");
    MenuTemplate contextMenuTemplate = BaseHolder.Factory.ContextMenuTemplate;
    for (int index = 0; index < this._toolBar.Items.Count; ++index)
    {
      if (this._toolBar.Items[index].Tag != null)
      {
        ButtonItem buttonItem = this._toolBar.Items[index] as ButtonItem;
        string name = this._toolBar.Items[index].Tag.ToString();
        MenuTemplateNode menuTemplateNode = contextMenuTemplate[name];
        if (menuTemplateNode != null)
        {
          buttonItem.Text = menuTemplateNode.Text;
          buttonItem.ToolTipText = menuTemplateNode.Text;
          buttonItem.ImageIndex = menuTemplateNode.ImageIndex;
        }
      }
    }
    this._grid.RowMode = true;
    this.buttonHeightSet.Index = 1000;
    List<ButtonItem> buttonItemList = new List<ButtonItem>();
    buttonItemList.AddRange((IEnumerable<ButtonItem>) new ButtonItem[8]
    {
      this.SettingsBtn,
      this.BackBtn,
      this.RejectBtn,
      this.AcceptBtn,
      this.NextBtn,
      this.DelBtn,
      this.UndelBtn,
      this.ViewProcBtn
    });
    int index1 = this._filtersComboBoxItem.Index;
    foreach (ToolbarItemBase toolbarItemBase in buttonItemList)
      toolbarItemBase.Index = index1++;
    this._manualSortingSetupButtonItem.Visible = false;
    this.DisableFiltration = true;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MailItemsView));
    this.SettingsBtn = new ButtonItem();
    this.BackBtn = new ButtonItem();
    this.RejectBtn = new ButtonItem();
    this.AcceptBtn = new ButtonItem();
    this.NextBtn = new ButtonItem();
    this.DelBtn = new ButtonItem();
    this.UndelBtn = new ButtonItem();
    this.ViewProcBtn = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this.SettingsBtn,
      (ToolbarItemBase) this.BackBtn,
      (ToolbarItemBase) this.RejectBtn,
      (ToolbarItemBase) this.AcceptBtn,
      (ToolbarItemBase) this.NextBtn,
      (ToolbarItemBase) this.DelBtn,
      (ToolbarItemBase) this.UndelBtn,
      (ToolbarItemBase) this.ViewProcBtn
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.ButtonClick += new Intermech.Bars.ToolBar.ButtonClickEventHandler(this.tbViewBar_ButtonClick);
    this._toggleManualSortingButtonItem.Visible = false;
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintBackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    componentResourceManager.ApplyResources((object) this._pageViewsManager, "_pageViewsManager");
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "_gridHeaderMenuBar");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this.SettingsBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.SettingsBtn, "SettingsBtn");
    this.SettingsBtn.ImageIndex = 1;
    this.SettingsBtn.Click += new EventHandler(this.SettingsBtn_Click);
    this.BackBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BackBtn, "BackBtn");
    this.BackBtn.Tag = (object) "SendToBack";
    this.RejectBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.RejectBtn, "RejectBtn");
    this.RejectBtn.Tag = (object) "RejectWO";
    componentResourceManager.ApplyResources((object) this.AcceptBtn, "AcceptBtn");
    this.AcceptBtn.Tag = (object) "AcceptWO";
    componentResourceManager.ApplyResources((object) this.NextBtn, "NextBtn");
    this.NextBtn.Tag = (object) "SendToNext";
    this.DelBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.DelBtn, "DelBtn");
    this.DelBtn.Tag = (object) "DelMessage";
    componentResourceManager.ApplyResources((object) this.UndelBtn, "UndelBtn");
    this.UndelBtn.Tag = (object) "UndelMessage";
    this.ViewProcBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.ViewProcBtn, "ViewProcBtn");
    this.ViewProcBtn.Tag = (object) "ViewProcess";
    this.Name = nameof (MailItemsView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._gridHeaderMenuBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._pictureBox, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public override ContentType ViewContentType => ContentType.NonFolders;

  private void MailRefreshEvent(object sender, NotificationEventArgs e)
  {
    this.ReloadItems();
    if (!(e is MailRefreshWithReloadWorkOfferEventArgs workOfferEventArgs) || this._grid == null || workOfferEventArgs.ActivityIDs.Count <= 0)
      return;
    List<INodeID> nodeIDs = new List<INodeID>();
    foreach (iGRow row in (IEnumerable) this._grid.Rows)
    {
      INodeID nodeIdForRow = this.GetNodeIDForRow(row);
      if (nodeIdForRow is MailNodeID mailNodeId && workOfferEventArgs.ActivityIDs.Contains(mailNodeId.ObjectID))
      {
        nodeIDs.Add(nodeIdForRow);
        workOfferEventArgs.ActivityIDs.Remove(mailNodeId.ObjectID);
      }
    }
    this.SelectNodes(nodeIDs);
  }

  protected override void UpdateToolbar()
  {
    base.UpdateToolbar();
    if (this.RejectBtn == null)
      return;
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices);
    this.RejectBtn.Visible = commandsTable.Contains("RejectWO");
    this.AcceptBtn.Visible = commandsTable.Contains("AcceptWO");
    this.BackBtn.Visible = !this.RejectBtn.Visible;
    this.NextBtn.Visible = !this.AcceptBtn.Visible;
    this.BackBtn.Enabled = commandsTable.Contains("SendToBack");
    this.NextBtn.Enabled = commandsTable.Contains("SendToNext");
    this.DelBtn.Enabled = commandsTable.Contains("DelMessage");
    this.UndelBtn.Enabled = commandsTable.Contains("UndelMessage");
    this.ViewProcBtn.Enabled = commandsTable.Contains("ViewProcess");
  }

  private void tbViewBar_ButtonClick(object sender, ToolBarItemEventArgs e)
  {
    if (e.Item.Tag == null)
      return;
    this.ExecuteMenuCommand(e.Item.Tag.ToString());
  }

  protected override void SetPainters(HybridDictionary _painters)
  {
    base.SetPainters(_painters);
    object key1 = (object) (wfConsts.AttrPriorityID.ToString() + ".images");
    if (_painters[key1] == null)
      _painters.Add(key1, (object) new PriorityIcon());
    object key2 = (object) (wfConsts.AttrAttachmentsID.ToString() + ".images");
    if (_painters[key2] != null)
      return;
    _painters.Add(key2, (object) new AttachsIcon());
  }

  public Font BoldFont(Font proto)
  {
    if (this._boldFont != null && (proto.Name != this._boldFont.Name || (double) proto.Size != (double) this._boldFont.Size))
      this._boldFont = (Font) null;
    if (this._boldFont == null)
      this._boldFont = new Font(proto, FontStyle.Bold);
    return this._boldFont;
  }

  public Font BoldUnderlineFont(Font proto)
  {
    if (this._boldUnderlineFont != null && (proto.Name != this._boldUnderlineFont.Name || (double) proto.Size != (double) this._boldUnderlineFont.Size))
      this._boldUnderlineFont = (Font) null;
    if (this._boldUnderlineFont == null)
      this._boldUnderlineFont = new Font(proto, FontStyle.Bold | FontStyle.Underline);
    return this._boldUnderlineFont;
  }

  private INodeID GetNodeID(int RowIndex, int ColIndex) => this.GetNodeIDForRow(RowIndex);

  protected override void GridDynamicFont(object sender, iGDynamicFontEventArgs e)
  {
    base.GridDynamicFont(sender, e);
    INodeID nodeId = this.GetNodeID(e.RowIndex, e.ColIndex);
    if (nodeId == null || !(nodeId is MailNodeID) || !this._inboxMode || ((MailNodeID) nodeId).RecipStatus != RecipStatus.Unread)
      return;
    if (e.Font.Underline)
      e.Font = this.BoldUnderlineFont(e.Font);
    else
      e.Font = this.BoldFont(e.Font);
  }

  protected override void GridDynamicForeColor(object sender, iGDynamicColorEventArgs e)
  {
    base.GridDynamicForeColor(sender, e);
    INodeID nodeId = this.GetNodeID(e.RowIndex, e.ColIndex);
    if (nodeId == null || !(nodeId is MailNodeID))
      return;
    if (this._inboxMode && ((MailNodeID) nodeId).CompletedTerm != DateTime.MinValue)
    {
      if (DateTime.Now > ((MailNodeID) nodeId).CompletedTerm)
        e.Color = Color.Red;
      else
        e.Color = Color.Navy;
    }
    else
    {
      if (((MailNodeID) nodeId).ActivityStatus != ActivityStatus.Terminated)
        return;
      e.Color = SystemColors.GrayText;
    }
  }

  public static int NodeCategoryID(ISelectedItems items, System.IServiceProvider services = null)
  {
    if (wfFunx.OrganizerContext(services) != null)
      return Intermech.Navigator.Consts.CategoryMailInbox;
    INodeID itemId = items.GetItemID(0);
    if (MailItemsView.MailNodeCategories.Contains(itemId.CategoryID))
      return itemId.CategoryID;
    NodeIDPath parentPath = items.GetParentPath(0);
    if (parentPath != null)
    {
      if (parentPath.RootDescriptor is ActivitiesDescriptor)
        return Intermech.Navigator.Consts.CategoryMail;
      for (int Index = parentPath.Length - 1; Index >= 0; --Index)
      {
        INodeID nodeId = parentPath[Index];
        if (!(nodeId is SelectionNodeID) && nodeId.TypeID != Intermech.Navigator.Selections.Consts.SelectionTypeID)
          return nodeId.CategoryID;
      }
    }
    return 0;
  }

  public static bool IsMailNode(ISelectedItems items)
  {
    int num = MailItemsView.NodeCategoryID(items);
    return MailItemsView.MailNodeCategories.Contains(num);
  }

  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
    this._inboxMode = MailItemsView.NodeCategoryID(items) == Intermech.Navigator.Consts.CategoryMailInbox;
    if (this._inboxMode)
    {
      ISelectedItemsHost selectedItemsHost = (ISelectedItemsHost) this;
      if (selectedItemsHost != null)
        selectedItemsHost.SelectedItemsChanged += new EventHandler(this.OnSelectedItemsChanged);
    }
    NavigatorTreeView service = services.GetService(typeof (NavigatorTreeView)) as NavigatorTreeView;
    Intermech.Workflow.Design.Holder.LastMailTree = service;
    if (service != null && service.FocusedNode != null)
    {
      string displayText = service.FocusedNode.GetDisplayText(0);
      this._caption = displayText.Contains("Входящие") ? "Входящие" : displayText;
    }
    this.ShowEmbeddedViewsIfNeeded();
  }

  private void ShowEmbeddedViewsIfNeeded()
  {
    if (MailSettings.Cfg.ShowTabs)
      this.OpenEmbeddedViews();
    else
      this.CloseEmbeddedViews();
  }

  private void SaveEmbeddedViewsIfNeeded()
  {
    bool flag = !this._disposing ? this != null && ((IEmbeddedViews) this).IsOpen : MailSettings.Cfg.ShowTabs;
    if (MailSettings.Cfg.ShowTabs == flag && MailSettings.Cfg.MailTabsHeight == this.EMVAbsHeight)
      return;
    MailSettings.Cfg.ShowTabs = flag;
    MailSettings.Cfg.MailTabsHeight = this.EMVAbsHeight;
    MailSettings.Cfg.Save();
  }

  public override void OpenEmbeddedViews(int height)
  {
    height = MailSettings.Cfg.MailTabsHeight;
    base.OpenEmbeddedViews(height);
    this.SaveEmbeddedViewsIfNeeded();
  }

  public override void CloseEmbeddedViews()
  {
    base.CloseEmbeddedViews();
    this.SaveEmbeddedViewsIfNeeded();
  }

  protected override void Dispose(bool disposing)
  {
    BaseHolder.NotificationService.Unsubscribe("MailRefresh", new NotificationEventHandler(this.MailRefreshEvent));
    if (!disposing)
      return;
    this._disposing = true;
    base.Dispose(disposing);
  }

  public override string Caption => this._caption;

  public override void Deactivate(IView nextView)
  {
    Intermech.Workflow.Design.Holder.IsInboxActive = false;
    if (this._unreadTimer != null)
    {
      this._unreadTimer.Dispose();
      this._unreadTimer = (Timer) null;
    }
    base.Deactivate(nextView);
  }

  public override void Activate(IView previousView)
  {
    Intermech.Workflow.Design.Holder.IsInboxActive = this._inboxMode;
    base.Activate(previousView);
    if (this._viewsActivated)
      return;
    this._viewsActivated = true;
  }

  private void RestartUnreadTimer()
  {
    if (MailSettings.Cfg.MarkReadInterval > 0)
    {
      if (this._unreadTimer == null)
      {
        this._unreadTimer = new Timer();
        this._unreadTimer.Interval = MailSettings.Cfg.MarkReadInterval * 1000;
        this._unreadTimer.Tick += new EventHandler(this.unreadTimer_Tick);
      }
      this._unreadTimer.Stop();
      this._unreadTimer.Start();
    }
    else
      this.unreadTimer_Tick((object) null, (EventArgs) null);
  }

  private void unreadTimer_Tick(object sender, EventArgs e)
  {
    if (this.SelectedItems.Count <= 0)
      return;
    if (this._unreadTimer != null)
      this._unreadTimer.Stop();
    MailNodeID itemId = this.SelectedItems.GetItemID(0) as MailNodeID;
    if (!this.InboxMode || itemId == null || itemId.RecipStatus != RecipStatus.Unread)
      return;
    if (ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service)
    {
      int count = 0;
      service.BeginUpdate();
      try
      {
        itemId.RecipStatus = RecipStatus.Read;
        count = -1;
      }
      finally
      {
        service.EndUpdate(count);
      }
    }
    this.Refresh();
  }

  private void OnSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this.SelectedItems.Count <= 0 || !(this.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData) || this._lastObjectID == itemData.ObjectID && this._lastObjectType == (long) itemData.ObjectType)
      return;
    this._lastObjectID = itemData.ObjectID;
    this._lastObjectType = (long) itemData.ObjectType;
    if ((this.SelectedItems.GetItemID(0) as MailNodeID).RecipStatus == RecipStatus.Unread)
    {
      this.RestartUnreadTimer();
    }
    else
    {
      if (this._unreadTimer == null)
        return;
      this._unreadTimer.Stop();
    }
  }

  private void SettingsBtn_Click(object sender, EventArgs e)
  {
    if (!MailSettingsForm.EditSettings())
      return;
    this.ShowEmbeddedViewsIfNeeded();
  }

  protected override void CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    base.CustomDrawCellForeground(sender, e);
    if (this._dataAdapter == null)
      return;
    if (e.RowIndex >= 0)
    {
      iGRow row = this._grid.Rows[e.RowIndex];
    }
    INodeID nodeId = this.GetNodeID(e.RowIndex, e.ColIndex);
    if (!(this._grid.Cols[e.ColIndex].Key == "Special_StateImage") || nodeId == null || !(nodeId is MailNodeID) || (!this._inboxMode || ((MailNodeID) nodeId).SenderStatus != SenderStatus.Rejected) && (this._inboxMode || ((MailNodeID) nodeId).RecipStatus != RecipStatus.Rejected))
      return;
    ImageList imageList = BaseHolder.NamedList.ImageList;
    Graphics graphics = e.Graphics;
    Rectangle bounds = e.Bounds;
    int x = bounds.Left + 7;
    bounds = e.Bounds;
    int y = bounds.Top + 1;
    int taskRejectedIndex = Intermech.Workflow.Design.Holder.TaskRejectedIndex;
    imageList.Draw(graphics, x, y, taskRejectedIndex);
  }

  protected override void GridSetColumns(NodeColumnCollection columns, bool reloadData)
  {
    NodeColumn nodeColumn1 = columns.Find((object) wfConsts.AttrAttachmentsID);
    NodeColumn nodeColumn2 = columns.Find((object) wfConsts.AttrPriorityID);
    if (nodeColumn2 != null)
      nodeColumn2.TransformationMode = CellTransformationMode.WithoutTransformation;
    base.GridSetColumns(columns, reloadData);
    this._grid.Header.ImageList = BaseHolder.NamedList.ImageList;
    foreach (iGCol col in (IEnumerable) this._grid.Cols)
    {
      if (col.Tag is NodeColumn tag && (tag == nodeColumn2 || tag == nodeColumn1))
      {
        col.ImageIndex = tag != nodeColumn2 ? Intermech.Workflow.Design.Holder.AttachsImageIndex : Intermech.Workflow.Design.Holder.HighImageIndex;
        col.Text = (object) "";
        col.Width = 23;
      }
    }
    iGCol col1 = this._grid.Cols["Special_StateImage"];
    if (col1 == null)
      return;
    col1.CellStyle.CustomDrawFlags |= iGCustomDrawFlags.Foreground;
  }

  internal void SaveEmbeddedViewsData() => this._pageViewsManager.SaveChanges();

  internal PageViewsManager GetViewsManager() => this._pageViewsManager;

  public bool InboxMode => this._inboxMode;

  protected override int StateStreamCategoryID
  {
    get
    {
      int length = this._path.Length;
      if (length > 0 && this._path[length - 1] is SelectionNodeID)
      {
        for (int Index = length - 1; Index >= 0; --Index)
        {
          INodeID nodeId = this._path[Index];
          if (!(nodeId is SelectionNodeID) && nodeId.TypeID != Intermech.Navigator.Selections.Consts.SelectionTypeID)
            return nodeId.CategoryID;
        }
      }
      return base.StateStreamCategoryID;
    }
  }

  protected override int StateStreamCategoryType => 0;

  public override bool DisablePacketsReading
  {
    get => true;
    set => base.DisablePacketsReading = value;
  }
}
