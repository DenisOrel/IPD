// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.TPStructureObjectsConfigsLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.TPStructureObjectsConfigs)]
internal class TPStructureObjectsConfigsLoader : DocumentConfigElementLoader
{
  private void LoadTPObjectConfig(XElement configNode, TPStructureObjectsConfigs parentConfig)
  {
    if (configNode.Name.ToString() != "tp_structure_object_config")
      return;
    XAttribute xattribute = configNode.Attribute((XName) "obj_type_guid");
    Guid result;
    if (xattribute == null || !Guid.TryParse(xattribute.Value, out result))
      return;
    TPStructureObjectConfig parentConfig1 = parentConfig.Add(result);
    if (parentConfig1 == null)
      return;
    XElement ordersConfigsNode = configNode.Element((XName) "object_orders_configs");
    if (ordersConfigsNode == null)
      return;
    this.LoadObjectOrdersConfigs(ordersConfigsNode, parentConfig1);
  }

  private void LoadObjectOrdersConfigs(
    XElement ordersConfigsNode,
    TPStructureObjectConfig parentConfig)
  {
    foreach (XElement element in ordersConfigsNode.Elements())
    {
      if (string.Compare(element.Name.ToString(), "object_orders_config", true) == 0)
      {
        Guid result1 = Guid.Empty;
        Guid result2 = Guid.Empty;
        int result3 = 0;
        XAttribute xattribute1 = element.Attribute((XName) "obj_type_guid");
        if (xattribute1 == null || Guid.TryParse(xattribute1.Value, out result1))
        {
          XAttribute xattribute2 = element.Attribute((XName) "rel_type_guid");
          if (xattribute2 == null || Guid.TryParse(xattribute2.Value, out result2))
          {
            XAttribute xattribute3 = element.Attribute((XName) "order");
            if (xattribute3 == null || int.TryParse(xattribute3.Value.ToString(), out result3))
              parentConfig.ChildsOrdersConfigs.Add(result1, result2, result3);
          }
        }
      }
    }
  }

  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.TPStructureObjectsConfigs || !(configElement is TPStructureObjectsConfigs parentConfig))
      return;
    foreach (XElement element in configNode.Elements())
      this.LoadTPObjectConfig(element, parentConfig);
  }
}
