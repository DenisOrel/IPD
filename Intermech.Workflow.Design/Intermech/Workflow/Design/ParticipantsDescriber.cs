// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ParticipantsDescriber
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.PropertyEditors;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ParticipantsDescriber.</summary>
public class ParticipantsDescriber : IAttributePropertyDescriber
{
  public TypeConverter GetPropDescriptorConverter(int attributeId) => (TypeConverter) null;

  public bool GetPropDescriptorReadonly(int attributeId, bool baseReadonly) => baseReadonly;

  public string GetPropDescriptorMask(int attributeId, string baseMask) => baseMask;

  public object GetAttributeValue(IElementInfo elementInfo, int attributeId, object propertyValue)
  {
    return (object) ((ParticipantsPropertyClass) propertyValue).Value;
  }

  public object GetPropDescriptorEditor(int attributeId) => (object) new ParticipantsEditor();

  public bool GetPropDescriptorReset(int attributeId, bool baseReset) => false;

  public Type GetPropDescriptorType(int attributeId, FieldTypes attributeType)
  {
    return typeof (ParticipantsPropertyClass);
  }

  public object GetPropDescriptorValue(
    IElementInfo elementInfo,
    int attributeId,
    object actualValue)
  {
    return (object) new ParticipantsPropertyClass(Convert.ToString(actualValue));
  }

  public TypeConverter GetConverter(int attributeId, object attributeProcessor)
  {
    return (TypeConverter) null;
  }
}
