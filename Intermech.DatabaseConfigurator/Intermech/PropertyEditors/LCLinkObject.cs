// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCLinkObject
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCLinkObject : PropDescriptorHolder, ILCObject
{
  private static readonly int notePropID;
  private bool reversible;
  private int reversibleParams;
  private LCSchema parentSchema;
  private LCStepsLinkProperties lcStepLinkProperties;

  public bool Reversible
  {
    get => this.reversible;
    set => this.reversible = value;
  }

  public int ReversibleParams
  {
    get => this.reversibleParams;
    set => this.reversibleParams = value;
  }

  public LCStepsLinkProperties LCStepLinkProperties
  {
    get => this.lcStepLinkProperties;
    set => this.lcStepLinkProperties = value;
  }

  public LCLinkObject(LCStepsLinkProperties lp, LCSchema aParentSchema)
  {
    this.lcStepLinkProperties = lp;
    this.parentSchema = aParentSchema;
  }

  public override void CreateProperties(PropertyDescriptorCollection pdc)
  {
    pdc.Add((PropertyDescriptor) new PropDescriptor(LCLinkObject.notePropID, (object) this, LocalizationHolder.rm.GetString("DatabaseConfigurator_210"), (object) null, typeof (string), (TypeConverter) null, (object) null, string.Empty, LocalizationHolder.rm.GetString("DatabaseConfigurator_210"), false, true, false));
  }

  public void LoadProps()
  {
    bool aReadOnly = this.parentSchema.ReadOnly;
    ((PropDescriptor) this.PropDescriptorCollection[LCLinkObject.notePropID]).SetReadOnly(aReadOnly);
    this.PropDescriptorCollection[LCLinkObject.notePropID].SetValue((object) this, (object) this.lcStepLinkProperties.Note);
  }

  public void SaveProps()
  {
    this.lcStepLinkProperties.Note = this.PropDescriptorCollection[LCLinkObject.notePropID].GetValue((object) this).ToString();
  }

  public void ChangeEvent(EventArgs e)
  {
  }

  public bool Apply(object oldId) => true;

  public void Cancel()
  {
  }

  public bool IsNode => false;

  public bool IsLink => true;
}
