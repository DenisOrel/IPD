// Decompiled with JetBrains decompiler
// Type: Intermech.Common.StructComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Common;

public class StructComparer<T> : IComparer<T?>, IComparer<T>, IComparer where T : struct
{
  [NotNull]
  private readonly Func<T, T, int> _compareNotNullObjectsFunc;

  public StructComparer()
  {
    Intermech.Diagnostics.Check.ObjectState(typeof (IComparable<T>).IsAssignableFrom(typeof (T)), "Type T must support interface IComparable");
    this._compareNotNullObjectsFunc = new Func<T, T, int>(StructComparer<T>.CompareComparable);
  }

  public StructComparer([NotNull] Func<T, T, int> compareNotNullObjectsFunc)
  {
    this._compareNotNullObjectsFunc = compareNotNullObjectsFunc;
  }

  private static int CompareComparable(T x, T y) => ((IComparable<T>) x).CompareTo(y);

  public int Compare([CanBeNull] T? x, [CanBeNull] T? y)
  {
    if ((ValueType) x == (ValueType) y)
      return 0;
    if ((ValueType) y == null)
      return 1;
    return (ValueType) x == null ? -1 : this._compareNotNullObjectsFunc(x.Value, y.Value);
  }

  public int Compare(T x, T y) => this._compareNotNullObjectsFunc(x, y);

  public int Compare([CanBeNull] object x, [CanBeNull] object y)
  {
    if (x == y)
      return 0;
    if (y == null)
      return 1;
    if (x == null)
      return -1;
    return x is T obj1 ? (y is T obj2 ? this._compareNotNullObjectsFunc(obj1, obj2) : 1) : (y is T ? -1 : 0);
  }
}
