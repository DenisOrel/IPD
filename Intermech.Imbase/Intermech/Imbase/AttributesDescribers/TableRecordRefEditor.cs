// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.TableRecordRefEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class TableRecordRefEditor : UITypeEditor
{
  private UITypeEditor editor = (UITypeEditor) new TableRecordRefFlagEditor();

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return this.editor.GetEditStyle();
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    object obj = value;
    if (obj is TableRecordRefPropertyClass)
      obj = (object) ((TableRecordRefPropertyClass) obj).TableRecordRef;
    object aTableRecordRef = this.editor.EditValue(context, provider, obj);
    return value == null && aTableRecordRef == null || aTableRecordRef.Equals(obj) ? value : (object) new TableRecordRefPropertyClass(aTableRecordRef as string);
  }
}
