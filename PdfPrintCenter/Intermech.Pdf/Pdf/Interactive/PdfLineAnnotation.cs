// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLineAnnotation
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

public class PdfLineAnnotation : PdfAnnotation
{
  private PdfColor m_backgroundColor;
  private PdfLineEndingStyle m_beginLine;
  public PdfLineCaptionType m_captionType;
  private PdfLineEndingStyle m_endLine;
  private PdfColor m_innerLineColor;
  private int m_leaderLine;
  private int m_leaderLineExt;
  private LineBorder m_lineBorder;
  private bool m_lineCaption;
  private PdfLineIntent m_lineIntent;
  internal PdfArray m_linePoints;
  internal PdfArray m_lineStyle;

  public PdfLineAnnotation(int[] linePoints)
  {
    this.m_lineBorder = new LineBorder();
    this.m_linePoints = new PdfArray(linePoints);
  }

  public PdfLineAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
    this.m_lineBorder = new LineBorder();
  }

  public PdfLineAnnotation(int[] linePoints, string text)
  {
    this.m_lineBorder = new LineBorder();
    this.m_linePoints = new PdfArray(linePoints);
    this.Text = text;
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Line"));
  }

  protected override void Save()
  {
    base.Save();
    this.m_lineStyle = new PdfArray();
    this.m_lineStyle.Insert(0, (IPdfPrimitive) new PdfName((Enum) this.BeginLineStyle));
    this.m_lineStyle.Insert(1, (IPdfPrimitive) new PdfName((Enum) this.EndLineStyle));
    this.Dictionary.SetProperty("LE", (IPdfPrimitive) this.m_lineStyle);
    this.Dictionary.SetProperty("L", (IPdfPrimitive) this.m_linePoints);
    this.Dictionary.SetProperty("BS", (IPdfWrapper) this.m_lineBorder);
    float num1 = (float) this.InnerLineColor.R / (float) byte.MaxValue;
    float num2 = (float) this.InnerLineColor.G / (float) byte.MaxValue;
    float num3 = (float) this.InnerLineColor.B / (float) byte.MaxValue;
    PdfArray primitive = new PdfArray();
    primitive.Insert(0, (IPdfPrimitive) new PdfNumber(num1));
    primitive.Insert(1, (IPdfPrimitive) new PdfNumber(num2));
    primitive.Insert(2, (IPdfPrimitive) new PdfNumber(num3));
    this.Dictionary.SetProperty("IC", (IPdfPrimitive) primitive);
    PdfArray array = new PdfArray();
    array.Insert(0, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.R / (float) byte.MaxValue));
    array.Insert(1, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.G / (float) byte.MaxValue));
    array.Insert(2, (IPdfPrimitive) new PdfNumber((float) this.m_backgroundColor.B / (float) byte.MaxValue));
    this.Dictionary["C"] = (IPdfPrimitive) new PdfArray(array);
    this.Dictionary.SetProperty("IT", (IPdfPrimitive) new PdfName((Enum) this.m_lineIntent));
    this.Dictionary.SetProperty("LLE", (IPdfPrimitive) new PdfNumber(this.m_leaderLineExt));
    this.Dictionary.SetProperty("LL", (IPdfPrimitive) new PdfNumber(this.m_leaderLine));
    this.Dictionary.SetProperty("CP", (IPdfPrimitive) new PdfName((Enum) this.m_captionType));
    this.Dictionary.SetProperty("Cap", (IPdfPrimitive) new PdfBoolean(this.m_lineCaption));
  }

  public PdfColor BackColor
  {
    get => this.m_backgroundColor;
    set => this.m_backgroundColor = value;
  }

  public PdfLineEndingStyle BeginLineStyle
  {
    get => this.m_beginLine;
    set
    {
      if (this.m_beginLine == value)
        return;
      this.m_beginLine = value;
    }
  }

  public PdfLineCaptionType CaptionType
  {
    get => this.m_captionType;
    set => this.m_captionType = value;
  }

  public PdfLineEndingStyle EndLineStyle
  {
    get => this.m_endLine;
    set
    {
      if (this.m_endLine == value)
        return;
      this.m_endLine = value;
    }
  }

  public PdfColor InnerLineColor
  {
    get => this.m_innerLineColor;
    set => this.m_innerLineColor = value;
  }

  public int LeaderLine
  {
    get => this.m_leaderLine;
    set
    {
      if (this.m_leaderLineExt == 0)
        return;
      this.m_leaderLine = value;
    }
  }

  public int LeaderLineExt
  {
    get => this.m_leaderLineExt;
    set => this.m_leaderLineExt = value;
  }

  public LineBorder lineBorder
  {
    get => this.m_lineBorder;
    set => this.m_lineBorder = value;
  }

  public bool LineCaption
  {
    get => this.m_lineCaption;
    set => this.m_lineCaption = value;
  }

  public PdfLineIntent LineIntent
  {
    get => this.m_lineIntent;
    set => this.m_lineIntent = value;
  }
}
