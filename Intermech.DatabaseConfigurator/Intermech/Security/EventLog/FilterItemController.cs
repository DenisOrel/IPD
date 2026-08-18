// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal abstract class FilterItemController : ICloneable
{
  protected FilterItem _filterItem;
  protected CheckBox _checkBox;
  protected ConditionComboBox _comboBox;

  public event EventHandler ItemChanged;

  public FilterItemController(FilterItem filterItem, CheckBox checkBox, ConditionComboBox comboBox)
  {
    this._filterItem = filterItem;
    this._checkBox = checkBox;
    this._comboBox = comboBox;
  }

  public void Initialize()
  {
    this._checkBox.Checked = this._filterItem.Enabled;
    this._checkBox.CheckedChanged += new EventHandler(this.CheckedChanged);
    this._comboBox.BeginUpdate();
    try
    {
      this._comboBox.Enabled = this._filterItem.Enabled;
      this._comboBox.AssignItems(this._filterItem.PossibleOperators);
      this._comboBox.SelectedCondition = this._filterItem.Operator;
      this._comboBox.SelectedIndexChanged += new EventHandler(this.IndexChanged);
    }
    finally
    {
      this._comboBox.EndUpdate();
    }
    this.InitializeControls();
  }

  public void Uninitialize()
  {
    this._comboBox.SelectedIndexChanged -= new EventHandler(this.IndexChanged);
    this._checkBox.CheckedChanged -= new EventHandler(this.CheckedChanged);
    this.UninitializeControls();
  }

  protected void FireItemChanged(object sender, EventArgs e)
  {
    if (this.ItemChanged == null)
      return;
    this.ItemChanged(sender, e);
  }

  private void CheckedChanged(object sender, EventArgs e)
  {
    this._comboBox.Enabled = this._checkBox.Checked;
    this._filterItem.Enabled = this._checkBox.Checked;
    this._filterItem.Operator = this._comboBox.SelectedCondition;
    this.SetCheckState(this._checkBox.Checked);
    this.FireItemChanged((object) this, (EventArgs) null);
  }

  private void IndexChanged(object sender, EventArgs e)
  {
    this._filterItem.Operator = this._comboBox.SelectedCondition;
    this.FireItemChanged((object) this, (EventArgs) null);
  }

  protected abstract void InitializeControls();

  protected abstract void UninitializeControls();

  protected abstract void SetCheckState(bool isChecked);

  public abstract object Clone();

  protected virtual void Assign(FilterItemController source)
  {
    if (source == null)
      return;
    this._filterItem.Assign(source._filterItem);
    this._checkBox = source._checkBox;
    this._comboBox = source._comboBox;
  }
}
