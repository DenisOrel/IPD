
// Type: Intermech.PropertyEditors.CustomFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DatabaseConfigurator;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Localization;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Security;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class CustomFolder : DBPropDescriptorHolder, IFolder, ISecurityCallback
{
  protected Guid instGuid = Guid.Empty;
  public TreeNode node;
  public TreeNode nodeParent;
  public DataTable dataTable;
  private bool markSaveCallbackDone;
  private LocalizationForm localizationForm;
  protected MenuButtonItem activeMenuItem;
  protected MenuButtonItem miAdd;
  protected MenuButtonItem miExclude;
  protected MenuButtonItem miDelete;
  protected MenuButtonItem miUpdate;
  protected MenuButtonItem miFind;
  protected MenuButtonItem miCopy;
  protected MenuButtonItem miCut;
  protected MenuButtonItem miPaste;
  protected MenuButtonItem miClone;
  protected MenuButtonItem miExportImage;
  protected MenuButtonItem miLocalization;
  protected MenuButtonItem miSetSystemGuid;
  protected MenuButtonItem miOpenInNewWindow;
  protected int folderType;
  protected string textValue = string.Empty;
  protected bool isVirtualFolder;
  private bool inChange;
  private Panel placePanel;

  public MenuButtonItem MIAdd => this.miAdd;

  public Panel PlacePanel => this.placePanel;

  public CustomFolder(Guid aInstGuid, string aText, object aNodeParent, object aId)
    : this(aInstGuid, aText, aNodeParent, aId, false)
  {
  }

  public CustomFolder(Guid aInstGuid, string aText, object aNodeParent, object aId, bool isNew)
    : base(aId)
  {
    this.instGuid = aInstGuid;
    switch (aNodeParent)
    {
      case TreeNode _:
        this.nodeParent = (TreeNode) aNodeParent;
        if (!((CustomFolder) this.nodeParent.Tag).AddChildEnabled)
          throw new Exception(sc_2407.ssp_imclient_2408());
        if (this.nodeParent.Nodes.Count == 1 && this.nodeParent.Nodes[0].Text == ClientConsts.FakeNodeString)
          this.nodeParent.Nodes.Clear();
        this.node = new TreeNode(aText);
        this.node.Tag = (object) this;
        this.nodeParent.Nodes.Add(this.node);
        break;
      case TreeView _:
        this.node = new TreeNode(aText);
        this.node.Tag = (object) this;
        (aNodeParent as TreeView).Nodes.Add(this.node);
        break;
      default:
        throw new Exception(sc_2407.ssp_imclient_2409());
    }
    this.textValue = aText;
    if (isNew && this.NeedApply)
    {
      this.inChange = true;
      this.isVirtualFolder = true;
    }
    if (isNew)
      return;
    this.UpdateHasChildStatus();
  }

  public void UpdateHasChildStatus()
  {
    if (!this.AddChildEnabled)
      return;
    this.node.Nodes.Add(new TreeNode(ClientConsts.FakeNodeString));
  }

  /// <summary>
  /// Добавляем в таблицу допустимых значений штамп времени последней модификации метаданных, содержащихся в клиентском кэше N1527508
  /// </summary>
  /// <param name="possibleValuesDT"></param>
  /// <returns></returns>
  protected DataTable StoreClientCacheTimestamp(DataTable possibleValuesDT)
  {
    if (possibleValuesDT != null)
    {
      DataRow[] dataRowArray = (ServicesManager.GetService(typeof (IClientCache)) as IClientCache).CacheDataSet.Tables["IMS_METADATA"].Select("F_TABLE_NAME='IMS_POSSIBLE_VALUES'");
      if (dataRowArray != null && dataRowArray.Length != 0)
        possibleValuesDT.ExtendedProperties[(object) "modify_date"] = dataRowArray[0]["F_MODIFY_DATE"];
    }
    return possibleValuesDT;
  }

  public virtual object GetServerObject(IUserSession session) => (object) null;

  public virtual void LoadDataTable(bool reload) => this.dataTable = (DataTable) null;

  public virtual void ConstructPages(TabControl tabControl)
  {
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage);
  }

  public TreeNode Node => this.node;

  public TreeNode NodeParent => this.nodeParent;

  public virtual bool DelEnabled => false;

  public virtual bool ExcludeEnabled => false;

  public virtual bool AddChildEnabled => true;

  public virtual bool NeedApply => false;

  public virtual bool NeedSave => false;

  public virtual bool NeedPageSave => false;

  public Guid InstGuid => this.instGuid;

  public virtual int ObjectTypeProcessing => 0;

  public int FolderType => this.folderType;

  public string Text => this.textValue;

  public virtual bool CanAddChild => true;

  public virtual bool CanExclude => true;

  public virtual bool CanPaste => true;

  public IDatabaseConfiguratorControl IDatabaseConfiguratorControl
  {
    get
    {
      IDatabaseConfiguratorControl configuratorControl = (IDatabaseConfiguratorControl) null;
      if (this.node != null && this.node.TreeView != null && this.node.TreeView.Parent != null)
      {
        for (Control parent = this.node.TreeView.Parent; parent != null; parent = parent.Parent)
        {
          if (parent is IDatabaseConfiguratorControl)
          {
            configuratorControl = parent as IDatabaseConfiguratorControl;
            break;
          }
        }
      }
      return configuratorControl;
    }
  }

  public IFolder AddChild(MenuButtonItem mi)
  {
    if (!this.AddChildEnabled)
      throw new Exception(LocalizationHolder.rm.GetString(sc_2407.ssp_imclient_2410()));
    if (!this.CanAddChild)
      throw new Exception(LocalizationHolder.rm.GetString(sc_2407.ssp_imclient_2411()));
    this.activeMenuItem = mi;
    return this.AddChildCallback();
  }

  public IFolder AddChildDubbed(IFolder ifolder)
  {
    if (!this.AddChildEnabled)
      throw new Exception(LocalizationHolder.rm.GetString(sc_2407.ssp_imclient_2412()));
    if (!this.CanAddChild)
      throw new Exception(LocalizationHolder.rm.GetString(sc_2407.ssp_imclient_2413()));
    return this.AddChildDubbedCallback(ifolder);
  }

  public virtual bool CanDelete => true;

  public virtual bool CopyEnabled => false;

  public virtual bool CutEnabled => false;

  public virtual bool ExportImageEnabled => Statics.IconSrv != null && this.node.ImageIndex > 0;

  public virtual bool PasteEnabled => false;

  public virtual bool CloneEnabled => false;

  public bool Exclude()
  {
    bool flag = false;
    if (this.ExcludeEnabled && this.CanExclude && this.ExcludeCallbackBefore())
    {
      bool needNodeRemove = true;
      flag = this.ExcludeCallback(ref needNodeRemove);
      if (flag & needNodeRemove)
        this.node.Remove();
    }
    return flag;
  }

  public ActionResult Delete(EventHandler postDeleteHandler)
  {
    ActionResult actionResult = ActionResult.OK;
    long deleteMode = 0;
    if (this.DelEnabled && this.CanDelete)
    {
      if (this.DeleteCallbackBefore(ref deleteMode))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (this.GetServerObject(sessionKeeper.Session) is IDeletable serverObject)
          {
            try
            {
              serverObject.Delete(deleteMode);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
              return ActionResult.Failure;
            }
          }
          this.DeleteCallback();
          if (postDeleteHandler != null)
            postDeleteHandler((object) this, new EventArgs());
          this.node.Remove();
        }
      }
      else
        actionResult = ActionResult.Cancel;
    }
    return actionResult;
  }

  public virtual bool ExcludeCallbackBefore()
  {
    return IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(MessageDialogs.msgReallyExclude0, (object) this.textValue), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes;
  }

  public virtual bool DeleteCallbackBefore(ref long deleteMode)
  {
    return IMMessageBox.Show(MessageDialogs.msgConfirmDelete, string.Format(MessageDialogs.msgReallyDelete0, (object) this.textValue), MessageBoxButtons.YesNo, IMMessageBoxImage.Question) == DialogResult.Yes;
  }

  public virtual bool DeleteCallback() => true;

  public void Update()
  {
    this.Populate(true);
    this.LoadData(this.placePanel, false);
  }

  public void UpdateData() => this.LoadDataTable(true);

  public void Populate(bool reload) => this.Populate(reload, true);

  public void Populate(bool reload, bool populateFirstSublevel)
  {
    ICategoryTypeIconService service = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    this.node.TreeView.BeginUpdate();
    service?.BeginUpdate();
    try
    {
      this.LoadDataTable(reload);
      this.node.Nodes.Clear();
      this.PopulateCallback(reload);
      if (!populateFirstSublevel)
        return;
      for (int index = 0; index < this.node.Nodes.Count; ++index)
        ((CustomFolder) this.node.Nodes[index].Tag).Populate(false, false);
    }
    finally
    {
      service?.EndUpdate();
      this.node.TreeView.EndUpdate();
    }
  }

  public UserControl GetPropertyForm()
  {
    return (UserControl) PropertyFormsHolder.PropertyForms(this.instGuid).PropertyTabPageForm;
  }

  public bool LoadData(Panel panel, bool reload)
  {
    this.placePanel = panel;
    if (!(this.GetPropertyForm() is IConfigPage propertyForm))
      return false;
    propertyForm.Folder = (IFolder) this;
    if (propertyForm.TabControl != null)
      this.ConstructPages(propertyForm.TabControl);
    propertyForm.SetChangedStatus(this.InChange);
    propertyForm.DockToPanel(this.placePanel);
    if (propertyForm.PropertyGrid != null)
      propertyForm.PropertyGrid.SelectedObject = (object) null;
    if (propertyForm.GridControl != null)
      propertyForm.GridControl.DataSource = (object) null;
    try
    {
      propertyForm.DefaultsOnLoad();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
    int num = !this.LoadDataCallback(reload) ? 0 : (this.LoadListCallback(reload) ? 1 : 0);
    if (num != 0)
      this.AddRegisteredPropertyDescriptors();
    if (num == 0)
      return num != 0;
    if (propertyForm.PropertyGrid == null)
      return num != 0;
    propertyForm.PropertyGrid.Refresh();
    return num != 0;
  }

  public virtual bool CanSave => true;

  public virtual IFolder AddChildCallback() => (IFolder) null;

  public virtual IFolder AddChildDubbedCallback(IFolder ifolder) => (IFolder) null;

  public virtual bool SaveCallback() => true;

  /// <summary>
  /// при обработке данных в SaveCallbackEnd нужно зачитать сохраненные данные так как после
  /// SaveCallback могли записаться пользовательские свойства (по подписке)
  /// </summary>
  /// <param name="aVirtualFolder"></param>
  /// <returns></returns>
  public virtual bool SaveCallbackEnd(bool aVirtualFolder) => true;

  public virtual bool ExcludeCallback(ref bool needNodeRemove)
  {
    needNodeRemove = true;
    return true;
  }

  public virtual void PopulateCallback(bool reload)
  {
  }

  public virtual bool LoadDataCallback(bool reload) => true;

  public virtual bool LoadListCallback(bool reload)
  {
    GridControl gridControl = (this.GetPropertyForm() as IConfigPage).GridControl;
    if (gridControl != null)
    {
      this.LoadDataTable(reload);
      MemoryStream ms = (MemoryStream) null;
      if (ServicesManager.GetService(typeof (IGuidMapper)) is IGuidMapper service)
      {
        Guid key = service[this.ListCategoryValue];
        if (key != Guid.Empty)
          ms = ConfigCache.GetConfig(key);
      }
      DataTableConverter.ApplyToGridControl(DataTableConverter.ConvertDataTable(this.dataTable, this.ListCategoryValue), gridControl, ms);
    }
    return true;
  }

  public bool ApplyData()
  {
    object id = this.Id;
    bool flag1 = true;
    if (this.InChange)
    {
      IConfigPage propertyForm = this.GetPropertyForm() as IConfigPage;
      bool flag2 = false;
      this.markSaveCallbackDone = false;
      if (this.NeedSave && this.CanSave && (propertyForm != null && propertyForm.TabControl != null && propertyForm.TabControl.TabPages.IndexOf((TabPage) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage) != -1 && (!StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage) || StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage) && (flag2 = true) && this.SaveCallback() && this.MarkSaveCallbackDone()) || !flag2 && this.SaveCallback() && this.MarkSaveCallbackDone()))
      {
        bool isVirtualFolder = this.isVirtualFolder;
        this.isVirtualFolder = false;
        if (propertyForm != null)
        {
          if (propertyForm.PropertyGrid != null)
            propertyForm.PropertyGrid.Refresh();
          if (!propertyForm.DefaultsOnSave())
            flag1 = false;
        }
        if (flag1)
        {
          flag1 = this.ApplyToRegisteredPropertyDescriptors(id);
          if (flag1 && this.markSaveCallbackDone)
            flag1 = this.SaveCallbackEnd(isVirtualFolder);
          if (flag1)
          {
            this.inChange = false;
            if (this.NeedPageSave)
            {
              for (int index = 0; index < StatesController.LoadedList.Count; ++index)
                StatesController.LoadedList[index] = (object) false;
            }
          }
        }
        if (this.node.Text != this.textValue)
          this.node.Text = this.textValue;
      }
      else
        flag1 = false;
    }
    return flag1;
  }

  public void Cancel() => this.Cancel(true);

  public void Cancel(bool withRefresh)
  {
    this.inChange = false;
    this.CancelToRegisteredPropertyDescriptors();
    if (this.IsVirtualFolder)
    {
      this.node.Remove();
    }
    else
    {
      if (this.GetPropertyForm() is IConfigPage propertyForm)
      {
        bool flag = propertyForm.LastTabPage.TabPageProcessingForm.RefreshAfterCanceling();
        if (withRefresh && !flag)
          withRefresh = flag;
      }
      if (!withRefresh)
        return;
      this.LoadData(this.placePanel, false);
    }
  }

  public void FormLostFocus()
  {
    if (!(this.GetPropertyForm() is IConfigPage propertyForm))
      return;
    propertyForm.DefaultsOnLostFocus((IFolder) this);
  }

  public virtual void Copy()
  {
  }

  public virtual void Cut()
  {
  }

  public virtual void Paste()
  {
  }

  public virtual IFolder Clone() => (IFolder) null;

  public virtual void SetSystemGuid() => this.Update();

  public virtual void LocalizationConfig()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (this.localizationForm == null)
        this.localizationForm = new LocalizationForm();
      if (!(this.GetServerObject(sessionKeeper.Session) is IDBLocalizable serverObject1))
        return;
      string languages = serverObject1.Languages;
      if (this.localizationForm.ExecuteDialog(ref languages) != DialogResult.OK || !(this.GetServerObject(sessionKeeper.Session) is IDBLocalizable serverObject2) || !(serverObject2.Languages != languages))
        return;
      serverObject2.Languages = languages;
    }
  }

  public virtual void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    IGuidService guidService = (IGuidService) null;
    contextMenu.Items.Clear();
    contextMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[11]
    {
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_94"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiAdd]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_95"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiExclude]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_96"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiDelete]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_97"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiUpdate]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_FindItem"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiFind]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_98"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiCopy]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_Cut"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiCut]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_99"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiPaste]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_100"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiExportImage]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_1176"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiOpenInNewWindow]),
      new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_Clone"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiClone])
    });
    int num1 = contextMenu.Items.Count - 1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      guidService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (guidService != null)
      {
        if (this.NodeParent != null)
          contextMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_1219"), (EventHandler) iEventsDispatcher.EventsList[(object) ContextMenuID.cmiSetSystemGuid]));
      }
    }
    this.miAdd = contextMenu.Items[0];
    this.miExclude = contextMenu.Items[1];
    this.miDelete = contextMenu.Items[2];
    this.miUpdate = contextMenu.Items[3];
    this.miFind = contextMenu.Items[4];
    this.miCopy = contextMenu.Items[5];
    this.miCut = contextMenu.Items[6];
    this.miPaste = contextMenu.Items[7];
    this.miExportImage = contextMenu.Items[8];
    this.miOpenInNewWindow = contextMenu.Items[9];
    this.miClone = contextMenu.Items[10];
    if (guidService != null && this.NodeParent != null)
    {
      int num2;
      this.miSetSystemGuid = contextMenu.Items[num2 = num1 + 1];
    }
    INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
    if (service == null)
      return;
    this.miAdd.ImageIndex = service.ImageIndex("imgInsertItem");
    this.miDelete.ImageIndex = service.ImageIndex("imgDelete");
    this.miUpdate.ImageIndex = service.ImageIndex("imgRefresh");
    this.miCopy.ImageIndex = service.ImageIndex("imgCopy");
    this.miCut.ImageIndex = service.ImageIndex("imgCut");
    this.miPaste.ImageIndex = service.ImageIndex("imgPaste");
    this.miOpenInNewWindow.ImageIndex = service.ImageIndex("imgNavigator");
    MenuButtonItem miLocalization = this.miLocalization;
  }

  public virtual void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    this.miOpenInNewWindow.Visible = false;
    this.miAdd.Visible = this.AddChildEnabled;
    this.miAdd.Enabled = this.AddChildEnabled && this.CanAddChild && !this.InChange;
    this.miExclude.Visible = this.ExcludeEnabled;
    this.miExclude.Enabled = this.ExcludeEnabled && this.CanExclude && !this.InChange;
    this.miDelete.Visible = this.DelEnabled;
    this.miDelete.Enabled = this.DelEnabled && this.CanDelete && !this.InChange;
    this.miUpdate.Visible = this.AddChildEnabled;
    this.miUpdate.Enabled = this.AddChildEnabled && !this.InChange;
    this.miCopy.Visible = this.CopyEnabled;
    this.miCopy.Enabled = this.CopyEnabled && !this.InChange;
    this.miCut.Visible = this.CutEnabled;
    this.miCut.Enabled = this.CutEnabled && !this.InChange;
    this.miExportImage.Visible = this.ExportImageEnabled;
    this.miExportImage.Enabled = this.ExportImageEnabled && !this.InChange;
    this.miPaste.Visible = this.PasteEnabled;
    this.miPaste.Enabled = false;
    this.miClone.Visible = this.CloneEnabled;
    this.miClone.Enabled = this.CloneEnabled && !this.InChange;
    if (this.miCopy.Visible)
    {
      this.miCopy.BeginGroup = true;
      this.miCut.BeginGroup = false;
      this.miPaste.BeginGroup = false;
    }
    else if (this.miCut.Visible)
    {
      this.miCopy.BeginGroup = false;
      this.miCut.BeginGroup = true;
      this.miPaste.BeginGroup = false;
    }
    else
    {
      if (!this.miPaste.Visible)
        return;
      this.miCopy.BeginGroup = false;
      this.miCut.BeginGroup = false;
      this.miPaste.BeginGroup = true;
    }
  }

  public bool IsVirtualFolder => this.isVirtualFolder;

  public bool InChange
  {
    get => this.inChange;
    set => this.inChange = value;
  }

  public virtual void ChangeEventProcessing(object s, EventArgs e)
  {
    this.ChangeEventDataToRegisteredPropertyDescriptors(e);
  }

  public virtual int ExportCategoryValue => 0;

  public virtual int ListCategoryValue => 0;

  public ExportAttribute GetExportAttributes(object[] objects)
  {
    return new ExportAttribute(this.ExportCategoryValue, objects);
  }

  public UserControl PropertiesForm => this.GetPropertyForm();

  private bool MarkSaveCallbackDone()
  {
    this.markSaveCallbackDone = true;
    return true;
  }

  public void ExportImage()
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.DefaultExt = "ico";
    saveFileDialog.Filter = LocalizationHolder.rm.GetString("Client.Core_102");
    saveFileDialog.SupportMultiDottedExtensions = true;
    saveFileDialog.Title = LocalizationHolder.rm.GetString("Client.Core_103");
    saveFileDialog.FileName = this.textValue;
    saveFileDialog.RestoreDirectory = true;
    if (saveFileDialog.ShowDialog() != DialogResult.OK || !(saveFileDialog.FileName != string.Empty))
      return;
    byte[] array = ArraySrv.IconToArray(Statics.IconSrv.GetIndexIcon(this.node.ImageIndex));
    FileStream output = File.OpenWrite(saveFileDialog.FileName);
    BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
    try
    {
      binaryWriter.Write(array);
    }
    finally
    {
      binaryWriter.Flush();
      binaryWriter.Close();
      output.Close();
    }
  }

  public virtual IDBSecurity GetSecurity(IUserSession session, object id)
  {
    return this.GetServerObject(session) as IDBSecurity;
  }

  public virtual int MaintainedCategory => this.ExportCategoryValue;

  public virtual Tuple<int, object> Applicability => (Tuple<int, object>) null;
}
