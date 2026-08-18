// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.PictureConfigSerializer
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

[DocumentConfigElementSerializer(DocumentConfigElementType.PictureField)]
internal class PictureConfigSerializer : DocumentConfigElementSerializer
{
  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (!(configElement is PictureFieldConfig pictureFieldConfig))
      return;
    configNode.Add((object) new XElement((XName) "sketch_field", (object) pictureFieldConfig.SketchField.ToString()), (object) new XElement((XName) "sketch_type", (object) pictureFieldConfig.SketchType.ToString()));
  }
}
