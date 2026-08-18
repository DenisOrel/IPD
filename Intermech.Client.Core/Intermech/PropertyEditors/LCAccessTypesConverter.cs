
// Type: Intermech.PropertyEditors.LCAccessTypesConverter
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

public class LCAccessTypesConverter : DropDownTypeConverter
{
  public LCAccessTypesConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public LCAccessTypesConverter(EventsHolder.GetListDelegate getListDelegate)
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
    return value.GetType() == typeof (string) ? (object) new LCAccessTypePropertyClass(LCAccessTypesHelper.GetLCAccessType((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList((ICollection) Enum.GetValues(typeof (LCAccessTypes)));
    for (int index = 0; index < valuesCustomList.Count; ++index)
      valuesCustomList[index] = (object) new LCAccessTypePropertyClass((LCAccessTypes) valuesCustomList[index]);
    return valuesCustomList;
  }
}
