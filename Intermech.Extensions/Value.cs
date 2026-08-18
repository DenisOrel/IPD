// Decompiled with JetBrains decompiler
// Type: Intermech.Value
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech;

public static class Value
{
  [ContractAnnotation("=> value:NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T GetOrInit<T>(ref T? value, [NotNull, InstantHandle] Func<T> initMethod) where T : struct
  {
    if (value.HasValue)
      return value.Value;
    T orInit = initMethod();
    value = new T?(orInit);
    return orInit;
  }
}
