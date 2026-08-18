// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.TemplateFieldContents
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

[DocumentConfigElementType(DocumentConfigElementType.TemplateFieldContents)]
public class TemplateFieldContents : BaseFieldContents
{
  private readonly ICollection<AttributeSettings> _templateAttributes = (ICollection<AttributeSettings>) new List<AttributeSettings>();

  public TemplateFieldContents() => this.Template = string.Empty;

  public TemplateFieldContents(string template, IEnumerable<AttributeSettings> attributes)
  {
    this.Template = template;
    foreach (AttributeSettings attribute in attributes)
      this._templateAttributes.Add(new AttributeSettings(attribute));
  }

  public override string ToString() => this.Template;

  protected override FieldContentsType GetContentsType() => FieldContentsType.Template;

  public override void CollectAttributeSettings([NotNull] ICollection<AttributeSettings> attributeSettings)
  {
    attributeSettings.AddRange<AttributeSettings>((IEnumerable<AttributeSettings>) this._templateAttributes);
  }

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new TemplateFieldContents();
  }

  public override DocumentConfigElementType ElementType
  {
    get => DocumentConfigElementType.TemplateFieldContents;
  }

  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is TemplateFieldContents templateFieldContents))
      return;
    this.Template = templateFieldContents.Template;
    foreach (AttributeSettings templateAttribute in templateFieldContents.TemplateAttributes)
      this._templateAttributes.Add(new AttributeSettings(templateAttribute));
  }

  public override void Clear()
  {
    base.Clear();
    this._templateAttributes.Clear();
    this.Template = string.Empty;
  }

  public IEnumerable<AttributeSettings> TemplateAttributes
  {
    get => (IEnumerable<AttributeSettings>) this._templateAttributes;
    set
    {
      this._templateAttributes.Clear();
      this._templateAttributes.AddRange<AttributeSettings>(value);
    }
  }

  public string Template { get; set; }
}
