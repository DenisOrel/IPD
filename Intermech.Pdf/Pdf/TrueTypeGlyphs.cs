// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TrueTypeGlyphs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;
using System.Drawing.Drawing2D;


namespace Syncfusion.Pdf
{
    internal class TrueTypeGlyphs : TableBase
    {
      private readonly ushort glyphIndex;
      public GraphicsPath graphic;
      private List<OutlinePoint[]> m_contours;
      private int m_id;
      private short m_numberOfContours;
      public Dictionary<ushort, GraphicsPath> PathTable;

      public TrueTypeGlyphs(FontFile2 fontFile)
        : base(fontFile)
      {
        this.graphic = new GraphicsPath();
        this.PathTable = new Dictionary<ushort, GraphicsPath>();
        this.m_id = 4;
        this.glyphIndex = this.glyphIndex;
      }

      public TrueTypeGlyphs(FontFile2 fontFile, ushort glyphIndex)
        : base(fontFile)
      {
        this.graphic = new GraphicsPath();
        this.PathTable = new Dictionary<ushort, GraphicsPath>();
        this.m_id = 4;
        this.glyphIndex = glyphIndex;
      }

      public override void Read(ReadFontArray reader)
      {
      }

      public static TrueTypeGlyphs ReadGlyf(FontFile2 fontFile, ushort glyphIndex)
      {
        short num = fontFile.FontArrayReader.getnextshort();
        TrueTypeGlyphs trueTypeGlyphs = num != (short) 0 ? (num <= (short) 0 ? (TrueTypeGlyphs) new CompositeGlyph(fontFile, glyphIndex) : (TrueTypeGlyphs) new SimpleGlyf(fontFile, glyphIndex)) : new TrueTypeGlyphs(fontFile, glyphIndex);
        trueTypeGlyphs.NumberOfContours = num;
        trueTypeGlyphs.Read(fontFile.FontArrayReader);
        switch (trueTypeGlyphs)
        {
          case SimpleGlyf _:
            SimpleGlyf simpleGlyf = trueTypeGlyphs as SimpleGlyf;
            trueTypeGlyphs.m_contours = simpleGlyf.Contours;
            return trueTypeGlyphs;
          case CompositeGlyph _:
            CompositeGlyph compositeGlyph = trueTypeGlyphs as CompositeGlyph;
            trueTypeGlyphs.m_contours = compositeGlyph.Contours;
            break;
        }
        return trueTypeGlyphs;
      }

      internal List<OutlinePoint[]> Contours
      {
        get
        {
          if (this.m_contours == null)
            this.m_contours = new List<OutlinePoint[]>();
          return this.m_contours;
        }
        set => this.m_contours = value;
      }

      public ushort GlyphIndex => this.glyphIndex;

      internal override int Id => this.m_id;

      internal short NumberOfContours
      {
        get => this.m_numberOfContours;
        set => this.m_numberOfContours = value;
      }
    }
}
