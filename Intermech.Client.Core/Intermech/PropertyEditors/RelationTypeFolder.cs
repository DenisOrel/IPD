
// Type: Intermech.PropertyEditors.RelationTypeFolder
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
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class RelationTypeFolder : CustomFolder
{
  private PropDescriptor anyAttributePropDescriptor;
  private PropDescriptor guidPropDescriptor;
  private PropDescriptor iconPropDescriptor;
  private PropDescriptor optionEnableCycleRelationsDescriptor;
  private PropDescriptor optionEnableCheckAnnulmentDescriptor;
  protected bool _BlockOnChange;
  private int iconIndex4Category;
  private Icon iconValue;
  private RelationTypeProperties relationTypeProperties;
  private MenuButtonItem miRecreateView;

  public override bool DelEnabled => true;

  public override bool AddChildEnabled => false;

  public override bool NeedApply => true;

  public override bool NeedSave => true;

  public RelationTypeFolder(
    Guid aInstGuid,
    string aText,
    object aNodeParent,
    int aId,
    bool isNew,
    string aTypeName,
    string aRevTypeName,
    string aNote,
    bool aChkoutFile,
    RelationKinds aRelKind,
    bool aSaveHistory,
    Guid aGuid,
    string aArea,
    bool aAnyAttributes,
    string aShortName,
    RelationTypeOptions aOptions)
    : base(aInstGuid, aText, aNodeParent, (object) aId, isNew)
  {
    this.relationTypeProperties = new RelationTypeProperties(aId, aTypeName, aRevTypeName, aNote, aChkoutFile, aSaveHistory, aText, aGuid, aArea, aAnyAttributes, aShortName, aOptions);
    if (Statics.IconSrv != null)
      this.iconIndex4Category = 0;
    this.iconValue = (Icon) null;
    int num = 0;
    if (!isNew)
    {
      if (Statics.IconSrv != null)
      {
        num = Statics.IconSrv.IndexOf(6, aId);
        if (this.iconIndex4Category != num)
          this.iconValue = Statics.IconSrv.GetIconEx(6, aId);
      }
    }
    else
      num = this.iconIndex4Category;
    this.node.ImageIndex = num;
    this.node.SelectedImageIndex = num;
  }

  public override object GetServerObject(IUserSession session)
  {
    return (object) session.GetRelationType((int) this.Id);
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
          IDBRelationType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBRelationType;
          this.idValue = (object) serverObject.PropertiesStructure.RelationType;
          this.textValue = serverObject.PropertiesStructure.Description;
          this.relationTypeProperties = serverObject.PropertiesStructure;
          int num = this.iconIndex4Category;
          this.iconValue = (Icon) null;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.IndexOf(6, (int) this.idValue);
            if (num != this.iconIndex4Category)
              this.iconValue = Statics.IconSrv.GetIconEx(6, (int) this.idValue);
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
        }
      }
      this.PropDescriptorCollection[0].SetValue((object) this, (object) this.textValue);
      this.PropDescriptorCollection[1].SetValue((object) this, (object) this.relationTypeProperties.TypeName);
      this.PropDescriptorCollection[2].SetValue((object) this, (object) this.relationTypeProperties.ReverseName);
      this.PropDescriptorCollection[3].SetValue((object) this, (object) this.relationTypeProperties.Note);
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new BoolPropertyClass(this.relationTypeProperties.CheckoutFile));
      this.PropDescriptorCollection[5].SetValue((object) this, (object) new SubjectAreaPropertyClass(this.relationTypeProperties.AreaID));
      this.PropDescriptorCollection[6].SetValue((object) this, (object) this.relationTypeProperties.RelationTypeGuid);
      this.PropDescriptorCollection[7].SetValue((object) this, (object) this.iconValue);
      this.PropDescriptorCollection[8].SetValue((object) this, (object) new BoolPropertyClass(this.relationTypeProperties.AnyAttributes));
      this.PropDescriptorCollection[9].SetValue((object) this, this.idValue);
      this.PropDescriptorCollection[10].SetValue((object) this, (object) this.relationTypeProperties.ShortName);
      this.PropDescriptorCollection[11].SetValue((object) this, (object) new BoolPropertyClass((this.relationTypeProperties.Options & RelationTypeOptions.EnableCycleRelations) == RelationTypeOptions.EnableCycleRelations));
      this.PropDescriptorCollection[12].SetValue((object) this, (object) new BoolPropertyClass((this.relationTypeProperties.Options & RelationTypeOptions.EnableCheckAnnulment) == RelationTypeOptions.EnableCheckAnnulment));
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
        this.textValue = (string) this.PropDescriptorCollection[2].GetValue((object) this);
        string note = (string) this.PropDescriptorCollection[3].GetValue((object) this);
        Guid nextSystemGuid = customService.GenerateNextSystemGuid(6, this.textValue, note);
        IDBRelationType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBRelationType;
        this.relationTypeProperties.RelationTypeGuid = nextSystemGuid;
        RelationTypeProperties relationTypeProperties = this.relationTypeProperties;
        serverObject.PropertiesStructure = relationTypeProperties;
      }
      base.SetSystemGuid();
    }
  }

  private void SetOption(
    ref RelationTypeOptions options,
    RelationTypeOptions currentOption,
    RelationTypePropID propID)
  {
    if (((BoolPropertyClass) this.PropDescriptorCollection[(int) propID].GetValue((object) this)).Boolean)
      options |= currentOption;
    else
      options &= ~currentOption;
  }

  public override bool SaveCallback()
  {
    this.textValue = (string) this.PropDescriptorCollection[0].GetValue((object) this);
    this.relationTypeProperties.Description = this.textValue;
    this.relationTypeProperties.TypeName = (string) this.PropDescriptorCollection[1].GetValue((object) this);
    this.relationTypeProperties.ReverseName = (string) this.PropDescriptorCollection[2].GetValue((object) this);
    this.relationTypeProperties.Note = (string) this.PropDescriptorCollection[3].GetValue((object) this);
    this.relationTypeProperties.CheckoutFile = ((BoolPropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).Boolean;
    this.relationTypeProperties.AreaID = ((SubjectAreaPropertyClass) this.PropDescriptorCollection[5].GetValue((object) this)).Areas;
    this.relationTypeProperties.RelationTypeGuid = (Guid) this.PropDescriptorCollection[6].GetValue((object) this);
    this.relationTypeProperties.AnyAttributes = ((BoolPropertyClass) this.PropDescriptorCollection[8].GetValue((object) this)).Boolean;
    this.relationTypeProperties.ShortName = (string) this.PropDescriptorCollection[10].GetValue((object) this);
    this.SetOption(ref this.relationTypeProperties.Options, RelationTypeOptions.EnableCycleRelations, RelationTypePropID.OptionEnableCycleRelations);
    this.SetOption(ref this.relationTypeProperties.Options, RelationTypeOptions.EnableCheckAnnulment, RelationTypePropID.OptionEnableCheckAnnulment);
    this.iconValue = (Icon) this.PropDescriptorCollection[7].GetValue((object) this);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (this.IsVirtualFolder)
        {
          this.idValue = (object) ((this.nodeParent.Tag as CustomFolder).GetServerObject(sessionKeeper.Session) as IDBRelationTypeCollection).Create(this.relationTypeProperties);
          (this.GetServerObject(sessionKeeper.Session) as IDBRelationType).Icon = ArraySrv.IconToArray(this.iconValue);
          int num = this.iconIndex4Category;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.AddIcon(this.iconValue, 6, (int) this.idValue);
            if (num == 0)
              num = this.iconIndex4Category;
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
          this.relationTypeProperties.RelationType = (int) this.idValue;
        }
        else
        {
          IDBRelationType serverObject = this.GetServerObject(sessionKeeper.Session) as IDBRelationType;
          serverObject.PropertiesStructure = this.relationTypeProperties;
          byte[] array = ArraySrv.IconToArray(this.iconValue);
          if (!ArraySrv.Compare(serverObject.Icon, array))
            serverObject.Icon = array;
          int num = this.iconIndex4Category;
          if (Statics.IconSrv != null)
          {
            num = Statics.IconSrv.AddIcon(this.iconValue, 6, (int) this.idValue);
            if (num == 0)
              num = this.iconIndex4Category;
          }
          this.node.ImageIndex = num;
          this.node.SelectedImageIndex = num;
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
      DataHolders.RelationTypesHolder.ClearInfo();
    }
    return true;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_33"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_Descr, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("Client.Core_149"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_TypeName, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_150"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_RevTypeName, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("Client.Core_35"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_Note, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_139"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.RelationType_ChkoutFile, false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_70"), (object) null, typeof (SubjectAreaPropertyClass), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_Area, false, true, false));
    this.guidPropDescriptor = new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("Client.Core_39"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_GUID, false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    this.iconPropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("Client.Core_17"), (object) null, typeof (Icon), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_Icon, false, true, true);
    pdc.Add((PropertyDescriptor) this.iconPropDescriptor);
    this.anyAttributePropDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("Client.Core_127"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.RelationType_AnyAttribute, false, true, false);
    pdc.Add((PropertyDescriptor) this.anyAttributePropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(9, (object) this, LocalizationHolder.rm.GetString("Client.Core_37"), (object) null, typeof (long), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_Ident, true, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(10, (object) this, LocalizationHolder.rm.GetString("Client.Core_ShortName"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, PropDescriptions.RelationType_ShortName, false, true, false));
    this.optionEnableCycleRelationsDescriptor = new PropDescriptor(11, (object) this, RelationTypeOptionsHelper.GetCaption(RelationTypeOptions.EnableCycleRelations), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.RelationType_EnableCycleRelations, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionEnableCycleRelationsDescriptor);
    this.optionEnableCheckAnnulmentDescriptor = new PropDescriptor(12, (object) this, RelationTypeOptionsHelper.GetCaption(RelationTypeOptions.EnableCheckAnnulment), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.RelationType_EnableCheckAnnulment, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionEnableCheckAnnulmentDescriptor);
  }

  public override void GetContextMenu(
    ContextMenuBarItem contextMenu,
    IEventsDispatcher iEventsDispatcher)
  {
    base.GetContextMenu(contextMenu, iEventsDispatcher);
    this.miRecreateView = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_130"), new EventHandler(this.RecreateView));
    this.miRecreateView.BeginGroup = true;
    contextMenu.Items.AddRange((ToolbarItemBase[]) new MenuButtonItem[1]
    {
      this.miRecreateView
    });
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!SystemGUIDs.IsSystemGUID((this.GetServerObject(sessionKeeper.Session) as IDBRelationType).PropertiesStructure.RelationTypeGuid) || this.miSetSystemGuid == null)
        return;
      contextMenu.Items.Remove((ToolbarItemBase) this.miSetSystemGuid);
    }
  }

  public override void SetContextMenuItemStatus(ContextMenuBarItem contextMenu)
  {
    base.SetContextMenuItemStatus(contextMenu);
    this.miRecreateView.Enabled = !this.InChange;
  }

  private void RecreateView(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(this.GetServerObject(sessionKeeper.Session) is IDBRelationType serverObject))
        return;
      serverObject.RebuildView();
    }
  }

  public override void ConstructPages(TabControl tabControl)
  {
    if (tabControl == null)
      return;
    TabControlProcessor.AssignTabPages(tabControl, (object) TabPagesHolder.TabPages(this.instGuid).PropertyTabPage, (object) TabPagesHolder.TabPages(this.instGuid).Attr4RelTypeTabPage, (object) TabPagesHolder.TabPages(this.instGuid).SecurityTabPage, (object) TabPagesHolder.TabPages(this.instGuid).Forms4RelationTypePage, (object) TabPagesHolder.TabPages(this.instGuid).ActionsTabPage);
  }

  public override int Category => 6;
}
