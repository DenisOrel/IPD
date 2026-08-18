// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.PropertyEditors.MemberOfAssemblyDescriber
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Client.UI.PropertyEditors;

/// <summary>
/// Класс для регистрации кастом редактора для атрибута "Входимость - сборка"
/// </summary>
internal class MemberOfAssemblyDescriber : IAttributePropertyDescriber
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="baseType"></param>
  /// <returns></returns>
  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ObjectPropertyClass);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <returns></returns>
  public object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new MemberOfAssemblyEditor(attributeId);
  }

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
    return actualValue is long ? (object) new ObjectPropertyClass(Convert.ToInt64(actualValue)) : (object) null;
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
    return propertyValue is ObjectPropertyClass objectPropertyClass ? (object) objectPropertyClass.ObjectID : propertyValue;
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
