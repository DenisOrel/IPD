// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfBlend
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics
{
    public sealed class PdfBlend : PdfBlendBase
    {
      private float[] m_factors;

      public PdfBlend()
      {
      }

      public PdfBlend(int count)
        : base(count)
      {
      }

      internal PdfBlend Clone()
      {
        PdfBlend pdfBlend = this.MemberwiseClone() as PdfBlend;
        if (this.m_factors != null)
          pdfBlend.Factors = this.m_factors.Clone() as float[];
        if (this.Positions != null)
          pdfBlend.Positions = this.Positions.Clone() as float[];
        return pdfBlend;
      }

      internal PdfColorBlend GenerateColorBlend(PdfColor[] colours, PdfColorSpace colorSpace)
      {
        if (colours == null)
          throw new ArgumentNullException(nameof (colours));
        if (this.Positions == null)
          this.Positions = new float[1];
        PdfColorBlend colorBlend = new PdfColorBlend(this.Count);
        float[] numArray = this.Positions;
        PdfColor[] pdfColorArray;
        if (numArray.Length == 1)
        {
          numArray = new float[3]{ 0.0f, this.Positions[0], 1f };
          pdfColorArray = new PdfColor[3]
          {
            colours[0],
            colours[0],
            colours[1]
          };
        }
        else
        {
          PdfColor colour1 = colours[0];
          PdfColor colour2 = colours[1];
          pdfColorArray = new PdfColor[this.Count];
          int index = 0;
          for (int count = this.Count; index < count; ++index)
            pdfColorArray[index] = PdfBlendBase.Interpolate((double) this.m_factors[index], colour1, colour2, colorSpace);
        }
        colorBlend.Positions = numArray;
        colorBlend.Colors = pdfColorArray;
        return colorBlend;
      }

      public float[] Factors
      {
        get => this.m_factors;
        set
        {
          this.m_factors = value != null ? this.SetArray((Array) value) as float[] : throw new ArgumentNullException(nameof (Factors));
        }
      }
    }
}
