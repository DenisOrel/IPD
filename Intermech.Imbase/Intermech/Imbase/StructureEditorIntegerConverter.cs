// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorIntegerConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorIntegerConverter : Int64Converter
{
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()) ? value : base.ConvertFrom(context, culture, value);
  }
}
