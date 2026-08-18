
// Type: Intermech.Client.Core.FormDesigner.Controls.AttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Редактор выбора атрибута (в PropertyGrid).</summary>
public class AttributeEditor : UITypeEditor
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
    Component context1 = !(context.Instance is ClassWrapperForPropertyGrid instance1) ? (context.Instance is IFormDesignerActionParams instance2 ? instance2.Component as Component : (Component) null) : (instance1.BaseClass as IWrapper).BaseClass as Component;
    if (context1 != null)
    {
      DesForm desForm = (DesForm) null;
      IFormDesignerEditorHook designerEditorHook = (IFormDesignerEditorHook) null;
      if (context1.Container is IDesignerHost container)
      {
        desForm = container.RootComponent as DesForm;
        designerEditorHook = container.GetService(typeof (IFormDesignerEditorHookable)) is IFormDesignerEditorHookable service ? service.Hook : (IFormDesignerEditorHook) null;
      }
      if (desForm == null)
        desForm = this.GetForm((context1 as Control).Parent);
      PropertyDescriptor propertyDescriptor = context.PropertyDescriptor;
      if (designerEditorHook == null || !designerEditorHook.CanExecuteSelector)
      {
        FieldTypesAttribute attribute1 = propertyDescriptor.Attributes[typeof (FieldTypesAttribute)] as FieldTypesAttribute;
        MultiValueModesAttribute attribute2 = propertyDescriptor.Attributes[typeof (MultiValueModesAttribute)] as MultiValueModesAttribute;
        if (attribute1 != null && attribute2 != null)
        {
          FieldTypes[] fields;
          MultiValueModes[] modes;
          if (context1 is IMPictureBox)
          {
            List<FieldTypes> fieldTypesList = new List<FieldTypes>();
            List<MultiValueModes> multiValueModesList = new List<MultiValueModes>();
            switch (((IMPictureBox) context1).PictureSelectMode)
            {
              case PictureSelectMode.Fixed:
                fieldTypesList.Add(FieldTypes.ftObjectLink);
                multiValueModesList.AddRange((IEnumerable<MultiValueModes>) attribute2.MultiValuesModes);
                break;
              case PictureSelectMode.UserRuntime:
                fieldTypesList.Add(FieldTypes.ftFile);
                multiValueModesList.Add(MultiValueModes.SingleValue);
                break;
              default:
                fieldTypesList.AddRange((IEnumerable<FieldTypes>) attribute1.FieldTypes);
                multiValueModesList.AddRange((IEnumerable<MultiValueModes>) attribute2.MultiValuesModes);
                break;
            }
            fields = fieldTypesList.ToArray();
            modes = multiValueModesList.ToArray();
          }
          else
          {
            fields = attribute1.FieldTypes;
            modes = attribute2.MultiValuesModes;
          }
          using (AttributeEditorForm attributeEditorForm = new AttributeEditorForm(desForm?.Links, value as AttributeInfo, fields, modes))
          {
            if (context1 is AttrTextEdit)
              attributeEditorForm.SysSpecialAttr = new List<int>((IEnumerable<int>) new int[1]
              {
                -7
              });
            else if (context1 is AttrTextBtn)
              attributeEditorForm.SysSpecialAttr = new List<int>((IEnumerable<int>) new int[2]
              {
                -14,
                -8
              });
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
        if (designerEditorHook.ExecuteSelector((object) context1, propertyDescriptor, ref obj))
          value = obj == value ? value : obj;
      }
    }
    return value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="cnt"></param>
  /// <returns></returns>
  private DesForm GetForm(Control cnt)
  {
    form = (DesForm) null;
    switch (cnt)
    {
      case null:
      case DesForm form:
        return form;
      default:
        form = this.GetForm(cnt.Parent);
        goto case null;
    }
  }
}
