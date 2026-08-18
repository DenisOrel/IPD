// Decompiled with JetBrains decompiler
// Type: Intermech.Common.ObjectComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Common;

public class ObjectComparer<T> : IComparer<T>, IComparer where T : class
{
  [NotNull]
  private readonly Func<T, T, int> _compareNotNullObjectsFunc;

  public ObjectComparer()
  {
    Intermech.Diagnostics.Check.ObjectState(typeof (IComparable<T>).IsAssignableFrom(typeof (T)), "Type T must support interface IComparable");
    this._compareNotNullObjectsFunc = new Func<T, T, int>(ObjectComparer<T>.CompareComparable);
  }

  public ObjectComparer([NotNull] Func<T, T, int> compareNotNullObjectsFunc)
  {
    this._compareNotNullObjectsFunc = compareNotNullObjectsFunc;
  }

  private static int CompareComparable([CanBeNull] T x, [CanBeNull] T y)
  {
    if ((object) x == (object) y)
      return 0;
    if ((object) y == null)
      return 1;
    return (object) x == null ? -1 : ((IComparable<T>) (object) x).CompareTo(y);
  }

  public int Compare([CanBeNull] T x, [CanBeNull] T y)
  {
    if ((object) x == (object) y)
      return 0;
    if ((object) y == null)
      return 1;
    return (object) x == null ? -1 : this._compareNotNullObjectsFunc(x, y);
  }

  public int Compare([CanBeNull] object x, [CanBeNull] object y)
  {
    if (x == y)
      return 0;
    return !(x is T obj1) ? (y is T ? -1 : 0) : (!(y is T obj2) ? 1 : this._compareNotNullObjectsFunc(obj1, obj2));
  }
}
