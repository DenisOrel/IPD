
// Type: Intermech.PropertyEditors.LanguageFolder
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
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class LanguageFolder : CustomFolder
{
  private bool byDefaultValue;
  private string cultureId = string.Empty;
  private Guid guidValue = Guid.Empty;
  private PropDescriptor guidPropDescriptor;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public LanguageFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    char aId,
    bool isNew,
    bool aByDefault,
    Guid aGuid,
    string aCultureId)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.byDefaultValue = aByDefault;
    this.cultureId = aCultureId;
    this.guidValue = aGuid;
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(9, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLanguage(Convert.ToString(this.Id));
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
      if (this.IsVirtualFolder)
      {
        this.idValue = (object) ' ';
        this.textValue = this.node.Text;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBLanguageType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLanguageType;
          this.idValue = (object) Convert.ToChar(serverObject.LanguageID);
          this.textValue = serverObject.LanguageName;
          this.byDefaultValue = serverObject.IsDefaultLanguage;
          this.cultureId = serverObject.CultureID;
          this.guidValue = (serverObject as IDBGuid).GUID;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) new BoolPropertyClass(this.byDefaultValue));
      this.PropDescriptorCollection[2].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this.guidValue);
      this.PropDescriptorCollection[4].SetValue((object) this, (object) this.cultureId);
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
        this.guidValue = customService.GenerateNextSystemGuid(9, this.textValue, string.Empty);
        (this.GetServerObject(sessionKeeper.Session) as IDBLanguageType).GUID = this.guidValue;
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
      if (!SystemGUIDs.IsSystemGUID((this.GetServerObject(sessionKeeper.Session) as IDBLanguageType).GUID) || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.byDefaultValue = ((BoolPropertyClass) this.PropDescriptorCollection[1].GetValue((object) this)).Boolean;
    this.guidValue = (Guid) this.PropDescriptorCollection[3].GetValue((object) this);
    this.cultureId = (string) this.PropDescriptorCollection[4].GetValue((object) this);
    try
    {
      if (this.textValue == LocalizationHolder.rm.GetString("Client.Core_106"))
        throw new Exception(LocalizationHolder.rm.GetString("Client.Core_BadLangName"));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBLanguageCollection).Create(this.textValue, this.guidValue, this.cultureId);
          if (this.byDefaultValue)
            (this.GetServerObject(sessionKeeper.Session) as IDBLanguageType).IsDefaultLanguage = this.byDefaultValue;
        }
        else
        {
          IDBLanguageType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLanguageType;
          if (serverObject.LanguageName != this.textValue)
            serverObject.LanguageName = this.textValue;
          if (serverObject.IsDefaultLanguage != this.byDefaultValue)
            serverObject.IsDefaultLanguage = this.byDefaultValue;
          if (serverObject.GUID != this.guidValue)
            serverObject.GUID = this.guidValue;
          if (serverObject.CultureID != this.cultureId)
            serverObject.CultureID = this.cultureId;
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
      DataHolders.LanguagesHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Language_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_106"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.Language_Default, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (char), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Language_Ident, true, true, false));
    this.guidPropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Language_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_108"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_108"), false, true, false));
  }

  public override int Category => 9;
}
