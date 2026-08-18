// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.TextRegionFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class TextRegionFlags : JBIG2BaseFlags
{
  public const string LOG_SB_STRIPES = "LOG_SB_STRIPES";
  public const string REF_CORNER = "REF_CORNER";
  public const string SB_COMB_OP = "SB_COMB_OP";
  public const string SB_DEF_PIXEL = "SB_DEF_PIXEL";
  public const string SB_DS_OFFSET = "SB_DS_OFFSET";
  public const string SB_HUFF = "SB_HUFF";
  public const string SB_R_TEMPLATE = "SB_R_TEMPLATE";
  public const string SB_REFINE = "SB_REFINE";
  public const string TRANSPOSED = "TRANSPOSED";

  public override void setFlags(int flagsAsInt)
  {
    this.flagsAsInt = flagsAsInt;
    this.flags.Add((object) "SB_HUFF", (object) (flagsAsInt & 1));
    this.flags.Add((object) "SB_REFINE", (object) (flagsAsInt >> 1 & 1));
    this.flags.Add((object) "LOG_SB_STRIPES", (object) (flagsAsInt >> 2 & 3));
    this.flags.Add((object) "REF_CORNER", (object) (flagsAsInt >> 4 & 3));
    this.flags.Add((object) "TRANSPOSED", (object) (flagsAsInt >> 6 & 1));
    this.flags.Add((object) "SB_COMB_OP", (object) (flagsAsInt >> 7 & 3));
    this.flags.Add((object) "SB_DEF_PIXEL", (object) (flagsAsInt >> 9 & 1));
    int num = flagsAsInt >> 10 & 31 /*0x1F*/;
    if ((num & 16 /*0x10*/) != 0)
      num |= -16;
    this.flags.Add((object) "SB_DS_OFFSET", (object) num);
    this.flags.Add((object) "SB_R_TEMPLATE", (object) (flagsAsInt >> 15 & 1));
  }
}
