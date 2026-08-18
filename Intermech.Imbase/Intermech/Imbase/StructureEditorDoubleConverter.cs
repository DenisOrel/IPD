// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorDoubleConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorDoubleConverter : DoubleConverter
{
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
      return value;
    string str = value.ToString().Replace(',', '.');
    return base.ConvertFrom(context, CultureInfo.InvariantCulture, (object) str);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return value == null || value == DBNull.Value ? (object) null : base.ConvertTo(context, culture, value, destinationType);
  }
}
