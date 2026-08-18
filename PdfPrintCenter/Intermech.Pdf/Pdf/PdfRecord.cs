// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfRecord
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Diagnostics;

#nullable disable
namespace Syncfusion.Pdf;

[DebuggerDisplay("({OperatorName}, operands={Operands.Length})")]
internal class PdfRecord
{
  private string[] m_operands;
  private string m_operatorName;

  public PdfRecord(string name, string[] operands)
  {
    this.m_operatorName = name;
    this.m_operands = operands;
  }

  internal string[] Operands
  {
    get => this.m_operands;
    set => this.m_operands = value;
  }

  internal string OperatorName
  {
    get => this.m_operatorName;
    set => this.m_operatorName = value;
  }
}
