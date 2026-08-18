// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.FieldContentsFactory
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using System;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

public sealed class FieldContentsFactory
{
  private static readonly Lazy<FieldContentsFactory> instance = new Lazy<FieldContentsFactory>();

  public IFieldContents Create(FieldContentsType contentsType)
  {
    IFieldContents fieldContents = (IFieldContents) null;
    switch (contentsType)
    {
      case FieldContentsType.Attribute:
        fieldContents = (IFieldContents) new AttributeFieldContents();
        goto case FieldContentsType.Custom;
      case FieldContentsType.Template:
        fieldContents = (IFieldContents) new TemplateFieldContents();
        goto case FieldContentsType.Custom;
      case FieldContentsType.Formula:
        fieldContents = (IFieldContents) new FormulaFieldContents();
        goto case FieldContentsType.Custom;
      case FieldContentsType.Custom:
        return fieldContents;
      default:
        throw new Exception("Unknown type : " + EnumTypeHelper.GetCaption((Enum) contentsType));
    }
  }

  public IFieldContents Create(XElement element)
  {
    return this.Create(BaseFieldContents.LoadContentsType(element));
  }

  public static FieldContentsFactory Instance => FieldContentsFactory.instance.Value;
}
