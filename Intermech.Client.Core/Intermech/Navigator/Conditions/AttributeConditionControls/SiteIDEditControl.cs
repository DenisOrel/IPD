
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.SiteIDEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class SiteIDEditControl(
  IConditionDataProvider dataProvider,
  int attributeID,
  Dictionary<object, string> pValues,
  bool firstValue) : ObjectEditControl(dataProvider, attributeID, (int[]) null, SelectionParameterTypes.sptSiteID, pValues, firstValue)
{
  protected override IButtonDialog ButtonDialog
  {
    get => (IButtonDialog) new SiteIDButtonDialog(this.dataProvider, this.attributeID, this.Value);
  }
}
