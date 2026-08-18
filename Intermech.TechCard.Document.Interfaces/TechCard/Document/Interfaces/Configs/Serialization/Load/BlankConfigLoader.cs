// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.BlankConfigLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Serialization.Services;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.Document)]
internal class BlankConfigLoader : DocumentConfigElementLoader
{
  private void LoadVariants(BlankConfig targetConfig, XElement sourceNode)
  {
    foreach (XElement element in sourceNode.Elements())
    {
      string str = Convert.ToString(element.Value);
      targetConfig.ChildList.Add(str);
    }
  }

  private void LoadDocNodesConfigs(BlankConfig targetConfig, XElement sourceNode)
  {
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    if (service == null)
      return;
    foreach (XElement element in sourceNode.Elements())
    {
      IDocumentConfigElement documentConfigElement = service.Load(element);
      if (documentConfigElement != null)
        targetConfig.Elements.Add(documentConfigElement);
    }
  }

  private void LoadTPStructureObjectsConfig(BlankConfig targetConfig, XElement childNode)
  {
    TechCardDocumentConfigLoadService service = ApplicationServices.Container.GetService<TechCardDocumentConfigLoadService>();
    if (service == null)
      return;
    targetConfig.ObjectsConfigs = service.Load(childNode) as TPStructureObjectsConfigs;
  }

  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.Document || !(configElement is BlankConfig targetConfig))
      return;
    foreach (XElement element in configNode.Elements())
    {
      string localName = element.Name.LocalName;
      string str = Convert.ToString(element.Value);
      switch (localName)
      {
        case "characters_in_document_number":
          targetConfig.CharactersInDocumentNumber = int.Parse(str);
          continue;
        case "doc_nodes_configs":
          this.LoadDocNodesConfigs(targetConfig, element);
          continue;
        case "document_type":
          targetConfig.DocumentType = str.ToEnum<DocumentOwnership>();
          continue;
        case "first_number_page_in_document":
          targetConfig.FirstNumberPageInDocument = int.Parse(str);
          continue;
        case "flags":
          targetConfig.Flags = str.ToEnum<BlankFlags>();
          continue;
        case "material_setup":
          targetConfig.MaterialSetup = str.ToEnum<MaterialSetupType>();
          continue;
        case "new_shop_setup":
          targetConfig.NewShopSetup = str.ToEnum<NewShopSetupType>();
          continue;
        case "numbering_interval":
          targetConfig.NumberingInterval = int.Parse(str);
          continue;
        case "step_setup":
          targetConfig.StepSetup = str.ToEnum<StepSetupType>();
          continue;
        case "tool_setup":
          targetConfig.ToolSetup = str.ToEnum<ToolSetupType>();
          continue;
        case "variants":
          this.LoadVariants(targetConfig, element);
          continue;
        default:
          if (element.GetConfigType() == DocumentConfigElementType.TPStructureObjectsConfigs)
          {
            this.LoadTPStructureObjectsConfig(targetConfig, element);
            continue;
          }
          continue;
      }
    }
  }
}
