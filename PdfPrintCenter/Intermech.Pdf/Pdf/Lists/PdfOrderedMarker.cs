// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfOrderedMarker
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Lists;

public class PdfOrderedMarker : PdfMarker
{
  private int m_currentIndex;
  private string m_delimiter;
  private int m_startNumber;
  private PdfNumberStyle m_style;
  private string m_suffix;

  public PdfOrderedMarker(PdfNumberStyle style, PdfFont font)
    : this(style, string.Empty, string.Empty, font)
  {
  }

  public PdfOrderedMarker(PdfNumberStyle style, string suffix, PdfFont font)
    : this(style, string.Empty, suffix, font)
  {
  }

  public PdfOrderedMarker(PdfNumberStyle style, string delimiter, string suffix, PdfFont font)
  {
    this.m_startNumber = 1;
    this.m_style = style;
    this.m_delimiter = delimiter;
    this.m_suffix = suffix;
    this.Font = font;
  }

  internal void Draw(PdfGraphics graphics, PointF point)
  {
    graphics.DrawString(this.GetNumber() + this.Suffix, this.Font, this.Brush, point);
  }

  internal void Draw(PdfPage page, PointF point) => this.Draw(page.Graphics, point);

  internal string GetNumber()
  {
    return PdfNumbersConvertor.Convert(this.m_startNumber + this.m_currentIndex, this.m_style);
  }

  internal int CurrentIndex
  {
    get => this.m_currentIndex;
    set => this.m_currentIndex = value;
  }

  public string Delimiter
  {
    get => !(this.m_delimiter == string.Empty) && this.m_delimiter != null ? this.m_delimiter : ".";
    set => this.m_delimiter = value;
  }

  public int StartNumber
  {
    get => this.m_startNumber;
    set
    {
      this.m_startNumber = value > 0 ? value : throw new ArgumentException("Start number should be greater than 0");
    }
  }

  public PdfNumberStyle Style
  {
    get => this.m_style;
    set => this.m_style = value;
  }

  public string Suffix
  {
    get => this.m_suffix != null && !(this.m_suffix == string.Empty) ? this.m_suffix : ".";
    set => this.m_suffix = value;
  }
}
