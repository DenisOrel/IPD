// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.PossibleValuesFromListConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class PossibleValuesFromListConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return value == null || value == DBNull.Value || context == null ? (object) LocalizationHolder.rm.GetString("Imbase_AllValues") : (object) LocalizationHolder.rm.GetString("Imbase_SetFilter");
  }
}
