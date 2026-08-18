// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfExternalGraphicsState
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;


namespace Syncfusion.Pdf.Graphics
{
    internal sealed class PdfExternalGraphicsState : IPdfWrapper
    {
      private PdfDictionary m_stateDictionary = new PdfDictionary();

      IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stateDictionary;
    }
}
