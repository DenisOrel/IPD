// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AttachmentsView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Workflow.Design;

public class AttachmentsView : ObjectsViewBase
{
  public bool UseCompatibleStateImageBehavior;
  public View View;
  public bool OwnerDraw;
  private DropDownMenuItem AttachBtn;
  private ButtonItem DetachBtn;
  private MenuButtonItem AttachMI;
  private MenuButtonItem AttachFileMI;
  public object SubitemImages;
  private HashSet<string> _addCommands = new HashSet<string>();
  private int _activateCounter;
  protected AttachmentList _attachments;
  /// <summary>Отфильтованные типы вложений</summary>
  private string _filteredTypes = string.Empty;
  private WarningControl _HiddenWarningControl;
  /// <summary>
  /// Дополнительные условия, которые могут использоваться при загрузке списка в Load
  /// </summary>
  protected ConditionStructure[] Conditions;
  protected int RelationTypeID = wfConsts.AttachmentRelationTypeID;
  private bool _readOnly;
  private bool _canAttach = true;
  private bool _canDetach = true;
  private long _objectID;
  private long _processID;
  private bool _allowedTypesLoaded;
  private AllowedTypes _allowedTypes;
  protected bool _modified;

  public event EventHandler ItemsChanged;

  public override ContentType ViewContentType => ContentType.Folders;

  public AttachmentsView()
  {
    this.InitializeComponent();
    this._toggleManualSortingButtonItem.Visible = false;
    this.DisableFiltration = true;
    this.DisablePacketsReading = true;
    string[] strArray = new string[5]
    {
      "ViewDocument",
      "SignUp",
      "CryptoSignUp",
      "ParametersCard",
      "OpenInNewWindow"
    };
    int index1 = this.DetachBtn.Index + 1;
    bool flag = false;
    foreach (string str in strArray)
    {
      ButtonItem buttonItem = new ButtonItem();
      buttonItem.Tag = (object) str;
      buttonItem.CommandName = str;
      buttonItem.ShowText = true;
      this._addCommands.Add(str);
      if (!flag)
      {
        buttonItem.BeginGroup = true;
        flag = true;
      }
      this._toolBar.Items.Insert(index1, (ToolbarItemBase) buttonItem);
      ++index1;
    }
    if (BaseHolder.Factory != null)
    {
      MenuTemplate contextMenuTemplate = BaseHolder.Factory.ContextMenuTemplate;
      for (int index2 = 0; index2 < this._toolBar.Items.Count; ++index2)
      {
        if (this._toolBar.Items[index2].Tag != null && this._toolBar.Items[index2] is ButtonItemBase buttonItemBase)
        {
          string name = this._toolBar.Items[index2].Tag.ToString();
          MenuTemplateNode menuTemplateNode = contextMenuTemplate[name];
          if (menuTemplateNode != null)
          {
            buttonItemBase.Text = menuTemplateNode.Text;
            buttonItemBase.ImageIndex = menuTemplateNode.ImageIndex;
          }
        }
      }
    }
    this.buttonHeightSet.Index = 1000;
    this.SelectedItemsChanged += new EventHandler(this.AttachmentsView_SelectedItemsChanged);
    this._services.AddService(typeof (AttachmentsView), (object) this);
    if (BaseHolder.NotificationService != null)
      this._services.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    this._services.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
  }

  public override void Activate(IView previousView)
  {
    ++this._activateCounter;
    base.Activate(previousView);
  }

