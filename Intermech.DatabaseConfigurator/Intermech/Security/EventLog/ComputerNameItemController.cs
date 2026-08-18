// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.ComputerNameItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Navigator;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class ComputerNameItemController : FilterItemController
{
  private TextBox _valueBox;
  private Button _button;
  private static readonly IComputerNamesCache _cache = CacheManager.Cache("ComputerNamesCache") as IComputerNamesCache;

  public ComputerNameItemController(
    FilterItem filterItem,
    CheckBox checkBox,
    ConditionComboBox comboBox,
    TextBox valueBox,
    Button button)
    : base(filterItem, checkBox, comboBox)
  {
    this._valueBox = valueBox;
    this._button = button;
  }

  protected override void InitializeControls()
  {
    this._valueBox.Text = this._filterItem.AsString;
    this._button.Enabled = this._valueBox.Enabled = this._filterItem.Enabled;
    this._button.Click += new EventHandler(this.Click);
  }

  protected override void UninitializeControls()
  {
    this._button.Click -= new EventHandler(this.Click);
  }

  protected override void SetCheckState(bool isChecked)
  {
    this._button.Enabled = this._valueBox.Enabled = isChecked;
  }

  private void Click(object sender, EventArgs e)
  {
    SelectCompNameForm selectCompNameForm = new SelectCompNameForm();
    selectCompNameForm.SelectedName = this._filterItem.AsString;
    if (selectCompNameForm.ShowDialog(ComputerNameItemController._cache.GetComputerNames()) != DialogResult.OK)
      return;
    this._valueBox.Text = selectCompNameForm.SelectedName;
    this._filterItem.AsString = selectCompNameForm.SelectedName;
    this.FireItemChanged((object) this, (EventArgs) null);
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is ComputerNameItemController nameItemController))
      return;
    this._valueBox = nameItemController._valueBox;
    this._button = nameItemController._button;
  }

  public override object Clone()
  {
    ComputerNameItemController nameItemController = new ComputerNameItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox, this._button);
    nameItemController.Assign((FilterItemController) this);
    return (object) nameItemController;
  }
}
