
// Type: Intermech.PropertyEditors.RelationTypeConverter
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

public class RelationTypeConverter : DropDownTypeConverter
{
  public RelationTypeConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public RelationTypeConverter(EventsHolder.GetListDelegate getListDelegate)
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
    return value.GetType() == typeof (string) ? (object) new RelationTypePropertyClass(DataHolders.RelationTypesHolder.GetIDbyName((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    foreach (DataRow row in (InternalDataCollectionBase) DataHolders.RelationTypesHolder.DataTable.Rows)
      valuesCustomList.Add((object) new RelationTypePropertyClass(Convert.ToInt32(row["F_RELATION_TYPE"])));
    return valuesCustomList;
  }
}
