// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.VariantConfigSerializer
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

[DocumentConfigElementSerializer(DocumentConfigElementType.Variant)]
internal class VariantConfigSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    VariantConfig variantConfig = configElement as VariantConfig;
    if (variantConfig == null)
      return;
    configNode.Add((object) new XElement((XName) "number", (object) variantConfig.Number.ToString()), (object) new XElement((XName) "on_detail", (object) variantConfig.OnDetail.ToString()));
    if (variantConfig.ObjType != null)
      configNode.Add((object) new XElement((XName) "object_type", (object) variantConfig.ObjType.Guid.ToString()));
    if (variantConfig.ChildsList.Count > 0)
    {
      XElement content = new XElement((XName) "variants", (object) Enumerable.Range(0, variantConfig.ChildsList.Count).Select<int, XElement>((Func<int, XElement>) (idx => new XElement((XName) "variant", (object) variantConfig.ChildsList[idx]))));
      configNode.Add((object) content);
    }
    if (!(variantConfig.Condition is IDocumentConfigElement condition))
      return;
    TechCardDocumentConfigSerializeService service = ApplicationServices.Container.GetService<TechCardDocumentConfigSerializeService>();
    if (service == null)
      return;
    XElement content1 = service.Serialize(condition);
    if (content1 == null)
      return;
    XElement content2 = new XElement((XName) "condition");
    content2.Add((object) content1);
    configNode.Add((object) content2);
  }
}
