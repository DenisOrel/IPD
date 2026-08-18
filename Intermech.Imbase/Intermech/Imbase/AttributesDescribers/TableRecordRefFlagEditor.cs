// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.TableRecordRefFlagEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Selection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;
using System.ComponentModel;
using System.Drawing.Design;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class TableRecordRefFlagEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (!(ServicesManager.GetService(typeof (IImbaseSelector)) is ImbaseSelector service))
      return value;
    string empty = string.Empty;
    if (value != null)
      empty = value.ToString();
    string str = service.SelectRecord(empty, true);
    return !string.IsNullOrEmpty(str) ? (object) str : value;
  }
}
