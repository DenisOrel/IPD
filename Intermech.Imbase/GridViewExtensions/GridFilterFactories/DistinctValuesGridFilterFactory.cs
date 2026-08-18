// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.DistinctValuesGridFilterFactory
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilters;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public class DistinctValuesGridFilterFactory : GridFilterFactoryBase
{
  protected override IGridFilter CreateGridFilterInternal(DataGridViewColumn column)
  {
    return (IGridFilter) new DistinctValuesGridFilter(column);
  }
}
