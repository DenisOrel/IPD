// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Mbom.AddingToMbomStatuses
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using System;

#nullable disable
namespace Intermech.Search.Mbom;

[Flags]
public enum AddingToMbomStatuses
{
  None = 0,
  AllowAdding = 1,
  AddingError = 2,
  BindedEbom = 4,
  NotBindedEbom = 8,
  NotEbom = 16, // 0x00000010
  InMbomComposition = 32, // 0x00000020
  TotalCountError = 64, // 0x00000040
}
