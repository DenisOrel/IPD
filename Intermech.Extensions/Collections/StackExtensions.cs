// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.StackExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

public static class StackExtensions
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Stack<T> PushRange<T>([NotNull] this Stack<T> stack, [NotNull] IEnumerable<T> elements)
  {
    foreach (T element in elements)
      stack.Push(element);
    return stack;
  }

  [NotNull]
  [ItemCanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<T> PopRange<T>([NotNull] this Stack<T> stack, int count)
  {
    while (count-- > 0 && stack.Count > 0)
      yield return stack.Pop();
  }
}
