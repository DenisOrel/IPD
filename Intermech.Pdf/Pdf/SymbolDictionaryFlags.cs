// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.SymbolDictionaryFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class SymbolDictionaryFlags : JBIG2BaseFlags
    {
      public const string BITMAP_CC_RETAINED = "BITMAP_CC_RETAINED";
      public const string BITMAP_CC_USED = "BITMAP_CC_USED";
      public const string SD_HUFF = "SD_HUFF";
      public const string SD_HUFF_AGG_INST = "SD_HUFF_AGG_INST";
      public const string SD_HUFF_BM_SIZE = "SD_HUFF_BM_SIZE";
      public const string SD_HUFF_DH = "SD_HUFF_DH";
      public const string SD_HUFF_DW = "SD_HUFF_DW";
      public const string SD_R_TEMPLATE = "SD_R_TEMPLATE";
      public const string SD_REF_AGG = "SD_REF_AGG";
      public const string SD_TEMPLATE = "SD_TEMPLATE";

      public override void setFlags(int flagsAsInt)
      {
        this.flagsAsInt = flagsAsInt;
        this.flags.Add((object) "SD_HUFF", (object) (flagsAsInt & 1));
        this.flags.Add((object) "SD_REF_AGG", (object) (flagsAsInt >> 1 & 1));
        this.flags.Add((object) "SD_HUFF_DH", (object) (flagsAsInt >> 2 & 3));
        this.flags.Add((object) "SD_HUFF_DW", (object) (flagsAsInt >> 4 & 3));
        this.flags.Add((object) "SD_HUFF_BM_SIZE", (object) (flagsAsInt >> 6 & 1));
        this.flags.Add((object) "SD_HUFF_AGG_INST", (object) (flagsAsInt >> 7 & 1));
        this.flags.Add((object) "BITMAP_CC_USED", (object) (flagsAsInt >> 8 & 1));
        this.flags.Add((object) "BITMAP_CC_RETAINED", (object) (flagsAsInt >> 9 & 1));
        this.flags.Add((object) "SD_TEMPLATE", (object) (flagsAsInt >> 10 & 3));
        this.flags.Add((object) "SD_R_TEMPLATE", (object) (flagsAsInt >> 12 & 1));
      }
    }
}
