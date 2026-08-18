// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PatternDictionaryFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class PatternDictionaryFlags : JBIG2BaseFlags
{
  public const string HD_MMR = "HD_MMR";
  public const string HD_TEMPLATE = "HD_TEMPLATE";

  public override void setFlags(int flagsAsInt)
  {
    this.flagsAsInt = flagsAsInt;
    this.flags.Add((object) "HD_MMR", (object) (flagsAsInt & 1));
    this.flags.Add((object) "HD_TEMPLATE", (object) (flagsAsInt >> 1 & 3));
  }
}
