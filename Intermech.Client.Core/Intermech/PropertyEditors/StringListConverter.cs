
// Type: Intermech.PropertyEditors.StringListConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class StringListConverter : TypeConverter
{
  private ArrayList values;
  private bool _exclusive = true;
  private StringListConverterEvents.GetStandardValuesDelegate _GetStandardValuesEvent;

  public StringListConverter()
    : this(new ArrayList((ICollection) new string[0]), (StringListConverterEvents.GetStandardValuesDelegate) null, true)
  {
  }

  public StringListConverter(ArrayList values)
    : this(values, (StringListConverterEvents.GetStandardValuesDelegate) null, true)
  {
  }

  public StringListConverter(
    StringListConverterEvents.GetStandardValuesDelegate _GetStandardValuesEvent)
    : this((ArrayList) null, _GetStandardValuesEvent, true)
  {
  }

  public StringListConverter(
    ArrayList values,
    StringListConverterEvents.GetStandardValuesDelegate _GetStandardValuesEvent,
    bool exclusive)
  {
    this.values = values;
    this._exclusive = exclusive;
    this._GetStandardValuesEvent = _GetStandardValuesEvent;
  }

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
  {
    return this._exclusive;
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (this._GetStandardValuesEvent != null)
      this.values = this._GetStandardValuesEvent((object) this);
    return new TypeConverter.StandardValuesCollection((ICollection) this.values);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value.GetType() == typeof (string) ? (object) (string) value : base.ConvertFrom(context, culture, value);
  }
}
