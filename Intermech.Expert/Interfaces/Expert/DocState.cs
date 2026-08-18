// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.DocState
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>
/// These Trace Flags can be set to obtain needed trace info
/// </summary>
[TypeConverter(typeof (EnumDescConverter))]
[Category("Expert System")]
[Flags]
[Serializable]
public enum DocState
{
  NoFlags = 0,
  CondFalse = 1,
  Empty = 2,
  Ready = 4,
  Aligned = 8,
  Complect = 16, // 0x00000010
  Delayed = 32, // 0x00000020
  GenError = 64, // 0x00000040
  AccessError = 128, // 0x00000080
  CoWorker = 256, // 0x00000100
  DocLink = 512, // 0x00000200
  OtherComplect = 1024, // 0x00000400
  AnyError = AccessError | GenError, // 0x000000C0
  NotGenerating = AnyError | OtherComplect | Ready | Empty | CondFalse, // 0x000004C7
}
