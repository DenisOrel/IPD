// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.AttributesComplianceUIEditor
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

[Serializable]
internal sealed class AttributesComplianceUIEditor : CollectionEditor
{
  public AttributesComplianceUIEditor()
    : base(typeof (List<AttributesCompliance>))
  {
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (ChangeTrackingListAdapter<AttributesCompliance>)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : base.GetEditStyle(context);
  }

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Text = this.Context.PropertyDescriptor.DisplayName;
    collectionForm.Width = 645;
    collectionForm.Height = 400;
    collectionForm.Controls[0].Controls[3].Text = "&Соответствия атрибутов:";
    collectionForm.Controls[0].Controls[2].Text = "&Атрибуты:";
    PropertyGrid itemGrid = (PropertyGrid) collectionForm.Controls[0].Controls[5];
    itemGrid.ToolbarVisible = false;
    itemGrid.HelpVisible = true;
    itemGrid.PropertySort = PropertySort.NoSort;
    itemGrid.SelectedObjectsChanged += new EventHandler(this.AttachHighlighter);
    itemGrid.ContextMenuStrip = new ContextMenuStrip();
    itemGrid.ContextMenuStrip.Items.Add("Очистить значение", (Image) null, (EventHandler) ((sender, e) => this.ResetAttributes(itemGrid)));
    itemGrid.ContextMenuStrip.Opening += (CancelEventHandler) ((sender, e) => this.ResetAttributesMenuOpening(itemGrid, e));
    return collectionForm;
  }

  private void AttachHighlighter(object sender, EventArgs e)
  {
    PropertyGrid propertyGrid = (PropertyGrid) sender;
    if (propertyGrid.SelectedObject == null || !(propertyGrid.SelectedObject is ICloneable) || propertyGrid.SelectedObject is EditableObjectChangeHighlighter)
      return;
    propertyGrid.SelectedObject = (object) new EditableObjectChangeHighlighter((ICloneable) propertyGrid.SelectedObject);
  }

  private void ResetAttributesMenuOpening(PropertyGrid itemGrid, CancelEventArgs e)
  {
    GridItem selectedGridItem = itemGrid.SelectedGridItem;
    e.Cancel = selectedGridItem == null || selectedGridItem.PropertyDescriptor.PropertyType != typeof (string) || selectedGridItem.Value == null;
  }

  private void ResetAttributes(PropertyGrid itemGrid)
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
    ChangeTrackingListAdapter<AttributesCompliance> objA = (ChangeTrackingListAdapter<AttributesCompliance>) value;
    ChangeTrackingListAdapter<AttributesCompliance> objB = objA.Clone();
    base.EditValue(context, provider, (object) objB.Items);
    return !object.Equals((object) objA, (object) objB) ? (object) objB : (object) objA;
  }

  protected override object CreateInstance(System.Type itemType)
  {
    object instance = base.CreateInstance(itemType);
    if (instance.GetType() == typeof (AttributesCompliance))
    {
      AttributesCompliance attributesCompliance = (AttributesCompliance) instance;
      attributesCompliance.DBAttributeName = "";
      attributesCompliance.CADAttributeName = "";
    }
    return instance;
  }

  protected override string GetDisplayText(object value)
  {
    AttributesCompliance attributesCompliance = (AttributesCompliance) value;
    return $"{attributesCompliance.DBAttributeName}={attributesCompliance.CADAttributeName}";
  }
}
