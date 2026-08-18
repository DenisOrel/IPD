// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.CustomBrowseFolderDialogEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Params;

internal class CustomBrowseFolderDialogEditor : UITypeEditor
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
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      using (BrowseFolderDialog browseFolderDialog = new BrowseFolderDialog(ServiceUtils.GetService<IServerBriefcase>((object) sessionKeeper.Session, true).GetFolderBrowser()))
        return browseFolderDialog.ShowDialog() == DialogResult.OK ? (object) browseFolderDialog.Path : value;
    }
  }
}
