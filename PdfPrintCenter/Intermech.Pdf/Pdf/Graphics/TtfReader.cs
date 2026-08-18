// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.TtfReader
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics.Fonts;
using Syncfusion.Pdf.IO;
using Syncfusion.Pdf.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

internal class TtfReader
{
  private const int c_fp = 16 /*0x10*/;
  private const int c_ttfVersion1 = 65536 /*0x010000*/;
  private const int c_ttfVersion2 = 1330926671;
  internal static readonly Encoding Encoding = Encoding.GetEncoding("windows-1252");
  private bool m_bIsLocaShort;
  private Font m_font;
  private long m_lowestPosition;
  private Dictionary<int, TtfGlyphInfo> m_macintosh;
  private Dictionary<int, TtfGlyphInfo> m_macintoshGlyphs;
  private int m_maxMacIndex;
  private TtfMetrics m_metrics;
  private Dictionary<int, TtfGlyphInfo> m_microsoft;
  private Dictionary<int, TtfGlyphInfo> m_microsoftGlyphs;
  private BigEndianReader m_reader;
  private bool m_subset;
  private Dictionary<string, TtfTableInfo> m_tableDirectory;
  private static readonly string[] m_tableNames = new string[10]
  {
    "cmap",
    "cvt ",
    "fpgm",
    "glyf",
    "head",
    "hhea",
    "hmtx",
    "loca",
    "maxp",
    "prep"
  };
  private int[] m_width;
  private static readonly short[] s_entrySelectors = new short[21]
  {
    (short) 0,
    (short) 0,
    (short) 1,
    (short) 1,
    (short) 2,
    (short) 2,
    (short) 2,
    (short) 2,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 3,
    (short) 4,
    (short) 4,
    (short) 4,
    (short) 4,
    (short) 4
  };
  private static readonly string[] s_tableNames = new string[9]
  {
    "cvt ",
    "fpgm",
    "glyf",
    "head",
    "hhea",
    "hmtx",
    "loca",
    "maxp",
    "prep"
  };
  internal const int WidthMultiplier = 1000;

  public TtfReader(BinaryReader reader)
  {
    this.m_reader = reader != null ? new BigEndianReader(reader) : throw new ArgumentNullException(nameof (reader));
    this.Initialize();
  }

  public TtfReader(BinaryReader reader, Font font)
  {
    this.m_reader = reader != null ? new BigEndianReader(reader) : throw new ArgumentNullException(nameof (reader));
    this.m_font = font;
    this.Initialize();
  }

  private void AddGlyph(TtfGlyphInfo glyph, TtfCmapEncoding encoding)
  {
    Dictionary<int, TtfGlyphInfo> dictionary = (Dictionary<int, TtfGlyphInfo>) null;
    switch (encoding)
    {
      case TtfCmapEncoding.Symbol:
      case TtfCmapEncoding.Macintosh:
        dictionary = this.MacintoshGlyphs;
        break;
      case TtfCmapEncoding.Unicode:
        dictionary = this.MicrosoftGlyphs;
        break;
    }
    dictionary[glyph.Index] = glyph;
  }

  private uint Align(uint value) => value;

  private int CalculateCheckSum(byte[] bytes)
  {
    if (bytes == null)
      throw new ArgumentNullException(nameof (bytes));
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    int num6 = 0;
    for (int index1 = (bytes.Length + 1) / 4; num6 < index1; ++num6)
    {
      int num7 = num5;
      byte[] numArray1 = bytes;
      int index2 = num1;
      int num8 = index2 + 1;
      int num9 = (int) numArray1[index2] & (int) byte.MaxValue;
      num5 = num7 + num9;
      int num10 = num4;
      byte[] numArray2 = bytes;
      int index3 = num8;
      int num11 = index3 + 1;
      int num12 = (int) numArray2[index3] & (int) byte.MaxValue;
      num4 = num10 + num12;
      int num13 = num3;
      byte[] numArray3 = bytes;
      int index4 = num11;
      int num14 = index4 + 1;
      int num15 = (int) numArray3[index4] & (int) byte.MaxValue;
      num3 = num13 + num15;
      int num16 = num2;
      byte[] numArray4 = bytes;
      int index5 = num14;
      num1 = index5 + 1;
      int num17 = (int) numArray4[index5] & (int) byte.MaxValue;
      num2 = num16 + num17;
    }
    return num2 + (num3 << 8) + (num4 << 16 /*0x10*/) + (num5 << 24);
  }

  private void CheckPreambula()
  {
    switch (this.m_reader.ReadInt32())
    {
      case 65536 /*0x010000*/:
        break;
      case 1330926671:
        break;
      default:
        throw new PdfException("Can't read TTF font data");
    }
  }

  public void Close()
  {
    if (this.m_reader != null)
    {
      this.m_reader.Close();
      this.m_reader = (BigEndianReader) null;
    }
    if (this.m_tableDirectory != null)
    {
      this.m_tableDirectory.Clear();
      this.m_tableDirectory = (Dictionary<string, TtfTableInfo>) null;
    }
    if (this.m_macintosh != null)
    {
      this.m_macintosh.Clear();
      this.m_macintosh = (Dictionary<int, TtfGlyphInfo>) null;
    }
    if (this.m_microsoft != null)
    {
      this.m_microsoft.Clear();
      this.m_microsoft = (Dictionary<int, TtfGlyphInfo>) null;
    }
    if (this.m_macintoshGlyphs != null)
    {
      this.m_macintoshGlyphs.Clear();
      this.m_macintoshGlyphs = (Dictionary<int, TtfGlyphInfo>) null;
    }
    if (this.m_microsoftGlyphs != null)
    {
      this.m_microsoftGlyphs.Clear();
      this.m_microsoftGlyphs = (Dictionary<int, TtfGlyphInfo>) null;
    }
    this.m_width = (int[]) null;
  }

  private bool CompareArrays(byte[] buff1, byte[] buff2)
  {
    bool flag = false;
    if (buff1.Length == buff2.Length)
    {
      int index = 0;
      while (index < buff2.Length && (int) buff2[index] == (int) buff1[index])
        ++index;
      if (index == buff2.Length)
        flag = true;
    }
    return flag;
  }

  public string ConvertString(string text)
  {
    char[] chArray = text != null ? new char[text.Length] : throw new ArgumentNullException(nameof (text));
    int length1 = 0;
    int index = 0;
    for (int length2 = text.Length; index < length2; ++index)
    {
      TtfGlyphInfo glyph = this.GetGlyph(text[index]);
      if (!glyph.Empty)
        chArray[length1++] = (char) glyph.Index;
    }
    return new string(chArray, 0, length1);
  }

