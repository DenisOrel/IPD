// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.IListReadOnlyWrap
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Collections;

public static class IListReadOnlyWrap
{
  [NotNull]
  public static IReadOnlyList<T> WrapAsReadOnly<T>([NotNull] this IList<T> list)
  {
    return list is IReadOnlyList<T> objList ? objList : (IReadOnlyList<T>) new IList2IReadOnlyListAdapter<T>(list);
  }
}
