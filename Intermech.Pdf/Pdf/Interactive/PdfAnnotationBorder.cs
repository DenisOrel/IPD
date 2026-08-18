// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAnnotationBorder
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Interactive
{
    public class PdfAnnotationBorder : IPdfWrapper
    {
      private PdfArray m_array;
      private float m_borderWidth;
      private float m_horizontalRadius;
      private float m_verticalRadius;

      public PdfAnnotationBorder()
      {
        this.m_borderWidth = 1f;
        this.m_array = new PdfArray();
        this.Initialize(this.m_borderWidth, this.m_horizontalRadius, this.m_verticalRadius);
      }

      public PdfAnnotationBorder(float borderWidth)
      {
        this.m_borderWidth = 1f;
        this.m_array = new PdfArray();
        this.Initialize(borderWidth, this.m_horizontalRadius, this.m_verticalRadius);
      }

      public PdfAnnotationBorder(float borderWidth, float horizontalRadius, float verticalRadius)
      {
        this.m_borderWidth = 1f;
        this.m_array = new PdfArray();
        this.Initialize(borderWidth, horizontalRadius, verticalRadius);
      }

      private void Initialize(float borderWidth, float horizontalRadius, float verticalRadius)
      {
        this.m_array.Add((IPdfPrimitive) new PdfNumber(horizontalRadius), (IPdfPrimitive) new PdfNumber(verticalRadius), (IPdfPrimitive) new PdfNumber(borderWidth));
      }

      private void SetNumber(int index, float value)
      {
        (this.m_array[index] as PdfNumber).FloatValue = value;
      }

      public float HorizontalRadius
      {
        get => this.m_horizontalRadius;
        set
        {
          if ((double) this.m_horizontalRadius == (double) value)
            return;
          this.m_horizontalRadius = value;
          this.SetNumber(0, value);
        }
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_array;

      public float VerticalRadius
      {
        get => this.m_verticalRadius;
        set
        {
          if ((double) this.m_verticalRadius == (double) value)
            return;
          this.m_verticalRadius = value;
          this.SetNumber(1, value);
        }
      }

      public float Width
      {
        get => this.m_borderWidth;
        set
        {
          if ((double) this.m_borderWidth == (double) value)
            return;
          this.m_borderWidth = value;
          this.SetNumber(2, value);
        }
      }
    }
}
