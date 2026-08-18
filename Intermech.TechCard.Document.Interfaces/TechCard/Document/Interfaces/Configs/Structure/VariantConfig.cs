// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.VariantConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[DocumentConfigElementType(DocumentConfigElementType.Variant)]
public class VariantConfig : DocumentConfigElement
{
  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new VariantConfig();
  }

  public override DocumentConfigElementType ElementType => DocumentConfigElementType.Variant;

  public override void Assign(object source)
  {
    base.Assign(source);
    switch (source)
    {
      case VariantConfig variantConfig:
        this.Number = variantConfig.Number;
        this.ObjType = variantConfig.ObjType;
        this.OnDetail = variantConfig.OnDetail;
        this.Condition = (variantConfig.Condition is ICloneable condition1 ? condition1.Clone() : (object) null) as IFieldContents;
        this.Interior = variantConfig.Interior;
        this.ChildsList.AddRange((IEnumerable<string>) variantConfig.ChildsList);
        break;
      case TableProperties tableProperties:
        this.Number = tableProperties.Number;
        this.ObjType = tableProperties.ObjType;
        this.OnDetail = tableProperties.OnDetail;
        this.Condition = (tableProperties.Condition is ICloneable condition2 ? condition2.Clone() : (object) null) as IFieldContents;
        this.Interior = tableProperties.Interior;
        this.ChildsList.AddRange((IEnumerable<string>) tableProperties.ChildsList);
        break;
    }
  }

  public override void Clear()
  {
    base.Clear();
    this.Number = 0;
    this.ObjType = (IMSObjectType) null;
    this.OnDetail = false;
    this.Condition = (IFieldContents) null;
    this.Interior = false;
    this.ChildsList.Clear();
  }

  public int Number { get; set; }

  public IMSObjectType ObjType { get; set; }

  public bool NotRepeated { get; set; }

  public bool OnDetail { get; set; }

  public IFieldContents Condition { get; set; }

  public bool Interior { get; set; }

  public List<string> ChildsList { get; } = new List<string>();
}
