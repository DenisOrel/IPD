// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwDAT
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

#nullable disable
namespace Intermech.Archives.ScanDocums;

internal enum TwDAT : short
{
  Null = 0,
  Capability = 1,
  Event = 2,
  Identity = 3,
  Parent = 4,
  PendingXfers = 5,
  SetupMemXfer = 6,
  SetupFileXfer = 7,
  Status = 8,
  UserInterface = 9,
  XferGroup = 10, // 0x000A
  TwunkIdentity = 11, // 0x000B
  CustomDSData = 12, // 0x000C
  DeviceEvent = 13, // 0x000D
  FileSystem = 14, // 0x000E
  PassThru = 15, // 0x000F
  ImageInfo = 257, // 0x0101
  ImageLayout = 258, // 0x0102
  ImageMemXfer = 259, // 0x0103
  ImageNativeXfer = 260, // 0x0104
  ImageFileXfer = 261, // 0x0105
  CieColor = 262, // 0x0106
  GrayResponse = 263, // 0x0107
  RGBResponse = 264, // 0x0108
  JpegCompression = 265, // 0x0109
  Palette8 = 266, // 0x010A
  ExtImageInfo = 267, // 0x010B
  SetupFileXfer2 = 769, // 0x0301
}
