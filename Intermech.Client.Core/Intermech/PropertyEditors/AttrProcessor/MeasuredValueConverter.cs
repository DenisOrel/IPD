
// Type: Intermech.PropertyEditors.AttrProcessor.MeasuredValueConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

public class MeasuredValueConverter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || sourceType == typeof (MeasuredValue) || base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    switch (value)
    {
      case null:
      case MeasuredValue _:
        return value;
      default:
        string mValue = value as string;
        switch (mValue)
        {
          case "":
            return (object) null;
          case null:
            return base.ConvertFrom(context, culture, value);
          default:
            return (object) MeasureHelper.ConvertToMeasuredValue(mValue);
        }
    }
  }

  public override IAttributeEditorControl GetEditorControl(UITypeEditorEditStyle style)
  {
    return style == UITypeEditorEditStyle.Modal ? (IAttributeEditorControl) new MeasureForm() : (IAttributeEditorControl) null;
  }

  public override List<UITypeEditorEditStyle> GetPossibleEditorControlStyle()
  {
    return new List<UITypeEditorEditStyle>(1)
    {
      UITypeEditorEditStyle.Modal
    };
  }
}
