
// Type: Intermech.PropertyEditors.ObjTypeApplPGClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.PropertyEditors;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Summary description for ObjTypeApplPGClass.</summary>
public class ObjTypeApplPGClass : PropDescriptorHolder
{
  private PropDescriptor publicPropDescriptor;
  public int relType;
  public int objType;
  public int inObjType;
  public bool applExists;
  public bool isModified;
  public RelationsApplicabilityProperties rap;
  public RelationsApplicabilityProperties rapOnFillCopy;
  private PropertyGrid propertyGrid;
  private PropDescriptor optionEnableMultilink;
  private PropDescriptor optionDefaultRelation;
  private PropDescriptor optionCompositionTracking;
  private PropDescriptor optionCompositionTrackingMode;
  private PropDescriptor optionChangeLCStep;
  private PropDescriptor optionSyncIdentifiers;
  private PropDescriptor optionCreateSnapshotChild;
  private PropDescriptor optionSyncCheckin;
  private PropDescriptor optionSoftInstantiation;
  private PropDescriptor optionDisableCopy2Version;
  private PropDescriptor optionAutoInstantiation;
  private PropDescriptor optionCopyAttributes2Child;
  private PropDescriptor optionAutoClassificationChildObject;

  public InheritModePropertyClass InheritModePropertyClass
  {
    get
    {
      return this.publicPropDescriptor == null ? (InheritModePropertyClass) null : (InheritModePropertyClass) this.publicPropDescriptor.GetValue((object) this);
    }
  }

