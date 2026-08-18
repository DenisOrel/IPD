// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.FontFile2
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf;

internal class FontFile2
{
  private ushort[] array4;
  public const int BASE = 18;
  public const int BSLN = 29;
  public const int CFF = 16 /*0x10*/;
  protected internal int[][] checksums;
  public const int CMAP = 2;
  private MemoryStream cmapStream;
  public List<OutlinePoint[]> contours;
  public int currentFontID;
  public const int CVT = 9;
  public const int DSIG = 15;
  public const int EBDT = 19;
  public const int EBLC = 20;
  private ushort[] encodevalue;
  protected internal int entrySelector;
  public const int FDSC = 31 /*0x1F*/;
  public const int FEAT = 34;
  public const int FFTM = 32 /*0x20*/;
  internal int fontCount;
  public byte[] fontDataAsArray;
  public const int FPGM = 10;
  public const int GASP = 21;
  public const int GDEF = 24;
  public const int GLYF = 4;
  private ushort[] glyphsIdValue;
  public const int GPOS = 33;
  public GraphicsPath graphic;
  public const int GSUB = 17;
  public const int HDMX = 11;
  public const int HEAD = 0;
  public const int HHEA = 5;
  public const int HMTX = 6;
  private int id;
  private short[] idDeltavalue;
  public const int JSTF = 25;
  public const int JUST = 35;
  public const int KERN = 12;
  public const int LOCA = 3;
  private uint[] locaoffset;
  public const int LTSH = 26;
  private TrueTypeCmap m_cmap;
  private int m_firstCode;
  private Head m_head;
  private bool m_isfontfile2;
  private IndexLocation m_loca;
  private Maxp m_maxp;
  private int m_offset;
  private ReadFontArray m_reader;
  private RectangularArrays m_rectangularArrays;
  private TrueTypeGlyphs m_trueTypeGlypf;
  public const int MAXP = 1;
  public const int MORT = 30;
  public const int NAME = 7;
  private ushort noofSubtable;
  private List<ushort[]> notable;
  private ushort numgl;
  private int numglyphs;
  protected internal int numTables;
  protected internal int[][] offsets;
  public const int OPENTYPE = 1;
  public const int OS2 = 13;
  private Dictionary<ushort, TrueTypeGlyphs> pathtable;
  public const int PCLT = 27;
  internal int pointer;
  public const int POST = 8;
  public const int PREP = 14;
  public const int PROP = 36;
  protected internal int rangeShift;
  protected internal int searchRange;
  public int segment;
  private const long serialVersionUID = -3097990864237320960;
  private ushort[] startcodevalue;
  internal Dictionary<int, TableEntry> table;
  protected internal int tableCount;
  internal List<TableEntry> tableEntries;
  protected internal int[][] tableLength;
  protected internal List<string> tableList;
  protected internal int[][] tables;
  private Matrix translate;
  public const int TRUETYPE = 2;
  public const int TTC = 3;
  protected internal int type;
  private bool useArray;
  public const int VDMX = 28;
  public const int VHEA = 22;
  public const int VMTX = 23;

  public FontFile2()
  {
    this.m_rectangularArrays = new RectangularArrays();
    this.tableCount = 37;
    this.notable = new List<ushort[]>();
    this.cmapStream = new MemoryStream();
    this.pathtable = new Dictionary<ushort, TrueTypeGlyphs>();
    this.useArray = true;
    this.tableList = new List<string>();
    this.tableEntries = new List<TableEntry>();
    this.table = new Dictionary<int, TableEntry>();
    this.type = 2;
    this.fontCount = 1;
    this.numTables = 11;
    this.searchRange = 128 /*0x80*/;
    this.entrySelector = 3;
    this.rangeShift = 48 /*0x30*/;
    this.translate = new Matrix();
  }

