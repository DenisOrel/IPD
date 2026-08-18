// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.BITMAPINFOHEADER
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal class BITMAPINFOHEADER
{
  public int biSize;
  public int biWidth;
  public int biHeight;
  public short biPlanes;
  public short biBitCount;
  public int biCompression;
  public int biSizeImage;
  public int biXPelsPerMeter;
  public int biYPelsPerMeter;
  public int biClrUsed;
  public int biClrImportant;
}
