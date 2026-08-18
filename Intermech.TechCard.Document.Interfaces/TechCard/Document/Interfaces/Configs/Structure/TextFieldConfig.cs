// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.TextFieldConfig
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.Obsolete;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

[DocumentConfigElementType(DocumentConfigElementType.TextField)]
public class TextFieldConfig : DocumentConfigElement
{
  protected override IDocumentConfigElement CreateEmptyClone()
  {
    return (IDocumentConfigElement) new TextFieldConfig();
  }

  public override DocumentConfigElementType ElementType => DocumentConfigElementType.TextField;

  public override void Assign(object source)
  {
    base.Assign(source);
    switch (source)
    {
      case TextFieldConfig textFieldConfig:
        this.Digits = textFieldConfig.Digits;
        this.NotRepeated = textFieldConfig.NotRepeated;
        this.CalcOnFill = textFieldConfig.CalcOnFill;
        this.FieldContents = (textFieldConfig.FieldContents is ICloneable fieldContents1 ? fieldContents1.Clone() : (object) null) as IFieldContents;
        this.Condition = (textFieldConfig.Condition is ICloneable condition1 ? condition1.Clone() : (object) null) as IFieldContents;
        break;
      case TableProperties tableProperties:
        this.Digits = tableProperties.Digits;
        this.NotRepeated = tableProperties.NotRepeated;
        this.CalcOnFill = tableProperties.CalcOnFill;
        this.FieldContents = (tableProperties.FieldContents is ICloneable fieldContents2 ? fieldContents2.Clone() : (object) null) as IFieldContents;
        this.Condition = (tableProperties.Condition is ICloneable condition2 ? condition2.Clone() : (object) null) as IFieldContents;
        break;
    }
  }

  public override void Clear()
  {
    base.Clear();
    this.Digits = 3;
    this.NotRepeated = false;
    this.CalcOnFill = false;
    this.FieldContents = (IFieldContents) null;
    this.Condition = (IFieldContents) null;
  }

  public int Digits { get; set; } = 3;

  public bool NotRepeated { get; set; }

  public bool CalcOnFill { get; set; }

  public IFieldContents FieldContents { get; set; }

  public IFieldContents Condition { get; set; }
}
