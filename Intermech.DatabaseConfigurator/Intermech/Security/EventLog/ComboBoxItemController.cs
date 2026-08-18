// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.ComboBoxItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class ComboBoxItemController : FilterItemController
{
  private SmallNumberFilterItem _numberItem;
  private ComboBox _valueBox;
  private bool _canChange;

  public ComboBoxItemController(
    FilterItem filterItem,
    CheckBox checkBox,
    ConditionComboBox comboBox,
    ComboBox valueBox)
    : base(filterItem, checkBox, comboBox)
  {
    this._numberItem = (SmallNumberFilterItem) filterItem;
    this._valueBox = valueBox;
    this._canChange = true;
  }

  protected override void InitializeControls()
  {
    this._valueBox.BeginUpdate();
    try
    {
      this._valueBox.Enabled = this._filterItem.Enabled;
      this.UpdateSelectedIndex();
    }
    finally
    {
      this._valueBox.EndUpdate();
    }
    this._valueBox.SelectedIndexChanged += new EventHandler(this.IndexChanged);
  }

  protected override void UninitializeControls()
  {
    this._valueBox.SelectedIndexChanged -= new EventHandler(this.IndexChanged);
  }

  protected override void SetCheckState(bool isChecked) => this._valueBox.Enabled = isChecked;

  private void IndexChanged(object sender, EventArgs e)
  {
    if (!this._canChange)
      return;
    try
    {
      this._numberItem.Value = ((IdTextItem) this._valueBox.Items[this._valueBox.SelectedIndex]).Id;
      this.FireItemChanged((object) this, (EventArgs) null);
    }
    catch (Exception ex)
    {
      this._canChange = false;
      try
      {
        this.UpdateSelectedIndex();
        int num = (int) MessageBox.Show(ex.Message);
      }
      finally
      {
        this._canChange = true;
      }
    }
  }

  private void UpdateSelectedIndex()
  {
    for (int index = 0; index < this._valueBox.Items.Count; ++index)
    {
      if (((IdTextItem) this._valueBox.Items[index]).Id.Equals(this._numberItem.Value))
      {
        this._valueBox.SelectedIndex = index;
        break;
      }
    }
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is ComboBoxItemController boxItemController))
      return;
    this._numberItem.Assign((FilterItem) boxItemController._numberItem);
    this._valueBox = boxItemController._valueBox;
    this._canChange = boxItemController._canChange;
  }

  public override object Clone()
  {
    ComboBoxItemController boxItemController = new ComboBoxItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox);
    boxItemController.Assign((FilterItemController) this);
    return (object) boxItemController;
  }
}
