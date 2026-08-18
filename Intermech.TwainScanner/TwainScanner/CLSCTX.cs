// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.CLSCTX
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;

#nullable disable
namespace Intermech.TwainScanner;

/// <summary>
/// Values from the CLSCTX enumeration are used in activation calls to
/// indicate the execution contexts in which an object is to be run. These
/// values are also used in calls to CoRegisterClassObject to indicate the
/// set of execution contexts in which a class object is to be made available
/// for requests to construct instances.
/// </summary>
[Flags]
internal enum CLSCTX : uint
{
  INPROC_SERVER = 1,
  INPROC_HANDLER = 2,
  LOCAL_SERVER = 4,
  INPROC_SERVER16 = 8,
  REMOTE_SERVER = 16, // 0x00000010
  INPROC_HANDLER16 = 32, // 0x00000020
  RESERVED1 = 64, // 0x00000040
  RESERVED2 = 128, // 0x00000080
  RESERVED3 = 256, // 0x00000100
  RESERVED4 = 512, // 0x00000200
  NO_CODE_DOWNLOAD = 1024, // 0x00000400
  RESERVED5 = 2048, // 0x00000800
  NO_CUSTOM_MARSHAL = 4096, // 0x00001000
  ENABLE_CODE_DOWNLOAD = 8192, // 0x00002000
  NO_FAILURE_LOG = 16384, // 0x00004000
  DISABLE_AAA = 32768, // 0x00008000
  ENABLE_AAA = 65536, // 0x00010000
  FROM_DEFAULT_CONTEXT = 131072, // 0x00020000
  ACTIVATE_32_BIT_SERVER = 262144, // 0x00040000
  ACTIVATE_64_BIT_SERVER = 524288, // 0x00080000
}
