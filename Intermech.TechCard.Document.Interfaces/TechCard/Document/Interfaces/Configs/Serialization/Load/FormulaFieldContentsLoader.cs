// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load.FormulaFieldContentsLoader
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Expert;
using Intermech.Localization;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Load;

[DocumentConfigElementLoader(DocumentConfigElementType.FormulaFieldContents)]
internal class FormulaFieldContentsLoader : DocumentConfigElementLoader
{
  private bool LoadFormulaFromBase64(string base64Text, out TempFormula formula)
  {
    formula = (TempFormula) null;
    if (string.IsNullOrEmpty(base64Text))
      return false;
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    try
    {
      binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
      using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(base64Text)))
        formula = binaryFormatter.Deserialize((Stream) serializationStream) as TempFormula;
    }
    catch (Exception ex)
    {
      throw new Exception(LocalizationHolder.rm.GetString("TechCard.Document_007"), ex);
    }
    return formula != null;
  }

  protected override void LoadConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    TempFormula formula;
    if (configNode.GetConfigType() != DocumentConfigElementType.FormulaFieldContents || !(configElement is FormulaFieldContents formulaFieldContents) || !this.LoadFormulaFromBase64((configNode.Attribute(XName.Get("template_formula")) ?? configNode.Attribute(XName.Get("TemplateFormula")))?.Value, out formula))
      return;
    formulaFieldContents.TemplateFormula = formula;
  }
}
