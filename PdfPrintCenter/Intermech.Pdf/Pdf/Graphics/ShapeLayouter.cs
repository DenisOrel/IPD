// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.ShapeLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Images.Metafiles;
using Syncfusion.Pdf.HtmlToPdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal class ShapeLayouter(PdfShapeElement element) : ElementLayouter((PdfLayoutElement) element)
{
  private static int index;
  private static bool last;
  private static float splitDiff;

  protected virtual RectangleF CheckCorrectCurrentBounds(
    PdfPage currentPage,
    RectangleF currentBounds,
    RectangleF shapeLayoutBounds,
    PdfLayoutParams param)
  {
    if (currentPage == null)
      throw new ArgumentNullException(nameof (currentPage));
    SizeF clientSize = currentPage.Graphics.ClientSize;
    currentBounds.Width = (double) currentBounds.Width > 0.0 ? currentBounds.Width : clientSize.Width - currentBounds.X;
    currentBounds.Height = (double) currentBounds.Height > 0.0 ? currentBounds.Height : clientSize.Height - currentBounds.Y;
    return currentBounds;
  }

  private void DrawShape(PdfGraphics g, RectangleF currentBounds, RectangleF drawRectangle)
  {
    PdfGraphicsState state = g != null ? g.Save() : throw new ArgumentNullException(nameof (g));
    try
    {
      g.SetClip(currentBounds);
      this.Element.Draw(g, drawRectangle.Location);
    }
    finally
    {
      g.Restore(state);
    }
  }

  private void DrawShape(
    ref PdfPage pdfPage,
    RectangleF currentBounds,
    RectangleF drawRectangle,
    bool tagged)
  {
    PdfMetafile element = this.Element as PdfMetafile;
    using (Metafile metaFile = element.InternalImage.Clone() as Metafile)
    {
      using (PdfEmfRenderer renderer = new PdfEmfRenderer(pdfPage.Graphics, currentBounds.Location, true))
      {
        using (MetaRecordParser metaRecordParser = new MetaRecordParser(renderer, metaFile))
        {
          metaRecordParser.Parser.PageScale = element.PageScale;
          metaRecordParser.Parser.PageUnit = element.PageUnit;
          PdfGraphicsState state = pdfPage.Graphics.Save();
          metaRecordParser.Enumerate();
          PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
          if ((double) pdfPage.Graphics.Split > 0.0)
          {
            TextRegionManager context = renderer.Context as TextRegionManager;
            ImageRegionManager imageContext = metaRecordParser.ImageContext as ImageRegionManager;
            float pixels1 = pdfUnitConvertor.ConvertToPixels(pdfPage.Graphics.Split, PdfGraphicsUnit.Point);
            double topCoordinate1 = (double) context.GetTopCoordinate(pixels1);
            float topCoordinate2 = imageContext.GetTopCoordinate((float) topCoordinate1);
            float pixels2 = pdfUnitConvertor.ConvertToPixels(pdfPage.Graphics.ClientSize.Width, PdfGraphicsUnit.Point);
            float pixels3 = pdfUnitConvertor.ConvertToPixels(pdfPage.Graphics.ClientSize.Height, PdfGraphicsUnit.Point);
            pdfPage.Graphics.DrawRectangle(PdfBrushes.White, new RectangleF(0.0f, topCoordinate2, pixels2, pixels3 - topCoordinate2));
            pdfPage.Graphics.SetClip(new RectangleF(0.0f, topCoordinate2, pixels2, pixels3 - topCoordinate2));
            pdfPage.Graphics.Split = pdfUnitConvertor.ConvertFromPixels(topCoordinate2, PdfGraphicsUnit.Point);
          }
          pdfPage.Graphics.Restore(state);
          pdfPage = renderer.Graphics.Page as PdfPage;
        }
      }
    }
  }

  private bool FitsToBounds(RectangleF currentBounds, RectangleF shapeLayoutBounds)
  {
    return (double) shapeLayoutBounds.Height <= (double) currentBounds.Height;
  }

  private RectangleF GetDrawBounds(RectangleF currentBounds, RectangleF shapeLayoutBounds)
  {
    RectangleF drawBounds = currentBounds;
    drawBounds.Y -= shapeLayoutBounds.Y;
    drawBounds.Height += shapeLayoutBounds.Y;
    return drawBounds;
  }

  private PdfLayoutResult GetLayoutResult(ShapeLayouter.ShapeLayoutResult pageResult)
  {
    return new PdfLayoutResult(pageResult.Page, pageResult.Bounds);
  }

  private RectangleF GetNextShapeBounds(
    RectangleF shapeLayoutBounds,
    ShapeLayouter.ShapeLayoutResult pageResult)
  {
    RectangleF bounds = pageResult.Bounds;
    shapeLayoutBounds.Y += bounds.Height;
    shapeLayoutBounds.Height -= bounds.Height;
    return shapeLayoutBounds;
  }

  private RectangleF GetPageResultBounds(RectangleF currentBounds, RectangleF shapeLayoutBounds)
  {
    RectangleF pageResultBounds = currentBounds;
    pageResultBounds.Height = Math.Min(pageResultBounds.Height, shapeLayoutBounds.Height);
    return pageResultBounds;
  }

  protected override PdfLayoutResult LayoutInternal(PdfLayoutParams param)
  {
    PdfPage currentPage = param != null ? param.Page : throw new ArgumentNullException(nameof (param));
    RectangleF currentBounds = param.Bounds;
    RectangleF shapeLayoutBounds = this.Element.GetBounds() with
    {
      Location = PointF.Empty
    };
    ShapeLayouter.ShapeLayoutResult pageResult = new ShapeLayouter.ShapeLayoutResult();
    pageResult.Page = currentPage;
    if (this.Element is PdfImage && (double) (this.Element as PdfImage).ScrollBarHeight > 0.0)
    {
      double height1 = (double) shapeLayoutBounds.Height;
      SizeF physicalDimension = (this.Element as PdfImage).PhysicalDimension;
      double height2 = (double) physicalDimension.Height;
      if (height1 <= height2)
      {
        shapeLayoutBounds.Height -= (this.Element as PdfImage).ScrollBarHeight;
      }
      else
      {
        ref RectangleF local = ref shapeLayoutBounds;
        physicalDimension = (this.Element as PdfImage).PhysicalDimension;
        double num = (double) physicalDimension.Width - (double) (this.Element as PdfImage).ScrollBarHeight;
        local.Height = (float) num;
      }
    }
    while (true)
    {
      bool flag = this.RaiseBeforePageLayout(currentPage, ref currentBounds);
      EndPageLayoutEventArgs pageLayoutEventArgs = (EndPageLayoutEventArgs) null;
      if (!flag)
      {
        pageResult = this.LayoutOnPage(currentPage, currentBounds, shapeLayoutBounds, param);
        pageLayoutEventArgs = this.RaiseEndPageLayout(pageResult);
        flag = pageLayoutEventArgs != null && pageLayoutEventArgs.Cancel;
      }
      if (!pageResult.Page.Document.FileStructure.TaggedPdf || pageResult.End || flag)
      {
        if (!pageResult.End && !flag)
        {
          currentBounds = this.GetPaginateBounds(param);
          shapeLayoutBounds = this.GetNextShapeBounds(shapeLayoutBounds, pageResult);
          currentPage = pageLayoutEventArgs == null || pageLayoutEventArgs.NextPage == null ? this.GetNextPage(currentPage) : pageLayoutEventArgs.NextPage;
        }
        else
          goto label_12;
      }
      else
        break;
    }
    return new PdfLayoutResult(pageResult.Page, pageResult.Bounds);
label_12:
    return this.GetLayoutResult(pageResult);
  }

  protected override PdfLayoutResult LayoutInternal(HtmlToPdfLayoutParams param)
  {
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    PdfLayoutParams pdfLayoutParams = new PdfLayoutParams();
    pdfLayoutParams.Bounds = param.Bounds;
    pdfLayoutParams.Format = param.Format;
    pdfLayoutParams.Page = param.Page;
    if (param.VerticalOffsets.Length == 1)
      return this.LayoutInternal(pdfLayoutParams);
    PdfPage currentPage = param.Page;
    RectangleF currentBounds = param.Bounds;
    RectangleF shapeLayoutBounds = this.Element.GetBounds() with
    {
      Location = PointF.Empty
    };
    PdfLayoutResult pdfLayoutResult = (PdfLayoutResult) null;
    ShapeLayouter.ShapeLayoutResult pageResult = new ShapeLayouter.ShapeLayoutResult();
    pageResult.Page = currentPage;
    if (param.Page.Section.Count == 1)
    {
      ShapeLayouter.last = false;
      ShapeLayouter.index = 0;
      ShapeLayouter.splitDiff = 0.0f;
    }
    SizeF sizeF;
    if (this.Element is PdfImage && (double) (this.Element as PdfImage).ScrollBarHeight > 0.0)
    {
      double height1 = (double) shapeLayoutBounds.Height;
      sizeF = (this.Element as PdfImage).PhysicalDimension;
      double height2 = (double) sizeF.Height;
      if (height1 <= height2)
      {
        shapeLayoutBounds.Height -= (this.Element as PdfImage).ScrollBarHeight;
      }
      else
      {
        ref RectangleF local = ref shapeLayoutBounds;
        sizeF = (this.Element as PdfImage).PhysicalDimension;
        double num = (double) sizeF.Width - (double) (this.Element as PdfImage).ScrollBarHeight;
        local.Height = (float) num;
      }
    }
    int num1 = 0;
    float num2 = (param.Format as PdfMetafileLayoutFormat).TrackHeight;
    bool flag1 = false;
    int length = param.VerticalOffsets.Length;
    foreach (float verticalOffset in param.VerticalOffsets)
    {
      if ((double) param.VerticalOffsets[ShapeLayouter.index] == (double) verticalOffset)
      {
        bool flag2 = false;
        float val2 = verticalOffset;
        while (!flag2)
        {
          float num3;
          if ((double) val2 >= 0.0)
          {
            if ((double) num2 > 0.0 && num1 == 0 && (double) val2 > (double) num2)
            {
              val2 = verticalOffset - num2;
              --ShapeLayouter.index;
            }
            else
              val2 = verticalOffset;
            sizeF = currentPage.Graphics.ClientSize;
            num3 = Math.Min(sizeF.Height, val2);
            if ((double) num3 == (double) val2)
            {
              (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak = true;
              ++ShapeLayouter.index;
            }
            else if ((double) num2 + (double) num3 > (double) val2)
            {
              num3 = val2 - num2;
              ++ShapeLayouter.index;
              (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak = true;
            }
          }
          else
          {
            sizeF = currentPage.Graphics.ClientSize;
            num3 = Math.Min(sizeF.Height, shapeLayoutBounds.Height);
          }
          if (ShapeLayouter.index == length)
          {
            --ShapeLayouter.index;
            ShapeLayouter.last = true;
          }
          if (num1 == 0)
          {
            sizeF = currentPage.Graphics.ClientSize;
            double height3 = (double) sizeF.Height;
            RectangleF bounds = param.Bounds;
            double y1 = (double) bounds.Y;
            float num4 = Math.Min((float) (height3 - y1), num3);
            if ((double) num4 != (double) num3 && (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak)
            {
              --ShapeLayouter.index;
              (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak = false;
            }
            float num5 = (double) num4 < 0.0 ? -num4 : num4;
            ref RectangleF local = ref currentBounds;
            bounds = param.Bounds;
            double y2 = (double) bounds.Y;
            double height4 = (double) num5;
            local = new RectangleF(0.0f, (float) y2, 0.0f, (float) height4);
          }
          else if (ShapeLayouter.last)
          {
            currentBounds = RectangleF.Empty;
            (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak = false;
          }
          else
            currentBounds = new RectangleF(0.0f, 0.0f, 0.0f, num3);
          bool flag3 = this.RaiseBeforePageLayout(currentPage, ref currentBounds);
          EndPageLayoutEventArgs pageLayoutEventArgs = (EndPageLayoutEventArgs) null;
          if (!flag3)
          {
            pageResult = this.LayoutOnPage(currentPage, currentBounds, shapeLayoutBounds, pdfLayoutParams);
            pageLayoutEventArgs = this.RaiseEndPageLayout(pageResult);
            flag3 = pageLayoutEventArgs != null && pageLayoutEventArgs.Cancel;
            (pdfLayoutParams.Format as PdfMetafileLayoutFormat).IsHTMLPageBreak = false;
          }
          num2 += (double) pageResult.Bounds.Height > 0.0 ? pageResult.Bounds.Height : num3;
          ++num1;
          if (!pageResult.End && !flag3)
          {
            currentBounds = this.GetPaginateBounds(pdfLayoutParams);
            shapeLayoutBounds = this.GetNextShapeBounds(shapeLayoutBounds, pageResult);
            currentPage = pageLayoutEventArgs == null || pageLayoutEventArgs.NextPage == null ? this.GetNextPage(currentPage) : pageLayoutEventArgs.NextPage;
            if ((int) num2 == (int) verticalOffset)
            {
              num2 = 0.0f;
              break;
            }
          }
          else
          {
            pdfLayoutResult = this.GetLayoutResult(pageResult);
            flag1 = true;
            break;
          }
        }
        if (pageResult.End)
          break;
      }
    }
    (param.Format as PdfMetafileLayoutFormat).TrackHeight = num2;
    return pdfLayoutResult;
  }

  private ShapeLayouter.ShapeLayoutResult LayoutOnPage(
    PdfPage currentPage,
    RectangleF currentBounds,
    RectangleF shapeLayoutBounds,
    PdfLayoutParams param)
  {
    if (currentPage == null)
      throw new ArgumentNullException(nameof (currentPage));
    if (param == null)
      throw new ArgumentNullException(nameof (param));
    ShapeLayouter.ShapeLayoutResult shapeLayoutResult = new ShapeLayouter.ShapeLayoutResult();
    currentBounds = this.CheckCorrectCurrentBounds(currentPage, currentBounds, shapeLayoutBounds, param);
    if (this.Element is PdfImage && (double) (this.Element as PdfImage).ScrollBarWidth > 0.0)
    {
      if ((double) currentBounds.Width <= (double) (this.Element as PdfImage).PhysicalDimension.Width)
        currentBounds.Width -= (this.Element as PdfImage).ScrollBarWidth;
      else
        currentBounds.Width = (this.Element as PdfImage).PhysicalDimension.Width - (this.Element as PdfImage).ScrollBarWidth;
    }
    bool bounds = this.FitsToBounds(currentBounds, shapeLayoutBounds);
    int num = param.Format.Break != PdfLayoutBreakType.FitElement | bounds ? 1 : (currentPage != param.Page ? 1 : 0);
    bool flag1 = false;
    if (num != 0)
    {
      RectangleF drawBounds = this.GetDrawBounds(currentBounds, shapeLayoutBounds);
      if ((double) shapeLayoutBounds.Height <= (double) drawBounds.Bottom && this.Element is PdfImage && (double) (this.Element as PdfImage).ScrollBarHeight > 0.0)
        currentBounds.Height = shapeLayoutBounds.Height;
      if (this.Element is PdfMetafile && currentPage != null && currentPage.Section.ParentDocument is PdfDocument && currentPage.Section.ParentDocument.FileStructure.TaggedPdf)
        this.DrawShape(ref currentPage, currentBounds, drawBounds, true);
      else
        this.DrawShape(currentPage.Graphics, currentBounds, drawBounds);
      shapeLayoutResult.Bounds = this.GetPageResultBounds(currentBounds, shapeLayoutBounds);
      flag1 = (int) currentBounds.Height >= (int) shapeLayoutBounds.Height;
      if (this.Element is PdfMetafile && currentPage != null && currentPage.Section.ParentDocument is PdfDocument && currentPage.Section.ParentDocument.FileStructure.TaggedPdf)
      {
        bool flag2;
        if ((double) currentPage.Graphics.Split > 0.0)
        {
          shapeLayoutResult.End = false;
          flag2 = true;
          shapeLayoutResult.Page = currentPage;
          shapeLayoutResult.Bounds = currentBounds;
          shapeLayoutResult.Bounds.Height = Math.Min(currentBounds.Height, currentPage.Graphics.Split);
          currentPage.Graphics.Split = 0.0f;
          return shapeLayoutResult;
        }
        shapeLayoutResult.End = true;
        flag2 = true;
        shapeLayoutResult.Page = currentPage;
        shapeLayoutResult.Bounds = currentBounds;
        return shapeLayoutResult;
      }
    }
    shapeLayoutResult.End = flag1 || param.Format.Layout == PdfLayoutType.OnePage;
    shapeLayoutResult.Page = currentPage;
    return shapeLayoutResult;
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

  private EndPageLayoutEventArgs RaiseEndPageLayout(ShapeLayouter.ShapeLayoutResult pageResult)
  {
    EndPageLayoutEventArgs e = (EndPageLayoutEventArgs) null;
    if (this.Element.RaiseEndPageLayout)
    {
      e = new EndPageLayoutEventArgs(this.GetLayoutResult(pageResult));
      this.Element.OnEndPageLayout(e);
    }
    return e;
  }

  protected virtual float ToCorrectBounds(
    RectangleF currentBounds,
    RectangleF shapeLayoutBounds,
    PdfPage currentPage)
  {
    return currentBounds.Height;
  }

  public PdfShapeElement Element => base.Element as PdfShapeElement;

  private struct ShapeLayoutResult
  {
    public PdfPage Page;
    public RectangleF Bounds;
    public bool End;
  }
}
