// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.EnumerationGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using GridViewExtensions.GridFilters.EnumerationSources;
using Intermech.Imbase;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class EnumerationGridFilter : GridFilterBase
{
  private const string FILTER_FORMAT = "{0} = {1}";
  private const string FILTER_FORMAT_LIKE = "CONVERT({0},System.String) LIKE '*{1}*'";
  private const string FILTER_REGEX = "\\[[a-zA-Z].*\\] = (?<Value>(\\+|-)?[0-9][0-9]*)";
  private ComboBox _combo;
  private IEnumerationSource _enumerationSource;
  private bool _useQuotes;
  private bool _useLike;

  public EnumerationGridFilter(IEnumerationSource enumerationSource)
    : this(enumerationSource, new ComboBox(), false)
  {
  }

  public EnumerationGridFilter(IEnumerationSource enumerationSource, ComboBox comboBox)
    : this(enumerationSource, comboBox, true)
  {
  }

  public EnumerationGridFilter(System.Type dataType)
    : this((IEnumerationSource) new TypeEnumerationSource(dataType))
  {
  }

  private EnumerationGridFilter(
    IEnumerationSource enumerationSource,
    ComboBox comboBox,
    bool useCustomFilterPlacement)
    : base(useCustomFilterPlacement)
  {
    this._useQuotes = false;
    this._useLike = false;
    this._enumerationSource = enumerationSource;
    this._combo = comboBox;
    this._combo.DropDownStyle = ComboBoxStyle.DropDownList;
    this._combo.SelectedIndexChanged += new EventHandler(this.OnComboSelectedIndexChanged);
    this.SetValues();
  }

  public void SetValues()
  {
    this.Lock();
    try
    {
      object selectedItem = this._combo.SelectedItem;
      this._combo.Items.Clear();
      this._combo.Items.Add((object) "");
      this._combo.SelectedIndex = 0;
      this._combo.Items.AddRange(this._enumerationSource.AllValues);
      this._combo.Sorted = true;
      this._combo.SelectedItem = selectedItem;
    }
    finally
    {
      this.UnLock();
    }
  }

  public object Value
  {
    get => this._combo.SelectedItem;
    set
    {
      if (this._combo.Items.Contains(value))
        this._combo.SelectedItem = value;
      else
        this._combo.SelectedIndex = 0;
    }
  }

  public IEnumerationSource Source => this._enumerationSource;

  public override Control FilterControl => (Control) this._combo;

  public override ComboBox ComboBox => this._combo;

  public override bool ApplyAutoComplete(DataColumn column) => true;

  public override bool HasFilter => this._combo.Text.Length > 0;

  public bool UseQuotes
  {
    get => this._useQuotes;
    set
    {
      this._useLike = false;
      this._useQuotes = value;
    }
  }

  public bool UseLike
  {
    get => this._useLike;
    set
    {
      this._useLike = value;
      this._useQuotes = false;
    }
  }

  public override string GetFilterText(string columnName)
  {
    if (!this.HasFilter)
      return string.Empty;
    string str = this._enumerationSource.GetFilterFromValue(this._combo.SelectedItem);
    if (this.UseLike)
      return $"CONVERT({columnName},System.String) LIKE '*{str}*'";
    if (this.UseQuotes)
      str = $"'{this.EscapeString(str)}'";
    return $"{columnName} = {str}";
  }

  public override ConditionItem GetFilter(string columnName)
  {
    return !this.HasFilter ? (ConditionItem) null : this.NewConditionItem(Condition.Equal, this._combo.SelectedItem.ToString());
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    this._combo.SelectedItem = this._enumerationSource.GetValueFromFilter(filter.Data);
  }

  public override void Clear() => this._combo.SelectedIndex = 0;

  private void OnComboSelectedIndexChanged(object sender, EventArgs e) => this.OnChanged();

  public override void Dispose()
  {
    this._combo.SelectedIndexChanged -= new EventHandler(this.OnComboSelectedIndexChanged);
    this._combo.Dispose();
  }
}
