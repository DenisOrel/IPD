// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfLightTableLayoutFormat
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfLightTableLayoutFormat : PdfLayoutFormat
{
  private int m_endColumn;
  private int m_startColumn;

  public PdfLightTableLayoutFormat()
  {
  }

  public PdfLightTableLayoutFormat(PdfLayoutFormat baseFormat)
    : base(baseFormat)
  {
  }

  public int EndColumnIndex
  {
    get => this.m_endColumn;
    set
    {
      this.m_endColumn = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof (EndColumnIndex));
    }
  }

  public int StartColumnIndex
  {
    get => this.m_startColumn;
    set => this.m_startColumn = value;
  }
}
