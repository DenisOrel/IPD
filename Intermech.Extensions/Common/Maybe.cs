// Decompiled with JetBrains decompiler
// Type: Intermech.Common.Maybe
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Common;

[ComVisible(true)]
public abstract class Maybe
{
  [ComVisible(true)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int Compare<TValue>(in Maybe<TValue> container1, in Maybe<TValue> container2) where TValue : class
  {
    return container1.HasValue ? (!container2.HasValue ? 1 : Comparer<TValue>.Default.Compare(container1._Value, container2._Value)) : (!container2.HasValue ? 0 : -1);
  }

  [ComVisible(true)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool Equals<TValue>(in Maybe<TValue> container1, in Maybe<TValue> container2) where TValue : class
  {
    if (!container1.HasValue)
      return !container2.HasValue;
    return container2.HasValue && EqualityComparer<TValue>.Default.Equals(container1._Value, container2._Value);
  }

  [CanBeNull]
  public static Type GetUnderlyingType([NotNull] Type nullableType)
  {
    Type underlyingType = (Type) null;
    if (nullableType.IsGenericType && !nullableType.IsGenericTypeDefinition && (object) nullableType.GetGenericTypeDefinition() == (object) typeof (Maybe<>))
      underlyingType = nullableType.GetGenericArguments()[0];
    return underlyingType;
  }
}
