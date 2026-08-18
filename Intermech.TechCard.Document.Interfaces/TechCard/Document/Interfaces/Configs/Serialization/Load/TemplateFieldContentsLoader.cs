// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.TemplateFieldContentsLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.TemplateFieldContents)]
internal class TemplateFieldContentsLoader : DocumentConfigElementLoader
{
  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.TemplateFieldContents || !(configElement is TemplateFieldContents templateFieldContents))
      return;
    XAttribute xattribute1 = configNode.Attribute(XName.Get("template_text")) ?? configNode.Attribute(XName.Get("TemplateText"));
    templateFieldContents.Template = xattribute1?.Value ?? string.Empty;
    List<AttributeSettings> attributeSettingsList = new List<AttributeSettings>();
    foreach (XElement element in configNode.Elements())
    {
      if (element.Name.LocalName == "template_entity" || element.Name.LocalName == "TemplateEntity")
      {
        XAttribute xattribute2 = element.Attribute(XName.Get("template_kind")) ?? element.Attribute(XName.Get("TemplateKind"));
        XAttribute xattribute3 = element.Attribute(XName.Get("template_object")) ?? element.Attribute(XName.Get("TemplateObject"));
        XAttribute xattribute4 = element.Attribute(XName.Get("template_attribute")) ?? element.Attribute(XName.Get("TemplateAttribute"));
        AttributableElements itemKind = xattribute2 != null ? (AttributableElements) Enum.Parse(typeof (AttributableElements), xattribute2.Value) : AttributableElements.None;
        string itemGuid = xattribute3?.Value ?? string.Empty;
        string attributeGuid = xattribute4?.Value ?? string.Empty;
        attributeSettingsList.Add(new AttributeSettings(itemKind, itemGuid, attributeGuid));
      }
    }
    templateFieldContents.TemplateAttributes = (IEnumerable<AttributeSettings>) attributeSettingsList;
  }
}
