// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.HtmlToPdf.HtmlToPdfResult
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Syncfusion.Pdf.HtmlToPdf
{
    public class HtmlToPdfResult : IDisposable
    {
      private ArrayList m_anchorsCollection;
      private bool m_Completed;
      private Stream m_docStream;
      private ArrayList m_documentLinkCollection;
      private float m_height;
      private Image[] m_images;
      private bool m_isImagePath;
      private PdfLayoutResult m_layoutResult;
      private PointF m_location;
      private float m_metafileTransparency;
      private ArrayList m_pageBreakCollection;
      private long m_quality;
      private float m_remHeight;
      private float m_scrollBarHeight;
      private float m_scrollBarWidth;
      private const int m_splitOffset = 4;

      public HtmlToPdfResult(Stream docStream)
      {
        this.m_quality = 100L;
        this.m_Completed = true;
        this.m_docStream = docStream;
      }

      public HtmlToPdfResult(
        Image[] image,
        ArrayList pageBreaks,
        ArrayList anchors,
        ArrayList documentLinks)
      {
        this.m_quality = 100L;
        this.m_Completed = true;
        this.m_images = image;
        this.m_anchorsCollection = anchors;
        this.m_pageBreakCollection = pageBreaks;
        this.m_documentLinkCollection = documentLinks;
      }

      internal HtmlToPdfResult(
        Image[] image,
        ArrayList pageBreaks,
        ArrayList anchors,
        ArrayList documentLinks,
        float remHeight)
        : this(image, pageBreaks, anchors, documentLinks)
      {
        this.m_remHeight = remHeight;
      }

      private PdfLayoutResult DrawBitmap(
        Bitmap bitmap,
        PdfPageBase page,
        RectangleF bounds,
        PdfLayoutFormat format)
      {
        PdfBitmap pdfBitmap = new PdfBitmap((Image) bitmap);
        pdfBitmap.ScrollBarWidth = this.ScrollBarWidth;
        pdfBitmap.ScrollBarHeight = this.ScrollBarHeight;
        format = format == null ? new PdfLayoutFormat() : format;
        return pdfBitmap.Draw((PdfPage) page, bounds.Location, format);
      }

      private PdfLayoutResult DrawMetaFile(
        Metafile metafile,
        PdfPageBase page,
        RectangleF bounds,
        PdfLayoutFormat format,
        long quality)
      {
        PdfMetafile pdfMetafile = new PdfMetafile(metafile);
        pdfMetafile.ScrollBarWidth = this.ScrollBarWidth;
        pdfMetafile.ScrollBarHeight = this.ScrollBarHeight;
        pdfMetafile.Quality = quality;
        if ((double) this.m_metafileTransparency > 0.0)
          pdfMetafile.SetTransparency(this.m_metafileTransparency, this.m_metafileTransparency, PdfBlendMode.Normal, true);
        PdfMetafileLayoutFormat format1 = format is PdfMetafileLayoutFormat ? format as PdfMetafileLayoutFormat : new PdfMetafileLayoutFormat();
        float[] pageOffsets = new float[this.m_pageBreakCollection.Count];
        format = format == null ? (PdfLayoutFormat) new PdfMetafileLayoutFormat() : format;
        this.m_pageBreakCollection.CopyTo((Array) pageOffsets);
        pdfMetafile.HtmlHyperlinksCollection = this.m_anchorsCollection;
        pdfMetafile.DocumentLinksCollection = this.m_documentLinkCollection;
        pdfMetafile.IsImagePath = this.IsImagePath;
        return pdfMetafile.Draw((PdfPage) page, bounds, pageOffsets, (PdfLayoutFormat) format1);
      }

      public void Render(PdfDocument document)
      {
        PdfLoadedDocument ldDoc = new PdfLoadedDocument(this.m_docStream);
        document.ImportPageRange(ldDoc, 0, ldDoc.Pages.Count - 1);
        document.Pages.Remove(document.Pages[0]);
      }

      public void Render(PdfPageBase page, PdfLayoutFormat format)
      {
        if (page == null)
          throw new PdfException("Page cannot be null.");
        if (this.m_images == null)
          throw new PdfException("Image cannot be null.");
        format = format == null ? new PdfLayoutFormat() : format;
        ArrayList pageBreakCollection = this.m_pageBreakCollection;
        PdfLayoutResult pdfLayoutResult = (PdfLayoutResult) null;
        foreach (Image image in this.m_images)
        {
          if (pdfLayoutResult != null && (double) pdfLayoutResult.Bounds.Size.Height <= (double) page.Size.Height)
          {
            page = (PdfPageBase) pdfLayoutResult.Page;
            pdfLayoutResult = image is Metafile ? this.DrawMetaFile((Metafile) image, page, new RectangleF(0.0f, pdfLayoutResult.Bounds.Size.Height - 4f, page.Size.Width, 0.0f), format, this.m_quality) : this.DrawBitmap((Bitmap) image, page, new RectangleF(0.0f, pdfLayoutResult.Bounds.Size.Height - 4f, page.Size.Width, 0.0f), format);
          }
          else if (image is Metafile && pdfLayoutResult == null)
          {
            PdfGraphicsState state = (PdfGraphicsState) null;
            if (page is PdfPage && (page as PdfPage).Document.FileStructure.TaggedPdf)
            {
              state = page.Graphics.Save();
              page.Graphics.ScaleTransform(0.75f, 0.75f);
            }
            pdfLayoutResult = this.DrawMetaFile((Metafile) image, page, RectangleF.Empty, format, this.m_quality);
            if (page is PdfPage && (page as PdfPage).Document.FileStructure.TaggedPdf && state != null)
            {
              page.Graphics.ScaleTransform(1f, 1f);
              page.Graphics.Restore(state);
            }
          }
          else
            pdfLayoutResult = this.DrawBitmap((Bitmap) image, page, RectangleF.Empty, format);
        }
        if (page is PdfPage && (page as PdfPage).Section.ParentDocument is PdfDocument && (page as PdfPage).Section.ParentDocument.FileStructure.TaggedPdf)
        {
          if ((double) this.m_remHeight > 0.0)
          {
            this.m_height = pdfLayoutResult.Bounds.Height;
            this.m_Completed = false;
          }
          else
            this.m_Completed = true;
          this.m_layoutResult = pdfLayoutResult;
        }
        Dictionary<int, List<PdfUriAnnotation>> dictionary = new Dictionary<int, List<PdfUriAnnotation>>();
        PdfDocument document = (page as PdfPage).Document;
        if (document != null && this.m_documentLinkCollection != null && this.m_documentLinkCollection.Count > 0)
        {
          foreach (PdfPage page1 in document.Pages)
          {
            for (int index1 = 0; index1 < page1.Annotations.Count; ++index1)
            {
              if (page1.Annotations[index1] is PdfDocumentLinkAnnotation)
              {
                PdfDocumentLinkAnnotation annotation1 = page1.Annotations[index1] as PdfDocumentLinkAnnotation;
                if (annotation1.Destination == null)
                {
                  float num = 0.0f;
                  for (int index2 = 0; index2 < document.Pages.Count && annotation1.Destination == null; ++index2)
                  {
                    PdfPage page2 = document.Pages[index2];
                    for (int index3 = page2.Annotations.Count - 1; index3 >= 0; --index3)
                    {
                      foreach (KeyValuePair<int, List<PdfUriAnnotation>> keyValuePair in dictionary)
                      {
                        bool flag = false;
                        if (keyValuePair.Key == index3)
                        {
                          foreach (PdfUriAnnotation pdfUriAnnotation in keyValuePair.Value)
                          {
                            if (pdfUriAnnotation.Text == annotation1.Text)
                            {
                              PdfDestination pdfDestination = new PdfDestination((PdfPageBase) pdfUriAnnotation.Page, pdfUriAnnotation.Location);
                              annotation1.Destination = pdfDestination;
                              flag = true;
                              break;
                            }
                          }
                          if (flag)
                            break;
                        }
                        else if (flag)
                          break;
                      }
                      if (page2.Annotations[index3] is PdfUriAnnotation)
                      {
                        PdfUriAnnotation annotation2 = page2.Annotations[index3] as PdfUriAnnotation;
                        if (annotation2.Text == annotation1.Text)
                        {
                          PointF location = annotation2.Location;
                          if ((double) location.Y > (double) num)
                            location.Y -= num;
                          PdfDestination pdfDestination = new PdfDestination((PdfPageBase) page2, location);
                          annotation1.Destination = pdfDestination;
                          if (!dictionary.ContainsKey(index2))
                            dictionary.Add(index2, new List<PdfUriAnnotation>());
                          dictionary[index2].Add(annotation2);
                          page2.Annotations.RemoveAt(index3);
                          break;
                        }
                      }
                    }
                    num += page2.Graphics.ClientSize.Height;
                  }
                }
              }
            }
          }
        }
        dictionary.Clear();
      }

      public void Render(PdfPageBase page, PdfLayoutFormat format, out PdfLayoutResult result)
      {
        if (page == null)
          throw new PdfException("Page cannot be null.");
        if (this.m_images == null)
          throw new PdfException("Image cannot be null.");
        format = format == null ? new PdfLayoutFormat() : format;
        ArrayList pageBreakCollection = this.m_pageBreakCollection;
        result = (PdfLayoutResult) null;
        foreach (Image image in this.m_images)
        {
          RectangleF bounds1;
          SizeF size;
          if (result != null)
          {
            bounds1 = result.Bounds;
            size = bounds1.Size;
            double height1 = (double) size.Height;
            size = page.Size;
            double height2 = (double) size.Height;
            if (height1 <= height2)
            {
              page = (PdfPageBase) result.Page;
              ref PdfLayoutResult local = ref result;
              PdfLayoutResult pdfLayoutResult;
              if (!(image is Metafile))
              {
                Bitmap bitmap = (Bitmap) image;
                PdfPageBase page1 = page;
                bounds1 = result.Bounds;
                size = bounds1.Size;
                double height3 = (double) size.Height;
                size = page.Size;
                double width = (double) size.Width;
                RectangleF bounds2 = new RectangleF(0.0f, (float) height3, (float) width, 0.0f);
                PdfLayoutFormat format1 = format;
                pdfLayoutResult = this.DrawBitmap(bitmap, page1, bounds2, format1);
              }
              else
              {
                Metafile metafile = (Metafile) image;
                PdfPageBase page2 = page;
                bounds1 = result.Bounds;
                size = bounds1.Size;
                double height4 = (double) size.Height;
                size = page.Size;
                double width = (double) size.Width;
                RectangleF bounds3 = new RectangleF(0.0f, (float) height4, (float) width, 0.0f);
                PdfLayoutFormat format2 = format;
                long quality = this.m_quality;
                pdfLayoutResult = this.DrawMetaFile(metafile, page2, bounds3, format2, quality);
              }
              local = pdfLayoutResult;
              continue;
            }
          }
          if (result == null)
          {
            PointF location1 = this.Location;
            if (image is Metafile)
            {
              ref PdfLayoutResult local1 = ref result;
              Metafile metafile = (Metafile) image;
              PdfPageBase page3 = page;
              PointF location2 = this.Location;
              double x1 = (double) location2.X;
              location2 = this.Location;
              double y1 = (double) location2.Y;
              size = page.Size;
              double width1 = (double) size.Width;
              RectangleF bounds4 = new RectangleF((float) x1, (float) y1, (float) width1, 0.0f);
              PdfLayoutFormat format3 = format;
              long quality = this.m_quality;
              PdfLayoutResult pdfLayoutResult1 = this.DrawMetaFile(metafile, page3, bounds4, format3, quality);
              local1 = pdfLayoutResult1;
              if (page == result.Page)
              {
                ref PdfLayoutResult local2 = ref result;
                PdfPage page4 = result.Page;
                bounds1 = result.Bounds;
                double x2 = (double) bounds1.X;
                bounds1 = result.Bounds;
                double y2 = (double) bounds1.Y;
                bounds1 = result.Bounds;
                double width2 = (double) bounds1.Width;
                bounds1 = result.Bounds;
                double height5 = (double) bounds1.Height;
                location2 = this.Location;
                double y3 = (double) location2.Y;
                double height6 = height5 + y3;
                RectangleF bounds5 = new RectangleF((float) x2, (float) y2, (float) width2, (float) height6);
                PdfLayoutResult pdfLayoutResult2 = new PdfLayoutResult(page4, bounds5);
                local2 = pdfLayoutResult2;
                continue;
              }
              continue;
            }
          }
          result = !(image is Metafile) || result != null ? this.DrawBitmap((Bitmap) image, page, RectangleF.Empty, format) : this.DrawMetaFile((Metafile) image, page, RectangleF.Empty, format, this.m_quality);
        }
      }

      void IDisposable.Dispose()
      {
        if (this.m_images != null)
        {
          for (int index = 0; index < this.m_images.Length; ++index)
            this.m_images[index].Dispose();
        }
        this.m_images = (Image[]) null;
        this.m_pageBreakCollection = (ArrayList) null;
      }

      internal ArrayList AnchorsCollection => this.m_anchorsCollection;

      internal bool Completed => this.m_Completed;

      internal float Height => this.m_height;

      public Image[] Images => this.m_images;

      internal bool IsImagePath
      {
        get => this.m_isImagePath;
        set => this.m_isImagePath = value;
      }

      internal PdfLayoutResult LayoutResult => this.m_layoutResult;

      public PointF Location
      {
        get => this.m_location;
        set => this.m_location = value;
      }

      public float MetafileTransparency
      {
        get => this.m_metafileTransparency;
        set
        {
          this.m_metafileTransparency = (double) value > 0.0 && (double) value <= 1.0 ? value : throw new PdfException("Value can only be greater than 0 and less than or equal to 1");
        }
      }

      internal ArrayList PageBreakCollection => this.m_pageBreakCollection;

      public long Quality
      {
        set => this.m_quality = value;
      }

      public Image RenderedImage => this.m_images[0];

      internal float ScrollBarHeight
      {
        get => this.m_scrollBarHeight;
        set => this.m_scrollBarHeight = value;
      }

      internal float ScrollBarWidth
      {
        get => this.m_scrollBarWidth;
        set => this.m_scrollBarWidth = value;
      }
    }
}
