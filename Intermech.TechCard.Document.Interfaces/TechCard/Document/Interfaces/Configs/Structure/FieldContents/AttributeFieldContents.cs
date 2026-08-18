// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.AttributeFieldContents
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

[DocumentConfigElementType(DocumentConfigElementType.AttributeFieldContents)]
public class AttributeFieldContents : BaseFieldContents
{
  private AttributeSettings _attributeSettings;

  protected override FieldContentsType GetContentsType() => FieldContentsType.Attribute;

  public override void CollectAttributeSettings(ICollection<AttributeSettings> attrs)
  {
    if (this._attributeSettings == null)
      return;
    attrs.Add(this._attributeSettings);
  }

  public override string ToString() => this._attributeSettings?.GetText() ?? string.Empty;

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new AttributeFieldContents();
  }

  public override DocumentConfigElementType ElementType
  {
    get => DocumentConfigElementType.AttributeFieldContents;
  }

  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is AttributeFieldContents attributeFieldContents))
      return;
    this.AttributeSettings = attributeFieldContents.AttributeSettings;
  }

  public override void Clear()
  {
    base.Clear();
    this.AttributeSettings = (AttributeSettings) null;
  }

  public AttributeSettings AttributeSettings
  {
    get => this._attributeSettings;
    set => this._attributeSettings = value;
  }
}
