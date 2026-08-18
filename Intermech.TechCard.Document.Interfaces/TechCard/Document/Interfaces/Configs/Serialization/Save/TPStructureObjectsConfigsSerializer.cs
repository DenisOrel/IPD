// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.TPStructureObjectsConfigsSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.TPStructureObjectsConfigs)]
internal class TPStructureObjectsConfigsSerializer : DocumentConfigElementSerializer
{
  private void SerializeTPObjectConfig(
    TPStructureObjectConfig configElement,
    XElement parentConfigNode)
  {
    XElement xelement = new XElement((XName) "tp_structure_object_config");
    parentConfigNode.Add((object) xelement);
    if (configElement.ObjectType != null)
      xelement.Add((object) new XAttribute((XName) "obj_type_guid", (object) configElement.ObjectType.Guid));
    this.SerializeObjectsOrdersConfigs(configElement.ChildsOrdersConfigs, xelement);
  }

  private void SerializeObjectsOrdersConfigs(ObjectsOrdersConfigs config, XElement parentConfigNode)
  {
    XElement xelement = new XElement((XName) "object_orders_configs");
    parentConfigNode.Add((object) xelement);
    foreach (ObjectOrderConfig config1 in config.Configs)
      this.SerializeObjectOrderConfig(config1, xelement);
  }

  private void SerializeObjectOrderConfig(ObjectOrderConfig config, XElement parentConfigNode)
  {
    XElement content = new XElement((XName) "object_orders_config");
    parentConfigNode.Add((object) content);
    if (config.ObjectType != null)
      content.Add((object) new XAttribute((XName) "obj_type_guid", (object) config.ObjectType.Guid));
    if (config.RelationType != null)
      content.Add((object) new XAttribute((XName) "rel_type_guid", (object) config.RelationType.Guid));
    content.Add((object) new XAttribute((XName) "order", (object) config.Order));
  }

  protected override void SerializeConfig([NotNull] IDocumentConfigElement config, [NotNull] XElement configNode)
  {
    if (!(config is TPStructureObjectsConfigs structureObjectsConfigs))
      return;
    foreach (TPStructureObjectConfig config1 in structureObjectsConfigs.Configs)
      this.SerializeTPObjectConfig(config1, configNode);
  }
}
