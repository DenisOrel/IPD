// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.UniqueList`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Collections;

public class UniqueList<T> : List<T>
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public new void Add([CanBeNull] T item)
  {
    if (this.Contains(item))
      return;
    base.Add(item);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public new void AddRange([NotNull] IEnumerable<T> collection)
  {
    foreach (T obj in collection)
    {
      if (!this.Contains(obj))
        base.Add(obj);
    }
  }
}
