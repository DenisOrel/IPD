
// Type: Intermech.PropertyEditors.IntTypeConverter
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

/// <summary>Summary description for IntEditor.</summary>
public class IntTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getIntList;

  public IntTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public IntTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
  }

  public IntTypeConverter(EventsHolder.GetListDelegate aGetIntList)
    : this(aGetIntList, true)
  {
  }

  public IntTypeConverter(EventsHolder.GetListDelegate aGetIntList, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getIntList = aGetIntList;
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
          valuesCustomList.Insert(0, (object) new Int64PropertyClass());
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          try
          {
            valuesCustomList.Add((object) new Int64PropertyClass(Convert.ToInt64(row["F_INTEGER_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null));
          }
          catch
          {
          }
        }
      }
    }
    if (this.getIntList != null)
    {
      valuesCustomList = this.getIntList((object) this, (object) typeof (long));
      valuesCustomList?.Insert(0, (object) new Int64PropertyClass());
    }
    return valuesCustomList;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) && context.PropertyDescriptor.PropertyType == typeof (Int64PropertyClass) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string) || !(context.PropertyDescriptor.PropertyType == typeof (Int64PropertyClass)))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == string.Empty ? (object) null : (object) new Int64PropertyClass(Convert.ToInt64(value));
  }
}
