// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridColumn
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridColumn
{
  private PdfStringFormat m_format;
  private PdfGrid m_grid;
  private float m_width = float.MinValue;

  public PdfGridColumn(PdfGrid grid) => this.m_grid = grid;

  private PdfStringFormat GetDefaultFormat()
  {
    return new PdfStringFormat()
    {
      LineAlignment = PdfVerticalAlignment.Middle,
      Alignment = PdfTextAlignment.Left
    };
  }

  public PdfStringFormat Format
  {
    get
    {
      if (this.m_format == null)
        this.m_format = new PdfStringFormat();
      return this.m_format;
    }
    set => this.m_format = value;
  }

  public PdfGrid Grid => this.m_grid;

  public float Width
  {
    get => this.m_width;
    set => this.m_width = value;
  }
}
