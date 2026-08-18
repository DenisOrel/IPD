// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.EnumToXmlValueConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.Reflection;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

internal static class EnumToXmlValueConverter
{
  public static string GetEnumValue(Type enumType, string xmlValue)
  {
    FieldInfo[] fields = enumType.GetFields();
    foreach (FieldInfo fieldInfo in fields)
    {
      if (fieldInfo.Name == xmlValue)
        return fieldInfo.GetValue((object) fieldInfo.Name).ToString();
    }
    foreach (FieldInfo fieldInfo in fields)
    {
      XmlValueAttribute[] customAttributes = (XmlValueAttribute[]) fieldInfo.GetCustomAttributes(typeof (XmlValueAttribute), false);
      if (customAttributes.Length != 0 && customAttributes[0].XmlValue == xmlValue)
        return fieldInfo.GetValue((object) fieldInfo.Name).ToString();
    }
    return xmlValue;
  }

  public static string GetEnumXmlValue(this object value)
  {
    FieldInfo field = value.GetType().GetField(value.ToString());
    if (!(field != (FieldInfo) null))
      return value.ToString();
    XmlValueAttribute[] customAttributes = (XmlValueAttribute[]) field.GetCustomAttributes(typeof (XmlValueAttribute), false);
    return customAttributes.Length == 0 ? value.ToString() : customAttributes[0].XmlValue;
  }
}
