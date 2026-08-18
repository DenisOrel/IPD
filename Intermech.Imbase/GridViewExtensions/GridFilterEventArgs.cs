// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterEventArgs
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public class GridFilterEventArgs : EventArgs
{
  private DataGridViewColumn _column;
  private IGridFilter _gridFilter;

  public GridFilterEventArgs(DataGridViewColumn column, IGridFilter gridFilter)
  {
    this._column = column;
    this._gridFilter = gridFilter;
  }

  public System.Type DataType => this._column.ValueType;

  public string ColumnName => this._column.DataPropertyName;

  public DataGridViewColumn Column => this._column;

  public string HeaderText => this._column.HeaderText;

  public IGridFilter GridFilter
  {
    get => this._gridFilter;
    set => this._gridFilter = value;
  }
}