  public FontFile2(byte[] data)
  {
    this.m_rectangularArrays = new RectangularArrays();
    this.tableCount = 37;
    this.notable = new List<ushort[]>();
    this.cmapStream = new MemoryStream();
    this.pathtable = new Dictionary<ushort, TrueTypeGlyphs>();
    this.useArray = true;
    this.tableList = new List<string>();
    this.tableEntries = new List<TableEntry>();
    this.table = new Dictionary<int, TableEntry>();
    this.type = 2;
    this.fontCount = 1;
    this.numTables = 11;
    this.searchRange = 128 /*0x80*/;
    this.entrySelector = 3;
    this.rangeShift = 48 /*0x30*/;
    this.translate = new Matrix();
    this.m_isfontfile2 = true;
    this.useArray = true;
    this.fontDataAsArray = data;
    this.readHeader();
  }

  public PointF ConvertFunittoPoint(PointF units, float fontSize)
  {
    return new PointF((float) ((double) units.X * 72.0 * (double) fontSize / (72.0 * (double) this.Header.UnitsPerEm)), (float) -((double) units.Y * 72.0 * (double) fontSize / (72.0 * (double) this.Header.UnitsPerEm)));
  }

  public GraphicsPath CreatePath(OutlinePoint[] points, float fontSize)
  {
    this.graphic.StartFigure();
    PointF units;
    ref PointF local = ref units;
    PointF point = points[0].Point;
    double x = (double) point.X;
    point = points[0].Point;
    double y = (double) point.Y;
    local = new PointF((float) x, (float) y);
    PointF pointF1 = this.ConvertFunittoPoint(units, 100f);
    List<PointF> pointFList = new List<PointF>();
    for (int index = 1; index < points.Length; ++index)
    {
      if (points[index].IsOnCurve)
      {
        PointF pointF2 = this.ConvertFunittoPoint(points[index].Point, 100f);
        PointF[] pointFArray = new PointF[2]
        {
          pointF1,
          pointF2
        };
        this.graphic.AddLine(pointFArray[0], pointFArray[1]);
        pointF1 = new PointF(pointFArray[1].X, pointFArray[1].Y);
      }
      else if (points[(index + 1) % points.Length].IsOnCurve)
      {
        PointF[] pointFArray = new PointF[3]
        {
          pointF1,
          this.ConvertFunittoPoint(points[index].Point, 100f),
          this.ConvertFunittoPoint(points[(index + 1) % points.Length].Point, 100f)
        };
        this.graphic.AddBezier(pointFArray[0], pointFArray[1], pointFArray[2], pointFArray[2]);
        ++index;
        pointF1 = new PointF(pointFArray[2].X, pointFArray[2].Y);
      }
      else
      {
        PointF[] pointFArray = new PointF[3]
        {
          pointF1,
          this.ConvertFunittoPoint(points[index].Point, 100f),
          this.ConvertFunittoPoint(this.GetMidPoint(points[index].Point, points[(index + 1) % points.Length].Point), 100f)
        };
        this.graphic.AddBezier(pointFArray[0], pointFArray[1], pointFArray[2], pointFArray[2]);
        pointF1 = new PointF(pointFArray[2].X, pointFArray[2].Y);
      }
    }
    this.graphic.FillMode = FillMode.Alternate;
    this.graphic.CloseAllFigures();
    return this.graphic;
  }

  internal static bool GetBit(int n, byte bit) => (n & 1 << (int) bit) != 0;

  public byte[] GetByte(int val)
  {
    byte[] bytes = BitConverter.GetBytes(val);
    byte[] numArray = new byte[2]{ bytes[0], bytes[1] };
    Array.Reverse((Array) numArray);
    return numArray;
  }

  public ushort GetChatid(byte b)
  {
    new byte[1][0] = b;
    return (ushort) b;
  }

