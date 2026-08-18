// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.FileDocumentTypesUIEditor
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Settings.PropertyEditors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class FileDocumentTypesUIEditor : UITypeEditor
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
    ChangeTrackingListAdapter<GlobalId<int>> source;
    if (value is GlobalId<int>)
    {
      source = new ChangeTrackingListAdapter<GlobalId<int>>(1);
      source.Items.Add((GlobalId<int>) value);
    }
    else
      source = (ChangeTrackingListAdapter<GlobalId<int>>) value;
    using (ObjectTypeListEditorForm typeListEditorForm = new ObjectTypeListEditorForm())
    {
      typeListEditorForm.SelectorFormRootType = MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
      typeListEditorForm.ObjectTypes = source.ToList<GlobalId<int>>();
      if (typeListEditorForm.ShowDialog() == DialogResult.OK)
        return (object) new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) typeListEditorForm.ObjectTypes);
    }
    return value;
  }
}