  public void CreateInternals() => this.ReadMetrics();

  private void FixOffsets()
  {
    int num1 = int.MaxValue;
    foreach (KeyValuePair<string, TtfTableInfo> keyValuePair in this.TableDirectory)
    {
      int offset = keyValuePair.Value.Offset;
      if (num1 > offset)
      {
        num1 = offset;
        if ((long) num1 <= this.m_lowestPosition)
          break;
      }
    }
    int num2 = num1 - (int) this.m_lowestPosition;
    if (num2 == 0)
      return;
    Dictionary<string, TtfTableInfo> dictionary = new Dictionary<string, TtfTableInfo>();
    foreach (KeyValuePair<string, TtfTableInfo> keyValuePair in this.TableDirectory)
    {
      TtfTableInfo ttfTableInfo = this.TableDirectory[keyValuePair.Key];
      ttfTableInfo.Offset -= num2;
      dictionary[keyValuePair.Key] = ttfTableInfo;
    }
    this.m_tableDirectory = dictionary;
  }

  private uint FormatTableName(string name)
  {
    byte[] numArray = name != null ? Encoding.UTF8.GetBytes(name) : throw new ArgumentNullException(nameof (name));
    int uint32 = (int) BitConverter.ToUInt32(numArray, 0);
    return (uint) ((int) numArray[3] << 24 | (int) numArray[2] << 16 /*0x10*/ | (int) numArray[1] << 8) | (uint) numArray[0];
  }

  private uint GenerateGlyphTable(
    Dictionary<int, int> glyphChars,
    TtfLocaTable locaTable,
    out int[] newLocaTable,
    out byte[] newGlyphTable)
  {
    if (glyphChars == null)
      throw new ArgumentNullException(nameof (glyphChars));
    newLocaTable = new int[locaTable.Offsets.Length];
    int[] array = new List<int>((IEnumerable<int>) glyphChars.Keys).ToArray();
    Array.Sort<int>(array);
    uint glyphTable = 0;
    int index1 = 0;
    for (int length = array.Length; index1 < length; ++index1)
    {
      int index2 = array[index1];
      if (locaTable.Offsets.Length != 0)
        glyphTable += locaTable.Offsets[index2 + 1] - locaTable.Offsets[index2];
    }
    uint length1 = this.Align(glyphTable);
    newGlyphTable = new byte[(int) length1];
    int index3 = 0;
    int index4 = 0;
    TtfTableInfo table = this.GetTable("glyf");
    int index5 = 0;
    for (int length2 = newLocaTable.Length; index5 < length2; ++index5)
    {
      newLocaTable[index5] = index3;
      if (index4 < array.Length && array[index4] == index5)
      {
        ++index4;
        newLocaTable[index5] = index3;
        int offset = (int) locaTable.Offsets[index5];
        int count = (int) locaTable.Offsets[index5 + 1] - offset;
        if (count > 0)
        {
          this.m_reader.Seek((long) (table.Offset + offset));
          this.m_reader.Read(newGlyphTable, index3, count);
          index3 += count;
        }
      }
    }
    return glyphTable;
  }

  public int GetCharWidth(char code)
  {
    TtfGlyphInfo glyph = this.GetGlyph(code);
    TtfGlyphInfo ttfGlyphInfo = !glyph.Empty ? glyph : this.GetDefaultGlyph();
    return ttfGlyphInfo.Empty ? 0 : ttfGlyphInfo.Width;
  }

  private TtfCmapEncoding GetCmapEncoding(int platformID, int encodingID)
  {
    TtfCmapEncoding cmapEncoding = TtfCmapEncoding.Unknown;
    if (platformID == 3 && encodingID == 0)
      return TtfCmapEncoding.Symbol;
    if (platformID == 3 && encodingID == 1)
      return TtfCmapEncoding.Unicode;
    if (platformID == 1 && encodingID == 0)
      cmapEncoding = TtfCmapEncoding.Macintosh;
    return cmapEncoding;
  }

  private TtfGlyphInfo GetDefaultGlyph() => this.GetGlyph(' ');

  private byte[] GetFontData(Font font, uint tableName)
  {
    if (font == null)
      throw new ArgumentNullException(nameof (font));
    IntPtr dc = GdiApi.CreateDC("DISPLAY", (string) null, (string) null, IntPtr.Zero);
    IntPtr hfont = font.ToHfont();
    IntPtr hgdiobj = GdiApi.SelectObject(dc, hfont);
    uint fontData1 = GdiApi.GetFontData(dc, tableName, 0U, (byte[]) null, 0U);
    if (fontData1 == uint.MaxValue)
    {
      int lastError = (int) KernelApi.GetLastError();
      throw new PdfException("Can't parse the font");
    }
    byte[] lpvBuffer = new byte[(int) fontData1];
    int fontData2 = (int) GdiApi.GetFontData(dc, tableName, 0U, lpvBuffer, fontData1);
    GdiApi.SelectObject(dc, hgdiobj);
    GdiApi.DeleteObject(hfont);
    GdiApi.DeleteDC(hfont);
    GdiApi.DeleteDC(dc);
    return lpvBuffer;
  }

  private byte[] GetFontProgram(
    byte[] newLocaTableOut,
    byte[] newGlyphTable,
    uint glyphTableSize,
    uint locaTableSize)
  {
    if (newLocaTableOut == null)
      throw new ArgumentNullException(nameof (newLocaTableOut));
    if (newGlyphTable == null)
      throw new ArgumentNullException(nameof (newGlyphTable));
    string[] tableNames = this.TableNames;
    short numTables = 0;
    BigEndianWriter writer = new BigEndianWriter(this.GetFontProgramLength(newLocaTableOut, newGlyphTable, out numTables));
    writer.Write(65536 /*0x010000*/);
    writer.Write(numTables);
    short entrySelector = TtfReader.s_entrySelectors[(int) numTables];
    writer.Write((short) ((1 << (int) entrySelector) * 16 /*0x10*/));
    writer.Write(entrySelector);
    writer.Write((short) (((int) numTables - (1 << (int) entrySelector)) * 16 /*0x10*/));
    this.WriteCheckSums(writer, numTables, newLocaTableOut, newGlyphTable, glyphTableSize, locaTableSize);
    this.WriteGlyphs(writer, newLocaTableOut, newGlyphTable);
    return writer.Data;
  }

