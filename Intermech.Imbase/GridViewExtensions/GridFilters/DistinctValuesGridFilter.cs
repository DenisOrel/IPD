// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.DistinctValuesGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class DistinctValuesGridFilter : GridFilterBase
{
  private const string FILTER_FORMAT = "Convert({0}, System.String) = '{1}'";
  private const string FILTER_REGEX = "Convert\\(\\[[a-zA-Z].*\\],\\sSystem.String\\)\\s=\\s'(?<Value>.*)'";
  private ComboBox _combo;
  private string[] _values;

  public DistinctValuesGridFilter(DataGridViewColumn column)
    : this(new ComboBox(), false)
  {
    this.Fill(column);
  }

  public DistinctValuesGridFilter(DataGridViewColumn column, ComboBox comboBox)
    : this(comboBox, true)
  {
    this.Fill(column);
  }

  public DistinctValuesGridFilter(string[] values, bool containsDbNull)
    : this(new ComboBox(), false)
  {
    this.Fill(values, containsDbNull);
  }

  public DistinctValuesGridFilter(string[] values, bool containsDbNull, ComboBox comboBox)
    : this(comboBox, true)
  {
    this.Fill(values, containsDbNull);
  }

  private DistinctValuesGridFilter(ComboBox comboBox, bool useCustomFilterPlacement)
    : base(useCustomFilterPlacement)
  {
    this._combo = comboBox;
    this._combo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._combo.SelectedIndexChanged += new EventHandler(this.OnComboSelectedIndexChanged);
    this._combo.Items.Clear();
    this._combo.Items.Add((object) SpecialValue.NoValue);
    this._combo.SelectedIndex = 0;
  }

  public object[] Values
  {
    get
    {
      object[] destination = new object[this._combo.Items.Count];
      this._combo.Items.CopyTo(destination, 0);
      return destination;
    }
  }

  public object CurrentValue
  {
    get => this._combo.SelectedItem;
    set
    {
      this._combo.SelectedItem = value is string && value is SpecialValue ? value : throw new ArgumentException("Value must be either a string or of type SpecialValue", nameof (value));
    }
  }

  public override Control FilterControl => (Control) this._combo;

  public override ComboBox ComboBox => this._combo;

  public override bool ApplyAutoComplete(DataColumn column) => true;

  public override bool HasFilter => this._combo.SelectedItem != SpecialValue.NoValue;

  public override string GetFilterText(string columnName)
  {
    if (!this.HasFilter)
      return "";
    return this._combo.SelectedItem == SpecialValue.NullValue ? $"Convert(ISNULL({columnName}, 'a§df43dj§цap'), System.String) {"="} 'a§df43dj§цap'" : $"Convert({columnName}, System.String) = '{(string) this._combo.SelectedItem}'";
  }

  public override ConditionItem GetFilter(string columnName)
  {
    return !this.HasFilter ? (ConditionItem) null : this.NewConditionItem(Condition.Equal, (string) this._combo.SelectedItem);
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    this._combo.SelectedItem = (object) filter.Data;
  }

  public override void Clear() => this._combo.SelectedIndex = 0;

  private void Fill(DataGridViewColumn column)
  {
    bool containsDbNull;
    this.Fill(DistinctValuesGridFilter.GetDistinctValues(column, out containsDbNull), containsDbNull);
  }

  private void Fill(string[] values, bool containsDbNull)
  {
    Array.Sort<string>(values);
    this._values = values;
    if (containsDbNull)
      this._combo.Items.Add((object) SpecialValue.NullValue);
    this._combo.Items.AddRange((object[]) values);
  }

  private void OnComboSelectedIndexChanged(object sender, EventArgs e) => this.OnChanged();

  public static string[] GetDistinctValues(DataGridViewColumn column, out bool containsDbNull)
  {
    return DistinctValuesGridFilter.GetDistinctValues(column, int.MaxValue, out containsDbNull);
  }

  public static string[] GetDistinctValues(
    DataGridViewColumn column,
    int maximumValues,
    out bool containsDbNull)
  {
    Hashtable hashtable = new Hashtable();
    containsDbNull = false;
    IBindingListView bindingListView = !(column.DataGridView.DataSource is BindingSource) ? GridFiltersControl.GetViewFromDataSource(column.DataGridView.DataSource, column.DataGridView.DataMember) : (column.DataGridView.DataSource as BindingSource).List as IBindingListView;
    ITypedList typedList = bindingListView as ITypedList;
    if (bindingListView == null || typedList == null)
      return new string[0];
    PropertyDescriptor itemProperty = typedList.GetItemProperties((PropertyDescriptor[]) null)[column.DataPropertyName];
    if (itemProperty == null)
      return new string[0];
    int count = bindingListView.Count;
    for (int index = 0; index < count; ++index)
    {
      object obj = itemProperty.GetValue(bindingListView[index]);
      if (obj == null || obj == DBNull.Value)
      {
        containsDbNull = true;
      }
      else
      {
        string key = obj.ToString();
        if (!hashtable.ContainsKey((object) key))
        {
          hashtable.Add((object) key, (object) 0);
          if (hashtable.Count > maximumValues)
            return (string[]) null;
        }
      }
    }
    string[] distinctValues = new string[hashtable.Count];
    hashtable.Keys.CopyTo((Array) distinctValues, 0);
    return distinctValues;
  }

  public override void Dispose()
  {
    this._combo.SelectedIndexChanged -= new EventHandler(this.OnComboSelectedIndexChanged);
    this._combo.Dispose();
  }
}
