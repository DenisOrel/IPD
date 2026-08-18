// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.TemplateAttribute
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Expressions;
using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public class TemplateAttribute : Variable
{
  private static Type ConvertType(FieldTypes fieldType)
  {
    switch (fieldType)
    {
      case FieldTypes.ftString:
        return typeof (string);
      case FieldTypes.ftInteger:
        return typeof (long);
      case FieldTypes.ftDouble:
        return typeof (double);
      case FieldTypes.ftDateTime:
        return typeof (DateTime);
      case FieldTypes.ftShortBlob:
        return typeof (string);
      case FieldTypes.ftFile:
        return typeof (string);
      case FieldTypes.ftObjectLink:
        return typeof (string);
      case FieldTypes.ftMemo:
        return typeof (string);
      case FieldTypes.ftBlob:
        return typeof (string);
      case FieldTypes.ftBoolean:
        return typeof (bool);
      case FieldTypes.ftMeasured:
        return typeof (string);
      case FieldTypes.ftAutoInc:
        return typeof (long);
      default:
        return (Type) null;
    }
  }

  public TemplateAttribute(string name, Type type)
    : base(name, type, FieldTypes.ftUnknown)
  {
  }

  public TemplateAttribute(string name, Type type, FieldTypes fieldType)
    : base(name, type, fieldType)
  {
  }

  public TemplateAttribute([NotNull] AttributeSettings attribute)
  {
    string text = attribute.GetText();
    IMSAttributeType attribute1 = attribute.GetAttribute();
    Type type = TemplateAttribute.ConvertType(attribute1 != null ? attribute1.FieldType : FieldTypes.ftUnknown);
    IMSAttributeType attribute2 = attribute.GetAttribute();
    int fieldType = attribute2 != null ? (int) attribute2.FieldType : 0;
    // ISSUE: explicit constructor call
    base.\u002Ector(text, type, (FieldTypes) fieldType);
    this.Attribute = attribute;
  }

  public AttributeSettings Attribute { get; }
}
