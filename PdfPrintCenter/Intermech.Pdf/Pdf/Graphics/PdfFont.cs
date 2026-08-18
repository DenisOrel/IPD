// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfFont : IPdfWrapper, IPdfCache
{
  internal const float CharSizeMultiplier = 0.001f;
  private IPdfPrimitive m_fontInternals;
  private PdfFontMetrics m_fontMetrics;
  private string m_internalFontName;
  private float m_size;
  private PdfFontStyle m_style;
  protected static object s_syncObject = new object();

  protected PdfFont(float size) => this.m_size = size;

  protected PdfFont(float size, PdfFontStyle style)
    : this(size)
  {
    this.SetStyle(style);
  }

  protected float ApplyFormatSettings(string line, PdfStringFormat format, float width)
  {
    if (line == null)
      throw new ArgumentNullException(nameof (line));
    float num = width;
    if (format != null && (double) width > 0.0)
    {
      if ((double) format.CharacterSpacing != 0.0)
        num += (float) (line.Length - 1) * format.CharacterSpacing;
      if ((double) format.WordSpacing != 0.0)
      {
        char[] spaces = StringTokenizer.Spaces;
        int charsCount = StringTokenizer.GetCharsCount(line, spaces);
        num += (float) charsCount * format.WordSpacing;
      }
    }
    return num;
  }

  protected abstract bool EqualsToFont(PdfFont font);

  protected internal abstract float GetCharWidth(char charCode, PdfStringFormat format);

  protected internal abstract float GetLineWidth(string line, PdfStringFormat format);

  public SizeF MeasureString(string text) => this.MeasureString(text, (PdfStringFormat) null);

  public SizeF MeasureString(string text, PdfStringFormat format)
  {
    int charactersFitted = 0;
    int linesFilled = 0;
    return this.MeasureString(text, format, out charactersFitted, out linesFilled);
  }

  public SizeF MeasureString(string text, SizeF layoutArea)
  {
    return this.MeasureString(text, layoutArea, (PdfStringFormat) null);
  }

  public SizeF MeasureString(string text, float width)
  {
    return this.MeasureString(text, width, (PdfStringFormat) null);
  }

  public SizeF MeasureString(string text, SizeF layoutArea, PdfStringFormat format)
  {
    int charactersFitted = 0;
    int linesFilled = 0;
    return this.MeasureString(text, layoutArea, format, out charactersFitted, out linesFilled);
  }

  public SizeF MeasureString(string text, float width, PdfStringFormat format)
  {
    int charactersFitted = 0;
    int linesFilled = 0;
    return this.MeasureString(text, width, format, out charactersFitted, out linesFilled);
  }

  public SizeF MeasureString(
    string text,
    PdfStringFormat format,
    out int charactersFitted,
    out int linesFilled)
  {
    return this.MeasureString(text, 0.0f, format, out charactersFitted, out linesFilled);
  }

  public SizeF MeasureString(
    string text,
    SizeF layoutArea,
    PdfStringFormat format,
    out int charactersFitted,
    out int linesFilled)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    PdfStringLayoutResult stringLayoutResult = new PdfStringLayouter().Layout(text, this, format, layoutArea);
    charactersFitted = stringLayoutResult.Remainder == null ? text.Length : text.Length - stringLayoutResult.Remainder.Length;
    linesFilled = stringLayoutResult.Empty ? 0 : stringLayoutResult.Lines.Length;
    return stringLayoutResult.ActualSize;
  }

  public SizeF MeasureString(
    string text,
    float width,
    PdfStringFormat format,
    out int charactersFitted,
    out int linesFilled)
  {
    SizeF layoutArea = new SizeF(width, 0.0f);
    return this.MeasureString(text, layoutArea, format, out charactersFitted, out linesFilled);
  }

  protected void SetStyle(PdfFontStyle style) => this.m_style = style;

  bool IPdfCache.EqualsTo(IPdfCache obj) => this.EqualsToFont(obj as PdfFont);

  IPdfPrimitive IPdfCache.GetInternals() => this.m_fontInternals;

  void IPdfCache.SetInternals(IPdfPrimitive internals)
  {
    this.m_fontInternals = internals != null ? internals : throw new ArgumentNullException(nameof (internals));
  }

  public bool Bold => (this.Style & PdfFontStyle.Bold) > PdfFontStyle.Regular;

  public float Height => this.Metrics.GetHeight((PdfStringFormat) null);

  internal string InternalFontName
  {
    get => this.m_internalFontName;
    set => this.m_internalFontName = value;
  }

  public bool Italic => (this.Style & PdfFontStyle.Italic) > PdfFontStyle.Regular;

  internal PdfFontMetrics Metrics
  {
    get => this.m_fontMetrics;
    set => this.m_fontMetrics = value;
  }

  public string Name => this.Metrics.Name;

  public float Size => this.m_size;

  public bool Strikeout => (this.Style & PdfFontStyle.Strikeout) > PdfFontStyle.Regular;

  public PdfFontStyle Style => this.m_style;

  IPdfPrimitive IPdfWrapper.Element => this.m_fontInternals;

  public bool Underline => (this.Style & PdfFontStyle.Underline) > PdfFontStyle.Regular;
}
