// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.SkipExistsConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Extensions;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class SkipExistsConverter : TypeConverter
{
  public SkipExistsConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    SkipExistsMode result;
    if (!Enum.TryParse<SkipExistsMode>(value.ToString(), out result))
      return value;
    if (result == SkipExistsMode.None)
      return (object) SkipExistsMode.None.GetDescription<SkipExistsMode>();
    List<string> values = new List<string>();
    foreach (SkipExistsMode flag in Enum.GetValues(typeof (SkipExistsMode)))
    {
      if (flag != SkipExistsMode.None && result.HasFlag((Enum) flag))
        values.Add(flag.GetDescription<SkipExistsMode>());
    }
    return values.Count > 0 ? (object) string.Join("; ", (IEnumerable<string>) values) : value;
  }
}
