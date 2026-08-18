// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.TimeZoneConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class TimeZoneConverter : TypeConverter
{
  private ReadOnlyCollection<TimeZoneInfo> _timeZones = TimeZoneInfo.GetSystemTimeZones();

  public TimeZoneConverter(Type type)
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value != null && value.ToString().Length > 0)
    {
      foreach (TimeZoneInfo timeZone in this._timeZones)
      {
        if (value.ToString() == timeZone.Id)
          return (object) timeZone.DisplayName;
      }
    }
    return value;
  }
}
