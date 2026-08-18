// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfMetafile
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Images.Metafiles;
using Syncfusion.Pdf.HtmlToPdf;
using Syncfusion.Pdf.Native;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfMetafile : PdfImage, IPdfWrapper, IDisposable
    {
      private float m_alphaBrush;
      private float m_alphaPen;
      private bool m_bDisposed;
      private bool m_bIsTransparency;
      private PdfBlendMode m_blendMode;
      private bool m_bSaved;
      private ArrayList m_documentLinks;
      private ArrayList m_htmlHyperlinks;
      private Metafile m_image;
      private ImageRegionManager m_imageRegions;
      private int m_imageResolution;
      private bool m_isImagePath;
      private Metafile m_originalImage;
      private float m_pageScale;
      private GraphicsUnit m_pageUnit;
      private long m_quality;
      private PdfTemplate m_template;
      private TextRegionManager m_textRegions;

      public PdfMetafile(Metafile metafile)
      {
        this.m_quality = 100L;
        this.m_htmlHyperlinks = new ArrayList();
        this.m_documentLinks = new ArrayList();
        this.m_pageScale = 1f;
        this.m_pageUnit = GraphicsUnit.Display;
        this.m_alphaPen = 1f;
        this.m_alphaBrush = 1f;
        this.m_image = metafile != null ? PdfMetafile.AdjustMetafile(metafile) : throw new ArgumentNullException(nameof (metafile));
        this.m_originalImage = metafile;
        this.SetResolution(PdfUnitConvertor.HorizontalResolution, PdfUnitConvertor.VerticalResolution);
        this.m_template = new PdfTemplate((float) this.m_image.Width, (float) this.m_image.Height);
        this.SetContent(((IPdfWrapper) this.m_template).Element);
      }

      public PdfMetafile(System.IO.Stream stream)
        : this(Image.FromStream(PdfImage.CheckStreamExistance(stream)) as Metafile)
      {
      }

      public PdfMetafile(string path)
        : this(new Metafile(Utils.CheckFilePath(path)))
      {
      }

      internal static Metafile AdjustMetafile(Metafile metafile)
      {
        Metafile metafile1 = metafile != null ? metafile : throw new ArgumentNullException(nameof (metafile));
        if (!metafile.GetMetafileHeader().IsEmfOrEmfPlus())
        {
          metafile1 = PdfMetafile.ConvertToEmf(metafile);
          if (metafile1 == null)
            throw new ArgumentException("Can't parse metafile. Format is unknown.");
        }
        return metafile1;
      }

      private static Metafile ConvertToEmf(Metafile image)
      {
        MetafileHeader metafileHeader = image != null ? image.GetMetafileHeader() : throw new ArgumentNullException(nameof (image));
        Metafile emf = (Metafile) null;
        if (!metafileHeader.IsEmfOrEmfPlus())
        {
          image = (Metafile) image.Clone();
          SizeF physicalDimension = image.PhysicalDimension;
          IntPtr henhmetafile1 = image.GetHenhmetafile();
          int metaFileBitsEx = GdiApi.GetMetaFileBitsEx(henhmetafile1, 0, (byte[]) null);
          if (metaFileBitsEx > 0)
          {
            byte[] numArray = new byte[metaFileBitsEx];
            if (GdiApi.GetMetaFileBitsEx(henhmetafile1, metaFileBitsEx, numArray) > 0)
            {
              IntPtr zero = IntPtr.Zero;
              IntPtr dc = GdiApi.CreateDC("DISPLAY", (string) null, (string) null, IntPtr.Zero);
              float num1 = (float) ((double) PdfUnitConvertor.PxHorizontalResolution / (double) PdfUnitConvertor.HorizontalSize * 25.399999618530273);
              float num2 = (float) ((double) PdfUnitConvertor.PxVerticalResolution / (double) PdfUnitConvertor.VerticalSize * 25.399999618530273);
              float num3 = PdfUnitConvertor.HorizontalResolution / num1;
              float num4 = PdfUnitConvertor.VerticalResolution / num2;
              IntPtr henhmetafile2 = GdiApi.SetWinMetaFileBits(metaFileBitsEx, numArray, dc, ref new METAFILEPICT()
              {
                xExt = (int) ((double) physicalDimension.Width * (double) num3),
                yExt = (int) ((double) physicalDimension.Height * (double) num4),
                mm = 8
              });
              if (henhmetafile2 != IntPtr.Zero)
                emf = new Metafile(henhmetafile2, true);
              GdiApi.DeleteDC(dc);
            }
          }
          GdiApi.DeleteEnhMetaFile(henhmetafile1);
          image.Dispose();
          return emf;
        }
        if (metafileHeader.Type == MetafileType.EmfPlusDual)
        {
          Rectangle frameRect = new Rectangle(0, 0, image.Width, image.Height);
          System.Drawing.Graphics graphics1 = System.Drawing.Graphics.FromImage((Image) new Bitmap(1, 1));
          IntPtr hdc = graphics1.GetHdc();
          MemoryStream memoryStream = new MemoryStream();
          emf = new Metafile((System.IO.Stream) memoryStream, hdc, frameRect, MetafileFrameUnit.Pixel, EmfType.EmfOnly);
          graphics1.Dispose();
          System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage((Image) emf);
          Rectangle rectangle = frameRect;
          graphics2.DrawImage((Image) image, rectangle, rectangle, GraphicsUnit.Pixel);
          graphics2.Dispose();
          memoryStream.Dispose();
        }
        return emf;
      }

      public void Dispose()
      {
        this.Dispose(true);
        GC.SuppressFinalize((object) this);
      }

      private void Dispose(bool disposing)
      {
        if (this.m_bDisposed)
          return;
        if (disposing)
        {
          if (this.m_originalImage != null)
          {
            this.m_originalImage.Dispose();
            this.m_originalImage = (Metafile) null;
          }
          if (this.m_image != null)
          {
            this.m_image.Dispose();
            this.m_image = (Metafile) null;
          }
        }
        else if (this.m_originalImage != null && this.m_image != null && this.m_image != this.m_originalImage)
        {
          this.m_image.Dispose();
          this.m_image = (Metafile) null;
        }
        this.m_bDisposed = true;
      }

      ~PdfMetafile() => this.Dispose(false);

      protected override PdfLayoutResult Layout(PdfLayoutParams param)
      {
        this.m_template.Graphics.ColorSpace = param.Page.Document.ColorSpace;
        if (param == null)
          throw new ArgumentNullException(nameof (param));
        if (!param.Page.Document.FileStructure.TaggedPdf)
          this.Save();
        MetafileLayouter metafileLayouter = new MetafileLayouter(this);
        metafileLayouter.IsImagePath = this.IsImagePath;
        return metafileLayouter.Layout(param);
      }

      protected override PdfLayoutResult Layout(HtmlToPdfLayoutParams param)
      {
        if (param == null)
          throw new ArgumentNullException(nameof (param));
        if (!param.Page.Document.FileStructure.TaggedPdf)
          this.Save();
        return new MetafileLayouter(this).Layout(param);
      }

      internal override void Save()
      {
        if (this.m_bSaved)
          return;
        PdfEmfRenderer renderer = this.ImageResolution <= 0 ? new PdfEmfRenderer(this.m_template.Graphics, this.m_quality, this.EmbedFontResource) : new PdfEmfRenderer(this.m_template.Graphics, this.m_imageResolution, this.EmbedFontResource);
        using (Metafile metaFile = this.m_image.Clone() as Metafile)
        {
          using (MetaRecordParser metaRecordParser = new MetaRecordParser(renderer, metaFile))
          {
            if (this.IsTranparency)
            {
              renderer.AlphaBrush = this.AlphaBrush;
              renderer.AlphaPen = this.AlphaPen;
              renderer.BlendMode = this.BlendMode;
              renderer.IsTranparency = this.IsTranparency;
            }
            metaRecordParser.Parser.PageScale = this.m_pageScale;
            metaRecordParser.Parser.PageUnit = this.m_pageUnit;
            metaRecordParser.Enumerate();
            this.m_textRegions = metaRecordParser.Context as TextRegionManager;
            this.m_imageRegions = metaRecordParser.ImageContext as ImageRegionManager;
          }
        }
        this.m_bSaved = true;
      }

      public void SetTransparency(
        float alphaPen,
        float alphaBrush,
        PdfBlendMode blendMode,
        bool transparency)
      {
        this.m_alphaBrush = alphaBrush;
        this.m_alphaPen = alphaPen;
        this.m_blendMode = blendMode;
        this.m_bIsTransparency = transparency;
      }

      internal float AlphaBrush
      {
        get => this.m_alphaBrush;
        set => this.m_alphaBrush = value;
      }

      internal float AlphaPen
      {
        get => this.m_alphaPen;
        set => this.m_alphaPen = value;
      }

      internal PdfBlendMode BlendMode
      {
        get => this.m_blendMode;
        set => this.m_blendMode = value;
      }

      internal ArrayList DocumentLinksCollection
      {
        get => this.m_documentLinks;
        set => this.m_documentLinks = value;
      }

      internal ArrayList HtmlHyperlinksCollection
      {
        get => this.m_htmlHyperlinks;
        set => this.m_htmlHyperlinks = value;
      }

      internal ImageRegionManager ImageRegions => this.m_imageRegions;

      public int ImageResolution
      {
        get => this.m_imageResolution;
        set => this.m_imageResolution = value;
      }

      internal override Image InternalImage => (Image) this.m_image;

      internal bool IsImagePath
      {
        get => this.m_isImagePath;
        set => this.m_isImagePath = value;
      }

      internal bool IsTranparency
      {
        get => this.m_bIsTransparency;
        set => this.m_bIsTransparency = value;
      }

      public float PageScale
      {
        get => this.m_pageScale;
        set => this.m_pageScale = value;
      }

      public GraphicsUnit PageUnit
      {
        get => this.m_pageUnit;
        set => this.m_pageUnit = value;
      }

      public long Quality
      {
        get => this.m_quality;
        set => this.m_quality = value;
      }

      internal PdfTemplate Template => this.m_template;

      internal TextRegionManager TextRegions => this.m_textRegions;
    }
}
