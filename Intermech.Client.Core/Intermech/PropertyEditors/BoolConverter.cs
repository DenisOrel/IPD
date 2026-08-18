
// Type: Intermech.PropertyEditors.BoolConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class BoolConverter : DropDownTypeConverter
{
  public BoolConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public BoolConverter(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this.sortValues = true;
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
    if (!(value.GetType() == typeof (string)))
      return base.ConvertFrom(context, culture, value);
    return BoolSrv.CanBoolConvert((string) value) ? (object) new BoolPropertyClass(BoolSrv.BoolConvert((string) value)) : (object) new BoolPropertyClass(false, true);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    return new ArrayList()
    {
      (object) new BoolPropertyClass(true),
      (object) new BoolPropertyClass(false)
    };
  }
}
