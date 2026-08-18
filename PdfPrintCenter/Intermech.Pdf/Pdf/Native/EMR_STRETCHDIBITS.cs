// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.EMR_STRETCHDIBITS
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct EMR_STRETCHDIBITS
{
  public RECT rclBounds;
  public int xDest;
  public int yDest;
  public int xSrc;
  public int ySrc;
  public int cxSrc;
  public int cySrc;
  public int offBmiSrc;
  public int cbBmiSrc;
  public int offBitsSrc;
  public uint cbBitsSrc;
  public int iUsageSrc;
  public uint dwRop;
  public int cxDest;
  public int cyDest;
}
