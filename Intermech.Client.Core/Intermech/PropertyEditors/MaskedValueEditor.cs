
// Type: Intermech.PropertyEditors.MaskedValueEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class MaskedValueEditor : UITypeEditor
{
  private IWindowsFormsEditorService edSvc;
  private MaskedTextBox mtb;
  private bool errorDetected;
  private bool valCanNull = true;
  private System.Type type = typeof (string);
  private string mask = string.Empty;

  public MaskedValueEditor()
    : this(string.Empty, typeof (string), true)
  {
  }

  public MaskedValueEditor(string mask, System.Type valueType, bool valCanNull)
  {
    this.mask = mask;
    this.valCanNull = valCanNull;
    this.type = valueType;
    this.errorDetected = false;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this.edSvc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    if (this.edSvc != null)
    {
      string str = string.Empty;
      if (value != null && value is string)
        str = value as string;
      if (value != null && value is PropertyClass)
        str = value.ToString();
      this.mtb = new MaskedTextBox();
      this.mtb.BorderStyle = BorderStyle.None;
      this.mtb.TypeValidationCompleted += new TypeValidationEventHandler(this.maskedTextBox_TypeValidationCompleted);
      if (this.type == typeof (string))
        this.mtb.ValidatingType = typeof (string);
      if (this.type == typeof (StringMaskedPropertyClass))
        this.mtb.ValidatingType = typeof (string);
      this.mtb.Mask = context == null || context.PropertyDescriptor == null || !(context.PropertyDescriptor is PropDescriptor) ? string.Empty : ((PropDescriptor) context.PropertyDescriptor).Mask;
      this.mtb.Text = str;
      this.edSvc.DropDownControl((Control) this.mtb);
      this.errorDetected = false;
      object aString = (object) null;
      try
      {
        aString = this.mtb.ValidateText();
      }
      catch
      {
        this.errorDetected = true;
      }
      if (this.errorDetected)
      {
        this.errorDetected = false;
        return value;
      }
      if (this.valCanNull && (aString == null || aString != null && aString.ToString().Trim() == string.Empty))
        return (object) null;
      if (this.type == typeof (string))
        return aString;
      if (this.type == typeof (StringMaskedPropertyClass))
        return (object) new StringMaskedPropertyClass((string) aString, this.mask);
    }
    return value;
  }

  private void maskedTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
  {
    if (e.IsValidInput)
      return;
    this.errorDetected = true;
    if (this.valCanNull && (this.mtb.Text == null || this.mtb.Text != null && this.mtb.Text.ToString().Trim() == string.Empty))
    {
      this.errorDetected = false;
    }
    else
    {
      e.Cancel = true;
      int num = (int) MessageBox.Show(e.Message + LocalizationHolder.rm.GetString("Client.Core_1556") + this.mtb.Mask, LocalizationHolder.rm.GetString("Client.Core_1557"), MessageBoxButtons.OK);
    }
  }
}
