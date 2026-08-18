// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.BlankConfigSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.Document)]
internal class BlankConfigSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    BlankConfig blankConfig = configElement as BlankConfig;
    if (blankConfig == null)
      return;
    configNode.Add((object) new XElement((XName) "document_type", (object) blankConfig.DocumentType.ToString()), (object) new XElement((XName) "flags", (object) blankConfig.Flags.ToString()), (object) new XElement((XName) "characters_in_document_number", (object) blankConfig.CharactersInDocumentNumber.ToString()), (object) new XElement((XName) "first_number_page_in_document", (object) blankConfig.FirstNumberPageInDocument.ToString()), (object) new XElement((XName) "numbering_interval", (object) blankConfig.NumberingInterval.ToString()), (object) new XElement((XName) "new_shop_setup", (object) blankConfig.NewShopSetup.ToString()), (object) new XElement((XName) "step_setup", (object) blankConfig.StepSetup.ToString()), (object) new XElement((XName) "tool_setup", (object) blankConfig.ToolSetup.ToString()), (object) new XElement((XName) "material_setup", (object) blankConfig.MaterialSetup.ToString()));
    if (blankConfig.ChildList.Count > 0)
    {
      XElement content = new XElement((XName) "variants", (object) Enumerable.Range(0, blankConfig.ChildList.Count).Select<int, XElement>((Func<int, XElement>) (idx => new XElement((XName) "variant", (object) blankConfig.ChildList[idx]))));
      configNode.Add((object) content);
    }
    TechCardDocumentConfigSerializeService service = ApplicationServices.Container.GetService<TechCardDocumentConfigSerializeService>();
    if (service == null)
      return;
    XElement content1 = new XElement((XName) "doc_nodes_configs");
    foreach (IDocumentConfigElement element in blankConfig.Elements)
    {
      XElement content2 = service.Serialize(element);
      if (content2 != null)
        content1.Add((object) content2);
    }
    configNode.Add((object) content1);
    XElement content3 = service.Serialize((IDocumentConfigElement) blankConfig.ObjectsConfigs);
    if (content3 == null)
      return;
    configNode.Add((object) content3);
  }
}
