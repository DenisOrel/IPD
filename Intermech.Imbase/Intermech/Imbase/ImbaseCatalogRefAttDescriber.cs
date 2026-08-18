// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseCatalogRefAttDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

public class ImbaseCatalogRefAttDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public object GetPropDescriptorEditor(int attributeId)
  {
    return (object) new ImbaseCatalogRefAttEditor();
  }

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null ? (object) null : (object) ((ImbaseCatalogRefAttProxy) propertyValue).ID;
  }

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (ImbaseCatalogRefAttProxy);
  }

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    if (actualValue == DBNull.Value || actualValue == null)
      return (object) new ImbaseCatalogRefAttProxy(0L);
    long result;
    return !long.TryParse(actualValue.ToString(), out result) ? (object) new ImbaseCatalogRefAttProxy(0L) : (object) new ImbaseCatalogRefAttProxy(result);
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
