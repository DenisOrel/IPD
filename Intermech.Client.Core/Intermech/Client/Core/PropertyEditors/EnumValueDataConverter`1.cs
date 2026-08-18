
// Type: Intermech.Client.Core.PropertyEditors.EnumValueDataConverter`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.PropertyEditors;

/// <summary>Custom converter for EnumValueData</summary>
public class EnumValueDataConverter<T> : DropDownTypeConverter
{
  /// <summary>Конструктор</summary>
  public EnumValueDataConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="getListDelegate"></param>
  public EnumValueDataConverter(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this.sortValues = true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="sourceType"></param>
  /// <returns></returns>
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value is string ? value : base.ConvertFrom(context, culture, value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    ArrayList arrayList = new ArrayList((ICollection) Enum.GetValues(typeof (T)));
    for (int index = 0; index < arrayList.Count; ++index)
    {
      if (Convert.ToInt32(arrayList[index]) != 0)
        valuesCustomList.Add((object) new EnumValueData<T>(arrayList[index]));
    }
    return valuesCustomList;
  }
}
