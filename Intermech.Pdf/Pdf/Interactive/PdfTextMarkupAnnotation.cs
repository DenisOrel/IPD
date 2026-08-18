// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfTextMarkupAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfTextMarkupAnnotation : PdfAnnotation
    {
      private PdfFont m_font;
      private PdfArray m_points;
      private int[] m_quadPoints;
      private string m_text;
      private PdfTextMarkupAnnotationType m_textMarkupAnnotationType;
      private PdfColor m_textMarkupColor;
      private PointF m_textPoint;
      private SizeF m_textSize;

      public PdfTextMarkupAnnotation() => this.m_quadPoints = new int[8];

      public PdfTextMarkupAnnotation(RectangleF rectangle)
        : base(rectangle)
      {
        this.m_quadPoints = new int[8];
      }

      public PdfTextMarkupAnnotation(
        string markupTitle,
        string text,
        string markupText,
        PointF point,
        PdfFont pdfFont)
      {
        this.m_quadPoints = new int[8];
        this.Text = text;
        this.m_text = markupTitle;
        this.m_font = pdfFont;
        this.Location = point;
        this.m_textSize = this.m_font.MeasureString(markupText);
        this.m_textPoint = point;
        this.m_textPoint.X += 25f;
        this.m_textPoint.Y = 800f - this.m_textPoint.Y;
        this.Initialize();
      }

      protected override void Initialize() => base.Initialize();

      protected override void Save()
      {
        base.Save();
        PdfArray primitive = new PdfArray();
        PdfColor pdfColor = !this.TextMarkupColor.IsEmpty ? this.TextMarkupColor : throw new Exception("TextMarkupColor is not null");
        float num1 = (float) pdfColor.R / (float) byte.MaxValue;
        pdfColor = this.TextMarkupColor;
        float num2 = (float) pdfColor.G / (float) byte.MaxValue;
        pdfColor = this.TextMarkupColor;
        float num3 = (float) pdfColor.B / (float) byte.MaxValue;
        primitive.Insert(0, (IPdfPrimitive) new PdfNumber(num1));
        primitive.Insert(1, (IPdfPrimitive) new PdfNumber(num2));
        primitive.Insert(2, (IPdfPrimitive) new PdfNumber(num3));
        this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName((Enum) this.m_textMarkupAnnotationType));
        this.Dictionary.SetProperty("QuadPoints", (IPdfPrimitive) this.m_points);
        this.Dictionary.SetProperty("C", (IPdfPrimitive) primitive);
        this.Dictionary.SetString("T", this.m_text);
        this.Dictionary.SetNumber("CA", 0.5f);
      }

      internal void SetQuadPoints(SizeF pageSize)
      {
        float[] array = new float[8];
        float x = this.Location.X;
        float y = this.Location.Y;
        double width = (double) pageSize.Width;
        float height = pageSize.Height;
        array[0] = x;
        array[1] = height - y;
        array[2] = x + this.m_textSize.Width;
        array[3] = height - y;
        array[4] = x;
        array[5] = array[1] - this.m_textSize.Height;
        array[6] = x + this.m_textSize.Width;
        array[7] = array[5];
        this.m_points = new PdfArray(array);
      }

      public PdfTextMarkupAnnotationType TextMarkupAnnotationType
      {
        get => this.m_textMarkupAnnotationType;
        set => this.m_textMarkupAnnotationType = value;
      }

      public PdfColor TextMarkupColor
      {
        get => this.m_textMarkupColor;
        set => this.m_textMarkupColor = value;
      }
    }
}
