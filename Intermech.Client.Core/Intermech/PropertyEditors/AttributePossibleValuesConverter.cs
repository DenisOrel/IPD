
// Type: Intermech.PropertyEditors.AttributePossibleValuesConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class AttributePossibleValuesConverter : DropDownTypeConverter
{
  private Hashtable _attributePossibleValues;

  public AttributePossibleValuesConverter(Hashtable attributePossibleValues)
  {
    this._attributePossibleValues = attributePossibleValues;
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
    IDictionaryEnumerator enumerator = this._attributePossibleValues.GetEnumerator();
    while (enumerator.MoveNext())
    {
      if (enumerator.Value.ToString() == value.ToString())
        return (object) new AttributePossibleValuesClass(enumerator.Key, enumerator.Value.ToString());
    }
    return base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    IDictionaryEnumerator enumerator = this._attributePossibleValues.GetEnumerator();
    while (enumerator.MoveNext())
      valuesCustomList.Add((object) new AttributePossibleValuesClass(enumerator.Key, enumerator.Value.ToString()));
    return valuesCustomList;
  }
}
