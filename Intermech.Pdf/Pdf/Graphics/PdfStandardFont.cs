// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfStandardFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Primitives;
using System;
using System.Text;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfStandardFont : PdfFont
    {
      private const int c_charOffset = 32 /*0x20*/;
      private PdfFontFamily m_fontFamily;

      public PdfStandardFont(PdfFontFamily fontFamily, float size)
        : this(fontFamily, size, PdfFontStyle.Regular)
      {
      }

      public PdfStandardFont(PdfStandardFont prototype, float size)
        : this(prototype.FontFamily, size, prototype.Style)
      {
      }

      public PdfStandardFont(PdfFontFamily fontFamily, float size, PdfFontStyle style)
        : base(size, style)
      {
        this.m_fontFamily = fontFamily;
        this.CheckStyle();
        this.InitializeInternals();
      }

      public PdfStandardFont(PdfStandardFont prototype, float size, PdfFontStyle style)
        : this(prototype.FontFamily, size, style)
      {
      }

      private void CheckStyle()
      {
        if (this.FontFamily != PdfFontFamily.Symbol && this.FontFamily != PdfFontFamily.ZapfDingbats)
          return;
        this.SetStyle(this.Style & ~(PdfFontStyle.Bold | PdfFontStyle.Italic));
      }

      internal static string Convert(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        byte[] numArray = Encoding.Convert(Encoding.Unicode, Encoding.Default, Encoding.Unicode.GetBytes(text));
        int length = numArray.Length;
        char[] chArray = new char[length];
        for (int index = 0; index < length; ++index)
          chArray[index] = (char) numArray[index];
        return new string(chArray);
      }

      private PdfDictionary CreateInternals()
      {
        PdfDictionary internals = new PdfDictionary();
        internals["Type"] = (IPdfPrimitive) new PdfName("Font");
        internals["Subtype"] = (IPdfPrimitive) new PdfName("Type1");
        internals["BaseFont"] = (IPdfPrimitive) new PdfName(this.Metrics.PostScriptName);
        if (this.FontFamily != PdfFontFamily.Symbol && this.FontFamily != PdfFontFamily.ZapfDingbats)
        {
          string str = FontEncoding.WinAnsiEncoding.ToString();
          internals["Encoding"] = (IPdfPrimitive) new PdfName(str);
        }
        return internals;
      }

      protected override bool EqualsToFont(PdfFont font)
      {
        bool font1 = false;
        if (font is PdfStandardFont pdfStandardFont)
          font1 = this.FontFamily == pdfStandardFont.FontFamily & (this.Style & ~(PdfFontStyle.Strikeout | PdfFontStyle.Underline)) == (pdfStandardFont.Style & ~(PdfFontStyle.Strikeout | PdfFontStyle.Underline));
        return font1;
      }

      protected internal override float GetCharWidth(char charCode, PdfStringFormat format)
      {
        return this.GetCharWidthInternal(charCode, format) * (1f / 1000f * this.Metrics.GetSize(format));
      }

      private float GetCharWidthInternal(char charCode, PdfStringFormat format)
      {
        int num = (int) charCode - 32 /*0x20*/;
        return (float) this.Metrics.WidthTable[num < 0 || num == 128 /*0x80*/ ? 0 : num];
      }

      protected internal override float GetLineWidth(string line, PdfStringFormat format)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        float num = 0.0f;
        line = PdfStandardFont.Convert(line);
        int index = 0;
        for (int length = line.Length; index < length; ++index)
        {
          float charWidthInternal = this.GetCharWidthInternal(line[index], format);
          num += charWidthInternal;
        }
        float size = this.Metrics.GetSize(format);
        float width = num * (1f / 1000f * size);
        return this.ApplyFormatSettings(line, format, width);
      }

      private void InitializeInternals()
      {
        if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
          throw new PdfConformanceException("All the fonts must be embedded in PDF/A1-B document.");
        if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_X1A2001)
          throw new PdfConformanceException("All the fonts must be embedded in PDF/X1A document.");
        lock (PdfFont.s_syncObject)
        {
          IPdfCache pdfCache = (IPdfCache) null;
          if (PdfDocument.EnableCache)
            pdfCache = PdfDocument.Cache.Search((IPdfCache) this);
          IPdfPrimitive internals;
          if (pdfCache == null)
          {
            this.Metrics = PdfStandardFontMetricsFactory.GetMetrics(this.m_fontFamily, this.Style, this.Size);
            internals = (IPdfPrimitive) this.CreateInternals();
          }
          else
          {
            internals = pdfCache.GetInternals();
            PdfFontMetrics pdfFontMetrics = (PdfFontMetrics) ((PdfFont) pdfCache).Metrics.Clone();
            pdfFontMetrics.Size = this.Size;
            this.Metrics = pdfFontMetrics;
          }
          ((IPdfCache) this).SetInternals(internals);
        }
      }

      public PdfFontFamily FontFamily => this.m_fontFamily;
    }
}