  public GraphicsPath GetCIDGlyphs(char character, Dictionary<string, double> cmap)
  {
    this.graphic = new GraphicsPath();
    Encoding.ASCII.GetBytes(character.ToString())[0] = (byte) character;
    ushort num = (ushort) character;
    if (cmap.ContainsValue((double) num))
    {
      TrueTypeGlyphs trueTypeGlyphs = this.readGlyphdata(num);
      if (trueTypeGlyphs.NumberOfContours != (short) 0)
      {
        foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
          this.CreatePath(contour, 100f);
      }
    }
    return this.graphic;
  }

  public MemoryStream Getcmapstream() => this.cmapStream;

  public void GetFirstCode(CmapTables unicode)
  {
    if (this.m_firstCode != 0)
      return;
    this.m_firstCode = (int) unicode.FirstCode;
  }

  public GraphicsPath GetGlyfPathMicrosoftwithencoding(CmapTables unicode, char character)
  {
    this.graphic = new GraphicsPath();
    this.FirstCode = (int) unicode.FirstCode;
    ushort num1 = 0;
    byte[] numArray = new byte[1]{ (byte) character };
    foreach (byte num2 in numArray)
    {
      int chatid = (int) this.GetChatid(num2);
      string winencodeCharactername = this.GetWinencodeCharactername(num2, "WinAnsiEncoding");
      if (AdobeGlyphList.IsSupportedPdfName(winencodeCharactername))
        num1 = unicode.GetGlyphId((ushort) AdobeGlyphList.GetUnicode(winencodeCharactername));
    }
    TrueTypeGlyphs trueTypeGlyphs = this.readGlyphdata(num1);
    if (trueTypeGlyphs.NumberOfContours != (short) 0)
    {
      foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
        this.CreatePath(contour, 100f);
    }
    return this.graphic;
  }

  public GraphicsPath GetGlyfPathWindowsWithoutEncoding(CmapTables unicode, char character)
  {
    this.graphic = new GraphicsPath();
    this.FirstCode = (int) unicode.FirstCode;
    ushort num = 0;
    byte[] numArray = new byte[1]{ (byte) character };
    foreach (byte b in numArray)
    {
      ushort res;
      num = !this.TryAppendByte(b, out res) ? (ushort) 0 : unicode.GetGlyphId(res);
    }
    TrueTypeGlyphs trueTypeGlyphs = this.readGlyphdata(num);
    if (trueTypeGlyphs.NumberOfContours != (short) 0)
    {
      foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
        this.CreatePath(contour, 100f);
    }
    return this.graphic;
  }

  public GraphicsPath GetGlyphsPathMacWithEncoding(CmapTables unicode, char character)
  {
    this.graphic = new GraphicsPath();
    this.FirstCode = (int) unicode.FirstCode;
    ushort num1 = 0;
    byte[] numArray = new byte[1]{ (byte) character };
    foreach (byte num2 in numArray)
    {
      int chatid = (int) this.GetChatid(num2);
      byte charId = PredefinedEncoding.StandardMacRomanEncoding.GetCharId(this.GetWinencodeCharactername(num2, "MacRomanEncoding"));
      num1 = unicode.GetGlyphId((ushort) charId);
    }
    TrueTypeGlyphs trueTypeGlyphs = this.readGlyphdata(num1);
    if (trueTypeGlyphs.NumberOfContours != (short) 0)
    {
      foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
        this.CreatePath(contour, 100f);
    }
    return this.graphic;
  }

  public GraphicsPath GetGlyphsPathMacWithoutencoding(CmapTables unicode, char character)
  {
    this.graphic = new GraphicsPath();
    this.FirstCode = (int) unicode.FirstCode;
    ushort num = 0;
    byte[] numArray = new byte[1]{ (byte) character };
    foreach (byte charCode in numArray)
      num = unicode.GetGlyphId((ushort) charCode);
    TrueTypeGlyphs trueTypeGlyphs = this.readGlyphdata(num);
    if (trueTypeGlyphs.NumberOfContours != (short) 0)
    {
      foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
        this.CreatePath(contour, 100f);
    }
    return this.graphic;
  }

