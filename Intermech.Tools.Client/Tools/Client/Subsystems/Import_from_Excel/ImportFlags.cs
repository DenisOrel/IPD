// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ImportFlags
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

[Obsolete]
[Flags]
public enum ImportFlags
{
  None = 0,
  SkipFirtsRow = 1,
  IgnoreExistingObjectErrs = 2,
  IgnoreExisitingRelationErrs = IgnoreExistingObjectErrs | SkipFirtsRow, // 0x00000003
}
