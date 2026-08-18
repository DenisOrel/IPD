
// Type: Intermech.PropertyEditors.AttrProcessor.MultipleValuesPropertyDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing.Design;


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// PropertyDescriptor для содержащего другие PropertyDescriptor
/// </summary>
/// <summary>
/// 
/// </summary>
/// <param name="attributeId"></param>
/// <param name="attributeProcessor"></param>
/// <param name="displayName"></param>
/// <param name="attrs"></param>
/// <param name="readOnly">автоопределение при null</param>
/// <param name="canReset">автоопределение при null</param>
internal class MultipleValuesPropertyDescriptor(
  int attributeId,
  AttributeProcessor attributeProcessor,
  string displayName,
  Attribute[] attrs,
  bool? readOnly,
  bool? canReset) : CommonPropertyDescriptor(attributeId, attributeProcessor, displayName, attrs, typeof (object[]), attributeProcessor.GetType(), attributeProcessor.GetMultipleValuesConverter(attributeId), readOnly.HasValue ? readOnly.Value : attributeProcessor.GetReadOnly(attributeId), canReset.HasValue ? canReset.Value : attributeProcessor.GetCanReset(attributeId))
{
  protected override void InitEditor()
  {
    if (this.editor != null)
      return;
    IAttributeEditorControl editorControl = this.attributeProcessor.GetEditorControl(this.attributeId, new int?(), UITypeEditorEditStyle.Modal);
    if (editorControl == null)
      return;
    this.editor = (object) new CommonUITypeEditor(editorControl);
  }

  public override object GetValue(object component)
  {
    return (object) this.attributeProcessor.GetValues(this.attributeId);
  }

  public override void SetValue(object component, object value)
  {
    this.attributeProcessor.SetValues(this.attributeId, (object[]) value);
  }
}
