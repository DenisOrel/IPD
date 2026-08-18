// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.NumericGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class NumericGridFilter : GridFilterBase
{
  internal const string IN_BETWEEN = "<x<";
  internal const string NOT_IN_BETWEEN = "<!x<";
  private const string FILTER_FORMAT_SINGLE = "{0} {1} {2}";
  private const string FILTER_REGEX_SINGLE = "\\[[a-zA-Z].*\\] (?<Operator>(<|>|<=|>=|=|<>|)) (?<Value>(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?)";
  private const string FILTER_FORMAT_BETWEEN = "{0} >= {1} AND {0} <= {2}";
  private const string FILTER_REGEX_BETWEEN = "\\[[a-zA-Z].*\\] (?<Operator1>(>=)) (?<Value1>(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?) AND \\[[a-zA-Z].*\\] (?<Operator2>(<=)) (?<Value2>(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?)";
  private const string FILTER_FORMAT_STRING = "Convert({0}, 'System.String') LIKE '{1}*'";
  private const string FILTER_REGEX_STRING = "Convert\\(\\[[a-zA-Z].*\\],\\s'System.String'\\)\\sLIKE\\s'(?<Value>(\\+|-)?[0-9][0-9]*(\\.[0-9]*)?)\\*'";
  private NumericGridFilterControl _numericGridFilterControl;

  public NumericGridFilter()
    : this(new NumericGridFilterControl(), false, false)
  {
  }

  public NumericGridFilter(bool showInBetweenOperator)
    : this(new NumericGridFilterControl(), false, showInBetweenOperator)
  {
  }

  public NumericGridFilter(NumericGridFilterControl numericGridFilterControl)
    : this(numericGridFilterControl, true, false)
  {
  }

  public NumericGridFilter(
    NumericGridFilterControl numericGridFilterControl,
    bool showInBetweenOperator)
    : this(numericGridFilterControl, true, showInBetweenOperator)
  {
  }

  private NumericGridFilter(
    NumericGridFilterControl numericGridFilterControl,
    bool useCustomFilterPlacement,
    bool showInBetweenOperator)
    : base(useCustomFilterPlacement)
  {
    this._numericGridFilterControl = numericGridFilterControl;
    this._numericGridFilterControl.Changed += new EventHandler(this.OnNumericGridFilterControlChanged);
    this.ShowInBetweenOperator = showInBetweenOperator;
  }

  public bool ShowInBetweenOperator
  {
    get => this._numericGridFilterControl.ComboBox.Items.Contains((object) "<x<");
    set
    {
      if (value == this.ShowInBetweenOperator)
        return;
      if (value)
      {
        this._numericGridFilterControl.ComboBox.Items.Add((object) "<x<");
      }
      else
      {
        this._numericGridFilterControl.ComboBox.Items.Remove((object) "<x<");
        if (!(this.Operator == "<x<"))
          return;
        this._numericGridFilterControl.ComboBox.SelectedIndex = 0;
      }
    }
  }

  public string Text1
  {
    get
    {
      return this._numericGridFilterControl.TextBox1.Visible ? this._numericGridFilterControl.TextBox1.Text : this._numericGridFilterControl.ComboBox1.Text;
    }
    set
    {
      this._numericGridFilterControl.TextBox1.Text = value;
      this._numericGridFilterControl.ComboBox1.Text = value;
    }
  }

  public string Text2
  {
    get => this._numericGridFilterControl.TextBox2.Text;
    set => this._numericGridFilterControl.TextBox2.Text = value;
  }

  public string Operator
  {
    get => (string) this._numericGridFilterControl.ComboBox.SelectedItem;
    set => this._numericGridFilterControl.ComboBox.SelectedItem = (object) value;
  }

  public override Control FilterControl => (Control) this._numericGridFilterControl;

  public override ComboBox ComboBox => this._numericGridFilterControl.ComboBox1;

  public override bool ApplyAutoComplete(DataColumn column)
  {
    if (!this.ApplyAutoComplete(column, this._numericGridFilterControl.TextBox1))
      return false;
    this.ApplyAutoComplete(column, this._numericGridFilterControl.ComboBox1);
    this._numericGridFilterControl.TextBox2.AutoCompleteMode = this._numericGridFilterControl.TextBox1.AutoCompleteMode;
    this._numericGridFilterControl.TextBox2.AutoCompleteCustomSource = this._numericGridFilterControl.TextBox1.AutoCompleteCustomSource;
    this._numericGridFilterControl.TextBox2.AutoCompleteSource = this._numericGridFilterControl.TextBox1.AutoCompleteSource;
    return true;
  }

  public override bool HasFilter
  {
    get
    {
      if (this.Text1.Length <= 0)
        return false;
      return this.Operator != "<x<" || this.Text2.Length > 0;
    }
  }

  public override string GetFilterText(string columnName)
  {
    if (!this.HasFilter)
      return "";
    if (this.Operator == "*")
      return $"Convert({columnName}, 'System.String') LIKE '{this.EscapeString(this.Text1)}*'";
    try
    {
      if (this.Operator == "<x<")
      {
        Decimal num1 = this.Text1.Length == 0 ? Decimal.MinValue : Convert.ToDecimal(this.Text1);
        Decimal num2 = this.Text2.Length == 0 ? Decimal.MaxValue : Convert.ToDecimal(this.Text2);
        string str1 = num1.ToString((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"));
        string str2 = num2.ToString((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"));
        return string.Format("{0} >= {1} AND {0} <= {2}", (object) columnName, (object) str1, (object) str2);
      }
      string text1 = this.Text1;
      if (SpecialValue.NullValue.ToString().Equals(text1))
        return $"Convert(ISNULL({columnName}, 'a§df43dj§цap'), System.String) {"="} 'a§df43dj§цap'";
      string str = Convert.ToDecimal(text1).ToString((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"));
      return $"{columnName} {this.Operator} {str}";
    }
    catch
    {
      return $"{columnName} = {false.ToString()}";
    }
  }

  public override ConditionItem GetFilter(string columnName)
  {
    if (!this.HasFilter)
      return (ConditionItem) null;
    if (this.Operator == "*")
      return this.NewConditionItem(Condition.Substring, this.Text1);
    try
    {
      double num1 = this.Text1.Length == 0 ? 0.0 : Convert.ToDouble(this.Text1);
      double num2 = this.Text2.Length == 0 ? 0.0 : Convert.ToDouble(this.Text2);
      string data = num1.ToString((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"));
      string data2 = num2.ToString((IFormatProvider) CultureInfo.CreateSpecificCulture("en-US"));
      return this.NewConditionItem(ConditionHelper.ConditionFromString(this.Operator), data, data2);
    }
    catch
    {
      return (ConditionItem) null;
    }
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    if (filter.Condition == Condition.Between || filter.Condition == Condition.NotBetween)
    {
      this._numericGridFilterControl.ComboBox.SelectedItem = filter.Condition != Condition.Between ? (object) "<!x<" : (object) "<x<";
      double num1 = Convert.ToDouble(filter.Data);
      double num2 = Convert.ToDouble(filter.Data2);
      this._numericGridFilterControl.TextBox1.Text = num1 == double.MinValue ? "" : num1.ToString();
      this._numericGridFilterControl.TextBox2.Text = num2 == double.MaxValue ? "" : num2.ToString();
    }
    else
    {
      this.Text1 = filter.Condition != Condition.Substring ? Convert.ToDouble(filter.Data).ToString() : filter.Data;
      this.Operator = ConditionHelper.StringFromCondition(filter.Condition);
    }
  }

  public override void Clear()
  {
    this._numericGridFilterControl.ComboBox.SelectedIndex = 0;
    this.Text1 = "";
    this.Text2 = "";
  }

  private void OnNumericGridFilterControlChanged(object sender, EventArgs e) => this.OnChanged();
}
