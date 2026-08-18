
// Type: Intermech.Client.Core.FormDesigner.Controls.AttributeInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Класс на замену Guid атрибута.</summary>
[TypeConverter(typeof (AttributeInfo.AttributeInfoConverter))]
[Serializable]
public class AttributeInfo
{
  /// <summary>Guid типа атрибута.</summary>
  public Guid AttributeGuid { get; set; }

  /// <summary>Guid типа объекта/связи.</summary>
  public Guid TypeGuid { get; set; }

  /// <summary>Конструктор.</summary>
  public AttributeInfo()
  {
  }

  /// <summary>Конструктор.</summary>
  /// <param name="attributeGuid">Guid типа атрибута</param>
  /// <param name="typeGuid">Guid типа объекта/связи</param>
  public AttributeInfo(Guid attributeGuid, Guid typeGuid)
  {
    this.AttributeGuid = attributeGuid;
    this.TypeGuid = typeGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    if (!(obj is AttributeInfo attributeInfo))
      return base.Equals(obj);
    return this.AttributeGuid == attributeInfo.AttributeGuid && this.TypeGuid == attributeInfo.TypeGuid;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"{Convert.ToString((object) this.TypeGuid)};{Convert.ToString((object) this.AttributeGuid)}";
  }

  /// <summary>
  /// 
  /// </summary>
  internal class AttributeInfoConverter : TypeConverter
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sourceType"></param>
    /// <returns></returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      string[] strArray = value.ToString().Split(';');
      return strArray.Length == 2 ? (object) new AttributeInfo(new Guid(strArray[1]), new Guid(strArray[0])) : throw new FormatException(LocalizationHolder.rm.GetString("Client.Core_1145"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      return !(value is AttributeInfo attributeInfo) || !(destinationType == typeof (string)) ? base.ConvertTo(context, culture, value, destinationType) : (object) attributeInfo.ToString();
    }
  }
}