  public int GetInt(byte[] val)
  {
    int num = 0;
    int length = val.Length;
    for (int index = 0; index < length; ++index)
    {
      num |= length <= index ? 0 : (int) val[index] & (int) byte.MaxValue;
      if (index < length - 1)
        num <<= 8;
    }
    return num;
  }

  private PointF GetMidPoint(PointF a, PointF b)
  {
    return new PointF((float) (((double) a.X + (double) b.X) / 2.0), (float) (((double) a.Y + (double) b.Y) / 2.0));
  }

  public byte getnextbyte()
  {
    int fontDataAs = (int) this.fontDataAsArray[this.pointer];
    ++this.pointer;
    return (byte) fontDataAs;
  }

  public short getnextshort()
  {
    byte[] numArray = new byte[2];
    for (int index = 1; index >= 0; --index)
    {
      if (this.fontDataAsArray.Length != 0)
        numArray[index] = this.fontDataAsArray[this.pointer];
      ++this.pointer;
    }
    return BitConverter.ToInt16(numArray, 0);
  }

  public int getnextUint16()
  {
    int num1 = 0;
    for (int index = 0; index < 2; ++index)
    {
      if (this.fontDataAsArray.Length != 0)
      {
        int num2 = (int) this.fontDataAsArray[this.pointer] & (int) byte.MaxValue;
        num1 += num2 << 8 * (1 - index);
      }
      ++this.pointer;
    }
    return num1;
  }

  public int getnextUint32()
  {
    int num1 = 0;
    for (int index = 0; index < 4; ++index)
    {
      int num2 = this.pointer >= this.fontDataAsArray.Length ? 0 : (int) this.fontDataAsArray[this.pointer] & (int) byte.MaxValue;
      num1 += num2 << 8 * (3 - index);
      ++this.pointer;
    }
    return num1;
  }

  public string getnextUint32AsTag()
  {
    StringBuilder stringBuilder = new StringBuilder();
    for (int index = 0; index < 4; ++index)
    {
      char fontDataAs = (char) this.fontDataAsArray[this.pointer];
      stringBuilder.Append(fontDataAs);
      ++this.pointer;
    }
    return stringBuilder.ToString();
  }

  public int getnextUint64()
  {
    int num1 = 0;
    for (int index = 0; index < 8; ++index)
    {
      int num2 = (int) this.fontDataAsArray[this.pointer];
      if (num2 < 0)
        num2 = 256 /*0x0100*/ + num2;
      num1 += num2 << 8 * (7 - index);
      ++this.pointer;
    }
    return num1;
  }

  public ulong getnextULong()
  {
    byte[] numArray = new byte[4];
    for (int index = 3; index >= 0; --index)
    {
      if (this.fontDataAsArray.Length != 0)
        numArray[index] = this.fontDataAsArray[this.pointer];
      ++this.pointer;
    }
    return (ulong) BitConverter.ToUInt32(numArray, 0);
  }

  public ushort getnextUshort()
  {
    byte[] numArray = new byte[2];
    for (int index = 1; index >= 0; --index)
    {
      if (this.fontDataAsArray.Length != 0)
        numArray[index] = this.fontDataAsArray[this.pointer];
      ++this.pointer;
    }
    return BitConverter.ToUInt16(numArray, 0);
  }

  public int getOffsets(int tableID)
  {
    this.m_offset = this.tables[tableID][this.currentFontID];
    return this.m_offset;
  }

  public byte[] getTableBytes(int tableID)
  {
    if (this.id == 2)
      return this.cmapStream.GetBuffer();
    int sourceIndex = this.tables[tableID][this.currentFontID];
    int length = this.tableLength[tableID][this.currentFontID];
    byte[] destinationArray = new byte[length];
    Array.Copy((Array) this.fontDataAsArray, sourceIndex, (Array) destinationArray, 0, length);
    return destinationArray;
  }

