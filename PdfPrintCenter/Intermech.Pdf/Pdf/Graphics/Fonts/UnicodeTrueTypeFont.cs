// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.UnicodeTrueTypeFont
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
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal class UnicodeTrueTypeFont : ITrueTypeFont
{
  private const int c_cidStreamLength = 11;
  private const string c_cmapBeginRange = "beginbfrange\r\n";
  private const string c_cmapEndCodespaceRange = "endcodespacerange\r\n";
  private const string c_cmapEndRange = "endbfrange\r\n";
  private const int c_cmapNextRangeValue = 100;
  private const string c_cmapPrefix = "/CIDInit /ProcSet findresource begin\n12 dict begin\nbegincmap\r\n/CIDSystemInfo << /Registry (Adobe)/Ordering (UCS)/Supplement 0>> def\n/CMapName /Adobe-Identity-UCS def\n/CMapType 2 def\n1 begincodespacerange\r\n";
  private const string c_cmapSuffix = "endbfrange\nendcmap\nCMapName currentdict /CMap defineresource pop\nend end\r\n";
  private const int c_defWidthIndex = 32 /*0x20*/;
  private const string c_driverName = "DISPLAY";
  private const string c_nameString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
  private const string c_registry = "Adobe";
  private PdfStream m_cmap;
  private PdfDictionary m_descendantFont;
  private string m_filePath;
  private Font m_font;
  private PdfDictionary m_fontDictionary;
  private PdfStream m_fontProgram;
  private Stream m_fontStream;
  private PdfFontMetrics m_metrics;
  private float m_size;
  private string m_subsetName;
  internal TtfMetrics m_ttfMetrics;
  private TtfReader m_ttfReader;
  private CompositeFontType m_type;
  private Dictionary<char, char> m_usedChars;
  private static object s_syncLock = new object();

  public UnicodeTrueTypeFont(UnicodeTrueTypeFont prototype)
  {
    this.m_ttfMetrics = prototype != null ? prototype.TtfMetrics : throw new ArgumentNullException(nameof (prototype));
    this.m_font = ((ITrueTypeFont) prototype).Font;
    this.m_filePath = prototype.FontFile;
    this.m_size = ((ITrueTypeFont) prototype).Size;
  }

  public UnicodeTrueTypeFont(Font font, float size, CompositeFontType type)
  {
    this.m_font = font != null ? font : throw new ArgumentNullException(nameof (font));
    this.m_size = size;
    this.m_type = type;
    this.Initialize();
  }

  public UnicodeTrueTypeFont(Stream font, float size, CompositeFontType type)
  {
    this.m_fontStream = font != null ? font : throw new ArgumentNullException(nameof (font));
    this.m_size = size;
    this.m_type = type;
    byte[] buffer = new byte[font.Length];
    font.Read(buffer, 0, buffer.Length);
    using (MemoryStream font1 = new MemoryStream(buffer))
      this.Initialize((Stream) font1);
  }

  public UnicodeTrueTypeFont(string filePath, float size, CompositeFontType type)
  {
    switch (filePath)
    {
      case null:
        throw new ArgumentNullException(nameof (filePath));
      case "":
        throw new ArgumentException("filePath - string can not be empty");
      default:
        this.m_filePath = filePath;
        this.m_size = size;
        this.m_type = type;
        this.Initialize();
        break;
    }
  }

  private void CmapBeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.GenerateCmap();

  private void CreateCmap()
  {
    this.m_cmap.BeginSave += new SavePdfPrimitiveEventHandler(this.CmapBeginSave);
  }

  private void CreateDescendantFont()
  {
    this.m_descendantFont.BeginSave += new SavePdfPrimitiveEventHandler(this.DescendantFontBeginSave);
    this.m_descendantFont["Type"] = (IPdfPrimitive) new PdfName("Font");
    this.m_descendantFont["Subtype"] = (IPdfPrimitive) new PdfName("CIDFontType2");
    this.m_descendantFont["BaseFont"] = (IPdfPrimitive) new PdfName(this.m_subsetName);
    this.m_descendantFont["CIDToGIDMap"] = (IPdfPrimitive) new PdfName("Identity");
    this.m_descendantFont["DW"] = (IPdfPrimitive) new PdfNumber(1000);
    IPdfPrimitive fontDescriptor = this.CreateFontDescriptor();
    byte[] data = new byte[11];
    PdfStream pdfStream = new PdfStream();
    pdfStream.Write(data);
    pdfStream.SetProperty("Filter", (IPdfPrimitive) new PdfName("FlateDecode"));
    (fontDescriptor as PdfDictionary).Items.Add(new PdfName("CIDSet"), (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) pdfStream));
    this.m_descendantFont["FontDescriptor"] = (IPdfPrimitive) new PdfReferenceHolder(fontDescriptor);
    this.m_descendantFont["CIDSystemInfo"] = this.CreateSystemInfo();
  }

  private IPdfPrimitive CreateFontDescriptor()
  {
    PdfDictionary fontDescriptor = new PdfDictionary();
    TtfMetrics metrics = this.m_ttfReader.Metrics;
    fontDescriptor["Type"] = (IPdfPrimitive) new PdfName("FontDescriptor");
    fontDescriptor["FontName"] = (IPdfPrimitive) new PdfName(this.m_subsetName);
    fontDescriptor["Flags"] = (IPdfPrimitive) new PdfNumber(this.GetDescriptorFlags());
    fontDescriptor["FontBBox"] = (IPdfPrimitive) PdfArray.FromRectangle(this.GetBoundBox());
    fontDescriptor["MissingWidth"] = (IPdfPrimitive) new PdfNumber(metrics.WidthTable[32 /*0x20*/]);
    fontDescriptor["StemV"] = (IPdfPrimitive) new PdfNumber((int) metrics.StemV);
    fontDescriptor["ItalicAngle"] = (IPdfPrimitive) new PdfNumber((int) metrics.ItalicAngle);
    fontDescriptor["CapHeight"] = (IPdfPrimitive) new PdfNumber((int) metrics.CapHeight);
    fontDescriptor["Ascent"] = (IPdfPrimitive) new PdfNumber((int) metrics.WinAscent);
    fontDescriptor["Descent"] = (IPdfPrimitive) new PdfNumber((int) metrics.WinDescent);
    fontDescriptor["Leading"] = (IPdfPrimitive) new PdfNumber((int) metrics.Leading);
    fontDescriptor["AvgWidth"] = (IPdfPrimitive) new PdfNumber(metrics.WidthTable[32 /*0x20*/]);
    fontDescriptor["FontFile2"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_fontProgram);
    fontDescriptor["MaxWidth"] = (IPdfPrimitive) new PdfNumber(metrics.WidthTable[32 /*0x20*/]);
    fontDescriptor["XHeight"] = (IPdfPrimitive) new PdfNumber(0);
    fontDescriptor["StemH"] = (IPdfPrimitive) new PdfNumber(0);
    return (IPdfPrimitive) fontDescriptor;
  }

  private void CreateFontDictionary()
  {
    this.m_fontDictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.FontDictionaryBeginSave);
    this.m_fontDictionary["Type"] = (IPdfPrimitive) new PdfName("Font");
    this.m_fontDictionary["BaseFont"] = (IPdfPrimitive) new PdfName(this.m_subsetName);
    if (this.m_type == CompositeFontType.Type0)
    {
      this.m_fontDictionary["Subtype"] = (IPdfPrimitive) new PdfName("Type0");
      this.m_fontDictionary["Encoding"] = (IPdfPrimitive) new PdfName("Identity-H");
      this.m_fontDictionary["DescendantFonts"] = (IPdfPrimitive) new PdfArray()
      {
        (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_descendantFont)
      };
    }
    else
    {
      this.m_fontDictionary["Subtype"] = (IPdfPrimitive) new PdfName("TrueType");
      this.m_fontDictionary["Encoding"] = (IPdfPrimitive) new PdfName("WinAnsiEncoding");
      this.m_fontDictionary["Widths"] = (IPdfPrimitive) new PdfArray(this.m_ttfMetrics.WidthTable);
      this.m_fontDictionary["FirstChar"] = (IPdfPrimitive) new PdfNumber(0);
      this.m_fontDictionary["LastChar"] = (IPdfPrimitive) new PdfNumber((int) byte.MaxValue);
      this.m_fontDictionary["FontDescriptor"] = (IPdfPrimitive) new PdfReferenceHolder(this.CreateFontDescriptor());
    }
  }

  private void CreateFontProgram()
  {
    this.m_fontProgram.BeginSave += new SavePdfPrimitiveEventHandler(this.FontProgramBeginSave);
  }

  private IPdfPrimitive CreateSystemInfo()
  {
    return (IPdfPrimitive) new PdfDictionary()
    {
      ["Registry"] = (IPdfPrimitive) new PdfString("Adobe"),
      ["Ordering"] = (IPdfPrimitive) new PdfString("Identity"),
      ["Supplement"] = (IPdfPrimitive) new PdfNumber(0)
    };
  }

  private void DescendantFontBeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    if (this.m_usedChars == null || this.m_usedChars.Count <= 0)
      return;
    PdfArray descendantWidth = this.GetDescendantWidth();
    if (descendantWidth == null)
      return;
    this.m_descendantFont["W"] = (IPdfPrimitive) descendantWidth;
  }

  private void FontDictionaryBeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    if (this.m_usedChars == null || this.m_usedChars.Count <= 0 || this.m_fontDictionary.ContainsKey("ToUnicode"))
      return;
    this.m_fontDictionary["ToUnicode"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfPrimitive) this.m_cmap);
  }

  private void FontProgramBeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    this.GenerateFontProgram();
  }

  private string FormatName(string fontName)
  {
    if (fontName == null)
      throw new ArgumentNullException(nameof (fontName));
    return !(fontName == string.Empty) ? fontName.Replace("(", "#28").Replace(")", "#29").Replace("[", "#5B").Replace("]", "#5D").Replace("<", "#3C").Replace(">", "#3E").Replace("{", "#7B").Replace("}", "#7D").Replace("/", "#2F").Replace("%", "#25").Replace(" ", "#20") : throw new ArgumentOutOfRangeException(nameof (fontName), "Parameter can not be empty");
  }

  private void GenerateCmap()
  {
    if (this.m_usedChars == null || this.m_usedChars.Count <= 0)
      return;
    Dictionary<int, int> glyphChars = this.m_ttfReader.GetGlyphChars(this.m_usedChars);
    if (glyphChars.Count <= 0)
      return;
    int[] array = new int[glyphChars.Count];
    glyphChars.Keys.CopyTo(array, 0);
    Array.Sort<int>(array);
    List<int> intList = new List<int>(glyphChars.Keys.Count);
    intList.AddRange((IEnumerable<int>) glyphChars.Keys);
    Array.Sort<int>(intList.ToArray());
    int n1 = array[0];
    int n2 = array[array.Length - 1];
    string str = $"{this.ToHexString(n1)}{this.ToHexString(n2)}\r\n";
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("/CIDInit /ProcSet findresource begin\n12 dict begin\nbegincmap\r\n/CIDSystemInfo << /Registry (Adobe)/Ordering (UCS)/Supplement 0>> def\n/CMapName /Adobe-Identity-UCS def\n/CMapType 2 def\n1 begincodespacerange\r\n");
    stringBuilder.Append(str);
    stringBuilder.Append("endcodespacerange\r\n");
    int num = 0;
    int index = 0;
    for (int length = array.Length; index < length; ++index)
    {
      if (num == 0)
      {
        if (index != 0)
          stringBuilder.Append("endbfrange\r\n");
        num = Math.Min(100, array.Length - index);
        stringBuilder.Append(num);
        stringBuilder.Append(" ");
        stringBuilder.Append("beginbfrange\r\n");
      }
      --num;
      int key = array[index];
      stringBuilder.AppendFormat("<{0:X04}><{0:X04}><{1:X04}>\n", (object) key, (object) glyphChars[key]);
    }
    stringBuilder.Append("endbfrange\nendcmap\nCMapName currentdict /CMap defineresource pop\nend end\r\n");
    this.m_cmap.Clear();
    this.m_cmap.Write(stringBuilder.ToString());
  }

  private void GenerateFontProgram()
  {
    this.m_usedChars = this.m_usedChars == null ? new Dictionary<char, char>() : this.m_usedChars;
    this.m_ttfReader.InternalReader.Seek(0L);
    byte[] numArray;
    if (this.m_type == CompositeFontType.Type0 && this.m_ttfReader.Font != null)
    {
      numArray = this.m_ttfReader.ReadFontProgram(this.m_usedChars);
    }
    else
    {
      Stream baseStream = this.GetFontData().BaseStream;
      numArray = new byte[baseStream.Length];
      this.m_fontProgram["Length1"] = (IPdfPrimitive) new PdfNumber(numArray.Length);
      baseStream.Read(numArray, 0, (int) baseStream.Length - 1);
      baseStream.Dispose();
    }
    this.m_fontProgram.Clear();
    this.m_fontProgram.Write(numArray);
  }

  private RectangleF GetBoundBox()
  {
    RECT fontBox = this.m_ttfReader.Metrics.FontBox;
    int width = Math.Abs(fontBox.right - fontBox.left);
    int height = Math.Abs(fontBox.top - fontBox.bottom);
    return new RectangleF((float) fontBox.left, (float) fontBox.bottom, (float) width, (float) height);
  }

  public PdfArray GetDescendantWidth()
  {
    lock (UnicodeTrueTypeFont.s_syncLock)
    {
      PdfArray descendantWidth = (PdfArray) null;
      if (this.m_usedChars != null && this.m_usedChars.Count > 0)
      {
        descendantWidth = new PdfArray();
        List<TtfGlyphInfo> ttfGlyphInfoList = new List<TtfGlyphInfo>();
        foreach (KeyValuePair<char, char> usedChar in this.m_usedChars)
        {
          TtfGlyphInfo glyph = this.m_ttfReader.GetGlyph(usedChar.Key);
          if (!glyph.Empty)
            ttfGlyphInfoList.Add(glyph);
        }
        ttfGlyphInfoList.Sort();
        int num1 = 0;
        int num2 = 0;
        bool flag = false;
        PdfArray element = new PdfArray();
        int index = 0;
        for (int count = ttfGlyphInfoList.Count; index < count; ++index)
        {
          TtfGlyphInfo ttfGlyphInfo = ttfGlyphInfoList[index];
          if (!flag)
          {
            flag = true;
            num1 = ttfGlyphInfo.Index;
            num2 = ttfGlyphInfo.Index - 1;
          }
          if ((num2 + 1 != ttfGlyphInfo.Index || index + 1 == count) && count > 1)
          {
            descendantWidth.Add((IPdfPrimitive) new PdfNumber(num1));
            if (index != 0)
              descendantWidth.Add((IPdfPrimitive) element);
            num1 = ttfGlyphInfo.Index;
            element = new PdfArray();
          }
          element.Add((IPdfPrimitive) new PdfNumber(ttfGlyphInfo.Width));
          if (index + 1 == count)
          {
            descendantWidth.Add((IPdfPrimitive) new PdfNumber(num1));
            descendantWidth.Add((IPdfPrimitive) element);
          }
          num2 = ttfGlyphInfo.Index;
        }
      }
      return descendantWidth;
    }
  }

  private int GetDescriptorFlags()
  {
    int num = 0;
    TtfMetrics metrics = this.m_ttfReader.Metrics;
    if (metrics.IsFixedPitch)
      num |= 1;
    int descriptorFlags = !metrics.IsSymbol ? num | 32 /*0x20*/ : num | 4;
    if (metrics.IsItalic)
      descriptorFlags |= 64 /*0x40*/;
    if (metrics.IsBold)
      descriptorFlags |= 262144 /*0x040000*/;
    return descriptorFlags;
  }

  private BinaryReader GetFontData()
  {
    Stream input;
    if (this.m_font != null)
      input = this.GetFontData(this.m_font);
    else if (this.m_fontStream != null)
    {
      input = this.m_fontStream;
      if (input.CanRead)
        input.Position = 0L;
    }
    else
    {
      try
      {
        input = (Stream) new FileStream(this.m_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
      }
      catch (Exception ex)
      {
        throw new Exception($"Cannot open file: {this.m_filePath} for reading.");
      }
    }
    return new BinaryReader(input, TtfReader.Encoding);
  }

  private Stream GetFontData(Font font)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    if (PdfDocument.EnableCache && PdfDocument.Cache.FontData.ContainsKey(font))
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
    if (PdfDocument.EnableCache)
    {
      lock (PdfDocument.Cache)
      {
        if (!PdfDocument.Cache.FontData.ContainsKey(font))
          PdfDocument.Cache.FontData.Add(font, numArray);
      }
    }
    return (Stream) new MemoryStream(numArray, 0, numArray.Length, false);
  }

  private string GetFontName()
  {
    StringBuilder stringBuilder = new StringBuilder();
    Random random = new Random();
    if (this.m_type == CompositeFontType.Type0)
    {
      for (int index1 = 0; index1 < 6; ++index1)
      {
        int index2 = random.Next("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Length);
        stringBuilder.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ"[index2]);
      }
      stringBuilder.Append('+');
      stringBuilder.Append(this.m_ttfReader.Metrics.PostScriptName);
    }
    else
      stringBuilder.Append(this.m_ttfReader.Metrics.PostScriptName);
    return this.FormatName(stringBuilder.ToString());
  }

  private void Initialize()
  {
    using (BinaryReader fontData = this.GetFontData())
    {
      this.m_ttfReader = new TtfReader(fontData, this.m_font);
      this.m_ttfMetrics = this.m_ttfReader.Metrics;
    }
  }

  private void Initialize(Stream font)
  {
    using (BinaryReader reader = new BinaryReader(font, TtfReader.Encoding))
    {
      this.m_ttfReader = new TtfReader(reader);
      this.m_ttfMetrics = this.m_ttfReader.Metrics;
    }
  }

  private void InitializeMetrics()
  {
    TtfMetrics metrics = this.m_ttfReader.Metrics;
    this.m_metrics.Ascent = metrics.MacAscent;
    this.m_metrics.Descent = metrics.MacDescent;
    this.m_metrics.Height = metrics.MacAscent - metrics.MacDescent + (float) metrics.LineGap;
    this.m_metrics.Name = metrics.FontFamily;
    this.m_metrics.PostScriptName = metrics.PostScriptName;
    this.m_metrics.Size = this.m_size;
    this.m_metrics.WidthTable = (WidthTable) new StandardWidthTable(metrics.WidthTable);
    this.m_metrics.LineGap = metrics.LineGap;
    this.m_metrics.SubScriptSizeFactor = metrics.SubScriptSizeFactor;
    this.m_metrics.SuperscriptSizeFactor = metrics.SuperscriptSizeFactor;
  }

  public void SetSymbols(ushort[] glyphs)
  {
    if (glyphs == null)
      throw new ArgumentNullException(nameof (glyphs));
    if (this.m_usedChars == null)
      this.m_usedChars = new Dictionary<char, char>();
    for (int index = 0; index < glyphs.Length; ++index)
    {
      TtfGlyphInfo glyph = this.m_ttfReader.GetGlyph((int) glyphs[index]);
      if (!glyph.Empty)
        this.m_usedChars[(char) glyph.CharCode] = char.MinValue;
    }
    this.GetDescendantWidth();
  }

  public void SetSymbols(string text)
  {
    lock (PdfDocument.Cache)
    {
      if (text == null)
        throw new ArgumentNullException(nameof (text));
      if (this.m_usedChars == null)
        this.m_usedChars = new Dictionary<char, char>();
      for (int index = 0; index < text.Length; ++index)
        this.m_usedChars[text[index]] = char.MinValue;
      this.GetDescendantWidth();
    }
  }

  void ITrueTypeFont.Close()
  {
    if (this.m_fontDictionary != null)
    {
      this.m_fontDictionary.Clear();
      this.m_fontDictionary = (PdfDictionary) null;
    }
    if (this.m_descendantFont != null)
    {
      this.m_descendantFont.Clear();
      this.m_descendantFont = (PdfDictionary) null;
    }
    if (this.m_fontProgram != null)
    {
      this.m_fontProgram.Clear();
      this.m_fontProgram = (PdfStream) null;
    }
    if (this.m_cmap != null)
    {
      this.m_cmap.Clear();
      this.m_cmap = (PdfStream) null;
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
    this.m_filePath = (string) null;
    this.m_metrics = (PdfFontMetrics) null;
    this.m_subsetName = (string) null;
  }

  void ITrueTypeFont.CreateInternals()
  {
    this.m_fontDictionary = new PdfDictionary();
    this.m_fontProgram = new PdfStream();
    this.m_cmap = new PdfStream();
    this.m_descendantFont = new PdfDictionary();
    this.m_metrics = new PdfFontMetrics();
    this.m_ttfReader.Reader = this.GetFontData();
    this.m_ttfReader.CreateInternals();
    this.m_ttfMetrics = this.m_ttfReader.Metrics;
    this.InitializeMetrics();
    this.m_subsetName = this.GetFontName();
    this.CreateDescendantFont();
    this.CreateCmap();
    this.CreateFontDictionary();
    this.CreateFontProgram();
  }

  bool ITrueTypeFont.EqualsToFont(PdfFont font)
  {
    bool font1 = false;
    if (!(font is PdfTrueTypeFont pdfTrueTypeFont) || !pdfTrueTypeFont.Unicode)
      return font1;
    bool flag1;
    bool flag2;
    if (this.m_font != null && pdfTrueTypeFont.InternalFont.Font != null)
    {
      flag1 = this.m_font.Name.Equals(pdfTrueTypeFont.InternalFont.Font.Name);
      flag2 = this.m_font.Style == pdfTrueTypeFont.InternalFont.Font.Style;
    }
    else
    {
      UnicodeTrueTypeFont internalFont = (UnicodeTrueTypeFont) pdfTrueTypeFont.InternalFont;
      flag1 = this.m_ttfMetrics.FontFamily.Equals(internalFont.m_ttfMetrics.FontFamily);
      flag2 = this.m_ttfMetrics.MacStyle == internalFont.m_ttfMetrics.MacStyle;
    }
    return flag1 & flag2;
  }

  int ITrueTypeFont.GetCharWidth(char charCode) => this.m_ttfReader.GetCharWidth(charCode);

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

  private string ToHexString(int n)
  {
    string str = Convert.ToString(n, 16 /*0x10*/);
    return $"{"<0000".Substring(0, 5 - str.Length)}{str}>";
  }

  internal string FontFile => this.m_filePath;

  internal CompositeFontType FontType
  {
    get => this.m_type;
    set => this.m_type = value;
  }

  Font ITrueTypeFont.Font => this.m_font;

  PdfFontMetrics ITrueTypeFont.Metrics => this.m_metrics;

  float ITrueTypeFont.Size => this.m_size;

  internal TtfMetrics TtfMetrics => this.m_ttfMetrics;

  internal TtfReader TtfReader => this.m_ttfReader;
}
