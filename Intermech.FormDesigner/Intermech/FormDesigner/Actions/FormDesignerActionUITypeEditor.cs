// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Actions.FormDesignerActionUITypeEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Actions;

/// <summary>Класс обработчик нажатия на кнопку.</summary>
public class FormDesignerActionUITypeEditor : UITypeEditor
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
    if (value is FormDesignerAction selectedAction)
    {
      using (FormDesignerActionEditor designerActionEditor = new FormDesignerActionEditor(selectedAction))
      {
        if (designerActionEditor.ShowDialog() == DialogResult.OK)
        {
          value = designerActionEditor.SelectedAction.Clone();
          if (value is FormDesignerAction formDesignerAction)
          {
            IFormDesignerActionParams actionParams = formDesignerAction.ActionParams;
            if (actionParams != null)
              actionParams.Component = (context.Instance as IWrapper).BaseClass;
          }
        }
      }
    }
    return value;
  }
}
