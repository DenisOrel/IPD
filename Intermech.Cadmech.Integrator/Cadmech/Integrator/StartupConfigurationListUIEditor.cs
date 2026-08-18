// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.StartupConfigurationListUIEditor
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.PropertyEditors.ChangeHighlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator;

[Serializable]
internal sealed class StartupConfigurationListUIEditor : CollectionEditor
{
  public StartupConfigurationListUIEditor()
    : base(typeof (List<StartupConfigurationSurrogate>))
  {
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return (!(context.PropertyDescriptor.PropertyType == typeof (ChangeTrackingListAdapter<StartupConfigurationSurrogate>)) ? 0 : (!context.PropertyDescriptor.IsReadOnly ? 1 : 0)) == 0 ? UITypeEditorEditStyle.None : base.GetEditStyle(context);
  }

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Text = this.Context.PropertyDescriptor.DisplayName;
    collectionForm.Width = 645;
    collectionForm.Height = 400;
    collectionForm.Controls[0].Controls[3].Text = "&Роль пользователя:";
    collectionForm.Controls[0].Controls[2].Text = "&Свойства роли:";
    PropertyGrid itemGrid = (PropertyGrid) collectionForm.Controls[0].Controls[5];
    itemGrid.ToolbarVisible = false;
    itemGrid.HelpVisible = true;
    itemGrid.PropertySort = PropertySort.NoSort;
    itemGrid.SelectedObjectsChanged += new EventHandler(this.AttachHighlighter);
    itemGrid.ContextMenuStrip = new ContextMenuStrip();
    itemGrid.ContextMenuStrip.Items.Add("Очистить роль", (Image) null, (EventHandler) ((sender, e) => this.ResetUserRole(itemGrid)));
    itemGrid.ContextMenuStrip.Opening += (CancelEventHandler) ((sender, e) => this.ResetUserRoleMenuOpening(itemGrid, e));
    return collectionForm;
  }

  private void AttachHighlighter(object sender, EventArgs e)
  {
    PropertyGrid propertyGrid = (PropertyGrid) sender;
    if (propertyGrid.SelectedObject == null || !(propertyGrid.SelectedObject is ICloneable) || propertyGrid.SelectedObject is EditableObjectChangeHighlighter)
      return;
    propertyGrid.SelectedObject = (object) new EditableObjectChangeHighlighter((ICloneable) propertyGrid.SelectedObject);
  }

  private void ResetUserRoleMenuOpening(PropertyGrid itemGrid, CancelEventArgs e)
  {
    GridItem selectedGridItem = itemGrid.SelectedGridItem;
    e.Cancel = selectedGridItem == null || selectedGridItem.PropertyDescriptor.PropertyType != typeof (UserRoleMarker) || selectedGridItem.Value == null;
  }

  private void ResetUserRole(PropertyGrid itemGrid)
  {
    if (itemGrid.SelectedGridItem == null || !(itemGrid.SelectedGridItem.PropertyDescriptor.PropertyType == typeof (UserRoleMarker)))
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
    ChangeTrackingListAdapter<StartupConfigurationSurrogate> objA = (ChangeTrackingListAdapter<StartupConfigurationSurrogate>) value;
    ChangeTrackingListAdapter<StartupConfigurationSurrogate> objB = objA.Clone();
    base.EditValue(context, provider, (object) objB.Items);
    return !object.Equals((object) objA, (object) objB) ? (object) objB : (object) objA;
  }

  protected override object CreateInstance(System.Type itemType)
  {
    object instance = base.CreateInstance(itemType);
    if (instance.GetType() == typeof (StartupConfigurationSurrogate))
    {
      StartupConfigurationSurrogate configurationSurrogate = (StartupConfigurationSurrogate) instance;
      configurationSurrogate.UseSpecificProfile = true;
      configurationSurrogate.ProfileName = "<<CADM_IPS>>";
    }
    return instance;
  }

  protected override string GetDisplayText(object value)
  {
    return $"'{SettingsUtils.GetRoleCaption(((StartupConfigurationSurrogate) value).UserRole)}'";
  }
}
