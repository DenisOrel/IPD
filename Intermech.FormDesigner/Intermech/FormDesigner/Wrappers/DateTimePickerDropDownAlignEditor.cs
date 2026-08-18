// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.DateTimePickerDropDownAlignEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// Положение выпадающего календаря в контроле управления.
/// </summary>
public class DateTimePickerDropDownAlignEditor : BaseDropDownEditor
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    DateTimePickerDropDownAlignConverter downAlignConverter = new DateTimePickerDropDownAlignConverter();
    return downAlignConverter.Hash[this.SetEditor(provider, 32 /*0x20*/, downAlignConverter.Hash.forward.Values, downAlignConverter.Hash[value])];
  }
}
