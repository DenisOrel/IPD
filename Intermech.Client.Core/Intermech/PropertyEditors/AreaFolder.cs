
// Type: Intermech.PropertyEditors.AreaFolder
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

public class AreaFolder : CustomFolder
{
  private string noteValue = string.Empty;
  private Guid guidValue = Guid.Empty;
  private PropDescriptor guidPropDescriptor;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public AreaFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    char aId,
    bool isNew,
    string aNote,
    Guid aGuid)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.noteValue = aNote;
    this.guidValue = aGuid;
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(11, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetSubjectAreaType(Convert.ToChar(this.Id));
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
          IDBSubjectAreaType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBSubjectAreaType;
          this.idValue = (object) serverObject.AreaID;
          this.textValue = serverObject.AreaName;
          this.noteValue = serverObject.Note;
          this.guidValue = (serverObject as IDBGuid).GUID;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.noteValue);
      this.PropDescriptorCollection[2].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this.guidValue);
    }
    finally
    {
      EventsHolder.BlockOnChange = false;
    }
    return true;
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.noteValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.guidValue = (Guid) this.PropDescriptorCollection[3].GetValue((object) this);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBSubjectAreaCollection).Create(this.textValue, this.noteValue, this.guidValue);
        }
        else
        {
          IDBSubjectAreaType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBSubjectAreaType;
          if (serverObject.AreaName != this.textValue)
            serverObject.AreaName = this.textValue;
          if (serverObject.Note != this.noteValue)
            serverObject.Note = this.noteValue;
          if ((serverObject as IDBGuid).GUID != this.guidValue)
            serverObject.SetGUID(this.guidValue);
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
      DataHolders.SubjectAreasHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, PropDescriptions.Area_Name, (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_34"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, PropDescriptions.Area_Note, (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_36"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, PropDescriptions.Area_Ident, (object) null, typeof (char), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_38"), true, true, false));
    this.guidPropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.Area_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
  }

  public override int Category => 11;

  public override void SetSystemGuid()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IGuidService customService = (IGuidService) sessionKeeper.Session.GetCustomService(typeof (IGuidService));
      if (customService != null)
      {
        this.guidValue = customService.GenerateNextSystemGuid(11, this.textValue, this.noteValue);
        (this.GetServerObject(sessionKeeper.Session) as IDBSubjectAreaType).SetGUID(this.guidValue);
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
      if (!(this.GetServerObject(sessionKeeper.Session) as IDBSubjectAreaType as IDBGuid).IsSystemGUID || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }
}
