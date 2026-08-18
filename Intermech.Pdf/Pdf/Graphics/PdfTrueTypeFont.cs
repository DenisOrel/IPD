// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfTrueTypeFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.IO;
using System.Text;


namespace Syncfusion.Pdf.Graphics
{
    public class PdfTrueTypeFont : PdfFont, IDisposable
    {
      private const int c_codePage = 1252;
      internal static readonly Encoding Encoding = Encoding.GetEncoding(1252);
      private bool m_bUseTrueType;
      private bool m_embed;
      private ITrueTypeFont m_fontInternal;
      private bool m_unicode;

      public PdfTrueTypeFont(Font font)
        : this(font, false)
      {
      }

      public PdfTrueTypeFont(PdfTrueTypeFont prototype, float size)
        : base(size, prototype.Style)
      {
        this.m_unicode = true;
        this.m_unicode = prototype != null ? prototype.Unicode : throw new ArgumentNullException(nameof (prototype));
        this.CreateFontInternal(prototype);
      }

      public PdfTrueTypeFont(Font font, bool unicode)
        : this(font, font.SizeInPoints, unicode)
      {
      }

      public PdfTrueTypeFont(Font font, float size)
        : this(font, size, false)
      {
      }

      internal PdfTrueTypeFont(Stream fontStream, float size)
        : base(size)
      {
        this.m_unicode = true;
        this.m_unicode = true;
        this.CreateFontInternal(fontStream, PdfFontStyle.Regular);
      }

      public PdfTrueTypeFont(string fontFile, float size)
        : this(fontFile, size, PdfFontStyle.Regular)
      {
      }

      internal PdfTrueTypeFont(Font font, bool unicode, bool useTrueType)
        : this(font, font.SizeInPoints, unicode, useTrueType)
      {
      }

      public PdfTrueTypeFont(Font font, float size, bool unicode)
        : base(size, (PdfFontStyle) font.Style)
      {
        this.m_unicode = true;
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        this.m_unicode = unicode;
        this.CreateFontInternal(font);
      }

      public PdfTrueTypeFont(string fontFile, float size, PdfFontStyle style)
        : base(size)
      {
        this.m_unicode = true;
        switch (fontFile)
        {
          case null:
            throw new ArgumentNullException(nameof (fontFile));
          case "":
            throw new ArgumentException("fontFile - string can not be empty");
          default:
            this.m_unicode = true;
            this.CreateFontInternal(fontFile, style);
            break;
        }
      }

      internal PdfTrueTypeFont(string fontFile, float size, bool isTrueType)
        : this(fontFile, size, PdfFontStyle.Regular, isTrueType)
      {
      }

      internal PdfTrueTypeFont(Font font, float size, bool unicode, bool useTrueType)
        : base(size, (PdfFontStyle) font.Style)
      {
        this.m_unicode = true;
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        this.m_unicode = unicode;
        this.m_bUseTrueType = useTrueType;
        this.CreateFontInternal(font);
      }

      internal PdfTrueTypeFont(string fontFile, float size, PdfFontStyle style, bool useTrueType)
        : base(size)
      {
        this.m_unicode = true;
        switch (fontFile)
        {
          case null:
            throw new ArgumentNullException(nameof (fontFile));
          case "":
            throw new ArgumentException("fontFile - string can not be empty");
          default:
            this.m_bUseTrueType = useTrueType;
            this.m_unicode = true;
            this.CreateFontInternal(fontFile, style);
            break;
        }
      }

      public PdfTrueTypeFont(Font font, FontStyle style, float size, bool unicode, bool embed)
        : base(size, (PdfFontStyle) style)
      {
        this.m_unicode = true;
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        font = new Font(font.Name, font.Size, style);
        this.m_unicode = unicode;
        this.m_bUseTrueType = true;
        this.m_embed = embed;
        if (unicode && !embed)
          throw new Exception("Unicode font need to be embedded");
        this.CreateFontInternal(font);
      }

