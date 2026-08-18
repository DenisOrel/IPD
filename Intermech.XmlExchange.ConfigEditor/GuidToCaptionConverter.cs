// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.GuidToCaptionConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class GuidToCaptionConverter : TypeConverter
{
  private Dictionary<Guid, string> _guidToCaptionDictionary = new Dictionary<Guid, string>();

  public GuidToCaptionConverter(Type type)
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
      return value;
    if (result == Guid.Empty)
      return (object) string.Empty;
    string str;
    if (this._guidToCaptionDictionary.TryGetValue(result, out str))
      return (object) str;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return value;
      IDBObject dbObject = session.GetObject(result, false);
      str = dbObject == null ? result.ToString() : dbObject.Caption;
      this._guidToCaptionDictionary.Add(result, str);
      return (object) str;
    }
  }
}
