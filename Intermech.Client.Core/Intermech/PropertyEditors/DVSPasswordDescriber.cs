
// Type: Intermech.PropertyEditors.DVSPasswordDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>для ввода пароля для подкючения к IPS.DVS</summary>
public class DVSPasswordDescriber : IAttributePropertyDescriber
{
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (PasswordPropertyClass);
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) new PasswordEditor();

  public TypeConverter GetPropDescriptorConverter(int attributeId)
  {
    return (TypeConverter) new PasswordTypeConverter();
  }

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  /// <summary>
  ///  вернуть значение для PropertyDescriptor PropertyGrid
  /// ( типа GetType() ) по actualValue -&gt; оно же attributeValue[ index ]
  /// </summary>
  /// <param name="elementInfo"></param>
  /// <param name="attributeId"></param>
  /// <param name="actualValue"></param>
  /// <returns></returns>
  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    return actualValue;
  }

  /// <summary>
  /// вернуть реальное значение (как оно хранится в базе) из aPropDescriptor
  /// </summary>
  /// <param name="elementInfo"></param>
  /// <param name="attributeId"></param>
  /// <param name="propertyValue"></param>
  /// <returns></returns>
  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null || !(propertyValue is PasswordPropertyClass) ? (object) string.Empty : (object) CryptHelper.CryptPassword((propertyValue as PasswordPropertyClass).Password, CryptHelper.DVSCrypt);
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
