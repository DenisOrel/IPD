// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.LcStepConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class LcStepConverter : TypeConverter
{
  public LcStepConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value == null)
      return (object) string.Empty;
    Guid result;
    if (!Guid.TryParse(value.ToString(), out result))
      return (object) string.Empty;
    return result == Guid.Empty ? (object) string.Empty : (object) MetaDataHelper.GetLCStepName(result);
  }
}
