
// Type: Intermech.Client.Core.Organizer.OrganizerPropertiesView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core.Organizer;

/// <summary>Вьюшка переписана для контроля интервала времени.</summary>
[ViewDescriptionProvider(typeof (OrganizerPropertiesView.OrganizerPropertiesViewDescriptionProvider))]
public class OrganizerPropertiesView : PropertiesView
{
  private int _startAttrID = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
  private int _finishAttrID = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
  private string _setDateMsg = string.Empty;
  private string _setDateCaption = string.Empty;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Конструктор.</summary>
  public OrganizerPropertiesView()
  {
    this.InitializeComponent();
    this._setDateMsg = LocalizationHolder.rm.GetString("Organizer_FinishDate_LessStartDateMessage");
    this._setDateCaption = LocalizationHolder.rm.GetString("Organizer_OrganizerTask");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected override void ViewPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
  {
    if (!(e.ChangedItem.PropertyDescriptor is SimplePropDescriptor propertyDescriptor))
      return;
    if (e.ChangedItem.Parent != null)
    {
      if (this._finishAttrID == propertyDescriptor.PropID)
      {
        GridItem property = this.FindProperty(e.ChangedItem.Parent.GridItems, this._startAttrID);
        if (property != null)
        {
          if (e.ChangedItem.Value == null || e.ChangedItem.Value == DBNull.Value)
          {
            property.PropertyDescriptor.SetValue((object) this, (object) null);
            (property.PropertyDescriptor as SimplePropDescriptor).ValueChanged = true;
          }
          else if (property.Value != null && (DateTime) e.ChangedItem.Value < (DateTime) property.Value)
          {
            int num = (int) MessageBox.Show(this._setDateMsg, this._setDateCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            e.ChangedItem.PropertyDescriptor.SetValue((object) this, property.Value);
          }
        }
      }
      else if (this._startAttrID == propertyDescriptor.PropID && e.ChangedItem.Value != null && e.ChangedItem.Value != DBNull.Value)
      {
        GridItem property = this.FindProperty(e.ChangedItem.Parent.GridItems, this._finishAttrID);
        if (property != null && (property.Value == null || property.Value == DBNull.Value || (DateTime) e.ChangedItem.Value > (DateTime) property.Value))
        {
          property.PropertyDescriptor.SetValue((object) this, e.ChangedItem.Value);
          (property.PropertyDescriptor as SimplePropDescriptor).ValueChanged = true;
        }
      }
    }
    this.PropertyGrid.Refresh();
    base.ViewPropertyValueChanged(sender, e);
  }

  /// <summary>
  /// Поиск свойства в PropertyGrid, соответствующего атрибуту с указанным propID.
  /// </summary>
  /// <param name="collection">Коллекция свойств</param>
  /// <param name="propID">Идентификатор атрибута, свойство которого нужно найти в PropertyGrid</param>
  /// <returns>Искомое свойство</returns>
  private GridItem FindProperty(GridItemCollection collection, int propID)
  {
    foreach (GridItem property in collection)
    {
      if (property.PropertyDescriptor is SimplePropDescriptor propertyDescriptor && propertyDescriptor.PropID == propID)
        return property;
    }
    return (GridItem) null;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OrganizerPropertiesView));
    this.pnButtons.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    componentResourceManager.ApplyResources((object) this.pnButtons, "pnButtons");
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.DoubleBuffered = true;
    this.Name = nameof (OrganizerPropertiesView);
    this.pnButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private sealed class OrganizerPropertiesViewDescriptionProvider : 
    PropertiesView.PropertiesViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return base.DoGetViewDescription(selectedItems, serviceProvider);
    }
  }
}
