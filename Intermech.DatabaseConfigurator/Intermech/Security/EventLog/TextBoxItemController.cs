// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.TextBoxItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class TextBoxItemController : FilterItemController
{
  private TextBox _valueBox;

  public TextBoxItemController(
    FilterItem filterItem,
    CheckBox checkBox,
    ConditionComboBox comboBox,
    TextBox valueBox)
    : base(filterItem, checkBox, comboBox)
  {
    this._valueBox = valueBox;
  }

  protected override void InitializeControls()
  {
    this._valueBox.Enabled = this._filterItem.Enabled;
    this._valueBox.ReadOnly = !this._filterItem.Enabled;
    this._valueBox.Text = this._filterItem.AsString;
    this._valueBox.KeyUp += new KeyEventHandler(this.KeyUp);
    this._valueBox.LostFocus += new EventHandler(this._valueBox_LostFocus);
  }

  private void _valueBox_LostFocus(object sender, EventArgs e)
  {
    if (!this._valueBox.Modified)
      return;
    this.UpdateFilterValue();
  }

  protected override void UninitializeControls()
  {
    this._valueBox.LostFocus -= new EventHandler(this._valueBox_LostFocus);
    this._valueBox.KeyUp -= new KeyEventHandler(this.KeyUp);
  }

  protected override void SetCheckState(bool isChecked)
  {
    this._valueBox.Enabled = isChecked;
    this._valueBox.ReadOnly = !isChecked;
  }

  private void KeyUp(object sender, KeyEventArgs e)
  {
    if (!this._valueBox.Modified || e.KeyCode != Keys.Return)
      return;
    this.UpdateFilterValue();
  }

  private void UpdateFilterValue()
  {
    try
    {
      this._filterItem.AsString = this._valueBox.Text;
      this._valueBox.Modified = false;
      this.FireItemChanged((object) this, (EventArgs) null);
    }
    catch (Exception ex)
    {
      this._valueBox.Text = this._filterItem.AsString;
      this._valueBox.Modified = false;
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is TextBoxItemController boxItemController))
      return;
    this._valueBox = boxItemController._valueBox;
  }

  public override object Clone()
  {
    TextBoxItemController boxItemController = new TextBoxItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox);
    boxItemController.Assign((FilterItemController) this);
    return (object) boxItemController;
  }
}