      private void CalculateStyle(PdfFontStyle style)
      {
        int macStyle = ((UnicodeTrueTypeFont) this.m_fontInternal).TtfMetrics.MacStyle;
        if ((style & PdfFontStyle.Underline) != PdfFontStyle.Regular)
          macStyle |= 4;
        if ((style & PdfFontStyle.Strikeout) != PdfFontStyle.Regular)
        {
          int num = macStyle | 8;
        }
        this.SetStyle(style);
      }

      private void CreateFontInternal(PdfTrueTypeFont prototype)
      {
        if (prototype == null)
          throw new ArgumentNullException(nameof (prototype));
        if (PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
          this.m_unicode = true;
        this.m_fontInternal = !this.Unicode ? (ITrueTypeFont) new TrueTypeFont(prototype.Font, this.Size) : (ITrueTypeFont) new UnicodeTrueTypeFont(prototype.InternalFont as UnicodeTrueTypeFont);
        this.InitializeInternals();
      }

      private void CreateFontInternal(Font font)
      {
        this.m_fontInternal = this.Unicode || !this.m_bUseTrueType || !this.Embed ? (!this.Unicode || !this.m_bUseTrueType || !this.Embed ? (!this.Unicode || !this.m_bUseTrueType ? (!this.Unicode || this.m_bUseTrueType ? (ITrueTypeFont) new TrueTypeFont(font, this.Size) : (ITrueTypeFont) new UnicodeTrueTypeFont(font, this.Size, CompositeFontType.Type0)) : (ITrueTypeFont) new UnicodeTrueTypeFont(font, this.Size, CompositeFontType.TrueType)) : (ITrueTypeFont) new UnicodeTrueTypeFont(font, this.Size, CompositeFontType.Type0)) : (ITrueTypeFont) new TrueTypeFont(font, this.Size, true);
        this.InitializeInternals();
      }

      private void CreateFontInternal(Stream fontStream, PdfFontStyle style)
      {
        if (fontStream == null)
          throw new ArgumentNullException("fontFile");
        if (!fontStream.CanSeek || !fontStream.CanRead)
          throw new PdfException("Unable to parse the given font stream");
        this.m_fontInternal = this.m_bUseTrueType ? (ITrueTypeFont) new UnicodeTrueTypeFont(fontStream, this.Size, CompositeFontType.TrueType) : (ITrueTypeFont) new UnicodeTrueTypeFont(fontStream, this.Size, CompositeFontType.Type0);
        this.CalculateStyle(style);
        this.InitializeInternals();
      }

      private void CreateFontInternal(string fontFile, PdfFontStyle style)
      {
        switch (fontFile)
        {
          case null:
            throw new ArgumentNullException(nameof (fontFile));
          case "":
            throw new ArgumentException("fontFile - string can not be empty");
          default:
            this.m_fontInternal = this.m_bUseTrueType ? (ITrueTypeFont) new UnicodeTrueTypeFont(fontFile, this.Size, CompositeFontType.TrueType) : (ITrueTypeFont) new UnicodeTrueTypeFont(fontFile, this.Size, CompositeFontType.Type0);
            this.CalculateStyle(style);
            this.InitializeInternals();
            break;
        }
      }

      public void Dispose()
      {
        if (this.m_fontInternal == null)
          return;
        lock (PdfFont.s_syncObject)
        {
          if (!PdfDocument.EnableCache)
            return;
          PdfDocument.Cache.Remove((IPdfCache) this);
          if (PdfDocument.Cache.GroupCount((IPdfCache) this) == 0)
            this.m_fontInternal.Close();
          this.m_fontInternal = (ITrueTypeFont) null;
        }
      }

      protected override bool EqualsToFont(PdfFont font) => this.m_fontInternal.EqualsToFont(font);

      ~PdfTrueTypeFont() => this.Dispose();

      protected internal override float GetCharWidth(char charCode, PdfStringFormat format)
      {
        return (float) this.InternalFont.GetCharWidth(charCode) * (1f / 1000f * this.Metrics.GetSize(format));
      }

      protected internal override float GetLineWidth(string line, PdfStringFormat format)
      {
        float width1 = 0.0f;
        if (format == null || !format.RightToLeft || !this.Unicode)
          width1 = (float) this.InternalFont.GetLineWidth(line);
        else if (!this.GetUnicodeLineWidth(line, out width1))
          width1 = (float) this.InternalFont.GetLineWidth(line);
        float size = this.Metrics.GetSize(format);
        float width2 = width1 * (1f / 1000f * size);
        return this.ApplyFormatSettings(line, format, width2);
      }

      private float GetSymbolSize(char ch, PdfStringFormat format)
      {
        float width = 0.0f;
        if (format == null || !format.RightToLeft || !this.Unicode)
          return this.GetCharWidth(ch, format);
        float size = this.Metrics.GetSize(format);
        this.GetUnicodeLineWidth(new string(ch, 1), out width);
        return width * (1f / 1000f * size);
      }

      private bool GetUnicodeLineWidth(string line, out float width)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        width = 0.0f;
        ushort[] glyphs = (ushort[]) null;
        bool glyphIndices = RtlRenderer.GetGlyphIndices(line, this, false, out glyphs);
        if (glyphIndices && glyphs != null)
        {
          TtfReader ttfReader = (this.InternalFont as UnicodeTrueTypeFont).TtfReader;
          int index = 0;
          for (int length = glyphs.Length; index < length; ++index)
          {
            int glyphIndex = (int) glyphs[index];
            TtfGlyphInfo glyph = ttfReader.GetGlyph(glyphIndex);
            if (!glyph.Empty)
              width += (float) glyph.Width;
          }
        }
        return glyphIndices;
      }

