// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.PictureFieldConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[DocumentConfigElementType(DocumentConfigElementType.PictureField)]
public class PictureFieldConfig : DocumentConfigElement
{
  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new PictureFieldConfig();
  }

  public override DocumentConfigElementType ElementType => DocumentConfigElementType.PictureField;

  public override void Assign(object source)
  {
    base.Assign(source);
    switch (source)
    {
      case PictureFieldConfig pictureFieldConfig:
        this.SketchField = pictureFieldConfig.SketchField;
        this.SketchType = pictureFieldConfig.SketchType;
        break;
      case TableProperties tableProperties:
        this.SketchField = tableProperties.SketchField;
        this.SketchType = tableProperties.SketchType;
        break;
    }
  }

  public override void Clear()
  {
    base.Clear();
    this.SketchField = false;
    this.SketchType = SketchTypes.Unsupported;
  }

  public bool SketchField { get; set; }

  public SketchTypes SketchType { get; set; }
}
