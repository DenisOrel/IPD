// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwCC
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

#nullable disable
namespace Intermech.Archives.ScanDocums;

internal enum TwCC : short
{
  Success = 0,
  Bummer = 1,
  LowMemory = 2,
  NoDS = 3,
  MaxConnections = 4,
  OperationError = 5,
  BadCap = 6,
  BadProtocol = 9,
  BadValue = 10, // 0x000A
  SeqError = 11, // 0x000B
  BadDest = 12, // 0x000C
  CapUnsupported = 13, // 0x000D
  CapBadOperation = 14, // 0x000E
  CapSeqError = 15, // 0x000F
  Denied = 16, // 0x0010
  FileExists = 17, // 0x0011
  FileNotFound = 18, // 0x0012
  NotEmpty = 19, // 0x0013
  PaperJam = 20, // 0x0014
  PaperDoubleFeed = 21, // 0x0015
  FileWriteError = 22, // 0x0016
  CheckDeviceOnline = 23, // 0x0017
}
