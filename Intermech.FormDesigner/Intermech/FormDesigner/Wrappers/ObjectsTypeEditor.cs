// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ObjectsTypeEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для свойства "Тип".</summary>
public class ObjectsTypeEditor : UITypeEditor
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
    object obj = value;
    int result = -1;
    string str = Convert.ToString(value);
    if (GuidHelper.IsGuid(str))
      result = MetaDataHelper.GetObjectTypeID(new Guid(str));
    System.Type[] aCheckType = new System.Type[2]
    {
      typeof (ObjectTypesFolder),
      typeof (ObjectTypeFolder)
    };
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("FormDesigner_AllObjectsTypes"), aCheckType, false);
    selectorForm.ClearSelection();
    selectorForm.InitSelectionAsType(new ArrayList((ICollection) new int[1]
    {
      result
    }), new ArrayList((ICollection) new System.Type[1]
    {
      typeof (ObjectTypeFolder)
    }));
    if (selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0)
      obj = (object) (int.TryParse(Convert.ToString(selectorForm.IDList[0]), out result) ? MetaDataHelper.GetObjectTypeGuid(result) : Guid.Empty);
    return obj;
  }
}
