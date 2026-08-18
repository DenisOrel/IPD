
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.MeasureButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class MeasureButtonDialog : ButtonDialog
{
  private readonly MeasureDescriptor[] _enableDescriptors;

  public MeasureButtonDialog(
    IConditionDataProvider dataProvider,
    int attributeID,
    object value,
    long[] physicalQuantityIDs)
    : base(dataProvider, attributeID, value)
  {
    this._enableDescriptors = this.GetEnableDescriptors(physicalQuantityIDs);
  }

  private MeasureDescriptor[] GetEnableDescriptors(long[] physicalQuantityIDs)
  {
    return physicalQuantityIDs == null || physicalQuantityIDs.Length == 0 ? MeasureHelper.Measures : Array.FindAll<MeasureDescriptor>(MeasureHelper.Measures, (Predicate<MeasureDescriptor>) (x => Array.BinarySearch<long>(physicalQuantityIDs, x.PhysicalQuantityID) >= 0));
  }

  public override bool OnOpenDialog(bool multiselect)
  {
    if (this.Value == null || this.Value.GetType() != typeof (MeasuredValue))
      this.Value = (object) new MeasuredValue(0.0, MeasureHelper.FindBaseValue(this._enableDescriptors[0]).MeasureID);
    using (MeasureForm measureForm = new MeasureForm())
    {
      MeasuredValue aMeasureValue = (MeasuredValue) this.Value;
      if (measureForm.ExecuteDialog(ref aMeasureValue, this._enableDescriptors) == DialogResult.OK)
      {
        if (!((MeasuredValue) this.Value).Equals(aMeasureValue))
        {
          this.Value = (object) aMeasureValue;
          this.Text = aMeasureValue.Caption;
          return true;
        }
      }
    }
    return false;
  }
}
