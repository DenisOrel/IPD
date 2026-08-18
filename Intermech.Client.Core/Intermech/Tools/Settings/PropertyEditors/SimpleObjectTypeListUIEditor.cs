
// Type: Intermech.Tools.Settings.PropertyEditors.SimpleObjectTypeListUIEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class SimpleObjectTypeListUIEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (ChangeTrackingListAdapter<GlobalId<int>>)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    ChangeTrackingListAdapter<GlobalId<int>> collection1 = (ChangeTrackingListAdapter<GlobalId<int>>) value;
    string str = context.PropertyDescriptor.DisplayName;
    if (!string.IsNullOrEmpty(context.PropertyDescriptor.Description))
      str = $"{str} - {context.PropertyDescriptor.Description}";
    List<GlobalId<int>> collection2 = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) collection1);
    ObjectTypeListEditorForm2 typeListEditorForm2 = new ObjectTypeListEditorForm2();
    typeListEditorForm2.Text = str;
    typeListEditorForm2.List = (IList) collection2;
    typeListEditorForm2.ListAdapter = (IObjectTypeListAdapter) new SimpleObjectTypeListUIEditor.ObjectTypeListAdapter();
    if (typeListEditorForm2.ShowDialog() != DialogResult.OK)
      return (object) collection1;
    ChangeTrackingListAdapter<GlobalId<int>> trackingListAdapter = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) collection2);
    if (trackingListAdapter.Equals((object) collection1))
      trackingListAdapter = collection1;
    return (object) trackingListAdapter;
  }

  private sealed class ObjectTypeListAdapter : IObjectTypeListAdapter
  {
    public object Create(Guid objectTypeGuid, int objectTypeId, string objectTypeName)
    {
      return (object) new GlobalId<int>(objectTypeGuid, objectTypeId, objectTypeName);
    }

    public int GetObjectTypeId(object listItem) => ((LocalId<int>) listItem).Id;

    public string GetObjectTypeName(object listItem) => ((LocalId<int>) listItem).Name;
  }
}
