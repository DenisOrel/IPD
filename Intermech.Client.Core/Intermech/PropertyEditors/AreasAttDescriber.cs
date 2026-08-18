
// Type: Intermech.PropertyEditors.AreasAttDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Класс для регистрации редактора на атрибут "Предметные области" в Навигаторе/Imbase
/// </summary>
public class AreasAttDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetPropDescriptorEditor(int attributeId) => (object) new SubjectAreaEditor();

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => false;

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (SubjectAreaPropertyClass);
  }

  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return (object) ((SubjectAreaPropertyClass) propertyValue).Areas;
  }

  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    return (object) new SubjectAreaPropertyClass(actualValue == DBNull.Value || actualValue == null ? string.Empty : Convert.ToString(actualValue));
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
