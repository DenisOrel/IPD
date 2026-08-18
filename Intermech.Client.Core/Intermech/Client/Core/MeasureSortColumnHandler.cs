
// Type: Intermech.Client.Core.MeasureSortColumnHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;


namespace Intermech.Client.Core;

/// <summary>
/// Обработчик сортировки данных в колонках со значениями, выраженными в ед.измерения
/// </summary>
internal class MeasureSortColumnHandler : SortColumnHandler
{
  public MeasureSortColumnHandler()
    : base("_m")
  {
  }

  public override bool Handle(
    DataTable table,
    int columnIndex,
    ColumnAttributeData attrData,
    out string sortSQL)
  {
    sortSQL = string.Empty;
    if (attrData.AttributeType != FieldTypes.ftMeasured)
      return false;
    DataColumn column1 = this.NewAdditionalColumn(table, typeof (double));
    DataColumn column2 = this.NewAdditionalColumn(table, typeof (long));
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      string mValue = Convert.ToString(table.Rows[index][columnIndex]);
      if (!(mValue == string.Empty))
      {
        MeasuredValue measuredValue = MeasureHelper.ConvertToMeasuredValue(mValue);
        if (measuredValue != null)
        {
          table.Rows[index][column1] = (object) MeasureHelper.ConvertToBaseMeasure(measuredValue).Value;
          MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(measuredValue);
          table.Rows[index][column2] = (object) descriptor.PhysicalQuantityID;
        }
      }
    }
    table.AcceptChanges();
    sortSQL = $"{this.ColumnNameInSQL(column2.ColumnName)} ASC, {this.ColumnNameInSQL(column1.ColumnName)} {this.GetSortOrdersString(attrData.Sort)}";
    return true;
  }
}
