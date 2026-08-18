// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents.SetCondition
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Expert;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;

public static class SetCondition
{
  public static IFieldContents Set(object _object)
  {
    IFieldContents fieldContents = (IFieldContents) null;
    if (_object is TempFormula)
      fieldContents = (IFieldContents) new TemplateFieldContents();
    if (_object is string)
      fieldContents = (IFieldContents) new FormulaFieldContents();
    return fieldContents;
  }
}
