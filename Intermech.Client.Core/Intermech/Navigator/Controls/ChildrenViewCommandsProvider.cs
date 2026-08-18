
// Type: Intermech.Navigator.Controls.ChildrenViewCommandsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using Intermech.Search.iGrid;
using System;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewCommandsProvider : ICommandsProvider
{
  private ChildrenView _childrenView;

  public ChildrenViewCommandsProvider(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
  }

  public CommandsInfo GetMergedCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    return CommandsInfo.Empty;
  }

  public CommandsInfo GetGroupCommands(ISelectedItems items, IServiceProvider viewServices)
  {
    CommandsInfo groupCommands = new CommandsInfo();
    if (!EventsViewConsts.IsFile)
      groupCommands.Add("Refresh", new CommandInfo(0, new ClickEventHandler(this._childrenView.RefreshViewCommand)));
    this._childrenView.GetSelectedHandles();
    if (this._childrenView.Grid.SelectedCells.Count > 0 && this._childrenView._dataAdapter != null && this._childrenView.Grid.SelectionMode != iGSelectionMode.One)
    {
      if ((long) this._childrenView.Grid.SelectedCells.Count != this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount)
        groupCommands.Add("InvertMarkers", new CommandInfo(0, new ClickEventHandler(this.InvertMarkersCommand)));
      if (this._childrenView.Grid.SelectedCells.Count > 0)
        groupCommands.Add("MarkGroupUp", new CommandInfo(0, new ClickEventHandler(this.MarkGroupUpCommand)));
      if (this._childrenView.Grid.CurRow != null && this._childrenView.Grid.CurRow.Index >= 0 && (long) this._childrenView.Grid.CurRow.Index < this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount || this._childrenView.Grid.CurCell != null && this._childrenView.Grid.CurCell.RowIndex >= 0 && (long) this._childrenView.Grid.CurCell.RowIndex < this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount)
        groupCommands.Add("MarkGroupDown", new CommandInfo(0, new ClickEventHandler(this.MarkGroupDownCommand)));
      if ((long) this._childrenView.Grid.SelectedCells.Count < this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount)
        groupCommands.Add("MarkGroupAll", new CommandInfo(0, new ClickEventHandler(this.MarkGroupAllCommand)));
      if (this._childrenView.Grid.CurRow != null && this._childrenView.Grid.CurRow.Index >= 0 || this._childrenView.Grid.CurCell != null && this._childrenView.Grid.CurCell.RowIndex >= 0)
        groupCommands.Add("UnMarkGroupUp", new CommandInfo(0, new ClickEventHandler(this.UnMarkGroupUpCommand)));
      if (this._childrenView.Grid.CurRow != null && this._childrenView.Grid.CurRow.Index >= 0 && (long) this._childrenView.Grid.CurRow.Index < this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount || this._childrenView.Grid.CurCell != null && this._childrenView.Grid.CurCell.RowIndex >= 0 && (long) this._childrenView.Grid.CurCell.RowIndex < this._childrenView._dataAdapter.ReadedRecordCount + (long) this._childrenView._groupRowsCount)
        groupCommands.Add("UnMarkGroupDown", new CommandInfo(0, new ClickEventHandler(this.UnMarkGroupDownCommand)));
      groupCommands.Add("UnMarkGroupAll", new CommandInfo(0, new ClickEventHandler(this.UnMarkGroupAllCommand)));
    }
    if ((this._childrenView.Options & ChildrenViewOptions.ShowSetColumnsCommand) != (ChildrenViewOptions) 0 && !this._childrenView.DisableColumnsSettings)
    {
      groupCommands.Add("ResetColumns", new CommandInfo(0, new ClickEventHandler(this._childrenView.ResetColumnsCommand)));
      groupCommands.Add("SetupColumns", new CommandInfo(0, new ClickEventHandler(this._childrenView.SetColumnsCommand)));
    }
    if (!this._childrenView.DisableManualSortingSetup)
    {
      ISelectedItems selectedItems = items;
      if (selectedItems == null || selectedItems.Count == 0)
        selectedItems = (ISelectedItems) this._childrenView._parentSelItem;
      if (selectedItems != null && selectedItems.Count > 0 && ManualSortingEditForm.FindFirstSortingObjectItem(items) == 0)
        groupCommands.Add("ManualSortingSetup", new CommandInfo(0, new ClickEventHandler(this._childrenView.ManualSortingSetupCommand)));
    }
    return groupCommands;
  }

  private void InvertMarkersCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.GridInvertSelection(true);
    this._childrenView.SelectionChanged();
  }

  private void MarkGroupDownCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SetSelectionDown(this._childrenView.GridSelectedRowIndex(), true);
    this._childrenView.SelectionChanged();
  }

  private void MarkGroupUpCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SetSelectionUp(this._childrenView.GridSelectedRowIndex(), true);
    this._childrenView.SelectionChanged();
  }

  private void UnMarkGroupUpCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SetSelectionUp(this._childrenView.GridSelectedRowIndex(), false);
    this._childrenView.SelectionChanged();
  }

  private void MarkGroupAllCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.GridSelectAll(true);
    this._childrenView.SelectionChanged();
  }

  private void UnMarkGroupDownCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this.SetSelectionDown(this._childrenView.GridSelectedRowIndex(), false);
    this._childrenView.SelectionChanged();
  }

  private void UnMarkGroupAllCommand(
    ISelectedItems selectedItems,
    IServiceProvider viewServices,
    object additionalInfo)
  {
    this._childrenView.GridDeselectAll(true);
    this._childrenView.SelectionChanged();
  }

  private void GridInvertSelection(bool lockGrid)
  {
    try
    {
      if (lockGrid)
      {
        this._childrenView.Grid.BeginUpdate();
        this._childrenView.Grid.Redraw = false;
      }
      for (int index = 0; index < this._childrenView.Grid.Rows.Count; ++index)
      {
        iGRow row = this._childrenView.Grid.Rows[index];
        bool flag = row.IsAnyCellSelected();
        if (row != null)
          this._childrenView.SetSelectedForRow(row, !flag);
      }
    }
    finally
    {
      if (lockGrid)
      {
        this._childrenView.Grid.Redraw = true;
        this._childrenView.Grid.EndUpdate();
      }
    }
  }

  private void SetSelectionUp(int focusedRowHandle, bool select)
  {
    for (int index = 0; index < focusedRowHandle; ++index)
    {
      iGRow row = this._childrenView.Grid.Rows[index];
      if (row != null && row.Type == iGRowType.Normal)
        this.SetSelectionForRow(index, select);
    }
  }

  private void SetSelectionDown(int focusedRowHandle, bool select)
  {
    if (this._childrenView._dataAdapter == null)
      return;
    for (int index = focusedRowHandle + 1; index < this._childrenView.Grid.Rows.Count; ++index)
    {
      iGRow row = this._childrenView.Grid.Rows[index];
      if (row != null && row.Type == iGRowType.Normal)
        this.SetSelectionForRow(index, select);
    }
  }

  private void GridSelectAll(bool lockGrid)
  {
    try
    {
      if (lockGrid)
      {
        this._childrenView.Grid.BeginUpdate();
        this._childrenView.Grid.Redraw = false;
      }
      this._childrenView.Grid.PerformAction(iGActions.SelectAll);
    }
    finally
    {
      if (lockGrid)
      {
        this._childrenView.Grid.Redraw = true;
        this._childrenView.Grid.EndUpdate();
      }
    }
  }

  private void SetSelectionForRow(int row, bool select)
  {
    iGRow row1 = this._childrenView.Grid.Rows[row];
    if (row1 == null)
      return;
    this._childrenView.SetSelectedForRow(row1, select);
  }
}
