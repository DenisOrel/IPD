
// Type: Intermech.PropertyEditors.AttrProcessor.ObjectLinkConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.PropertyEditors.AttrProcessor;

internal class ObjectLinkConverter : CommonTypeConverter
{
  /// <summary>
  /// флаг обработки версии объектов по VersionID или объектов по ID
  /// </summary>
  protected bool objectVersionProcessed = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <param name="_objectVersionProcessed">флаг обработки версии объектов по VersionID (true) или объектов по ID (false)</param>
  public ObjectLinkConverter(
    int attributeId,
    AttributeProcessor attributeProcessor,
    bool _objectVersionProcessed = true)
    : base(attributeId, attributeProcessor)
  {
    this.objectVersionProcessed = _objectVersionProcessed;
  }

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
    string empty = string.Empty;
    if (value != null)
    {
      this.InitMultiValuesModes();
      if (MultiValueModesHelper.IsValuedFromList(this.multiValueModes.Value))
        empty = (string) base.ConvertTo(context, culture, value, destinationType);
      if (empty == string.Empty)
        empty = new ObjectPropertyClass((long) value, this.objectVersionProcessed).ToString();
    }
    return (object) empty;
  }
}
