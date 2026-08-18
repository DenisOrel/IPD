// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ColorSpace.PdfColorSpaces
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.ColorSpace
{
    public abstract class PdfColorSpaces : IPdfWrapper, IPdfCache
    {
      private PdfArray colorspace = new PdfArray();
      private IPdfPrimitive m_colorInternals;
      private PdfDictionary m_dictionary = new PdfDictionary();
      internal PdfResources resources;
      protected static object s_syncObject = new object();

      bool IPdfCache.EqualsTo(IPdfCache obj) => false;

      IPdfPrimitive IPdfCache.GetInternals() => this.m_colorInternals;

      void IPdfCache.SetInternals(IPdfPrimitive internals)
      {
        this.m_colorInternals = internals != null ? internals : throw new ArgumentNullException(nameof (internals));
      }

      IPdfPrimitive IPdfWrapper.Element => this.m_colorInternals;
    }
}
