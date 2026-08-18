
// Type: Intermech.PropertyEditors.DateTimeTypeConverter
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

/// <summary>Выбор из списка выпадающих дат</summary>
public class DateTimeTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getDateList;

  public DateTimeTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public DateTimeTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
  }

  public DateTimeTypeConverter(EventsHolder.GetListDelegate aGetDateList)
    : this(aGetDateList, true)
  {
  }

  public DateTimeTypeConverter(EventsHolder.GetListDelegate aGetDateList, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getDateList = aGetDateList;
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = (ArrayList) null;
    if (this.iPossibleValuesHolder != null)
    {
      DataTable possibleValues = this.iPossibleValuesHolder.GetPossibleValues(context);
      if (possibleValues == null)
        return (ArrayList) null;
      valuesCustomList = new ArrayList();
      if (this.valueCanNull)
        valuesCustomList.Insert(0, (object) new DateTimePropertyClass());
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        try
        {
          valuesCustomList.Add((object) new DateTimePropertyClass(Convert.ToDateTime(row["F_DATE_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null));
        }
        catch
        {
        }
      }
    }
    if (this.getDateList != null)
    {
      valuesCustomList = this.getDateList((object) this, (object) typeof (DateTime));
      valuesCustomList?.Insert(0, (object) new DateTimePropertyClass());
    }
    return valuesCustomList;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) && context.PropertyDescriptor.PropertyType == typeof (DateTimePropertyClass) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string) || !(context.PropertyDescriptor.PropertyType == typeof (DateTimePropertyClass)))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == string.Empty ? (object) null : (object) new DateTimePropertyClass(Convert.ToDateTime(value));
  }
}
