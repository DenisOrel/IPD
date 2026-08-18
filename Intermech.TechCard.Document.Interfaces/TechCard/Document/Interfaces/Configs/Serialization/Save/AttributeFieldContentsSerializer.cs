// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.AttributeFieldContentsSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.AttributeFieldContents)]
internal class AttributeFieldContentsSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (!(configElement is AttributeFieldContents attributeFieldContents) || attributeFieldContents.AttributeSettings == null)
      return;
    configNode.Add((object) new XAttribute((XName) "attributable_elements", (object) attributeFieldContents.AttributeSettings.ItemKind.ToString()), (object) new XAttribute((XName) "AttributableElementsId", (object) (int) attributeFieldContents.AttributeSettings.ItemKind));
    if (attributeFieldContents.AttributeSettings.ItemGuid != Guid.Empty)
      configNode.Add((object) new XAttribute((XName) "entity_object", (object) attributeFieldContents.AttributeSettings.ItemGuid));
    if (!(attributeFieldContents.AttributeSettings.AttributeGuid != Guid.Empty))
      return;
    configNode.Add((object) new XAttribute((XName) "entity_attribute", (object) attributeFieldContents.AttributeSettings.AttributeGuid));
  }
}
