// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfDestination
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfDestination : IPdfWrapper
{
  private PdfArray m_array;
  private RectangleF m_bounds;
  private PdfDestinationMode m_destinationMode;
  private bool m_isValid;
  private PointF m_location;
  private PdfPageBase m_page;
  private float m_zoom;

  public PdfDestination(PdfPageBase page)
  {
    this.m_location = PointF.Empty;
    this.m_bounds = RectangleF.Empty;
    this.m_array = new PdfArray();
    this.m_isValid = true;
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    PdfPageRotateAngle pdfPageRotateAngle = PdfPageRotateAngle.RotateAngle0;
    if (page.Rotation != PdfPageRotateAngle.RotateAngle0 && page.Rotation != PdfPageRotateAngle.RotateAngle90)
      pdfPageRotateAngle = page.Rotation;
    if (page is PdfPage)
    {
      PdfPageRotateAngle rotate = (page as PdfPage).Section.PageSettings.Rotate;
      switch (rotate)
      {
        case PdfPageRotateAngle.RotateAngle0:
        case PdfPageRotateAngle.RotateAngle90:
          break;
        default:
          if (rotate != pdfPageRotateAngle)
            break;
          break;
      }
    }
    this.m_location = page.Rotation != PdfPageRotateAngle.RotateAngle180 ? (page.Rotation != PdfPageRotateAngle.RotateAngle90 ? (page.Rotation != PdfPageRotateAngle.RotateAngle270 ? new PointF(0.0f, this.m_location.Y) : new PointF(page.Size.Width, 0.0f)) : new PointF(0.0f, 0.0f)) : new PointF(page.Size.Width, this.m_location.Y);
    this.m_page = page;
  }

  public PdfDestination(PdfPageBase page, PointF location)
    : this(page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    this.m_location = location;
  }

  internal PdfDestination(PdfPageBase page, RectangleF rect)
    : this(page)
  {
    if (page == null)
      throw new ArgumentNullException(nameof (page));
    this.m_bounds = rect;
  }

  private void Initialize()
  {
  }

  private void InitializePrimitive()
  {
    this.m_array.Clear();
    this.m_array.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_page));
    switch (this.m_destinationMode)
    {
      case PdfDestinationMode.Location:
        PdfPage page1 = this.m_page as PdfPage;
        PointF pointF = PointF.Empty;
        if (page1 == null)
        {
          PdfLoadedPage page2 = this.m_page as PdfLoadedPage;
          if (this.m_page.Rotation == PdfPageRotateAngle.RotateAngle180)
          {
            pointF.X = page2.Size.Width;
            pointF.Y = this.m_location.Y;
          }
          else if (this.m_page.Rotation == PdfPageRotateAngle.RotateAngle90)
            pointF.X = this.m_location.Y;
          else if (this.m_page.Rotation == PdfPageRotateAngle.RotateAngle270)
          {
            pointF.X = page2.Size.Width - this.m_location.Y;
            pointF.Y = page2.Size.Height;
          }
          else
            pointF.Y = page2.Size.Height - this.m_location.Y;
        }
        else
          pointF = this.PointToNativePdf(page1, this.m_location);
        this.m_array.Add((IPdfPrimitive) new PdfName("XYZ"));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(pointF.X));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(pointF.Y));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(this.m_zoom));
        break;
      case PdfDestinationMode.FitToPage:
        this.m_array.Add((IPdfPrimitive) new PdfName("Fit"));
        break;
      case PdfDestinationMode.FitR:
        if (!(this.m_page is PdfLoadedPage))
          break;
        this.m_array.Add((IPdfPrimitive) new PdfName("FitR"));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(this.m_bounds.X));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(this.m_bounds.Y));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(this.m_bounds.Width));
        this.m_array.Add((IPdfPrimitive) new PdfNumber(this.m_bounds.Height));
        break;
    }
  }

  private PointF PointToNativePdf(PdfPage page, PointF point)
  {
    return page.Section.PointToNativePdf(page, point);
  }

  internal void SetValidation(bool valid) => this.m_isValid = valid;

  public bool IsValid => this.m_isValid;

  public PointF Location
  {
    get => this.m_location;
    set
    {
      if (!(this.m_location != value))
        return;
      this.m_location = value;
      this.InitializePrimitive();
    }
  }

  public PdfDestinationMode Mode
  {
    get => this.m_destinationMode;
    set
    {
      if (this.m_destinationMode == value)
        return;
      this.m_destinationMode = value;
      this.InitializePrimitive();
    }
  }

  public PdfPageBase Page
  {
    get => this.m_page;
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (Page));
      if (this.m_page == value)
        return;
      this.m_page = value;
      this.InitializePrimitive();
    }
  }

  IPdfPrimitive IPdfWrapper.Element
  {
    get
    {
      this.InitializePrimitive();
      return (IPdfPrimitive) this.m_array;
    }
  }

  public float Zoom
  {
    get => this.m_zoom;
    set
    {
      if ((double) this.m_zoom == (double) value)
        return;
      this.m_zoom = value;
      this.InitializePrimitive();
    }
  }
}
