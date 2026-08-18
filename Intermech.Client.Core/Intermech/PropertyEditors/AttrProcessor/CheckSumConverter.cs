
// Type: Intermech.PropertyEditors.AttrProcessor.CheckSumConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Checksums;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

public class CheckSumConverter(int attributeId, AttributeProcessor attributeProcessor) : 
  CommonTypeConverter(attributeId, attributeProcessor)
{
  private IDescriptor rootDescriptor;
  private DescriptorCollection descriptors;
  private IImbaseSelector selector;

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(destinationType == typeof (string)))
      return base.ConvertTo(context, culture, value, destinationType);
    return value != null ? (object) new ChecksumClass(ChecksumAlgorithm.Crc32, value).ToString() : (object) string.Empty;
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (ObjectPropertyClass) || base.CanConvertFrom(context, sourceType);
  }
}
