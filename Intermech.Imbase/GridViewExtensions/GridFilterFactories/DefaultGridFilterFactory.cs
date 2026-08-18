// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilterFactories.DefaultGridFilterFactory
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilters;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilterFactories;

public class DefaultGridFilterFactory : GridFilterFactoryBase
{
  private Dictionary<System.Type, System.Type> _hash;
  private System.Type _defaultGridFilterType;
  private bool _handleEnumerationTypes = true;
  private bool _createDistinctGridFilters;
  private int _maximumDistinctValues = 20;
  private bool _defaultShowDateInBetweenOperator;
  private bool _defaultShowNumericInBetweenOperator = true;

  public DefaultGridFilterFactory()
  {
    this._hash = new Dictionary<System.Type, System.Type>();
    this.DefaultGridFilterType = typeof (TextGridFilterCombo);
    this.AddGridFilter(typeof (bool), typeof (BoolGridFilter));
    this.AddGridFilter(typeof (byte), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (short), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (int), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (long), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (float), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (double), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (Decimal), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (ushort), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (ulong), typeof (NumericGridFilter));
    this.AddGridFilter(typeof (DateTime), typeof (DateGridFilter));
  }

  public bool DefaultShowDateInBetweenOperator
  {
    get => this._defaultShowDateInBetweenOperator;
    set
    {
      if (value == this._defaultShowDateInBetweenOperator)
        return;
      this._defaultShowDateInBetweenOperator = value;
      this.OnChanged(EventArgs.Empty);
    }
  }

  public bool DefaultShowNumericInBetweenOperator
  {
    get => this._defaultShowNumericInBetweenOperator;
    set
    {
      if (value == this._defaultShowNumericInBetweenOperator)
        return;
      this._defaultShowNumericInBetweenOperator = value;
      this.OnChanged(EventArgs.Empty);
    }
  }

  public bool HandleEnumerationTypes
  {
    get => this._handleEnumerationTypes;
    set
    {
      this._handleEnumerationTypes = value;
      this.OnChanged(EventArgs.Empty);
    }
  }

  public System.Type DefaultGridFilterType
  {
    get => this._defaultGridFilterType;
    set
    {
      this.CheckIfValidGridFilterType(value);
      this._defaultGridFilterType = value;
      this.OnChanged(EventArgs.Empty);
    }
  }

  public bool CreateDistinctGridFilters
  {
    get => this._createDistinctGridFilters;
    set => this.ConfigureDistinctGridFilterHandling(value, this._maximumDistinctValues);
  }

  public int MaximumDistinctValues
  {
    get => this._maximumDistinctValues;
    set => this.ConfigureDistinctGridFilterHandling(this._createDistinctGridFilters, value);
  }

  public void ConfigureDistinctGridFilterHandling(
    bool createDistinctGridFilters,
    int maximumDistinctValues)
  {
    this._maximumDistinctValues = maximumDistinctValues > 0 ? maximumDistinctValues : throw new ArgumentException("Value must be 1 or greater.", nameof (maximumDistinctValues));
    this._createDistinctGridFilters = createDistinctGridFilters;
    this.OnChanged(EventArgs.Empty);
  }

  public void AddGridFilter(System.Type dataType, System.Type gridFilterType)
  {
    this.CheckIfValidGridFilterType(gridFilterType);
    this._hash.Add(dataType, gridFilterType);
    this.OnChanged(EventArgs.Empty);
  }

  public void RemoveGridFilter(System.Type dataType)
  {
    this._hash.Remove(dataType);
    this.OnChanged(EventArgs.Empty);
  }

  private void CheckIfValidGridFilterType(System.Type gridFilterType)
  {
    if (!gridFilterType.IsClass && !gridFilterType.IsValueType)
      throw new ArgumentException("Specified grid filter type must be either a class or a struct.", nameof (gridFilterType));
    if (!typeof (IGridFilter).IsAssignableFrom(gridFilterType))
      throw new ArgumentException("Specified grid filter type does not implement IGridFilter.", nameof (gridFilterType));
    if (gridFilterType.GetConstructor(new System.Type[0]) == (ConstructorInfo) null)
      throw new ArgumentException("Specified grid filter type does not have an empty public constructor are allowed.", nameof (gridFilterType));
  }

  protected override IGridFilter CreateGridFilterInternal(DataGridViewColumn column)
  {
    IGridFilter gridFilterInternal = (IGridFilter) null;
    System.Type valueType = column.ValueType;
    if (column.ValueType == (System.Type) null)
      return (IGridFilter) new EmptyGridFilter();
    if (valueType.IsEnum && this._handleEnumerationTypes)
      gridFilterInternal = (IGridFilter) new EnumerationGridFilter(valueType);
    else if (this._createDistinctGridFilters)
    {
      bool containsDbNull;
      string[] distinctValues = DistinctValuesGridFilter.GetDistinctValues(column, this._maximumDistinctValues, out containsDbNull);
      if (distinctValues != null)
        gridFilterInternal = (IGridFilter) new DistinctValuesGridFilter(distinctValues, containsDbNull);
    }
    if (gridFilterInternal == null)
    {
      if (this._hash.ContainsKey(valueType))
      {
        System.Type type = this._hash[valueType];
        gridFilterInternal = type.Assembly.CreateInstance(type.FullName) as IGridFilter;
      }
      else if (this._defaultGridFilterType != (System.Type) null)
        gridFilterInternal = this._defaultGridFilterType.Assembly.CreateInstance(this._defaultGridFilterType.FullName) as IGridFilter;
    }
    if (gridFilterInternal is DateGridFilter)
      (gridFilterInternal as DateGridFilter).ShowInBetweenOperator = this._defaultShowDateInBetweenOperator;
    if (gridFilterInternal is NumericGridFilter)
      (gridFilterInternal as NumericGridFilter).ShowInBetweenOperator = this._defaultShowNumericInBetweenOperator;
    return gridFilterInternal;
  }
}
