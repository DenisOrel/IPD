// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ExtraDataModeConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ExtraDataModeConverter : TypeConverter
{
  public ExtraDataModeConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    XmlExportExtraDataMode result;
    if (!Enum.TryParse<XmlExportExtraDataMode>(value.ToString(), out result))
      return value;
    if (result == XmlExportExtraDataMode.None)
      return (object) XmlExportExtraDataMode.None.GetDescription<XmlExportExtraDataMode>();
    List<string> values = new List<string>();
    foreach (XmlExportExtraDataMode flag in Enum.GetValues(typeof (XmlExportExtraDataMode)))
    {
      if (flag != XmlExportExtraDataMode.None && result.HasFlag((Enum) flag))
        values.Add(flag.GetDescription<XmlExportExtraDataMode>());
    }
    return values.Count > 0 ? (object) string.Join("; ", (IEnumerable<string>) values) : value;
  }
}
