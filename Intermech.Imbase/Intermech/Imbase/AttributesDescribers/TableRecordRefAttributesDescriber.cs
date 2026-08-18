// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.TableRecordRefAttributesDescriber
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

internal class TableRecordRefAttributesDescriber : IAttributePropertyDescriber
{
  private TableRecordRefEditor _editor = new TableRecordRefEditor();

  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return propertyValue == null || !(propertyValue is TableRecordRefPropertyClass) ? (object) null : (object) (propertyValue as TableRecordRefPropertyClass).TableRecordRef;
  }

  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    return actualValue == null || string.IsNullOrEmpty(actualValue.ToString()) ? (object) null : (object) new TableRecordRefPropertyClass(actualValue.ToString());
  }

  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public object GetPropDescriptorEditor(int attributeId) => (object) this._editor;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => true;

  public Type GetPropDescriptorType(int attributeId, FieldTypes baseType)
  {
    return typeof (TableRecordRefPropertyClass);
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
