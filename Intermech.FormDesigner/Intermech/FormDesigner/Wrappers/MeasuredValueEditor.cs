// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.MeasuredValueEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class MeasuredValueEditor : UITypeEditor
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
    ArrayList listByAttributeId = MeasureEditor.GetMeasureDescriptorListByAttributeId(MetaDataHelper.GetAttributeTypeID(((context.Instance as IWrapper).BaseClass as AttrMeasuredEdit).AttributeInfo.AttributeGuid));
    using (MeasureForm measureForm = new MeasureForm())
    {
      MeasuredValue aMeasureValue = value as MeasuredValue;
      if (measureForm.ExecuteDialog(ref aMeasureValue, listByAttributeId.ToArray(typeof (MeasureDescriptor)) as MeasureDescriptor[]) == DialogResult.OK)
        value = (object) aMeasureValue;
    }
    return value;
  }
}
