// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.TextLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal class TextLayouter(PdfTextElement element) : ElementLayouter((PdfLayoutElement) element)
{
  private PdfStringFormat m_format;

  private void CheckCorectStringFormat(LineInfo lineInfo)
  {
    if (this.m_format == null)
      return;
    this.m_format.FirstLineIndent = (lineInfo.LineType & LineType.NewLineBreak) > LineType.None ? this.Element.StringFormat.FirstLineIndent : 0.0f;
  }

  private RectangleF CheckCorrectBounds(PdfPage currentPage, RectangleF currentBounds)
  {
    if (currentPage == null)
      throw new ArgumentNullException(nameof (currentPage));
    SizeF clientSize = currentPage.Graphics.ClientSize;
    currentBounds.Height = (double) currentBounds.Height > 0.0 ? currentBounds.Height : clientSize.Height - currentBounds.Y;
    return currentBounds;
  }

  private PdfTextLayoutResult GetLayoutResult(TextLayouter.TextPageLayoutResult pageResult)
  {
    return new PdfTextLayoutResult(pageResult.Page, pageResult.Bounds, pageResult.Remainder, pageResult.LastLineBounds);
  }

  private RectangleF GetTextPageBounds(
    PdfPage currentPage,
    RectangleF currentBounds,
    PdfStringLayoutResult stringResult)
  {
    if (currentPage == null)
      throw new ArgumentNullException(nameof (currentPage));
    SizeF textSize = stringResult != null ? stringResult.ActualSize : throw new ArgumentNullException(nameof (stringResult));
    float x = currentBounds.X;
    float y = currentBounds.Y;
    float width = (double) currentBounds.Width > 0.0 ? currentBounds.Width : textSize.Width;
    float height = textSize.Height;
    RectangleF rectangleF = currentPage.Graphics.CheckCorrectLayoutRectangle(textSize, currentBounds.X, currentBounds.Y, this.m_format);
    if ((double) currentBounds.Width <= 0.0)
      x = rectangleF.X;
    if ((double) currentBounds.Height <= 0.0)
      y = rectangleF.Y;
    float verticalAlignShift = currentPage.Graphics.GetTextVerticalAlignShift(textSize.Height, currentBounds.Height, this.m_format);
    return new RectangleF(x, y + verticalAlignShift, width, height);
  }

  protected override PdfLayoutResult LayoutInternal(PdfLayoutParams param)
  {
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    this.m_format = this.Element.StringFormat != null ? (PdfStringFormat) this.Element.StringFormat.Clone() : (PdfStringFormat) null;
    PdfPage currentPage = param.Page;
    RectangleF currentBounds = param.Bounds;
    string remainder = this.Element.Value;
    TextLayouter.TextPageLayoutResult pageResult = new TextLayouter.TextPageLayoutResult();
    pageResult.Page = currentPage;
    pageResult.Remainder = remainder;
    while (true)
    {
      bool flag = this.RaiseBeforePageLayout(currentPage, ref currentBounds);
      EndTextPageLayoutEventArgs pageLayoutEventArgs = (EndTextPageLayoutEventArgs) null;
      if (!flag)
      {
        pageResult = this.LayoutOnPage(remainder, currentPage, currentBounds, param);
        pageLayoutEventArgs = this.RaisePageLayouted(pageResult);
        flag = pageLayoutEventArgs != null && pageLayoutEventArgs.Cancel;
      }
      if (!pageResult.End && !flag)
      {
        currentBounds = this.GetPaginateBounds(param);
        remainder = pageResult.Remainder;
        currentPage = pageLayoutEventArgs == null || pageLayoutEventArgs.NextPage == null ? this.GetNextPage(currentPage) : pageLayoutEventArgs.NextPage;
      }
      else
        break;
    }
    return (PdfLayoutResult) this.GetLayoutResult(pageResult);
  }

  private TextLayouter.TextPageLayoutResult LayoutOnPage(
    string text,
    PdfPage currentPage,
    RectangleF currentBounds,
    PdfLayoutParams param)
  {
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (currentPage == null)
      throw new ArgumentNullException(nameof (currentPage));
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    TextLayouter.TextPageLayoutResult pageLayoutResult = new TextLayouter.TextPageLayoutResult();
    pageLayoutResult.Remainder = text;
    pageLayoutResult.Page = currentPage;
    currentBounds = this.CheckCorrectBounds(currentPage, currentBounds);
    if ((double) currentBounds.Height < 0.0)
    {
      currentPage = this.GetNextPage(currentPage);
      PdfMargins margins = currentPage.Section.PageSettings.Margins;
      pageLayoutResult.Page = currentPage;
      currentBounds = new RectangleF(currentBounds.X, 0.0f, currentBounds.Width, currentBounds.Height);
    }
    PdfStringLayoutResult stringLayoutResult = new PdfStringLayouter().Layout(text, this.Element.Font, this.m_format, currentBounds, currentPage.GetClientSize().Height);
    bool flag1 = stringLayoutResult.Remainder == null || stringLayoutResult.Remainder.Length == 0;
    if (((param.Format.Break != PdfLayoutBreakType.FitElement ? 1 : (currentPage != param.Page ? 1 : 0)) | (flag1 ? 1 : 0)) != 0 && !stringLayoutResult.Empty)
    {
      PdfGraphics graphics = currentPage.Graphics;
      graphics.DrawStringLayoutResult(stringLayoutResult, this.Element.Font, this.Element.Pen, this.Element.GetBrush(), currentBounds, this.m_format);
      LineInfo line = stringLayoutResult.Lines[stringLayoutResult.LineCount - 1];
      pageLayoutResult.LastLineBounds = graphics.GetLineBounds(stringLayoutResult.LineCount - 1, stringLayoutResult, this.Element.Font, currentBounds, this.m_format);
      pageLayoutResult.Bounds = this.GetTextPageBounds(currentPage, currentBounds, stringLayoutResult);
      pageLayoutResult.Remainder = stringLayoutResult.Remainder;
      this.CheckCorectStringFormat(line);
    }
    bool flag2 = stringLayoutResult.Empty && (param.Format.Break != PdfLayoutBreakType.FitElement || param.Format.Break == PdfLayoutBreakType.FitElement && currentPage != param.Page);
    pageLayoutResult.End = flag1 | flag2 || param.Format.Layout == PdfLayoutType.OnePage;
    return pageLayoutResult;
  }

  private bool RaiseBeforePageLayout(PdfPage currentPage, ref RectangleF currentBounds)
  {
    bool flag = false;
    if (this.Element.RaiseBeginPageLayout)
    {
      BeginPageLayoutEventArgs e = new BeginPageLayoutEventArgs(currentBounds, currentPage);
      this.Element.OnBeginPageLayout(e);
      flag = e.Cancel;
      currentBounds = e.Bounds;
    }
    return flag;
  }

  private EndTextPageLayoutEventArgs RaisePageLayouted(TextLayouter.TextPageLayoutResult pageResult)
  {
    EndTextPageLayoutEventArgs e = (EndTextPageLayoutEventArgs) null;
    if (this.Element.RaiseEndPageLayout)
    {
      e = new EndTextPageLayoutEventArgs(this.GetLayoutResult(pageResult));
      this.Element.OnEndPageLayout((EndPageLayoutEventArgs) e);
    }
    return e;
  }

  public PdfTextElement Element => base.Element as PdfTextElement;

  private struct TextPageLayoutResult
  {
    public PdfPage Page;
    public RectangleF Bounds;
    public bool End;
    public string Remainder;
    public RectangleF LastLineBounds;
  }
}
