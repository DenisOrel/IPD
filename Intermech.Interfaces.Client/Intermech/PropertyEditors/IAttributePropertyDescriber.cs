// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.IAttributePropertyDescriber
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.PropertyEditors;

/// <summary>
/// Интерфейс для обработки атрибутов, цепляемых к ObjectPropertyGrid
/// </summary>
public interface IAttributePropertyDescriber
{
  Type GetPropDescriptorType(int attributeId, FieldTypes baseType);

  object GetPropDescriptorEditor(int attributeId);

  TypeConverter GetPropDescriptorConverter(int attributeId);

  bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly);

  bool GetPropDescriptorReset(int attributeId, bool baseReset);

  string GetPropDescriptorMask(int attributeId, string baseMask);

  object GetPropDescriptorValue(IElementInfo elementInfo, int attributeId, object actualValue);

  object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue);

  TypeConverter GetConverter(int attributeId, object attributeProcessor);
}
