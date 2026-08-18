// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.GridFilterFactoryBase
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public abstract class GridFilterFactoryBase : IGridFilterFactory
{
  protected virtual void OnChanged(EventArgs eventArgs)
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, eventArgs);
  }

  protected virtual void OnGridFilterCreated(GridFilterEventArgs eventArgs)
  {
    GridFilterEventHandler gridFilterCreated = this.GridFilterCreated;
    if (gridFilterCreated == null)
      return;
    gridFilterCreated((object) this, eventArgs);
  }

  protected abstract IGridFilter CreateGridFilterInternal(DataGridViewColumn column);

  public event EventHandler Changed;

  public event GridFilterEventHandler GridFilterCreated;

  public virtual void BeginGridFilterCreation()
  {
  }

  public virtual void EndGridFilterCreation()
  {
  }

  public IGridFilter CreateGridFilter(DataGridViewColumn column)
  {
    IGridFilter gridFilterInternal = this.CreateGridFilterInternal(column);
    GridFilterEventArgs eventArgs = new GridFilterEventArgs(column, gridFilterInternal);
    this.OnGridFilterCreated(eventArgs);
    return eventArgs.GridFilter;
  }

  public override string ToString() => this.GetType().Name;
}
