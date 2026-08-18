// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.TrueTypeFont
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;
using Syncfusion.Pdf.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace Syncfusion.Pdf.Graphics.Fonts
{
    internal class TrueTypeFont : ITrueTypeFont
    {
      private const string c_boldItalicSuffix = ",BoldItalic";
      private const string c_boldSuffix = ",Bold";
      private const string c_driverName = "DISPLAY";
      private const float c_fontSizeMultiplier = 72000f;
      private const string c_italicSuffix = ",Italic";
      private const string c_nameString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      private bool m_embed;
      private Font m_font;
      private PdfDictionary m_fontDictionary;
      private PdfStream m_fontProgram;
      private PdfFontMetrics m_metrics;
      private OUTLINETEXTMETRIC m_nativeMetrics;
      private float m_size;
      internal TtfMetrics m_ttfMetrics;
      private TtfReader m_ttfReader;
      private Dictionary<char, char> m_usedChars;

      public TrueTypeFont(Font font, float size)
      {
        this.m_font = font != null ? font : throw new ArgumentNullException(nameof (font));
        this.m_size = size;
      }

      public TrueTypeFont(Font font, float size, bool embed)
      {
        this.m_font = font != null ? font : throw new ArgumentException(nameof (font));
        this.m_size = size;
        this.m_embed = embed;
        using (BinaryReader fontData = this.GetFontData())
        {
          this.m_ttfReader = new TtfReader(fontData, this.m_font);
          this.m_ttfMetrics = this.m_ttfReader.Metrics;
          this.m_ttfReader.TrueTypeSubset = true;
        }
      }

      private PdfDictionary CreateDescriptor()
      {
        PdfDictionary descriptor = new PdfDictionary();
        TEXTMETRIC otmTextMetrics = this.m_nativeMetrics.otmTextMetrics;
        descriptor["Type"] = (IPdfPrimitive) new PdfName("FontDescriptor");
        descriptor["FontName"] = (IPdfPrimitive) new PdfName(this.GetFontName());
        descriptor["Flags"] = (IPdfPrimitive) new PdfNumber(this.GetDescriptorFlags());
        descriptor["FontBBox"] = (IPdfPrimitive) PdfArray.FromRectangle(this.GetBoundBox());
        descriptor["MissingWidth"] = (IPdfPrimitive) new PdfNumber(otmTextMetrics.tmAveCharWidth);
        descriptor["StemV"] = (IPdfPrimitive) new PdfNumber(this.m_font.Bold ? 144 /*0x90*/ : 72);
        descriptor["StemH"] = (IPdfPrimitive) new PdfNumber(this.m_font.Bold ? 144 /*0x90*/ : 72);
        descriptor["ItalicAngle"] = (IPdfPrimitive) new PdfNumber(this.m_font.Italic ? this.m_nativeMetrics.otmItalicAngle / 10 : 0);
        if (this.m_embed)
          descriptor["FontFile2"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_fontProgram);
        descriptor["CapHeight"] = (IPdfPrimitive) new PdfNumber((long) this.m_nativeMetrics.otmsCapEmHeight);
        descriptor["XHeight"] = (IPdfPrimitive) new PdfNumber((int) this.m_nativeMetrics.otmsXHeight);
        descriptor["Ascent"] = (IPdfPrimitive) new PdfNumber(this.m_nativeMetrics.otmAscent);
        descriptor["Descent"] = (IPdfPrimitive) new PdfNumber(this.m_nativeMetrics.otmDescent);
        descriptor["Leading"] = (IPdfPrimitive) new PdfNumber(this.m_nativeMetrics.otmMacAscent - this.m_nativeMetrics.otmMacDescent + (int) this.m_nativeMetrics.otmMacLineGap);
        descriptor["MaxWidth"] = (IPdfPrimitive) new PdfNumber(otmTextMetrics.tmMaxCharWidth);
        descriptor["AvgWidth"] = (IPdfPrimitive) new PdfNumber(otmTextMetrics.tmAveCharWidth);
        return descriptor;
      }

      private void CreateFontDictionary(PdfDictionary fontDescriptor)
      {
        if (fontDescriptor == null)
          throw new ArgumentNullException(nameof (fontDescriptor));
        this.m_fontDictionary["Type"] = (IPdfPrimitive) new PdfName("Font");
        this.m_fontDictionary["Subtype"] = (IPdfPrimitive) new PdfName("TrueType");
        this.m_fontDictionary["BaseFont"] = (IPdfPrimitive) new PdfName(this.GetFontName());
        this.m_fontDictionary["FontDescriptor"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) fontDescriptor);
        this.m_fontDictionary["FirstChar"] = (IPdfPrimitive) new PdfNumber(this.m_metrics.FirstChar);
        this.m_fontDictionary["LastChar"] = (IPdfPrimitive) new PdfNumber(this.IsFixedPitch() ? this.m_metrics.FirstChar : this.m_metrics.LastChar);
        this.m_fontDictionary["Widths"] = (IPdfPrimitive) this.m_metrics.WidthTable.ToArray();
        this.m_fontDictionary["Encoding"] = (IPdfPrimitive) new PdfName(Syncfusion.Pdf.Graphics.FontEncoding.WinAnsiEncoding.ToString());
      }

      private void CreateFontMetrics(IntPtr graphicsDC)
      {
        TEXTMETRIC otmTextMetrics = this.m_nativeMetrics.otmTextMetrics;
        this.m_metrics.Ascent = (float) this.m_nativeMetrics.otmMacAscent;
        this.m_metrics.Descent = (float) this.m_nativeMetrics.otmMacDescent;
        this.m_metrics.FirstChar = (int) otmTextMetrics.tmFirstChar;
        this.m_metrics.LastChar = (int) otmTextMetrics.tmLastChar;
        this.m_metrics.Height = (float) ((long) (this.m_nativeMetrics.otmMacAscent - this.m_nativeMetrics.otmMacDescent) + (long) this.m_nativeMetrics.otmMacLineGap);
        this.m_metrics.LineGap = (int) this.m_nativeMetrics.otmMacLineGap;
        this.m_metrics.Size = this.m_size;
        this.m_metrics.WidthTable = (WidthTable) new StandardWidthTable(this.CreateWidthTable(graphicsDC));
        this.m_metrics.PostScriptName = this.GetFontName();
        this.m_metrics.Name = this.m_font.Name;
        this.m_metrics.SubScriptSizeFactor = (float) this.m_nativeMetrics.otmEMSquare / (float) (this.m_nativeMetrics.otmptSubscriptSize.x + this.m_nativeMetrics.otmptSubscriptSize.y);
        this.m_metrics.SuperscriptSizeFactor = (float) this.m_nativeMetrics.otmEMSquare / (float) (this.m_nativeMetrics.otmptSuperscriptSize.x + this.m_nativeMetrics.otmptSuperscriptSize.y);
      }

      private void CreateFontProgram()
      {
        this.m_fontProgram.BeginSave += new SavePdfPrimitiveEventHandler(this.FontProgramBeginSave);
      }

      private int[] CreateWidthTable(IntPtr graphicsDC)
      {
        int firstChar = this.m_metrics.FirstChar;
        int iLastChar = this.IsFixedPitch() ? this.m_metrics.FirstChar : this.m_metrics.LastChar;
        int[] lpBuffer = new int[iLastChar - firstChar + 1];
        if (Environment.OSVersion.Platform >= PlatformID.Win32NT)
        {
          GdiApi.GetCharWidth(graphicsDC, firstChar, iLastChar, lpBuffer);
          return lpBuffer;
        }
        Size empty = Size.Empty;
        int index = 0;
        for (int length = lpBuffer.Length; index < length; ++index)
        {
          string lpString = (firstChar + index).ToString();
          GdiApi.GetTextExtentPoint(graphicsDC, lpString, lpString.Length, ref empty);
          lpBuffer[index] = empty.Width;
        }
        return lpBuffer;
      }

      private void FontProgramBeginSave(object sender, SavePdfPrimitiveEventArgs ars)
      {
        this.GenerateFontProgram();
      }

      private string FormatName(string fontName)
      {
        if (fontName == null)
          throw new ArgumentNullException(nameof (fontName));
        StringBuilder stringBuilder = new StringBuilder();
        byte[] bytes = PdfTrueTypeFont.Encoding.GetBytes(fontName);
        int index = 0;
        for (int length = bytes.Length; index < length; ++index)
        {
          byte byteToTest = bytes[index];
          if (this.IsWordSymbol(byteToTest))
            stringBuilder.Append((char) byteToTest);
          else
            stringBuilder.AppendFormat("#{0:X2}", (object) byteToTest);
        }
        return stringBuilder.ToString();
      }

      private void GenerateFontProgram()
      {
        byte[] numArray = (byte[]) null;
        this.m_usedChars = this.m_usedChars == null ? new Dictionary<char, char>() : this.m_usedChars;
        this.m_ttfReader.InternalReader.Seek(0L);
        byte[] data = this.m_ttfReader.ReadFontProgram(this.m_usedChars);
        this.m_fontProgram["Length1"] = (IPdfPrimitive) new PdfNumber(data.Length);
        this.m_fontProgram.Write(data);
        numArray = (byte[]) null;
      }

      private RectangleF GetBoundBox()
      {
        int left = this.m_nativeMetrics.otmrcFontBox.left;
        int right = this.m_nativeMetrics.otmrcFontBox.right;
        int otmMacDescent = this.m_nativeMetrics.otmMacDescent;
        int num = this.m_nativeMetrics.otmMacAscent + (int) this.m_nativeMetrics.otmMacLineGap;
        return new RectangleF((float) left, (float) otmMacDescent, (float) (right - left), (float) (num - otmMacDescent));
      }

      private int GetDescriptorFlags()
      {
        int descriptorFlags = 0;
        if (this.IsFixedPitch())
          descriptorFlags |= 1;
        if (!this.IsSerif())
          descriptorFlags |= 2;
        if (this.IsSymbolic())
          descriptorFlags |= 4;
        if (this.IsScript())
          descriptorFlags |= 8;
        if (!this.IsSymbolic())
          descriptorFlags |= 32 /*0x20*/;
        if (this.m_font.Italic)
          descriptorFlags |= 64 /*0x40*/;
        return descriptorFlags;
      }

      private string GetErrorMessage()
      {
        IntPtr num = Marshal.AllocHGlobal(4);
        uint lastError = KernelApi.GetLastError();
        uint length = KernelApi.FormatMessage(FormatMessageFlags.AllocateBuffer | FormatMessageFlags.FromSystem, IntPtr.Zero, lastError, 0U, num, 4U, IntPtr.Zero);
        byte[] destination = new byte[4];
        Marshal.Copy(num, destination, 0, 4);
        int int32 = BitConverter.ToInt32(destination, 0);
        Marshal.FreeHGlobal(num);
        num = new IntPtr(int32);
        byte[] numArray = new byte[(int) length];
        Marshal.Copy(num, numArray, 0, (int) length);
        Marshal.FreeHGlobal(num);
        return Encoding.UTF8.GetString(numArray);
      }

      private BinaryReader GetFontData()
      {
        Stream input = (Stream) null;
        if (this.m_font != null)
          input = this.GetFontData(this.m_font);
        return new BinaryReader(input, TtfReader.Encoding);
      }

      private Stream GetFontData(Font font)
      {
        if (font == null)
          throw new ArgumentNullException(nameof (font));
        if (PdfDocument.Cache.FontData.ContainsKey(font))
          return (Stream) new MemoryStream(PdfDocument.Cache.FontData[font]);
        IntPtr dc = GdiApi.CreateDC("DISPLAY", (string) null, (string) null, IntPtr.Zero);
        IntPtr hfont = font.ToHfont();
        IntPtr hgdiobj = GdiApi.SelectObject(dc, hfont);
        uint fontData = GdiApi.GetFontData(dc, 0U, 0U, (byte[]) null, 0U);
        if (fontData == uint.MaxValue)
        {
          int lastError = (int) KernelApi.GetLastError();
          throw new PdfException("Can't parse the font");
        }
        byte[] numArray = new byte[(int) fontData];
        if (GdiApi.GetFontData(dc, 0U, 0U, numArray, fontData) == uint.MaxValue)
        {
          int lastError = (int) KernelApi.GetLastError();
          throw new PdfException("Can't parse the font");
        }
        GdiApi.SelectObject(dc, hgdiobj);
        GdiApi.DeleteObject(hfont);
        GdiApi.DeleteDC(dc);
        PdfDocument.Cache.FontData.Add(font, numArray);
        return (Stream) new MemoryStream(numArray, 0, numArray.Length, false);
      }

      private string GetFontName()
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (this.m_embed)
        {
          Random random = new Random();
          for (int index1 = 0; index1 < 6; ++index1)
          {
            int index2 = random.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length);
            stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[index2]);
          }
          stringBuilder.Append('+');
        }
        stringBuilder.Append(this.FormatName(this.m_font.Name));
        if (this.m_font.Bold && this.m_font.Italic)
          stringBuilder.Append(",BoldItalic");
        else if (this.m_font.Bold)
          stringBuilder.Append(",Bold");
        else if (this.m_font.Italic)
          stringBuilder.Append(",Italic");
        return stringBuilder.ToString();
      }

      private bool IsFixedPitch()
      {
        return ((int) this.m_nativeMetrics.otmTextMetrics.tmPitchAndFamily & 1) != 1;
      }

      private bool IsScript()
      {
        return ((int) this.m_nativeMetrics.otmTextMetrics.tmPitchAndFamily & 64 /*0x40*/) == 64 /*0x40*/;
      }

      private bool IsSerif()
      {
        return ((int) this.m_nativeMetrics.otmTextMetrics.tmPitchAndFamily & 32 /*0x20*/) == 32 /*0x20*/;
      }

      private bool IsSymbolic() => this.m_nativeMetrics.otmTextMetrics.tmCharSet == (byte) 2;

      private bool IsWordSymbol(byte byteToTest) => this.IsWordSymbol((char) byteToTest);

      private bool IsWordSymbol(char chToTest) => char.IsLetter(chToTest) || char.IsNumber(chToTest);

      private void RetrieveFontData()
      {
        this.m_nativeMetrics = new OUTLINETEXTMETRIC();
        this.m_nativeMetrics.otmSize = (uint) Marshal.SizeOf(typeof (OUTLINETEXTMETRIC));
        using (Bitmap bitmap = new Bitmap(1, 1))
        {
          using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage((Image) bitmap))
          {
            Font font = new Font(this.m_font.Name, 72000f / graphics.DpiX, this.m_font.Style, GraphicsUnit.Point);
            IntPtr hdc = graphics.GetHdc();
            IntPtr hfont = font.ToHfont();
            IntPtr hgdiobj = GdiApi.SelectObject(hdc, hfont);
            if (GdiApi.GetOutlineTextMetrics(hdc, (int) this.m_nativeMetrics.otmSize, ref this.m_nativeMetrics) != 0)
              this.CreateFontMetrics(hdc);
            else
              this.GetErrorMessage();
            GdiApi.SelectObject(hdc, hgdiobj);
            GdiApi.DeleteObject(hfont);
            graphics.ReleaseHdc(hdc);
            font.Dispose();
            this.m_nativeMetrics.otmTextMetrics.tmAveCharWidth = ((ITrueTypeFont) this).GetCharWidth(' ');
          }
        }
      }

      public void SetSymbols(string text)
      {
        if (text == null)
          throw new ArgumentNullException(nameof (text));
        if (this.m_usedChars == null)
          this.m_usedChars = new Dictionary<char, char>();
        for (int index = 0; index < text.Length; ++index)
          this.m_usedChars[text[index]] = char.MinValue;
      }

      void ITrueTypeFont.Close()
      {
        if (this.m_fontDictionary != null)
        {
          this.m_fontDictionary.Clear();
          this.m_fontDictionary = (PdfDictionary) null;
        }
        if (this.m_fontProgram != null)
        {
          this.m_fontProgram.Clear();
          this.m_fontProgram = (PdfStream) null;
        }
        if (this.m_ttfReader != null)
        {
          this.m_ttfReader.Close();
          this.m_ttfReader = (TtfReader) null;
        }
        if (this.m_usedChars != null)
        {
          this.m_usedChars.Clear();
          this.m_usedChars = (Dictionary<char, char>) null;
        }
        this.m_font = (Font) null;
        this.m_metrics = (PdfFontMetrics) null;
      }

      void ITrueTypeFont.CreateInternals()
      {
        this.m_fontDictionary = new PdfDictionary();
        this.m_metrics = new PdfFontMetrics();
        this.m_fontProgram = new PdfStream();
        this.RetrieveFontData();
        this.CreateFontDictionary(this.CreateDescriptor());
        if (!this.m_embed)
          return;
        this.m_ttfReader.Reader = this.GetFontData();
        this.m_ttfReader.CreateInternals();
        this.m_ttfMetrics = this.m_ttfReader.Metrics;
        this.CreateFontProgram();
      }

      bool ITrueTypeFont.EqualsToFont(PdfFont font)
      {
        bool font1 = false;
        if (font is PdfTrueTypeFont pdfTrueTypeFont && !pdfTrueTypeFont.Unicode && pdfTrueTypeFont.InternalFont.Font != null && this.m_font != null)
          font1 = this.m_font.Name.Equals(pdfTrueTypeFont.InternalFont.Metrics.Name) & (this.m_font.Style & ~(FontStyle.Underline | FontStyle.Strikeout)) == (pdfTrueTypeFont.InternalFont.Font.Style & ~(FontStyle.Underline | FontStyle.Strikeout));
        return font1;
      }

      int ITrueTypeFont.GetCharWidth(char charCode)
      {
        if (charCode > 'ÿ')
          throw new PdfException("Couldn't find information about the character. Unicode is not supported by this font.");
        int index = (int) charCode - (int) this.m_nativeMetrics.otmTextMetrics.tmFirstChar;
        WidthTable widthTable = this.m_metrics.WidthTable;
        return index >= 0 && index < (widthTable as StandardWidthTable).Length ? widthTable[index] : this.m_nativeMetrics.otmTextMetrics.tmAveCharWidth;
      }

      IPdfPrimitive ITrueTypeFont.GetInternals() => (IPdfPrimitive) this.m_fontDictionary;

      int ITrueTypeFont.GetLineWidth(string line)
      {
        if (line == null)
          throw new ArgumentNullException(nameof (line));
        int lineWidth = 0;
        int index = 0;
        for (int length = line.Length; index < length; ++index)
        {
          int charWidth = ((ITrueTypeFont) this).GetCharWidth(line[index]);
          lineWidth += charWidth;
        }
        return lineWidth;
      }

      Font ITrueTypeFont.Font => this.m_font;

      PdfFontMetrics ITrueTypeFont.Metrics => this.m_metrics;

      float ITrueTypeFont.Size => this.m_size;

      internal TtfReader TtfReader => this.m_ttfReader;
    }
}
