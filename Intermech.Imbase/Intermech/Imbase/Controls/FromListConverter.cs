// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.FromListConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class FromListConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    object obj = (object) null;
    string key = Convert.ToString(value);
    if (!string.IsNullOrEmpty(key) && context != null)
      obj = !(context.Instance is RestructuringPropGridDescriptor instance) || instance.PossibleValues == null || !instance.PossibleValues.ContainsKey(key) ? value : instance.PossibleValues[key];
    return obj;
  }
}
