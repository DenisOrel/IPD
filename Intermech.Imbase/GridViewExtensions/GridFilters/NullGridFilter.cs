// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.NullGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class NullGridFilter : GridFilterBase
{
  private const string DUMMY_STRING_VALUE = "a§df43dj§цap";
  internal const string FILTER_FORMAT = "Convert(ISNULL({0}, 'a§df43dj§цap'), System.String) {1} 'a§df43dj§цap'";
  internal const string FILTER_REGEX = "Convert\\(ISNULL\\(\\[[a-zA-Z].*\\], 'a§df43dj§цap'\\), System.String\\) (?<Operator>(=|<>)) 'a§df43dj§цap'";
  private CheckBox _checkBox;

  public NullGridFilter()
    : this(new CheckBox(), false)
  {
    this._checkBox.CheckAlign = ContentAlignment.MiddleCenter;
  }

  public NullGridFilter(CheckBox checkBox)
    : this(checkBox, true)
  {
  }

  private NullGridFilter(CheckBox checkBox, bool useCustomFilterPlacement)
    : base(useCustomFilterPlacement)
  {
    this._checkBox = checkBox;
    this._checkBox.ThreeState = true;
    this._checkBox.CheckState = CheckState.Indeterminate;
    this._checkBox.CheckStateChanged += new EventHandler(this.OnCheckBoxCheckStateChanged);
  }

  public CheckState CheckState
  {
    get => this._checkBox.CheckState;
    set => this._checkBox.CheckState = value;
  }

  public override Control FilterControl => (Control) this._checkBox;

  public override ComboBox ComboBox => (ComboBox) null;

  public override bool ApplyAutoComplete(DataColumn column) => true;

  public override bool HasFilter => this._checkBox.CheckState != CheckState.Indeterminate;

  public override string GetFilterText(string columnName)
  {
    return !this.HasFilter ? "" : $"Convert(ISNULL({columnName}, 'a§df43dj§цap'), System.String) {(this._checkBox.Checked ? (object) "<>" : (object) "=")} 'a§df43dj§цap'";
  }

  public override ConditionItem GetFilter(string columnName)
  {
    return !this.HasFilter ? (ConditionItem) null : this.NewConditionItem(this._checkBox.Checked ? Condition.NotEqual : Condition.Equal, "");
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    this._checkBox.CheckState = CheckState.Indeterminate;
    this._checkBox.CheckState = filter.Condition == Condition.Equal ? CheckState.Unchecked : CheckState.Checked;
  }

  public override void Clear() => this._checkBox.CheckState = CheckState.Indeterminate;

  private void OnCheckBoxCheckStateChanged(object sender, EventArgs e) => this.OnChanged();

  public override void Dispose()
  {
    this._checkBox.CheckStateChanged -= new EventHandler(this.OnCheckBoxCheckStateChanged);
    this._checkBox.Dispose();
  }
}
