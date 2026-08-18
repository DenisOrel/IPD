// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ListFactory
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class ListFactory
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> Create<T>([CanBeNull] IEnumerable<T> enumeration, int capacity = 16 /*0x10*/)
  {
    Check.ArgumentIsZeroOrPositive(capacity, nameof (capacity));
    int result;
    List<T> objList = enumeration == null || !enumeration.TryGetCountOrCapacity<T>(out result) ? new List<T>(capacity) : new List<T>(result);
    if (enumeration != null)
      objList.AddRange(enumeration);
    return objList;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> Create<T>([CanBeNull, CanBeEmpty] params T[] items)
  {
    List<T> objList = items != null ? new List<T>(items.Length) : new List<T>();
    if (items != null)
      objList.AddRange((IEnumerable<T>) items);
    return objList;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<T> Create<T>([CanBeNull] T item)
  {
    return new List<T>(1) { item };
  }
}
