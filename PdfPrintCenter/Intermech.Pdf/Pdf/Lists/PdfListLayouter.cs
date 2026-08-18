// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Lists.PdfListLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Lists;

internal class PdfListLayouter : ElementLayouter
{
  private PdfBrush currentBrush;
  private PdfFont currentFont;
  private PdfStringFormat currentFormat;
  private PdfPage currentPage;
  private PdfPen currentPen;
  private RectangleF m_bounds;
  private PdfList m_curList;
  private bool m_finish;
  private PdfGraphics m_graphics;
  private float m_indent;
  private int m_index;
  private Stack<ListInfo> m_info;
  private float m_resultHeight;
  private float markerMaxWidth;
  private SizeF size;
  private bool usePaginateBounds;

  public PdfListLayouter(PdfList element)
    : base((PdfLayoutElement) element)
  {
    this.m_info = new Stack<ListInfo>();
    this.usePaginateBounds = true;
  }

  private void AfterItemLayouted(PdfListItem item, PdfPage page)
  {
    this.Element.OnEndItemLayout(new EndItemLayoutEventArgs(item, page));
  }

  private ListEndPageLayoutEventArgs AfterPageLayouted(
    RectangleF currentBounds,
    PdfPage currentPage,
    PdfList list)
  {
    ListEndPageLayoutEventArgs e = (ListEndPageLayoutEventArgs) null;
    if (this.Element.RaiseEndPageLayout && currentPage != null)
    {
      e = new ListEndPageLayoutEventArgs(new PdfLayoutResult(currentPage, currentBounds), list);
      this.Element.OnEndPageLayout((EndPageLayoutEventArgs) e);
    }
    return e;
  }

  private void BeforeItemLayout(PdfListItem item, PdfPage page)
  {
    this.Element.OnBeginItemLayout(new BeginItemLayoutEventArgs(item, page));
  }

  private bool BeforePageLayout(RectangleF currentBounds, PdfPage currentPage, PdfList list)
  {
    bool flag = false;
    if (this.Element.RaiseBeginPageLayout && currentPage != null)
    {
      ListBeginPageLayoutEventArgs e = new ListBeginPageLayoutEventArgs(currentBounds, currentPage, list);
      this.Element.OnBeginPageLayout((BeginPageLayoutEventArgs) e);
      flag = e.Cancel;
      this.m_bounds = e.Bounds;
      this.usePaginateBounds = false;
    }
    return flag;
  }

  private PdfStringLayoutResult CreateMarkerResult(
    int index,
    PdfList curList,
    Stack<ListInfo> info,
    PdfListItem item)
  {
    if (curList is PdfOrderedList)
      return this.CreateOrderedMarkerResult(curList, item, index, info, false);
    SizeF empty = SizeF.Empty;
    return this.CreateUnorderedMarkerResult(curList, item, ref empty);
  }

  private PdfStringLayoutResult CreateOrderedMarkerResult(
    PdfList list,
    PdfListItem item,
    int index,
    Stack<ListInfo> info,
    bool findMaxWidth)
  {
    PdfOrderedList pdfOrderedList = list as PdfOrderedList;
    pdfOrderedList.Marker.CurrentIndex = index;
    string str = string.Empty;
    if (pdfOrderedList.Marker.Style != PdfNumberStyle.None)
      str = pdfOrderedList.Marker.GetNumber() + pdfOrderedList.Marker.Suffix;
    if (pdfOrderedList.MarkerHierarchy)
    {
      foreach (ListInfo listInfo in info.ToArray())
      {
        if (listInfo.List is PdfOrderedList list1 && list1.Marker.Style != PdfNumberStyle.None)
        {
          PdfOrderedMarker marker = list1.Marker;
          str = listInfo.Number + marker.Delimiter + str;
          if (!list1.MarkerHierarchy)
            break;
        }
        else
          break;
      }
    }
    PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
    PdfOrderedMarker marker1 = (list as PdfOrderedList).Marker;
    PdfFont markerFont = this.GetMarkerFont((PdfMarker) marker1, item);
    PdfStringFormat markerFormat = this.GetMarkerFormat((PdfMarker) marker1, item);
    SizeF sizeF = new SizeF(this.size.Width, this.size.Height);
    if (!findMaxWidth)
    {
      sizeF.Width = this.markerMaxWidth;
      markerFormat = this.SetMarkerStringFormat(marker1, markerFormat);
    }
    string text = str;
    PdfFont font = markerFont;
    PdfStringFormat format = markerFormat;
    SizeF size = sizeF;
    return pdfStringLayouter.Layout(text, font, format, size);
  }

