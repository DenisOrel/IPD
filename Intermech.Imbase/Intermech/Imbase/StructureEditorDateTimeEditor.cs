// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.StructureEditorDateTimeEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

#nullable disable
namespace Intermech.Imbase;

internal class StructureEditorDateTimeEditor : DateTimeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (value == null || value == DBNull.Value || string.IsNullOrEmpty(value.ToString()))
      value = (object) DateTime.Today;
    DateTime result = DateTime.Now;
    return value is string && DateTime.TryParse(value.ToString(), out result) ? base.EditValue(context, provider, (object) result) : base.EditValue(context, provider, value);
  }
}
