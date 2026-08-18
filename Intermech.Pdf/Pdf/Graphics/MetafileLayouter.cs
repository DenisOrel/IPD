// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.MetafileLayouter
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Images.Metafiles;
using Syncfusion.Pdf.HtmlToPdf;
using Syncfusion.Pdf.Interactive;
using System;
using System.Collections;
using System.Drawing;


namespace Syncfusion.Pdf.Graphics
{
    internal class MetafileLayouter(PdfMetafile element) : ShapeLayouter((PdfShapeElement) element)
    {
      protected override RectangleF CheckCorrectCurrentBounds(
        PdfPage currentPage,
        RectangleF currentBounds,
        RectangleF shapeLayoutBounds,
        PdfLayoutParams param)
      {
        if (param == null)
          throw new ArgumentNullException(nameof (param));
        RectangleF rectangleF = base.CheckCorrectCurrentBounds(currentPage, currentBounds, shapeLayoutBounds, param);
        bool flag1 = !(param.Format is PdfMetafileLayoutFormat format) || format.SplitTextLines;
        bool flag2 = format != null && format.SplitImages;
        bool flag3 = format != null && format.IsHTMLPageBreak;
        if (!this.IsImagePath)
        {
          SizeF sizeF;
          if (this.TextRegions != null && !flag1 && !flag3)
          {
            float num1 = shapeLayoutBounds.Y + rectangleF.Height;
            PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor(this.Element.VerticalResolution);
            float topCoordinate1 = this.TextRegions.GetTopCoordinate(pdfUnitConvertor.ConvertToPixels(num1, PdfGraphicsUnit.Point));
            double height1 = (double) rectangleF.Height;
            sizeF = currentPage.GetClientSize();
            double height2 = (double) sizeF.Height;
            if (height1 > height2)
              topCoordinate1 = this.TextRegions.GetTopCoordinate(topCoordinate1 - 2f);
            float num2 = pdfUnitConvertor.ConvertFromPixels(topCoordinate1, PdfGraphicsUnit.Point);
            float num3 = 0.0f;
            if ((double) num2 > (double) shapeLayoutBounds.Y)
              num3 = num2 - shapeLayoutBounds.Y;
            ref RectangleF local1 = ref rectangleF;
            double num4;
            if (currentPage != null)
            {
              sizeF = currentPage.GetClientSize();
              if ((double) sizeF.Height < (double) num3)
              {
                sizeF = currentPage.GetClientSize();
                num4 = (double) sizeF.Height;
                goto label_12;
              }
            }
            num4 = (double) num3;
    label_12:
            local1.Height = (float) num4;
            if ((double) rectangleF.Y != 0.0)
            {
              float num5 = rectangleF.Y + num3;
              double num6 = (double) num5;
              sizeF = currentPage.GetClientSize();
              double height3 = (double) sizeF.Height;
              if (num6 > height3)
              {
                sizeF = currentPage.GetClientSize();
                float height4 = sizeF.Height;
                float num7 = num5 - height4;
                rectangleF.Height = num3 - num7;
                float num8 = shapeLayoutBounds.Y + rectangleF.Height;
                float topCoordinate2 = this.TextRegions.GetTopCoordinate(pdfUnitConvertor.ConvertToPixels(num8, PdfGraphicsUnit.Point));
                float num9 = pdfUnitConvertor.ConvertFromPixels(topCoordinate2, PdfGraphicsUnit.Point);
                if ((double) num9 > (double) shapeLayoutBounds.Y)
                  num3 = num9 - shapeLayoutBounds.Y;
                ref RectangleF local2 = ref rectangleF;
                double num10;
                if (currentPage != null)
                {
                  sizeF = currentPage.GetClientSize();
                  if ((double) sizeF.Height < (double) num3)
                  {
                    sizeF = currentPage.GetClientSize();
                    num10 = (double) sizeF.Height;
                    goto label_20;
                  }
                }
                num10 = (double) num3;
    label_20:
                local2.Height = (float) num10;
              }
            }
          }
          if (this.ImageRegions != null && !flag2 && !flag3)
          {
            float height5 = rectangleF.Height;
            float num11 = shapeLayoutBounds.Y + rectangleF.Height;
            PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor(this.Element.VerticalResolution);
            float topCoordinate3 = this.ImageRegions.GetTopCoordinate(pdfUnitConvertor.ConvertToPixels(num11, PdfGraphicsUnit.Point));
            float num12 = pdfUnitConvertor.ConvertFromPixels(topCoordinate3, PdfGraphicsUnit.Point);
            if (Math.Round((double) num12) != Math.Round((double) shapeLayoutBounds.Y + (double) rectangleF.Height))
              num12 = (float) Math.Floor((double) num12);
            float num13 = 0.0f;
            if ((double) num12 > (double) shapeLayoutBounds.Y)
              num13 = num12 - shapeLayoutBounds.Y;
            if ((double) num13 == 0.0 || this.TextRegions.Count == 0)
            {
              rectangleF.Height = height5;
            }
            else
            {
              PdfPage page = param.Page;
              double height6 = (double) shapeLayoutBounds.Height;
              sizeF = page.Size;
              double height7 = (double) sizeF.Height;
              if (height6 > height7)
                rectangleF.Height = num13;
              if (this.TextRegions != null && !flag1)
              {
                float num14 = shapeLayoutBounds.Y + rectangleF.Height;
                float topCoordinate4 = this.TextRegions.GetTopCoordinate(pdfUnitConvertor.ConvertToPixels(num14, PdfGraphicsUnit.Point));
                float num15 = pdfUnitConvertor.ConvertFromPixels(topCoordinate4, PdfGraphicsUnit.Point);
                if ((double) num15 > (double) shapeLayoutBounds.Y)
                  num13 = num15 - shapeLayoutBounds.Y;
                ref RectangleF local = ref rectangleF;
                double num16;
                if (currentPage != null)
                {
                  sizeF = currentPage.GetClientSize();
                  if ((double) sizeF.Height < (double) num13)
                  {
                    sizeF = currentPage.GetClientSize();
                    num16 = (double) sizeF.Height;
                    goto label_37;
                  }
                }
                num16 = (double) num13;
    label_37:
                local.Height = (float) num16;
                if ((double) rectangleF.Height == 0.0)
                {
                  sizeF = currentPage.GetClientSize();
                  if ((double) sizeF.Height > (double) num13)
                    rectangleF.Height = height5;
                }
              }
              else
                rectangleF.Height = num13;
            }
          }
        }
        ArrayList list = new ArrayList();
        foreach (HtmlHyperLink htmlHyperlinks in this.Element.HtmlHyperlinksCollection)
        {
          if ((double) rectangleF.Height > (double) htmlHyperlinks.Bounds.Y)
          {
            if (string.IsNullOrEmpty(htmlHyperlinks.Hash))
            {
              PdfUriAnnotation annotation = new PdfUriAnnotation(htmlHyperlinks.Bounds, htmlHyperlinks.Href);
              annotation.Border.Width = 0.0f;
              currentPage.Annotations.Add((PdfAnnotation) annotation);
            }
            else
            {
              PdfDocumentLinkAnnotation annotation = new PdfDocumentLinkAnnotation(htmlHyperlinks.Bounds);
              annotation.Border.Width = 0.0f;
              annotation.ApplyText(htmlHyperlinks.Hash);
              currentPage.Annotations.Add((PdfAnnotation) annotation);
            }
            list.Add((object) htmlHyperlinks);
          }
        }
        foreach (HtmlHyperLink documentLinks in this.Element.DocumentLinksCollection)
        {
          float height = rectangleF.Height;
          double y1 = (double) shapeLayoutBounds.Y;
          RectangleF bounds1 = documentLinks.Bounds;
          double y2 = (double) bounds1.Y;
          if (y1 < y2)
          {
            double num = (double) height + (double) shapeLayoutBounds.Y;
            bounds1 = documentLinks.Bounds;
            double y3 = (double) bounds1.Y;
            if (num > y3)
            {
              RectangleF bounds2 = documentLinks.Bounds;
              bounds2.Y -= shapeLayoutBounds.Y;
              PdfUriAnnotation annotation = new PdfUriAnnotation(bounds2);
              annotation.ApplyText(documentLinks.Name);
              currentPage.Annotations.Add((PdfAnnotation) annotation);
              list.Add((object) documentLinks);
            }
          }
        }
        this.RepositionLinks(list, rectangleF.Height);
        return rectangleF;
      }

