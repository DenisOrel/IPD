// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentGroupUIEditor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Settings.PropertyEditors;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class DocumentGroupUIEditor : UITypeEditor
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
    DocumentGroupViewModel documentGroupViewModel = ((DocumentGroupViewModel) value).Clone();
    return new ObjectTypeListEditorForm()
    {
      ObjectTypes = documentGroupViewModel.DocumentTypes
    }.ShowDialog() != DialogResult.OK ? value : (object) documentGroupViewModel;
  }
}
