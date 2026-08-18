// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFreeTextAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfFreeTextAnnotation : PdfAnnotation
{
  private const string c_annotationType = "FreeText";
  private PdfAnnotationIntent m_annotationIntent;
  private PdfColor m_borderColor;
  private PointF[] m_calloutLines;
  private PdfFont m_font;
  private PdfLineEndingStyle m_lineEndingStyle;
  private string m_markUpText;
  private float m_opacity;
  private PdfColor m_textMarkupColor;
  private WidgetAnnotation m_widgetAnnotation;

  private PdfFreeTextAnnotation()
  {
    this.m_opacity = 0.9f;
    this.m_widgetAnnotation = new WidgetAnnotation();
  }

  public PdfFreeTextAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
    this.m_opacity = 0.9f;
    this.m_widgetAnnotation = new WidgetAnnotation();
    base.Initialize();
  }

  protected override void Initialize() => base.Initialize();

  protected override void Save()
  {
    base.Save();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("FreeText"));
    PdfArray primitive1 = new PdfArray();
    if (!this.Color.IsEmpty)
    {
      float num1 = (float) this.Color.R / (float) byte.MaxValue;
      PdfColor color = this.Color;
      float num2 = (float) color.G / (float) byte.MaxValue;
      color = this.Color;
      float num3 = (float) color.B / (float) byte.MaxValue;
      primitive1.Insert(0, (IPdfPrimitive) new PdfNumber(num1));
      primitive1.Insert(1, (IPdfPrimitive) new PdfNumber(num2));
      primitive1.Insert(2, (IPdfPrimitive) new PdfNumber(num3));
    }
    this.Dictionary.SetProperty("C", (IPdfPrimitive) primitive1);
    this.Dictionary.SetNumber("CA", this.m_opacity);
    this.Dictionary.SetProperty("T", (IPdfPrimitive) new PdfString(this.m_markUpText));
    this.Dictionary.SetProperty("Contents", (IPdfPrimitive) new PdfString(this.m_markUpText));
    this.Dictionary.SetProperty("IT", (IPdfPrimitive) new PdfName(this.m_annotationIntent.ToString()));
    this.Dictionary.SetProperty("LE", (IPdfPrimitive) new PdfName(this.m_lineEndingStyle.ToString()));
    this.Dictionary.SetProperty("DS", (IPdfPrimitive) new PdfString($"font:{this.Font.Name} {this.Font.Size}pt; color:{ColorTranslator.ToHtml(System.Drawing.Color.FromArgb((int) this.m_textMarkupColor.R, (int) this.m_textMarkupColor.G, (int) this.m_textMarkupColor.B))}"));
    this.Dictionary.SetProperty("DA", (IPdfPrimitive) new PdfString($"{(ValueType) (float) ((double) this.m_borderColor.R / (double) byte.MaxValue)} {(ValueType) (float) ((double) this.m_borderColor.G / (double) byte.MaxValue)} {(ValueType) (float) ((double) this.m_borderColor.B / (double) byte.MaxValue)} rg "));
    if (this.m_calloutLines.Length < 2)
      return;
    PdfArray primitive2 = new PdfArray();
    for (int index = 0; index < this.m_calloutLines.Length; ++index)
    {
      primitive2.Add((IPdfPrimitive) new PdfNumber(this.m_calloutLines[index].X));
      primitive2.Add((IPdfPrimitive) new PdfNumber(this.m_calloutLines[index].Y));
    }
    this.Dictionary.SetProperty("CL", (IPdfPrimitive) primitive2);
  }

  public PdfAnnotationIntent AnnotationIntent
  {
    get => this.m_annotationIntent;
    set => this.m_annotationIntent = value;
  }

  public PdfColor BorderColor
  {
    get => this.m_borderColor;
    set => this.m_borderColor = value;
  }

  public PointF[] CalloutLines
  {
    get => this.m_calloutLines;
    set => this.m_calloutLines = value;
  }

  public PdfFont Font
  {
    get => this.m_font;
    set => this.m_font = value != null ? value : throw new ArgumentNullException(nameof (Font));
  }

  public PdfLineEndingStyle LineEndingStyle
  {
    get => this.m_lineEndingStyle;
    set => this.m_lineEndingStyle = value;
  }

  public string MarkupText
  {
    get => this.m_markUpText;
    set => this.m_markUpText = value;
  }

  public float Opacity
  {
    get => this.m_opacity;
    set
    {
      this.m_opacity = (double) value >= 0.0 && (double) value <= 1.0 ? value : throw new ArgumentException("Valid value should be between 0 to 1.");
    }
  }

  public PdfColor TextMarkupColor
  {
    get => this.m_textMarkupColor;
    set => this.m_textMarkupColor = value;
  }
}
