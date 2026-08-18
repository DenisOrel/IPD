// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.FormulaFieldContents
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

[DocumentConfigElementType(DocumentConfigElementType.FormulaFieldContents)]
public class FormulaFieldContents : BaseFieldContents
{
  public TempFormula TemplateFormula { get; set; }

  protected override FieldContentsType GetContentsType() => FieldContentsType.Formula;

  public override string ToString() => this.TemplateFormula?.Text ?? string.Empty;

  public override void CollectAttributeSettings(ICollection<AttributeSettings> attrs)
  {
    if (this.TemplateFormula == null)
      return;
    foreach (AttribPair usedAttr in this.TemplateFormula.usedAttrs)
    {
      Guid attributeTypeGuid = MetaDataHelper.GetAttributeTypeGuid(usedAttr.attribID);
      if (!(attributeTypeGuid == Guid.Empty))
        attrs.Add(new AttributeSettings(AttributableElements.Object, MetaDataHelper.GetObjectTypeGuid(usedAttr.objTypeID), attributeTypeGuid));
    }
  }

  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new FormulaFieldContents();
  }

  public override DocumentConfigElementType ElementType
  {
    get => DocumentConfigElementType.FormulaFieldContents;
  }

  public override void Assign(object source)
  {
    base.Assign(source);
    if (!(source is FormulaFieldContents formulaFieldContents))
      return;
    this.TemplateFormula = formulaFieldContents.TemplateFormula;
  }

  public override void Clear()
  {
    base.Clear();
    this.TemplateFormula = (TempFormula) null;
  }
}