  private PdfStringLayoutResult CreateUnorderedMarkerResult(
    PdfList curList,
    PdfListItem item,
    ref SizeF markerSize)
  {
    PdfUnorderedMarker marker = (curList as PdfUnorderedList).Marker;
    PdfStringLayoutResult unorderedMarkerResult1 = (PdfStringLayoutResult) null;
    PdfFont markerFont = this.GetMarkerFont((PdfMarker) marker, item);
    PdfStringFormat markerFormat = this.GetMarkerFormat((PdfMarker) marker, item);
    PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
    switch (marker.Style)
    {
      case PdfUnorderedMarkerStyle.CustomString:
        return pdfStringLayouter.Layout(marker.Text, markerFont, markerFormat, this.size);
      case PdfUnorderedMarkerStyle.CustomImage:
        markerSize = new SizeF(markerFont.Size, markerFont.Size);
        marker.Size = markerSize;
        return unorderedMarkerResult1;
      case PdfUnorderedMarkerStyle.CustomTemplate:
        markerSize = new SizeF(markerFont.Size, markerFont.Size);
        marker.Size = markerSize;
        return unorderedMarkerResult1;
      default:
        PdfStandardFont font = new PdfStandardFont(PdfFontFamily.ZapfDingbats, markerFont.Size);
        PdfStringLayoutResult unorderedMarkerResult2 = pdfStringLayouter.Layout(marker.GetStyledText(), (PdfFont) font, (PdfStringFormat) null, this.size);
        marker.Size = unorderedMarkerResult2.ActualSize;
        if (marker.Pen != null)
          unorderedMarkerResult2.m_actualSize = new SizeF(unorderedMarkerResult2.ActualSize.Width + 2f * marker.Pen.Width, unorderedMarkerResult2.ActualSize.Height + 2f * marker.Pen.Width);
        return unorderedMarkerResult2;
    }
  }

