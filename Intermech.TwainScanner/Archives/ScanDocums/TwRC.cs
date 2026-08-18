// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwRC
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

#nullable disable
namespace Intermech.Archives.ScanDocums;

internal enum TwRC : short
{
  Success,
  Failure,
  CheckStatus,
  Cancel,
  DSEvent,
  NotDSEvent,
  XferDone,
  EndOfList,
  InfoNotSupported,
  DataNotAvailable,
}
