// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.ObjectTypeUITypeEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Params;

internal class ObjectTypeUITypeEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    using (SelectorForm selectorForm = new SelectorForm("Выберите тип объекта", 4, false))
      return selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0 ? value : selectorForm.IDList[0];
  }
}
