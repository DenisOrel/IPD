
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.UserEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class UserEditControl(
  IConditionDataProvider dataProvider,
  int attributeID,
  int[] selection4types,
  Dictionary<object, string> pValues,
  bool firstValue) : ObjectEditControl(dataProvider, attributeID, selection4types, SelectionParameterTypes.sptUser, pValues, firstValue)
{
  protected override IButtonDialog ButtonDialog
  {
    get
    {
      return (IButtonDialog) new ObjectButtonDialog(this.dataProvider, this.attributeID, this.selection4types, (object) this.dataProvider.UserTypeID, this.Value);
    }
  }
}
