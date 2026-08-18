// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.ComparisonAttributesDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Imbase;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal sealed class ComparisonAttributesDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attrID) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attrID, bool baseReadonly) => baseReadonly;

  public object GetPropDescriptorEditor(int attrID) => (object) new AttributesComparisonEditor();

  public object GetAttributeValue(IElementInfo iElementInfo, int attrID, object propertyValue)
  {
    object attributeValue = (object) null;
    if (propertyValue != null && propertyValue is AttributesComparison)
      attributeValue = (object) (propertyValue as AttributesComparison).ToBase();
    return attributeValue;
  }

  public Type GetPropDescriptorType(int attrID, FieldTypes baseType)
  {
    return typeof (AttributesComparison);
  }

  public object GetPropDescriptorValue(IElementInfo iElementInfo, int attrID, object actualValue)
  {
    return actualValue == DBNull.Value || actualValue == null ? (object) new AttributesComparison(Guid.Empty, string.Empty, Guid.Empty) : (object) new AttributesComparison((string) actualValue);
  }

  public bool GetPropDescriptorReset(int attrID, bool baseReset) => true;

  public string GetPropDescriptorMask(int attrID, string baseMask) => baseMask;

  public TypeConverter GetConverter(int attrID, object attrProcessor) => (TypeConverter) null;
}
