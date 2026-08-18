// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorDateTimeConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorDateTimeConverter : DateTimeConverter
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null || value == DBNull.Value)
      return (object) null;
    DateTime result = DateTime.Now;
    if (!DateTime.TryParse(value.ToString(), out result))
      return base.ConvertTo(context, culture, value, destinationType);
    return !(result != DateTime.MinValue) ? (object) null : base.ConvertTo(context, culture, (object) result.ToShortDateString(), destinationType);
  }
}
