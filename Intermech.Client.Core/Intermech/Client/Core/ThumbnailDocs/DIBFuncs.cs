
// Type: Intermech.Client.Core.ThumbnailDocs.DIBFuncs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core.ThumbnailDocs;

internal static class DIBFuncs
{
  [DllImport("gdi32.dll")]
  private static extern int StretchDIBits(
    IntPtr hdc,
    int XDest,
    int YDest,
    int nDestWidth,
    int nDestHeight,
    int XSrc,
    int YSrc,
    int nSrcWidth,
    int nSrcHeight,
    byte[] lpBits,
    byte[] lpBitsInfo,
    uint iUsage,
    uint dwRop);

  public static Bitmap CF_DIBV5ToBitmap(byte[] data)
  {
    GCHandle gcHandle = GCHandle.Alloc((object) data, GCHandleType.Pinned);
    try
    {
      DIBFuncs.BITMAPV5HEADER structure = (DIBFuncs.BITMAPV5HEADER) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (DIBFuncs.BITMAPV5HEADER));
      long sourceIndex = (long) structure.bV5Size + (long) ((1 << (int) structure.bV5BitCount) * 4);
      byte[] numArray = new byte[(long) data.Length - sourceIndex];
      Array.Copy((Array) data, sourceIndex, (Array) numArray, 0L, (long) numArray.Length);
      Bitmap bitmap = new Bitmap(structure.bV5Width, structure.bV5Height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        IntPtr hdc = graphics.GetHdc();
        DIBFuncs.StretchDIBits(hdc, 0, 0, structure.bV5Width, structure.bV5Height, 0, 0, structure.bV5Width, structure.bV5Height, numArray, data, 0U, 13369376U);
        graphics.ReleaseHdc(hdc);
      }
      return bitmap;
    }
    finally
    {
      gcHandle.Free();
    }
  }

  public struct BITMAPV5HEADER
  {
    public uint bV5Size;
    public int bV5Width;
    public int bV5Height;
    public ushort bV5Planes;
    public ushort bV5BitCount;
    public uint bV5Compression;
    public uint bV5SizeImage;
    public int bV5XPelsPerMeter;
    public int bV5YPelsPerMeter;
    public ushort bV5ClrUsed;
    public ushort bV5ClrImportant;
    public ushort bV5RedMask;
    public ushort bV5GreenMask;
    public ushort bV5BlueMask;
    public ushort bV5AlphaMask;
    public ushort bV5CSType;
    public IntPtr bV5Endpoints;
    public ushort bV5GammaRed;
    public ushort bV5GammaGreen;
    public ushort bV5GammaBlue;
    public ushort bV5Intent;
    public ushort bV5ProfileData;
    public ushort bV5ProfileSize;
    public ushort bV5Reserved;
  }
}
