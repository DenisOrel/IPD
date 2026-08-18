// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.BoolGridFilter
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

public class BoolGridFilter : GridFilterBase
{
  private const string FILTER_FORMAT = "{0} = {1}";
  private const string FILTER_REGEX = "\\[[a-zA-Z].*\\] = (?<Value>(True|False))";
  private CheckBox _checkBox;

  public BoolGridFilter()
    : this(new CheckBox(), false)
  {
    this._checkBox.CheckAlign = ContentAlignment.MiddleCenter;
  }

  public BoolGridFilter(CheckBox checkBox)
    : this(checkBox, true)
  {
  }

  private BoolGridFilter(CheckBox checkBox, bool useCustomFilterPlacement)
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
    return !this.HasFilter ? "" : $"{columnName} = {this._checkBox.Checked}";
  }

  public override ConditionItem GetFilter(string columnName)
  {
    return !this.HasFilter ? (ConditionItem) null : this.NewConditionItem(Condition.Equal, this._checkBox.Checked.ToString());
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    this._checkBox.CheckState = CheckState.Unchecked;
    this._checkBox.Checked = bool.Parse(filter.Data);
  }

  public override void Clear() => this._checkBox.CheckState = CheckState.Indeterminate;

  private void OnCheckBoxCheckStateChanged(object sender, EventArgs e) => this.OnChanged();

  public override void Dispose()
  {
    this._checkBox.CheckStateChanged -= new EventHandler(this.OnCheckBoxCheckStateChanged);
    this._checkBox.Dispose();
  }
}
