// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.TemplateFieldContentsSerializer
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
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.TemplateFieldContents)]
internal class TemplateFieldContentsSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (!(configElement is TemplateFieldContents templateFieldContents))
      return;
    configNode.Add((object) new XAttribute((XName) "template_text", (object) templateFieldContents.Template));
    foreach (AttributeSettings templateAttribute in templateFieldContents.TemplateAttributes)
    {
      XElement content = new XElement((XName) "template_entity");
      content.Add((object) new XAttribute((XName) "template_kind", (object) templateAttribute.ItemKind.ToString()));
      if (templateAttribute.ItemGuid != Guid.Empty)
        content.Add((object) new XAttribute((XName) "template_object", (object) templateAttribute.ItemGuid));
      if (templateAttribute.AttributeGuid != Guid.Empty)
        content.Add((object) new XAttribute((XName) "template_attribute", (object) templateAttribute.AttributeGuid));
      configNode.Add((object) content);
    }
  }
}
