// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TrueTypeCmap
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class TrueTypeCmap : TableBase
{
  public FontEncoding[] encodings;
  private Dictionary<FontEncoding, CmapTables> encodingtable;
  private int m_id;
  private ushort noofSubtable;
  private uint subOffset;

  public TrueTypeCmap(FontFile2 fontsource)
    : base(fontsource)
  {
    this.m_id = 2;
  }

  public CmapTables GetCmaptable(ushort platformid, ushort encodingid)
  {
    FontEncoding encode = (FontEncoding) null;
    for (int index = 0; index < (int) this.noofSubtable; ++index)
    {
      if ((int) this.encodings[index].PlatformId == (int) platformid && (int) this.encodings[index].EncodingId == (int) encodingid)
        encode = this.encodings[index];
    }
    return encode == null ? (CmapTables) null : this.GetCmapTable(encode, this.Reader);
  }

  public CmapTables GetCmapTable(FontEncoding encode, ReadFontArray reader)
  {
    CmapTables cmapTable;
    if (!this.encodingtable.TryGetValue(encode, out cmapTable))
    {
      reader.Pointer = (int) encode.Offset + this.Offset;
      cmapTable = CmapTables.ReadCmapTable(reader);
      this.encodingtable[encode] = cmapTable;
    }
    return cmapTable;
  }

  public override void Read(ReadFontArray reader)
  {
    int num = (int) reader.getnextUshort();
    this.noofSubtable = reader.getnextUshort();
    this.encodings = new FontEncoding[(int) this.noofSubtable];
    this.encodingtable = new Dictionary<FontEncoding, CmapTables>((int) this.noofSubtable);
    for (int index = 0; index < (int) this.noofSubtable; ++index)
    {
      FontEncoding fontEncoding = new FontEncoding();
      fontEncoding.ReadEncodingDeatils(reader);
      this.encodings[index] = fontEncoding;
    }
  }

  internal override int Id => this.m_id;
}
