// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImbaseItemConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ImbaseItemConverter : TypeConverter
{
  public ImbaseItemConverter(Type type)
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
    return !(value is XmlExchangeImportImbaseItem importImbaseItem) ? value : (object) importImbaseItem.Caption;
  }
}
