// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.CompositeGlyph
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class CompositeGlyph(FontFile2 fontFile, ushort glyphIndex) : TrueTypeGlyphs(fontFile, glyphIndex)
{
  private List<OutlinePoint[]> contours;

  private void AddGlyph(GlyphDescription gd)
  {
    TrueTypeGlyphs trueTypeGlyphs = this.FontSource.readGlyphdata(gd.GlyphIndex);
    if (trueTypeGlyphs == null)
      return;
    foreach (OutlinePoint[] contour in trueTypeGlyphs.Contours)
    {
      OutlinePoint[] outlinePointArray = new OutlinePoint[contour.Length];
      for (int index = 0; index < contour.Length; ++index)
        outlinePointArray[index] = CompositeGlyph.GetTransformedPoint(gd, contour[index]);
      this.contours.Add(outlinePointArray);
    }
  }

  private static OutlinePoint GetTransformedPoint(GlyphDescription compostite, OutlinePoint point)
  {
    return new OutlinePoint(point.Flags)
    {
      Point = compostite.Transformpoint(point.Point)
    };
  }

  public override void Read(ReadFontArray reader)
  {
    int num1 = (int) reader.getnextshort();
    int num2 = (int) reader.getnextshort();
    int num3 = (int) reader.getnextshort();
    int num4 = (int) reader.getnextshort();
    this.contours = new List<OutlinePoint[]>();
    GlyphDescription gd;
    do
    {
      gd = new GlyphDescription();
      gd.Read(reader);
      this.AddGlyph(gd);
    }
    while (gd.CheckFlag((byte) 5));
  }

  internal new List<OutlinePoint[]> Contours => this.contours;
}
