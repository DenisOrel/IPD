
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.NumberEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal sealed class NumberEditControl(bool firstValue) : EditControl<MaskedTextBox>(firstValue)
{
  public override object Value
  {
    get
    {
      Decimal result;
      return !string.IsNullOrEmpty(this.control.Text) && Decimal.TryParse(this.control.Text, out result) ? (object) result : (object) null;
    }
    set
    {
      if (value != null)
        this.control.Text = Convert.ToString(value, (IFormatProvider) CultureInfo.CurrentCulture);
      else
        this.control.Text = string.Empty;
    }
  }

  protected override object defaultValue => (object) 0;

  protected override object PrepareValue(object value) => (object) Convert.ToDecimal(value);

  protected override void OnCreateControl()
  {
    this.control = new MaskedTextBox();
    this.control.TypeValidationCompleted += new TypeValidationEventHandler(this.Control_TypeValidationCompleted);
    this.control.TextChanged += new EventHandler(((EditControl<MaskedTextBox>) this).OnValueChanged);
  }

  private void Control_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
  {
    if (!string.IsNullOrEmpty(this.control.Text) && !e.IsValidInput)
    {
      int num = (int) MessageBox.Show(e.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      e.Cancel = true;
    }
    else
      this.OnValueChanged((object) this, new EventArgs());
  }

  protected override bool ValidValue(object value)
  {
    switch (value)
    {
      case int _:
      case double _:
      case long _:
        return true;
      default:
        return value is Decimal;
    }
  }

  protected override void OnOKControlDialog(Control control1)
  {
    this.control.Text = (control1 as MaskedTextBox).Text;
  }
}
