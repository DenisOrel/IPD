
// Type: Intermech.Search.UI.VirtualTree.IRowBindingExtension
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;


namespace Intermech.Search.UI.VirtualTree;

public interface IRowBindingExtension
{
  void GetCellData(Row row, ColumnBase column, CellData cellData);

  void GetRowData(Row row, RowData rowData);

  CellWidgetBase GetCellWidget(ColumnBase column);
}
