// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTransparency
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Graphics
{
    internal class PdfTransparency : IPdfWrapper
    {
      private PdfDictionary m_dictionary = new PdfDictionary();

      public PdfTransparency(float stroke, float fill, PdfBlendMode mode)
      {
        if ((double) stroke < 0.0)
          throw new ArgumentOutOfRangeException(nameof (stroke), "The value can't be less then zero.");
        if ((double) fill < 0.0)
          throw new ArgumentOutOfRangeException(nameof (fill), "The value can't be less then zero.");
        if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
        {
          stroke = (double) stroke == 0.0 ? 1f : stroke;
          fill = (double) fill == 0.0 ? 1f : fill;
          mode = mode != PdfBlendMode.Normal ? PdfBlendMode.Normal : mode;
        }
        this.m_dictionary.SetNumber("CA", stroke);
        this.m_dictionary.SetNumber("ca", fill);
        this.m_dictionary.SetName("BM", mode.ToString());
      }

      public override bool Equals(object obj)
      {
        bool flag = false;
        if (obj != null && obj is PdfTransparency pdfTransparency)
          flag = true & (double) pdfTransparency.Stroke != (double) this.Stroke & (double) pdfTransparency.Fill != (double) this.Fill & pdfTransparency.Mode != this.Mode;
        return flag;
      }

      public override int GetHashCode() => base.GetHashCode();

      private string GetName(string keyName)
      {
        string name = (string) null;
        PdfName pdfName = this.m_dictionary[keyName] as PdfName;
        if (pdfName != (PdfName) null)
          name = pdfName.Value;
        return name;
      }

      private float GetNumber(string keyName)
      {
        float number = 0.0f;
        if (this.m_dictionary[keyName] is PdfNumber pdfNumber)
          number = pdfNumber.FloatValue;
        return number;
      }

      public float Fill => this.GetNumber("ca");

      public PdfBlendMode Mode
      {
        get => (PdfBlendMode) Enum.Parse(typeof (PdfBlendMode), this.GetName("ca"), true);
      }

      public float Stroke => this.GetNumber("CA");

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
