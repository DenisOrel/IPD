// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.AttributeFieldContentsLoader
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
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.AttributeFieldContents)]
internal class AttributeFieldContentsLoader : DocumentConfigElementLoader
{
  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.AttributeFieldContents || !(configElement is AttributeFieldContents attributeFieldContents))
      return;
    AttributableElements itemKind = AttributableElements.None;
    XAttribute xattribute1 = configNode.Attribute(XName.Get("AttributableElementsId")) ?? configNode.Attribute(XName.Get("attributable_elements_id"));
    int result;
    if (xattribute1 != null && int.TryParse(xattribute1.Value, out result))
      itemKind = (AttributableElements) result;
    XAttribute xattribute2 = configNode.Attribute(XName.Get("attributable_elements")) ?? configNode.Attribute(XName.Get("AttributableElements"));
    if (xattribute2 != null)
      itemKind = (AttributableElements) Enum.Parse(typeof (AttributableElements), xattribute2.Value);
    XAttribute xattribute3 = configNode.Attribute(XName.Get("entity_object")) ?? configNode.Attribute(XName.Get("EntityObject"));
    string itemGuid = xattribute3 != null ? xattribute3.Value : string.Empty;
    XAttribute xattribute4 = configNode.Attribute(XName.Get("entity_attribute")) ?? configNode.Attribute(XName.Get("EntityAttribute"));
    string attributeGuid = xattribute4 != null ? xattribute4.Value : string.Empty;
    attributeFieldContents.AttributeSettings = new AttributeSettings(itemKind, itemGuid, attributeGuid);
  }
}
