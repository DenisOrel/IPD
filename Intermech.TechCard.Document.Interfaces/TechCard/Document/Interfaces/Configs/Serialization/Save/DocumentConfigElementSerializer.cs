// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.DocumentConfigElementSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

internal abstract class DocumentConfigElementSerializer
{
  protected abstract void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode);

  [CanBeNull]
  public XElement Serialize([NotNull] IDocumentConfigElement config)
  {
    XElement configNode = new XElement((XName) config.ElementType.ToXmlTag());
    if (!string.IsNullOrEmpty(config.Id))
      configNode.Add((object) new XAttribute((XName) "id", (object) config.Id));
    this.SerializeConfig(config, configNode);
    return configNode;
  }
}
