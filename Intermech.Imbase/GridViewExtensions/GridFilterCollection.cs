// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterCollection
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions;

public class GridFilterCollection : ReadOnlyCollectionBase
{
  private Dictionary<DataGridViewColumn, IGridFilter> _columnsToGridFiltersHash;

  internal GridFilterCollection(
    IList columns,
    Dictionary<DataGridViewColumn, IGridFilter> columnsToGridFiltersHash)
  {
    this._columnsToGridFiltersHash = new Dictionary<DataGridViewColumn, IGridFilter>((IDictionary<DataGridViewColumn, IGridFilter>) columnsToGridFiltersHash);
    foreach (DataGridViewColumn column in (IEnumerable) columns)
    {
      IGridFilter gridFilter = this._columnsToGridFiltersHash[column];
      if (gridFilter != null)
        this.InnerList.Add((object) gridFilter);
    }
  }

  public bool Contains(IGridFilter gridFilter) => this.InnerList.Contains((object) gridFilter);

  public IGridFilter this[int index] => (IGridFilter) this.InnerList[index];

  public IGridFilter this[DataGridViewColumn column]
  {
    get
    {
      return this.InnerList.Contains((object) this._columnsToGridFiltersHash[column]) ? this._columnsToGridFiltersHash[column] : (IGridFilter) null;
    }
  }

  public IGridFilter GetByName(string name)
  {
    foreach (DataGridViewColumn key in this._columnsToGridFiltersHash.Keys)
    {
      if (key.Name == name)
        return this[key];
    }
    return (IGridFilter) null;
  }

  public IGridFilter GetByHeaderText(string headerText)
  {
    foreach (DataGridViewColumn key in this._columnsToGridFiltersHash.Keys)
    {
      if (key.HeaderText == headerText)
        return this[key];
    }
    return (IGridFilter) null;
  }

  public IGridFilter GetByDataPropertyName(string dataPropertyName)
  {
    foreach (DataGridViewColumn key in this._columnsToGridFiltersHash.Keys)
    {
      if (key.DataPropertyName == dataPropertyName)
        return this[key];
    }
    return (IGridFilter) null;
  }

  public GridFilterCollection FilterByGridFilterType(System.Type dataType, bool exactMatch)
  {
    if (!typeof (IGridFilter).IsAssignableFrom(dataType))
      throw new ArgumentException("Given type must implement IGridFilter.", nameof (dataType));
    ArrayList columns = new ArrayList();
    foreach (DataGridViewColumn key in this._columnsToGridFiltersHash.Keys)
    {
      if (this[key] != null && (this[key].GetType().Equals(dataType) || !exactMatch && dataType.IsAssignableFrom(this[key].GetType())))
        columns.Add((object) key);
    }
    return new GridFilterCollection((IList) columns, this._columnsToGridFiltersHash);
  }
}
