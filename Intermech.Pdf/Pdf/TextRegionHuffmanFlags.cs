// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TextRegionHuffmanFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class TextRegionHuffmanFlags : JBIG2BaseFlags
    {
      public const string SB_HUFF_DS = "SB_HUFF_DS";
      public const string SB_HUFF_DT = "SB_HUFF_DT";
      public const string SB_HUFF_FS = "SB_HUFF_FS";
      public const string SB_HUFF_RDH = "SB_HUFF_RDH";
      public const string SB_HUFF_RDW = "SB_HUFF_RDW";
      public const string SB_HUFF_RDX = "SB_HUFF_RDX";
      public const string SB_HUFF_RDY = "SB_HUFF_RDY";
      public const string SB_HUFF_RSIZE = "SB_HUFF_RSIZE";

      public override void setFlags(int flagsAsInt)
      {
        this.flagsAsInt = flagsAsInt;
        this.flags.Add((object) "SB_HUFF_FS", (object) (flagsAsInt & 3));
        this.flags.Add((object) "SB_HUFF_DS", (object) (flagsAsInt >> 2 & 3));
        this.flags.Add((object) "SB_HUFF_DT", (object) (flagsAsInt >> 4 & 3));
        this.flags.Add((object) "SB_HUFF_RDW", (object) (flagsAsInt >> 6 & 3));
        this.flags.Add((object) "SB_HUFF_RDH", (object) (flagsAsInt >> 8 & 3));
        this.flags.Add((object) "SB_HUFF_RDX", (object) (flagsAsInt >> 10 & 3));
        this.flags.Add((object) "SB_HUFF_RDY", (object) (flagsAsInt >> 12 & 3));
        this.flags.Add((object) "SB_HUFF_RSIZE", (object) (flagsAsInt >> 14 & 1));
      }
    }
}
