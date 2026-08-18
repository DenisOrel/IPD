
// Type: Intermech.PropertyEditors.StorageConverter
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

public class StorageConverter : DropDownTypeConverter
{
  public StorageConverter() => this.sortValues = true;

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return value.GetType() == typeof (string) ? (object) new StoragePropertyClass(DataHolders.StoragesHolder.GetIDbyName((string) value)) : base.ConvertFrom(context, culture, value);
  }

  public override ArrayList GetStandardValuesCustomList(
    ITypeDescriptorContext context,
    params object[] args)
  {
    ArrayList valuesCustomList = new ArrayList();
    valuesCustomList.Add((object) new StoragePropertyClass(0L));
    DataTable dataTable = DataHolders.StoragesHolder.LoadData(false);
    foreach (DataRow dataRow in dataTable.Select("", dataTable.Columns[1].ColumnName))
      valuesCustomList.Add((object) new StoragePropertyClass(Convert.ToInt64(dataRow[0]), Convert.ToString(dataRow[1])));
    return valuesCustomList;
  }
}
