// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Descriptors.AttrTextBtnDataSourceNameEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Wrappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.FormDesigner.Descriptors;

/// <summary>Редактор для свойства "Наименование источника".</summary>
public class AttrTextBtnDataSourceNameEditor : BaseDropDownEditor
{
  /// <summary>Наименование редактируемого контрола</summary>
  private string _ctrlName = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="ctrl">Родительский контрол</param>
  /// <param name="collection">Список наименований и найденных контролов</param>
  private void GetAttrTextBtnCollection(Control ctrl, List<string> collection)
  {
    foreach (Control control in (ArrangedElementCollection) ctrl.Controls)
    {
      if (control is AttrTextBtn)
      {
        if (!(this._ctrlName == control.Name) && !collection.Contains(control.Name))
          collection.Add(control.Name);
      }
      else if (control is IFormDesignerControl formDesignerControl && formDesignerControl.CanContainsChildren && control.HasChildren)
        this.GetAttrTextBtnCollection(control, collection);
    }
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
    AttrTextBtn attrTextBtn = (AttrTextBtn) null;
    if (context != null && context.Instance != null && context.Instance is IWrapper)
      attrTextBtn = (context.Instance as IWrapper).BaseClass as AttrTextBtn;
    if (attrTextBtn != null)
    {
      this._ctrlName = attrTextBtn.Name;
      Form form = attrTextBtn.FindForm();
      if (form != null)
      {
        List<string> stringList = new List<string>()
        {
          string.Empty
        };
        this.GetAttrTextBtnCollection((Control) form, stringList);
        int height = stringList.Count > 5 ? 73 : stringList.Count * 14 + 3;
        string str = Convert.ToString(value);
        value = (object) Convert.ToString(this.SetEditor(provider, height, (ICollection) stringList, stringList.Contains(str) ? (object) str : (object) string.Empty));
      }
    }
    return value;
  }
}
