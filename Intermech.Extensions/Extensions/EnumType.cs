// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumType
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[Obsolete("Используйте класс EnumHelper")]
public static class EnumType
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<TEnumerationType> GetValuesList<TEnumerationType>() where TEnumerationType : Enum
  {
    return (IReadOnlyList<TEnumerationType>) Enum.GetValues(typeof (TEnumerationType));
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<TEnumerationType> GetValuesEnumeration<TEnumerationType>() where TEnumerationType : Enum
  {
    return Enum.GetValues(typeof (TEnumerationType)).OfType<TEnumerationType>();
  }
}
