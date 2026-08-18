// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.FindReplaceOptions
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Editors;

[Flags]
internal enum FindReplaceOptions
{
  None = 0,
  MatchCase = 1,
  WholeWord = 2,
  SearchUp = 4,
  RelaceAll = 8,
  FromCurrent = 16, // 0x00000010
  Selected = 32, // 0x00000020
}
