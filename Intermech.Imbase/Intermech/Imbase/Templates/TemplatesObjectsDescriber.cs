// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Templates.TemplatesObjectsDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Templates;

internal class TemplatesObjectsDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo iElementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null ? (object) null : (object) (propertyValue as TemplatesBody).Body;
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) new TemplatesObjectsEditor();

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType) => typeof (TemplatesBody);

  public object GetPropDescriptorValue(
    IElementInfo iElementInfo,
    int attributeId,
    object actualValue)
  {
    return (object) new TemplatesBody(actualValue != null ? actualValue.ToString() : string.Empty, UseTemplate.Obj);
  }

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
