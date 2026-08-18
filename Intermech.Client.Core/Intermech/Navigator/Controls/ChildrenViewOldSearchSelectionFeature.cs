
// Type: Intermech.Navigator.Controls.ChildrenViewOldSearchSelectionFeature
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Search.Configuration;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewOldSearchSelectionFeature
{
  private ChildrenView _childrenView;
  private bool _enabled;
  private iGSelectionMode _childrenViewGridSelectionModeBackup;

  public ChildrenViewOldSearchSelectionFeature(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
    this._childrenView.Disposed += new EventHandler(this.ChildrenView_Disposed);
    this._childrenView.Grid.CellMouseDown += new iGCellMouseDownEventHandler(this.ChildrenViewGrid_CellMouseDown);
    this._childrenView.Grid.KeyDown += new KeyEventHandler(this.ChildrenViewGrid_KeyDown);
    this._childrenView.Grid.KeyUp += new KeyEventHandler(this.ChildrenViewGrid_KeyUp);
    if (!(ServicesManager.GetService(typeof (IConfigurationOptionRepository)) is IConfigurationOptionRepository service))
      return;
    service.OptionChanged += new EventHandler<Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs>(this.ConfigurationOptionRepository_OptionChanged);
  }

  public void Activate()
  {
    if (!(ServicesManager.GetService(typeof (IConfigurationOptionRepository)) is IConfigurationOptionRepository))
      return;
    this.Enabled = CoreConfigurationOptions.UI_UseSearchSelectionMode;
  }

  public bool Enabled
  {
    get => this._enabled;
    private set
    {
      if (this._enabled == value)
        return;
      this._enabled = value;
      this.SetGridSelectionMode();
    }
  }

  private void ChildrenView_Disposed(object sender, EventArgs e)
  {
    this._childrenView.Disposed -= new EventHandler(this.ChildrenView_Disposed);
    this._childrenView.Grid.CellMouseDown -= new iGCellMouseDownEventHandler(this.ChildrenViewGrid_CellMouseDown);
    this._childrenView.Grid.KeyDown -= new KeyEventHandler(this.ChildrenViewGrid_KeyDown);
    this._childrenView.Grid.KeyUp -= new KeyEventHandler(this.ChildrenViewGrid_KeyUp);
    if (!(ServicesManager.GetService(typeof (IConfigurationOptionRepository)) is IConfigurationOptionRepository service))
      return;
    service.OptionChanged -= new EventHandler<Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs>(this.ConfigurationOptionRepository_OptionChanged);
  }

  private void ChildrenViewGrid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
  {
    if (!this.Enabled)
      return;
    iGRow row = this._childrenView.Grid.Rows[e.RowIndex];
    if (row == null || row.Type != iGRowType.Normal)
      return;
    int currentRowIndex = this.GetCurrentRowIndex();
    iGCell cell = this._childrenView.Grid.Cells[e.RowIndex, e.ColIndex];
    if (cell != null)
      this._childrenView.Grid.CurCell = cell;
    if (e.ModifierKeys == Keys.Control)
    {
      if (this._childrenView.Grid.CurRow != null)
        this.InvertRowSelection(this._childrenView.Grid.CurRow);
    }
    else if (e.ModifierKeys == Keys.Shift)
      this.InvertRowsRangeSelection(this.GetCurrentRowIndex(), currentRowIndex);
    e.DoDefault = false;
  }

  private void ChildrenViewGrid_KeyDown(object sender, KeyEventArgs e)
  {
    if (!this.Enabled)
      return;
    if (e.KeyCode == Keys.Insert)
    {
      this.InvertRowSelection(this._childrenView.Grid.CurRow);
      this._childrenView.Grid.PerformAction(iGActions.GoNextRow);
    }
    else if (e.Shift && e.KeyCode.HasFlag((Enum) Keys.Down))
    {
      this.InvertRowSelection(this._childrenView.Grid.CurRow);
      this._childrenView.Grid.PerformAction(iGActions.GoNextRow);
      e.Handled = true;
    }
    else if (e.Shift && e.KeyCode.HasFlag((Enum) Keys.Up))
    {
      this.InvertRowSelection(this._childrenView.Grid.CurRow);
      this._childrenView.Grid.PerformAction(iGActions.GoPrevRow);
      e.Handled = true;
    }
    else if (e.Shift && e.KeyCode.HasFlag((Enum) Keys.Next))
    {
      int currentRowIndex = this.GetCurrentRowIndex();
      this.InvertRowsRangeSelection(currentRowIndex, currentRowIndex + this._childrenView.Grid.PageCapacity);
      this._childrenView.Grid.PerformAction(iGActions.GoNextPage);
      e.Handled = true;
    }
    else if (e.Shift && e.KeyCode.HasFlag((Enum) Keys.Prior))
    {
      int currentRowIndex = this.GetCurrentRowIndex();
      this.InvertRowsRangeSelection(currentRowIndex - this._childrenView.Grid.PageCapacity, currentRowIndex);
      this._childrenView.Grid.PerformAction(iGActions.GoPrevPage);
      e.Handled = true;
    }
    else if (e.Shift && e.KeyCode.HasFlag((Enum) Keys.Home))
    {
      this.InvertRowsRangeSelection(0, this.GetCurrentRowIndex());
      this._childrenView.Grid.PerformAction(iGActions.GoFirstRow);
      e.Handled = true;
    }
    else
    {
      if (!e.Shift || !e.KeyCode.HasFlag((Enum) Keys.End))
        return;
      this.InvertRowsRangeSelection(0, this._childrenView.Grid.Rows.Count - 1);
      this._childrenView.Grid.PerformAction(iGActions.GoLastRow);
      e.Handled = true;
    }
  }

  private void ChildrenViewGrid_KeyUp(object sender, KeyEventArgs e)
  {
    if (!this.Enabled || e.KeyCode != Keys.Escape)
      return;
    foreach (iGCell iGcell in this._childrenView.Grid.SelectedCells.Cast<iGCell>().ToArray<iGCell>())
      this.SetRowSelected(iGcell.Row, false);
  }

  private void ConfigurationOptionRepository_OptionChanged(
    object sender,
    Intermech.Search.Configuration.ConfigurationOptionChangedEventArgs e)
  {
    if (!(e.OptionKey == ConfigurationOptionKeys.UI_UseSearchSelectionMode))
      return;
    this.Enabled = CoreConfigurationOptions.UI_UseSearchSelectionMode;
  }

  private void SetGridSelectionMode()
  {
    if (this.Enabled)
    {
      this._childrenViewGridSelectionModeBackup = this._childrenView.Grid.SelectionMode;
      this._childrenView.Grid.SelectionMode = iGSelectionMode.MultiSimple;
    }
    else
      this._childrenView.Grid.SelectionMode = this._childrenViewGridSelectionModeBackup != iGSelectionMode.None ? this._childrenViewGridSelectionModeBackup : iGSelectionMode.MultiExtended;
  }

  private void SetEnabled()
  {
    this.Enabled = ServicesManager.GetService(typeof (IConfigurationOptionRepository)) is IConfigurationOptionRepository && CoreConfigurationOptions.UI_UseSearchSelectionMode;
  }

  private void InvertRowsRangeSelection(int firstRowIndex, int secondRowIndex)
  {
    int num1 = Math.Max(0, Math.Min(firstRowIndex, secondRowIndex));
    int num2 = Math.Min(Math.Max(firstRowIndex, secondRowIndex), this._childrenView.Grid.Rows.Count - 1);
    for (int index = num1; index <= num2; ++index)
      this.InvertRowSelection(this._childrenView.Grid.Rows[index]);
  }

  private void InvertRowSelection(iGRow row) => this.SetRowSelected(row, !this.IsRowSelected(row));

  private bool IsRowSelected(iGRow row)
  {
    return row != null && row.Cells.Count > 0 && row.Cells[0].Selected;
  }

  private void SetRowSelected(iGRow row, bool selected)
  {
    if (row == null)
      return;
    foreach (iGCell cell in (IEnumerable) row.Cells)
      cell.Selected = selected;
  }

  private int GetCurrentRowIndex()
  {
    return this._childrenView.Grid.CurRow == null ? -1 : this._childrenView.Grid.CurRow.Index;
  }
}
