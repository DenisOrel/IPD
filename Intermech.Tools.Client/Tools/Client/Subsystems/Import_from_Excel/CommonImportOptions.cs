// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.CommonImportOptions
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Flags]
public enum CommonImportOptions
{
  None = 0,
  SkipFirstRow = 1,
  IgnoreExistingObjectErrs = 16, // 0x00000010
  IgnoreExistingRelationErrs = 256, // 0x00000100
}
