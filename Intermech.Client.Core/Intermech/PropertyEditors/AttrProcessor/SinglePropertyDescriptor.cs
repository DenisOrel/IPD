
// Type: Intermech.PropertyEditors.AttrProcessor.SinglePropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// PropertyDescriptor для отдельного свойства, как самостоятельного одиночного, так и в составе CommonPropertyDescriptor
/// </summary>
internal class SinglePropertyDescriptor : CommonPropertyDescriptor
{
  private int index;

  public int Index => this.index;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <param name="displayName"></param>
  /// <param name="attrs"></param>
  /// <param name="propertyType"></param>
  /// <param name="converter"></param>
  /// <param name="readOnly"></param>
  /// <param name="canReset"></param>
  public SinglePropertyDescriptor(
    int attributeId,
    AttributeProcessor attributeProcessor,
    string displayName,
    Attribute[] attrs,
    Type propertyType,
    TypeConverter converter,
    bool? readOnly,
    bool? canReset)
    : this(attributeId, attributeProcessor, displayName, attrs, 0, propertyType, converter, readOnly, canReset)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <param name="displayName"></param>
  /// <param name="attrs"></param>
  /// <param name="index"></param>
  /// <param name="propertyType">тип свойства, может быть null -&gt; автоопределение</param>
  /// <param name="converter">конвертор, может быть null -&gt; автоопределение</param>
  /// <param name="readOnly">автоопределение при null</param>
  /// <param name="canReset">автоопределение при null</param>
  public SinglePropertyDescriptor(
    int attributeId,
    AttributeProcessor attributeProcessor,
    string displayName,
    Attribute[] attrs,
    int index,
    Type propertyType,
    TypeConverter converter,
    bool? readOnly,
    bool? canReset)
    : base(attributeId, attributeProcessor, displayName, attrs, propertyType != (Type) null ? propertyType : attributeProcessor.GetPropertyType(attributeId), typeof (object[]), converter != null ? converter : attributeProcessor.GetSingleValueConverter(attributeId), readOnly.HasValue ? readOnly.Value : attributeProcessor.GetReadOnly(attributeId), canReset.HasValue ? canReset.Value : attributeProcessor.GetCanReset(attributeId))
  {
    this.index = index;
  }

  protected override void InitEditor()
  {
    if (this.editor != null)
      return;
    IAttributeEditorControl editorControl = this.attributeProcessor.GetEditorControl(this.attributeId, new int?(this.index), UITypeEditorEditStyle.DropDown);
    if (editorControl == null)
      return;
    this.editor = (object) new CommonUITypeEditor(editorControl);
  }

  public override object GetValue(object component)
  {
    return this.attributeProcessor.GetValue(this.attributeId, this.index);
  }

  public override void SetValue(object component, object value)
  {
    this.attributeProcessor.SetValue(this.attributeId, this.index, value);
  }
}
