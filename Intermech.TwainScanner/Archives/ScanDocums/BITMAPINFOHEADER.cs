// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.BITMAPINFOHEADER
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

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
