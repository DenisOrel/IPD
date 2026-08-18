// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.EMR_BITBLT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Native;

internal struct EMR_BITBLT
{
  public RECT rclBounds;
  public int xDest;
  public int yDest;
  public int cxDest;
  public int cyDest;
  public RASTER_CODE dwRop;
  public int xSrc;
  public int ySrc;
  public XFORM xformSrc;
  public int crBkColorSrc;
  public int iUsageSrc;
  public int offBmiSrc;
  public int cbBmiSrc;
  public int offBitsSrc;
  public uint cbBitsSrc;
}
