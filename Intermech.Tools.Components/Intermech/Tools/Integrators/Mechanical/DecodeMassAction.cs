// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.DecodeMassAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Text;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class DecodeMassAction(
  ValueBag source,
  StringKey sourceKey,
  ValueBag target,
  StringKey targetKey) : TransferValueRecordAction(source, sourceKey, target, targetKey)
{
  public override void Perform()
  {
    ValueRecord massItem = this.Source.Find(this.SourceKey);
    if (massItem == null || massItem.IsNull)
      return;
    if (massItem.DataType == typeof (MeasuredValue))
      this.CopyMeasuredValue(massItem);
    else if (massItem.DataType == typeof (string))
      this.StringToMeasuredValue(massItem);
    else if (massItem.DataType == typeof (double) || massItem.DataType == typeof (float))
      this.RealToMeasuredValue(massItem);
    else
      this.ReportBadTypedItem(massItem);
  }

  private void CopyMeasuredValue(ValueRecord massItem)
  {
    if (!massItem.IsNull)
    {
      this.Target.Update(this.TargetKey, massItem.Value);
      this.Target.CopyFlag(this.TargetKey, massItem.Flags, NamedFlags.ThrowSetException);
    }
    else
      this.ReportBadValuedItem(massItem, new Exception("Mass can't be null."));
  }

  private void StringToMeasuredValue(ValueRecord massItem)
  {
    string str = TextServices.Trim(massItem.Read<string>(string.Empty).Trim());
    if (string.IsNullOrEmpty(str))
      this.Target.Update(this.TargetKey, (object) TypedNull.Instance(typeof (MeasuredValue)));
    else if (this.IsTableDrivenValue(str))
    {
      this.Target.Update(this.TargetKey, (object) TypedNull.Instance(typeof (MeasuredValue))).Flags.Set(MechanicalNamedFlags.TableDrivenValue);
    }
    else
    {
      try
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(str, this.DetectMeasure(), true);
        MeasureHelper.CorrectCaption(measuredValue);
        this.CheckIfMass(measuredValue);
        this.Target.Add(this.TargetKey, (object) measuredValue);
      }
      catch (KernelException ex)
      {
        this.ReportBadValuedItem(massItem, (Exception) ex);
      }
      catch (FormatException ex)
      {
        this.ReportBadValuedItem(massItem, (Exception) ex);
      }
    }
  }

  private bool IsTableDrivenValue(string massText)
  {
    return massText.StartsWith("см. табл", StringComparison.CurrentCultureIgnoreCase) || massText.StartsWith("см.табл", StringComparison.CurrentCultureIgnoreCase) || massText.Equals("см. тт", StringComparison.CurrentCultureIgnoreCase) || massText.Equals("см.тт", StringComparison.CurrentCultureIgnoreCase);
  }

  private void RealToMeasuredValue(ValueRecord massItem)
  {
    try
    {
      MeasuredValue mass = new MeasuredValue(massItem.Read<double>(0.0), this.DetectMeasure().MeasureID);
      this.CheckIfMass(mass);
      this.Target.Add(this.TargetKey, (object) mass);
    }
    catch (InvalidCastException ex)
    {
      this.ReportBadValuedItem(massItem, (Exception) ex);
    }
    catch (FormatException ex)
    {
      this.ReportBadValuedItem(massItem, (Exception) ex);
    }
  }

  private void CheckIfMass(MeasuredValue mass)
  {
    if (MeasureHelper.FindDescriptor(mass.MeasureID).PhysicalQuantityID != IDCache.Default.MassPhysQty.Id)
      throw new FormatException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_226"), (object) mass.Caption));
  }

  private MeasureDescriptor DetectMeasure()
  {
    ValueRecord valueRecord = this.Source.Find((StringKey) CADDocumentResources.EMB_MassMeasureAttribute);
    if (valueRecord != null && valueRecord.DataType == typeof (string))
    {
      MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(TextServices.Trim(valueRecord.Read<string>(string.Empty)));
      if (!descriptor.Empty)
        return descriptor;
    }
    MeasureDescriptor descriptor1 = MeasureHelper.FindDescriptor(IDCache.Default.KilogramMeasure.Id);
    return !descriptor1.Empty ? descriptor1 : throw new FaultException(LocalizationHolder.rm.GetString("Tools.Components_227"));
  }
}
