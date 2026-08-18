// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.FullTextSearchGridFilterFactoryTextBox
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilters;
using System;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public class FullTextSearchGridFilterFactoryTextBox : TextBox, IGridFilterFactory
{
  public event EventHandler Changed;

  public event GridFilterEventHandler GridFilterCreated;

  public void BeginGridFilterCreation()
  {
  }

  public void EndGridFilterCreation()
  {
  }

  public IGridFilter CreateGridFilter(DataGridViewColumn column)
  {
    IGridFilter gridFilter = (IGridFilter) new TextGridFilter((TextBox) this);
    this.OnGridFilterCreated(new GridFilterEventArgs(column, gridFilter));
    return gridFilter;
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  private void OnGridFilterCreated(GridFilterEventArgs gridFilterEventArgs)
  {
    GridFilterEventHandler gridFilterCreated = this.GridFilterCreated;
    if (gridFilterCreated == null)
      return;
    gridFilterCreated((object) this, gridFilterEventArgs);
  }
}
