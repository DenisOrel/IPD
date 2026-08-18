// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfColorBlend
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Functions;
using Syncfusion.Pdf.Primitives;
using System;
using System.Text;


namespace Syncfusion.Pdf.Graphics
{
    public sealed class PdfColorBlend : PdfBlendBase
    {
      private PdfBrush m_brush;
      private PdfColor[] m_colors;

      public PdfColorBlend()
      {
      }

      internal PdfColorBlend(PdfBrush brush) => this.m_brush = brush;

      public PdfColorBlend(int count)
        : base(count)
      {
      }

      internal PdfColorBlend Clone()
      {
        PdfColorBlend pdfColorBlend = this.MemberwiseClone() as PdfColorBlend;
        if (this.m_colors != null)
          pdfColorBlend.Colors = this.m_colors.Clone() as PdfColor[];
        if (this.Positions != null)
          pdfColorBlend.Positions = this.Positions.Clone() as float[];
        return pdfColorBlend;
      }

      private byte[] GetCmykSamples(int sampleCount, int maxComponentValue, float step)
      {
        byte[] cmykSamples = new byte[sampleCount * 4];
        for (int index1 = 0; index1 < sampleCount; ++index1)
        {
          PdfColor nextColor = this.GetNextColor(index1, step, PdfColorSpace.CMYK);
          int index2 = index1 * 4;
          cmykSamples[index2] = (byte) ((double) nextColor.C * (double) maxComponentValue);
          cmykSamples[index2 + 1] = (byte) ((double) nextColor.M * (double) maxComponentValue);
          cmykSamples[index2 + 2] = (byte) ((double) nextColor.Y * (double) maxComponentValue);
          cmykSamples[index2 + 3] = (byte) ((double) nextColor.K * (double) maxComponentValue);
        }
        return cmykSamples;
      }

      private static int GetColorComponentsCount(PdfColorSpace colorSpace)
      {
        switch (colorSpace)
        {
          case PdfColorSpace.RGB:
            return 3;
          case PdfColorSpace.CMYK:
            return 4;
          case PdfColorSpace.GrayScale:
            return 1;
          default:
            throw new ArgumentException("Unsupported color space: " + (object) colorSpace, nameof (colorSpace));
        }
      }

      internal PdfFunction GetFunction(PdfColorSpace colorSpace)
      {
        float[] domain = new float[2]{ 0.0f, 1f };
        int colorComponentsCount = PdfColorBlend.GetColorComponentsCount(colorSpace);
        int maxComponentValue = this.GetMaxComponentValue(colorSpace);
        double maxValue = (double) maxComponentValue;
        float[] numArray = PdfColorBlend.SetRange(colorComponentsCount, (float) maxValue);
        if (this.m_brush == null)
        {
          int[] sizes = new int[1];
          float step = 1f;
          int sampleCount;
          if (this.Positions.Length == 2)
          {
            sampleCount = 2;
          }
          else
          {
            float num = PdfBlendBase.Gcd(this.GetIntervals(this.Positions));
            step = num;
            sampleCount = (int) (1.0 / (double) num) + 2;
          }
          sizes[0] = sampleCount;
          return (PdfFunction) new PdfSampledFunction(domain, numArray, sizes, this.GetSamplesValues(colorSpace, sampleCount, maxComponentValue, step));
        }
        if (this.m_brush is PdfLinearGradientBrush || this.m_brush is PdfRadialGradientBrush)
        {
          PdfLinearGradientBrush brush1 = this.m_brush as PdfLinearGradientBrush;
          PdfRadialGradientBrush brush2 = this.m_brush as PdfRadialGradientBrush;
          if (brush1 != null && brush1.Extend == PdfExtend.Both || brush2 != null)
          {
            PdfStitchingFunction function = new PdfStitchingFunction();
            PdfArray pdfArray = new PdfArray();
            StringBuilder stringBuilder1 = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            for (int index = 1; index < this.Positions.Length; ++index)
            {
              PdfExponentialInterpolationFunction wrapper = new PdfExponentialInterpolationFunction(true);
              float[] array1 = new float[2]{ 0.0f, 1f };
              wrapper.Domain = new PdfArray(array1);
              wrapper.Range = new PdfArray(numArray);
              float[] array2 = new float[3]
              {
                this.Colors[index - 1].Red,
                this.Colors[index - 1].Green,
                this.Colors[index - 1].Blue
              };
              float[] array3 = new float[3]
              {
                this.Colors[index].Red,
                this.Colors[index].Green,
                this.Colors[index].Blue
              };
              wrapper.Dictionary["FunctionType"] = (IPdfPrimitive) new PdfNumber(2);
              wrapper.Dictionary["N"] = (IPdfPrimitive) new PdfNumber(1);
              wrapper.Dictionary["C0"] = (IPdfPrimitive) new PdfArray(array2);
              wrapper.Dictionary["C1"] = (IPdfPrimitive) new PdfArray(array3);
              if (index > 1)
              {
                stringBuilder1.Append(' ');
                stringBuilder2.Append(' ');
              }
              if (index < this.Positions.Length - 1)
                stringBuilder1.Append(this.Positions[index]);
              if (brush1 != null)
                stringBuilder2.Append("0 1");
              else if (brush2 != null)
                stringBuilder2.Append("1 0");
              PdfReferenceHolder element = new PdfReferenceHolder((IPdfWrapper) wrapper);
              pdfArray.Add((IPdfPrimitive) element);
            }
            float[] array4 = new float[stringBuilder2.ToString().Split(new char[1]
            {
              ' '
            }, StringSplitOptions.RemoveEmptyEntries).Length];
            float[] array5 = new float[stringBuilder1.ToString().Split(new char[1]
            {
              ' '
            }, StringSplitOptions.RemoveEmptyEntries).Length];
            for (int index = 0; index < array4.Length; ++index)
              array4[index] = float.Parse(stringBuilder2.ToString().Split(new char[1]
              {
                ' '
              }, StringSplitOptions.RemoveEmptyEntries)[index]);
            for (int index = 0; index < array5.Length; ++index)
              array5[index] = float.Parse(stringBuilder1.ToString().Split(new char[1]
              {
                ' '
              }, StringSplitOptions.RemoveEmptyEntries)[index]);
            function.Dictionary["Bounds"] = (IPdfPrimitive) new PdfArray(array5);
            function.Dictionary["Encode"] = (IPdfPrimitive) new PdfArray(array4);
            if (brush2 != null)
              function.Range = new PdfArray(numArray);
            float[] array6 = new float[2]{ 0.0f, 1f };
            function.Domain = new PdfArray(array6);
            function.Dictionary["Functions"] = (IPdfPrimitive) pdfArray;
            function.Dictionary["FunctionType"] = (IPdfPrimitive) new PdfNumber(3);
            return (PdfFunction) function;
          }
          if (brush1 != null)
            brush1.Extend = PdfExtend.Both;
        }
        return (PdfFunction) null;
      }

