// Decompiled with JetBrains decompiler
// Type: GridViewExtensions.FilterErrorModes
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace GridViewExtensions;

[Flags]
public enum FilterErrorModes
{
  Off = 0,
  General = 1,
  ExceptionMessage = 2,
  StackTrace = 4,
  All = StackTrace | ExceptionMessage | General, // 0x00000007
}