      private void InitializeInternals()
      {
        lock (PdfFont.s_syncObject)
        {
          IPdfCache pdfCache = (IPdfCache) null;
          if (PdfDocument.EnableCache)
            pdfCache = PdfDocument.Cache.Search((IPdfCache) this);
          IPdfPrimitive internals = (IPdfPrimitive) null;
          if (pdfCache != null)
          {
            internals = pdfCache.GetInternals();
            PdfFontMetrics pdfFontMetrics = (PdfFontMetrics) ((PdfFont) pdfCache).Metrics.Clone();
            pdfFontMetrics.Size = this.Size;
            this.Metrics = pdfFontMetrics;
            this.m_fontInternal = ((PdfTrueTypeFont) pdfCache).InternalFont;
          }
          else if (pdfCache == null || this.m_bUseTrueType)
          {
            if (PdfDocument.EnableCache && this.m_bUseTrueType)
              PdfDocument.Cache.Remove(pdfCache);
            this.m_fontInternal.CreateInternals();
            internals = this.m_fontInternal.GetInternals();
            this.Metrics = this.m_fontInternal.Metrics;
          }
          ((IPdfCache) this).SetInternals(internals);
        }
      }

      internal void SetSymbols(string text)
      {
        if (this.m_fontInternal is UnicodeTrueTypeFont)
        {
          if (!(this.m_fontInternal is UnicodeTrueTypeFont fontInternal))
            return;
          fontInternal.SetSymbols(text);
        }
        else
        {
          if (!(this.m_fontInternal is TrueTypeFont fontInternal))
            return;
          fontInternal.SetSymbols(text);
        }
      }

      internal void SetSymbols(ushort[] glyphs)
      {
        if (!(this.m_fontInternal is UnicodeTrueTypeFont fontInternal))
          return;
        fontInternal.SetSymbols(glyphs);
      }

      internal bool Embed => this.m_embed;

      internal Font Font => this.InternalFont.Font;

      internal string FontFile
      {
        get
        {
          string fontFile = (string) null;
          if (this.InternalFont is UnicodeTrueTypeFont internalFont)
            fontFile = internalFont.FontFile;
          return fontFile;
        }
      }

      internal ITrueTypeFont InternalFont => this.m_fontInternal;

      public bool Unicode => this.m_unicode;
    }
}
