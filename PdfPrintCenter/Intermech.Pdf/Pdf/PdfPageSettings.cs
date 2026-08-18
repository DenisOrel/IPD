// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageSettings
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPageSettings : ICloneable
{
  private PdfGraphicsUnit m_logicalUnit;
  private PdfMargins m_margins;
  private PdfPageOrientation m_orientation;
  private PointF m_origin;
  private PdfPageRotateAngle m_rotateAngle;
  private SizeF m_size;
  private PdfPageTransition m_transition;

  public PdfPageSettings()
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
  }

  public PdfPageSettings(PdfPageOrientation pageOrientation)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_orientation = pageOrientation;
    this.UpdateSize(pageOrientation);
  }

  public PdfPageSettings(SizeF size)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
  }

  public PdfPageSettings(float margins)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_margins.SetMargins(margins);
  }

  public PdfPageSettings(SizeF size, PdfPageOrientation pageOrientation)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
    this.m_orientation = pageOrientation;
    this.UpdateSize(pageOrientation);
  }

  public PdfPageSettings(SizeF size, float margins)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
    this.m_margins.SetMargins(margins);
  }

  public PdfPageSettings(SizeF size, PdfPageOrientation pageOrientation, float margins)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
    this.m_orientation = pageOrientation;
    this.m_margins.SetMargins(margins);
    this.UpdateSize(pageOrientation);
  }

  public PdfPageSettings(float leftMargin, float topMargin, float rightMargin, float bottomMargin)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_margins.SetMargins(leftMargin, topMargin, rightMargin, bottomMargin);
  }

  public PdfPageSettings(
    SizeF size,
    float leftMargin,
    float topMargin,
    float rightMargin,
    float bottomMargin)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
    this.m_margins.SetMargins(leftMargin, topMargin, rightMargin, bottomMargin);
  }

  public PdfPageSettings(
    SizeF size,
    PdfPageOrientation pageOrientation,
    float leftMargin,
    float topMargin,
    float rightMargin,
    float bottomMargin)
  {
    this.m_size = PdfPageSize.A4;
    this.m_margins = new PdfMargins();
    this.m_logicalUnit = PdfGraphicsUnit.Point;
    this.m_origin = PointF.Empty;
    this.m_size = size;
    this.m_orientation = pageOrientation;
    this.m_margins.SetMargins(leftMargin, topMargin, rightMargin, bottomMargin);
    this.UpdateSize(pageOrientation);
  }

  public object Clone()
  {
    PdfPageSettings pdfPageSettings = (PdfPageSettings) this.MemberwiseClone();
    pdfPageSettings.m_margins = (PdfMargins) this.Margins.Clone();
    if (this.GetTransition() != null)
      pdfPageSettings.Transition = (PdfPageTransition) this.Transition.Clone();
    return (object) pdfPageSettings;
  }

  internal SizeF GetActualSize()
  {
    return new SizeF(this.Width - (this.Margins.Left + this.Margins.Right), this.Height - (this.Margins.Top + this.Margins.Bottom));
  }

  internal PdfPageTransition GetTransition() => this.m_transition;

  public void SetMargins(float margins) => this.m_margins.SetMargins(margins);

  public void SetMargins(float leftRight, float topBottom)
  {
    this.m_margins.SetMargins(leftRight, topBottom);
  }

  public void SetMargins(float left, float top, float right, float bottom)
  {
    this.m_margins.SetMargins(left, top, right, bottom);
  }

  private void SetSize(SizeF size)
  {
    float num1 = Math.Min(size.Width, size.Height);
    float num2 = Math.Max(size.Width, size.Height);
    if (this.Orientation == PdfPageOrientation.Portrait)
      this.m_size = new SizeF(num1, num2);
    else
      this.m_size = new SizeF(num2, num1);
  }

  private void UpdateSize(PdfPageOrientation orientation)
  {
    float num1 = Math.Min(this.Width, this.Height);
    float num2 = Math.Max(this.Width, this.Height);
    if (orientation != PdfPageOrientation.Portrait)
    {
      if (orientation != PdfPageOrientation.Landscape)
        return;
      this.Size = new SizeF(num2, num1);
    }
    else
      this.Size = new SizeF(num1, num2);
  }

  public float Height
  {
    get => this.m_size.Height;
    set => this.m_size.Height = value;
  }

  public PdfMargins Margins
  {
    get => this.m_margins;
    set => this.m_margins = value;
  }

  public PdfPageOrientation Orientation
  {
    get => this.m_orientation;
    set
    {
      if (this.m_orientation == value)
        return;
      this.m_orientation = value;
      this.UpdateSize(value);
    }
  }

  internal PointF Origin
  {
    get => this.m_origin;
    set => this.m_origin = value;
  }

  public PdfPageRotateAngle Rotate
  {
    get => this.m_rotateAngle;
    set => this.m_rotateAngle = value;
  }

  public SizeF Size
  {
    get => this.m_size;
    set => this.SetSize(value);
  }

  public PdfPageTransition Transition
  {
    get
    {
      if (this.m_transition == null)
        this.m_transition = new PdfPageTransition();
      return this.m_transition;
    }
    set => this.m_transition = value;
  }

  public PdfGraphicsUnit Unit
  {
    get => this.m_logicalUnit;
    set => this.m_logicalUnit = value;
  }

  public float Width
  {
    get => this.m_size.Width;
    set => this.m_size.Width = value;
  }
}