  private int GetFontProgramLength(
    byte[] newLocaTableOut,
    byte[] newGlyphTable,
    out short numTables)
  {
    if (newLocaTableOut == null)
      throw new ArgumentNullException(nameof (newLocaTableOut));
    if (newGlyphTable == null)
      throw new ArgumentNullException(nameof (newGlyphTable));
    numTables = (short) 2;
    string[] tableNames = this.TableNames;
    int num = 0;
    int index = 0;
    for (int length = tableNames.Length; index < length; ++index)
    {
      string name = tableNames[index];
      if (name != "glyf" && name != "loca")
      {
        TtfTableInfo table = this.GetTable(name);
        if (!table.Empty)
        {
          ++numTables;
          num += (int) this.Align((uint) table.Length);
        }
      }
    }
    return num + newLocaTableOut.Length + newGlyphTable.Length + ((int) numTables * 16 /*0x10*/ + 12);
  }

  public TtfGlyphInfo GetGlyph(char charCode)
  {
    object obj = (object) null;
    int key1 = (int) charCode;
    if (!this.m_metrics.IsSymbol && this.m_microsoft != null)
    {
      if (this.Font != null && (this.Font.Name.ToLower() == "gautami" || this.Font.Name.ToLower() == "latha" || this.Font.Name.ToLower() == "shruti" || this.Font.Name.ToLower() == "mangal" || this.Font.Name.ToLower() == "tunga" || this.Font.Name.ToLower() == "vrinda") && this.m_width.Length > key1)
        return new TtfGlyphInfo()
        {
          CharCode = key1,
          Index = key1,
          Width = this.m_width[key1]
        };
      if (this.m_microsoft.ContainsKey(key1))
        obj = (object) this.m_microsoft[key1];
    }
    else if (this.m_metrics.IsSymbol && this.m_macintosh != null)
    {
      int key2 = key1 % (this.m_maxMacIndex + 1);
      if (this.m_macintosh.ContainsKey(key2))
        obj = (object) this.m_macintosh[key2];
      if (obj == null && PdfDocument.ConformanceLevel == PdfConformanceLevel.Pdf_A1B)
        obj = (object) new TtfGlyphInfo();
    }
    if (charCode == ' ' && obj == null)
      obj = (object) new TtfGlyphInfo();
    return obj == null ? this.GetDefaultGlyph() : (TtfGlyphInfo) obj;
  }

  public TtfGlyphInfo GetGlyph(int glyphIndex)
  {
    object obj = (object) null;
    if (!this.m_metrics.IsSymbol && this.m_microsoftGlyphs != null)
    {
      if (this.m_microsoftGlyphs.ContainsKey(glyphIndex))
        obj = (object) this.m_microsoftGlyphs[glyphIndex];
    }
    else if (this.m_metrics.IsSymbol && this.m_macintoshGlyphs != null && this.m_macintoshGlyphs.ContainsKey(glyphIndex))
      obj = (object) this.m_macintoshGlyphs[glyphIndex];
    return obj == null ? this.GetDefaultGlyph() : (TtfGlyphInfo) obj;
  }

  internal Dictionary<int, int> GetGlyphChars(Dictionary<char, char> chars)
  {
    if (chars == null)
      throw new ArgumentNullException(nameof (chars));
    Dictionary<int, int> glyphChars = new Dictionary<int, int>();
    foreach (KeyValuePair<char, char> keyValuePair in chars)
    {
      char key = keyValuePair.Key;
      TtfGlyphInfo glyph = this.GetGlyph(key);
      if (!glyph.Empty)
        glyphChars[glyph.Index] = (int) key;
    }
    return glyphChars;
  }

