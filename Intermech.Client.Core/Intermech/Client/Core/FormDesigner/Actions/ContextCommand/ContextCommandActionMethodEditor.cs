
// Type: Intermech.Client.Core.FormDesigner.Actions.ContextCommand.ContextCommandActionMethodEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Actions.ContextCommand;

/// <summary>
/// 
/// </summary>
internal class ContextCommandActionMethodEditor : UITypeEditor
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
  /// <param name="sp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    string str = value is ContextCommandActionMethod commandActionMethod ? commandActionMethod.CommandName : string.Empty;
    using (ContextCommandSelectMethodWindow selectMethodWindow = new ContextCommandSelectMethodWindow())
    {
      selectMethodWindow.SelectedCommand = str;
      if (selectMethodWindow.ShowDialog() == DialogResult.OK)
        return (object) new ContextCommandActionMethod(selectMethodWindow.SelectedCommand);
    }
    return value;
  }
}