      private byte[] GetGrayscaleSamples(int sampleCount, int maxComponentValue, float step)
      {
        byte[] grayscaleSamples = new byte[sampleCount * 2];
        for (int index1 = 0; index1 < sampleCount; ++index1)
        {
          PdfColor nextColor = this.GetNextColor(index1, step, PdfColorSpace.GrayScale);
          int index2 = index1 * 2;
          byte[] bytes = BitConverter.GetBytes((short) ((double) nextColor.Gray * (double) maxComponentValue));
          grayscaleSamples[index2] = bytes[0];
          grayscaleSamples[index2 + 1] = bytes[1];
        }
        return grayscaleSamples;
      }

      private void GetIndices(float position, out int indexLow, out int indexHi)
      {
        float[] positions = this.Positions;
        indexLow = 0;
        indexHi = 0;
        for (int index = 0; index < this.m_colors.Length; ++index)
        {
          float num = positions[index];
          if ((double) num == (double) position)
          {
            indexLow = indexHi = index;
            break;
          }
          if ((double) num > (double) position)
          {
            indexHi = index;
            break;
          }
          indexLow = index;
          indexHi = index;
        }
      }

      private float[] GetIntervals(float[] positions)
      {
        int length = positions.Length;
        float[] intervals = new float[length - 1];
        float num = positions[0];
        for (int index = 1; index < length; ++index)
        {
          float position = positions[index];
          intervals[index - 1] = position - num;
          num = position;
        }
        return intervals;
      }

      private int GetMaxComponentValue(PdfColorSpace colorSpace)
      {
        switch (colorSpace)
        {
          case PdfColorSpace.RGB:
          case PdfColorSpace.CMYK:
            return (int) byte.MaxValue;
          case PdfColorSpace.GrayScale:
            return (int) ushort.MaxValue;
          default:
            throw new ArgumentException("Unsupported color space: " + (object) colorSpace, nameof (colorSpace));
        }
      }

      private PdfColor GetNextColor(int index, float step, PdfColorSpace colorSpace)
      {
        float position1 = step * (float) index;
        int indexLow;
        int indexHi;
        this.GetIndices(position1, out indexLow, out indexHi);
        if (indexLow == indexHi)
          return this.m_colors[indexLow];
        float position2 = this.Positions[indexLow];
        float position3 = this.Positions[indexHi];
        PdfColor color1 = this.m_colors[indexLow];
        PdfColor color2 = this.m_colors[indexHi];
        return PdfBlendBase.Interpolate(((double) position1 - (double) position2) / ((double) position3 - (double) position2), color1, color2, colorSpace);
      }

      private byte[] GetRgbSamples(int sampleCount, int maxComponentValue, float step)
      {
        byte[] rgbSamples = new byte[sampleCount * 3];
        for (int index1 = 0; index1 < sampleCount; ++index1)
        {
          PdfColor nextColor = this.GetNextColor(index1, step, PdfColorSpace.RGB);
          int index2 = index1 * 3;
          rgbSamples[index2] = nextColor.R;
          rgbSamples[index2 + 1] = nextColor.G;
          rgbSamples[index2 + 2] = nextColor.B;
        }
        return rgbSamples;
      }

      private byte[] GetSamplesValues(
        PdfColorSpace colorSpace,
        int sampleCount,
        int maxComponentValue,
        float step)
      {
        switch (colorSpace)
        {
          case PdfColorSpace.RGB:
            return this.GetRgbSamples(sampleCount, maxComponentValue, step);
          case PdfColorSpace.CMYK:
            return this.GetCmykSamples(sampleCount, maxComponentValue, step);
          case PdfColorSpace.GrayScale:
            return this.GetGrayscaleSamples(sampleCount, maxComponentValue, step);
          default:
            throw new ArgumentException("Unsupported color space: " + (object) colorSpace, nameof (colorSpace));
        }
      }

      private static float[] SetRange(int colourComponents, float maxValue)
      {
        float[] numArray = new float[colourComponents * 2];
        for (int index = 0; index < colourComponents; ++index)
        {
          numArray[index * 2] = 0.0f;
          numArray[index * 2 + 1] = 1f;
        }
        return numArray;
      }

      public PdfColor[] Colors
      {
        get => this.m_colors;
        set
        {
          this.m_colors = value != null ? this.SetArray((Array) value) as PdfColor[] : throw new ArgumentNullException(nameof (Colors));
        }
      }
    }
}
