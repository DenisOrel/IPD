// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.UserNameItemController
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

internal class UserNameItemController : FilterItemController
{
  private TextBox _valueBox;
  private Button _button;
  private static readonly IUserNamesCache _cache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;

  public UserNameItemController(
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
    this._valueBox.Text = UserNameItemController._cache.GetUserName((long) Convert.ToInt32(this._filterItem.AsString));
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
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("DatabaseConfigurator_106"), LocalizationHolder.rm.GetString("DatabaseConfigurator_107"), (IDescriptor) new UsersGroupsDescriptor(), typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect);
    if (objArray == null)
      return;
    long userObjectID = (objArray[0] as IDBObjectID).Value;
    this._valueBox.Text = UserNameItemController._cache.GetUserName(userObjectID);
    this._filterItem.AsString = userObjectID.ToString();
    this.FireItemChanged((object) this, (EventArgs) null);
  }

  protected override void Assign(FilterItemController source)
  {
    base.Assign(source);
    if (!(source is UserNameItemController nameItemController))
      return;
    this._valueBox = nameItemController._valueBox;
    this._button = nameItemController._button;
  }

  public override object Clone()
  {
    UserNameItemController nameItemController = new UserNameItemController(this._filterItem, this._checkBox, this._comboBox, this._valueBox, this._button);
    nameItemController.Assign((FilterItemController) this);
    return (object) nameItemController;
  }
}