  private void DrawItem(
    ref PageLayoutResult pageResult,
    float x,
    PdfList curList,
    int index,
    float indent,
    Stack<ListInfo> info,
    PdfListItem item,
    ref float height,
    ref float y)
  {
    PdfStringLayouter pdfStringLayouter = new PdfStringLayouter();
    PdfStringLayoutResult result = (PdfStringLayoutResult) null;
    bool flag1 = false;
    float textIndent = curList.TextIndent;
    float num1 = height + y;
    float num2 = indent + x;
    float num3 = 0.0f;
    SizeF size = this.size;
    string text1 = item.Text;
    string text2 = (string) null;
    PdfBrush brush = this.currentBrush;
    if (item.Brush != null)
      brush = item.Brush;
    PdfPen pen = this.currentPen;
    if (item.Pen != null)
      pen = item.Pen;
    PdfFont font = this.currentFont;
    if (item.Font != null)
      font = item.Font;
    PdfStringFormat format = this.currentFormat;
    if (item.StringFormat != null)
      format = item.StringFormat;
    if (((double) this.size.Width <= 0.0 || (double) this.size.Width < (double) font.Size) && this.currentPage != null)
      throw new Exception("There is not enough space to layout list.");
    this.size.Height -= height;
    PdfMarker marker = !(curList is PdfUnorderedList) ? (PdfMarker) (curList as PdfOrderedList).Marker : (PdfMarker) (curList as PdfUnorderedList).Marker;
    if (pageResult.Broken)
    {
      text1 = pageResult.ItemText;
      text2 = pageResult.MarkerText;
    }
    bool flag2 = true;
    PdfStringLayoutResult markerResult;
    float num4;
    SizeF sizeF;
    float height1;
    if (text2 != null && marker is PdfUnorderedMarker && (marker as PdfUnorderedMarker).Style == PdfUnorderedMarkerStyle.CustomString)
    {
      markerResult = pdfStringLayouter.Layout(text2, this.GetMarkerFont(marker, item), this.GetMarkerFormat(marker, item), this.size);
      num4 = num2 + markerResult.ActualSize.Width;
      PageLayoutResult pageLayoutResult = pageResult;
      sizeF = markerResult.ActualSize;
      double width = (double) sizeF.Width;
      pageLayoutResult.MarkerWidth = (float) width;
      sizeF = markerResult.ActualSize;
      height1 = sizeF.Height;
      flag2 = true;
    }
    else
    {
      markerResult = this.CreateMarkerResult(index, curList, info, item);
      if (markerResult != null)
      {
        if (curList is PdfOrderedList)
        {
          num4 = num2 + this.markerMaxWidth;
          pageResult.MarkerWidth = this.markerMaxWidth;
        }
        else
        {
          num4 = num2 + markerResult.ActualSize.Width;
          pageResult.MarkerWidth = markerResult.ActualSize.Width;
        }
        sizeF = markerResult.ActualSize;
        height1 = sizeF.Height;
        if (this.currentPage != null)
          flag2 = (double) height1 < (double) this.size.Height;
        if (markerResult.Empty)
          flag2 = false;
      }
      else
      {
        double num5 = (double) num2;
        sizeF = (marker as PdfUnorderedMarker).Size;
        double width1 = (double) sizeF.Width;
        num4 = (float) (num5 + width1);
        PageLayoutResult pageLayoutResult = pageResult;
        sizeF = (marker as PdfUnorderedMarker).Size;
        double width2 = (double) sizeF.Width;
        pageLayoutResult.MarkerWidth = (float) width2;
        sizeF = (marker as PdfUnorderedMarker).Size;
        height1 = sizeF.Height;
        if (this.currentPage != null)
          flag2 = (double) height1 < (double) this.size.Height;
      }
    }
    if (text2 == null || text2 != null && text2.Length == 0)
      flag2 = true;
    if (text1 != null & flag2)
    {
      size = this.size;
      size.Width -= pageResult.MarkerWidth;
      if ((double) item.TextIndent == 0.0)
        size.Width -= textIndent;
      else
        size.Width -= item.TextIndent;
      if (((double) size.Width <= 0.0 || (double) size.Width < (double) font.Size) && this.currentPage != null)
        throw new Exception("There is not enough space to layout the item text. Marker is too long or there is no enough space to draw it.");
      float num6 = num4;
      float x1;
      if (!marker.RightToLeft)
      {
        x1 = (double) item.TextIndent != 0.0 ? num6 + item.TextIndent : num6 + textIndent;
      }
      else
      {
        x1 = num6 - pageResult.MarkerWidth;
        if (format != null && (format.Alignment == PdfTextAlignment.Right || format.Alignment == PdfTextAlignment.Center))
          x1 -= indent;
      }
      if (this.currentPage == null && format != null)
      {
        format = (PdfStringFormat) format.Clone();
        format.Alignment = PdfTextAlignment.Left;
      }
      result = pdfStringLayouter.Layout(text1, font, format, size);
      RectangleF layoutRectangle = new RectangleF(x1, num1, size.Width, size.Height);
      this.m_graphics.DrawStringLayoutResult(result, font, pen, brush, layoutRectangle, format);
      y = num1;
      sizeF = result.ActualSize;
      num3 = sizeF.Height;
    }
    height = (double) num3 < (double) height1 ? height1 : num3;
    if (result != null && !PdfListLayouter.IsNullOrEmpty(result.Remainder) || markerResult != null && !PdfListLayouter.IsNullOrEmpty(markerResult.Remainder) || !flag2)
    {
      y = 0.0f;
      height = 0.0f;
      if (result != null)
      {
        pageResult.ItemText = result.Remainder;
        if (result.Remainder == item.Text)
          flag2 = false;
      }
      else
        pageResult.ItemText = flag2 ? (string) null : item.Text;
      pageResult.MarkerText = markerResult == null ? (string) null : markerResult.Remainder;
      pageResult.Broken = true;
      pageResult.Y = 0.0f;
      this.m_bounds.Y = 0.0f;
    }
    else
      pageResult.Broken = false;
    if (result != null)
    {
      pageResult.MarkerX = num4;
      if (format != null)
      {
        switch (format.Alignment)
        {
          case PdfTextAlignment.Center:
            PageLayoutResult pageLayoutResult1 = pageResult;
            double num7 = (double) num4 + (double) size.Width / 2.0;
            sizeF = result.ActualSize;
            double num8 = (double) sizeF.Width / 2.0;
            double num9 = num7 - num8;
            pageLayoutResult1.MarkerX = (float) num9;
            break;
          case PdfTextAlignment.Right:
            PageLayoutResult pageLayoutResult2 = pageResult;
            double num10 = (double) num4 + (double) size.Width;
            sizeF = result.ActualSize;
            double width3 = (double) sizeF.Width;
            double num11 = num10 - width3;
            pageLayoutResult2.MarkerX = (float) num11;
            break;
        }
      }
      if (marker.RightToLeft)
      {
        PageLayoutResult pageLayoutResult3 = pageResult;
        double markerX = (double) pageLayoutResult3.MarkerX;
        sizeF = result.ActualSize;
        double width4 = (double) sizeF.Width;
        pageLayoutResult3.MarkerX = (float) (markerX + width4);
        if ((double) item.TextIndent == 0.0)
          pageResult.MarkerX += textIndent;
        else
          pageResult.MarkerX += item.TextIndent;
        if (format != null && (format.Alignment == PdfTextAlignment.Right || format.Alignment == PdfTextAlignment.Center))
          pageResult.MarkerX -= indent;
      }
    }
    if (marker is PdfUnorderedMarker && (marker as PdfUnorderedMarker).Style == PdfUnorderedMarkerStyle.CustomString)
    {
      if (markerResult == null)
        return;
      flag1 = this.DrawMarker(curList, item, markerResult, num1, pageResult.MarkerX);
      pageResult.MarkerWrote = true;
      PageLayoutResult pageLayoutResult = pageResult;
      sizeF = markerResult.ActualSize;
      double width = (double) sizeF.Width;
      pageLayoutResult.MarkerWidth = (float) width;
    }
    else
    {
      if (!flag2 || pageResult.MarkerWrote)
        return;
      bool flag3 = this.DrawMarker(curList, item, markerResult, num1, pageResult.MarkerX);
      pageResult.MarkerWrote = flag3;
      if (curList is PdfOrderedList)
      {
        PageLayoutResult pageLayoutResult = pageResult;
        sizeF = markerResult.ActualSize;
        double width = (double) sizeF.Width;
        pageLayoutResult.MarkerWidth = (float) width;
      }
      else
      {
        PageLayoutResult pageLayoutResult = pageResult;
        sizeF = (marker as PdfUnorderedMarker).Size;
        double width = (double) sizeF.Width;
        pageLayoutResult.MarkerWidth = (float) width;
      }
    }
  }

