
// Type: Intermech.PropertyEditors.AttrProcessor.DateTimeAttributeConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;


namespace Intermech.PropertyEditors.AttrProcessor;

public class DateTimeAttributeConverter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
  {
    return destinationType == typeof (string) || destinationType == typeof (DateTime) || new DateTimeConverter().CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    System.Type destinationType)
  {
    return new DateTimeConverter().ConvertTo(context, culture, value, destinationType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    if (value == null || value is DBNull)
      return (object) null;
    return value as string == "" ? (object) null : new DateTimeConverter().ConvertFrom(value);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
  {
    return sourceType == typeof (DBNull) || sourceType == typeof (string) || sourceType == typeof (DateTime) || base.CanConvertFrom(context, sourceType);
  }

  public override IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    if (style == UITypeEditorEditStyle.DropDown)
      return (IAttributeEditorControl) new DataTimeAttributeEditor();
    if (style != UITypeEditorEditStyle.Modal)
      return (IAttributeEditorControl) null;
    DataTimeAttributeEditorForm editorControl = new DataTimeAttributeEditorForm();
    editorControl.Text = LocalizationHolder.rm.GetString("Client.Core_429");
    return (IAttributeEditorControl) editorControl;
  }

  private void editorControl_DoubleClick(object sender, EventArgs e)
  {
    if (!(sender is Control control) || !(control.FindForm() is EditorControlForm form))
      return;
    form.Apply();
    form.DialogResult = DialogResult.OK;
    form.Close();
  }

  public override List<UITypeEditorEditStyle> GetPossibleEditorControlStyle()
  {
    return new List<UITypeEditorEditStyle>(1)
    {
      UITypeEditorEditStyle.DropDown,
      UITypeEditorEditStyle.Modal
    };
  }
}
