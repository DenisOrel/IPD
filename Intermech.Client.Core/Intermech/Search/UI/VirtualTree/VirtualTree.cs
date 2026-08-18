
// Type: Intermech.Search.UI.VirtualTree.VirtualTree
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using System;
using System.Windows.Forms;


namespace Intermech.Search.UI.VirtualTree;

public sealed class VirtualTree : Infralution.Controls.VirtualTree.VirtualTree
{
  private int _iconWidth;

  public VirtualTree() => this.HeaderContextMenu = (ContextMenuStrip) null;

  public int IconWidth
  {
    get => this._iconWidth;
    set
    {
      if (this._iconWidth == value)
        return;
      this._iconWidth = value;
      this.Refresh();
    }
  }

  public void UpdateRowDataRecurcive(Row row)
  {
    if (row == null)
      throw new ArgumentNullException(nameof (row));
    this.SuspendLayout();
    try
    {
      this.UpdateRowData(row);
      int childIndex = 0;
      int lastChildRowIndex = row.LastChildRowIndex;
      for (; childIndex < 0; ++childIndex)
      {
        Row row1 = row.ChildRowByIndex(childIndex);
        if (row1 != null)
          this.UpdateRowDataRecurcive(row1);
      }
    }
    finally
    {
      this.ResumeLayout(true);
    }
  }

  internal void OnInitializeCellWidget(ExtendedCellWidget cellWidget)
  {
    if (cellWidget == null)
      throw new ArgumentNullException(nameof (cellWidget));
    if (cellWidget.Row == null)
      throw new ArgumentException();
    if (!(this.GetBindingForRow(cellWidget.Row) is ICellWidgetCustomer bindingForRow))
      return;
    bindingForRow.InitializeCellWidget(cellWidget);
  }

  internal void OnCellWidgetPropertyChanged(ExtendedCellWidget cellWidget)
  {
    if (cellWidget == null)
      throw new ArgumentNullException(nameof (cellWidget));
    if (cellWidget.Row == null)
      throw new ArgumentException();
    if (!(this.GetBindingForRow(cellWidget.Row) is ICellWidgetCustomer bindingForRow))
      return;
    bindingForRow.CellWidgetChanged(cellWidget);
  }

  protected override void BindDataSource()
  {
    base.BindDataSource();
    try
    {
      this.SelectedRow = (Row) null;
    }
    catch
    {
    }
  }

  protected override RowWidget CreateRowWidget(PanelWidget panelWidget, Row row)
  {
    return (RowWidget) new ExtendedRowWidget(panelWidget, row);
  }

  public override bool CompleteEdit()
  {
    try
    {
      return base.CompleteEdit();
    }
    catch (NullReferenceException ex)
    {
      return false;
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public override ContextMenuStrip HeaderContextMenu
  {
    get => (ContextMenuStrip) null;
    set
    {
    }
  }

  protected override ToolStripMenuItem FindHeaderMenuItem(string name) => (ToolStripMenuItem) null;
}
