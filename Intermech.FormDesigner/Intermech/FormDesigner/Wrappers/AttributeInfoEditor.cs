// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AttributeInfoEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces.Client;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор выбора атрибута (в PropertyGrid).</summary>
public class AttributeInfoEditor : UITypeEditor
{
  /// <summary>Возвращает тип редактора.</summary>
  /// <param name="context">Контекст</param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>Редактирукт переданное значение.</summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value">Старое значение</param>
  /// <returns>Новое значение</returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    Component baseClass = context.Instance is IWrapper instance ? instance.BaseClass as Component : (Component) null;
    if (baseClass != null)
    {
      DesForm desForm = (DesForm) null;
      IFormDesignerEditorHook designerEditorHook = (IFormDesignerEditorHook) null;
      if (baseClass.Container is IDesignerHost container)
      {
        desForm = container.RootComponent as DesForm;
        designerEditorHook = container.GetService(typeof (IFormDesignerEditorHookable)) is IFormDesignerEditorHookable service ? service.Hook : (IFormDesignerEditorHook) null;
      }
      PropertyDescriptor propertyDescriptor = context.PropertyDescriptor;
      if (designerEditorHook == null || !designerEditorHook.CanExecuteSelector)
      {
        FieldTypesAttribute attribute1 = propertyDescriptor.Attributes[typeof (FieldTypesAttribute)] as FieldTypesAttribute;
        MultiValueModesAttribute attribute2 = propertyDescriptor.Attributes[typeof (MultiValueModesAttribute)] as MultiValueModesAttribute;
        if (attribute1 != null && attribute2 != null)
        {
          using (AttributeEditorForm attributeEditorForm = new AttributeEditorForm(desForm?.Links, value as AttributeInfo, attribute1.FieldTypes, attribute2.MultiValuesModes))
          {
            if (attributeEditorForm.ShowDialog() == DialogResult.OK)
            {
              AttributeInfo result = attributeEditorForm.Result;
              value = result == value ? value : (object) result;
            }
          }
        }
      }
      else
      {
        object obj = value;
        if (designerEditorHook.ExecuteSelector((object) baseClass, propertyDescriptor, ref obj))
          value = obj == value ? value : obj;
      }
    }
    return value;
  }
}
