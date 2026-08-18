// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfBlendBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Graphics
{
    public abstract class PdfBlendBase
    {
      private int m_count;
      private float[] m_positions;
      private const float Precision = 1000f;

      protected PdfBlendBase()
      {
      }

      protected PdfBlendBase(int count)
      {
      }

      protected static float Gcd(float[] values)
      {
        if (values == null)
          throw new ArgumentNullException(nameof (values));
        float v = values.Length >= 1 ? values[0] : throw new ArgumentException("Not enough values in the array.", nameof (values));
        if (values.Length > 1)
        {
          int index = 1;
          for (int length = values.Length; index < length; ++index)
          {
            v = PdfBlendBase.Gcd(values[index], v);
            if ((double) v == 1.0 / 1000.0)
              return v;
          }
        }
        return v;
      }

      protected static int Gcd(int u, int v)
      {
        if (u <= 0)
          throw new ArgumentOutOfRangeException(nameof (u), "The arguments can't be less or equal to zero.");
        if (v <= 0)
          throw new ArgumentOutOfRangeException(nameof (v), "The arguments can't be less or equal to zero.");
        if (u == 1 || v == 1)
          return 1;
        int num1 = 0;
        for (; PdfBlendBase.IsEven(u, v); v >>= 1)
        {
          ++num1;
          u >>= 1;
        }
        while ((u & 1) <= 0)
          u >>= 1;
        do
        {
          while ((v & 1) <= 0)
            v >>= 1;
          if (u > v)
          {
            int num2 = v;
            v = u;
            u = num2;
          }
          v -= u;
        }
        while (v != 0);
        return u << num1;
      }

      protected static float Gcd(float u, float v)
      {
        if ((double) u < 0.0 || (double) u > 1.0)
          throw new ArgumentOutOfRangeException(nameof (u));
        if ((double) v < 0.0 || (double) v > 1.0)
          throw new ArgumentOutOfRangeException(nameof (v));
        return (float) PdfBlendBase.Gcd((int) Math.Max(1f, u * 1000f), (int) Math.Max(1f, v * 1000f)) / 1000f;
      }

      internal static double Interpolate(double t, double v1, double v2)
      {
        if (t == 0.0)
          return v1;
        return t == 1.0 ? v2 : v1 + (t - 0.0) * (v2 - v1) / 1.0;
      }

      internal static PdfColor Interpolate(
        double t,
        PdfColor color1,
        PdfColor color2,
        PdfColorSpace colorSpace)
      {
        switch (colorSpace)
        {
          case PdfColorSpace.RGB:
            return new PdfColor((float) PdfBlendBase.Interpolate(t, (double) color1.Red, (double) color2.Red), (float) PdfBlendBase.Interpolate(t, (double) color1.Green, (double) color2.Green), (float) PdfBlendBase.Interpolate(t, (double) color1.Blue, (double) color2.Blue));
          case PdfColorSpace.CMYK:
            double cyan = PdfBlendBase.Interpolate(t, (double) color1.C, (double) color2.C);
            float num1 = (float) PdfBlendBase.Interpolate(t, (double) color1.M, (double) color2.M);
            float num2 = (float) PdfBlendBase.Interpolate(t, (double) color1.Y, (double) color2.Y);
            double magenta = (double) num1;
            double yellow = (double) num2;
            double black = PdfBlendBase.Interpolate(t, (double) color1.K, (double) color2.K);
            return new PdfColor((float) cyan, (float) magenta, (float) yellow, (float) black);
          case PdfColorSpace.GrayScale:
            return new PdfColor((float) PdfBlendBase.Interpolate(t, (double) color1.Gray, (double) color2.Gray));
          default:
            throw new ArgumentException("Unsupported colour space");
        }
      }

      private static bool IsEven(int u) => (u & 1) <= 0;

      private static bool IsEven(int u, int v)
      {
        return (1 & ((u & 1) <= 0 ? 1 : 0) & ((v & 1) <= 0 ? 1 : 0)) != 0;
      }

      protected Array SetArray(Array array)
      {
        int num = array != null ? array.Length : throw new ArgumentNullException(nameof (array));
        if (num < 0)
          throw new ArgumentException("The array can't be an empmy array", nameof (array));
        if (this.Count <= 0)
        {
          this.m_count = num;
          return array;
        }
        if (num != this.Count)
          throw new ArgumentException("The array should agree with Count property", "Positions");
        return array;
      }

      protected int Count => this.m_count;

      public float[] Positions
      {
        get => this.m_positions;
        set
        {
          this.m_positions = value != null ? this.SetArray((Array) value) as float[] : throw new ArgumentNullException(nameof (Positions));
        }
      }
    }
}