  private bool DrawMarker(
    PdfList curList,
    PdfListItem item,
    PdfStringLayoutResult markerResult,
    float posY,
    float posX)
  {
    if (curList is PdfOrderedList)
    {
      if (curList.Font != null && markerResult != null && (double) curList.Font.Size > (double) markerResult.m_actualSize.Height)
      {
        posY += (float) ((double) curList.Font.Size / 2.0 - (double) markerResult.m_actualSize.Height / 2.0);
        markerResult.m_actualSize.Height += posY;
      }
      this.DrawOrderedMarker(curList, markerResult, item, posX, posY);
    }
    else
    {
      if (curList.Font != null && markerResult != null && (double) curList.Font.Size > (double) markerResult.m_actualSize.Height)
      {
        posY += (float) ((double) curList.Font.Size / 2.0 - (double) markerResult.m_actualSize.Height / 2.0);
        markerResult.m_actualSize.Height += posY;
      }
      this.DrawUnorderedMarker(curList, markerResult, item, posX, posY);
    }
    return true;
  }

  private PdfStringLayoutResult DrawOrderedMarker(
    PdfList curList,
    PdfStringLayoutResult markerResult,
    PdfListItem item,
    float posX,
    float posY)
  {
    PdfOrderedMarker marker = (curList as PdfOrderedList).Marker;
    PdfFont markerFont = this.GetMarkerFont((PdfMarker) marker, item);
    PdfStringFormat markerFormat = this.GetMarkerFormat((PdfMarker) marker, item);
    PdfPen markerPen = this.GetMarkerPen((PdfMarker) marker, item);
    PdfBrush markerBrush = this.GetMarkerBrush((PdfMarker) marker, item);
    RectangleF layoutRectangle = new RectangleF(new PointF(posX - this.markerMaxWidth, posY), markerResult.ActualSize);
    layoutRectangle.Width = this.markerMaxWidth;
    PdfStringFormat format = this.SetMarkerStringFormat(marker, markerFormat);
    this.m_graphics.DrawStringLayoutResult(markerResult, markerFont, markerPen, markerBrush, layoutRectangle, format);
    return markerResult;
  }

