// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.AutoSelAttrCollTypeConverter
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

[Serializable]
internal class AutoSelAttrCollTypeConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
  {
    return destType == typeof (string);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destType)
  {
    return destType == typeof (string) && value is ICollection collection && collection.Count > 0 ? (object) LocalizationHolder.rm.GetString("AutoSelection.Client_33") : (object) "< ... >";
  }
}
