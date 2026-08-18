// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Tables.PdfLightTableStyle
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;

#nullable disable
namespace Syncfusion.Pdf.Tables;

public class PdfLightTableStyle
{
  private PdfCellStyle m_alternateStyle;
  private PdfPen m_borderPen;
  private bool m_bRepeateHeader = true;
  private bool m_bShowHeader;
  private float m_cellPadding;
  private float m_cellSpacing;
  private PdfCellStyle m_defaultStyle = new PdfCellStyle();
  private int m_headerRowCount;
  private PdfHeaderSource m_headerSource;
  private PdfCellStyle m_headerStyle;
  private PdfBorderOverlapStyle m_overlappedBorders;

  public PdfCellStyle AlternateStyle
  {
    get => this.m_alternateStyle;
    set => this.m_alternateStyle = value;
  }

  public PdfBorderOverlapStyle BorderOverlapStyle
  {
    get => this.m_overlappedBorders;
    set => this.m_overlappedBorders = value;
  }

  public PdfPen BorderPen
  {
    get => this.m_borderPen;
    set => this.m_borderPen = value;
  }

  public float CellPadding
  {
    get => this.m_cellPadding;
    set => this.m_cellPadding = value;
  }

  public float CellSpacing
  {
    get => this.m_cellSpacing;
    set => this.m_cellSpacing = value;
  }

  public PdfCellStyle DefaultStyle
  {
    get => this.m_defaultStyle;
    set
    {
      this.m_defaultStyle = value != null ? value : throw new ArgumentNullException(nameof (DefaultStyle));
    }
  }

  public int HeaderRowCount
  {
    get => this.m_headerRowCount;
    set
    {
      this.m_headerRowCount = value >= 0 ? value : throw new ArgumentOutOfRangeException("HeaderRowsCount", "This parameter can't be less then zero");
    }
  }

  public PdfHeaderSource HeaderSource
  {
    get => this.m_headerSource;
    set => this.m_headerSource = value;
  }

  public PdfCellStyle HeaderStyle
  {
    get => this.m_headerStyle;
    set => this.m_headerStyle = value;
  }

  public bool RepeatHeader
  {
    get => this.m_bRepeateHeader;
    set => this.m_bRepeateHeader = value;
  }

  public bool ShowHeader
  {
    get => this.m_bShowHeader;
    set => this.m_bShowHeader = value;
  }
}
