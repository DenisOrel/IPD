// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.MeasureTypeConverter
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Конвертор для ручного ввода MeasuredValues только одной физической величины
/// </summary>
public class MeasureTypeConverter : TypeConverter
{
  private long _physID = -1;
  private List<long> _MeasureList;
  private MeasureDescriptor defMeasureId;

  public long PhysID => this._physID;

  public MeasureTypeConverter(long physID)
  {
    this._physID = physID;
    this._MeasureList = new List<long>();
    if (this._physID == -1L)
      return;
    foreach (MeasureDescriptor measure in MeasureHelper.Measures)
    {
      if (measure.PhysicalQuantityID == physID)
        this._MeasureList.Add(measure.MeasureID);
    }
    this.defMeasureId = MeasureHelper.GetDefaultMeasure(this._physID);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string))
      return base.ConvertFrom(context, culture, value);
    try
    {
      return (object) MeasureHelper.ConvertToMeasuredValue((string) value, this.defMeasureId, true);
    }
    catch
    {
      return (object) null;
    }
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType == typeof (string) && value is MeasuredValue ? (object) ((MeasuredValue) value).ToString() : base.ConvertTo(context, culture, value, destinationType);
  }

  public override bool IsValid(ITypeDescriptorContext context, object value)
  {
    MeasuredValue measuredValue = (MeasuredValue) null;
    if (value is MeasuredValue)
      measuredValue = (MeasuredValue) value;
    else if (value is string)
      measuredValue = MeasureHelper.ConvertToMeasuredValue((string) value, this.defMeasureId, false);
    return measuredValue != null && this._MeasureList.Contains(measuredValue.MeasureID) || base.IsValid(context, value);
  }
}
