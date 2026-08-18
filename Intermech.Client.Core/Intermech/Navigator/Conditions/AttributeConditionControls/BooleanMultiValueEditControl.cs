
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.BooleanMultiValueEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class BooleanMultiValueEditControl(bool firstValue) : EditControl<ComboBox>(firstValue)
{
  public override object Value
  {
    get
    {
      return this.control.SelectedIndex == -1 ? (object) null : (object) (this.control.SelectedIndex == 1);
    }
    set
    {
      if (value == null)
        this.control.SelectedIndex = -1;
      else
        this.control.SelectedIndex = (bool) value ? 1 : 0;
    }
  }

  protected override object defaultValue => (object) false;

  public override bool OnAddNewValue(OnOpenDialogEventArgs e) => false;

  protected override void OnCreateControl()
  {
    this.control = this.GetControlForDialog() as ComboBox;
    this.control.SelectedIndexChanged += new EventHandler(((EditControl<ComboBox>) this).OnValueChanged);
  }

  protected override Control GetControlForDialog()
  {
    return (Control) new ComboBox()
    {
      DropDownStyle = ComboBoxStyle.DropDownList,
      Items = {
        (object) Intermech.Consts.NoValue,
        (object) Intermech.Consts.YesValue
      }
    };
  }

  protected override void OnOKControlDialog(Control control)
  {
    this.Value = (object) ((control as ComboBox).SelectedIndex == 1);
  }

  protected override bool ValidValue(object value) => value is bool;
}
