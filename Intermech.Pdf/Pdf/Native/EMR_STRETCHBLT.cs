// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Native.EMR_STRETCHBLT
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Native
{
    internal struct EMR_STRETCHBLT
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
      public int cxSrc;
      public int cySrc;
    }
}
