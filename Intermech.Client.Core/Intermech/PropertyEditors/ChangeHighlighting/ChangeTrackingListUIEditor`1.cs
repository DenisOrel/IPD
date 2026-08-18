
// Type: Intermech.PropertyEditors.ChangeHighlighting.ChangeTrackingListUIEditor`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.ChangeHighlighting;

/// <summary>
/// Реализует редактор для списков типа <see cref="T:Intermech.PropertyEditors.ChangeHighlighting.ChangeTrackingListAdapter`1" /> .
/// </summary>
/// <typeparam name="T">Тип значений в списке</typeparam>
public class ChangeTrackingListUIEditor<T> : CollectionEditor where T : ICloneable, new()
{
  private readonly System.Type allowedListType;

  public ChangeTrackingListUIEditor(System.Type type)
    : base(typeof (IList))
  {
    this.allowedListType = typeof (ChangeTrackingListAdapter<T>);
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!this.allowedListType.IsAssignableFrom(context.PropertyDescriptor.PropertyType) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : base.GetEditStyle(context);
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    ChangeTrackingListAdapter<T> objA = (ChangeTrackingListAdapter<T>) value;
    ChangeTrackingListAdapter<T> objB = objA.Clone();
    base.EditValue(context, provider, (object) objB.Items);
    return !object.Equals((object) objA, (object) objB) ? (object) objB : (object) objA;
  }

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Text = this.Context.PropertyDescriptor.DisplayName;
    collectionForm.Width = 645;
    collectionForm.Height = 400;
    PropertyGrid control = (PropertyGrid) collectionForm.Controls[0].Controls[5];
    control.ToolbarVisible = false;
    control.HelpVisible = true;
    control.PropertySort = PropertySort.NoSort;
    control.SelectedObjectsChanged += new EventHandler(this.AttachHighlighter);
    return collectionForm;
  }

  private void AttachHighlighter(object sender, EventArgs e)
  {
    PropertyGrid propertyGrid = (PropertyGrid) sender;
    if (propertyGrid.SelectedObject == null || !(propertyGrid.SelectedObject is ICloneable) || propertyGrid.SelectedObject is EditableObjectChangeHighlighter)
      return;
    propertyGrid.SelectedObject = (object) new EditableObjectChangeHighlighter((ICloneable) propertyGrid.SelectedObject);
  }

  protected override System.Type CreateCollectionItemType() => typeof (T);
}
