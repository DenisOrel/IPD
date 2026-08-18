// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ScanDocums.TwRC
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

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