  private PdfStringLayoutResult DrawUnorderedMarker(
    PdfList curList,
    PdfStringLayoutResult markerResult,
    PdfListItem item,
    float posX,
    float posY)
  {
    PdfUnorderedMarker marker = (curList as PdfUnorderedList).Marker;
    PdfFont markerFont = this.GetMarkerFont((PdfMarker) marker, item);
    PdfPen markerPen = this.GetMarkerPen((PdfMarker) marker, item);
    PdfBrush markerBrush = this.GetMarkerBrush((PdfMarker) marker, item);
    PdfStringFormat markerFormat = this.GetMarkerFormat((PdfMarker) marker, item);
    if (markerResult != null)
    {
      PointF pointF = new PointF(posX - markerResult.ActualSize.Width, posY);
      marker.Size = markerResult.ActualSize;
      if (marker.Style == PdfUnorderedMarkerStyle.CustomString)
      {
        RectangleF layoutRectangle = new RectangleF(pointF, markerResult.ActualSize);
        this.m_graphics.DrawStringLayoutResult(markerResult, markerFont, markerPen, markerBrush, layoutRectangle, markerFormat);
      }
      else
      {
        marker.UnicodeFont = (PdfFont) new PdfStandardFont(PdfFontFamily.ZapfDingbats, markerFont.Size);
        marker.Draw(this.m_graphics, pointF, markerBrush, markerPen);
      }
    }
    else
    {
      marker.Size = new SizeF(markerFont.Size, markerFont.Size);
      PointF point = new PointF(posX - markerFont.Size, posY);
      marker.Draw(this.m_graphics, point, markerBrush, markerPen);
    }
    return (PdfStringLayoutResult) null;
  }

  private PdfBrush GetMarkerBrush(PdfMarker marker, PdfListItem item)
  {
    PdfBrush markerBrush = marker.Brush;
    if (marker.Brush == null)
    {
      markerBrush = item.Brush;
      if (item.Brush == null)
        markerBrush = this.currentBrush;
    }
    return markerBrush;
  }

  private PdfFont GetMarkerFont(PdfMarker marker, PdfListItem item)
  {
    PdfFont markerFont = marker.Font;
    if (marker.Font == null)
    {
      markerFont = item.Font;
      if (item.Font == null)
        markerFont = this.currentFont;
    }
    marker.Font = markerFont;
    return markerFont;
  }

  private PdfStringFormat GetMarkerFormat(PdfMarker marker, PdfListItem item)
  {
    PdfStringFormat markerFormat = marker.StringFormat;
    if (marker.StringFormat == null)
    {
      markerFormat = item.StringFormat;
      if (item.StringFormat == null)
        markerFormat = this.currentFormat;
    }
    return markerFormat;
  }

  private float GetMarkerMaxWidth(PdfOrderedList list, Stack<ListInfo> info)
  {
    float markerMaxWidth = -1f;
    for (int index = 0; index < list.Items.Count; ++index)
    {
      PdfStringLayoutResult orderedMarkerResult = this.CreateOrderedMarkerResult((PdfList) list, list.Items[index], index + list.Marker.StartNumber, info, true);
      double num = (double) markerMaxWidth;
      SizeF actualSize = orderedMarkerResult.ActualSize;
      double width = (double) actualSize.Width;
      if (num < width)
      {
        actualSize = orderedMarkerResult.ActualSize;
        markerMaxWidth = actualSize.Width;
      }
    }
    return markerMaxWidth;
  }

  private PdfPen GetMarkerPen(PdfMarker marker, PdfListItem item)
  {
    PdfPen markerPen = marker.Pen;
    if (marker.Pen == null)
    {
      markerPen = item.Pen;
      if (item.Pen == null)
        markerPen = this.currentPen;
    }
    return markerPen;
  }

