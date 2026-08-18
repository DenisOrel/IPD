
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.GuidButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.SelectionView;
using System;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class GuidButtonDialog(
  IConditionDataProvider dataProvider,
  int attributeID,
  object value) : ButtonDialog(dataProvider, attributeID, value)
{
  public override bool OnOpenDialog(bool multiselect)
  {
    object aObject = this.Value;
    if (!ValueRelationSelector.SelectVersionsGuid(ref aObject) || object.Equals(this.Value, aObject))
      return false;
    this.Value = aObject;
    this.Text = Convert.ToString(aObject);
    return true;
  }
}
