// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DrawingTypeListUIEditor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Settings.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DrawingTypeListUIEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (ChangeTrackingListAdapter<DrawingTypeSettings>)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    ChangeTrackingListAdapter<DrawingTypeSettings> collection1 = (ChangeTrackingListAdapter<DrawingTypeSettings>) value;
    List<DrawingTypeSettings> collection2 = new List<DrawingTypeSettings>((IEnumerable<DrawingTypeSettings>) collection1);
    string str = context.PropertyDescriptor.DisplayName;
    if (!string.IsNullOrEmpty(context.PropertyDescriptor.Description))
      str = $"{str} - {context.PropertyDescriptor.Description}";
    ObjectTypeListEditorForm2 typeListEditorForm2 = new ObjectTypeListEditorForm2();
    typeListEditorForm2.Text = str;
    typeListEditorForm2.List = (IList) collection2;
    typeListEditorForm2.ListAdapter = (IObjectTypeListAdapter) new DrawingTypeListUIEditor.DrawingTypeListAdapter();
    typeListEditorForm2.EditItem += new EventHandler<ObjectTypeListEditorForm2.ChangeItemEventArgs>(this.EditDrawingType);
    return typeListEditorForm2.ShowDialog() != DialogResult.OK ? (object) collection1 : (object) new ChangeTrackingListAdapter<DrawingTypeSettings>((IEnumerable<DrawingTypeSettings>) collection2);
  }

  private void EditDrawingType(object sender, ObjectTypeListEditorForm2.ChangeItemEventArgs e)
  {
    using (DrawingTypeEditor drawingTypeEditor = new DrawingTypeEditor())
    {
      DrawingTypeSettings listItem = (DrawingTypeSettings) e.ListItem;
      drawingTypeEditor.DrawingType = listItem.Clone();
      if (drawingTypeEditor.ShowDialog() != DialogResult.OK)
        return;
      e.ChangedItem = (object) drawingTypeEditor.DrawingType;
    }
  }

  private sealed class DrawingTypeListAdapter : IObjectTypeListAdapter
  {
    public object Create(Guid objectTypeGuid, int objectTypeId, string objectTypeName)
    {
      return (object) new DrawingTypeSettings(new GlobalId<int>(objectTypeGuid, objectTypeId, objectTypeName));
    }

    public int GetObjectTypeId(object listItem) => ((DrawingTypeSettings) listItem).DocumentType.Id;

    public string GetObjectTypeName(object listItem)
    {
      return ((DrawingTypeSettings) listItem).DocumentType.Name;
    }
  }
}
