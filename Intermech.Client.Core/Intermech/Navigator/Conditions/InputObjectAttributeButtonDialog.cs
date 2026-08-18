
// Type: Intermech.Navigator.Conditions.InputObjectAttributeButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

internal sealed class InputObjectAttributeButtonDialog : ButtonDialog
{
  private RelationalOperators _relationalOperator;

  public InputObjectAttributeButtonDialog(
    IConditionDataProvider dataProvider,
    object value,
    RelationalOperators relationalOperator)
    : base(dataProvider, 0, value)
  {
    this._relationalOperator = relationalOperator;
  }

  public override bool OnOpenDialog(bool multiselect)
  {
    InputObjectAttribute inputObjectAttribute = new InputObjectAttributeSelector().GetValue(this.Value as InputObjectAttribute);
    if (inputObjectAttribute == null || object.Equals(this.Value, (object) inputObjectAttribute) || !this.CheckAttribute(inputObjectAttribute))
      return false;
    this.Value = (object) inputObjectAttribute;
    this.Text = this.dataProvider.GetAttributeName((object) inputObjectAttribute.AttributeGUID);
    return true;
  }

  private bool CheckAttribute(InputObjectAttribute newValue)
  {
    switch (this.dataProvider.GetAttributeMultiValueMode((object) newValue.AttributeGUID))
    {
      case MultiValueModes.MultiValues:
      case MultiValueModes.MultiValuesFromList:
        if (this._relationalOperator != RelationalOperators.In && this._relationalOperator != RelationalOperators.NotIn)
        {
          int num = (int) IMMessageBox.Show("Ошибка", $"Нельзя использовать атрибут \"{this.dataProvider.GetAttributeName((object) newValue.AttributeGUID)}\", который может иметь список значений в условии \"{EnumDescConverter.GetEnumDescription((Enum) this._relationalOperator)}\". Используйте \"{EnumDescConverter.GetEnumDescription((Enum) RelationalOperators.In)}\" или \"{EnumDescConverter.GetEnumDescription((Enum) RelationalOperators.NotIn)}\"", MessageBoxButtons.OK, IMMessageBoxImage.Error);
          return false;
        }
        break;
    }
    return true;
  }
}
