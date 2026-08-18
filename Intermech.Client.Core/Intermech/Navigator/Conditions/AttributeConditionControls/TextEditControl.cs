
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.TextEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class TextEditControl(bool firstValue) : EditControl<TextBox>(firstValue)
{
  public override object Value
  {
    get => string.IsNullOrEmpty(this.control.Text) ? (object) null : (object) this.control.Text;
    set
    {
      if (value == null)
        this.control.Text = string.Empty;
      else
        this.control.Text = (string) value;
    }
  }

  protected override object PrepareValue(object value) => (object) Convert.ToString(value);

  protected override object defaultValue => (object) string.Empty;

  protected override void OnCreateControl()
  {
    this.control = new TextBox();
    this.control.TextChanged += new EventHandler(((EditControl<TextBox>) this).OnValueChanged);
  }

  protected override bool ValidValue(object value) => value is string;

  protected override void OnOKControlDialog(Control control)
  {
    this.Value = (object) (control as TextBox).Text;
  }
}
