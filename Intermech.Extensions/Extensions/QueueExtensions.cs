// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.QueueExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class QueueExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Queue<T> EnqueueRange<T>([NotNull] this Queue<T> queue, [NotNull] IEnumerable<T> items)
  {
    foreach (T obj in items)
      queue.Enqueue(obj);
    return queue;
  }
}
