
// Type: Intermech.PropertyEditors.RTFEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

internal class RTFEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (value == null || !(value is RTFPropertyClass rtfPropertyClass))
      return value;
    using (RTFEditorForm rtfEditorForm = new RTFEditorForm())
    {
      rtfEditorForm.RTFText = rtfPropertyClass.Text;
      return rtfEditorForm.ShowDialog() == DialogResult.OK ? (object) new RTFPropertyClass(rtfEditorForm.RTFText) : value;
    }
  }
}
