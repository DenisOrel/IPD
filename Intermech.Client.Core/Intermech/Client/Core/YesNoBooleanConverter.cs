
// Type: Intermech.Client.Core.YesNoBooleanConverter
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

public class YesNoBooleanConverter : TypeConverter
{
  private static string YesString = LocalizationHolder.rm.GetString("Client.Core_246");
  private static string NoString = LocalizationHolder.rm.GetString("Client.Core_247");
  protected string _yesString;
  protected string _noString;
  private static TypeConverter.StandardValuesCollection values;

  public YesNoBooleanConverter()
  {
    this._yesString = YesNoBooleanConverter.YesString;
    this._noString = YesNoBooleanConverter.NoString;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (bool) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    switch (value)
    {
      case bool flag:
        return flag ? (object) this._yesString : (object) this._noString;
      case string _:
        if (destinationType == typeof (bool))
          return value.Equals((object) this._yesString) ? (object) true : (object) false;
        break;
    }
    return base.ConvertTo(context, culture, value, destinationType);
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
        if (str == this._yesString)
          return (object) true;
        if (str == this._noString)
          return (object) false;
      }
      catch (FormatException ex)
      {
        object[] objArray = new object[2]
        {
          (object) (string) value,
          (object) "Boolean"
        };
        throw new FormatException(sc_4926.ssp_imclient_4927(), (Exception) ex);
      }
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (YesNoBooleanConverter.values == null)
      YesNoBooleanConverter.values = new TypeConverter.StandardValuesCollection((ICollection) new object[2]
      {
        (object) true,
        (object) false
      });
    return YesNoBooleanConverter.values;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
