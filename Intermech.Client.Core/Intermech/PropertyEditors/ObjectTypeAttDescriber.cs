
// Type: Intermech.PropertyEditors.ObjectTypeAttDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// Класс для регистрации редактора "Тип создаваемого объекта" в Imbase
/// </summary>
public class ObjectTypeAttDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null ? (object) null : (object) ((ObjectTypeAttProxy) propertyValue).Guid;
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) new ObjectTypeAttEditor();

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ObjectTypeAttProxy);
  }

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    return (object) new ObjectTypeAttProxy(actualValue == DBNull.Value || actualValue == null || Convert.ToString(actualValue) == string.Empty ? Guid.Empty : new Guid(Convert.ToString(actualValue)));
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
