// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.PdfCidFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class PdfCidFont : PdfDictionary
    {
      public PdfCidFont(
        PdfCjkFontFamily fontFamily,
        PdfFontStyle fontStyle,
        PdfFontMetrics fontMetrics)
      {
        this["Type"] = (IPdfPrimitive) new PdfName("Font");
        this["Subtype"] = (IPdfPrimitive) new PdfName("CIDFontType2");
        this["BaseFont"] = (IPdfPrimitive) new PdfName(fontMetrics.PostScriptName);
        this["DW"] = (IPdfPrimitive) new PdfNumber((fontMetrics.WidthTable as CjkWidthTable).DefaultWidth);
        this["W"] = (IPdfPrimitive) fontMetrics.WidthTable.ToArray();
        this["FontDescriptor"] = (IPdfPrimitive) PdfCjkFontDescryptorFactory.GetFontDescryptor(fontFamily, fontStyle, fontMetrics);
        this["CIDSystemInfo"] = (IPdfPrimitive) this.GetSystemInfo(fontFamily);
      }

      private PdfDictionary GetSystemInfo(PdfCjkFontFamily fontFamily)
      {
        PdfDictionary systemInfo = new PdfDictionary();
        systemInfo["Registry"] = (IPdfPrimitive) new PdfString("Adobe");
        switch (fontFamily)
        {
          case PdfCjkFontFamily.HeiseiKakuGothicW5:
          case PdfCjkFontFamily.HeiseiMinchoW3:
            systemInfo["Ordering"] = (IPdfPrimitive) new PdfString("Japan1");
            systemInfo["Supplement"] = (IPdfPrimitive) new PdfNumber(2);
            return systemInfo;
          case PdfCjkFontFamily.HanyangSystemsGothicMedium:
          case PdfCjkFontFamily.HanyangSystemsShinMyeongJoMedium:
            systemInfo["Ordering"] = (IPdfPrimitive) new PdfString("Korea1");
            systemInfo["Supplement"] = (IPdfPrimitive) new PdfNumber(1);
            return systemInfo;
          case PdfCjkFontFamily.MonotypeHeiMedium:
          case PdfCjkFontFamily.MonotypeSungLight:
            systemInfo["Ordering"] = (IPdfPrimitive) new PdfString("CNS1");
            systemInfo["Supplement"] = (IPdfPrimitive) new PdfNumber(0);
            return systemInfo;
          case PdfCjkFontFamily.SinoTypeSongLight:
            systemInfo["Ordering"] = (IPdfPrimitive) new PdfString("GB1");
            systemInfo["Supplement"] = (IPdfPrimitive) new PdfNumber(2);
            return systemInfo;
          default:
            throw new ArgumentException("Unsupported font family: " + fontFamily.ToString(), nameof (fontFamily));
        }
      }
    }
}
