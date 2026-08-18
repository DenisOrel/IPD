// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.DocumentConfigElementTypeExtension
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

public static class DocumentConfigElementTypeExtension
{
  private static DocumentConfigElementType ParseDocumentConfigElementType(string source)
  {
    foreach (FieldInfo field in typeof (DocumentConfigElementType).GetFields())
    {
      if (field.GetCustomAttributes<XmlElementAttribute>(false).Where<XmlElementAttribute>((Func<XmlElementAttribute, bool>) (attr => attr.ElementName == source)).FirstOrDefault<XmlElementAttribute>() != null)
        return (DocumentConfigElementType) field.GetValue((object) field.Name);
    }
    return DocumentConfigElementType.Unknown;
  }

  public static DocumentConfigElementType GetConfigType(this XElement value)
  {
    switch (value.Name.ToString())
    {
      case "Element":
        XAttribute xattribute = value.Attribute(XName.Get("name"));
        return xattribute != null && Convert.ToString(xattribute.Value) == "blank" ? DocumentConfigElementType.Document : DocumentConfigElementType.Unknown;
      case "tp_structure_objects_configs":
        return DocumentConfigElementType.TPStructureObjectsConfigs;
      case "ConditionType":
      case "FieldContents":
        switch (BaseFieldContents.LoadContentsType(value))
        {
          case FieldContentsType.Attribute:
            return DocumentConfigElementType.AttributeFieldContents;
          case FieldContentsType.Template:
            return DocumentConfigElementType.TemplateFieldContents;
          case FieldContentsType.Formula:
            return DocumentConfigElementType.FormulaFieldContents;
          default:
            return DocumentConfigElementType.Unknown;
        }
      default:
        return DocumentConfigElementTypeExtension.ParseDocumentConfigElementType(value.Name.ToString());
    }
  }

  public static string ToXmlTag(this DocumentConfigElementType value)
  {
    FieldInfo field = value.GetType().GetField(value.ToString());
    if (!(field != (FieldInfo) null))
      return value.ToString();
    XmlElementAttribute elementAttribute = field.GetCustomAttributes<XmlElementAttribute>(false).FirstOrDefault<XmlElementAttribute>();
    return elementAttribute == null ? value.ToString() : elementAttribute.ElementName;
  }

  public static bool IsFieldContentsType(this DocumentConfigElementType value)
  {
    return value == DocumentConfigElementType.AttributeFieldContents || value == DocumentConfigElementType.FormulaFieldContents || value == DocumentConfigElementType.TemplateFieldContents;
  }
}
