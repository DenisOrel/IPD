// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepObject
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Map;
using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepObject : DBPropDescriptorHolder, ILCObject, ILCStep
{
  private DBLifecycleStepProperties lcStepProperties;
  private PropDescriptor optionDisableParallelVersionsDescriptor;
  private PropDescriptor optionBaseVersionDescriptor;
  private PropDescriptor optionRestoreSoftInstantiation;
  private PropDescriptor optionDisableContextParallelVersions;
  private PropDescriptor autoTransferDescriptor;
  private LCSchema parentSchema;
  private PropDescriptor guidPropDescriptor;

  public DBLifecycleStepProperties LCStepProperties
  {
    get => this.lcStepProperties;
    set => this.lcStepProperties = value;
  }

  public LCStepObject(DBLifecycleStepProperties sp, LCSchema aParentSchema)
    : base((object) sp.LCStep)
  {
    this.lcStepProperties = sp;
    this.parentSchema = aParentSchema;
  }

  public override int Category => 7;

  public override object Id => (object) this.lcStepProperties.LCStep;

  public override void SetId(object aId) => this.lcStepProperties.LCStep = Convert.ToInt32(aId);

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(0, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_205"), (object) null, typeof (int), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_206"), true, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(1, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_207"), (object) null, typeof (LevelPropertyClass), (TypeConverter) new LevelConverter(false, false), (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_207"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(2, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_208"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_209"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(3, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_210"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_210"), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(4, (object) this, EnumTypeHelper.GetDescription(typeof (LCAccessTypes)), (object) null, typeof (LCAccessTypePropertyClass), (TypeConverter) new LCAccessTypesConverter(), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (LCAccessTypes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(5, (object) this, EnumTypeHelper.GetDescription(typeof (ObjectModifyModes)), (object) null, typeof (ObjectModifyModePropertyClass), (TypeConverter) new ObjectModifyModesConverter(), (object) null, string.Empty, EnumTypeHelper.GetDescription(typeof (ObjectModifyModes)), false, true, false));
    pdc.Add((PropertyDescriptor) new PropDescriptor(6, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_211"), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(new EventsHolder.GetListDelegate(this.GetList)), (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_212"), false, true, false));
    this.guidPropDescriptor = new PropDescriptor(7, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_213"), (object) null, typeof (Guid), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_213"), false, true, false);
    pdc.Add((PropertyDescriptor) this.guidPropDescriptor);
    this.guidPropDescriptor.SetReadOnly(!ClientConsts.InDeveloperMode);
    this.autoTransferDescriptor = new PropDescriptor(8, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_PrevAutoMove"), (object) null, typeof (LCStepPropertyClass), (TypeConverter) null, (object) new LCStepPropertyEditor(new EventsHolder.GetListDelegate(this.GetStepsList)), string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_PrevAutoMoveDescr"), false, true, false);
    pdc.Add((PropertyDescriptor) this.autoTransferDescriptor);
    this.optionDisableParallelVersionsDescriptor = new PropDescriptor(9, (object) this, LCStepOptionsHelper.GetCaption(LCStepOptions.DisableParallelVersions), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), LCStepOptionsHelper.GetCaption(LCStepOptions.DisableParallelVersions), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableParallelVersionsDescriptor);
    this.optionBaseVersionDescriptor = new PropDescriptor(10, (object) this, LCStepOptionsHelper.GetCaption(LCStepOptions.BaseVersion), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), LCStepOptionsHelper.GetCaption(LCStepOptions.BaseVersion), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionBaseVersionDescriptor);
    this.optionRestoreSoftInstantiation = new PropDescriptor(11, (object) this, LCStepOptionsHelper.GetCaption(LCStepOptions.RestoreSoftInstantiation), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), LCStepOptionsHelper.GetCaption(LCStepOptions.RestoreSoftInstantiation), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionRestoreSoftInstantiation);
    this.optionDisableContextParallelVersions = new PropDescriptor(12, (object) this, LCStepOptionsHelper.GetCaption(LCStepOptions.DisableContextParallelVersions), (object) null, typeof (BoolPropertyClass), (TypeConverter) new BoolConverter(), (object) null, VisualCategoriesHelper.GetCaption(VisualCategories.History), LCStepOptionsHelper.GetCaption(LCStepOptions.DisableContextParallelVersions), false, true, false);
    pdc.Add((PropertyDescriptor) this.optionDisableContextParallelVersions);
  }

  public void LoadProps()
  {
    bool aReadOnly = this.parentSchema.ReadOnly;
    ((PropDescriptor) this.PropDescriptorCollection[1]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[2]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[3]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[4]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[5]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[7]).SetReadOnly(aReadOnly || !ClientConsts.InDeveloperMode);
    ((PropDescriptor) this.PropDescriptorCollection[6]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[9]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[10]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[11]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[12]).SetReadOnly(aReadOnly);
    ((PropDescriptor) this.PropDescriptorCollection[8]).SetReadOnly(aReadOnly);
    this.PropDescriptorCollection[0].SetValue((object) this, (object) this.lcStepProperties.LCStep);
    this.PropDescriptorCollection[1].SetValue((object) this, (object) new LevelPropertyClass(this.lcStepProperties.LevelID));
    this.PropDescriptorCollection[2].SetValue((object) this, (object) this.lcStepProperties.LCName);
    this.PropDescriptorCollection[3].SetValue((object) this, (object) this.lcStepProperties.Note);
    this.PropDescriptorCollection[4].SetValue((object) this, (object) new LCAccessTypePropertyClass(this.lcStepProperties.AccessType));
    this.PropDescriptorCollection[5].SetValue((object) this, (object) new ObjectModifyModePropertyClass(this.lcStepProperties.ObjectModifyMode));
    this.PropDescriptorCollection[7].SetValue((object) this, (object) this.lcStepProperties.StepGuid);
    this.PropDescriptorCollection[6].SetValue((object) this, (object) new BoolPropertyClass(this.lcStepProperties.FirstStep));
    this.PropDescriptorCollection[9].SetValue((object) this, (object) new BoolPropertyClass((this.lcStepProperties.Options & LCStepOptions.DisableParallelVersions) == LCStepOptions.DisableParallelVersions));
    this.PropDescriptorCollection[10].SetValue((object) this, (object) new BoolPropertyClass((this.lcStepProperties.Options & LCStepOptions.BaseVersion) == LCStepOptions.BaseVersion));
    this.PropDescriptorCollection[11].SetValue((object) this, (object) new BoolPropertyClass((this.lcStepProperties.Options & LCStepOptions.RestoreSoftInstantiation) == LCStepOptions.RestoreSoftInstantiation));
    this.PropDescriptorCollection[12].SetValue((object) this, (object) new BoolPropertyClass((this.lcStepProperties.Options & LCStepOptions.DisableContextParallelVersions) == LCStepOptions.DisableContextParallelVersions));
    this.LoadAutoTransferProp(this.lcStepProperties.LCStep, this.autoTransferDescriptor);
    this.AddRegisteredPropertyDescriptors();
  }

  public void SaveProps()
  {
    this.lcStepProperties.LevelID = ((LevelPropertyClass) this.PropDescriptorCollection[1].GetValue((object) this)).Level;
    this.lcStepProperties.LCName = this.PropDescriptorCollection[2].GetValue((object) this).ToString();
    this.lcStepProperties.Note = this.PropDescriptorCollection[3].GetValue((object) this).ToString();
    this.lcStepProperties.AccessType = ((LCAccessTypePropertyClass) this.PropDescriptorCollection[4].GetValue((object) this)).LCAccessType;
    this.lcStepProperties.ObjectModifyMode = ((ObjectModifyModePropertyClass) this.PropDescriptorCollection[5].GetValue((object) this)).ObjectModifyMode;
    this.lcStepProperties.StepGuid = (Guid) this.PropDescriptorCollection[7].GetValue((object) this);
    this.lcStepProperties.FirstStep = ((BoolPropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).Boolean;
    this.lcStepProperties.Options = LCStepOptions.None;
    if (((BoolPropertyClass) this.PropDescriptorCollection[9].GetValue((object) this)).Boolean)
      this.lcStepProperties.Options |= LCStepOptions.DisableParallelVersions;
    if (((BoolPropertyClass) this.PropDescriptorCollection[10].GetValue((object) this)).Boolean)
      this.lcStepProperties.Options |= LCStepOptions.BaseVersion;
    if (((BoolPropertyClass) this.PropDescriptorCollection[11].GetValue((object) this)).Boolean)
      this.lcStepProperties.Options |= LCStepOptions.RestoreSoftInstantiation;
    if (((BoolPropertyClass) this.PropDescriptorCollection[12].GetValue((object) this)).Boolean)
      this.lcStepProperties.Options |= LCStepOptions.DisableContextParallelVersions;
    this.SaveAutoTransferProp(this.lcStepProperties.LCStep, this.autoTransferDescriptor);
  }

  private ArrayList GetStepsList(object s, params object[] args)
  {
    ArrayList stepsList = new ArrayList();
    stepsList.Add((object) string.Empty);
    int lcStep = this.lcStepProperties.LCStep;
    if (this.parentSchema.LCView.LCDocument.FindNodeByStepId(lcStep) != null)
    {
      foreach (MapObject mapObject in (MapDocument) this.parentSchema.LCView.LCDocument)
      {
        if (mapObject is LCLink lcLink)
        {
          LCStepsLinkProperties stepLinkProperties = lcLink.LCLinkObject.LCStepLinkProperties;
          if (!lcLink.LCLinkObject.Reversible && stepLinkProperties.FromStepID == lcStep || lcLink.LCLinkObject.Reversible && (stepLinkProperties.FromStepID == lcStep || stepLinkProperties.ToStepID == lcStep))
          {
            LCNode nodeByStepId = this.parentSchema.LCView.LCDocument.FindNodeByStepId(!lcLink.LCLinkObject.Reversible ? stepLinkProperties.ToStepID : (stepLinkProperties.FromStepID == lcStep ? stepLinkProperties.ToStepID : stepLinkProperties.FromStepID));
            if (nodeByStepId != null)
              stepsList.Add((object) new LCStepPropertyClass(nodeByStepId.LCStepObject.LCStepProperties.LCStep, nodeByStepId.LCStepObject.LCStepProperties.LCName));
          }
        }
      }
    }
    return stepsList;
  }

  private void LoadAutoTransferProp(int lcstep, PropDescriptor autoTransferPD)
  {
    bool flag = false;
    if (this.parentSchema.LCView.LCDocument.FindNodeByStepId(lcstep) != null)
    {
      foreach (MapObject mapObject in (MapDocument) this.parentSchema.LCView.LCDocument)
      {
        if (mapObject is LCLink lcLink)
        {
          LCStepsLinkProperties stepLinkProperties = lcLink.LCLinkObject.LCStepLinkProperties;
          if (!lcLink.LCLinkObject.Reversible && stepLinkProperties.FromStepID == lcstep || lcLink.LCLinkObject.Reversible && (stepLinkProperties.FromStepID == lcstep || stepLinkProperties.ToStepID == lcstep))
          {
            int num = !lcLink.LCLinkObject.Reversible ? stepLinkProperties.Params : (stepLinkProperties.FromStepID == lcstep ? stepLinkProperties.Params : lcLink.LCLinkObject.ReversibleParams);
            int aStepId = !lcLink.LCLinkObject.Reversible ? stepLinkProperties.ToStepID : (stepLinkProperties.FromStepID == lcstep ? stepLinkProperties.ToStepID : stepLinkProperties.FromStepID);
            if ((num & 1) != 0)
            {
              LCNode nodeByStepId = this.parentSchema.LCView.LCDocument.FindNodeByStepId(aStepId);
              if (nodeByStepId != null)
              {
                autoTransferPD.SetValue((object) this, (object) new LCStepPropertyClass(nodeByStepId.LCStepObject.LCStepProperties.LCStep, nodeByStepId.LCStepObject.LCStepProperties.LCName));
                flag = true;
                break;
              }
              break;
            }
          }
        }
      }
    }
    if (flag)
      return;
    autoTransferPD.SetValue((object) this, (object) null);
  }

  private void SaveAutoTransferProp(int lcstep, PropDescriptor autoTransferPD)
  {
    LCStepPropertyClass stepPropertyClass = autoTransferPD.GetValue((object) this) as LCStepPropertyClass;
    if (this.parentSchema.LCView.LCDocument.FindNodeByStepId(lcstep) == null)
      return;
    foreach (MapObject mapObject in (MapDocument) this.parentSchema.LCView.LCDocument)
    {
      if (mapObject is LCLink lcLink)
      {
        LCStepsLinkProperties stepLinkProperties = lcLink.LCLinkObject.LCStepLinkProperties;
        if (!lcLink.LCLinkObject.Reversible && stepLinkProperties.FromStepID == lcstep || lcLink.LCLinkObject.Reversible && (stepLinkProperties.FromStepID == lcstep || stepLinkProperties.ToStepID == lcstep))
        {
          int num = !lcLink.LCLinkObject.Reversible ? stepLinkProperties.ToStepID : (stepLinkProperties.FromStepID == lcstep ? stepLinkProperties.ToStepID : stepLinkProperties.FromStepID);
          if (stepPropertyClass != null && num == stepPropertyClass.LCStep)
          {
            if (!lcLink.LCLinkObject.Reversible)
              stepLinkProperties.Params |= 1;
            else if (stepLinkProperties.FromStepID == lcstep)
              stepLinkProperties.Params |= 1;
            else
              lcLink.LCLinkObject.ReversibleParams |= 1;
          }
          else if (!lcLink.LCLinkObject.Reversible)
            stepLinkProperties.Params &= -2;
          else if (stepLinkProperties.FromStepID == lcstep)
            stepLinkProperties.Params &= -2;
          else
            lcLink.LCLinkObject.ReversibleParams &= -2;
          lcLink.LCLinkObject.LCStepLinkProperties = new LCStepsLinkProperties(stepLinkProperties.FromStepID, stepLinkProperties.ToStepID, stepLinkProperties.Note, stepLinkProperties.RouteID, stepLinkProperties.Params);
        }
      }
    }
  }

  public void ChangeEvent(EventArgs e) => this.ChangeEventDataToRegisteredPropertyDescriptors(e);

  public bool Apply(object oldId)
  {
    this.ApplyToRegisteredPropertyDescriptors(oldId);
    return true;
  }

  public void Cancel() => this.CancelToRegisteredPropertyDescriptors();

  public bool IsNode => true;

  public bool IsLink => false;

  public ArrayList GetList(object s, params object[] args)
  {
    ArrayList list = new ArrayList();
    if (!((BoolPropertyClass) this.PropDescriptorCollection[6].GetValue((object) this)).Boolean)
      list.Add((object) new BoolPropertyClass(true));
    return list;
  }
}
