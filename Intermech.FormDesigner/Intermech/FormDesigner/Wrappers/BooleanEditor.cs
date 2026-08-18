// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.BooleanEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>
/// 
/// </summary>
public class BooleanEditor : BaseDropDownEditor
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
    BooleanConverter booleanConverter = new BooleanConverter();
    return booleanConverter.Hash[this.SetEditor(provider, 32 /*0x20*/, booleanConverter.Hash.forward.Values, booleanConverter.Hash[value])];
  }
}
