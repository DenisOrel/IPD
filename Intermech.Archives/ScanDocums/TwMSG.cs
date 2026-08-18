// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwMSG
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

#nullable disable
namespace Intermech.Archives.ScanDocums;

internal enum TwMSG : short
{
  Null = 0,
  Get = 1,
  GetCurrent = 2,
  GetDefault = 3,
  GetFirst = 4,
  GetNext = 5,
  Set = 6,
  Reset = 7,
  QuerySupport = 8,
  XFerReady = 257, // 0x0101
  CloseDSReq = 258, // 0x0102
  CloseDSOK = 259, // 0x0103
  DeviceEvent = 260, // 0x0104
  CheckStatus = 513, // 0x0201
  OpenDSM = 769, // 0x0301
  CloseDSM = 770, // 0x0302
  OpenDS = 1025, // 0x0401
  CloseDS = 1026, // 0x0402
  UserSelect = 1027, // 0x0403
  DisableDS = 1281, // 0x0501
  EnableDS = 1282, // 0x0502
  EnableDSUIOnly = 1283, // 0x0503
  ProcessEvent = 1537, // 0x0601
  EndXfer = 1793, // 0x0701
  StopFeeder = 1794, // 0x0702
  ChangeDirectory = 2049, // 0x0801
  CreateDirectory = 2050, // 0x0802
  Delete = 2051, // 0x0803
  FormatMedia = 2052, // 0x0804
  GetClose = 2053, // 0x0805
  GetFirstFile = 2054, // 0x0806
  GetInfo = 2055, // 0x0807
  GetNextFile = 2056, // 0x0808
  Rename = 2057, // 0x0809
  Copy = 2058, // 0x080A
  AutoCaptureDir = 2059, // 0x080B
  PassThru = 2305, // 0x0901
}
