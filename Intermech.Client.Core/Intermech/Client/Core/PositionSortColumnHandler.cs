
// Type: Intermech.Client.Core.PositionSortColumnHandler
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Data;
using System.Text.RegularExpressions;


namespace Intermech.Client.Core;

/// <summary>Обработчик сортировки данных в колонке Позиция</summary>
internal class PositionSortColumnHandler : SortColumnHandler
{
  private int _positionAttributeID;

  public PositionSortColumnHandler()
    : base("_pos")
  {
    this._positionAttributeID = MetaDataHelper.GetAttributeTypeID("cad00270-306c-11d8-b4e9-00304f19f545");
  }

  public override bool Handle(
    DataTable table,
    int columnIndex,
    ColumnAttributeData attrData,
    out string sortSQL)
  {
    sortSQL = string.Empty;
    if (attrData.AttributeID != this._positionAttributeID)
      return false;
    DataColumn column1 = this.NewAdditionalColumn(table, typeof (long));
    DataColumn column2 = this.NewAdditionalColumn(table, typeof (string));
    Regex regex = new Regex("^(?<digits>\\d*)(?<words>\\W*\\w*)$");
    for (int index = 0; index < table.Rows.Count; ++index)
    {
      string input = Convert.ToString(table.Rows[index][columnIndex]);
      if (!(input == string.Empty))
      {
        Match match = regex.Match(input);
        string str1 = match.Groups["digits"].Value;
        string str2 = match.Groups["words"].Value;
        if (str1 != string.Empty)
          table.Rows[index][column1] = (object) Convert.ToInt64(str1);
        if (str2 != string.Empty)
          table.Rows[index][column2] = (object) str2;
      }
    }
    table.AcceptChanges();
    sortSQL = string.Format("{0} {1}, {2} {1}", (object) this.ColumnNameInSQL(column1.ColumnName), (object) this.GetSortOrdersString(attrData.Sort), (object) this.ColumnNameInSQL(column2.ColumnName));
    return true;
  }
}
