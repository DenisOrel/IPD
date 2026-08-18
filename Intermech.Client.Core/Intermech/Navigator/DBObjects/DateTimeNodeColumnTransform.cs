
// Type: Intermech.Navigator.DBObjects.DateTimeNodeColumnTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using System;
using System.Globalization;


namespace Intermech.Navigator.DBObjects;

public sealed class DateTimeNodeColumnTransform : INodeColumnTransform
{
  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    if (sourceValue is DateTime && column != null)
    {
      IMSAttributeType attribute = column.Attribute;
      if (attribute != null)
      {
        DateTime dateTime = (DateTime) sourceValue;
        return attribute.Mask == Intermech.Consts.OnlyDateFunction || dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0 ? (object) dateTime.ToString("d", (IFormatProvider) CultureInfo.CurrentCulture) : (object) dateTime.ToString("G", (IFormatProvider) CultureInfo.CurrentCulture);
      }
    }
    return (object) string.Empty;
  }
}
