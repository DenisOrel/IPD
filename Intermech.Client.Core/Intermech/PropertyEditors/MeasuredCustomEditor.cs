
// Type: Intermech.PropertyEditors.MeasuredCustomEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class MeasuredCustomEditor : UITypeEditor
{
  private int attributeId;

  public MeasuredCustomEditor(int attributeId) => this.attributeId = attributeId;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    string options = value != null ? value.ToString() : string.Empty;
    using (MeasuredOptionEditorForm optionEditorForm = new MeasuredOptionEditorForm())
    {
      int num = (int) optionEditorForm.ShowDialog(ref options, this.attributeId);
      if (optionEditorForm.DialogResult == DialogResult.OK)
        value = (object) options;
    }
    return value;
  }
}
