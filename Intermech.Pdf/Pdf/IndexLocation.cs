// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.IndexLocation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class IndexLocation : TableBase
    {
      private int m_id;
      private uint[] m_offset;
      private int p;

      public IndexLocation(FontFile2 fontsource)
        : base(fontsource)
      {
        this.m_id = 3;
      }

      public long GetOffset(ushort index)
      {
        return this.Offset != null && (int) index < this.Offset.Length && ((int) index >= this.Offset.Length - 1 || (int) this.Offset[(int) index + 1] != (int) this.Offset[(int) index]) ? (long) this.Offset[(int) index] : -1L;
      }

      public override void Read(ReadFontArray reader)
      {
        this.p = reader.Pointer;
        this.m_offset = new uint[this.FontSource.NumGlyphs + 1];
        reader.Pointer = this.p;
        for (int index = 0; index < this.m_offset.Length; ++index)
          this.m_offset[index] = this.FontSource.Header.IndexToLocFormat != (short) 0 ? (uint) reader.getnextULong() : (uint) reader.getnextUshort() * 2U;
      }

      internal override int Id => this.m_id;

      public uint[] Offset
      {
        get => this.m_offset;
        set => this.m_offset = value;
      }
    }
}
