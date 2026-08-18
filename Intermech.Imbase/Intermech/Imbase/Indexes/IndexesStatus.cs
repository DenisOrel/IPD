// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Indexes.IndexesStatus
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Indexes;

[Flags]
public enum IndexesStatus
{
  None = 0,
  Added = 1,
  Removed = 16, // 0x00000010
  Changed = 32, // 0x00000020
  Update = 64, // 0x00000040
  UpdateLinkData = 128, // 0x00000080
  UpdateTableData = 256, // 0x00000100
  UpdateAfterCopyMove = 512, // 0x00000200
}
