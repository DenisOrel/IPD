// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DataSourceNameEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для свойства "Наименование источника".</summary>
public class DataSourceNameEditor : BaseDropDownEditor
{
  /// <summary>Форма, на которой расположен контрол</summary>
  private Form _form;
  /// <summary>Наименование редактируемого контрола</summary>
  private string _ctrlName = string.Empty;

  /// <summary>
  /// Поиск контролов "Список объектов" у указанного родительского контрола.
  /// </summary>
  /// <param name="ctrl">Родительский контрол</param>
  /// <param name="collection">Список наименований и найденных контролов</param>
  private void GetObjectsListCollection(Control ctrl, List<string> collection)
  {
    foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
    {
      switch (control)
      {
        case TabPage _:
label_6:
          this.GetObjectsListCollection(control, collection);
          continue;
        case ObjectsList ctrl1:
          if (!(this._ctrlName == control.Name) && !collection.Contains(control.Name) && this.ValidateControl(ctrl1))
          {
            collection.Add(control.Name);
            continue;
          }
          continue;
        case IFormDesignerControl formDesignerControl:
          if (!formDesignerControl.CanContainsChildren || !control.HasChildren)
            continue;
          goto label_6;
        default:
          continue;
      }
    }
  }

  /// <summary>Проверка на зацикливание ссылок контрола на контрол.</summary>
  /// <param name="ctrl">Проверяемый контрол</param>
  /// <returns>Результат проверки</returns>
  private bool ValidateControl(ObjectsList ctrl)
  {
    bool flag = false;
    if (this._form != null && ctrl != null)
    {
      if (string.IsNullOrEmpty(ctrl.DataSourceName))
        flag = true;
      else if (this._ctrlName != ctrl.DataSourceName)
      {
        Control[] controlArray = this._form.Controls.Find(ctrl.DataSourceName, true);
        if (controlArray.Length != 0)
        {
          int index = 0;
          while (index < controlArray.Length && (!(controlArray[index] is ObjectsList ctrl1) || !(flag = this.ValidateControl(ctrl1))))
            ++index;
        }
      }
    }
    return flag;
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
    ObjectsList objectsList = ObjectsListDescriptor.GetObjectsList(context);
    if (objectsList != null)
    {
      this._ctrlName = objectsList.Name;
      this._form = objectsList.FindForm();
      if (this._form != null)
      {
        List<string> stringList = new List<string>()
        {
          string.Empty
        };
        this.GetObjectsListCollection((Control) this._form, stringList);
        int height = stringList.Count > 5 ? 73 : stringList.Count * 14 + 3;
        string str = Convert.ToString(value);
        value = (object) Convert.ToString(this.SetEditor(provider, height, (ICollection) stringList, stringList.Contains(str) ? (object) str : (object) string.Empty));
      }
    }
    return value;
  }
}
