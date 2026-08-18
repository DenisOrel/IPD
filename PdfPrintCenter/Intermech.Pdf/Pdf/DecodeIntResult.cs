// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.DecodeIntResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class DecodeIntResult
{
  private bool m_booleanResult;
  private int m_intResult;

  internal DecodeIntResult(int intResult, bool booleanResult)
  {
    this.m_intResult = intResult;
    this.m_booleanResult = booleanResult;
  }

  internal bool BooleanResult => this.m_booleanResult;

  internal int IntResult => this.m_intResult;
}
