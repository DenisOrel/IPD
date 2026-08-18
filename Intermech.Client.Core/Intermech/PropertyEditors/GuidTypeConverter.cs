
// Type: Intermech.PropertyEditors.GuidTypeConverter
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
public class GuidTypeConverter : DropDownTypeConverter
{
  private IPossibleValuesHolder iPossibleValuesHolder;
  private EventsHolder.GetListDelegate getGuidList;

  public GuidTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder)
    : this(aIPossibleValuesHolder, true)
  {
  }

  public GuidTypeConverter(IPossibleValuesHolder aIPossibleValuesHolder, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.iPossibleValuesHolder = aIPossibleValuesHolder;
  }

  public GuidTypeConverter(EventsHolder.GetListDelegate aGetGuidList)
    : this(aGetGuidList, true)
  {
  }

  public GuidTypeConverter(EventsHolder.GetListDelegate aGetGuidList, bool valCanNull)
    : base((EventsHolder.GetListDelegate) null, valCanNull)
  {
    this.getGuidList = aGetGuidList;
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
          valuesCustomList.Insert(0, (object) new GuidPropertyClass());
        foreach (DataRow row in (InternalDataCollectionBase) possibleValues.Rows)
        {
          try
          {
            valuesCustomList.Add((object) new GuidPropertyClass(new Guid(Convert.ToString(row["F_STRING_VALUE"])), Convert.ToString(row["F_DESCRIPTION"]), (DataTable) null));
          }
          catch
          {
          }
        }
      }
    }
    if (this.getGuidList != null)
    {
      valuesCustomList = this.getGuidList((object) this, (object) typeof (Guid));
      valuesCustomList?.Insert(0, (object) new GuidPropertyClass());
    }
    return valuesCustomList;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) && context.PropertyDescriptor.PropertyType == typeof (GuidPropertyClass) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (!(value is string) || !(context.PropertyDescriptor.PropertyType == typeof (GuidPropertyClass)))
      return base.ConvertFrom(context, culture, value);
    return value.ToString() == string.Empty ? (object) null : (object) new GuidPropertyClass(new Guid((string) value));
  }
}
