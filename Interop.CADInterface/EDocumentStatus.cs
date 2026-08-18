// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.EDocumentStatus
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

#nullable disable
namespace Interop.CADInterface;

public enum EDocumentStatus
{
  DS_Unknown = -1, // 0xFFFFFFFF
  DS_CheckedIn = 0,
  DS_CheckedOut = 1,
  DS_CheckedOutByDifferentUser = 2,
  DS_Unregistered = 3,
  DS_Auxiliary = 4,
  DS_NotInWorkingDir = 1024, // 0x00000400
}
