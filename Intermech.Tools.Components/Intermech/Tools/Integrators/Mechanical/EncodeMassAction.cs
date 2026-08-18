// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.EncodeMassAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Text;
using Intermech.Tools.Components.Properties;
using System;
using System.Globalization;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class EncodeMassAction(
  ValueBag source,
  StringKey sourceKey,
  ValueBag target,
  StringKey targetKey) : TransferValueRecordAction(source, sourceKey, target, targetKey)
{
  public bool IsOpenMetadataTarget { get; set; }

  public override void Perform()
  {
    ValueRecord valueRecord1 = this.Source.Find(this.SourceKey);
    if (valueRecord1 == null)
      return;
    ValueRecord valueRecord2 = this.Target.Find(this.TargetKey);
    if (valueRecord2 != null)
    {
      if (valueRecord2.DataType == typeof (MeasuredValue))
        this.EncodeAsMeasuredValue(valueRecord1);
      else if (valueRecord2.DataType == typeof (double) || valueRecord2.DataType == typeof (float))
      {
        this.EncodeAsReal(valueRecord1, valueRecord2.DataType);
      }
      else
      {
        if (!(valueRecord2.DataType == typeof (string)))
          throw new CantUpdateAttributeValueException(valueRecord1, (Exception) new InvalidCastException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_225"), (object) valueRecord2.Key, (object) valueRecord2.DataType)));
        this.EncodeAsString(valueRecord1);
      }
    }
    else
      this.EncodeAsString(valueRecord1);
  }

  private void EncodeAsMeasuredValue(ValueRecord massItem)
  {
    if (!this.Target.CanUpdate(this.TargetKey, massItem.DataType, this.IsOpenMetadataTarget))
      throw new CantUpdateAttributeValueException(massItem);
    this.Target.Update(this.TargetKey, massItem.Value, this.IsOpenMetadataTarget);
    this.Target.CopyFlag(this.TargetKey, massItem.Flags, NamedFlags.ThrowSetException);
  }

  private void EncodeAsReal(ValueRecord massItem, Type dataType)
  {
    if (!this.Target.CanUpdate(this.TargetKey, dataType, this.IsOpenMetadataTarget) || !this.Target.CanUpdate((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, typeof (string), this.IsOpenMetadataTarget))
      throw new CantUpdateAttributeValueException(massItem);
    MeasuredValue measuredValue = massItem.Read<MeasuredValue>((MeasuredValue) null);
    object newValue = Convert.ChangeType((object) measuredValue.Value, dataType);
    string shortName = MeasureHelper.FindDescriptor(measuredValue.MeasureID).ShortName;
    this.Target.Update(this.TargetKey, newValue, this.IsOpenMetadataTarget);
    this.Target.CopyFlag(this.TargetKey, massItem.Flags, NamedFlags.ThrowSetException);
    this.Target.Update((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, (object) shortName, this.IsOpenMetadataTarget);
    this.Target.CopyFlag((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, massItem.Flags, NamedFlags.ThrowSetException);
  }

  private void EncodeAsString(ValueRecord massItem)
  {
    string str = TextServices.Trim(this.Target.Read<string>(this.TargetKey, (string) null));
    if (!string.IsNullOrEmpty(str) && !str.Contains(" "))
    {
      if (!this.Target.CanUpdate(this.TargetKey, typeof (string), this.IsOpenMetadataTarget) || !this.Target.CanUpdate((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, typeof (string), this.IsOpenMetadataTarget))
        throw new CantUpdateAttributeValueException(massItem);
      MeasuredValue measuredValue = massItem.Read<MeasuredValue>((MeasuredValue) null);
      string newValue = measuredValue.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      string shortName = MeasureHelper.FindDescriptor(measuredValue.MeasureID).ShortName;
      this.Target.Update(this.TargetKey, (object) newValue, this.IsOpenMetadataTarget);
      this.Target.CopyFlag(this.TargetKey, massItem.Flags, NamedFlags.ThrowSetException);
      this.Target.Update((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, (object) shortName, this.IsOpenMetadataTarget);
      this.Target.CopyFlag((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, massItem.Flags, NamedFlags.ThrowSetException);
    }
    else
    {
      if (!this.Target.CanUpdate(this.TargetKey, typeof (string), this.IsOpenMetadataTarget))
        throw new CantUpdateAttributeValueException(massItem);
      this.Target.Update(this.TargetKey, (object) massItem.Read<MeasuredValue>((MeasuredValue) null).Caption, this.IsOpenMetadataTarget);
      this.Target.CopyFlag(this.TargetKey, massItem.Flags, NamedFlags.ThrowSetException);
      if (!this.Target.CanUpdate((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, typeof (string), false))
        return;
      this.Target.Update((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, (object) string.Empty, false);
      this.Target.CopyFlag((StringKey) CADDocumentResources.EMB_MassMeasureAttribute, massItem.Flags, NamedFlags.ThrowSetException);
    }
  }
}
