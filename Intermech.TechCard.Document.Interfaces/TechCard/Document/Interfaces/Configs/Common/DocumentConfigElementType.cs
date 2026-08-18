// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.DocumentConfigElementType
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Localization;
using System.ComponentModel;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

[CustomDescription("TechCard.Document.Attributes_079")]
[CustomCategory("TechCard.Document.Attributes_052")]
public enum DocumentConfigElementType
{
  [CustomDescription("TechCard.Document.Attributes_080"), XmlElement(ElementName = "unknown")] Unknown,
  [CustomDescription("TechCard.Document.Attributes_081"), XmlElement(ElementName = "text_field")] TextField,
  [CustomDescription("TechCard.Document.Attributes_082"), Description("picture_field"), XmlElement(ElementName = "picture_field")] PictureField,
  [CustomDescription("TechCard.Document.Attributes_083"), XmlElement(ElementName = "variant")] Variant,
  [CustomDescription("TechCard.Document.Attributes_084"), XmlElement(ElementName = "tp_structure_objects_configs")] TPStructureObjectsConfigs,
  [CustomDescription("TechCard.Document.Attributes_085"), XmlElement(ElementName = "attribute_field_contents")] AttributeFieldContents,
  [CustomDescription("TechCard.Document.Attributes_086"), XmlElement(ElementName = "formula_field_contents")] FormulaFieldContents,
  [CustomDescription("TechCard.Document.Attributes_087"), XmlElement(ElementName = "template_field_contents")] TemplateFieldContents,
  [CustomDescription("TechCard.Document.Attributes_088"), XmlElement(ElementName = "document")] Document,
}
