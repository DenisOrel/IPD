// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfRubberStampAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfRubberStampAnnotation : PdfAnnotation
{
  private PdfAppearance m_appearance;
  private PdfRubberStampAnnotationIcon m_rubberStampAnnotaionIcon;

  public PdfRubberStampAnnotation()
  {
    this.m_rubberStampAnnotaionIcon = PdfRubberStampAnnotationIcon.Draft;
  }

  public PdfRubberStampAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
    this.m_rubberStampAnnotaionIcon = PdfRubberStampAnnotationIcon.Draft;
  }

  public PdfRubberStampAnnotation(RectangleF rectangle, string text)
    : base(rectangle)
  {
    this.m_rubberStampAnnotaionIcon = PdfRubberStampAnnotationIcon.Draft;
    this.Text = text != null ? text : throw new ArgumentNullException(nameof (text));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Stamp"));
  }

  protected override void Save()
  {
    base.Save();
    if (this.m_appearance == null || this.m_appearance.Normal == null)
      return;
    this.Dictionary.SetProperty("AP", (IPdfWrapper) this.m_appearance);
  }

  public PdfAppearance Appearance
  {
    get
    {
      if (this.m_appearance == null)
        this.m_appearance = new PdfAppearance((PdfAnnotation) this);
      return this.m_appearance;
    }
    set
    {
      if (this.m_appearance == value)
        return;
      this.m_appearance = value;
    }
  }

  public PdfRubberStampAnnotationIcon Icon
  {
    get => this.m_rubberStampAnnotaionIcon;
    set
    {
      this.m_rubberStampAnnotaionIcon = value;
      this.Dictionary.SetName("Name", this.m_rubberStampAnnotaionIcon.ToString());
    }
  }
}
