// Decompiled with JetBrains decompiler
// Type: IMLauncher.AdditionalInfo
// Assembly: IMLauncher, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DAC2135C-3212-4DE0-9552-DF99FF4FD793
// Assembly location: D:\IPS\Client\IMLauncher.exe

using System;

#nullable disable
namespace IMLauncher;

[Flags]
public enum AdditionalInfo
{
  None = 0,
  IMClient = 1,
  Custom = 16, // 0x00000010
  Com = 32, // 0x00000020
}
