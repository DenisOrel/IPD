// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.TextGridFilterCombo
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class TextGridFilterCombo : GridFilterBase
{
  private const string FILTER_FORMAT = "Convert({0}, 'System.String') {1} {2}";
  private const string FILTER_FORMAT_LIKE = "Convert({0}, 'System.String') {1} '*{2}*'";
  private const string FILTER_REGEX = "Convert\\(\\[[a-zA-Z].*\\],\\s'System.String'\\)\\sLIKE\\s'(?<Value>.*)\\*'";
  private TextGridFilterControl _textGridFilterControl;

  public TextGridFilterCombo(TextGridFilterControl textGridFilterControl)
    : this(textGridFilterControl, true)
  {
  }

  public TextGridFilterCombo()
    : this(new TextGridFilterControl(), false)
  {
  }

  private TextGridFilterCombo(
    TextGridFilterControl textGridFilterControl,
    bool useCustomFilterPlacement)
    : base(useCustomFilterPlacement)
  {
    this._textGridFilterControl = textGridFilterControl;
    this._textGridFilterControl.Changed += new EventHandler(this.OnTextGridFilterControlChanged);
  }

  public string Text
  {
    get => this._textGridFilterControl.ComboBoxValue.Text;
    set => this._textGridFilterControl.ComboBoxValue.Text = value;
  }

  public string Operator
  {
    get => (string) this._textGridFilterControl.ComboBoxCondition.SelectedItem;
    set => this._textGridFilterControl.ComboBoxCondition.SelectedItem = (object) value;
  }

  public override Control FilterControl => (Control) this._textGridFilterControl;

  public override ComboBox ComboBox => this._textGridFilterControl.ComboBoxValue;

  public override bool ApplyAutoComplete(DataColumn column)
  {
    if (!this.ApplyAutoComplete(column, this._textGridFilterControl.ComboBoxValue))
      return false;
    this.CalcDropDownWidth(this._textGridFilterControl.ComboBoxValue);
    ComboBox.ObjectCollection items = this._textGridFilterControl.ComboBoxValue.Items;
    int count = items.Count;
    for (int index = 0; index < count; ++index)
    {
      string str = items[index].ToString();
      if (SpecialValue.IsSpaces(str))
        items[index] = (object) new SpecialValue(str);
    }
    return true;
  }

  public override bool HasFilter => this.ControlText.Length > 0;

  public override string GetFilterText(string columnName)
  {
    string str1 = string.Empty;
    if (this.SelectedValue is SpecialValue selectedValue)
    {
      if (SpecialValue.NullValue.Equals((object) selectedValue))
        return $"Convert(ISNULL({columnName}, 'a§df43dj§цap'), System.String) {"="} 'a§df43dj§цap'";
      if (selectedValue._type == SpecialValueType.Spaces)
      {
        string str2 = selectedValue.ToString();
        return $"Convert({columnName}, 'System.String') {(this.Operator == "!*" ? (object) "<>" : (object) "=")} {str2}";
      }
    }
    if (str1.Length == 0)
      str1 = this.EscapeString(this.ControlText);
    if (SpecialValue.NullValue.ToString().Equals(str1))
      return $"Convert(ISNULL({columnName}, 'a§df43dj§цap'), System.String) {"="} 'a§df43dj§цap'";
    string str3 = this.Operator;
    return str3 == "*" || str3 == "!*" ? $"Convert({columnName}, 'System.String') {(this.Operator == "!*" ? (object) "NOT LIKE" : (object) "LIKE")} '*{str1}*'" : string.Empty;
  }

  public override ConditionItem GetFilter(string columnName)
  {
    if (!this.HasFilter)
      return (ConditionItem) null;
    return new ConditionItem()
    {
      Condition = ConditionHelper.ConditionFromString(this.Operator),
      Data = this.ControlText
    };
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    string data = filter.Data;
    if (SpecialValue.IsSpaces(data))
    {
      string str = $"'{data}'";
    }
    this._textGridFilterControl.ComboBoxValue.Text = filter.Data;
    this.Operator = ConditionHelper.StringFromCondition(filter.Condition);
  }

  public override void Clear() => this._textGridFilterControl.ComboBoxValue.Text = string.Empty;

  private string ControlText
  {
    get
    {
      return this._textGridFilterControl.ComboBoxValue.SelectedValue == null ? this._textGridFilterControl.ComboBoxValue.Text : this._textGridFilterControl.ComboBoxValue.SelectedValue.ToString();
    }
  }

  private object SelectedValue => this._textGridFilterControl.ComboBoxValue.SelectedItem;

  private void OnTextGridFilterControlChanged(object sender, EventArgs e) => this.OnChanged();

  public override void Dispose()
  {
    this._textGridFilterControl.Changed -= new EventHandler(this.OnTextGridFilterControlChanged);
    this._textGridFilterControl.Dispose();
  }
}
