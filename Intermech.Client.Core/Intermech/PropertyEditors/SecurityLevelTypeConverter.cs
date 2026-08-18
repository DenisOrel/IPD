
// Type: Intermech.PropertyEditors.SecurityLevelTypeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;


namespace Intermech.PropertyEditors;

public class SecurityLevelTypeConverter : DropDownTypeConverter
{
  public SecurityLevelTypeConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  private SecurityLevelTypeConverter(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
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
    return value.GetType() == typeof (string) ? (object) new SecurityLevelPropertyClass(SecurityLevelHolder.GetSecurityLevelByDescription(value.ToString())) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    int maxSecurityLevel = SecurityLevelHolder.MaxSecurityLevel;
    ArrayList valuesCustomList = new ArrayList();
    foreach (DataRow dataRow in SecurityLevelHolder.SecurityLevelDataTable.Select("", "F_INTEGER_VALUE ASC"))
    {
      int int16 = (int) Convert.ToInt16(dataRow["F_INTEGER_VALUE"]);
      if (int16 <= maxSecurityLevel)
        valuesCustomList.Add((object) new SecurityLevelPropertyClass(int16));
      else
        break;
    }
    return valuesCustomList;
  }
}
