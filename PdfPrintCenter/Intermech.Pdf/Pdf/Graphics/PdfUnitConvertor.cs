// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfUnitConvertor
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using System;
using System.Drawing;
using System.Security;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

[SecurityCritical]
public class PdfUnitConvertor
{
  internal static readonly float HorizontalResolution = 96f;
  internal static readonly float HorizontalSize;
  private double[] m_proportions;
  internal static readonly float PxHorizontalResolution;
  internal static readonly float PxVerticalResolution;
  internal static readonly float VerticalResolution = 96f;
  internal static readonly float VerticalSize;

  static PdfUnitConvertor()
  {
    IntPtr dc = GdiApi.CreateDC("DISPLAY", (string) null, (string) null, IntPtr.Zero);
    PdfUnitConvertor.HorizontalResolution = (float) GdiApi.GetDeviceCaps(dc, 88);
    PdfUnitConvertor.VerticalResolution = (float) GdiApi.GetDeviceCaps(dc, 90);
    PdfUnitConvertor.HorizontalSize = (float) GdiApi.GetDeviceCaps(dc, 4);
    PdfUnitConvertor.VerticalSize = (float) GdiApi.GetDeviceCaps(dc, 6);
    PdfUnitConvertor.PxHorizontalResolution = (float) GdiApi.GetDeviceCaps(dc, 8);
    PdfUnitConvertor.PxVerticalResolution = (float) GdiApi.GetDeviceCaps(dc, 10);
    GdiApi.DeleteDC(dc);
  }

  public PdfUnitConvertor() => this.UpdateProportions(PdfUnitConvertor.HorizontalResolution);

  public PdfUnitConvertor(System.Drawing.Graphics g)
  {
    if (g == null)
      throw new ArgumentNullException(nameof (g));
    this.UpdateProportions(g.DpiX);
  }

  public PdfUnitConvertor(float dpi) => this.UpdateProportions(dpi);

  public PointF ConvertFromPixels(PointF point, PdfGraphicsUnit to)
  {
    return new PointF(this.ConvertFromPixels(point.X, to), this.ConvertFromPixels(point.Y, to));
  }

  public RectangleF ConvertFromPixels(RectangleF rect, PdfGraphicsUnit to)
  {
    double x = (double) this.ConvertFromPixels(rect.X, to);
    float num1 = this.ConvertFromPixels(rect.Y, to);
    float num2 = this.ConvertFromPixels(rect.Width, to);
    double y = (double) num1;
    double width = (double) num2;
    double height = (double) this.ConvertFromPixels(rect.Height, to);
    return new RectangleF((float) x, (float) y, (float) width, (float) height);
  }

  public SizeF ConvertFromPixels(SizeF size, PdfGraphicsUnit to)
  {
    return new SizeF(this.ConvertFromPixels(size.Width, to), this.ConvertFromPixels(size.Height, to));
  }

  public float ConvertFromPixels(float value, PdfGraphicsUnit to)
  {
    int index = (int) to;
    return value / (float) this.m_proportions[index];
  }

  public PointF ConvertToPixels(PointF point, PdfGraphicsUnit from)
  {
    return new PointF(this.ConvertToPixels(point.X, from), this.ConvertToPixels(point.Y, from));
  }

  public RectangleF ConvertToPixels(RectangleF rect, PdfGraphicsUnit from)
  {
    double pixels1 = (double) this.ConvertToPixels(rect.X, from);
    float pixels2 = this.ConvertToPixels(rect.Y, from);
    float pixels3 = this.ConvertToPixels(rect.Width, from);
    double y = (double) pixels2;
    double width = (double) pixels3;
    double pixels4 = (double) this.ConvertToPixels(rect.Height, from);
    return new RectangleF((float) pixels1, (float) y, (float) width, (float) pixels4);
  }

  public SizeF ConvertToPixels(SizeF size, PdfGraphicsUnit from)
  {
    return new SizeF(this.ConvertToPixels(size.Width, from), this.ConvertToPixels(size.Height, from));
  }

  public float ConvertToPixels(float value, PdfGraphicsUnit from)
  {
    int index = (int) from;
    return value * (float) this.m_proportions[index];
  }

  public float ConvertUnits(float value, PdfGraphicsUnit from, PdfGraphicsUnit to)
  {
    return this.ConvertFromPixels(this.ConvertToPixels(value, from), to);
  }

  private void UpdateProportions(float pixelPerInch)
  {
    this.m_proportions = new double[7]
    {
      (double) pixelPerInch / 2.54,
      (double) pixelPerInch / 6.0,
      1.0,
      (double) pixelPerInch / 72.0,
      (double) pixelPerInch,
      (double) pixelPerInch / 300.0,
      (double) pixelPerInch / 25.4
    };
  }
}