  private TtfTableInfo GetTable(string name)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    object obj = (object) null;
    TtfTableInfo table = new TtfTableInfo();
    if (this.TableDirectory.ContainsKey(name))
      obj = (object) this.TableDirectory[name];
    if (obj != null)
      table = (TtfTableInfo) obj;
    return table;
  }

  private int GetWidth(int glyphCode)
  {
    glyphCode = glyphCode < this.m_width.Length ? glyphCode : this.m_width.Length - 1;
    return this.m_width[glyphCode];
  }

  private void Initialize()
  {
    this.ReadFontDirectory();
    TtfNameTable nameTable = this.ReadNameTable();
    TtfHeadTable ttfHeadTable = this.ReadHeadTable();
    this.InitializeFontName(nameTable);
    this.m_metrics.MacStyle = (int) ttfHeadTable.MacStyle;
  }

  private void InitializeFontName(TtfNameTable nameTable)
  {
    for (int index = 0; index < (int) nameTable.RecordsCount; ++index)
    {
      TtfNameRecord nameRecord = nameTable.NameRecords[index];
      if (nameRecord.NameID == (ushort) 1)
        this.m_metrics.FontFamily = nameRecord.Name;
      else if (nameRecord.NameID == (ushort) 6)
        this.m_metrics.PostScriptName = nameRecord.Name;
      if (this.m_metrics.FontFamily != null && this.m_metrics.PostScriptName != null)
        break;
    }
  }

  private void InitializeMetrics(
    TtfNameTable nameTable,
    TtfHeadTable headTable,
    TtfHorizontalHeaderTable horizontalHeadTable,
    TtfOS2Table os2Table,
    TtfPostTable postTable,
    TtfCmapSubTable[] cmapTables)
  {
    if (cmapTables == null)
      throw new ArgumentNullException(nameof (cmapTables));
    this.InitializeFontName(nameTable);
    bool flag = false;
    for (int index = 0; index < cmapTables.Length; ++index)
    {
      TtfCmapSubTable cmapTable = cmapTables[index];
      if (this.GetCmapEncoding((int) cmapTable.PlatformID, (int) cmapTable.EncodingID) == TtfCmapEncoding.Symbol)
      {
        flag = true;
        break;
      }
    }
    this.m_metrics.IsSymbol = flag;
    this.m_metrics.MacStyle = (int) headTable.MacStyle;
    this.m_metrics.IsFixedPitch = postTable.IsFixedPitch > 0U;
    this.m_metrics.ItalicAngle = postTable.ItalicAngle;
    float num = 1000f / (float) headTable.UnitsPerEm;
    this.m_metrics.WinAscent = (float) os2Table.STypoAscender * num;
    this.m_metrics.MacAscent = (float) horizontalHeadTable.Ascender * num;
    this.m_metrics.CapHeight = os2Table.SCapHeight != (short) 0 ? (float) os2Table.SCapHeight : 0.7f * (float) headTable.UnitsPerEm * num;
    this.m_metrics.WinDescent = (float) os2Table.STypoDescender * num;
    this.m_metrics.MacDescent = (float) horizontalHeadTable.Descender * num;
    this.m_metrics.Leading = (float) ((int) os2Table.STypoAscender - (int) os2Table.STypoDescender + (int) os2Table.STypoLineGap) * num;
    this.m_metrics.LineGap = (int) Math.Ceiling((double) horizontalHeadTable.LineGap * (double) num);
    this.m_metrics.FontBox = new RECT((int) ((double) headTable.XMin * (double) num), (int) Math.Ceiling((double) this.m_metrics.MacAscent + (double) this.m_metrics.LineGap), (int) ((double) headTable.XMax * (double) num), (int) this.m_metrics.MacDescent);
    this.m_metrics.StemV = 80f;
    this.m_metrics.WidthTable = this.UpdateWidth();
    this.m_metrics.ContainsCFF = this.TableDirectory.ContainsKey("CFF ");
    this.m_metrics.SubScriptSizeFactor = (float) headTable.UnitsPerEm / (float) os2Table.YSubscriptYSize;
    this.m_metrics.SuperscriptSizeFactor = (float) headTable.UnitsPerEm / (float) os2Table.YSuperscriptYSize;
  }

  private int NormalizeOffset(TtfTableInfo table, string name, BigEndianReader reader)
  {
    if (name == null)
      throw new ArgumentNullException(nameof (name));
    if (reader == null)
      throw new ArgumentNullException(nameof (reader));
    int num = 0;
    if (this.Font != null)
    {
      byte[] fontData = this.GetFontData(this.Font, this.FormatTableName(name));
      if (fontData == null)
        return num;
      int position = (int) reader.BaseStream.Position;
      for (int offset = table.Offset; offset >= 0; offset -= 4)
      {
        reader.BaseStream.Position = (long) offset;
        byte[] buff2 = reader.ReadBytes(table.Length);
        if (this.CompareArrays(fontData, buff2))
        {
          num = offset - table.Offset;
          break;
        }
      }
      reader.BaseStream.Position = (long) position;
    }
    return num;
  }

  private void ProcessCompositeGlyph(
    Dictionary<int, int> glyphChars,
    int glyph,
    TtfLocaTable locaTable)
  {
    if (glyphChars == null)
      throw new ArgumentNullException(nameof (glyphChars));
    if (glyph >= locaTable.Offsets.Length - 1)
      return;
    uint offset = locaTable.Offsets[glyph];
    if ((int) offset == (int) locaTable.Offsets[glyph + 1])
      return;
    this.m_reader.Seek((long) this.GetTable("glyf").Offset + (long) offset);
    if (new TtfGlyphHeader()
    {
      numberOfContours = this.m_reader.ReadInt16(),
      XMin = this.m_reader.ReadInt16(),
      YMin = this.m_reader.ReadInt16(),
      XMax = this.m_reader.ReadInt16(),
      YMax = this.m_reader.ReadInt16()
    }.numberOfContours >= (short) 0)
      return;
    while (true)
    {
      ushort num = this.m_reader.ReadUInt16();
      int key = (int) this.m_reader.ReadUInt16();
      if (!glyphChars.ContainsKey(key))
        glyphChars.Add(key, 0);
      if (((int) num & 32 /*0x20*/) != 0)
      {
        int numBytes = ((int) num & 1) != 0 ? 4 : 2;
        if (((int) num & 8) != 0)
          numBytes += 2;
        else if (((int) num & 64 /*0x40*/) != 0)
          numBytes += 4;
        else if (((int) num & 128 /*0x80*/) != 0)
          numBytes += 8;
        this.m_reader.Skip((long) numBytes);
      }
      else
        break;
    }
  }

  private void ReadAppleCmapTable(TtfCmapSubTable subTable, TtfCmapEncoding encoding)
  {
    this.m_reader.Seek((long) this.GetTable("cmap").Offset + (long) subTable.Offset);
    TtfAppleCmapSubTable appleCmapSubTable = new TtfAppleCmapSubTable()
    {
      Format = this.m_reader.ReadUInt16(),
      Length = this.m_reader.ReadUInt16(),
      Version = this.m_reader.ReadUInt16()
    };
    int num = 0;
    for (int index = 256 /*0x0100*/; num < index; ++num)
    {
      TtfGlyphInfo glyph = new TtfGlyphInfo()
      {
        Index = (int) this.m_reader.ReadByte()
      };
      glyph.Width = this.GetWidth(glyph.Index);
      glyph.CharCode = num;
      this.Macintosh[num] = glyph;
      this.AddGlyph(glyph, encoding);
      this.m_maxMacIndex = Math.Max(num, this.m_maxMacIndex);
    }
  }

  private void ReadCmapSubTable(TtfCmapSubTable subTable)
  {
    this.m_reader.Seek((long) this.GetTable("cmap").Offset + (long) subTable.Offset);
    TtfCmapFormat ttfCmapFormat = (TtfCmapFormat) this.m_reader.ReadUInt16();
    TtfCmapEncoding cmapEncoding = this.GetCmapEncoding((int) subTable.PlatformID, (int) subTable.EncodingID);
    if (cmapEncoding == TtfCmapEncoding.Unknown)
      return;
    switch (ttfCmapFormat)
    {
      case TtfCmapFormat.Apple:
        this.ReadAppleCmapTable(subTable, cmapEncoding);
        break;
      case TtfCmapFormat.Microsoft:
        this.ReadMicrosoftCmapTable(subTable, cmapEncoding);
        break;
      case TtfCmapFormat.Trimmed:
        this.ReadTrimmedCmapTable(subTable, cmapEncoding);
        break;
    }
  }

  private TtfCmapSubTable[] ReadCmapTable()
  {
    this.m_reader.Seek((long) this.GetTable("cmap").Offset);
    TtfCmapTable ttfCmapTable = new TtfCmapTable();
    ttfCmapTable.Version = this.m_reader.ReadUInt16();
    ttfCmapTable.TablesCount = this.m_reader.ReadUInt16();
    long position = this.m_reader.BaseStream.Position;
    TtfCmapSubTable[] ttfCmapSubTableArray = new TtfCmapSubTable[(int) ttfCmapTable.TablesCount];
    for (int index = 0; index < (int) ttfCmapTable.TablesCount; ++index)
    {
      this.m_reader.Seek(position);
      TtfCmapSubTable subTable = new TtfCmapSubTable();
      subTable.PlatformID = this.m_reader.ReadUInt16();
      subTable.EncodingID = this.m_reader.ReadUInt16();
      subTable.Offset = this.m_reader.ReadUInt32();
      position = this.m_reader.BaseStream.Position;
      this.ReadCmapSubTable(subTable);
      ttfCmapSubTableArray[index] = subTable;
    }
    return ttfCmapSubTableArray;
  }

  private void ReadFontDirectory()
  {
    this.m_reader.Seek(0L);
    this.CheckPreambula();
    int num1 = (int) this.m_reader.ReadInt16();
    int num2 = 0;
    bool flag = false;
    this.m_reader.Skip(6L);
    for (int index = 0; index < num1; ++index)
    {
      TtfTableInfo table = new TtfTableInfo();
      string str = this.m_reader.ReadString(4);
      table.Checksum = this.m_reader.ReadInt32();
      table.Offset = this.m_reader.ReadInt32();
      table.Length = this.m_reader.ReadInt32();
      if (this.Font != null)
        this.Font.Name.ToLower();
      if (PdfDocument.EnableCache)
      {
        lock (PdfDocument.Cache)
        {
          if (!flag && this.Font != null && PdfDocument.Cache.FontOffsetTable.ContainsKey(this.Font))
          {
            num2 = PdfDocument.Cache.FontOffsetTable[this.Font];
            flag = true;
          }
          else if (!flag)
          {
            if (this.Font != null)
            {
              if (!PdfDocument.Cache.FontOffsetTable.ContainsKey(this.Font))
              {
                num2 = this.NormalizeOffset(table, str, this.m_reader);
                PdfDocument.Cache.FontOffsetTable.Add(this.Font, num2);
                flag = true;
              }
            }
          }
        }
      }
      else
        num2 = this.NormalizeOffset(table, str, this.m_reader);
      if (num2 != 0)
        table.Offset += num2;
      this.TableDirectory[str] = table;
    }
    if (flag)
      return;
    this.m_lowestPosition = this.m_reader.BaseStream.Position;
    this.FixOffsets();
  }

  public byte[] ReadFontProgram(Dictionary<char, char> chars)
  {
    Dictionary<int, int> glyphChars = this.GetGlyphChars(chars);
    TtfLocaTable locaTable = this.ReadLocaTable(this.m_bIsLocaShort);
    this.UpdateGlyphChars(glyphChars, locaTable);
    int[] newLocaTable = (int[]) null;
    byte[] newGlyphTable = (byte[]) null;
    byte[] newLocaTableOut = (byte[]) null;
    uint glyphTable = this.GenerateGlyphTable(glyphChars, locaTable, out newLocaTable, out newGlyphTable);
    int locaTableSize = this.UpdateLocaTable(newLocaTable, this.m_bIsLocaShort, out newLocaTableOut);
    return this.GetFontProgram(newLocaTableOut, newGlyphTable, glyphTable, (uint) locaTableSize);
  }

  private TtfHeadTable ReadHeadTable()
  {
    this.m_reader.Seek((long) this.GetTable("head").Offset);
    return new TtfHeadTable()
    {
      Version = this.m_reader.ReadFixed(),
      FontRevision = this.m_reader.ReadFixed(),
      CheckSumAdjustment = this.m_reader.ReadUInt32(),
      MagicNumber = this.m_reader.ReadUInt32(),
      Flags = this.m_reader.ReadUInt16(),
      UnitsPerEm = this.m_reader.ReadUInt16(),
      Created = this.m_reader.ReadInt64(),
      Modified = this.m_reader.ReadInt64(),
      XMin = this.m_reader.ReadInt16(),
      YMin = this.m_reader.ReadInt16(),
      XMax = this.m_reader.ReadInt16(),
      YMax = this.m_reader.ReadInt16(),
      MacStyle = this.m_reader.ReadUInt16(),
      LowestRecPPEM = this.m_reader.ReadUInt16(),
      FontDirectionHint = this.m_reader.ReadInt16(),
      IndexToLocFormat = this.m_reader.ReadInt16(),
      GlyphDataFormat = this.m_reader.ReadInt16()
    };
  }

  private TtfHorizontalHeaderTable ReadHorizontalHeaderTable()
  {
    this.m_reader.Seek((long) this.GetTable("hhea").Offset);
    TtfHorizontalHeaderTable horizontalHeaderTable = new TtfHorizontalHeaderTable();
    horizontalHeaderTable.Version = this.m_reader.ReadFixed();
    horizontalHeaderTable.Ascender = this.m_reader.ReadInt16();
    horizontalHeaderTable.Descender = this.m_reader.ReadInt16();
    horizontalHeaderTable.LineGap = this.m_reader.ReadInt16();
    horizontalHeaderTable.AdvanceWidthMax = this.m_reader.ReadUInt16();
    horizontalHeaderTable.MinLeftSideBearing = this.m_reader.ReadInt16();
    horizontalHeaderTable.MinRightSideBearing = this.m_reader.ReadInt16();
    horizontalHeaderTable.XMaxExtent = this.m_reader.ReadInt16();
    horizontalHeaderTable.CaretSlopeRise = this.m_reader.ReadInt16();
    horizontalHeaderTable.CaretSlopeRun = this.m_reader.ReadInt16();
    this.m_reader.Skip(10L);
    horizontalHeaderTable.MetricDataFormat = this.m_reader.ReadInt16();
    horizontalHeaderTable.NumberOfHMetrics = this.m_reader.ReadUInt16();
    return horizontalHeaderTable;
  }

  private TtfLocaTable ReadLocaTable(bool bShort)
  {
    TtfTableInfo table = this.GetTable("loca");
    this.m_reader.Seek((long) table.Offset);
    TtfLocaTable ttfLocaTable = new TtfLocaTable();
    uint[] numArray;
    if (bShort)
    {
      int length = table.Length / 2;
      numArray = new uint[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = (uint) this.m_reader.ReadUInt16() * 2U;
    }
    else
    {
      int length = table.Length / 4;
      numArray = new uint[length];
      for (int index = 0; index < length; ++index)
        numArray[index] = this.m_reader.ReadUInt32();
    }
    ttfLocaTable.Offsets = numArray;
    return ttfLocaTable;
  }

  private void ReadMetrics()
  {
    this.m_metrics = new TtfMetrics();
    TtfNameTable nameTable = this.ReadNameTable();
    TtfHeadTable headTable = this.ReadHeadTable();
    this.m_bIsLocaShort = headTable.IndexToLocFormat == (short) 0;
    TtfHorizontalHeaderTable horizontalHeadTable = this.ReadHorizontalHeaderTable();
    TtfOS2Table os2Table = this.ReadOS2Table();
    TtfPostTable postTable = this.ReadPostTable();
    this.m_width = this.ReadWidthTable((int) horizontalHeadTable.NumberOfHMetrics, (int) headTable.UnitsPerEm);
    TtfCmapSubTable[] cmapTables = this.ReadCmapTable();
    this.InitializeMetrics(nameTable, headTable, horizontalHeadTable, os2Table, postTable, cmapTables);
  }

  private void ReadMicrosoftCmapTable(TtfCmapSubTable subTable, TtfCmapEncoding encoding)
  {
    this.m_reader.Seek((long) this.GetTable("cmap").Offset + (long) subTable.Offset);
    Dictionary<int, TtfGlyphInfo> dictionary = encoding == TtfCmapEncoding.Unicode ? this.Microsoft : this.Macintosh;
    TtfMicrosoftCmapSubTable microsoftCmapSubTable = new TtfMicrosoftCmapSubTable();
    microsoftCmapSubTable.Format = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.Length = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.Version = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.SegCountX2 = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.SearchRange = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.EntrySelector = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.RangeShift = this.m_reader.ReadUInt16();
    int len1 = (int) microsoftCmapSubTable.SegCountX2 / 2;
    microsoftCmapSubTable.EndCount = this.ReadUshortArray(len1);
    microsoftCmapSubTable.ReservedPad = this.m_reader.ReadUInt16();
    microsoftCmapSubTable.StartCount = this.ReadUshortArray(len1);
    microsoftCmapSubTable.IdDelta = this.ReadUshortArray(len1);
    microsoftCmapSubTable.IdRangeOffset = this.ReadUshortArray(len1);
    int len2 = (int) microsoftCmapSubTable.Length / 2 - 8 - len1 * 4;
    microsoftCmapSubTable.GlyphID = this.ReadUshortArray(len2);
    for (int index1 = 0; index1 < len1; ++index1)
    {
      int num1 = (int) microsoftCmapSubTable.StartCount[index1];
      for (int index2 = (int) microsoftCmapSubTable.EndCount[index1]; num1 <= index2 && num1 != (int) ushort.MaxValue; ++num1)
      {
        int num2;
        if (microsoftCmapSubTable.IdRangeOffset[index1] == (ushort) 0)
        {
          num2 = num1 + (int) microsoftCmapSubTable.IdDelta[index1] & (int) ushort.MaxValue;
        }
        else
        {
          int index3 = index1 + (int) microsoftCmapSubTable.IdRangeOffset[index1] / 2 - len1 + num1 - (int) microsoftCmapSubTable.StartCount[index1];
          if (index3 < microsoftCmapSubTable.GlyphID.Length)
            num2 = (int) microsoftCmapSubTable.GlyphID[index3] + (int) microsoftCmapSubTable.IdDelta[index1] & (int) ushort.MaxValue;
          else
            continue;
        }
        TtfGlyphInfo glyph = new TtfGlyphInfo()
        {
          Index = num2
        };
        glyph.Width = this.GetWidth(glyph.Index);
        int key = encoding == TtfCmapEncoding.Symbol ? ((num1 & 65280) == 61440 /*0xF000*/ ? num1 & (int) byte.MaxValue : num1) : num1;
        glyph.CharCode = key;
        dictionary[key] = glyph;
        this.AddGlyph(glyph, encoding);
      }
    }
  }

  private TtfNameTable ReadNameTable()
  {
    TtfTableInfo table = this.GetTable("name");
    this.m_reader.Seek((long) table.Offset);
    TtfNameTable ttfNameTable = new TtfNameTable()
    {
      FormatSelector = this.m_reader.ReadUInt16(),
      RecordsCount = this.m_reader.ReadUInt16(),
      Offset = this.m_reader.ReadUInt16()
    };
    ttfNameTable.NameRecords = new TtfNameRecord[(int) ttfNameTable.RecordsCount];
    long position = this.m_reader.BaseStream.Position;
    int num = 12;
    int index = 0;
    for (int recordsCount = (int) ttfNameTable.RecordsCount; index < recordsCount; ++index)
    {
      this.m_reader.Seek(position);
      TtfNameRecord ttfNameRecord = new TtfNameRecord();
      ttfNameRecord.PlatformID = this.m_reader.ReadUInt16();
      ttfNameRecord.EncodingID = this.m_reader.ReadUInt16();
      ttfNameRecord.LanguageID = this.m_reader.ReadUInt16();
      ttfNameRecord.NameID = this.m_reader.ReadUInt16();
      ttfNameRecord.Length = this.m_reader.ReadUInt16();
      ttfNameRecord.Offset = this.m_reader.ReadUInt16();
      this.m_reader.Seek((long) (table.Offset + (int) ttfNameTable.Offset + (int) ttfNameRecord.Offset));
      bool unicode = ttfNameRecord.PlatformID == (ushort) 0 || ttfNameRecord.PlatformID == (ushort) 3;
      ttfNameRecord.Name = this.m_reader.ReadString((int) ttfNameRecord.Length, unicode);
      ttfNameTable.NameRecords[index] = ttfNameRecord;
      position += (long) num;
    }
    return ttfNameTable;
  }

  private TtfOS2Table ReadOS2Table()
  {
    this.m_reader.Seek((long) this.GetTable("OS/2").Offset);
    TtfOS2Table ttfOs2Table = new TtfOS2Table();
    ttfOs2Table.Version = this.m_reader.ReadUInt16();
    ttfOs2Table.XAvgCharWidth = this.m_reader.ReadInt16();
    ttfOs2Table.UsWeightClass = this.m_reader.ReadUInt16();
    ttfOs2Table.UsWidthClass = this.m_reader.ReadUInt16();
    ttfOs2Table.FsType = this.m_reader.ReadInt16();
    ttfOs2Table.YSubscriptXSize = this.m_reader.ReadInt16();
    ttfOs2Table.YSubscriptYSize = this.m_reader.ReadInt16();
    ttfOs2Table.YSubscriptXOffset = this.m_reader.ReadInt16();
    ttfOs2Table.YSubscriptYOffset = this.m_reader.ReadInt16();
    ttfOs2Table.ySuperscriptXSize = this.m_reader.ReadInt16();
    ttfOs2Table.YSuperscriptYSize = this.m_reader.ReadInt16();
    ttfOs2Table.YSuperscriptXOffset = this.m_reader.ReadInt16();
    ttfOs2Table.YSuperscriptYOffset = this.m_reader.ReadInt16();
    ttfOs2Table.YStrikeoutSize = this.m_reader.ReadInt16();
    ttfOs2Table.YStrikeoutPosition = this.m_reader.ReadInt16();
    ttfOs2Table.SFamilyClass = this.m_reader.ReadInt16();
    ttfOs2Table.Panose = this.m_reader.ReadBytes(10);
    ttfOs2Table.UlUnicodeRange1 = this.m_reader.ReadUInt32();
    ttfOs2Table.UlUnicodeRange2 = this.m_reader.ReadUInt32();
    ttfOs2Table.UlUnicodeRange3 = this.m_reader.ReadUInt32();
    ttfOs2Table.UlUnicodeRange4 = this.m_reader.ReadUInt32();
    ttfOs2Table.AchVendID = this.m_reader.ReadBytes(4);
    ttfOs2Table.FsSelection = this.m_reader.ReadUInt16();
    ttfOs2Table.UsFirstCharIndex = this.m_reader.ReadUInt16();
    ttfOs2Table.UsLastCharIndex = this.m_reader.ReadUInt16();
    ttfOs2Table.STypoAscender = this.m_reader.ReadInt16();
    ttfOs2Table.STypoDescender = this.m_reader.ReadInt16();
    ttfOs2Table.STypoLineGap = this.m_reader.ReadInt16();
    ttfOs2Table.UsWinAscent = this.m_reader.ReadUInt16();
    ttfOs2Table.UsWinDescent = this.m_reader.ReadUInt16();
    ttfOs2Table.UlCodePageRange1 = this.m_reader.ReadUInt32();
    ttfOs2Table.UlCodePageRange2 = this.m_reader.ReadUInt32();
    if (ttfOs2Table.Version > (ushort) 1)
    {
      ttfOs2Table.SxHeight = this.m_reader.ReadInt16();
      ttfOs2Table.SCapHeight = this.m_reader.ReadInt16();
      ttfOs2Table.UsDefaultChar = this.m_reader.ReadUInt16();
      ttfOs2Table.UsBreakChar = this.m_reader.ReadUInt16();
      ttfOs2Table.UsMaxContext = this.m_reader.ReadUInt16();
    }
    return ttfOs2Table;
  }

  private TtfPostTable ReadPostTable()
  {
    this.m_reader.Seek((long) this.GetTable("post").Offset);
    return new TtfPostTable()
    {
      FormatType = this.m_reader.ReadFixed(),
      ItalicAngle = this.m_reader.ReadFixed(),
      UnderlinePosition = this.m_reader.ReadInt16(),
      UnderlineThickness = this.m_reader.ReadInt16(),
      IsFixedPitch = this.m_reader.ReadUInt32(),
      MinMemType42 = this.m_reader.ReadUInt32(),
      MaxMemType42 = this.m_reader.ReadUInt32(),
      MinMemType1 = this.m_reader.ReadUInt32(),
      MaxMemType1 = this.m_reader.ReadUInt32()
    };
  }

  private ValueType ReadStructure(BinaryReader reader, Type type)
  {
    if (reader == null)
      throw new ArgumentNullException(nameof (reader));
    int num1 = !(type == (Type) null) ? Marshal.SizeOf(type) : throw new ArgumentNullException(nameof (type));
    byte[] source = reader.ReadBytes(num1);
    IntPtr num2 = Marshal.AllocHGlobal(num1);
    IntPtr destination = num2;
    int length = num1;
    Marshal.Copy(source, 0, destination, length);
    ValueType structure = (ValueType) Marshal.PtrToStructure(num2, type);
    Marshal.FreeHGlobal(num2);
    return structure;
  }

  private void ReadTrimmedCmapTable(TtfCmapSubTable subTable, TtfCmapEncoding encoding)
  {
    this.m_reader.Seek((long) this.GetTable("cmap").Offset + (long) subTable.Offset);
    TtfTrimmedCmapSubTable trimmedCmapSubTable = new TtfTrimmedCmapSubTable();
    trimmedCmapSubTable.Format = this.m_reader.ReadUInt16();
    trimmedCmapSubTable.Length = this.m_reader.ReadUInt16();
    trimmedCmapSubTable.Version = this.m_reader.ReadUInt16();
    trimmedCmapSubTable.FirstCode = this.m_reader.ReadUInt16();
    trimmedCmapSubTable.EntryCount = this.m_reader.ReadUInt16();
    int num = 0;
    for (int entryCount = (int) trimmedCmapSubTable.EntryCount; num < entryCount; ++num)
    {
      TtfGlyphInfo glyph = new TtfGlyphInfo()
      {
        Index = (int) this.m_reader.ReadUInt16()
      };
      glyph.Width = this.GetWidth(glyph.Index);
      glyph.CharCode = num + (int) trimmedCmapSubTable.FirstCode;
      this.Macintosh[num] = glyph;
      this.AddGlyph(glyph, encoding);
      this.m_maxMacIndex = Math.Max(num, this.m_maxMacIndex);
    }
  }

  private uint[] ReadUintArray(int len)
  {
    uint[] numArray = new uint[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.m_reader.ReadUInt32();
    return numArray;
  }

  private ushort[] ReadUshortArray(int len)
  {
    ushort[] numArray = new ushort[len];
    for (int index = 0; index < len; ++index)
      numArray[index] = this.m_reader.ReadUInt16();
    return numArray;
  }

  private int[] ReadWidthTable(int glyphCount, int unitsPerEm)
  {
    this.m_reader.Seek((long) this.GetTable("hmtx").Offset);
    int[] numArray = new int[glyphCount];
    for (int index = 0; index < glyphCount; ++index)
      numArray[index] = (int) new TtfLongHorMertric()
      {
        AdvanceWidth = this.m_reader.ReadUInt16(),
        Lsb = this.m_reader.ReadInt16()
      }.AdvanceWidth * 1000 / unitsPerEm;
    return numArray;
  }

  private void UpdateGlyphChars(Dictionary<int, int> glyphChars, TtfLocaTable locaTable)
  {
    if (glyphChars == null)
      throw new ArgumentNullException(nameof (glyphChars));
    if (!glyphChars.ContainsKey(0))
      glyphChars.Add(0, 0);
    Dictionary<int, int> dictionary = new Dictionary<int, int>(glyphChars.Count);
    foreach (KeyValuePair<int, int> glyphChar in glyphChars)
      dictionary.Add(glyphChar.Key, glyphChar.Value);
    foreach (KeyValuePair<int, int> keyValuePair in dictionary)
    {
      int key = keyValuePair.Key;
      this.ProcessCompositeGlyph(glyphChars, key, locaTable);
    }
  }

  private int UpdateLocaTable(int[] newLocaTable, bool bLocaIsShort, out byte[] newLocaTableOut)
  {
    if (newLocaTable == null)
      throw new ArgumentNullException(nameof (newLocaTable));
    int num1 = bLocaIsShort ? newLocaTable.Length * 2 : newLocaTable.Length * 4;
    BigEndianWriter bigEndianWriter = new BigEndianWriter((int) this.Align((uint) num1));
    newLocaTableOut = bigEndianWriter.Data;
    for (int index = 0; index < newLocaTable.Length; ++index)
    {
      int num2 = newLocaTable[index];
      if (bLocaIsShort)
      {
        int num3 = num2 / 2;
        bigEndianWriter.Write((short) num3);
      }
      else
        bigEndianWriter.Write(num2);
    }
    return num1;
  }

  private int[] UpdateWidth()
  {
    int length = 256 /*0x0100*/;
    int[] numArray = new int[length];
    if (this.m_metrics.IsSymbol)
    {
      for (int charCode = 0; charCode < length; ++charCode)
      {
        TtfGlyphInfo glyph = this.GetGlyph((char) charCode);
        numArray[charCode] = glyph.Empty ? 0 : glyph.Width;
      }
      return numArray;
    }
    byte[] bytes = new byte[1];
    char ch = '?';
    char charCode1 = ' ';
    for (int index = 0; index < length; ++index)
    {
      bytes[0] = (byte) index;
      string str = TtfReader.Encoding.GetString(bytes, 0, bytes.Length);
      TtfGlyphInfo glyph = this.GetGlyph(str.Length > 0 ? str[0] : ch);
      if (!glyph.Empty)
      {
        numArray[index] = glyph.Width;
      }
      else
      {
        glyph = this.GetGlyph(charCode1);
        numArray[index] = glyph.Empty ? 0 : glyph.Width;
      }
    }
    return numArray;
  }

  private void WriteCheckSums(
    BigEndianWriter writer,
    short numTables,
    byte[] newLocaTableOut,
    byte[] newGlyphTable,
    uint glyphTableSize,
    uint locaTableSize)
  {
    if (writer == null)
      throw new ArgumentNullException(nameof (writer));
    if (newLocaTableOut == null)
      throw new ArgumentNullException(nameof (newLocaTableOut));
    if (newGlyphTable == null)
      throw new ArgumentNullException(nameof (newGlyphTable));
    string[] tableNames = this.TableNames;
    uint num1 = (uint) ((int) numTables * 16 /*0x10*/ + 12);
    int index = 0;
    for (int length = tableNames.Length; index < length; ++index)
    {
      string name = tableNames[index];
      TtfTableInfo table = this.GetTable(name);
      if (!table.Empty)
      {
        writer.Write(name);
        uint num2;
        switch (name)
        {
          case "glyf":
            int checkSum1 = this.CalculateCheckSum(newGlyphTable);
            writer.Write(checkSum1);
            num2 = glyphTableSize;
            break;
          case "loca":
            int checkSum2 = this.CalculateCheckSum(newLocaTableOut);
            writer.Write(checkSum2);
            num2 = locaTableSize;
            break;
          default:
            writer.Write(table.Checksum);
            num2 = (uint) table.Length;
            break;
        }
        writer.Write(num1);
        writer.Write(num2);
        num1 += this.Align(num2);
      }
    }
  }

  private void WriteGlyphs(BigEndianWriter writer, byte[] newLocaTableOut, byte[] newGlyphTable)
  {
    if (writer == null)
      throw new ArgumentNullException(nameof (writer));
    if (newLocaTableOut == null)
      throw new ArgumentNullException(nameof (newLocaTableOut));
    if (newGlyphTable == null)
      throw new ArgumentNullException(nameof (newGlyphTable));
    string[] tableNames = this.TableNames;
    int index = 0;
    for (int length = tableNames.Length; index < length; ++index)
    {
      string name = tableNames[index];
      TtfTableInfo table = this.GetTable(name);
      if (!table.Empty)
      {
        switch (name)
        {
          case "glyf":
            writer.Write(newGlyphTable);
            continue;
          case "loca":
            writer.Write(newLocaTableOut);
            continue;
          default:
            byte[] buffer = new byte[(int) this.Align((uint) table.Length)];
            this.m_reader.Seek((long) table.Offset);
            this.m_reader.Read(buffer, 0, table.Length);
            writer.Write(buffer);
            continue;
        }
      }
    }
  }

  internal Font Font => this.m_font;

  public BigEndianReader InternalReader => this.m_reader;

  private Dictionary<int, TtfGlyphInfo> Macintosh
  {
    get
    {
      if (this.m_macintosh == null)
        this.m_macintosh = new Dictionary<int, TtfGlyphInfo>();
      return this.m_macintosh;
    }
  }

  private Dictionary<int, TtfGlyphInfo> MacintoshGlyphs
  {
    get
    {
      if (this.m_macintoshGlyphs == null)
        this.m_macintoshGlyphs = new Dictionary<int, TtfGlyphInfo>();
      return this.m_macintoshGlyphs;
    }
  }

  public TtfMetrics Metrics => this.m_metrics;

  private Dictionary<int, TtfGlyphInfo> Microsoft
  {
    get
    {
      if (this.m_microsoft == null)
        this.m_microsoft = new Dictionary<int, TtfGlyphInfo>();
      return this.m_microsoft;
    }
  }

  private Dictionary<int, TtfGlyphInfo> MicrosoftGlyphs
  {
    get
    {
      if (this.m_microsoftGlyphs == null)
        this.m_microsoftGlyphs = new Dictionary<int, TtfGlyphInfo>();
      return this.m_microsoftGlyphs;
    }
  }

  public BinaryReader Reader
  {
    get => this.m_reader.Reader;
    set => this.m_reader.Reader = value;
  }

  private Dictionary<string, TtfTableInfo> TableDirectory
  {
    get
    {
      if (this.m_tableDirectory == null)
        this.m_tableDirectory = new Dictionary<string, TtfTableInfo>();
      return this.m_tableDirectory;
    }
  }

  private string[] TableNames
  {
    get => this.TrueTypeSubset ? TtfReader.m_tableNames : TtfReader.s_tableNames;
  }

  internal bool TrueTypeSubset
  {
    get => this.m_subset;
    set => this.m_subset = value;
  }
}
