
// Type: Intermech.PropertyEditors.LanguagesConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;


namespace Intermech.PropertyEditors;

public class LanguagesConverter : TypeConverter
{
  private LanguagesHolder _langHolder;

  public LanguagesConverter() => this._langHolder = DataHolders.LanguagesHolder;

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string ? value : base.ConvertFrom(context, culture, value);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return typeof (string) == destinationType || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(typeof (string) == destinationType) || !(value is string id))
      return base.ConvertTo(context, culture, value, destinationType);
    if (id.Length == 0)
      return (object) this._langHolder.GetNamebyID(id);
    StringBuilder stringBuilder = new StringBuilder(32 /*0x20*/);
    for (int index = 0; index < id.Length; ++index)
    {
      if (stringBuilder.Length != 0)
        stringBuilder.Append(", ");
      stringBuilder.Append(this._langHolder.GetNamebyID(id[index].ToString()));
    }
    return (object) stringBuilder.ToString();
  }
}