  private static bool IsNullOrEmpty(string text) => text == null || text == string.Empty;

  public void Layout(PdfGraphics graphics, PointF point)
  {
    RectangleF boundaries = new RectangleF(point, SizeF.Empty);
    this.Layout(graphics, boundaries);
  }

  public void Layout(PdfGraphics graphics, RectangleF boundaries)
  {
    this.m_graphics = graphics != null ? graphics : throw new ArgumentNullException(nameof (graphics));
    PdfLayoutParams pdfLayoutParams = new PdfLayoutParams()
    {
      Bounds = boundaries,
      Format = new PdfLayoutFormat()
    };
    pdfLayoutParams.Format.Layout = PdfLayoutType.OnePage;
    this.LayoutInternal(pdfLayoutParams);
  }

  public void Layout(PdfGraphics graphics, float x, float y)
  {
    RectangleF boundaries = new RectangleF(new PointF(x, y), SizeF.Empty);
    this.Layout(graphics, boundaries);
  }

  protected override PdfLayoutResult LayoutInternal(PdfLayoutParams param)
  {
    this.currentPage = param.Page;
    this.m_bounds = param.Bounds;
    if (param.Bounds.Size == SizeF.Empty && this.currentPage != null)
    {
      this.m_bounds.Size = this.currentPage.GetClientSize();
      this.m_bounds.Width -= this.m_bounds.X;
      this.m_bounds.Height -= this.m_bounds.Y;
    }
    if (this.currentPage != null)
      this.m_graphics = this.currentPage.Graphics;
    PageLayoutResult pageResult = new PageLayoutResult();
    pageResult.Broken = false;
    pageResult.Y = this.m_bounds.Y;
    this.m_curList = this.Element;
    this.m_indent = this.Element.Indent;
    this.SetCurrentParameters(this.Element);
    if (this.Element.Brush == null)
      this.currentBrush = PdfBrushes.Black;
    if (this.Element.Font == null)
      this.currentFont = PdfDocument.DefaultFont;
    if (this.m_curList is PdfOrderedList)
      this.markerMaxWidth = this.GetMarkerMaxWidth(this.m_curList as PdfOrderedList, this.m_info);
    bool flag1 = param.Format.Layout == PdfLayoutType.OnePage;
    while (!this.m_finish)
    {
      bool flag2 = this.BeforePageLayout(this.m_bounds, this.currentPage, this.m_curList);
      pageResult.Y = this.m_bounds.Y;
      ListEndPageLayoutEventArgs pageLayoutEventArgs = (ListEndPageLayoutEventArgs) null;
      if (!flag2)
      {
        pageResult = this.LayoutOnPage(pageResult);
        pageLayoutEventArgs = this.AfterPageLayouted(this.m_bounds, this.currentPage, this.m_curList);
        flag2 = pageLayoutEventArgs != null && pageLayoutEventArgs.Cancel;
      }
      if (!(flag1 | flag2))
      {
        if (this.currentPage != null && !this.m_finish)
        {
          this.currentPage = pageLayoutEventArgs == null || pageLayoutEventArgs.NextPage == null ? this.GetNextPage(this.currentPage) : pageLayoutEventArgs.NextPage;
          this.m_graphics = this.currentPage.Graphics;
          if (param.Bounds.Size == SizeF.Empty)
          {
            this.m_bounds.Size = this.currentPage.GetClientSize();
            this.m_bounds.Width -= this.m_bounds.X;
            this.m_bounds.Height -= this.m_bounds.Y;
          }
          if (param.Format != null && param.Format.UsePaginateBounds && this.usePaginateBounds)
            this.m_bounds = param.Format.PaginateBounds;
        }
      }
      else
        break;
    }
    this.m_info.Clear();
    return new PdfLayoutResult(this.currentPage, new RectangleF(this.m_bounds.X, pageResult.Y, this.m_bounds.Width, this.m_resultHeight));
  }

