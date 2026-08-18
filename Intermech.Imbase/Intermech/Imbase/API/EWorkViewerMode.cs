// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.EWorkViewerMode
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.API;

[Flags]
internal enum EWorkViewerMode
{
  WVM_All = 1,
  WVM_Alone = 0,
  WVM_Coating = 16, // 0x00000010
  WVM_Glue = 8,
  WVM_Material = 2,
  WVM_Oil = 32, // 0x00000020
  WVM_PaintCoating = 64, // 0x00000040
  WVM_Typesize = 4,
}
