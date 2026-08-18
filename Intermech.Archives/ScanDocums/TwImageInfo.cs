// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwImageInfo
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Archives.ScanDocums;

[StructLayout(LayoutKind.Sequential, Pack = 2)]
internal class TwImageInfo
{
  public int XResolution;
  public int YResolution;
  public int ImageWidth;
  public int ImageLength;
  public short SamplesPerPixel;
  [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
  public short[] BitsPerSample;
  public short BitsPerPixel;
  public short Planar;
  public short PixelType;
  public short Compression;
}
