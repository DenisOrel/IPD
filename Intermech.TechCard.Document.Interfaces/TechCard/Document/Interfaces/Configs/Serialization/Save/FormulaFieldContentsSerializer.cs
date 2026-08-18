// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save.FormulaFieldContentsSerializer
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Expert;
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
namespace Intermech.TechCard.Document.Interfaces.Configs.Serialization.Save;

[DocumentConfigElementSerializer(DocumentConfigElementType.FormulaFieldContents)]
internal class FormulaFieldContentsSerializer : DocumentConfigElementSerializer
{
  private bool SaveFormulaToBase64(TempFormula formula, out string base64Text)
  {
    base64Text = string.Empty;
    if (formula == null)
      return false;
    using (MemoryStream serializationStream = new MemoryStream())
    {
      new BinaryFormatter()
      {
        AssemblyFormat = FormatterAssemblyStyle.Simple
      }.Serialize((Stream) serializationStream, (object) formula);
      base64Text = Convert.ToBase64String(serializationStream.ToArray());
    }
    return true;
  }

  protected override void SerializeConfig([NotNull] IDocumentConfigElement configElement, [NotNull] XElement configNode)
  {
    string base64Text;
    if (!(configElement is FormulaFieldContents formulaFieldContents) || !this.SaveFormulaToBase64(formulaFieldContents.TemplateFormula, out base64Text))
      return;
    configNode.Add((object) new XAttribute((XName) "template_formula", (object) base64Text));
  }
}
