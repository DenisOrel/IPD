// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.GenericRegionFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

public class GenericRegionFlags : JBIG2BaseFlags
{
  public const string GB_TEMPLATE = "GB_TEMPLATE";
  public const string MMR = "MMR";
  public const string TPGDON = "TPGDON";

  public override void setFlags(int flagsAsInt)
  {
    this.flagsAsInt = flagsAsInt;
    this.flags.Add((object) "MMR", (object) (flagsAsInt & 1));
    this.flags.Add((object) "GB_TEMPLATE", (object) (flagsAsInt >> 1 & 3));
    this.flags.Add((object) "TPGDON", (object) (flagsAsInt >> 3 & 1));
  }
}
