// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfImage
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Images;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.Drawing.Imaging;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public abstract class PdfImage : PdfShapeElement, IPdfWrapper
{
  private float m_horizontalResolution;
  private int[] m_matte;
  private SizeF m_phisicalDimension;
  private float m_scrollBarHeight;
  private float m_scrollBarWidth;
  protected bool m_softmask;
  private PdfStream m_stream;
  internal static string m_tiffPath;
  internal static System.IO.Stream m_tiffStream;
  private float m_verticalResolution;

  internal static System.IO.Stream CheckStreamExistance(System.IO.Stream stream)
  {
    if (stream == null)
      throw new ArgumentNullException(nameof (stream));
    return stream.Length > 0L ? stream : throw new ArgumentException("The stream can't be empty", nameof (stream));
  }

  protected override void DrawInternal(PdfGraphics graphics)
  {
    if (graphics == null)
      throw new ArgumentNullException(nameof (graphics));
    graphics.DrawImage(this, PointF.Empty);
  }

  public static PdfImage FromFile(string path)
  {
    Image image = path != null ? Image.FromFile(Utils.CheckFilePath(path)) : throw new ArgumentNullException(nameof (path));
    PdfImage pdfImage = PdfImage.FromImage(image);
    if (image.RawFormat.Equals((object) ImageFormat.Tiff))
      PdfImage.m_tiffPath = path;
    return pdfImage;
  }

  public static PdfImage FromImage(Image image)
  {
    if (image == null)
      throw new ArgumentNullException(nameof (image));
    return image is Metafile ? (PdfImage) new PdfMetafile(image as Metafile) : (PdfImage) new PdfBitmap(image);
  }

  public static PdfImage FromRtf(string rtf, float width, PdfImageType type)
  {
    return PdfImage.FromRtf(rtf, width, 0.0f, type);
  }

  public static PdfImage FromRtf(string rtf, float width, float height, PdfImageType type)
  {
    if (rtf == null)
      throw new ArgumentNullException(nameof (rtf));
    SizeF pixelSize = PdfImage.GetPixelSize(width, height);
    PdfImage pdfImage = PdfImage.FromImage(RtfToImage.ConvertToImage(rtf, pixelSize.Width, pixelSize.Height, type) ?? throw new PdfException("Couldn't convert RTF to Image"));
    pdfImage.SetResolution(PdfUnitConvertor.HorizontalResolution, PdfUnitConvertor.VerticalResolution);
    return pdfImage;
  }

  public static PdfImage FromStream(System.IO.Stream stream)
  {
    Image image = stream != null ? Image.FromStream(PdfImage.CheckStreamExistance(stream)) : throw new ArgumentNullException(nameof (stream));
    PdfImage pdfImage = PdfImage.FromImage(image);
    if (image.RawFormat.Equals((object) ImageFormat.Tiff))
      PdfImage.m_tiffStream = stream;
    return pdfImage;
  }

  protected override RectangleF GetBoundsInternal()
  {
    return new RectangleF(PointF.Empty, this.PhysicalDimension);
  }

  protected static SizeF GetPixelSize(float width, float height)
  {
    double horizontalResolution = (double) PdfUnitConvertor.HorizontalResolution;
    float verticalResolution = PdfUnitConvertor.VerticalResolution;
    return new SizeF(new PdfUnitConvertor((float) horizontalResolution).ConvertToPixels(width, PdfGraphicsUnit.Point), new PdfUnitConvertor(verticalResolution).ConvertToPixels(height, PdfGraphicsUnit.Point));
  }

  protected internal SizeF GetPointSize(float width, float height)
  {
    float horizontalResolution = PdfUnitConvertor.HorizontalResolution;
    float verticalResolution = PdfUnitConvertor.VerticalResolution;
    return this.GetPointSize(width, height, horizontalResolution, verticalResolution);
  }

  protected internal SizeF GetPointSize(
    float width,
    float height,
    float horizontalResolution,
    float verticalResolution)
  {
    PdfUnitConvertor pdfUnitConvertor1 = new PdfUnitConvertor(horizontalResolution);
    PdfUnitConvertor pdfUnitConvertor2 = new PdfUnitConvertor(verticalResolution);
    double num = (double) width;
    return new SizeF(pdfUnitConvertor1.ConvertUnits((float) num, PdfGraphicsUnit.Pixel, PdfGraphicsUnit.Point), pdfUnitConvertor2.ConvertUnits(height, PdfGraphicsUnit.Pixel, PdfGraphicsUnit.Point));
  }

  internal abstract void Save();

  internal void SetContent(IPdfPrimitive content)
  {
    if (content == null)
      throw new ArgumentNullException(nameof (content));
    this.m_stream = content is PdfStream ? content as PdfStream : throw new ArgumentException("The content is not a stream.", nameof (content));
  }

  protected void SetResolution(float horizontalResolution, float verticalResolution)
  {
    this.m_horizontalResolution = horizontalResolution;
    this.m_verticalResolution = verticalResolution;
  }

  public int Height => this.InternalImage.Height;

  public float HorizontalResolution
  {
    get
    {
      float horizontalResolution = (double) this.m_horizontalResolution == 0.0 ? this.InternalImage.HorizontalResolution : this.m_horizontalResolution;
      if ((double) horizontalResolution <= 0.0)
        horizontalResolution = PdfUnitConvertor.HorizontalResolution;
      return horizontalResolution;
    }
  }

  internal abstract Image InternalImage { get; }

  internal int[] Matte
  {
    get => this.m_matte;
    set => this.m_matte = value;
  }

  public virtual SizeF PhysicalDimension
  {
    get
    {
      this.m_phisicalDimension = this.GetPointSize((float) this.Width, (float) this.Height, this.HorizontalResolution, this.VerticalResolution);
      return this.m_phisicalDimension;
    }
  }

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

  internal bool SoftMask => this.m_softmask;

  internal PdfStream Stream
  {
    get
    {
      if (this.m_stream == null)
        this.m_stream = new PdfStream();
      return this.m_stream;
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stream;

  public float VerticalResolution
  {
    get
    {
      float verticalResolution = (double) this.m_verticalResolution == 0.0 ? this.InternalImage.VerticalResolution : this.m_verticalResolution;
      if ((double) verticalResolution <= 0.0)
        verticalResolution = PdfUnitConvertor.VerticalResolution;
      return verticalResolution;
    }
  }

  public int Width => this.InternalImage.Width;
}
