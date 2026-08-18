// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SimpleComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class SimpleComparer<T> : IComparer<T>
{
  private static bool? _referenceType;
  [NotNull]
  private readonly SimpleComparer<T>.CompareMethodDelegate _compareMethod;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static bool IsReferenceType()
  {
    return SimpleComparer<T>._referenceType ?? (SimpleComparer<T>._referenceType = new bool?(!typeof (T).IsValueType)).Value;
  }

  public SimpleComparer(
    [NotNull] SimpleComparer<T>.CompareMethodDelegate compareMethod)
  {
    this._compareMethod = compareMethod;
  }

  public int Compare([CanBeNull] T first, [CanBeNull] T second)
  {
    if (SimpleComparer<T>.IsReferenceType())
    {
      bool flag1 = (object) first == null;
      bool flag2 = (object) second == null;
      if (flag1 & flag2)
        return 0;
      if (flag2)
        return 1;
      if (flag1)
        return -1;
    }
    return this._compareMethod(first, second);
  }

  public delegate int CompareMethodDelegate([NotNull] T first, [NotNull] T second);
}
