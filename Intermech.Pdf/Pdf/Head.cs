// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Head
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;


namespace Syncfusion.Pdf
{
    internal class Head(FontFile2 fontFile) : TableBase(fontFile)
    {
      private RectangleF m_bbox;
      private ushort m_flags;
      private short m_glyphDataFormat;
      private int m_id;
      private short m_indexFormat;
      private ushort m_unitsPerEm;
      private ushort macStyle;

      private bool CheckMacStyle(byte bit) => ((uint) this.macStyle & 1U << (int) bit) > 0U;

      public override void Read(ReadFontArray reader)
      {
        double num1 = (double) reader.getFixed();
        double num2 = (double) reader.getFixed();
        long num3 = (long) reader.getnextULong();
        long num4 = (long) reader.getnextULong();
        this.m_flags = reader.getnextUshort();
        this.m_unitsPerEm = reader.getnextUshort();
        reader.getLongDateTime();
        reader.getLongDateTime();
        this.m_bbox = new RectangleF((float) reader.getnextshort(), (float) reader.getnextshort(), (float) reader.getnextshort(), (float) reader.getnextshort());
        this.macStyle = reader.getnextUshort();
        int num5 = (int) reader.getnextUshort();
        int num6 = (int) reader.getnextshort();
        this.m_indexFormat = reader.getnextshort();
        int num7 = (int) reader.getnextshort();
      }

      public RectangleF BBox
      {
        get => this.m_bbox;
        private set => this.m_bbox = value;
      }

      public ushort Flags
      {
        get => this.m_flags;
        private set => this.m_flags = value;
      }

      public short GlyphDataFormat
      {
        get => this.m_glyphDataFormat;
        private set => this.m_glyphDataFormat = value;
      }

      internal override int Id => this.m_id;

      public short IndexToLocFormat
      {
        get => this.m_indexFormat;
        private set => this.m_indexFormat = value;
      }

      public bool IsBold => this.CheckMacStyle((byte) 0);

      public bool IsItalic => this.CheckMacStyle((byte) 1);

      public ushort UnitsPerEm
      {
        get => this.m_unitsPerEm;
        private set => this.m_unitsPerEm = value;
      }
    }
}
