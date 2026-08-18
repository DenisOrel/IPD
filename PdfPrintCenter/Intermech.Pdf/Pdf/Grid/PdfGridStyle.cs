// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Grid.PdfGridStyle
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Grid;

public class PdfGridStyle : PdfGridStyleBase
{
  private bool m_bAllowHorizontalOverflow;
  private PdfBorderOverlapStyle m_borderOverlapStyle;
  private PdfPaddings m_cellPadding;
  private float m_cellSpacing;
  private PdfHorizontalOverflowType m_HorizontalOverflowType = PdfHorizontalOverflowType.LastPage;

  public bool AllowHorizontalOverflow
  {
    get => this.m_bAllowHorizontalOverflow;
    set => this.m_bAllowHorizontalOverflow = value;
  }

  public PdfBorderOverlapStyle BorderOverlapStyle
  {
    get => this.m_borderOverlapStyle;
    set => this.m_borderOverlapStyle = value;
  }

  public PdfPaddings CellPadding
  {
    get
    {
      if (this.m_cellPadding == null)
        this.m_cellPadding = new PdfPaddings();
      return this.m_cellPadding;
    }
    set => this.m_cellPadding = value;
  }

  public float CellSpacing
  {
    get => this.m_cellSpacing;
    set => this.m_cellSpacing = value;
  }

  public PdfHorizontalOverflowType HorizontalOverflowType
  {
    get => this.m_HorizontalOverflowType;
    set => this.m_HorizontalOverflowType = value;
  }
}
