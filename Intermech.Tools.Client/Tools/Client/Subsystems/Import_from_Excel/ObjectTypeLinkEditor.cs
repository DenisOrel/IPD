// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ObjectTypeLinkEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Localization;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

public class ObjectTypeLinkEditor : UITypeEditor
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
    if (context != null && provider != null && provider.GetService(typeof (IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is int)
    {
      using (SelectorForm dialog = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Tools.Client_274"), typeof (ObjectTypeFolder), false))
      {
        if (service.ShowDialog((Form) dialog).Equals((object) DialogResult.OK))
        {
          if (dialog.IDList.Count == 1)
            return (object) (int) dialog.IDList[0];
        }
      }
    }
    return base.EditValue(context, provider, value);
  }
}
