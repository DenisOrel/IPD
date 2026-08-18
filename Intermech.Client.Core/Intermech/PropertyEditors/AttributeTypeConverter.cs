
// Type: Intermech.PropertyEditors.AttributeTypeConverter
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

public class AttributeTypeConverter : DropDownTypeConverter
{
  private bool excludeSystemAttributes;
  private FieldTypes[] filterByTypes;
  private int[] excludeAttrId;

  public AttributeTypeConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public AttributeTypeConverter(EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this.sortValues = true;
  }

  public AttributeTypeConverter(bool aExcludeSystemAttributes)
    : this((EventsHolder.GetListDelegate) null)
  {
    this.excludeSystemAttributes = aExcludeSystemAttributes;
  }

  public AttributeTypeConverter(bool aExcludeSystemAttributes, FieldTypes[] aFilterByTypes)
    : this(aExcludeSystemAttributes)
  {
    this.filterByTypes = aFilterByTypes;
  }

  public AttributeTypeConverter(
    bool aExcludeSystemAttributes,
    FieldTypes[] aFilterByTypes,
    int[] aExcludeAttrId)
    : this(aExcludeSystemAttributes, aFilterByTypes)
  {
    this.excludeAttrId = aExcludeAttrId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aExcludeSystemAttributes">при показе всех атрибутов выдавать все или без системных</param>
  /// <param name="getListDelegate"></param>
  public AttributeTypeConverter(
    bool aExcludeSystemAttributes,
    EventsHolder.GetListDelegate getListDelegate)
    : base(getListDelegate)
  {
    this.excludeSystemAttributes = aExcludeSystemAttributes;
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
    return value.GetType() == typeof (string) ? (object) new AttributePropertyClass(DataHolders.AttributesHolder.GetIDByName((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    foreach (DataRow dataRow in DataHolders.AttributesHolder.DataTable.Select(""))
    {
      int int32 = Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]);
      if ((this.excludeAttrId == null || Array.IndexOf<int>(this.excludeAttrId, int32) == -1) && (!this.excludeSystemAttributes || int32 >= 0) && (this.filterByTypes == null || Array.IndexOf<FieldTypes>(this.filterByTypes, (FieldTypes) Convert.ToInt32(dataRow["F_ATTRIBUTE_TYPE"])) != -1))
        valuesCustomList.Add((object) new AttributePropertyClass(int32));
    }
    return valuesCustomList;
  }

  public override int Compare(object x, object y)
  {
    if (x == null && y == null)
      return 0;
    if (x == null)
      return -1;
    return y == null ? 1 : DropDownTypeConverter._comparer.Compare((object) ((AttributePropertyClass) x).ToStringPrim(true), (object) ((AttributePropertyClass) y).ToStringPrim(true));
  }
}
