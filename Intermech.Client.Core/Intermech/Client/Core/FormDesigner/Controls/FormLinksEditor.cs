
// Type: Intermech.Client.Core.FormDesigner.Controls.FormLinksEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
internal class FormLinksEditor : UITypeEditor
{
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
    if (value is FormLinks formLinks)
    {
      using (FormLinksEditorForm formLinksEditorForm = new FormLinksEditorForm(false))
      {
        formLinksEditorForm.Links = formLinks;
        if (formLinksEditorForm.ShowDialog() == DialogResult.OK)
        {
          if (formLinksEditorForm.Changed)
            value = (object) formLinksEditorForm.Links;
        }
      }
    }
    return value;
  }
}
