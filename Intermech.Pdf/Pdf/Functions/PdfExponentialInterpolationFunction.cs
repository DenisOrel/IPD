// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Functions.PdfExponentialInterpolationFunction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Functions
{
    public class PdfExponentialInterpolationFunction : PdfFunction
    {
      protected float[] m_c0;
      protected float[] m_c1;
      private float m_interpolationExp;

      internal PdfExponentialInterpolationFunction()
        : base(new PdfDictionary())
      {
      }

      public PdfExponentialInterpolationFunction(bool Init)
        : base(new PdfDictionary())
      {
        this.m_interpolationExp = 1f;
        this.Domain = new PdfArray(new float[2]{ 0.0f, 1f });
        this.Range = new PdfArray(new float[8]
        {
          0.0f,
          1f,
          0.0f,
          1f,
          0.0f,
          1f,
          0.0f,
          1f
        });
        this.m_interpolationExp = 1f;
        this.C0 = new float[4];
      }

      internal float[] InterpolationExponent(float[] singleArray1)
      {
        int length = this.Range.Count / 2;
        float[] numArray = new float[length];
        for (int index = 0; index < length; ++index)
          numArray[index] = this.C0[index] + (float) Math.Pow((double) singleArray1[0], (double) this.m_interpolationExp) * (this.C1[index] - this.C0[index]);
        return numArray;
      }

      public float[] C0
      {
        get => this.m_c0;
        set => this.m_c0 = value;
      }

      public float[] C1
      {
        get => this.m_c1;
        set => this.m_c1 = value;
      }

      public float Exponent
      {
        get => this.m_interpolationExp;
        set => this.m_interpolationExp = value;
      }
    }
}
