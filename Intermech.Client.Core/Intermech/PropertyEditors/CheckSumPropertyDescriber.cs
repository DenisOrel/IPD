
// Type: Intermech.PropertyEditors.CheckSumPropertyDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Checksums;
using Intermech.PropertyEditors.AttrProcessor;
using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Класс для регистрации редактора для атрибута Контрольная сумма
/// </summary>
public class CheckSumPropertyDescriber : IAttributePropertyDescriber
{
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ObjectPropertyClass);
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) null;

  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    if (actualValue == null || !actualValue.GetType().Equals(typeof (long)))
      return (object) null;
    string aCaption = new ChecksumClass(ChecksumAlgorithm.Crc32, actualValue).ToString();
    return (object) new ObjectPropertyClass(Convert.ToInt64(actualValue), aCaption);
  }

  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    switch (propertyValue)
    {
      case ObjectPropertyClass objectPropertyClass:
        return (object) objectPropertyClass.ObjectID;
      case long attributeValue:
        return (object) attributeValue;
      default:
        return (object) null;
    }
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) new CheckSumConverter(attributeId, (AttributeProcessor) attributeProcessor);
  }
}
