// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ReqRevisionConverter
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.ECO.Client;

public class ReqRevisionConverter : DropDownTypeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string ? (object) new ReqRevisionClass((string) value) : base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType.Equals(typeof (string)) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType.Equals(typeof (string)) && value is ReqRevisionClass ? (object) value.ToString() : base.ConvertTo(context, culture, value, destinationType);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    Array values = Enum.GetValues(typeof (ReqRevision));
    for (int index = 0; index < values.Length; ++index)
      valuesCustomList.Add((object) new ReqRevisionClass((ReqRevision) values.GetValue(index)));
    return valuesCustomList;
  }
}
