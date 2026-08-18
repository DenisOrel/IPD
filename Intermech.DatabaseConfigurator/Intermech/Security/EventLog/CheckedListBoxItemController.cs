// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.CheckedListBoxItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class CheckedListBoxItemController : FilterItemController
{
  private SmallNumberListFilterItem _listItem;
  private CheckedListBox _valueBox;

  public CheckedListBoxItemController(
    FilterItem filterItem,
    CheckBox checkBox,
    ConditionComboBox comboBox,
    CheckedListBox valueBox)
    : base(filterItem, checkBox, comboBox)
  {
    this._listItem = (SmallNumberListFilterItem) filterItem;
    this._valueBox = valueBox;
    this._valueBox.EnabledChanged += new EventHandler(this._valueBox_EnabledChanged);
  }

  private void _valueBox_EnabledChanged(object sender, EventArgs e)
  {
    CheckState checkState = this._valueBox.Enabled ? CheckState.Checked : CheckState.Indeterminate;
    foreach (int checkedIndex in this._valueBox.CheckedIndices)
      this._valueBox.SetItemCheckState(checkedIndex, checkState);
  }

  protected override void InitializeControls()
  {
    this._valueBox.BeginUpdate();
    try
    {
      this._valueBox.Enabled = this._filterItem.Enabled;
      this._valueBox.ClearSelected();
      for (int index = 0; index < this._listItem.Count; ++index)
        this._valueBox.SetItemChecked(this.IndexOf(this._listItem[index]), true);
    }
    finally
    {
      this._valueBox.EndUpdate();
    }
    this._valueBox.ItemCheck += new ItemCheckEventHandler(this.ItemChecked);
  }

  protected override void UninitializeControls()
  {
    this._valueBox.ItemCheck -= new ItemCheckEventHandler(this.ItemChecked);
  }

  protected override void SetCheckState(bool isChecked) => this._valueBox.Enabled = isChecked;

  private void ItemChecked(object sender, ItemCheckEventArgs e)
  {
    IdTextItem idTextItem = (IdTextItem) this._valueBox.Items[e.Index];
    if (e.NewValue == CheckState.Checked)
      this._listItem.Add(idTextItem.Id);
    else
      this._listItem.Remove(idTextItem.Id);
    this.FireItemChanged((object) this, (EventArgs) null);
  }

  private int IndexOf(int id)
  {
    for (int index = 0; index < this._valueBox.Items.Count; ++index)
    {
      if (((IdTextItem) this._valueBox.Items[index]).Id.Equals(id))
        return index;
    }
    return -1;
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is CheckedListBoxItemController boxItemController))
      return;
    this._listItem.Assign((FilterItem) boxItemController._listItem);
    this._valueBox = boxItemController._valueBox;
  }

  public override object Clone()
  {
    CheckedListBoxItemController boxItemController = new CheckedListBoxItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox);
    boxItemController.Assign((FilterItemController) this);
    return (object) boxItemController;
  }
}
