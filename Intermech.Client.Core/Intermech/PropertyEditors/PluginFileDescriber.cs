
// Type: Intermech.PropertyEditors.PluginFileDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;


namespace Intermech.PropertyEditors;

/// <summary>
/// для редактирования атрибута загрузочный файл
/// (используется в объектах типа Загружаемые модули)
/// </summary>
internal class PluginFileDescriber : IAttributePropertyDescriber
{
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType) => typeof (string);

  public object GetPropDescriptorEditor(int attributeId) => (object) new PluginFileAttEditor();

  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    return actualValue == null ? (object) string.Empty : (object) Convert.ToString(actualValue);
  }

  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null ? (object) string.Empty : (object) Convert.ToString(propertyValue);
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
