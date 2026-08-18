
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.InputObjectAttributeEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class InputObjectAttributeEditControl : EditControl<TextBoxButton>
{
  private readonly IConditionDataProvider _dataProvider;
  private readonly RelationalOperators _relationalOperator;

  public InputObjectAttributeEditControl(
    IConditionDataProvider dataProvider,
    bool firstValue,
    RelationalOperators relationalOperator)
    : base(firstValue)
  {
    this._dataProvider = dataProvider;
    this._relationalOperator = relationalOperator;
  }

  protected override void OnSetValue(object value)
  {
    this.control.SetText(value != null ? this._dataProvider.GetAttributeName((object) ((InputObjectAttribute) value).AttributeGUID) : string.Empty);
  }

  protected override object defaultValue => (object) DateTime.Now;

  protected override void OnCreateControl()
  {
    this.control = new TextBoxButton(true, string.Empty);
    this.control.OnOpenDialog += new OnOpenDialogEventHandler(this.OnOpenDialog);
  }

  public override bool OnAddNewValue(OnOpenDialogEventArgs e)
  {
    this.control.OpenDialog_Click((object) this, e);
    return true;
  }

  private bool OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    InputObjectAttributeButtonDialog attributeButtonDialog = new InputObjectAttributeButtonDialog(this._dataProvider, this.Value, this._relationalOperator);
    int num = attributeButtonDialog.OnOpenDialog(e.Multiselect) ? 1 : 0;
    if (num == 0)
      return num != 0;
    e.SelectedValues = attributeButtonDialog.Value;
    this.Value = attributeButtonDialog.Value is IList list ? list[list.Count - 1] : attributeButtonDialog.Value;
    this.control.SetText(attributeButtonDialog.Text);
    this.OnValueChanged((object) this, new EventArgs());
    return num != 0;
  }

  protected override bool ValidValue(object value) => value is InputObjectAttribute;
}
