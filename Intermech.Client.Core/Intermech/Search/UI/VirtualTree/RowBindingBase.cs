
// Type: Intermech.Search.UI.VirtualTree.RowBindingBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using System;
using System.Collections.ObjectModel;


namespace Intermech.Search.UI.VirtualTree;

public abstract class RowBindingBase : ObjectRowBinding
{
  public RowBindingBase() => this.Extensions = new RowBindingExtensionCollection(this);

  public RowBindingExtensionCollection Extensions { get; private set; }

  public virtual CellWidget GetCellWidget(ColumnBase column)
  {
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    foreach (IRowBindingExtension bindingExtension in (Collection<IRowBindingExtension>) this.Extensions)
    {
      CellWidgetBase cellWidget = bindingExtension.GetCellWidget(column);
      if (cellWidget != null)
        return (CellWidget) cellWidget;
    }
    return (CellWidget) null;
  }

  public override void GetCellData(Row row, Column column, CellData cellData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (column == null)
      throw new ArgumentNullException(nameof (column));
    if (cellData == null)
      throw new ArgumentNullException(nameof (cellData));
    base.GetCellData(row, column, cellData);
    if (!(column is ColumnBase))
      return;
    foreach (IRowBindingExtension bindingExtension in (Collection<IRowBindingExtension>) this.Extensions)
      bindingExtension.GetCellData(row, (ColumnBase) column, cellData);
  }

  public override void GetRowData(Row row, RowData rowData)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    if (rowData == null)
      throw new ArgumentNullException(nameof (rowData));
    base.GetRowData(row, rowData);
    foreach (IRowBindingExtension bindingExtension in (Collection<IRowBindingExtension>) this.Extensions)
      bindingExtension.GetRowData(row, rowData);
  }
}
