// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.TemplatesObjectsEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Templates;

internal class TemplatesObjectsEditor : UITypeEditor
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
    TemplatesBody templatesBody = (TemplatesBody) null;
    if (value != null)
      templatesBody = value as TemplatesBody;
    SymbolSetEditor symbolSetEditor = new SymbolSetEditor();
    symbolSetEditor.Data = templatesBody != null ? templatesBody.Body : string.Empty;
    return symbolSetEditor.ShowDialog() == DialogResult.OK ? (object) new TemplatesBody(symbolSetEditor.Data, UseTemplate.Obj) : (object) templatesBody ?? (object) new TemplatesBody(string.Empty, UseTemplate.Obj);
  }
}
