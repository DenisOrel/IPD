
// Type: Intermech.PropertyEditors.LevelFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class LevelFolder : CustomFolder
{
  private PropDescriptor guidPropDescriptor;
  private PropDescriptor iconPropDescriptor;
  private PropDescriptor identPropDescriptor;
  private PropDescriptor storagePropDescriptor;
  private string literaValue = string.Empty;
  private int iconIndex4Category;
  private Icon iconValue;
  private string areaValue = string.Empty;
  private bool byDefaultValue;
  private Guid guidValue = Guid.Empty;
  private long storageValue;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public LevelFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aLitera,
    string aArea,
    bool aByDefault,
    Guid aGuid,
    long aStorage)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.literaValue = aLitera;
    if (Statics.IconSrv != null)
      this.iconIndex4Category = 0;
    this.iconValue = (Icon) null;
    int num = 0;
    if (!isNew)
    {
      if (Statics.IconSrv != null)
      {
        num = Statics.IconSrv.IndexOf(8, aId);
        if (this.iconIndex4Category != num)
          this.iconValue = Statics.IconSrv.GetIconEx(8, aId);
      }
    }
    else
      num = this.iconIndex4Category;
    this.areaValue = aArea;
    this.byDefaultValue = aByDefault;
    this.guidValue = aGuid;
    this.storageValue = aStorage;
    this.node.ImageIndex = num;
    this.node.SelectedImageIndex = num;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLifecycleLevel((int) this.Id);
  }

  public override bool LoadDataCallback(bool reload)
  {
    PropertyGrid propertyGrid = (this.GetPropertyForm() as IConfigPage).PropertyGrid;
    if (propertyGrid == null)
      return true;
    EventsHolder.BlockOnChange = true;
    try
    {
      propertyGrid.SelectedObject = (object) this;
      this.guidPropDescriptor.SetReadOnly(!this.IsVirtualFolder && !ClientConsts.InDeveloperMode);
      this.iconValue = (Icon) null;
      if (this.IsVirtualFolder)
      {
        this.idValue = (object) 0;
        this.textValue = this.node.Text;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBLifecycleLevelType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelType;
          this.idValue = (object) serverObject.LevelID;
          this.textValue = serverObject.LevelName;
          this.literaValue = serverObject.Litera;
          int num = this.iconIndex4Category;
          this.iconValue = (Icon) null;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.IndexOf(8, (int) this.idValue);
            if (num != this.iconIndex4Category)
              this.iconValue = Statics.IconSrv.GetIconEx(8, (int) this.idValue);
          }
          this.areaValue = (serverObject as IDBSubjectArea).SubjectAreas;
          this.byDefaultValue = serverObject.IsDefaultLevel;
          this.guidValue = (serverObject as IDBGuid).GUID;
          this.storageValue = serverObject.StorageID;
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.literaValue);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) this.iconValue);
      this.PropDescriptorCollection[3].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.areaValue));
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new BoolPropertyClass(this.byDefaultValue));
      this.PropDescriptorCollection[5].SetValue((object) this, (object) this.guidValue);
      this.PropDescriptorCollection[6].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[7].SetValue((object) this, this.storageValue != 0L ? (object) new StoragePropertyClass(this.storageValue) : (object) (StoragePropertyClass) null);
    }
    finally
    {
      EventsHolder.BlockOnChange = false;
    }
    return true;
  }

  /// <summary>назначение системного GUID</summary>
  public override void SetSystemGuid()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (customService != null)
      {
        this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
        this.guidValue = customService.GenerateNextSystemGuid(8, this.textValue, string.Empty);
        (this.GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelType).GUID = this.guidValue;
      }
      base.SetSystemGuid();
    }
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!SystemGUIDs.IsSystemGUID((this.GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelType).GUID) || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.literaValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.iconValue = (Icon) this.PropDescriptorCollection[2].GetValue((object) this);
    this.areaValue = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[3].GetValue((object) this)).Areas;
    this.byDefaultValue = ((BoolPropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).Boolean;
    this.guidValue = (Guid) this.PropDescriptorCollection[5].GetValue((object) this);
    this.storageValue = this.PropDescriptorCollection[7].GetValue((object) this) != null ? ((StoragePropertyClass) this.PropDescriptorCollection[7].GetValue((object) this)).Storage : 0L;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelCollection).Create(this.textValue, this.literaValue, this.areaValue, this.guidValue, this.byDefaultValue);
          IDBLifecycleLevelType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelType;
          serverObject.LevelIcon = ArraySrv.IconToArray(this.iconValue);
          if (serverObject.StorageID != this.storageValue)
            serverObject.StorageID = this.storageValue;
          int num = this.iconIndex4Category;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.AddIcon(this.iconValue, 8, (int) this.idValue);
            if (num == 0)
              num = this.iconIndex4Category;
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
          this.identPropDescriptor.SetValue((object) this, this.idValue);
        }
        else
        {
          IDBLifecycleLevelType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLifecycleLevelType;
          if (serverObject.LevelName != this.textValue)
            serverObject.LevelName = this.textValue;
          if (serverObject.Litera != this.literaValue)
            serverObject.Litera = this.literaValue;
          if (serverObject.GUID != this.guidValue)
            serverObject.GUID = this.guidValue;
          byte[] array = ArraySrv.IconToArray(this.iconValue);
          if (!ArraySrv.Compare(serverObject.LevelIcon, array))
            serverObject.LevelIcon = array;
          int num = this.iconIndex4Category;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.AddIcon(this.iconValue, 8, (int) this.idValue);
            if (num == 0)
              num = this.iconIndex4Category;
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
          if (serverObject.IsDefaultLevel != this.byDefaultValue)
            serverObject.IsDefaultLevel = this.byDefaultValue;
          if (serverObject.StorageID != this.storageValue)
            serverObject.StorageID = this.storageValue;
          IDBSubjectArea dbSubjectArea = serverObject as IDBSubjectArea;
          if (dbSubjectArea.SubjectAreas != this.areaValue)
            dbSubjectArea.SubjectAreas = this.areaValue;
        }
      }
      this.guidPropDescriptor.SetReadOnly(true);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      return false;
    }
    finally
    {
      if (ServicesManager.GetService(typeof (StatusesInfoService)) is StatusesInfoService service)
        service.Reload();
      DataHolders.LevelsHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_112"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_Litera, false, true, false));
    this.iconPropDescriptor = new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_17"), (object) null, typeof (Icon), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_Icon, false, true, true);
    pdc.Add((PropertyDescriptor) this.iconPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_70"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_Area, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_113"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.Level_Default, false, true, false));
    this.guidPropDescriptor = new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    this.identPropDescriptor = new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Level_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.identPropDescriptor);
    this.storagePropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("Client.Core_Storage"), (object) null, typeof (StoragePropertyClass), (TypeConverter) new StorageConverter(), (object) null, string.Empty, PropDescriptions.Level_Storage, false, true, false);
    pdc.Add((PropertyDescriptor) this.storagePropDescriptor);
  }

  public override void ConstructPages(TabControl tabControl)
  {
    if (tabControl == null)
      return;
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage);
  }

  public override int Category => 8;
}
