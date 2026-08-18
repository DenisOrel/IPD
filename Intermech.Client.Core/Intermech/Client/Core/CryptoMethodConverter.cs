
// Type: Intermech.Client.Core.CryptoMethodConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core;

public class CryptoMethodConverter : TypeConverter
{
  private static string _noneString = LocalizationHolder.rm.GetString("Client.Core_992");
  private static string _SHA1String = "SHA1";
  private static string _MD5String = "MD5";
  private static TypeConverter.StandardValuesCollection values;

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (int) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(value is int num))
      return base.ConvertTo(context, culture, value, destinationType);
    if (num == 1)
      return (object) CryptoMethodConverter._SHA1String;
    return num == 2 ? (object) CryptoMethodConverter._MD5String : (object) CryptoMethodConverter._noneString;
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value is string)
    {
      string str = ((string) value).Trim();
      try
      {
        if (str == CryptoMethodConverter._noneString)
          return (object) 0;
        if (str == CryptoMethodConverter._SHA1String)
          return (object) 1;
        if (str == CryptoMethodConverter._MD5String)
          return (object) 2;
      }
      catch (FormatException ex)
      {
        object[] objArray = new object[2]
        {
          (object) (string) value,
          (object) "int"
        };
        throw new FormatException(sc_4926.ssp_imclient_4928(), (Exception) ex);
      }
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (CryptoMethodConverter.values == null)
      CryptoMethodConverter.values = new TypeConverter.StandardValuesCollection((ICollection) new object[3]
      {
        (object) CryptoMethodConverter._noneString,
        (object) CryptoMethodConverter._SHA1String,
        (object) CryptoMethodConverter._MD5String
      });
    return CryptoMethodConverter.values;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
