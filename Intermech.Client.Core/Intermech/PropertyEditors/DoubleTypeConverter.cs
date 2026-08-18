
// Type: Intermech.PropertyEditors.DoubleTypeConverter
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

/// <summary>Summary description for DoubleEditor.</summary>
public class DoubleTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getDoubleList;

  public DoubleTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public DoubleTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
  }

  public DoubleTypeConverter(EventsHolder.GetListDelegate aGetDoubleList)
    : this(aGetDoubleList, true)
  {
  }

  public DoubleTypeConverter(EventsHolder.GetListDelegate aGetDoubleList, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getDoubleList = aGetDoubleList;
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
        valuesCustomList.Insert(0, (object) new DoublePropertyClass());
      foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
      {
        try
        {
          valuesCustomList.Add((object) new DoublePropertyClass(Convert.ToDouble(row["F_DOUBLE_VALUE"]), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null));
        }
        catch
        {
        }
      }
    }
    if (this.getDoubleList != null)
    {
      valuesCustomList = this.getDoubleList((object) this, (object) typeof (double));
      valuesCustomList?.Insert(0, (object) new DoublePropertyClass());
    }
    return valuesCustomList;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) && context.PropertyDescriptor.PropertyType == typeof (DoublePropertyClass) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string) || !(context.PropertyDescriptor.PropertyType == typeof (DoublePropertyClass)))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == string.Empty ? (object) null : (object) new DoublePropertyClass(Convert.ToDouble(value));
  }
}
