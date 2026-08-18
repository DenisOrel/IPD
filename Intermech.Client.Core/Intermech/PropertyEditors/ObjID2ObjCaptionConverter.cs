
// Type: Intermech.PropertyEditors.ObjID2ObjCaptionConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

/// <summary>Перевод идентификатора объекта в заголовок объекта.</summary>
public class ObjID2ObjCaptionConverter : TypeConverter
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
    if (destinationType != typeof (string))
      return base.ConvertTo(context, culture, value, destinationType);
    switch (value)
    {
      case null:
        return (object) string.Empty;
      case long _:
        long result = -1;
        if (!long.TryParse(value.ToString(), out result))
          return (object) string.Empty;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(result);
          return objectInfo.Empty ? (object) string.Empty : (string.IsNullOrEmpty(objectInfo.Caption) ? (object) string.Format(LocalizationHolder.rm.GetString("Client.Core_1142"), (object) objectInfo.ObjectID) : (object) objectInfo.Caption);
        }
      case List<long> _:
        if (!(value is List<long> longList) || longList.Count == 0)
          return (object) string.Empty;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          List<string> values = new List<string>(longList.Count);
          foreach (long objectID in longList)
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectID);
            if (!objectInfo.Empty)
              values.Add(string.IsNullOrEmpty(objectInfo.Caption) ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1142"), (object) objectInfo.ObjectID) : objectInfo.Caption);
          }
          return (object) string.Join("; ", (IEnumerable<string>) values);
        }
      default:
        return (object) string.Empty;
    }
  }
}
