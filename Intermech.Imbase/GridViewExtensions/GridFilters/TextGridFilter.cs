// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.GridFilters.TextGridFilter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase;
using System;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace GridViewExtensions.GridFilters;

public class TextGridFilter : GridFilterBase
{
  private const string FILTER_FORMAT_LIKE = "Convert({0}, 'System.String') LIKE '*{1}*'";
  private const string FILTER_REGEX = "Convert\\(\\[[a-zA-Z].*\\],\\s'System.String'\\)\\sLIKE\\s'(?<Value>.*)\\*'";
  private TextBox _textBox;

  public TextGridFilter(TextBox textBox)
    : this(textBox, true)
  {
  }

  public TextGridFilter()
    : this(new TextBox(), false)
  {
  }

  private TextGridFilter(TextBox textBox, bool useCustomFilterPlacement)
    : base(useCustomFilterPlacement)
  {
    this._textBox = textBox;
    this._textBox.TextChanged += new EventHandler(this.OnTextBoxTextChanged);
  }

  public string Text
  {
    get => this._textBox.Text;
    set => this._textBox.Text = value;
  }

  public override Control FilterControl => (Control) this._textBox;

  public override ComboBox ComboBox => (ComboBox) null;

  public override bool ApplyAutoComplete(DataColumn column)
  {
    return this.ApplyAutoComplete(column, this._textBox);
  }

  public override bool HasFilter => this._textBox.Text.Length > 0;

  public override string GetFilterText(string columnName)
  {
    return $"Convert({columnName}, 'System.String') LIKE '*{this.EscapeString(this._textBox.Text)}*'";
  }

  public override ConditionItem GetFilter(string columnName)
  {
    return this.NewConditionItem(Condition.Substring, this._textBox.Text);
  }

  public override void SetFilter(ConditionItem filter)
  {
    if (filter == null)
      return;
    this._textBox.Text = filter.Data;
  }

  public override void Clear() => this._textBox.Text = string.Empty;

  private void OnTextBoxTextChanged(object sender, EventArgs e) => this.OnChanged();

  public override void Dispose()
  {
    this._textBox.TextChanged -= new EventHandler(this.OnTextBoxTextChanged);
    this._textBox.Dispose();
  }
}
