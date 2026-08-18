// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.BlankCodeAttrDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class BlankCodeAttrDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attrID) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attrID, bool baseReadonly) => baseReadonly;

  public object GetPropDescriptorEditor(int attrID) => (object) new BlankCodeAttrEditor();

  public object GetAttributeValue(IElementInfo iElementInfo, int attrID, object propertyValue)
  {
    object attributeValue = (object) null;
    if (propertyValue != null && propertyValue is BlankCodeAttrProxy)
      attributeValue = (object) (propertyValue as BlankCodeAttrProxy).ID;
    return attributeValue;
  }

  public Type GetPropDescriptorType(int attrID, FieldTypes baseType) => typeof (BlankCodeAttrProxy);

  public object GetPropDescriptorValue(IElementInfo iElementInfo, int attrID, object actualValue)
  {
    object propDescriptorValue;
    if (actualValue != DBNull.Value && actualValue != null)
    {
      long result = 0;
      propDescriptorValue = long.TryParse(actualValue.ToString(), out result) ? (object) new BlankCodeAttrProxy(result) : (object) new BlankCodeAttrProxy(0L);
    }
    else
      propDescriptorValue = (object) new BlankCodeAttrProxy(0L);
    return propDescriptorValue;
  }

  public bool GetPropDescriptorReset(int attrID, bool baseReset) => true;

  public string GetPropDescriptorMask(int attrID, string baseMask) => baseMask;

  public TypeConverter GetConverter(int attrID, object attrProcessor) => (TypeConverter) null;
}