  public override void Deactivate(IView nextView)
  {
    --this._activateCounter;
    base.Deactivate(nextView);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._activateCounter > 0)
      this.Deactivate((IView) null);
    base.Dispose(disposing);
  }

  protected virtual void UpdateCommands()
  {
    this.DetachBtn.Enabled = this.CanDetach && this.SelectedItems.Count > 0;
  }

  private void AttachmentsView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this.UpdateCommands();
  }

  protected virtual AttachmentList NewAttachmentList() => new AttachmentList();

  [NotNull]
  public AttachmentList Attachments
  {
    get
    {
      if (this._attachments == null)
      {
        this._attachments = this.NewAttachmentList();
        this._attachments.RelationTypeID = this.RelationTypeID;
        this._attachments.Conditions = this.Conditions;
        this._attachments.OnSaveAttachment += this.OnSaveAttachment;
      }
      return this._attachments;
    }
  }

  private void FireItemsChanged()
  {
    EventHandler itemsChanged = this.ItemsChanged;
    if (itemsChanged == null)
      return;
    itemsChanged((object) this, (EventArgs) null);
  }

  /// <summary>
  /// Исключает вложения, типы которых запрещены для вложения в текущем процессе
  /// </summary>
  /// <param name="attachs"></param>
  private void CheckAttachments(AttachmentList attachs, bool showMessage = true)
  {
    if (this.AllowedTypes == null)
      return;
    List<int> attTypes = new List<int>();
    foreach (Attachment attach in (List<Attachment>) attachs)
    {
      if (!attTypes.Contains(attach.TypeID))
        attTypes.Add(attach.TypeID);
    }
    this.AllowedTypes.Filter(attTypes);
    List<int> intList = new List<int>();
    for (int index = attachs.Count - 1; index >= 0; --index)
    {
      int typeId = attachs[index].TypeID;
      if (!attTypes.Contains(typeId))
      {
        attachs.RemoveAt(index);
        if (!intList.Contains(typeId))
          intList.Add(typeId);
      }
    }
    this._filteredTypes = "";
    foreach (int objTypeID in intList)
    {
      if (this._filteredTypes != "")
        this._filteredTypes += ", ";
      this._filteredTypes += MetaDataHelper.GetObjectTypeName(objTypeID);
    }
    if (!showMessage)
      return;
    this.ShowFilteredMessage();
  }

  protected void ShowFilteredMessage()
  {
    if (!(this._filteredTypes != ""))
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_21763.ssp_workflow_21764()) + this._filteredTypes, LocalizationHolder.rm.GetString("Workflow.Design_103"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary>
  /// Загружает вложения из списка. Внимание, изменяет список при редактировании!
  /// </summary>
  public void Load(AttachmentList attachs, IView previousView)
  {
    this.CheckAttachments(attachs, false);
    this._attachments = attachs;
    this.SetModified(false);
    Dictionary<int, List<long>> objectIDs = new Dictionary<int, List<long>>();
    foreach (Attachment attachment in (List<Attachment>) this._attachments)
    {
      int parentObjectTypeId = MetaDataHelper.GetTopParentObjectTypeID(attachment.TypeID);
      List<long> longList = (List<long>) null;
      if (!objectIDs.TryGetValue(parentObjectTypeId, out longList))
      {
        longList = new List<long>();
        objectIDs.Add(parentObjectTypeId, longList);
      }
      longList.Add(attachment.ObjectID);
    }
    this.Initialize((IDescriptor) new AttachmentsDescriptor(objectIDs), (System.IServiceProvider) this._services);
    this.Activate(previousView);
    this.UpdateCommands();
    this.FireItemsChanged();
    this.Loaded();
    this.ShowFilteredMessage();
  }

  private bool ShowHiddenWarning
  {
    get => this._HiddenWarningControl != null;
    set
    {
      if (value)
      {
        if (this._HiddenWarningControl != null)
          return;
        this._HiddenWarningControl = WarningControl.Show((System.Windows.Forms.Control) this, LocalizationHolder.rm.GetString("HiddenAttachsWarning"));
      }
      else
      {
        if (this._HiddenWarningControl == null)
          return;
        this._HiddenWarningControl.Dispose();
        this._HiddenWarningControl = (WarningControl) null;
      }
    }
  }

  protected virtual void Loaded() => this.ShowHiddenWarning = this._attachments.HasInvisibleItems;

  /// <summary>Даст ист конец</summary>
  protected override bool Eof => true;

  /// <summary>
  /// Загружает вложения из списка. Внимание, изменяет список при редактировании!
  /// </summary>
  public void Load(AttachmentList attachs) => this.Load(attachs, (IView) null);

  public void Load(IDBObject obj) => this.Load(obj, (IView) null);

  protected event SaveAttachmentHandler OnSaveAttachment;

  public void Load(IDBObject obj, IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      previousView = (IView) null;
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrProcessID);
    if (attributeById != null)
      this.ProcessID = attributeById.AsInteger;
    this.Attachments.Load(obj);
    this.Load(this.Attachments, previousView);
  }

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      if (this._readOnly == value)
        return;
      this._readOnly = value;
      this.CanAttach = !value;
      this.CanDetach = !value;
    }
  }

  public bool CanAttach
  {
    get => this._canAttach;
    set
    {
      this._canAttach = value;
      this.AttachBtn.Enabled = value;
    }
  }

  public bool CanDetach
  {
    get => this._canDetach;
    set
    {
      this._canDetach = value;
      this.DetachBtn.Enabled = value;
    }
  }

  public void Init(IDBObject obj)
  {
    this._objectID = obj.ObjectID;
    IDBAttribute attributeById = obj.GetAttributeByID(wfConsts.AttrAddIDID);
    int num = !obj.Session.IsAdmin ? 0 : (MiscFunx.IsFlagSet(attributeById, ActivityFlags.AllowAdminAttach) ? 1 : 0);
    bool flag1 = obj.Session.IsSystemSession && MiscFunx.IsFlagSet(attributeById, ActivityFlags.AllowSystemAttach);
    bool flag2 = (num | (flag1 ? 1 : 0)) != 0 || !MiscFunx.IsFlagSet(attributeById, ActivityFlags.DenyAttach);
    bool flag3 = (num | (flag1 ? 1 : 0)) != 0 || !MiscFunx.IsFlagSet(attributeById, ActivityFlags.DenyDetach);
    this.CanAttach = !this._readOnly & flag2;
    this.CanDetach = !this._readOnly & flag3;
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AttachmentsView));
    this.AttachBtn = new DropDownMenuItem();
    this.AttachMI = new MenuButtonItem();
    this.AttachFileMI = new MenuButtonItem();
    this.DetachBtn = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.AttachBtn,
      (ToolbarItemBase) this.DetachBtn
    });
    componentResourceManager.ApplyResources((object) this._toolBar, "_toolBar");
    this._toolBar.ButtonClick += new Intermech.Bars.ToolBar.ButtonClickEventHandler(this._toolBar_ButtonClick);
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
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
    this.AttachBtn.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.AttachBtn, "AttachBtn");
    this.AttachBtn.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.AttachMI,
      (ToolbarItemBase) this.AttachFileMI
    });
    this.AttachBtn.ShowText = true;
    this.AttachBtn.Tag = (object) "wfAttach";
    this.AttachBtn.Click += new EventHandler(this.AttachBtn_Click);
    componentResourceManager.ApplyResources((object) this.AttachMI, "AttachMI");
    this.AttachMI.ShowText = true;
    this.AttachMI.Tag = (object) "";
    this.AttachMI.Click += new EventHandler(this.AttachBtn_Click);
    this.AttachFileMI.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.AttachFileMI, "AttachFileMI");
    this.AttachFileMI.ShowText = true;
    this.AttachFileMI.Tag = (object) "";
    this.AttachFileMI.Click += new EventHandler(this.AttachFIleBtn_Click);
    componentResourceManager.ApplyResources((object) this.DetachBtn, "DetachBtn");
    this.DetachBtn.ShowText = true;
    this.DetachBtn.Tag = (object) "wfDetach";
    this.DetachBtn.Click += new EventHandler(this.DetachBtn_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (AttachmentsView);
    this.Tag = (object) " ";
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._gridHeaderMenuBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._pictureBox, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._toolBar, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this._grid, 0);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public void Save(IDBObject obj) => this.Attachments.Save(obj);

  protected virtual void SaveChanges()
  {
    if (this._objectID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._objectID, false);
      if (dbObject == null)
        return;
      this.Save(dbObject);
    }
  }

  public virtual int ObjectType => wfConsts.ActivitiesTypeID;

  /// <summary>
  /// Идентификатор шаблона/процесса, для которого показывается список вложений. Используется для контроля разрешенных типов вложений.
  /// </summary>
  public long ProcessID
  {
    get => this._processID;
    set
    {
      if (this._processID == value)
        return;
      this._processID = value;
      this._allowedTypesLoaded = false;
    }
  }

  private AllowedTypes AllowedTypes
  {
    get
    {
      if (!this._allowedTypesLoaded)
      {
        this._allowedTypes = this.ProcessID == 0L ? (AllowedTypes) null : new AllowedTypes(this.ProcessID);
        this._allowedTypesLoaded = true;
      }
      return this._allowedTypes;
    }
  }

  public void DoAttach()
  {
    AttachmentList src = wfFunx.BrowseForAttachments(this.ObjectType, this.RelationTypeID, this.AllowedTypes);
    if (src == null)
      return;
    this.Attachments.AddList(src, false);
    this.SaveChanges();
    this.Load(this.Attachments);
    this.SetModified(true);
  }

  public void DoDetach(ISelectedItems items)
  {
    if (this._attachments == null)
      return;
    for (int index = 0; index < items.Count; ++index)
      this._attachments.Remove((items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID);
    this.SaveChanges();
    this.Load(this._attachments);
    this.SetModified(true);
  }

  public bool Modified => this._modified;

  protected virtual void SetModified(bool value) => this._modified = value;

  protected override ICommandsProvider GetCommandsProvider()
  {
    return (ICommandsProvider) new AttachmentsView.AttachmentsViewCommandsProvider(this);
  }

  protected void AttachCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.DoAttach();
  }

  private void AttachFIleBtn_Click(object sender, EventArgs e)
  {
    this.AttachFileCommand((ISelectedItems) null, (System.IServiceProvider) null, (object) null);
  }

  protected void AttachFileCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    List<long> longList = wfFunx.AddFileToObject(wfConsts.FileTypeID, 0L);
    foreach (long num in longList)
      this.Attachments.Add(new Attachment()
      {
        TypeID = wfConsts.FileTypeID,
        ObjectID = num
      });
    if (longList.Count <= 0)
      return;
    this.SaveChanges();
    this.Load(this.Attachments);
    this.SetModified(true);
  }

  protected void DetachCommand(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    this.DoDetach(items);
  }

  private void AttachBtn_Click(object sender, EventArgs e) => this.DoAttach();

  private void DetachBtn_Click(object sender, EventArgs e) => this.DoDetach(this.SelectedItems);

  private void SwapAttachments(IList<long> oldIDs, IList<long> newIDs)
  {
    if (this._attachments == null)
      return;
    bool flag = false;
    for (int index1 = 0; index1 < this._attachments.Count; ++index1)
    {
      int index2 = oldIDs.IndexOf(this._attachments[index1].ObjectID);
      if (index2 != -1)
      {
        if (newIDs != null)
          this._attachments[index1].ObjectID = newIDs[index2];
        else if (this._attachments[index1].ObjectID < 0L)
          this._attachments[index1].ObjectID = -this._attachments[index1].ObjectID;
        flag = true;
      }
    }
    if (!flag)
      return;
    iFocusAndSelection focusAndSelection = this.GridGetFocusAndSelection();
    try
    {
      this.GridSaveState((Stream) null);
      this.SaveChanges();
      this.Load(this._attachments);
    }
    finally
    {
      this.GridSetFocusAndSelection(focusAndSelection, true);
    }
  }

  protected override void NotificationEventFired(object sender, NotificationEventArgs e)
  {
    base.NotificationEventFired(sender, e);
    switch (e)
    {
      case DBObjectsCheckOutEventArgs _ when e.EventName == "ObjectsCheckedOut":
        DBObjectsCheckOutEventArgs checkOutEventArgs = (DBObjectsCheckOutEventArgs) e;
        this.SwapAttachments(checkOutEventArgs.ObjectIDs, checkOutEventArgs.NewObjectIDs);
        break;
      case DBObjectsEventArgs _ when e.EventName == "ObjectsCheckedIn" || e.EventName == "ObjectsChangesCancelled":
        this.SwapAttachments(((DBObjectsEventArgs) e).ObjectIDs, (IList<long>) null);
        break;
    }
  }

  protected override void Grid_DragEnter(object sender, DragEventArgs e)
  {
    base.Grid_DragEnter(sender, e);
    if (this.CanAttach)
      return;
    e.Effect = DragDropEffects.None;
  }

  protected override void Grid_DragDrop(object sender, DragEventArgs e)
  {
    if (!this.CanAttach)
    {
      e.Effect = DragDropEffects.None;
    }
    else
    {
      if (!(e.Data.GetData(typeof (IOSource)) is IOSource data))
        return;
      ISelectedItems selectedItems = data.SelectedItems;
      List<IDBTypedObjectID> dbTypedObjectIdList = new List<IDBTypedObjectID>();
      for (int index = 0; index < selectedItems.Count; ++index)
      {
        IDBTypedObjectID itemData = selectedItems.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
        dbTypedObjectIdList.Add(itemData);
      }
      AttachmentList attachmentList = new AttachmentList();
      wfFunx.CopyIDBTypedToAttachments(dbTypedObjectIdList.ToArray(), attachmentList);
      this.CheckAttachments(attachmentList);
      this.Attachments.AddList(attachmentList);
      this.SaveChanges();
      this.Load(this.Attachments);
    }
  }

  protected override void UpdateToolbar()
  {
    base.UpdateToolbar();
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(this.SelectedItems, (System.IServiceProvider) viewServices);
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this._toolBar.Items)
    {
      if (toolbarItemBase != null && this._addCommands.Contains(toolbarItemBase.CommandName))
        toolbarItemBase.Enabled = commandsTable.Contains(toolbarItemBase.CommandName);
    }
  }

  private void _toolBar_ButtonClick(object sender, ToolBarItemEventArgs e)
  {
    if (!this._addCommands.Contains(e.Item.CommandName))
      return;
    this.ExecuteMenuCommand(e.Item.CommandName);
  }

  private sealed class AttachmentsViewCommandsProvider : ICommandsProvider
  {
    private AttachmentsView _attachmentsView;
    private ChildrenViewCommandsProvider _childrenViewCommandsProvider;

    public AttachmentsViewCommandsProvider(AttachmentsView attachmentsView)
    {
      this._attachmentsView = attachmentsView != null ? attachmentsView : throw new ArgumentNullException(nameof (attachmentsView));
      this._childrenViewCommandsProvider = new ChildrenViewCommandsProvider((ChildrenView) attachmentsView);
    }

    public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
    {
      return this._childrenViewCommandsProvider.GetMergedCommands(items, viewServices);
    }

    public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
    {
      CommandsInfo groupCommands = this._childrenViewCommandsProvider.GetGroupCommands(items, viewServices);
      if (this._attachmentsView.CanAttach)
      {
        groupCommands.Add("wfAttach", new CommandInfo(0, new ClickEventHandler(this._attachmentsView.AttachCommand)));
        groupCommands.Add("wfAttachFile", new CommandInfo(0, new ClickEventHandler(this._attachmentsView.AttachFileCommand)));
      }
      this._attachmentsView.DetachBtn.Enabled = this._attachmentsView.CanDetach && items.Count > 0 && items.GetItemData(0, typeof (IDBObjectID)) != null;
      if (this._attachmentsView.DetachBtn.Enabled)
        groupCommands.Add("wfDetach", new CommandInfo(0, new ClickEventHandler(this._attachmentsView.DetachCommand)));
      return groupCommands;
    }
  }
}
