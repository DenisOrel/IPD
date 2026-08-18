
// Type: Intermech.Client.Core.PropertyEditors.AttributablePropertyDescriber
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;


namespace Intermech.Client.Core.PropertyEditors;

public abstract class AttributablePropertyDescriber : IAttributePropertyDescriber
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseType"></param>
  /// <returns></returns>
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (AttributablePropertyClass);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public abstract object GetPropDescriptorEditor(int attributeId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseReadonly"></param>
  /// <returns></returns>
  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseReset"></param>
  /// <returns></returns>
  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseMask"></param>
  /// <returns></returns>
  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  /// <summary>
  /// 
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
    long objectId = 0;
    if (actualValue is long)
      objectId = Convert.ToInt64(actualValue);
    return (object) new AttributablePropertyClass(elementInfo, attributeId, objectId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="elementInfo"></param>
  /// <param name="attributeId"></param>
  /// <param name="propertyValue"></param>
  /// <returns></returns>
  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return !(propertyValue is AttributablePropertyClass attributablePropertyClass) ? propertyValue : (object) attributablePropertyClass.ObjectID;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="attributeProcessor"></param>
  /// <returns></returns>
  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
