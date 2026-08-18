// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.CreatedObjectAttrDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class CreatedObjectAttrDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId)
  {
    return (TypeConverter) new CreatedObjectAttrConverter();
  }

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    object attributeValue = (object) null;
    if (propertyValue is ObjectTypeAttProxy objectTypeAttProxy && objectTypeAttProxy.Guid != Guid.Empty)
      attributeValue = (object) objectTypeAttProxy.Guid;
    return attributeValue;
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
    string g = Convert.ToString(actualValue);
    return (object) new ObjectTypeAttProxy(string.IsNullOrEmpty(g) ? Guid.Empty : new Guid(g));
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
