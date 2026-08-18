
// Type: Intermech.Client.Core.FormDesigner.Controls.Guid2ObjectCaptionConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Перевод Guid в заголовок объекта.</summary>
public class Guid2ObjectCaptionConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    object empty = (object) string.Empty;
    if (!(destinationType == typeof (string)) || !(value is Guid objectGUID))
      return base.ConvertTo(context, culture, value, destinationType);
    if (objectGUID == Guid.Empty)
      return (object) string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectGUID, false);
      return dbObject == null ? (object) string.Empty : (string.IsNullOrEmpty(dbObject.Caption) ? (object) string.Format(LocalizationHolder.rm.GetString("Client.Core_1142"), (object) dbObject.ObjectID) : (object) dbObject.Caption);
    }
  }
}
