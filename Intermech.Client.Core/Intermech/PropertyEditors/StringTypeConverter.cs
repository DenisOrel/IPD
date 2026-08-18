
// Type: Intermech.PropertyEditors.StringTypeConverter
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

/// <summary>Summary description for StringEditor.</summary>
public class StringTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getStringList;

  public StringTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public StringTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
  }

  public StringTypeConverter(EventsHolder.GetListDelegate aGetStringList)
    : this(aGetStringList, true)
  {
  }

  public StringTypeConverter(EventsHolder.GetListDelegate aGetStringList, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getStringList = aGetStringList;
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = (ArrayList) null;
    if (this.iPossibleValuesHolder != null)
    {
      DataTable possibleValues = this.iPossibleValuesHolder.GetPossibleValues(context);
      if (possibleValues != null)
      {
        valuesCustomList = new ArrayList();
        if (this.valueCanNull)
          valuesCustomList.Insert(0, (object) new StringPropertyClass());
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          try
          {
            valuesCustomList.Add((object) new StringPropertyClass(row["F_STRING_VALUE"].ToString(), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null));
          }
          catch
          {
          }
        }
      }
    }
    if (this.getStringList != null)
    {
      valuesCustomList = this.getStringList((object) this, (object) typeof (string));
      valuesCustomList?.Insert(0, (object) new StringPropertyClass());
    }
    return valuesCustomList;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) && context.PropertyDescriptor.PropertyType == typeof (StringPropertyClass) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string) || !(context.PropertyDescriptor.PropertyType == typeof (StringPropertyClass)))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == string.Empty ? (object) null : (object) new StringPropertyClass((string) value);
  }
}
