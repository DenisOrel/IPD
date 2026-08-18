
// Type: Intermech.Search.Versions.VersionRuleTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search.Versions;

public sealed class VersionRuleTypeConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(value is long))
      throw new ArgumentException();
    if (!(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject((long) value, false);
      return dbObject != null ? (object) dbObject.Caption : (object) $"Правило подбора версий #{(long) value} не найдено";
    }
  }
}
