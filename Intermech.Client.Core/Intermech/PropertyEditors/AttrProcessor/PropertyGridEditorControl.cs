
// Type: Intermech.PropertyEditors.AttrProcessor.PropertyGridEditorControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

public class PropertyGridEditorControl : PropertyGrid, IAttributeEditorControl, ICustomTypeDescriptor
{
  private int attributeId;
  private int? index;
  private Intermech.PropertyEditors.AttrProcessor.AttributeProcessor attributeProcessor;
  private bool wasChanged;
  private bool inContainer;
  private TypeConverter multipleConverter;
  private bool blockOnChange;
  private PropertyDescriptorCollection propertyDescriptorCollection;
  private MenuItem cmdAddValue;
  private MenuItem cmdDelValue;
  private ContextMenu contextMenu;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public PropertyGridEditorControl()
  {
    this.InitializeComponent();
    this.cmdAddValue = new MenuItem(LocalizationHolder.rm.GetString("Client.Core_94"), new EventHandler(this.AddValueClick));
    this.cmdDelValue = new MenuItem(LocalizationHolder.rm.GetString("Client.Core_96"), new EventHandler(this.DelValueClick));
    this.contextMenu = new ContextMenu(new MenuItem[2]
    {
      this.cmdAddValue,
      this.cmdDelValue
    });
    this.ContextMenu = this.contextMenu;
  }

  private void InitConverter()
  {
    if (this.multipleConverter != null)
      return;
    this.multipleConverter = this.attributeProcessor.GetMultipleValuesConverter(this.attributeId);
  }

  private void AddValueClick(object sender, EventArgs args) => this.AddValue();

  private void DelValueClick(object sender, EventArgs args) => this.DelValue();

  private void AddValue()
  {
    this.attributeProcessor.AddValue(this.attributeId, (object) null);
    this.wasChanged = true;
    this.SelectedObject = (object) this;
    if (this.OnAttributeValueChanged == null)
      return;
    this.OnAttributeValueChanged((object) this, new AttributeValuesChangedEventArgs(this.attributeId, AttributeValuesAction.ModifyValue, (object) new object[2]
    {
      (object) -1,
      null
    }));
  }

  private void DelValue()
  {
    if (this.SelectedGridItem == null || !(this.SelectedGridItem.PropertyDescriptor is SinglePropertyDescriptor))
      return;
    SinglePropertyDescriptor propertyDescriptor = (SinglePropertyDescriptor) this.SelectedGridItem.PropertyDescriptor;
    if (this.propertyDescriptorCollection.Count == 1)
      return;
    int index = propertyDescriptor.Index;
    object obj = this.attributeProcessor.GetValue(this.attributeId, index);
    this.attributeProcessor.RemoveValue(this.attributeId, index);
    this.wasChanged = true;
    this.SelectedObject = (object) this;
    if (this.OnAttributeValueChanged == null)
      return;
    this.OnAttributeValueChanged((object) this, new AttributeValuesChangedEventArgs(this.attributeId, AttributeValuesAction.RemoveValue, (object) new object[2]
    {
      (object) index,
      obj
    }));
  }

  public int AttributeId => this.attributeId;

  public object AttributeProcessor => (object) this.attributeProcessor;

  public int? Index => this.index;

  public void InitControl(int attributeId, object attributeProcessor, int? index)
  {
    this.attributeId = attributeId;
    this.index = index;
    this.attributeProcessor = (Intermech.PropertyEditors.AttrProcessor.AttributeProcessor) attributeProcessor;
    this.RefreshControl();
  }

  public bool InContainer
  {
    get => this.inContainer;
    set => this.inContainer = value;
  }

  public void RefreshControl() => this.SelectedObject = (object) this;

  public bool Apply()
  {
    if (!this.wasChanged)
      return true;
    this.wasChanged = false;
    return true;
  }

  public event AttributeValuesChangedHandler OnAttributeValueChanged;

  public event CloseDemandHandler OnCloseDemand;

  public bool WasChanged => this.wasChanged;

  public void Cancel()
  {
    this.wasChanged = false;
    this.RefreshControl();
  }

  public bool IsDropDownResizable => false;

  public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public bool GetPaintValueSupported(ITypeDescriptorContext context) => false;

  public void PaintValue(PaintValueEventArgs e)
  {
  }

  protected override void OnPropertyValueChanged(PropertyValueChangedEventArgs e)
  {
    try
    {
      if (this.blockOnChange || !(e.ChangedItem.PropertyDescriptor is SinglePropertyDescriptor))
        return;
      SinglePropertyDescriptor propertyDescriptor = (SinglePropertyDescriptor) e.ChangedItem.PropertyDescriptor;
      object x = this.attributeProcessor.GetValue(this.attributeId, propertyDescriptor.Index);
      object obj = propertyDescriptor.GetValue((object) this);
      this.wasChanged = true;
      object y = obj;
      if (!AttributeValues.ValueEquals(x, y))
        this.attributeProcessor.SetValue(this.attributeId, obj);
      if (this.OnAttributeValueChanged == null)
        return;
      this.OnAttributeValueChanged((object) this, new AttributeValuesChangedEventArgs(this.attributeId, AttributeValuesAction.ModifyValue, (object) new object[3]
      {
        (object) -1,
        (object) AttributeValuesAction.ModifyValue,
        null
      }));
    }
    finally
    {
      base.OnPropertyValueChanged(e);
    }
  }

  public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes((object) this, true);

  public string GetClassName() => TypeDescriptor.GetClassName((object) this, true);

  public string GetComponentName() => TypeDescriptor.GetComponentName((object) this, true);

  public TypeConverter GetConverter()
  {
    this.InitConverter();
    return this.multipleConverter;
  }

  public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent((object) this, true);

  public PropertyDescriptor GetDefaultProperty()
  {
    return TypeDescriptor.GetDefaultProperty((object) this, true);
  }

  public object GetEditor(System.Type editorBaseType) => (object) null;

  public EventDescriptorCollection GetEvents(Attribute[] attributes)
  {
    return TypeDescriptor.GetEvents((object) this, attributes, true);
  }

  public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents((object) this, true);

  public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
  {
    this.InitConverter();
    this.propertyDescriptorCollection = this.multipleConverter.GetProperties((ITypeDescriptorContext) null, (object) this, attributes);
    return this.propertyDescriptorCollection;
  }

  public PropertyDescriptorCollection GetProperties() => this.GetProperties((Attribute[]) null);

  public object GetPropertyOwner(PropertyDescriptor pd) => (object) this;

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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
