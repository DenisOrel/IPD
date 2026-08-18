
// Type: Intermech.Search.UI.VirtualTree.StatusesRowBindingExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Navigator.Interfaces;
using Intermech.Search.Statuses;
using System;


namespace Intermech.Search.UI.VirtualTree;

public sealed class StatusesRowBindingExtension : RowBindingExtensionBase
{
  public override void GetCellData(Row row, ColumnBase column, CellData cellData)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (cellData == null)
      throw new ArgumentNullException(nameof (cellData));
    if (!(column.Tag is NodeColumn tag) || tag.ID != (object) "F_STATUSES")
      return;
    cellData.Value = (object) StatusesClientHelper.ConvertBytesToStatuses(row.Item);
  }

  public override CellWidgetBase GetCellWidget(ColumnBase column)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    return column.Tag is NodeColumn tag && tag.ID == (object) "F_STATUSES" ? (CellWidgetBase) new StatusesCellWidget((RowWidget) null, (Column) null) : (CellWidgetBase) null;
  }
}
