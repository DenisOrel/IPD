// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ObjectsListDescriptor
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.FormDesigner.Descriptors;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Дескриптор для контрола "Список объектов".</summary>
internal class ObjectsListDescriptor : ICustomTypeDescriptor, IWrapper
{
  /// <summary>Отображаемые свойства</summary>
  private PropertyDescriptorCollection _pdc;
  /// <summary>Контрол</summary>
  private ObjectsList _ctrl;

  /// <summary>Конструктор.</summary>
  /// <param name="ctrl">Контрол</param>
  public ObjectsListDescriptor(ObjectsList ctrl)
  {
    this._ctrl = ctrl;
    this.CreatePDC();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public AttributeCollection GetAttributes()
  {
    return TypeDescriptor.GetAttributes((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetClassName() => TypeDescriptor.GetClassName((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public TypeConverter GetConverter() => TypeDescriptor.GetConverter((object) this._ctrl, true);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptor GetDefaultEvent()
  {
    return TypeDescriptor.GetDefaultEvent((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editorBaseType"></param>
  /// <returns></returns>
  public object GetEditor(System.Type editorBaseType)
  {
    return TypeDescriptor.GetEditor((object) this._ctrl, editorBaseType, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this._ctrl, attributes, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public EventDescriptorCollection GetEvents()
  {
    return TypeDescriptor.GetEvents((object) this._ctrl, true);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributes"></param>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties(Attribute[] attributes) => this.GetProperties();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public PropertyDescriptorCollection GetProperties()
  {
    return this._pdc ?? new PropertyDescriptorCollection((PropertyDescriptor[]) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pd"></param>
  /// <returns></returns>
  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this._ctrl;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  public object BaseClass => (object) this._ctrl;

  /// <summary>Создать коллекцию свойств.</summary>
  private void CreatePDC()
  {
    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this._ctrl, true);
    this._pdc = new PropertyDescriptorCollection((PropertyDescriptor[]) null);
    string empty = string.Empty;
    string category1 = LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Appearance");
    Attribute[] attributes1 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category1),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_DisableColumnsGrouping")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_DisableColumnsGrouping")),
      (Attribute) new TypeConverterAttribute(typeof (YesNoConverter)),
      (Attribute) new EditorAttribute(typeof (YesNoEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["DisableColumnsGrouping"], attributes1));
    Attribute[] attributes2 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category1),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_StatusBar_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_StatusBar_Description")),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["ShowStatusBar"], attributes2));
    Attribute[] attributes3 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category1),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_EditMode_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_EditMode_Description")),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["EditMode"], attributes3));
    Attribute[] attributes4 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category1),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ShowContextMenu_Description")),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["ShowContextMenu"], attributes4));
    string category2 = LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Data");
    Attribute[] attributes5 = new Attribute[6]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Columns_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Columns_Description")),
      (Attribute) new TypeConverterAttribute(typeof (ColumnCollectionConverter)),
      (Attribute) new EditorAttribute(typeof (ColumnCollectionEditor), typeof (UITypeEditor)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor1 = new FormDesignerControlsPropertyDescriptor((object) this, properties["ColumnCollection"], attributes5);
    propertyDescriptor1.SetCanReset(true);
    propertyDescriptor1.AfterSetValue += new PropertySetValue(this.OnAfterSetValue);
    propertyDescriptor1.AfterResetValue += new EventHandler(this.OnColumnCollection_AfterResetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor1);
    Attribute[] attributes6 = new Attribute[4]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_DataSourceName")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_DataSourceName")),
      (Attribute) new EditorAttribute(typeof (DataSourceNameEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor2 = new FormDesignerControlsPropertyDescriptor((object) this, properties["DataSourceName"], attributes6);
    propertyDescriptor2.SetCanReset(true);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor2);
    Attribute[] attributes7 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_DefaultSortingColumns_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_DefaultSortingColumns_Description")),
      (Attribute) new TypeConverterAttribute(typeof (ColumnCollectionConverter)),
      (Attribute) new EditorAttribute(typeof (DefaultSortingColumnsEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor3 = new FormDesignerControlsPropertyDescriptor((object) this, properties["DefaultSortingColumns"], attributes7);
    propertyDescriptor3.SetCanReset(true);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor3);
    Attribute[] attributes8 = new Attribute[6]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_62")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_62")),
      (Attribute) new TypeConverterAttribute(typeof (ListContextConverter)),
      (Attribute) new EditorAttribute(typeof (ListContextEditor), typeof (UITypeEditor)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor4 = new FormDesignerControlsPropertyDescriptor((object) this, properties["List"], attributes8);
    propertyDescriptor4.AfterSetValue += new PropertySetValue(this.OnAfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor4);
    Attribute[] attributes9 = new Attribute[4]
    {
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_AfterDoubleClick")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_AfterDoubleClick")),
      (Attribute) new TypeConverterAttribute(typeof (AfterDoubleClickConverter)),
      (Attribute) new EditorAttribute(typeof (AfterDoubleClickEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor5 = new FormDesignerControlsPropertyDescriptor((object) this._ctrl, properties["AfterDoubleClick"], attributes9);
    propertyDescriptor4.ChildProperties = new PropertyDescriptorCollection(new PropertyDescriptor[1]
    {
      (PropertyDescriptor) propertyDescriptor5
    });
    Attribute[] attributes10 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_ColumnsAliases")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ColumnsAliases")),
      (Attribute) new TypeConverterAttribute(typeof (ColumnCollectionConverter)),
      (Attribute) new EditorAttribute(typeof (ColumnsNamesEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor6 = new FormDesignerControlsPropertyDescriptor((object) this, properties["ColumnsAliases"], attributes10);
    propertyDescriptor6.SetCanReset(true);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor6);
    Attribute[] attributes11 = new Attribute[6]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ObjectsType_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_ObjectsType_Description")),
      (Attribute) new TypeConverterAttribute(typeof (ObjectsTypeConverter)),
      (Attribute) new EditorAttribute(typeof (ObjectsTypeEditor), typeof (UITypeEditor)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor7 = new FormDesignerControlsPropertyDescriptor((object) this, properties["ObjectTypesGuid"], attributes11);
    propertyDescriptor7.SetCanReset(true);
    propertyDescriptor7.AfterSetValue += new PropertySetValue(this.OnAfterSetValue);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor7);
    Attribute[] attributes12 = new Attribute[6]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_9")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_9")),
      (Attribute) new TypeConverterAttribute(typeof (RelationsTypeConverter)),
      (Attribute) new EditorAttribute(typeof (RelationsTypeEditor), typeof (UITypeEditor)),
      (Attribute) new RefreshPropertiesAttribute(RefreshProperties.All)
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor8 = new FormDesignerControlsPropertyDescriptor((object) this, properties["RelationsTypeGuid"], attributes12);
    propertyDescriptor8.SetCanReset(true);
    propertyDescriptor8.SetReadOnly((ListContext) properties["List"].GetValue((object) this._ctrl) == ListContext.Objects);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor8);
    Attribute[] attributes13 = new Attribute[6]
    {
      (Attribute) new DefaultValueAttribute(typeof (Guid), Guid.Empty.ToString()),
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_ContextSelection")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_ContextSelection")),
      (Attribute) new TypeConverterAttribute(typeof (SelectionsConverter)),
      (Attribute) new EditorAttribute(typeof (SelectionsEditor), typeof (UITypeEditor))
    };
    FormDesignerControlsPropertyDescriptor propertyDescriptor9 = new FormDesignerControlsPropertyDescriptor((object) this, properties["SelectionGuid"], attributes13);
    propertyDescriptor9.SetCanReset(true);
    this._pdc.Add((PropertyDescriptor) propertyDescriptor9);
    Attribute[] attributes14 = new Attribute[3]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_Tag")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Tag"))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Tag"], attributes14));
    Attribute[] attributes15 = new Attribute[5]
    {
      (Attribute) new CategoryAttribute(category2),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_UseColumnsAliases")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_UseColumnsAliases")),
      (Attribute) new TypeConverterAttribute(typeof (BooleanConverter)),
      (Attribute) new EditorAttribute(typeof (BooleanEditor), typeof (UITypeEditor))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["UseColumnsAliases"], attributes15));
    Attribute[] attributes16 = new Attribute[4]
    {
      (Attribute) new BrowsableAttribute(true),
      (Attribute) new CategoryAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Design")),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropName_Name")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Name"))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Name"], attributes16));
    string category3 = LocalizationHolder.rm.GetString("FormDesigner_PropGroupName_Layout");
    Attribute[] attributes17 = new Attribute[5]
    {
      (Attribute) new DefaultValueAttribute((object) (AnchorStyles.Top | AnchorStyles.Left)),
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_210")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Anchor")),
      (Attribute) new TypeConverterAttribute(typeof (AnchorStylesConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Anchor"], attributes17));
    Attribute[] attributes18 = new Attribute[5]
    {
      (Attribute) new DefaultValueAttribute((object) DockStyle.None),
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_209")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Dock")),
      (Attribute) new TypeConverterAttribute(typeof (DockStyleConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Dock"], attributes18));
    Attribute[] attributes19 = new Attribute[4]
    {
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_206")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Location")),
      (Attribute) new TypeConverterAttribute(typeof (PointConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Location"], attributes19));
    Attribute[] attributes20 = new Attribute[5]
    {
      (Attribute) new DefaultValueAttribute(3),
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_220")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Margin")),
      (Attribute) new TypeConverterAttribute(typeof (MarginPaddingConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Margin"], attributes20));
    Attribute[] attributes21 = new Attribute[5]
    {
      (Attribute) new DefaultValueAttribute((object) new Size(0, 0)),
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_219")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_MaximumSize")),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["MaximumSize"], attributes21));
    Attribute[] attributes22 = new Attribute[5]
    {
      (Attribute) new DefaultValueAttribute((object) new Size(0, 0)),
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_218")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_MinimumSize")),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["MinimumSize"], attributes22));
    Attribute[] attributes23 = new Attribute[4]
    {
      (Attribute) new CategoryAttribute(category3),
      (Attribute) new DisplayNameAttribute(LocalizationHolder.rm.GetString("FormDesigner_208")),
      (Attribute) new DescriptionAttribute(LocalizationHolder.rm.GetString("FormDesigner_PropDescr_Size")),
      (Attribute) new TypeConverterAttribute(typeof (SizeConverter))
    };
    this._pdc.Add((PropertyDescriptor) new FormDesignerControlsPropertyDescriptor((object) this, properties["Size"], attributes23));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="component"></param>
  /// <param name="e"></param>
  private void OnAfterSetValue(object component, SetValueEventArgs e)
  {
    if (e == null || e.PropertyDescriptor == null)
      return;
    switch (e.PropertyDescriptor.Name)
    {
      case "List":
        if (!(this._pdc["RelationsTypeGuid"] is FormDesignerControlsPropertyDescriptor propertyDescriptor))
          break;
        if ((ListContext) e.PropertyDescriptor.GetValue((object) this._ctrl) == ListContext.Objects)
        {
          propertyDescriptor.ResetValue((object) this._ctrl);
          propertyDescriptor.SetReadOnly(true);
          break;
        }
        propertyDescriptor.SetReadOnly(false);
        break;
      case "ObjectTypesGuid":
        if (this._pdc["ColumnCollection"].GetValue((object) this._ctrl) == null || MessageBox.Show(LocalizationHolder.rm.GetString("FormDesigner_MsgDlg_ViewSettings_Message"), LocalizationHolder.rm.GetString("FormDesigner_MsgDlg_ViewSettings_Caption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
          break;
        (this._pdc["ColumnCollection"] as FormDesignerControlsPropertyDescriptor).ResetValue((object) this._ctrl);
        break;
      case "ColumnCollection":
        (this._pdc["DefaultSortingColumns"] as FormDesignerControlsPropertyDescriptor).ResetValue((object) this._ctrl);
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnColumnCollection_AfterResetValue(object sender, EventArgs e)
  {
    (this._pdc["ColumnsAliases"] as FormDesignerControlsPropertyDescriptor).ResetValue((object) this._ctrl);
    (this._pdc["DefaultSortingColumns"] as FormDesignerControlsPropertyDescriptor).ResetValue((object) this._ctrl);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  internal static ObjectsList GetObjectsList(ITypeDescriptorContext context)
  {
    ObjectsList objectsList = (ObjectsList) null;
    if (context != null && context.Instance != null && context.Instance is ObjectsListDescriptor instance)
      objectsList = instance.BaseClass as ObjectsList;
    return objectsList;
  }
}
