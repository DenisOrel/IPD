// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.AfterDoubleClickEditor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Редактор для свойства "Список".</summary>
public class AfterDoubleClickEditor : BaseDropDownEditor
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
    AfterDoubleClickConverter doubleClickConverter = new AfterDoubleClickConverter();
    return doubleClickConverter.Hash[this.SetEditor(provider, 32 /*0x20*/, doubleClickConverter.Hash.forward.Values, doubleClickConverter.Hash[value])];
  }
}
