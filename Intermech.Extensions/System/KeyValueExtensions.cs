// Decompiled with JetBrains decompiler
// Type: System.KeyValueExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace System;

public static class KeyValueExtensions
{
  [DebuggerHidden]
  [Pure]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void Deconstruct<TKey, TValue>(
    this KeyValuePair<TKey, TValue> keyValuePair,
    [NotNull] out TKey key,
    [CanBeNull] out TValue value)
  {
    key = keyValuePair.Key;
    value = keyValuePair.Value;
  }
}
