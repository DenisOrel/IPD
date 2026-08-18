
// Type: Intermech.PropertyEditors.LCSchemaFolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.LifeCycles;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class LCSchemaFolder : CustomFolder
{
  private string noteValue = string.Empty;
  private string areaValue = string.Empty;
  private bool defaultSchemaValue;
  private Guid guidValue = Guid.Empty;
  private PropDescriptor identPropDescriptor;
  private PropDescriptor guidPropDescriptor;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public override bool NeedPageSave => true;

  public LCSchemaFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aNote,
    string aArea,
    bool aByDefaultSchema,
    Guid aGuid)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.noteValue = aNote;
    this.areaValue = aArea;
    this.guidValue = aGuid;
    this.defaultSchemaValue = aByDefaultSchema;
    if (Statics.IconSrv == null)
      return;
    this.node.ImageIndex = Statics.IconSrv.IndexOf(16 /*0x10*/, 0);
    this.node.SelectedImageIndex = this.node.ImageIndex;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetLCSchema((int) this.Id);
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
        this.idValue = (object) 0;
        this.textValue = this.node.Text;
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBLCSchema serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLCSchema;
          this.idValue = (object) serverObject.SchemaID;
          this.textValue = serverObject.Name;
          this.noteValue = serverObject.Note;
          this.areaValue = (serverObject as IDBSubjectArea).SubjectAreas;
          this.defaultSchemaValue = serverObject.IsDefaultSchema;
          this.guidValue = (serverObject as IDBGuid).GUID;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.noteValue);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.areaValue));
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new BoolPropertyClass(this.defaultSchemaValue));
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this.guidValue);
      this.PropDescriptorCollection[5].SetValue((object) this, this.idValue);
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
        this.noteValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
        this.guidValue = customService.GenerateNextSystemGuid(16 /*0x10*/, this.textValue, this.noteValue);
        IDBLCSchema serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLCSchema;
        serverObject.SchemaProperties = serverObject.SchemaProperties with
        {
          GUID = this.guidValue
        };
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
      if (!SystemGUIDs.IsSystemGUID((this.GetServerObject(sessionKeeper.Session) as IDBLCSchema).GUID) || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.noteValue = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.areaValue = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[2].GetValue((object) this)).Areas;
    this.defaultSchemaValue = ((BoolPropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).Boolean;
    this.guidValue = (Guid) this.PropDescriptorCollection[3].GetValue((object) this);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          DBLCSchemaProperties properties = new DBLCSchemaProperties(0, this.textValue, this.noteValue, this.guidValue, this.defaultSchemaValue, this.areaValue, LCSchemaOptions.None);
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBLCSchemaCollection).Create(properties);
          this.identPropDescriptor.SetValue((object) this, this.idValue);
        }
        else
        {
          bool flag = false;
          IDBLCSchema serverObject = this.GetServerObject(sessionKeeper.Session) as IDBLCSchema;
          DBLCSchemaProperties schemaProperties = serverObject.SchemaProperties;
          if (schemaProperties.Name != this.textValue)
          {
            schemaProperties.Name = this.textValue;
            flag = true;
          }
          if (schemaProperties.Note != this.noteValue)
          {
            schemaProperties.Note = this.noteValue;
            flag = true;
          }
          if (schemaProperties.GUID != this.guidValue)
          {
            schemaProperties.GUID = this.guidValue;
            flag = true;
          }
          if (schemaProperties.IsDefaultSchema != this.defaultSchemaValue)
          {
            schemaProperties.IsDefaultSchema = this.defaultSchemaValue;
            flag = true;
          }
          if (schemaProperties.AreaID != this.areaValue)
          {
            schemaProperties.AreaID = this.areaValue;
            flag = true;
          }
          if (flag)
            serverObject.SchemaProperties = schemaProperties;
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
      DataHolders.LCSchemasHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_1197"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.LCSchema_Name, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_1198"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.LCSchema_Note, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_1200"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.LCSchema_Area, false, true, false));
    this.guidPropDescriptor = new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_1202"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.LCSchema_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_1204"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(new EventsHolder.GetListDelegate(this.GetListDelegate)), (object) null, string.Empty, PropDescriptions.LCSchema_DefaultSchema, false, true, false));
    this.identPropDescriptor = new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_1206"), (object) null, typeof (long), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.LCSchema_Ident, true, true, false);
    pdc.Add((PropertyDescriptor) this.identPropDescriptor);
  }

  public override void ConstructPages(TabControl tabControl)
  {
    if (tabControl == null)
      return;
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).LCSchema4ObjTypeTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage);
  }

  private ArrayList GetListDelegate(object s, params object[] args)
  {
    return new ArrayList((ICollection) new BoolPropertyClass[1]
    {
      new BoolPropertyClass(true)
    });
  }

  public override int Category => 16 /*0x10*/;
}
