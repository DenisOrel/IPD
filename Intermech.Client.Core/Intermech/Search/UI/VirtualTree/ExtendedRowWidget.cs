
// Type: Intermech.Search.UI.VirtualTree.ExtendedRowWidget
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;


namespace Intermech.Search.UI.VirtualTree;

public sealed class ExtendedRowWidget(PanelWidget panelWidget, Row row) : RowWidget(panelWidget, row)
{
  public override CellWidget GetCellWidget(Column column)
  {
    if (this.Tree.GetRowBinding(this.Row) is RowBindingBase rowBinding)
    {
      CellWidget cellWidget = rowBinding.GetCellWidget(column as ColumnBase);
      if (cellWidget != null)
        return cellWidget;
    }
    return (CellWidget) new ExtendedCellWidget((RowWidget) this, column);
  }

  protected override int MainCellOffset
  {
    get
    {
      int mainCellOffset = base.MainCellOffset;
      if (this.RowData != null)
        mainCellOffset += ((Intermech.Search.UI.VirtualTree.VirtualTree) this.Tree).IconWidth;
      return mainCellOffset;
    }
  }
}
