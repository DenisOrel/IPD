
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.MeasureEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.SelectionService;
using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class MeasureEditControl(
  IConditionDataProvider dataProvider,
  int attributeID,
  Dictionary<object, string> pValues,
  bool firstValue) : ObjectEditControl(dataProvider, attributeID, (int[]) null, SelectionParameterTypes.sptMeasured, pValues, firstValue)
{
  private long[] _physicalQuantityIDs;

  protected override IButtonDialog ButtonDialog
  {
    get
    {
      if (this._physicalQuantityIDs == null && this.attributeID != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetAttributeType(this.attributeID) is IDBMeasureAttributeType attributeType)
            this._physicalQuantityIDs = attributeType.GetValidPhysicalValues();
        }
      }
      return (IButtonDialog) new MeasureButtonDialog(this.dataProvider, this.attributeID, this.Value, this._physicalQuantityIDs);
    }
  }

  protected override bool ValidValue(object value) => value is MeasuredValue;
}
