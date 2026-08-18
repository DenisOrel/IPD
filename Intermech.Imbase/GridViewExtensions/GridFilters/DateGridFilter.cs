// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.DateGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class DateGridFilter : GridFilterBase
{
  internal const string IN_BETWEEN = "<x<";
  private const string FILTER_FORMAT = "{0} {1} #{2:MM\\/dd\\/yyyy}#";
  private const string FILTER_REGEX = "\\[[a-zA-Z].*\\] (?<Operator>(<|>|<=|>=|=|<>|)) #(?<Month>[0-9]{2})/(?<Day>[0-9]{2})/(?<Year>[0-9]{4})#";
  private const string FILTER_FORMAT_BETWEEN = "{0} >= #{1:MM\\/dd\\/yyyy}# AND {0} <= #{2:MM\\/dd\\/yyyy}#";
  private const string FILTER_REGEX_BETWEEN = "\\[[a-zA-Z].*\\] (?<Operator1>(>=)) #(?<Month1>[0-9]{2})/(?<Day1>[0-9]{2})/(?<Year1>[0-9]{4})# AND \\[[a-zA-Z].*\\] (?<Operator2>(<=)) #(?<Month2>[0-9]{2})/(?<Day2>[0-9]{2})/(?<Year2>[0-9]{4})#";
  private DateGridFilterControl _dateGridFilterControl;

  public DateGridFilter()
    : this(new DateGridFilterControl(), false, false)
  {
  }

  public DateGridFilter(bool showInBetweenOperator)
    : this(new DateGridFilterControl(), false, showInBetweenOperator)
  {
  }

  public DateGridFilter(DateGridFilterControl dateGridFilterControl)
    : this(dateGridFilterControl, true, false)
  {
  }

  public DateGridFilter(DateGridFilterControl dateGridFilterControl, bool showInBetweenOperator)
    : this(dateGridFilterControl, true, showInBetweenOperator)
  {
  }

  private DateGridFilter(
    DateGridFilterControl dateGridFilterControl,
    bool useCustomFilterPlacement,
    bool showInBetweenOperator)
    : base(useCustomFilterPlacement)
  {
    this._dateGridFilterControl = dateGridFilterControl;
    this._dateGridFilterControl.Changed += new EventHandler(this.OnDateGridFilterControlChanged);
    this.ShowInBetweenOperator = showInBetweenOperator;
  }

  public bool ShowInBetweenOperator
  {
    get => this._dateGridFilterControl.ComboBox.Items.Contains((object) "<x<");
    set
    {
      if (value == this.ShowInBetweenOperator)
        return;
      if (value)
      {
        this._dateGridFilterControl.ComboBox.Items.Add((object) "<x<");
      }
      else
      {
        this._dateGridFilterControl.ComboBox.Items.Remove((object) "<x<");
        if (!(this.Operator == "<x<"))
          return;
        this._dateGridFilterControl.ComboBox.SelectedIndex = 0;
      }
    }
  }

  public DateTime Date1
  {
    get => this._dateGridFilterControl.DateTimePicker1.Value;
    set => this._dateGridFilterControl.DateTimePicker1.Value = value;
  }

  public DateTime Date2
  {
    get => this._dateGridFilterControl.DateTimePicker2.Value;
    set => this._dateGridFilterControl.DateTimePicker2.Value = value;
  }

  public string Operator
  {
    get => (string) this._dateGridFilterControl.ComboBox.SelectedItem;
    set => this._dateGridFilterControl.ComboBox.SelectedItem = (object) value;
  }

  public override Control FilterControl => (Control) this._dateGridFilterControl;

  public override ComboBox ComboBox => (ComboBox) null;

  public override bool ApplyAutoComplete(DataColumn column) => true;

  public override bool HasFilter
  {
    get => this._dateGridFilterControl.ComboBox.SelectedItem.ToString().Length > 0;
  }

  public override string GetFilterText(string columnName)
  {
    try
    {
      return this.Operator == "<x<" ? string.Format("{0} >= #{1:MM\\/dd\\/yyyy}# AND {0} <= #{2:MM\\/dd\\/yyyy}#", (object) columnName, (object) this._dateGridFilterControl.DateTimePicker1.Value, (object) this._dateGridFilterControl.DateTimePicker2.Value) : $"{columnName} {this._dateGridFilterControl.ComboBox.SelectedItem.ToString()} #{this._dateGridFilterControl.DateTimePicker1.Value:MM\/dd\/yyyy}#";
    }
    catch
    {
      return $"{columnName} = {false.ToString()}";
    }
  }

  public override ConditionItem GetFilter(string columnName)
  {
    try
    {
      return this.Operator == "<x<" ? this.NewConditionItem(Condition.Between, this._dateGridFilterControl.DateTimePicker1.Value.ToString(), this._dateGridFilterControl.DateTimePicker2.Value.ToString()) : this.NewConditionItem(ConditionHelper.ConditionFromString(this._dateGridFilterControl.ComboBox.SelectedItem.ToString()), this._dateGridFilterControl.DateTimePicker1.Value.ToString());
    }
    catch
    {
      return (ConditionItem) null;
    }
  }

  public override void SetFilter(ConditionItem filter)
  {
  }

  public override void Clear()
  {
    this._dateGridFilterControl.ComboBox.SelectedIndex = 0;
    this._dateGridFilterControl.DateTimePicker1.Value = DateTime.Now;
    this._dateGridFilterControl.DateTimePicker2.Value = DateTime.Now;
  }

  private void OnDateGridFilterControlChanged(object sender, EventArgs e) => this.OnChanged();
}
