// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PageInformationFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class PageInformationFlags : JBIG2BaseFlags
{
  internal const string DEFAULT_COMBINATION_OPERATOR = "DEFAULT_COMBINATION_OPERATOR";
  internal const string DEFAULT_PIXEL_VALUE = "DEFAULT_PIXEL_VALUE";

  public override void setFlags(int flagAsInt)
  {
    this.flagsAsInt = flagAsInt;
    this.flags.Add((object) "DEFAULT_PIXEL_VALUE", (object) (flagAsInt >> 2 & 1));
    this.flags.Add((object) "DEFAULT_COMBINATION_OPERATOR", (object) (flagAsInt >> 3 & 3));
  }
}