  protected internal int getTableID(string tag)
  {
    this.id = -1;
    switch (tag)
    {
      case "maxp":
        this.id = 1;
        break;
      case "head":
        this.id = 0;
        break;
      case "cmap":
        this.id = 2;
        break;
      case "loca":
        this.id = 3;
        break;
      case "glyf":
        this.id = 4;
        break;
      case "hhea":
        this.id = 5;
        break;
      case "hmtx":
        this.id = 6;
        break;
      case "name":
        this.id = 7;
        break;
      case "post":
        this.id = 8;
        break;
      case "cvt ":
        this.id = 9;
        break;
      case "fpgm":
        this.id = 10;
        break;
      case "hdmx":
        this.id = 11;
        break;
      case "kern":
        this.id = 12;
        break;
      case "OS/2":
        this.id = 13;
        break;
      case "prep":
        this.id = 14;
        break;
      case "DSIG":
        this.id = 15;
        break;
      case "BASE":
        this.id = 18;
        break;
      case "CFF ":
        this.id = 16 /*0x10*/;
        break;
      case "GSUB":
        this.id = 17;
        break;
      case "EBDT":
        this.id = 19;
        break;
      case "EBLC":
        this.id = 20;
        break;
      case "gasp":
        this.id = 21;
        break;
      case "vhea":
        this.id = 22;
        break;
      case "vmtx":
        this.id = 23;
        break;
      case "GDEF":
        this.id = 24;
        break;
      case "JSTF":
        this.id = 25;
        break;
      case "LTSH":
        this.id = 26;
        break;
      case "PCLT":
        this.id = 27;
        break;
      case "VDMX":
        this.id = 28;
        break;
      case "mort":
        this.id = 30;
        break;
      case "bsln":
        this.id = 29;
        break;
      case "fdsc":
        this.id = 31 /*0x1F*/;
        break;
      case "FFTM":
        this.id = 32 /*0x20*/;
        break;
      case "GPOS":
        this.id = 33;
        break;
      case "feat":
        this.id = 34;
        break;
      case "just":
        this.id = 35;
        break;
      case "prop":
        this.id = 36;
        break;
    }
    return this.id;
  }

  public uint getULong()
  {
    byte[] numArray = new byte[4];
    for (int index = 3; index >= 0; --index)
    {
      if (this.fontDataAsArray.Length != 0)
        numArray[index] = this.fontDataAsArray[this.pointer];
      ++this.pointer;
    }
    return BitConverter.ToUInt32(numArray, 0);
  }

  public string GetWinencodeCharactername(byte val, string Winansi)
  {
    return PredefinedEncoding.GetPredefinedEncoding(Winansi).GetNames()[(int) val];
  }

  public TrueTypeGlyphs readGlyphdata(ushort value)
  {
    if (this.pathtable.ContainsKey(value))
      return this.pathtable[value];
    long offset = this.Loca.GetOffset(value);
    TableEntry tableEntry;
    this.table.TryGetValue(this.TrueTypeFontGlyf.Id, out tableEntry);
    if (offset == -1L || tableEntry == null || offset >= (long) (tableEntry.offset + tableEntry.length))
      return new TrueTypeGlyphs(this, value);
    int pointer = this.FontArrayReader.Pointer;
    this.FontArrayReader.Pointer = (int) offset + tableEntry.offset;
    TrueTypeGlyphs trueTypeGlyphs = TrueTypeGlyphs.ReadGlyf(this, value);
    this.FontArrayReader.Pointer = pointer;
    this.pathtable[value] = trueTypeGlyphs;
    return trueTypeGlyphs;
  }