  public ObjTypeApplPGClass(
    int aRelType,
    int aObjType,
    int aInObjType,
    PropertyGrid aPropertyGrid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.relType = aRelType;
      this.objType = aObjType;
      this.inObjType = aInObjType;
      this.propertyGrid = aPropertyGrid;
      this.isModified = false;
      this.rap = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(aRelType, aObjType, aInObjType).PropertiesStructure;
      this.applExists = this.rap.InObjectType == this.inObjType && this.rap.ObjectType == this.objType;
    }
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("Client.Core_136"), (object) null, typeof (int), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_136"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, EnumTypeHelper.GetDescription(typeof (ApplicabilityModes)), (object) null, typeof (ApplicabilityModePropertyClass), (TypeConverter) new ApplicabilityModesConverter(), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (ApplicabilityModes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("Client.Core_137"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_138"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, EnumTypeHelper.GetDescription(typeof (RelationConstraintModes)), (object) null, typeof (RelationConstraintModePropertyClass), (TypeConverter) new RelationConstraintModesConverter(), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (RelationConstraintModes)), false, true, false));
    this.publicPropDescriptor = new PropDescriptor(4, (object) this, LocalizationHolder.rm.GetString("Client.Core_1165"), (object) null, typeof (InheritModePropertyClass), (TypeConverter) new InheritModesConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (InheritModes)), false, true, false);
    pdc.Add((PropertyDescriptor) this.publicPropDescriptor);
    pdc.Add((PropertyDescriptor) new PropDescriptor(5, (object) this, LocalizationHolder.rm.GetString("Client.Core_141"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_141"), false, true, false));
    this.optionEnableMultilink = new PropDescriptor(6, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.EnableMultiLink), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.EnableMultiLink), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionEnableMultilink);
    this.optionDefaultRelation = new PropDescriptor(7, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.DefaultRelation), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.DefaultRelation), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDefaultRelation);
    this.optionChangeLCStep = new PropDescriptor(8, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.ChangeLCStep), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionChangeLCStep"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionChangeLCStep);
    this.optionSyncIdentifiers = new PropDescriptor(9, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SyncIdentifiers), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionSyncIdentifiers"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSyncIdentifiers);
    this.optionCreateSnapshotChild = new PropDescriptor(10, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.CreateSnapshotChild), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionCreateSnapshotChild"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCreateSnapshotChild);
    this.optionSyncCheckin = new PropDescriptor(11, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SyncCheckin), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionSyncCheckin"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSyncCheckin);
    this.optionSoftInstantiation = new PropDescriptor(12, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.SoftInstantiation), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionSoftInstantiation"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionSoftInstantiation);
    this.optionDisableCopy2Version = new PropDescriptor(13, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.DisableCopy2Version), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionDisableCopy2Version"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableCopy2Version);
    this.optionAutoInstantiation = new PropDescriptor(14, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.AutoInstantiation), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionAutoInstantiation"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAutoInstantiation);
    this.optionCopyAttributes2Child = new PropDescriptor(15, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.CopyAttributes2Child), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, LocalizationHolder.rm.GetString("Client.Core_optionCopyAttributes2Child"), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionCopyAttributes2Child);
    this.optionAutoClassificationChildObject = new PropDescriptor(16 /*0x10*/, (object) this, ApplicabilityOptionsHelper.GetCaption(ApplicabilityOptions.AutoClassificationChildObject), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, string.Empty, PropDescriptions.RelationType_AutoClassificationChildObject, false, true, false);
    pdc.Add((PropertyDescriptor) this.optionAutoClassificationChildObject);
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ICompositionTrackingService)) is ICompositionTrackingService)
    {
      this.optionCompositionTracking = new PropDescriptor(17, (object) this, EnumTypeHelper.GetDescription(typeof (CompositionTrackingCommands)), (object) null, typeof (EnumValueData<CompositionTrackingCommands>), (TypeConverter) new EnumValueDataConverter<CompositionTrackingCommands>(), (object) new EnumValueDataEditor<CompositionTrackingCommands>(), string.Empty, LocalizationHolder.rm.GetString("Client.Core_1597"), false, true, false);
      this.optionCompositionTrackingMode = new PropDescriptor(18, (object) this, EnumTypeHelper.GetDescription(typeof (CompositionTrackingObjMode)), (object) null, typeof (EnumValueData<CompositionTrackingObjMode>), (TypeConverter) new EnumValueDataConverter<CompositionTrackingObjMode>(), (object) new EnumValueDataEditor<CompositionTrackingObjMode>(), string.Empty, LocalizationHolder.rm.GetString("Client.Core_1598"), false, true, false);
      pdc.Add((PropertyDescriptor) this.optionCompositionTracking);
      pdc.Add((PropertyDescriptor) this.optionCompositionTrackingMode);
    }
    else
    {
      this.optionCompositionTracking = (PropDescriptor) null;
      this.optionCompositionTrackingMode = (PropDescriptor) null;
    }
  }

  private void FixDataCopy() => this.rapOnFillCopy = this.rap;

  public void FillValuesWithRevert()
  {
    this.rap = this.rapOnFillCopy;
    this.FillValues();
  }

  public void FillValues()
  {
    this.isModified = false;
    this.PropDescriptorCollection[0].SetValue((object) this, (object) this.rap.MaximumLinks);
    this.PropDescriptorCollection[1].SetValue((object) this, (object) new ApplicabilityModePropertyClass(this.rap.ApplicabilityMode));
    this.PropDescriptorCollection[2].SetValue((object) this, (object) new BoolPropertyClass(this.rap.CloneChildRelations));
    this.PropDescriptorCollection[3].SetValue((object) this, (object) new RelationConstraintModePropertyClass(this.rap.RelationConstraintMode));
    this.PropDescriptorCollection[5].SetValue((object) this, (object) new BoolPropertyClass(this.rap.IsContent));
    this.PropDescriptorCollection[6].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.EnableMultiLink) == ApplicabilityOptions.EnableMultiLink));
    this.PropDescriptorCollection[7].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.DefaultRelation) == ApplicabilityOptions.DefaultRelation));
    this.PropDescriptorCollection[8].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.ChangeLCStep) == ApplicabilityOptions.ChangeLCStep));
    this.PropDescriptorCollection[9].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.SyncIdentifiers) == ApplicabilityOptions.SyncIdentifiers));
    this.PropDescriptorCollection[10].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.CreateSnapshotChild) == ApplicabilityOptions.CreateSnapshotChild));
    this.PropDescriptorCollection[11].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.SyncCheckin) == ApplicabilityOptions.SyncCheckin));
    this.PropDescriptorCollection[12].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.SoftInstantiation) == ApplicabilityOptions.SoftInstantiation));
    this.PropDescriptorCollection[13].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.DisableCopy2Version) == ApplicabilityOptions.DisableCopy2Version));
    this.PropDescriptorCollection[14].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.AutoInstantiation) == ApplicabilityOptions.AutoInstantiation));
    this.PropDescriptorCollection[15].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.CopyAttributes2Child) == ApplicabilityOptions.CopyAttributes2Child));
    this.PropDescriptorCollection[16 /*0x10*/].SetValue((object) this, (object) new BoolPropertyClass((this.rap.Options & ApplicabilityOptions.AutoClassificationChildObject) == ApplicabilityOptions.AutoClassificationChildObject));
    if (!this.applExists)
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new InheritModePropertyClass(InheritModes.Inherited));
    else
      this.PropDescriptorCollection[4].SetValue((object) this, (object) new InheritModePropertyClass(InheritModes.Private));
    if (this.optionCompositionTracking != null && this.optionCompositionTrackingMode != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        if (session.GetCustomService(typeof (ICompositionTrackingService)) is ICompositionTrackingService customService && customService.IsRegisteredTrackConfig((IObjectTypeApplicabilityContext) this.rap))
        {
          if (!this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTracking))
            this.PropDescriptorCollection.Add((PropertyDescriptor) this.optionCompositionTracking);
          if (!this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTrackingMode))
            this.PropDescriptorCollection.Add((PropertyDescriptor) this.optionCompositionTrackingMode);
          CompositionsTrackingSettings trackingSettings;
          customService.GetConfigValue(session.SessionGUID, (IObjectTypeApplicabilityContext) this.rap, out trackingSettings);
          CompositionTrackingCommands data1 = trackingSettings != null ? trackingSettings.Commands : CompositionTrackingCommands.ctcNone;
          CompositionTrackingObjMode data2 = trackingSettings != null ? trackingSettings.ObjMode : CompositionTrackingObjMode.ctomProceed;
          this.PropDescriptorCollection[17].SetValue((object) this, (object) new EnumValueData<CompositionTrackingCommands>(data1));
          this.PropDescriptorCollection[18].SetValue((object) this, (object) new EnumValueData<CompositionTrackingObjMode>(data2));
        }
        else
        {
          if (this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTracking))
            this.PropDescriptorCollection.Remove((PropertyDescriptor) this.optionCompositionTracking);
          if (this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTrackingMode))
            this.PropDescriptorCollection.Remove((PropertyDescriptor) this.optionCompositionTrackingMode);
        }
      }
    }
    this.FixDataCopy();
    this.UpdateReadOnlyStates();
  }

  public bool SaveValues()
  {
    if (this.isModified)
    {
      CompositionsTrackingSettings trackingSettings = (CompositionsTrackingSettings) null;
      if (this.optionCompositionTracking != null && this.optionCompositionTrackingMode != null && this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTracking) && this.PropDescriptorCollection.Contains((PropertyDescriptor) this.optionCompositionTrackingMode))
      {
        trackingSettings = new CompositionsTrackingSettings(((EnumValueData<CompositionTrackingCommands>) this.PropDescriptorCollection[17].GetValue((object) this)).Data, ((EnumValueData<CompositionTrackingObjMode>) this.PropDescriptorCollection[18].GetValue((object) this)).Data);
        int objectTypeId = MetaDataHelper.GetObjectTypeID("cad00163-306c-11d8-b4e9-00304f19f545");
        if (!trackingSettings.IsEmpty && !MetaDataHelper.IsObjectTypeChildOf(this.rap.ObjectType, objectTypeId) && !MetaDataHelper.IsObjectTypeChildOf(this.rap.InObjectType, objectTypeId))
        {
          CompositionsTrackingSettings other = (CompositionsTrackingSettings) null;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            ServiceUtils.GetService<ICompositionTrackingService>((object) sessionKeeper.Session, false)?.GetConfigValue(this.rap.ObjectType, this.rap.InObjectType, this.rap.RelationType, out other, sessionKeeper.Session.SessionGUID);
          if (!trackingSettings.Equals(other) && MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1679"), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            return false;
        }
      }
      this.rap.MaximumLinks = (int) this.PropDescriptorCollection[0].GetValue((object) this);
      this.rap.ApplicabilityMode = ((ApplicabilityModePropertyClass) this.PropDescriptorCollection[1].GetValue((object) this)).ApplicabilityMode;
      this.rap.CloneChildRelations = ((BoolPropertyClass) this.PropDescriptorCollection[2].GetValue((object) this)).Boolean;
      this.rap.RelationConstraintMode = ((RelationConstraintModePropertyClass) this.PropDescriptorCollection[3].GetValue((object) this)).RelationConstraintMode;
      this.rap.IsContent = ((BoolPropertyClass) this.PropDescriptorCollection[5].GetValue((object) this)).Boolean;
      int num = ((BoolPropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).Boolean ? 1 : 0;
      bool boolean1 = ((BoolPropertyClass) this.PropDescriptorCollection[7].GetValue((object) this)).Boolean;
      bool boolean2 = ((BoolPropertyClass) this.PropDescriptorCollection[8].GetValue((object) this)).Boolean;
      bool boolean3 = ((BoolPropertyClass) this.PropDescriptorCollection[9].GetValue((object) this)).Boolean;
      bool boolean4 = ((BoolPropertyClass) this.PropDescriptorCollection[10].GetValue((object) this)).Boolean;
      bool boolean5 = ((BoolPropertyClass) this.PropDescriptorCollection[11].GetValue((object) this)).Boolean;
      bool boolean6 = ((BoolPropertyClass) this.PropDescriptorCollection[12].GetValue((object) this)).Boolean;
      bool boolean7 = ((BoolPropertyClass) this.PropDescriptorCollection[13].GetValue((object) this)).Boolean;
      bool boolean8 = ((BoolPropertyClass) this.PropDescriptorCollection[14].GetValue((object) this)).Boolean;
      bool boolean9 = ((BoolPropertyClass) this.PropDescriptorCollection[15].GetValue((object) this)).Boolean;
      bool boolean10 = ((BoolPropertyClass) this.PropDescriptorCollection[16 /*0x10*/].GetValue((object) this)).Boolean;
      this.rap.Options = ApplicabilityOptions.None;
      if (num != 0)
        this.rap.Options |= ApplicabilityOptions.EnableMultiLink;
      if (boolean1)
        this.rap.Options |= ApplicabilityOptions.DefaultRelation;
      if (boolean2)
        this.rap.Options |= ApplicabilityOptions.ChangeLCStep;
      if (boolean3)
        this.rap.Options |= ApplicabilityOptions.SyncIdentifiers;
      if (boolean4)
        this.rap.Options |= ApplicabilityOptions.CreateSnapshotChild;
      if (boolean5)
        this.rap.Options |= ApplicabilityOptions.SyncCheckin;
      if (boolean6)
        this.rap.Options |= ApplicabilityOptions.SoftInstantiation;
      if (boolean7)
        this.rap.Options |= ApplicabilityOptions.DisableCopy2Version;
      if (boolean8)
        this.rap.Options |= ApplicabilityOptions.AutoInstantiation;
      if (boolean9)
        this.rap.Options |= ApplicabilityOptions.CopyAttributes2Child;
      if (boolean10)
        this.rap.Options |= ApplicabilityOptions.AutoClassificationChildObject;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (((InheritModePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).InheritMode != InheritModes.Inherited)
        {
          IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
          try
          {
            if (this.applExists)
            {
              applicabilityCollection.GetApplicability(this.rap.ApplicabilityID).PropertiesStructure = this.rap;
            }
            else
            {
              this.rap.InObjectType = this.inObjType;
              this.rap.ObjectType = this.objType;
              this.rap.RelationType = this.relType;
              this.rap.ApplicabilityID = applicabilityCollection.Create(this.rap);
              this.applExists = true;
            }
            this.FixDataCopy();
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
            return false;
          }
          if (trackingSettings != null)
            ServiceUtils.GetService<ICompositionTrackingService>((object) sessionKeeper.Session, false)?.SetConfigValue(this.rap.ObjectType, this.rap.InObjectType, this.rap.RelationType, trackingSettings, sessionKeeper.Session.SessionGUID);
        }
      }
      this.isModified = false;
    }
    return true;
  }

  private void UpdateReadOnlyStates()
  {
    bool aReadOnly = ((InheritModePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).InheritMode == InheritModes.Inherited;
    for (int index = 0; index < this.PropDescriptorCollection.Count; ++index)
    {
      if (((PropDescriptor) this.PropDescriptorCollection[index]).PropID != 4)
        ((PropDescriptor) this.PropDescriptorCollection[index]).SetReadOnly(aReadOnly);
    }
    this.propertyGrid.Refresh();
  }

  public bool ChangePropertyEventProcessing(object s, PropertyValueChangedEventArgs e)
  {
    if (e.ChangedItem.PropertyDescriptor == this.publicPropDescriptor)
    {
      if (!e.OldValue.Equals(this.publicPropDescriptor.GetValue((object) this)))
      {
        this.isModified = true;
        this.UpdateReadOnlyStates();
      }
    }
    else
      this.isModified = true;
    return this.isModified;
  }

  public ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = new ArrayList();
    if (((InheritModePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).InheritMode == InheritModes.Inherited)
      list.Add((object) new InheritModePropertyClass(InheritModes.Inherited));
    list.Add((object) new InheritModePropertyClass(InheritModes.Private));
    return list;
  }
}
