// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.FormDesignerEventsEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для свойства ...</summary>
public class FormDesignerEventsEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="propertyName"></param>
  /// <returns></returns>
  private System.Type GetType(string propertyName)
  {
    System.Type type = (System.Type) null;
    switch (propertyName)
    {
      case "FormDesignerEvents":
        type = typeof (IFormDesignerFormEventsHandler);
        break;
      case "AttributeChangingEvents":
        type = typeof (IAttributeChangingEventHandler);
        break;
    }
    return type;
  }

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
    FormDesignerAction[] events = value as FormDesignerAction[];
    using (FormDesignerEventsForm designerEventsForm = new FormDesignerEventsForm(this.GetType(context.PropertyDescriptor.Name), events))
    {
      if (designerEventsForm.ShowDialog() == DialogResult.OK)
        return (object) designerEventsForm.CheckedEvents;
    }
    return value;
  }
}