  private PageLayoutResult LayoutOnPage(PageLayoutResult pageResult)
  {
    float height = 0.0f;
    float num = 0.0f;
    float y = this.m_bounds.Y;
    float x = this.m_bounds.X;
    this.size = this.m_bounds.Size;
    this.size.Width -= this.m_indent;
    while (true)
    {
      for (; this.m_index < this.m_curList.Items.Count; ++this.m_index)
      {
        PdfListItem pdfListItem = this.m_curList.Items[this.m_index];
        if (this.currentPage != null && !pageResult.Broken)
          this.BeforeItemLayout(pdfListItem, this.currentPage);
        this.DrawItem(ref pageResult, x, this.m_curList, this.m_index, this.m_indent, this.m_info, pdfListItem, ref height, ref y);
        num += height;
        if (pageResult.Broken)
          return pageResult;
        if (this.currentPage != null)
          this.AfterItemLayouted(pdfListItem, this.currentPage);
        pageResult.MarkerWrote = false;
        if (pdfListItem.SubList != null && pdfListItem.SubList.Items.Count > 0)
        {
          if (this.m_curList is PdfOrderedList)
          {
            PdfOrderedList curList = this.m_curList as PdfOrderedList;
            curList.Marker.CurrentIndex = this.m_index;
            this.m_info.Push(new ListInfo(this.m_curList, this.m_index, curList.Marker.GetNumber())
            {
              Brush = this.currentBrush,
              Font = this.currentFont,
              Format = this.currentFormat,
              Pen = this.currentPen,
              MarkerWidth = this.markerMaxWidth
            });
          }
          else
            this.m_info.Push(new ListInfo(this.m_curList, this.m_index)
            {
              Brush = this.currentBrush,
              Font = this.currentFont,
              Format = this.currentFormat,
              Pen = this.currentPen
            });
          this.m_curList = pdfListItem.SubList;
          if (this.m_curList is PdfOrderedList)
            this.markerMaxWidth = this.GetMarkerMaxWidth(this.m_curList as PdfOrderedList, this.m_info);
          this.m_index = -1;
          this.m_indent += this.m_curList.Indent;
          this.size.Width -= this.m_curList.Indent;
          this.SetCurrentParameters(pdfListItem);
          this.SetCurrentParameters(this.m_curList);
        }
      }
      if (this.m_info.Count != 0)
      {
        ListInfo listInfo = this.m_info.Pop();
        this.m_index = listInfo.Index + 1;
        this.m_indent -= this.m_curList.Indent;
        this.size.Width += this.m_curList.Indent;
        this.markerMaxWidth = listInfo.MarkerWidth;
        this.currentBrush = listInfo.Brush;
        this.currentPen = listInfo.Pen;
        this.currentFont = listInfo.Font;
        this.currentFormat = listInfo.Format;
        this.m_curList = listInfo.List;
      }
      else
        break;
    }
    this.m_resultHeight = num;
    this.m_finish = true;
    return pageResult;
  }

  private void SetCurrentParameters(PdfList list)
  {
    if (list.Brush != null)
      this.currentBrush = list.Brush;
    if (list.Pen != null)
      this.currentPen = list.Pen;
    if (list.Font != null)
      this.currentFont = list.Font;
    if (list.StringFormat == null)
      return;
    this.currentFormat = list.StringFormat;
  }

  private void SetCurrentParameters(PdfListItem item)
  {
    if (item.Brush != null)
      this.currentBrush = item.Brush;
    if (item.Pen != null)
      this.currentPen = item.Pen;
    if (item.Font != null)
      this.currentFont = item.Font;
    if (item.StringFormat == null)
      return;
    this.currentFormat = item.StringFormat;
  }

  private PdfStringFormat SetMarkerStringFormat(
    PdfOrderedMarker marker,
    PdfStringFormat markerFormat)
  {
    markerFormat = markerFormat != null ? (PdfStringFormat) markerFormat.Clone() : new PdfStringFormat();
    if (marker.StringFormat == null)
    {
      markerFormat.Alignment = PdfTextAlignment.Right;
      if (marker.RightToLeft)
        markerFormat.Alignment = PdfTextAlignment.Left;
    }
    if (this.currentPage == null && markerFormat != null)
    {
      markerFormat = (PdfStringFormat) markerFormat.Clone();
      markerFormat.Alignment = PdfTextAlignment.Left;
    }
    return markerFormat;
  }

  public PdfList Element => base.Element as PdfList;
}
