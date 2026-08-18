// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.HalftoneRegionFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class HalftoneRegionFlags : JBIG2BaseFlags
    {
      internal const string H_COMB_OP = "H_COMB_OP";
      internal const string H_DEF_PIXEL = "H_DEF_PIXEL";
      internal const string H_ENABLE_SKIP = "H_ENABLE_SKIP";
      internal const string H_MMR = "H_MMR";
      internal const string H_TEMPLATE = "H_TEMPLATE";

      public override void setFlags(int flagsAsInt)
      {
        this.flagsAsInt = flagsAsInt;
        this.flags.Add((object) "H_MMR", (object) (flagsAsInt & 1));
        this.flags.Add((object) "H_TEMPLATE", (object) (flagsAsInt >> 1 & 3));
        this.flags.Add((object) "H_ENABLE_SKIP", (object) (flagsAsInt >> 3 & 1));
        this.flags.Add((object) "H_COMB_OP", (object) (flagsAsInt >> 4 & 7));
        this.flags.Add((object) "H_DEF_PIXEL", (object) (flagsAsInt >> 7 & 1));
      }
    }
}