      internal void RepositionLinks(ArrayList list, float height)
      {
        foreach (HtmlHyperLink htmlHyperLink in list)
          this.Element.HtmlHyperlinksCollection.Remove((object) htmlHyperLink);
        list.Clear();
        list = this.Element.HtmlHyperlinksCollection.Clone() as ArrayList;
        this.Element.HtmlHyperlinksCollection.Clear();
        foreach (HtmlHyperLink htmlHyperLink1 in list)
        {
          RectangleF bounds = htmlHyperLink1.Bounds;
          float num = bounds.Y - height;
          HtmlHyperLink htmlHyperLink2 = htmlHyperLink1;
          bounds = htmlHyperLink1.Bounds;
          double x = (double) bounds.X;
          double y = (double) num;
          bounds = htmlHyperLink1.Bounds;
          double width = (double) bounds.Width;
          bounds = htmlHyperLink1.Bounds;
          double height1 = (double) bounds.Height;
          RectangleF rectangleF = new RectangleF((float) x, (float) y, (float) width, (float) height1);
          htmlHyperLink2.Bounds = rectangleF;
          this.Element.HtmlHyperlinksCollection.Add((object) htmlHyperLink1);
        }
      }

      protected override float ToCorrectBounds(
        RectangleF currentBounds,
        RectangleF shapeLayoutBounds,
        PdfPage currentPage)
      {
        RectangleF rectangleF = currentBounds;
        int num1 = 0;
        bool flag1 = false;
        do
        {
          for (int height = (int) rectangleF.Height; height > 0; --height)
          {
            float num2 = shapeLayoutBounds.Y + (float) height;
            PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor(this.Element.VerticalResolution);
            float num3 = pdfUnitConvertor.ConvertFromPixels(this.TextRegions.GetCoordinate(pdfUnitConvertor.ConvertToPixels(num2, PdfGraphicsUnit.Point)), PdfGraphicsUnit.Point);
            bool flag2 = (double) num3 != 0.0;
            float num4 = pdfUnitConvertor.ConvertFromPixels(this.ImageRegions.GetCoordinate(pdfUnitConvertor.ConvertToPixels(num3, PdfGraphicsUnit.Point)), PdfGraphicsUnit.Point);
            bool flag3 = (double) num4 != 0.0;
            if (flag2 | flag3)
            {
              float num5 = num4 - shapeLayoutBounds.Y;
              rectangleF.Height = num5 - 1f;
              ++num1;
              break;
            }
          }
        }
        while (flag1 && num1 < 2);
        return rectangleF.Height;
      }

      public PdfMetafile Element => base.Element as PdfMetafile;

      private ImageRegionManager ImageRegions => this.Element.ImageRegions;

      private TextRegionManager TextRegions => this.Element.TextRegions;
    }
}
