
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.UserGroupEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class UserGroupEditControl(
  IConditionDataProvider dataProvider,
  int attributeID,
  int[] selection4types,
  SelectionParameterTypes paramType,
  Dictionary<object, string> pValues,
  bool firstValue) : ObjectEditControl(dataProvider, attributeID, selection4types, paramType, pValues, firstValue)
{
  protected override IButtonDialog ButtonDialog
  {
    get
    {
      return (IButtonDialog) new ObjectButtonDialog(this.dataProvider, this.attributeID, this.selection4types, (object) new List<int>((IEnumerable<int>) this.dataProvider.UserGroupTypeIDs)
      {
        this.dataProvider.UserTypeID
      }.ToArray(), this.Value);
    }
  }

  public override object Value
  {
    get
    {
      object groupID = base.Value;
      return groupID != null && groupID is long objectID && !this.dataProvider.IsUserObjectID(objectID) ? (object) new ConditionGroupIDReplacer((long) groupID, true) : groupID;
    }
    set => base.Value = value;
  }

  protected override bool ValidValue(object value)
  {
    return value is ConditionGroupIDReplacer || base.ValidValue(value);
  }
}
