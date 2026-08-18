// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.RelationTypeAttEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public class RelationTypeAttEditor : UITypeEditor
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
    if (context != null && provider != null)
    {
      SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), LocalizationHolder.rm.GetString("Tools.Client_273"), typeof (RelationTypeFolder), false);
      if (selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0)
        return (object) (int) selectorForm.IDList[0];
    }
    return base.EditValue(context, provider, value);
  }
}
