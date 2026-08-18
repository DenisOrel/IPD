// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationOfEnumerations
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

public static class EnumerationOfEnumerations
{
  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<T> IntersectAll<T>(
    [NotNull, ItemNotNull] this IEnumerable<IEnumerable<T>> enumerationOfEnumerations)
  {
    using (IEnumerator<IEnumerable<T>> enumerator = enumerationOfEnumerations.GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        IEnumerable<T> current1 = enumerator.Current;
        if (!(current1 is List<T> objList1))
          objList1 = new List<T>(current1);
        List<T> objList2 = objList1;
        int count = objList2.Count;
        while (count > 0 && enumerator.MoveNext())
        {
          IEnumerable<T> current2 = enumerator.Current;
          if (!(current2 is IReadOnlyCollection<T> objs))
            objs = (IReadOnlyCollection<T>) current2.ToList<T>();
          IReadOnlyCollection<T> source = objs;
          if (source.Count == 0)
            return (IReadOnlyList<T>) Array.Empty<T>();
          for (int index = count - 1; index >= 0; --index)
          {
            if (!source.Contains<T>(objList2[index]))
            {
              objList2.RemoveAt(index);
              --count;
            }
          }
        }
        return (IReadOnlyList<T>) objList2;
      }
    }
    return (IReadOnlyList<T>) Array.Empty<T>();
  }
}