  private void readHeader()
  {
    switch (this.FontArrayReader.getnextUint32())
    {
      case 1330926671:
        this.type = 1;
        break;
      case 1953784678:
        this.type = 3;
        break;
    }
    if (this.type == 3)
    {
      this.FontArrayReader.getnextUint32();
      this.fontCount = this.FontArrayReader.getnextUint32();
      this.checksums = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, this.fontCount);
      this.tables = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, this.fontCount);
      this.tableLength = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, this.fontCount);
      int[] numArray = new int[this.fontCount];
      for (int index = 0; index < this.fontCount; ++index)
      {
        this.currentFontID = index;
        numArray[index] = this.FontArrayReader.getnextUint32();
      }
      for (int index = 0; index < this.fontCount; ++index)
      {
        this.currentFontID = index;
        this.pointer = numArray[index];
        this.FontArrayReader.getnextUint32();
        this.readTablesForFont();
      }
      this.currentFontID = 0;
    }
    else
    {
      this.checksums = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, 1);
      this.tables = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, 1);
      this.tableLength = this.m_rectangularArrays.ReturnRectangularIntArray(this.tableCount, 1);
      this.readTablesForFont();
    }
  }

  public void ReadTable(TableBase tabb)
  {
    TableEntry tableEntry;
    if (!this.table.TryGetValue(tabb.Id, out tableEntry))
      return;
    int pointer = this.FontArrayReader.Pointer;
    this.FontArrayReader.Pointer = tableEntry.offset;
    tabb.Read(this.FontArrayReader);
    tabb.Offset = tableEntry.offset;
    this.FontArrayReader.Pointer = pointer;
  }

  private void readTablesForFont()
  {
    this.numTables = this.FontArrayReader.getnextUint16();
    this.searchRange = this.FontArrayReader.getnextUint16();
    this.entrySelector = this.FontArrayReader.getnextUint16();
    this.rangeShift = this.FontArrayReader.getnextUint16();
    for (int index = 0; index < this.numTables; ++index)
    {
      TableEntry tableEntry = new TableEntry();
      tableEntry.id = this.FontArrayReader.getnextUint32AsTag();
      tableEntry.checkSum = this.FontArrayReader.getnextUint32();
      tableEntry.offset = this.FontArrayReader.getnextUint32();
      tableEntry.length = this.FontArrayReader.getnextUint32();
      this.tableList.Add(tableEntry.id);
      this.tableEntries.Add(tableEntry);
      int tableId = this.getTableID(tableEntry.id);
      this.table.Add(tableId, tableEntry);
      if (tableId != -1)
      {
        this.checksums[tableId][this.currentFontID] = tableEntry.checkSum;
        this.tables[tableId][this.currentFontID] = tableEntry.offset;
        this.tableLength[tableId][this.currentFontID] = tableEntry.length;
      }
    }
  }

  private static bool Repeat(byte[] flags, int index)
  {
    return FontFile2.GetBit((int) flags[index], (byte) 3);
  }

  private bool TryAppendByte(byte b, out ushort res)
  {
    res = (ushort) 0;
    try
    {
      int num = this.GetInt(new byte[2]
      {
        this.GetByte(this.FirstCode)[0],
        b
      });
      res = (ushort) num;
      return true;
    }
    catch
    {
      return false;
    }
  }

  private void Write4()
  {
    this.WriteShort((ushort) 4);
    this.WriteShort((ushort) this.m_firstCode);
    this.WriteShort(this.numgl);
    for (int index1 = 0; index1 < (int) this.numgl; ++index1)
    {
      this.WriteShort(this.startcodevalue[index1]);
      this.WriteShort(this.encodevalue[index1]);
      this.WriteuShort(this.idDeltavalue[index1]);
      this.WriteShort((ushort) this.notable[index1].Length);
      for (int index2 = 0; index2 < this.notable[index1].Length; ++index2)
        this.WriteShort(this.notable[index1][index2]);
    }
  }

  public void Write6()
  {
    this.WriteShort((ushort) 6);
    this.WriteShort((ushort) this.m_firstCode);
    this.WriteShort((ushort) this.glyphsIdValue.Length);
    for (int index = 0; index < this.glyphsIdValue.Length; ++index)
      this.WriteShort(this.glyphsIdValue[index]);
  }

  private void WriteByte(byte value)
  {
    this.cmapStream.Write(new byte[1]{ value }, 0, 1);
  }

  private void WriteLong(ulong value)
  {
    byte[] numArray = new byte[4];
    byte[] bytes = BitConverter.GetBytes(value);
    for (int index = 3; index >= 0; --index)
      this.WriteByte(bytes[index]);
  }

  private void WriteShort(ushort value)
  {
    byte[] numArray = new byte[2];
    byte[] bytes = BitConverter.GetBytes(value);
    for (int index = 1; index >= 0; --index)
      this.WriteByte(bytes[index]);
  }

  private void WriteuShort(short value)
  {
    byte[] numArray = new byte[2];
    byte[] bytes = BitConverter.GetBytes(value);
    for (int index = 1; index >= 0; --index)
      this.WriteByte(bytes[index]);
  }

  private static bool XIsByte(byte[] flags, int index)
  {
    return FontFile2.GetBit((int) flags[index], (byte) 1);
  }

  private static bool XIsSame(byte[] flags, int index)
  {
    return FontFile2.GetBit((int) flags[index], (byte) 4);
  }

  private static bool YIsByte(byte[] flags, int index)
  {
    return FontFile2.GetBit((int) flags[index], (byte) 2);
  }

  private static bool YIsSame(byte[] flags, int index)
  {
    return FontFile2.GetBit((int) flags[index], (byte) 5);
  }

  public TrueTypeCmap Cmap
  {
    get
    {
      if (this.m_cmap == null)
      {
        this.m_cmap = new TrueTypeCmap(this);
        this.ReadTable((TableBase) this.m_cmap);
      }
      return this.m_cmap;
    }
  }

  public TrueTypeCmap CmapTable
  {
    get
    {
      TrueTypeCmap cmap = this.m_cmap;
      return this.m_cmap;
    }
  }

  public int FirstCode
  {
    get => this.m_firstCode;
    set => this.m_firstCode = value;
  }

  public ReadFontArray FontArrayReader
  {
    get
    {
      if (this.m_reader == null)
        this.m_reader = new ReadFontArray(this.fontDataAsArray);
      return this.m_reader;
    }
  }

  public byte[] FontFileArrayData => this.fontDataAsArray;

  public Head Header
  {
    get
    {
      if (this.m_head == null)
      {
        this.m_head = new Head(this);
        this.ReadTable((TableBase) this.m_head);
      }
      return this.m_head;
    }
  }

  public bool IsFontFile2
  {
    get => this.m_isfontfile2;
    set => this.m_isfontfile2 = value;
  }

  public IndexLocation Loca
  {
    get
    {
      if (this.m_loca == null)
      {
        this.m_loca = new IndexLocation(this);
        this.ReadTable((TableBase) this.m_loca);
      }
      return this.m_loca;
    }
  }

  public Maxp MaximumProfile
  {
    get
    {
      if (this.m_maxp == null)
      {
        this.m_maxp = new Maxp(this);
        this.ReadTable((TableBase) this.m_maxp);
      }
      return this.m_maxp;
    }
  }

  public int NumGlyphs => (int) this.MaximumProfile.NumGlyphs;

  public int OffsetVal
  {
    get
    {
      this.m_offset = this.getOffsets(this.id);
      return this.m_offset;
    }
    set => this.m_offset = value;
  }

  internal List<ushort[]> Segments
  {
    get => this.notable;
    set => this.notable = value;
  }

  public TrueTypeGlyphs TrueTypeFontGlyf
  {
    get
    {
      if (this.m_trueTypeGlypf == null)
      {
        this.m_trueTypeGlypf = new TrueTypeGlyphs(this);
        this.ReadTable((TableBase) this.m_trueTypeGlypf);
      }
      return this.m_trueTypeGlypf;
    }
  }
}
