// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.DateTimePickerItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class DateTimePickerItemController : FilterItemController
{
  private DateTimePicker _valueBox;
  private bool _canChange;

  public DateTimePickerItemController(
    FilterItem filterItem,
    CheckBox checkBox,
    ConditionComboBox comboBox,
    DateTimePicker valueBox)
    : base(filterItem, checkBox, comboBox)
  {
    this._valueBox = valueBox;
    this._canChange = true;
  }

  protected override void InitializeControls()
  {
    this._valueBox.Enabled = this._filterItem.Enabled;
    this._valueBox.Text = this._filterItem.AsString;
    this._valueBox.ValueChanged += new EventHandler(this.ValueChanged);
  }

  protected override void UninitializeControls()
  {
    this._valueBox.ValueChanged -= new EventHandler(this.ValueChanged);
  }

  protected override void SetCheckState(bool isChecked) => this._valueBox.Enabled = isChecked;

  private void ValueChanged(object sender, EventArgs e)
  {
    if (!this._canChange)
      return;
    try
    {
      FilterItem filterItem = this._filterItem;
      DateTime date = this._valueBox.Value;
      date = date.Date;
      string str = date.ToString();
      filterItem.AsString = str;
      this.FireItemChanged((object) this, (EventArgs) null);
    }
    catch (Exception ex)
    {
      this._canChange = false;
      try
      {
        this._valueBox.Text = this._filterItem.AsString;
        int num = (int) MessageBox.Show(ex.Message);
      }
      finally
      {
        this._canChange = true;
      }
    }
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is DateTimePickerItemController pickerItemController))
      return;
    this._canChange = pickerItemController._canChange;
    this._valueBox = pickerItemController._valueBox;
  }

  public override object Clone()
  {
    DateTimePickerItemController pickerItemController = new DateTimePickerItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox);
    pickerItemController.Assign((FilterItemController) this);
    return (object) pickerItemController;
  }
}
