
// Type: Intermech.PropertyEditors.RequiredModesConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class RequiredModesConverter : DropDownTypeConverter
{
  public RequiredModesConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public RequiredModesConverter(EventsHolder.GetListDelegate getListDelegate)
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
    return value.GetType() == typeof (string) ? (object) new RequiredModePropertyClass(RequiredModesHelper.GetRequiredMode((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = (ArrayList) null;
    if (args.Length == 0)
    {
      valuesCustomList = new ArrayList((ICollection) Enum.GetValues(typeof (RequiredModes)));
      for (int index = 0; index < valuesCustomList.Count; ++index)
        valuesCustomList[index] = (object) new RequiredModePropertyClass((RequiredModes) valuesCustomList[index]);
    }
    else
    {
      AttributeTypePropertiesValidator propertiesValidator = (AttributeTypePropertiesValidator) args[0];
      if (propertiesValidator.RequiredMode == null)
        valuesCustomList = new ArrayList();
      else if (propertiesValidator.RequiredMode.Length != 0)
      {
        valuesCustomList = new ArrayList();
        for (int index = 0; index < propertiesValidator.RequiredMode.Length; ++index)
          valuesCustomList.Add((object) new RequiredModePropertyClass(propertiesValidator.RequiredMode[index]));
      }
    }
    return valuesCustomList;
  }
}
