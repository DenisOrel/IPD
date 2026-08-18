// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Functions.PdfFunction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Functions
{
    public abstract class PdfFunction : IPdfWrapper
    {
      private PdfDictionary m_dictionary;

      internal PdfFunction(PdfDictionary dic) => this.m_dictionary = dic;

      internal PdfDictionary Dictionary => this.m_dictionary;

      internal PdfArray Domain
      {
        get => this.m_dictionary[nameof (Domain)] as PdfArray;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Domain));
          this.m_dictionary.SetProperty(nameof (Domain), (IPdfPrimitive) value);
        }
      }

      internal PdfArray Range
      {
        get => this.m_dictionary[nameof (Range)] as PdfArray;
        set => this.m_dictionary.SetProperty(nameof (Range), (IPdfPrimitive) value);
      }

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_dictionary;
    }
}
