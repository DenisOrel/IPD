// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.PictureConfigLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.PictureField)]
internal class PictureConfigLoader : DocumentConfigElementLoader
{
  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    if (configNode.GetConfigType() != DocumentConfigElementType.PictureField || !(configElement is PictureFieldConfig pictureFieldConfig))
      return;
    foreach (XElement element in configNode.Elements())
    {
      string localName = element.Name.LocalName;
      string enumValue = Convert.ToString(element.Value);
      switch (localName)
      {
        case "sketch_field":
          bool result;
          if (!bool.TryParse(enumValue, out result))
            result = false;
          pictureFieldConfig.SketchField = result;
          continue;
        case "sketch_type":
          pictureFieldConfig.SketchType = enumValue.ToEnum<SketchTypes>();
          continue;
        default:
          continue;
      }
    }
  }
}
