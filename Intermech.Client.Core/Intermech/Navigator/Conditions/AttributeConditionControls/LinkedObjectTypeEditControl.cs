
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.LinkedObjectTypeEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class LinkedObjectTypeEditControl(
  IConditionDataProvider dataProvider,
  int attributeID) : ObjectTypeEditControl(dataProvider, attributeID, (Dictionary<object, string>) null, true)
{
  protected override bool OnOpenDialog(object sender, OnOpenDialogEventArgs e)
  {
    if (!this.dataProvider.EnabledParameterTypes.Contains(this.paramType) || !this.OpenDialog(this.ButtonDialog, e))
      return false;
    this.OnValueChanged((object) this, new EventArgs());
    return true;
  }
}
