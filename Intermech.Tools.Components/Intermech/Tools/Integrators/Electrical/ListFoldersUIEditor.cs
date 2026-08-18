// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ListFoldersUIEditor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Редактор списка папок в каталоге проекта, содержимое которых не импортируется в базу IPS
/// </summary>
[Serializable]
public sealed class ListFoldersUIEditor : CollectionEditor
{
  public ListFoldersUIEditor()
    : base(typeof (List<FolderNameSurrogate>))
  {
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (ChangeTrackingListAdapter<FolderNameSurrogate>)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : base.GetEditStyle(context);
  }

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Text = this.Context.PropertyDescriptor.DisplayName;
    collectionForm.Width = 645;
    collectionForm.Height = 400;
    collectionForm.Controls[0].Controls[3].Text = "&Папки проекта:";
    collectionForm.Controls[0].Controls[2].Text = "&Название:";
    PropertyGrid itemGrid = (PropertyGrid) collectionForm.Controls[0].Controls[5];
    itemGrid.ToolbarVisible = false;
    itemGrid.HelpVisible = true;
    itemGrid.PropertySort = PropertySort.NoSort;
    itemGrid.SelectedObjectsChanged += new EventHandler(this.AttachHighlighter);
    itemGrid.ContextMenuStrip = new ContextMenuStrip();
    itemGrid.ContextMenuStrip.Items.Add("Очистить значение", (Image) null, (EventHandler) ((sender, e) => this.ResetFolder(itemGrid)));
    itemGrid.ContextMenuStrip.Opening += (CancelEventHandler) ((sender, e) => this.ResetFolderMenuOpening(itemGrid, e));
    return collectionForm;
  }

  private void AttachHighlighter(object sender, EventArgs e)
  {
    PropertyGrid propertyGrid = (PropertyGrid) sender;
    if (propertyGrid.SelectedObject == null || !(propertyGrid.SelectedObject is ICloneable) || propertyGrid.SelectedObject is EditableObjectChangeHighlighter)
      return;
    propertyGrid.SelectedObject = (object) new EditableObjectChangeHighlighter((ICloneable) propertyGrid.SelectedObject);
  }

  private void ResetFolderMenuOpening(PropertyGrid itemGrid, CancelEventArgs e)
  {
    GridItem selectedGridItem = itemGrid.SelectedGridItem;
    e.Cancel = selectedGridItem == null || selectedGridItem.PropertyDescriptor.PropertyType != typeof (string) || selectedGridItem.Value == null;
  }

  private void ResetFolder(PropertyGrid itemGrid)
  {
    if (itemGrid.SelectedGridItem == null || !(itemGrid.SelectedGridItem.PropertyDescriptor.PropertyType == typeof (string)))
      return;
    itemGrid.SelectedGridItem.PropertyDescriptor.SetValue(itemGrid.SelectedObject, (object) null);
    itemGrid.Refresh();
    itemGrid.Parent.Refresh();
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    ChangeTrackingListAdapter<FolderNameSurrogate> objA = (ChangeTrackingListAdapter<FolderNameSurrogate>) value;
    ChangeTrackingListAdapter<FolderNameSurrogate> objB = objA.Clone();
    base.EditValue(context, provider, (object) objB.Items);
    return !object.Equals((object) objA, (object) objB) ? (object) objB : (object) objA;
  }

  protected override object CreateInstance(System.Type itemType)
  {
    object instance = base.CreateInstance(itemType);
    if (instance.GetType() == typeof (FolderNameSurrogate))
      ((FolderNameSurrogate) instance).FolderName = "";
    return instance;
  }

  protected override string GetDisplayText(object value)
  {
    return ((FolderNameSurrogate) value).FolderName;
  }
}
